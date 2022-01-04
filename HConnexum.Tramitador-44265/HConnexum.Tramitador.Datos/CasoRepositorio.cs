using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: CasoRepositorio.</summary>	
	public sealed class CasoRepositorio : RepositorioBase<Caso>
	{
		///<summary>Estatus de un caso.</summary>
		public enum CasoEstatus
		{
			Anulado = 164,
			Auditoria = 166,
			Cerrado = 163,
			EnProceso = 162,
			Pendiente = 161,
			Solicitado = 160,
			Suspendido = 165
		}

		class EstatusCaso
		{
			public const int AuditoriaCaso = 166;
		}

		class EstatusMov
		{
			public const int AuditoriaMov = 158;
		}

		class EstatusSolicitud
		{
			public const int AuditoriaSol = 272;
		}
		
		

		#region "ConstructoreS"

		///<summary>Constructor de la clase CasoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public CasoRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}

		#endregion "Constructores"
		
		#region DTO

		public string ObtenerOrigen(int idCaso)
		{
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			var tabCasos = this.udt.Sesion.CreateObjectSet<Caso>();

			var coleccion = (from tab1 in tabCasos
							 where tab1.Id == idCaso
							 select new CasoDTO
							 {
								 Id = tab1.Id,
								 XMLRespuesta = tab1.XMLRespuesta,
							 }).FirstOrDefault();

			if (coleccion != null)
			{
				XElement xmlRespuesta = XElement.Parse(coleccion.XMLRespuesta);
				coleccion.IdSusIntermedario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					 where p.Attribute(atributoNombre).Value.ToUpper() == @"IDSUSINTERMEDIARIO"
																					 select p.Attribute(atributoValor).Value).SingleOrDefault());
			}
			return coleccion.IdSusIntermedario;
		}

		public string ModificarEstatusAuditoria(int id, int idSolicitud, int idMovimiento, int usuarioActual, int idSuscriptor)
		{
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			
			if ((idSolicitud != 0) && (id != 0) && (idMovimiento != 0))
			{
				#region Modificacion del estatus del movimiento
				
				var listaMovimientos = (from mr in tabMovimiento
										where mr.IdCaso == id &&
											  mr.Id >= idMovimiento
										orderby mr.Id ascending
										select new
										{
											Estatus = mr.Estatus,
											IdPaso = mr.IdPaso,
											UsuarioAsignado = mr.UsuarioAsignado
										}).ToList();
				
				if (listaMovimientos.Count() > 0)
				{
					foreach (var item in listaMovimientos)
					{
						this.udt.IniciarTransaccion();
						MovimientoRepositorio repositorio = new MovimientoRepositorio(this.udt);
						Movimiento mov = repositorio.ObtenerPorId(idMovimiento);
						
						if ((item.UsuarioAsignado != null) && (mov.Estatus == 158))
						{
							mov.Estatus = 155;
							mov.ModificadoPor = usuarioActual;
							mov.FechaModificacion = DateTime.Now;
						}
						else if ((item.UsuarioAsignado == null) && (mov.Estatus == 158))
						{
							mov.Estatus = 154;
							mov.ModificadoPor = usuarioActual;
							mov.FechaModificacion = DateTime.Now;
						}
						this.udt.MarcarModificado(mov);
						this.udt.Commit();
					}
					
					#endregion Modificacion del estatus del movimiento
				}
			}
			else if ((idSolicitud != 0) && (id == 0) && (idMovimiento == 0))
			{
				#region Modificación de Estatus de la Solicitud
				
				this.udt.IniciarTransaccion();
				SolicitudRepositorio repositorio = new SolicitudRepositorio(this.udt);
				Solicitud sol = repositorio.ObtenerPorId(idSolicitud);
				sol.Estatus = 268;
				sol.FechaModificacion = DateTime.Now;
				sol.ModificadoPor = usuarioActual;
				this.udt.MarcarModificado(sol);
				this.udt.Commit();
				
				#endregion Modificación de Estatus de la Solicitud
			}
			else if ((idSolicitud != 0) && (id != 0) && (idMovimiento == 0))
			{
				#region Modificación de Estatus del caso
				
				int estatusprimerMovdelCaso = (from tab in tabCaso
											   join tabmov in tabMovimiento on tab.Id equals tabmov.IdCaso
											   where tab.Id == id &&
													 tab.IdSolicitud == idSolicitud &&
													 tab.IndEliminado == false &&
													 tab.IndVigente == true &&
													 tab.FechaValidez <= DateTime.Now &&
													 tabmov.IndEliminado == false &&
													 tabmov.IndVigente == true &&
													 tabmov.FechaValidez <= DateTime.Now
											   select tabmov.Estatus).FirstOrDefault();
				
				if (estatusprimerMovdelCaso != null)
				{
					this.udt.IniciarTransaccion();
					Caso tabcaso = this.ObtenerPorId(id);
					if (estatusprimerMovdelCaso != 154)
					{
						tabcaso.Estatus = 162;
						tabcaso.FechaModificacion = DateTime.Now;
						tabcaso.ModificadoPor = usuarioActual;
					}
					else 
					{
						tabcaso.Estatus = 161;
						tabcaso.FechaModificacion = DateTime.Now;
						tabcaso.ModificadoPor = usuarioActual;
					}
					this.udt.MarcarModificado(tabcaso);
					this.udt.Commit();
				}
				
				#endregion Modificación de Estatus del caso
			}
			return "Solicitud procesada";
		}
		
		///<summary>llena grid de consulta de caso sin supervisor </summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		///---Modificado
		public IEnumerable<CasoDTO> ObtenerDTO(string orden, int pagina, int registros, int? suscriptor, int? servicio, bool incluirMov, int? caso, int? casoExt, int? casoExt2, int? casoExt3, string ticket, string numDocTit, string asegurado, string intermediario, int? tipoBusqueda, string fechaDesde, string fechaHasta, int[] listaPoteMov, int? estatus, int idSuscriptorConectado)
		{
			ObjectParameter totalregistro = new ObjectParameter("TOTALREG", 0);
			List<ListaPoteConsultaCasos> listaCasos = new List<ListaPoteConsultaCasos>();
			
			using (BD_HC_Tramitador dataBase = new BD_HC_Tramitador())
			{
				listaCasos = dataBase.pa_PoteConsultaCasos(suscriptor, servicio, incluirMov, caso, casoExt, casoExt2, casoExt3, ticket, numDocTit, asegurado, intermediario, tipoBusqueda, fechaDesde, fechaHasta, "", pagina, registros, estatus, idSuscriptorConectado).ToList();
			}
			
			var coleccion = (from casos in listaCasos
							 select new CasoDTO
							 {
								 IdEncriptado = casos.IdEncriptado,
								 Origen = casos.ORIGEN,
								 IdCasoexterno2 = casos.Idcasoexterno2 == null ? "" : casos.Idcasoexterno2.ToString(),
								 NombreServicioSuscriptor = casos.NombreServicioSuscriptor,
								 OrigenesDB = casos.ORIGENDB,
								 Idcasoexterno = casos.Idcasoexterno,
								 ImgChat = casos.ImgChat,
								 Id = casos.Id,
								 Ticket = casos.Ticket,
								 NombreSolicitante = casos.NombreSolicitante,
								 NumDocSolicitante = (casos.TipDocSolicitante == "" ? " " : casos.TipDocSolicitante + "-") + casos.NumDocSolicitante,
								 NombreEstatusCaso = casos.NombreEstatusCaso,
								 total = casos.TotalCols
							 }).AsQueryable();
			
			if (coleccion.Count() > 0)
			{
				this.Conteo = coleccion.FirstOrDefault().total.Value;
			}
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		public IEnumerable<CasoDTO> ReimpresionComprobanteMovimiento(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idSuscriptor)
		{
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string nombreServcioCE = ConfigurationManager.AppSettings[@"NombreServicioCE"];
			string nombreServicioCartaAval = ConfigurationManager.AppSettings[@"NombreServicioCartaAval"];
			
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptores = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabServiciosTiposSucriptores = this.udt.Sesion.CreateObjectSet<ServiciosTiposSucriptor>();
			var tabServicio = this.udt.Sesion.CreateObjectSet<Servicio>();
			var tabSolicitudes = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabMov = this.udt.Sesion.CreateObjectSet<Movimiento>();
			
			string idSusIntermedario = @"IDSUSINTERMEDIARIO";
			string idCasoexterno2 = @"IDEXPEDIENTEWEB";
			string ultimoMovimiento = @"NOMTIPOMOV";
			string nombreServicio = @"NOMTIPOSERVICIO";
			string diagnostico = @"DIAGNOSTICO";
			string asegurado = @"NOMCOMPLETOBENEF";
			string cedula = @"NUMDOCBENEF";
			string intermediario = @"NOMSUSINTERMED";
			string estatusMovimiento = @"ESTATUSMOVIMIENTOWEB";
			
			IQueryable<CasoDTO> coleccion;
			List<CasoDTO> caso = new List<CasoDTO>();
			
			coleccion = (from C in tabCaso
						 join tabM in tabMov on C.Id equals tabM.IdCaso
						 join Fs in tabFlujosServicio on C.IdFlujoServicio equals Fs.Id
						 join SS in tabServiciosSuscriptores on Fs.IdServicioSuscriptor equals SS.Id
						 join STS in tabServiciosTiposSucriptores on SS.IdServicioTipoSuscriptor equals STS.Id
						 join S in tabServicio on STS.IdServicio equals S.Id
						 join ts in tabSolicitudes on C.IdSolicitud equals ts.Id
						 where tabM.IdSuscriptor == idSuscriptor &&
							   (S.Nombre.Contains(nombreServcioCE) || S.Nombre.Contains(nombreServicioCartaAval))
						 select new CasoDTO
						 {
							 Id = C.Id,
							 IdMovimiento = tabM.Id,
							 IndEliminado = C.IndEliminado,
							 XMLRespuesta = C.XMLRespuesta,
							 FechaSolicitud = ts.FechaCreacion,
							 SupportIncident = ts.IdCasoExterno3 == null ? string.Empty : ts.IdCasoExterno3
						 }).OrderByDescending(c => c.IdMovimiento).GroupBy(c => c.Id).Select(c => c.FirstOrDefault());
			
			foreach (CasoDTO item in coleccion)
			{
				XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
				item.IdSusIntermedario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == idSusIntermedario
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.IdCasoexterno2 = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			 where p.Attribute(atributoNombre).Value.ToUpper() == idCasoexterno2
																			 select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.TipoMovimiento = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			 where p.Attribute(atributoNombre).Value.ToUpper() == ultimoMovimiento.ToUpper()
																			 select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.NombreServicio = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			 where p.Attribute(atributoNombre).Value.ToUpper() == nombreServicio.ToUpper()
																			 select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.Diagnostico = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																		  where p.Attribute(atributoNombre).Value.ToUpper() == diagnostico.ToUpper()
																		  select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.Asegurado = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																		where p.Attribute(atributoNombre).Value.ToUpper() == asegurado.ToUpper()
																		select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == cedula
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			where p.Attribute(atributoNombre).Value.ToUpper() == intermediario
																			select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.NombreEstatusMovimiento = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					  where p.Attribute(atributoNombre).Value.ToUpper() == estatusMovimiento.ToUpper()
																					  select p.Attribute(atributoValor).Value).SingleOrDefault());
				if (item.TipoMovimiento.ToUpper() != "ACTIVACIÓN" && item.NombreEstatusMovimiento.ToUpper() != "PENDIENTE" && item.NombreEstatusMovimiento.ToUpper() != "RECHAZADO")
				{
					caso.Add(item);
				}
			}
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(caso.AsQueryable(), false);
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerAuditoriaDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			
			var coleccion = from tab in tabCaso
							join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
							join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							where tab.Estatus == 166
							orderby "it." + orden
							select new CasoDTO
							{
								Id = tab.Id,
								IndEliminado = tab.IndEliminado,
								IdFlujoServicio = tab.IdFlujoServicio,
								NombreServicioSuscriptor = tabSS.Nombre,
								NombreEstatusCaso = tabLV.NombreValor,
								Estatus = tab.Estatus,
								CreadorPor = tab.CreadorPor,
								FechaCreacion = tab.FechaCreacion,
								IdSuscriptor = tabFS.IdSuscriptor,
								NombreSolicitante = "",
								NumDocSolicitante = "",
								XMLRespuesta = tab.XMLRespuesta
							};

			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, false);
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		/// <summary> Llena el grid de POTE CASO</summary>
		/// <param name="orden"></param>
		/// <param name="pagina"></param>
		/// <param name="registros"></param>
		/// <param name="parametrosFiltro"></param>
		/// <param name="ListaPoteMov"></param>
		/// <returns></returns>
		public IEnumerable<PoteCasoDTO> ObtenerDTO(string orden, int pagina, int registros, int? suscriptor, int? servicio, bool incluirMov, int? caso, int? casoExt, int? casoExt2, int? casoExt3, string ticket, string numDocTit, string asegurado, string intermediario, int? tipoBusqueda, string fechaDesde, string fechaHasta, int[] listaPoteMov, int IdSuscriptorConectado)
		{
			XElement xml = this.convertirArrayIntEnXml(listaPoteMov);
			List<ListaPoteConsultaCasos> listaCasos = new List<ListaPoteConsultaCasos>();
			
			using (BD_HC_Tramitador dataBase = new BD_HC_Tramitador())
			{
				listaCasos = dataBase.pa_PoteConsultaCasos(suscriptor, servicio, incluirMov, caso, casoExt, casoExt2, casoExt3, ticket, numDocTit, asegurado, intermediario, tipoBusqueda, fechaDesde, fechaHasta, xml.ToString(), pagina, registros, null, IdSuscriptorConectado).ToList();
			}
			
			var coleccion = (from pote in listaCasos
							 select new PoteCasoDTO
							 {
								 IdExp_Web = pote.Idcasoexterno2 == null ? "" : pote.Idcasoexterno2.ToString(),
								 OrigenesDB = pote.ORIGENDB,
								 origen = pote.ORIGEN,
								 NombreServicioSuscriptor = pote.NombreServicioSuscriptor,
								 Reclamo = pote.Idcasoexterno == null ? "" : pote.Idcasoexterno.ToString(),
								 Id = pote.IdMovimiento == null ? 0 : pote.IdMovimiento.Value,
								 IdCaso = pote.Id,
								 Ticket = pote.Ticket == null ? "" : pote.Ticket,
								 NombreAsegurado = pote.NombreSolicitante,
								 TipDocAsegurado = (pote.TipDocSolicitante == "" ? " " : pote.TipDocSolicitante + "-") + pote.NumDocSolicitante,
								 FechaCreacionMov = pote.FechaCreacion.Value,
								 NombreSuscriptor = pote.NombreSuscriptor,
								 Actividad = pote.NomMovimiento,
								 Intermediario = pote.NombreIntermediario,
								 total = pote.TotalCols.Value,
								 ImgChat = pote.ImgChat,
								 IndEliminado = pote.IndEliminado.Value,
								 IdFlujoServicio = pote.IdFlujoServicio.Value,
								 NombreEstatusCaso = pote.NombreEstatusCaso,
								 Estatus = pote.Estatus.Value,
								 CreadoPor = pote.CreadoPor,
								 IdMovimiento = pote.IdMovimiento,
								 NombreMovimiento = pote.NomMovimiento,
								 SupportIncident = pote.Idcasoexterno3,
								 IdEncriptado = pote.IdEncriptado,
								 NumDocTit = pote.NumDocTit
							 }).AsQueryable();
			
			if (coleccion.Count() > 0)
			{
				this.Conteo = coleccion.FirstOrDefault().total;
			}
			return UtilidadesDTO<PoteCasoDTO>.EncriptarId(coleccion);
		}
		
		private XElement convertirArrayIntEnXml(int[] array)
		{
			string XML = "<MOVIMIENTOS>";
			foreach (int item in array)
			{
				if (!string.IsNullOrEmpty(item.ToString()))
				{
					XML = string.Format("{0}<ID>{1}</ID>", XML, item.ToString());
				}
			}
			XML = string.Format("{0}</MOVIMIENTOS>", XML);
			XElement xmlRespuesta = XElement.Parse(XML);
			return xmlRespuesta;
		}
		
		public IEnumerable<PoteCasoDTO> ObtenerSuscriptorespaPoteDTO(int[] ListaPoteMov, string FiltrodeBusqueda)
		{
			#region Casos HC2

			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMov = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabSus = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabSolicitud = this.udt.Sesion.CreateObjectSet<Solicitud>();
			List<PoteCasoDTO> caso = new List<PoteCasoDTO>();
			
			IQueryable<PoteCasoDTO> coleccion = (from tab in tabCaso
												 join tabSol in tabSolicitud on tab.IdSolicitud equals tabSol.Id
												 join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
												 join tabM in tabMov on tab.Id equals tabM.IdCaso
												 join tabS in tabSus on tabFS.IdSuscriptor equals tabS.Id
												 where ListaPoteMov.Contains(tabM.Id) &&
													   tab.IndEliminado == false &&
													   tab.IndVigente == true &&
													   tab.FechaValidez <= DateTime.Now &&
													   tabFS.IndEliminado == false &&
													   tabFS.IndVigente == true &&
													   tabFS.FechaValidez <= DateTime.Now &&
													   tabM.IndEliminado == false &&
													   tabM.IndVigente == true &&
													   tabM.FechaValidez <= DateTime.Now &&
													   tabS.IndEliminado == false &&
													   tabS.IndVigente == true &&
													   tabS.FechaValidez <= DateTime.Now &&
													   (tab.Estatus == 161 || tab.Estatus == 162)
												 orderby tabM.FechaCreacion
												 select new PoteCasoDTO
												 {
													 IdSuscriptor = tabFS.IdSuscriptor,
													 NombreSuscriptor = tabS.Nombre.Trim()
												 }).Distinct();
			
			coleccion = UtilidadesDTO<PoteCasoDTO>.FiltrarColeccionEliminacion(coleccion, false);
			
			#endregion
			
			#region Casos HC1
			
			List<ListaPoteCasosExternosSuscriptores> listaCasosExternos = new List<ListaPoteCasosExternosSuscriptores>();
			using (BD_HC_Tramitador dataBase = new BD_HC_Tramitador())
			{
				listaCasosExternos = dataBase.PA_PoteCasosExternosSuscriptores(FiltrodeBusqueda).ToList();
			}
			var coleccionHC1 = (from CExt in listaCasosExternos
								select new PoteCasoDTO
								{
									NombreSuscriptor = CExt.Nombre,
									IdSuscriptor = CExt.IdSuscriptor ?? 0,
								}).AsQueryable();
			
			#endregion
			
			var coleccion1 = coleccion.Union(coleccionHC1);
			return coleccion1;
		}
		
		/// <summary>
		/// Obtiene suscriptores de los casos que dado un idsuscriptor este pueda ser dueño o participe de los mismos.
		/// </summary>
		/// <param name="idSuscriptor"></param>
		/// <param name="FiltrodeBusqueda"></param>
		/// <returns></returns>
		public IEnumerable<CasoDTO> ObtenerSuscriptoresPaConsultaDTO(int IdSuscriptor, string FiltrodeBusqueda)
		{
			#region Casos HC2
			
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMov = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabSus = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabSolicitud = this.udt.Sesion.CreateObjectSet<Solicitud>();
			List<CasoDTO> caso = new List<CasoDTO>();
			
			var bExisteMov = (from tab in tabCaso
							  join mov in tabMov on tab.Id equals mov.IdCaso
							  where tab.Id == mov.IdCaso && mov.IdSuscriptor == IdSuscriptor
							  select tab.Id).ToList();
			
			IQueryable<CasoDTO> coleccion = (from tab in tabCaso
											 join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
											 join tabS in tabSus on tabFS.IdSuscriptor equals tabS.Id
											 where tab.Estatus != 330 &&
												   (tabFS.IdSuscriptor == IdSuscriptor || bExisteMov.Contains(tab.Id)) &&
												   (tab.Estatus == 161 || tab.Estatus == 162)
											 select new CasoDTO
											 {
												 IdSuscriptor = tabFS.IdSuscriptor,
												 NombreSuscriptor = tabS.Nombre.Trim()
											 }).Distinct();
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, false);
			
			#endregion
			
			#region Casos HC1
			
			List<ListaPoteCasosExternosSuscriptores> listaCasosExternos = new List<ListaPoteCasosExternosSuscriptores>();
			using (BD_HC_Tramitador dataBase = new BD_HC_Tramitador())
			{
				listaCasosExternos = dataBase.PA_PoteCasosExternosSuscriptores(FiltrodeBusqueda).ToList();
			}
			var coleccionHC1 = (from CExt in listaCasosExternos
								select new CasoDTO
								{
									NombreSuscriptor = CExt.Nombre,
									IdSuscriptor = CExt.IdSuscriptor ?? 0,
								}).AsQueryable();
			
			#endregion
			
			var coleccion1 = coleccion.Union(coleccionHC1);
			return coleccion1;
		}
		
		public IEnumerable<PoteCasoDTO> ObtenerSuscriptoresPote(int[] ListaPoteMov)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMov = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabSus = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			
			IQueryable<PoteCasoDTO> coleccion = (from tab in tabCaso
												 join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
												 join tabM in tabMov on tab.Id equals tabM.IdCaso
												 join tabS in tabSus on tabFS.IdSuscriptor equals tabS.Id
												 where ListaPoteMov.Contains(tabM.Id) &&
													   tab.IndEliminado == false &&
													   tab.IndVigente == true &&
													   tab.FechaValidez <= DateTime.Now &&
													   tabFS.IndEliminado == false &&
													   tabFS.IndVigente == true &&
													   tabFS.FechaValidez <= DateTime.Now &&
													   tabM.IndEliminado == false &&
													   tabM.IndVigente == true &&
													   tabM.FechaValidez <= DateTime.Now &&
													   tabS.IndEliminado == false &&
													   tabS.IndVigente == true &&
													   tabS.FechaValidez <= DateTime.Now
												 select new PoteCasoDTO
												 {
													 IdCaso = tab.Id,
													 IndEliminado = tab.IndEliminado,
													 IdSuscriptor = tabFS.IdSuscriptor,
													 NombreSuscriptor = tabS.Nombre,
												 }).GroupBy(x => x.IdCaso).Select(x => x.FirstOrDefault());
			
			coleccion = UtilidadesDTO<PoteCasoDTO>.FiltrarColeccionEliminacion(coleccion, false);
			return coleccion;
		}
		
		public IEnumerable<ConsultaOpinionMedicaDTO> ObtenerCasosOpinionMedicaDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int IdSuscriptor)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMov = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabSus = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabServiciosSuscriptores = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabServiciosTiposSucriptores = this.udt.Sesion.CreateObjectSet<ServiciosTiposSucriptor>();
			var tabServicio = this.udt.Sesion.CreateObjectSet<Servicio>();
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string diagnostico = "DIAGNOSTICO";
			string Fechasolicitud = "FECHASOLICITUD";
			string nomAseg = "NOMASEG";
			string SupportIncident = "SUPPORTINCIDENT";
			string Numdocaseg = "NUMDOCASEG";
			//string filtroFechas = "";

			int filtroIdCaso = 0;
			string filtroAsegurado = null;
			string filtrosFecha = "";
			DateTime? fechaInicial = null;
			DateTime? fechaFinal = null;

			foreach (var item in parametrosFiltro)
			{
				if (item.Campo == "Id")
				{
					filtroIdCaso = int.Parse(item.Valor.ToString());
				}
				
				if (item.Campo == "Asegurado")
				{
					filtroAsegurado = (item.Valor.ToString());
				}
				
				if ((item.Campo == "FechaCreacion") && (item.Operador == "GreaterThanOrEqualTo"))
				{
					fechaInicial = DateTime.Parse(item.Valor.ToString());
				}

				if ((item.Campo == "FechaCreacion") && (item.Operador == "LessThanOrEqualTo"))
				{
					fechaFinal = DateTime.Parse(item.Valor.ToString()).AddMinutes(1439);
				}
			}
			
			IQueryable<ConsultaOpinionMedicaDTO> coleccion;
			List<ConsultaOpinionMedicaDTO> caso = new List<ConsultaOpinionMedicaDTO>();
			
			
			if (IdSuscriptor == 5)
			{
				coleccion = (from C in tabCaso
							 join M in tabMov on C.Id equals M.IdCaso
							 join Fs in tabFlujosServicio on C.IdFlujoServicio equals Fs.Id
							 join SS in tabServiciosSuscriptores on Fs.IdServicioSuscriptor equals SS.Id
							 join STS in tabServiciosTiposSucriptores on SS.IdServicioTipoSuscriptor equals STS.Id
							 join S in tabServicio on STS.IdServicio equals S.Id
							 where C.Estatus == 163 && S.Id == 1
							 select new ConsultaOpinionMedicaDTO
							 {
								 Id = C.Id,
								 IdMovimiento = M.Id,
								 SuscriptorFlujoServicio = Fs.IdSuscriptor,
								 IndEliminado = C.IndEliminado,
								 XMLRespuesta = C.XMLRespuesta,
								 Diagnostico = "",
								 Fechasolicitud = "",
								 nomAseg = "",
								 SupportIncident = "",
								 NumDocSolicitante = "",
								 FechaCreacion = C.FechaCreacion
							 }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());

				foreach (ConsultaOpinionMedicaDTO item in coleccion)
				{
					if (!string.IsNullOrWhiteSpace(item.XMLRespuesta))
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
						item.Diagnostico = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				  where p.Attribute(atributoNombre).Value.ToUpper() == diagnostico.ToUpper()
																				  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.Fechasolicitud = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					 where p.Attribute(atributoNombre).Value.ToUpper() == Fechasolicitud.ToUpper()
																					 select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.nomAseg = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			  where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																			  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.SupportIncident = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					  where p.Attribute(atributoNombre).Value.ToUpper() == SupportIncident.ToUpper()
																					  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == Numdocaseg
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						caso.Add(item);
					}
				}
				
				coleccion = UtilidadesDTO<ConsultaOpinionMedicaDTO>.FiltrarColeccionEliminacion(coleccion, false);
				coleccion = UtilidadesDTO<ConsultaOpinionMedicaDTO>.FiltrarPaginar(caso.AsQueryable(), pagina, registros, parametrosFiltro);
				this.Conteo = UtilidadesDTO<ConsultaOpinionMedicaDTO>.Conteo;
			}
			else //Si no es nubise.
			{
				caso.Clear();
				coleccion = (from C in tabCaso
							 join M in tabMov on C.Id equals M.IdCaso
							 join Fs in tabFlujosServicio on C.IdFlujoServicio equals Fs.Id
							 join SS in tabServiciosSuscriptores on Fs.IdServicioSuscriptor equals SS.Id
							 join STS in tabServiciosTiposSucriptores on SS.IdServicioTipoSuscriptor equals STS.Id
							 join S in tabServicio on STS.IdServicio equals S.Id
							 where C.Estatus == 163 && S.Id == 1 && Fs.IdSuscriptor == IdSuscriptor &&
							 C.IndEliminado == false &&
							 C.IndVigente == true &&
							 C.FechaValidez <= DateTime.Now &&
							 M.IndEliminado == false &&
							 M.IndVigente == true &&
							 M.FechaValidez <= DateTime.Now &&
							 Fs.IndEliminado == false &&
							 Fs.IndVigente == true &&
							 Fs.FechaValidez <= DateTime.Now
							 select new ConsultaOpinionMedicaDTO
							 {
								 Id = C.Id,
								 IdMovimiento = M.Id,
								 SuscriptorFlujoServicio = Fs.IdSuscriptor,
								 IndEliminado = C.IndEliminado,
								 XMLRespuesta = C.XMLRespuesta,
								 Diagnostico = "",
								 Fechasolicitud = "",
								 nomAseg = "",
								 SupportIncident = "",
								 NumDocSolicitante = "",
								 FechaCreacion = C.FechaCreacion
							 }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());
				
				if (filtroIdCaso != 0)
				{
					coleccion = coleccion.Where(x => x.Id == filtroIdCaso);
				}
				
				if (filtroAsegurado != null)
				{
					coleccion = coleccion.Where(x => x.nomAseg == filtroAsegurado);
				}
				
				if ((fechaInicial != null) && (fechaFinal != null))
				{
					coleccion = coleccion.Where(x => (x.FechaCreacion >= fechaInicial) && (x.FechaCreacion <= fechaFinal));
				}
				else if ((fechaInicial != null) && (fechaFinal == null))
				{
					coleccion = coleccion.Where(x => (x.FechaCreacion >= fechaInicial));
				}
				
				
				foreach (ConsultaOpinionMedicaDTO item in coleccion)
				{
					if (!string.IsNullOrWhiteSpace(item.XMLRespuesta))
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
						item.Diagnostico = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				  where p.Attribute(atributoNombre).Value.ToUpper() == diagnostico.ToUpper()
																				  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.Fechasolicitud = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					 where p.Attribute(atributoNombre).Value.ToUpper() == Fechasolicitud.ToUpper()
																					 select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.nomAseg = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			  where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																			  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.SupportIncident = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					  where p.Attribute(atributoNombre).Value.ToUpper() == SupportIncident.ToUpper()
																					  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == Numdocaseg
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						caso.Add(item);
					}
				}
				coleccion = UtilidadesDTO<ConsultaOpinionMedicaDTO>.FiltrarColeccionEliminacion(coleccion, false);
				coleccion = UtilidadesDTO<ConsultaOpinionMedicaDTO>.FiltrarPaginar(caso.AsQueryable(), pagina, registros, parametrosFiltro);
				this.Conteo = UtilidadesDTO<ConsultaOpinionMedicaDTO>.Conteo;
				
				if (this.Conteo == 0)
				{
					caso.Clear();
					coleccion = (from C in tabCaso
								 join tabM in tabMov on C.Id equals tabM.IdCaso
								 where tabM.Estatus == 156 && tabM.IdSuscriptor == IdSuscriptor &&
								 C.IndEliminado == false &&
								 C.IndVigente == true &&
								 C.FechaValidez <= DateTime.Now &&
								 tabM.IndEliminado == false &&
								 tabM.IndVigente == true &&
								 tabM.FechaValidez <= DateTime.Now
								 select new ConsultaOpinionMedicaDTO
								 {
									 Id = C.Id,
									 IdMovimiento = tabM.Id,
									 IndEliminado = C.IndEliminado,
									 XMLRespuesta = C.XMLRespuesta,
									 Diagnostico = "",
									 Fechasolicitud = "",
									 nomAseg = "",
									 SuscriptorMovimiento = tabM.IdSuscriptor,
									 SupportIncident = "",
									 NumDocSolicitante = "",
									 FechaCreacion = C.FechaCreacion
								 }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());

					if (filtroIdCaso != 0)
					{
						coleccion = coleccion.Where(x => x.Id == filtroIdCaso);
					}
					if (filtroAsegurado != null)
					{
						coleccion = coleccion.Where(x => x.nomAseg == filtroAsegurado);
					}
					if ((fechaInicial != null) && (fechaFinal != null))
					{
						coleccion = coleccion.Where(x => (x.FechaCreacion >= fechaInicial) && (x.FechaCreacion <= fechaFinal));
					}
					else if ((fechaInicial != null) && (fechaFinal == null))
					{
						coleccion = coleccion.Where(x => (x.FechaCreacion >= fechaInicial));
					}
					
					foreach (ConsultaOpinionMedicaDTO item in coleccion)
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
						item.Diagnostico = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				  where p.Attribute(atributoNombre).Value.ToUpper() == diagnostico.ToUpper()
																				  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.Fechasolicitud = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					 where p.Attribute(atributoNombre).Value.ToUpper() == Fechasolicitud.ToUpper()
																					 select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.nomAseg = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			  where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																			  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.SupportIncident = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					  where p.Attribute(atributoNombre).Value.ToUpper() == SupportIncident.ToUpper()
																					  select p.Attribute(atributoValor).Value).SingleOrDefault());
						item.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == Numdocaseg
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						caso.Add(item);
					}
					coleccion = UtilidadesDTO<ConsultaOpinionMedicaDTO>.FiltrarColeccionEliminacion(coleccion, false);
					coleccion = UtilidadesDTO<ConsultaOpinionMedicaDTO>.FiltrarPaginar(caso.AsQueryable(), pagina, registros, parametrosFiltro);
					this.Conteo = UtilidadesDTO<ConsultaOpinionMedicaDTO>.Conteo;
				}
			}
			return UtilidadesDTO<ConsultaOpinionMedicaDTO>.EncriptarId(coleccion.AsQueryable());
		}
		
		public IEnumerable<ConsultaCartaAvalDTO> ObtenerCasosCartaAvalDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idSuscriptor)
		{
			string[] estatusCaso = ConfigurationManager.AppSettings[@"NombreValorCortoEstatusCartaAval"].Split(';');
			string nombreServcioCartaAval = ConfigurationManager.AppSettings[@"NombreServicioCartaAval"];
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptores = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>(); 
			var tabServiciosTiposSucriptores = this.udt.Sesion.CreateObjectSet<ServiciosTiposSucriptor>();
			var tabServicio = this.udt.Sesion.CreateObjectSet<Servicio>();
			var tabSolicitudes = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabMov = this.udt.Sesion.CreateObjectSet<Movimiento>();
			
			string diagnostico = @"DIAGNOSTICO";
			string nomAseg = @"NOMCOMPLETOBENEF";
			string numdocaseg = @"NUMDOCBENEF";
			string activacionCA = @"ACTIVACIONCA";
			string intermediario = @"NOMSUSINTERMED";
			string TipoMov = @"TIPOMOV";
			
			IQueryable<ConsultaCartaAvalDTO> coleccion;
			List<ConsultaCartaAvalDTO> caso = new List<ConsultaCartaAvalDTO>();
			
			coleccion = (from C in tabCaso
						 join tabM in tabMov on C.Id equals tabM.IdCaso
						 join Fs in tabFlujosServicio on C.IdFlujoServicio equals Fs.Id
						 join SS in tabServiciosSuscriptores on Fs.IdServicioSuscriptor equals SS.Id
						 join STS in tabServiciosTiposSucriptores on SS.IdServicioTipoSuscriptor equals STS.Id
						 join S in tabServicio on STS.IdServicio equals S.Id
						 join ts in tabSolicitudes on C.IdSolicitud equals ts.Id
						 join lv in tabListaValor on C.Estatus equals lv.Id
						 where estatusCaso.Contains(lv.NombreValorCorto) &&
							   S.Nombre.Contains(nombreServcioCartaAval) &&
							   tabM.IdSuscriptor == idSuscriptor
						 select new ConsultaCartaAvalDTO
						 {
							 Id = C.Id,
							 SuscriptorFlujoServicio = Fs.IdSuscriptor,
							 IndEliminado = C.IndEliminado,
							 XMLRespuesta = C.XMLRespuesta,
							 Fechasolicitud = ts.FechaCreacion,
							 SupportIncident = ts.IdCasoExterno3 == null ? string.Empty : ts.IdCasoExterno3
						 }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());
			
			foreach (ConsultaCartaAvalDTO item in coleccion)
			{
				XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
				item.Diagnostico = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																		  where p.Attribute(atributoNombre).Value.ToUpper() == diagnostico.ToUpper()
																		  select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.nomAseg = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																	  where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																	  select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == numdocaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.ActivacionCA = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																		   where p.Attribute(atributoNombre).Value.ToUpper() == activacionCA
																		   select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			where p.Attribute(atributoNombre).Value.ToUpper() == intermediario
																			select p.Attribute(atributoValor).Value).SingleOrDefault());
				item.TipoMov = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																	  where p.Attribute(atributoNombre).Value.ToUpper() == TipoMov
																	  select p.Attribute(atributoValor).Value).SingleOrDefault());
				if (item.TipoMov != "VERIFICACION")
				{
					caso.Add(item);
				}
			}
			
			coleccion = UtilidadesDTO<ConsultaCartaAvalDTO>.FiltrarColeccionEliminacion(caso.AsQueryable(), false);
			coleccion = UtilidadesDTO<ConsultaCartaAvalDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<ConsultaCartaAvalDTO>.Conteo;
			return UtilidadesDTO<ConsultaCartaAvalDTO>.EncriptarId(coleccion.AsQueryable());
		}

		


		public IEnumerable<CasoDTO> ObtenerSolicitudesEnAuditoria(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idSuscriptor, int idServiciosuscriptor)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabSolicitud = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabServiciosTpSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosTiposSucriptor>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string nomAseg = @"Nomaseg";
			string nomContratante = @"nomcontratante";
			string nomAsegurado = @"Nomcompletobenef";

			string strcampo = string.Empty;
			int valor;
			valor = 0;
			foreach (var itemfiltro in parametrosFiltro)
			{
				strcampo = itemfiltro.Campo;
				if (strcampo == "IdSolicitud")
				{
					valor = (int)itemfiltro.Valor;
					break;
				}
			}

			List<CasoDTO> caso = new List<CasoDTO>();

			#region MOVIMIENTOS

			var movimientos = from tabMov in tabMovimiento
							  join tab in tabCaso on tabMov.IdCaso equals tab.Id

							  join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
							  join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							  join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							  where tab.IdSolicitud == valor &&
									tabMov.Estatus == EstatusMov.AuditoriaMov &&
									tabMov.IdSuscriptor == idSuscriptor &&
									tabMov.IndEliminado == false &&
									tabMov.IndVigente == true &&
									tabMov.FechaValidez <= DateTime.Now &&
									tab.IndEliminado == false &&
									tab.IndVigente == true &&
									tab.FechaValidez <= DateTime.Now &&
									tabFS.IdServicioSuscriptor == idServiciosuscriptor &&
									tabFS.IndEliminado == false &&
									tabFS.IndVigente == true &&
									tabFS.FechaValidez <= DateTime.Now &&
									tabSS.IndEliminado == false &&
									tabSS.IndVigente == true &&
									tabSS.FechaValidez <= DateTime.Now &&
									tabST.IndEliminado == false &&
									tabST.IndVigente == true &&
									tabST.FechaValidez <= DateTime.Now
							  select new CasoDTO
							  {
								  Id = tab.Id,
								  IdSolicitud = tab.IdSolicitud,
								  IdMovimiento = tabMov.Id,
								  Movimiento = tabMov.Nombre,
								  XMLRespuesta = "",
								  IdServicio = 0,
								  NombreSolicitante = "",
								  Intermediario = "",
								  IdPaso = tabMov.IdPaso,
								  FechaCreacion = tabMov.FechaCreacion
							  };

			if (movimientos.Count() > 0)
			{
				foreach (CasoDTO item in movimientos)
				{
					var listaMovimientos = (from mr in tabMovimiento
											where mr.IdCaso == item.Id &&
												  mr.Id >= item.IdMovimiento &&
												  mr.IdPaso == item.IdPaso &&
												  mr.IndEliminado == false &&
												  mr.IndVigente == true &&
												  mr.FechaValidez <= DateTime.Now
											orderby mr.Id ascending
											select new
											{
												Id = mr.Id,
												Estatus = mr.Estatus,
												IdPaso = mr.IdPaso,
											}).ToList();

					if (listaMovimientos.Count() > 1)
					{
						foreach (var item2 in listaMovimientos)
						{
							if (item.IdPaso != item2.IdPaso && item2.Estatus != EstatusMov.AuditoriaMov)
							{
								caso.Add(item);
								item.Intermediario = item.Movimiento.ToString();
							}
						}
					}
					else
					{
						caso.Add(item);
						item.Intermediario = item.Movimiento.ToString();
					}
				}


			}
			movimientos = UtilidadesDTO<CasoDTO>.FiltrarPaginar(movimientos, pagina, registros, parametrosFiltro);
			#endregion MOVIMIENTOS


			#region SOLICITUDES

			var solicitudes = from tabSol in tabSolicitud
							  join tabFS in tabFlujosServicio on tabSol.IdFlujoServicio equals tabFS.Id
							  join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							  join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							  where tabSol.Id == valor &&
									tabSol.IndVigente == true &&
									tabSol.IdServicioSuscriptor == idServiciosuscriptor &&
									tabSol.Estatus == EstatusSolicitud.AuditoriaSol &&
									tabSol.FechaValidez <= DateTime.Now &&
									tabFS.IndVigente == true &&
									tabFS.IdSuscriptor == idSuscriptor &&
									tabFS.FechaValidez <= DateTime.Now &&
									tabSS.IndVigente == true &&
									tabSS.FechaValidez <= DateTime.Now &&
									tabST.IndVigente == true &&
									tabST.FechaValidez <= DateTime.Now
							  //orderby tabSol.Id
							  select new CasoDTO
							  {
								  Id = 0,
								  IdSolicitud = tabSol.Id,
								  IdMovimiento = 0,
								  Movimiento = "",
								  XMLRespuesta = tabSol.XMLSolicitud,
								  IdServicio = tabST.IdServicio,
								  NombreSolicitante = "",
								  Intermediario = "",
								  IdPaso = 0,
								  FechaCreacion = tabSol.FechaCreacion
							  };

			if (solicitudes.Count() > 0)
			{
				foreach (CasoDTO item in solicitudes)
				{
					if (!string.IsNullOrWhiteSpace(item.XMLRespuesta))
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);

						if ((item.IdServicio == 1) || (item.IdServicio == 49))
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());

							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == this.Intermediario.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						else if (item.IdServicio == 33)
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAsegurado.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());

							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == nomContratante.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						caso.Add(item);
					}
					item.Intermediario = string.Format("{0} - {1}", item.NombreSolicitante.ToString(), item.Intermediario.ToString());
				}
			}

			#endregion SOLICITUDES

			var solicitudesMov = movimientos.Union(solicitudes.OrderBy(m => m.IdSolicitud));
			//var solicitudesMov = solicitudes;	

			#region CASOS

			var listacasos = from tab in tabCaso
							 join tabSol in tabSolicitud on tab.IdSolicitud equals tabSol.Id
							 join tabFS in tabFlujosServicio on tabSol.IdFlujoServicio equals tabFS.Id
							 join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							 join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							 where tabFS.IndVigente == true &&
								   tabSS.IndVigente == true &&
								   tabSol.IndVigente == true &&
								   tabST.IndVigente == true &&
								   tab.IdSolicitud == valor &&
								   tab.Estatus == EstatusCaso.AuditoriaCaso &&
								   tabFS.IdSuscriptor == idSuscriptor &&
								   tabSol.IdServicioSuscriptor == idServiciosuscriptor &&
								   tabFS.FechaValidez <= DateTime.Now &&
								   tabSol.FechaValidez <= DateTime.Now &&
								   tabSS.FechaValidez <= DateTime.Now &&
								   tabST.FechaValidez <= DateTime.Now
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 IdSolicitud = tabSol.Id,
								 IdMovimiento = 0,
								 Movimiento = "",
								 XMLRespuesta = tab.XMLRespuesta,
								 IdServicio = tabST.IdServicio,
								 NombreSolicitante = "",
								 Intermediario = "",
								 IdPaso = 0,
								 FechaCreacion = tab.FechaCreacion
							 };

			if (listacasos.Count() > 0)
			{
				foreach (CasoDTO item in listacasos)
				{
					if (!string.IsNullOrWhiteSpace(item.XMLRespuesta))
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);

						if ((item.IdServicio == 1) || (item.IdServicio == 49))
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());

							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == this.Intermediario.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						else if (item.IdServicio == 33)
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAsegurado.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());

							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == nomContratante.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						caso.Add(item);
					}

					item.Intermediario = string.Format("{0} - {1}", item.NombreSolicitante.ToString(), item.Intermediario.ToString());
				}
			}

			#endregion CASOS
			
			var coleccion = solicitudesMov.Union(listacasos);

			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(caso.AsQueryable(), pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion.AsQueryable());
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerCasosEnAuditoria(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idSuscriptor, int idServiciosuscriptor)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabSolicitud = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabServiciosTpSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosTiposSucriptor>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string nomAseg = @"Nomaseg";
			string nomContratante = @"nomcontratante";
			string nomAsegurado = @"Nomcompletobenef";

			string strcampo = string.Empty;
			int valor;
			valor = 0;
			foreach (var itemfiltro in parametrosFiltro)
			{
				strcampo = itemfiltro.Campo;
				if (strcampo == "Id")
				{
					valor = (int)itemfiltro.Valor;
					break;
				}
			}

			List<CasoDTO> caso = new List<CasoDTO>();

			#region MOVIMIENTOS

			var movimientos = from tabMov in tabMovimiento
							  join tab in tabCaso on tabMov.IdCaso equals tab.Id
							  join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
							  join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							  join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							  where tabMov.IdCaso == valor && 
									tabMov.Estatus == EstatusMov.AuditoriaMov &&
									tabMov.IdSuscriptor == idSuscriptor &&
									tabMov.IndEliminado == false &&
									tabMov.IndVigente == true &&
									tabMov.FechaValidez <= DateTime.Now &&
									tab.IndEliminado == false &&
									tab.IndVigente == true &&
									tab.FechaValidez <= DateTime.Now &&
									tabFS.IdServicioSuscriptor == idServiciosuscriptor &&
									tabFS.IndEliminado == false &&
									tabFS.IndVigente == true &&
									tabFS.FechaValidez <= DateTime.Now &&
									tabSS.IndEliminado == false &&
									tabSS.IndVigente == true &&
									tabSS.FechaValidez <= DateTime.Now &&
									tabST.IndEliminado == false &&
									tabST.IndVigente == true &&
									tabST.FechaValidez <= DateTime.Now
							  //orderby tab.IdMovimiento,tab.Id
							  select new CasoDTO
							  {
								  Id = tab.Id,
								  IdSolicitud = tab.IdSolicitud,
								  IdMovimiento = tabMov.Id,
								  Movimiento = tabMov.Nombre,
								  XMLRespuesta = "",
								  IdServicio = 0,
								  NombreSolicitante = "",
								  Intermediario = "",
								  IdPaso = tabMov.IdPaso,
								  FechaCreacion = tabMov.FechaCreacion
							  };

			if (movimientos.Count() > 0)
			{
				foreach (CasoDTO item in movimientos)
				{
					var listaMovimientos = (from mr in tabMovimiento
											where mr.IdCaso == item.Id &&
												  mr.Id >= item.IdMovimiento &&
												  mr.IdPaso == item.IdPaso &&
												  mr.IndEliminado == false &&
												  mr.IndVigente == true &&
												  mr.FechaValidez <= DateTime.Now
											orderby mr.Id ascending
											select new
											{
												Id = mr.Id,
												Estatus = mr.Estatus,
												IdPaso = mr.IdPaso,
											}).ToList();

					if (listaMovimientos.Count() > 1)
					{
						foreach (var item2 in listaMovimientos)
						{
							if (item.IdPaso != item2.IdPaso && item2.Estatus != EstatusMov.AuditoriaMov)
							{
								caso.Add(item);
								item.Intermediario = item.Movimiento.ToString();
							}
						}
					}
					else
					{
						caso.Add(item);
						item.Intermediario = item.Movimiento.ToString();
					}
				}
				

			}
			movimientos = UtilidadesDTO<CasoDTO>.FiltrarPaginar(movimientos, pagina, registros, parametrosFiltro);
			#endregion MOVIMIENTOS
			
			#region CASOS
			
			var listacasos = from tab in tabCaso
							 join tabSol in tabSolicitud on tab.IdSolicitud equals tabSol.Id
							 join tabFS in tabFlujosServicio on tabSol.IdFlujoServicio equals tabFS.Id
							 join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							 join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							 where tab.Id == valor &&
								   tabFS.IndVigente == true &&
								   tabSS.IndVigente == true &&
								   tabSol.IndVigente == true &&
								   tabST.IndVigente == true &&
								   tab.Estatus == EstatusCaso.AuditoriaCaso &&
								   tabFS.IdSuscriptor == idSuscriptor &&
								   tabSol.IdServicioSuscriptor == idServiciosuscriptor &&
								   tabFS.FechaValidez <= DateTime.Now &&
								   tabSol.FechaValidez <= DateTime.Now &&
								   tabSS.FechaValidez <= DateTime.Now &&
								   tabST.FechaValidez <= DateTime.Now
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 IdSolicitud = tabSol.Id,
								 IdMovimiento = 0,
								 Movimiento = "",
								 XMLRespuesta = tab.XMLRespuesta,
								 IdServicio = tabST.IdServicio,
								 NombreSolicitante = "",
								 Intermediario = "",
								 IdPaso = 0,
								 FechaCreacion = tab.FechaCreacion
							 };
			
			if (listacasos.Count() > 0)
			{
				foreach (CasoDTO item in listacasos)
				{
					if (!string.IsNullOrWhiteSpace(item.XMLRespuesta))
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
						
						if ((item.IdServicio == 1) || (item.IdServicio == 49))
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());
							
							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == this.Intermediario.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						else if (item.IdServicio == 33)
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAsegurado.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());
							
							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == nomContratante.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						caso.Add(item);
					}
					
					item.Intermediario = string.Format("{0} - {1}", item.NombreSolicitante.ToString(), item.Intermediario.ToString());
				}
			}
			
			#endregion CASOS
			
			var coleccion = movimientos.Union(listacasos);
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(caso.AsQueryable(), pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion.AsQueryable());
		}
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		

		public IEnumerable<CasoDTO> ObtenerCasosConMovEnAuditoria(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idSuscriptor, int idServiciosuscriptor)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabSolicitud = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabServiciosTpSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosTiposSucriptor>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string nomAseg = @"Nomaseg";
			string nomContratante = @"nomcontratante";
			string nomAsegurado = @"Nomcompletobenef"; 
			
			List<CasoDTO> caso = new List<CasoDTO>();
			
			string strcampo = string.Empty;
			

			foreach(var itemfiltro in parametrosFiltro)
			{
				strcampo = itemfiltro.Campo;
				if ((strcampo == "Id") || (strcampo == "IdSolicitud"))
				{
					break;
				}
			}
			
			if (strcampo== "Id")
			{
				return ObtenerCasosEnAuditoria(orden, pagina, registros, parametrosFiltro, idSuscriptor, idServiciosuscriptor);
			}
			if  (strcampo == "IdSolicitud")
			{
				return ObtenerSolicitudesEnAuditoria(orden, pagina, registros, parametrosFiltro, idSuscriptor, idServiciosuscriptor);
			}


			#region MOVIMIENTOS
			
			var movimientos = from tabMov in tabMovimiento
							  join tab in tabCaso on tabMov.IdCaso equals tab.Id
							  join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
							  join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							  join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							  where tabMov.Estatus == EstatusMov.AuditoriaMov &&
									tabMov.IdSuscriptor == idSuscriptor &&
									tabMov.IndEliminado == false &&
									tabMov.IndVigente == true &&
									tabMov.FechaValidez <= DateTime.Now &&
									tab.IndEliminado == false &&
									tab.IndVigente == true &&
									tab.FechaValidez <= DateTime.Now &&
									tabFS.IdServicioSuscriptor == idServiciosuscriptor &&
									tabFS.IndEliminado == false &&
									tabFS.IndVigente == true &&
									tabFS.FechaValidez <= DateTime.Now &&
									tabSS.IndEliminado == false &&
									tabSS.IndVigente == true &&
									tabSS.FechaValidez <= DateTime.Now &&
									tabST.IndEliminado == false &&
									tabST.IndVigente == true &&
									tabST.FechaValidez <= DateTime.Now
							  //orderby tab.IdMovimiento,tab.Id
							  select new CasoDTO
							  {
								  Id = tab.Id,
								  IdSolicitud = tab.IdSolicitud,
								  IdMovimiento = tabMov.Id,
								  Movimiento = tabMov.Nombre,
								  XMLRespuesta = "",
								  IdServicio = 0,
								  NombreSolicitante = "",
								  Intermediario = "",
								  IdPaso = tabMov.IdPaso,
								  FechaCreacion = tabMov.FechaCreacion
							  };
			
			if (movimientos.Count() > 0)
			{
				foreach (CasoDTO item in movimientos)
				{
					var listaMovimientos = (from mr in tabMovimiento
											where mr.IdCaso == item.Id &&
												  mr.Id >= item.IdMovimiento &&
												  mr.IdPaso == item.IdPaso &&
												  mr.IndEliminado == false &&
												  mr.IndVigente == true &&
												  mr.FechaValidez <= DateTime.Now
											orderby mr.Id ascending
											select new
											{
												Id = mr.Id,
												Estatus = mr.Estatus,
												IdPaso = mr.IdPaso,
											}).ToList();
					
					if (listaMovimientos.Count() > 1)
					{
						foreach (var item2 in listaMovimientos)
						{
							if (item.IdPaso != item2.IdPaso && item2.Estatus != EstatusMov.AuditoriaMov)
							{
								caso.Add(item);
								item.Intermediario = item.Movimiento.ToString();
							}
						}
					}
					else
					{
						caso.Add(item);
						item.Intermediario = item.Movimiento.ToString();
					}
				}
				movimientos = UtilidadesDTO<CasoDTO>.FiltrarPaginar(movimientos, pagina, registros, parametrosFiltro);
				
			}
			
			#endregion MOVIMIENTOS
			
			#region SOLICITUDES
			
			var solicitudes = from tabSol in tabSolicitud
							  join tabFS in tabFlujosServicio on tabSol.IdFlujoServicio equals tabFS.Id
							  join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							  join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							  where tabFS.IndVigente == true &&								
									tabSS.IndVigente == true &&									
									tabSol.IndVigente == true &&									
									tabST.IndVigente == true &&
									tabSol.Estatus == EstatusSolicitud.AuditoriaSol &&
									tabFS.IdSuscriptor == idSuscriptor &&
									tabSol.IdServicioSuscriptor == idServiciosuscriptor &&
									tabFS.FechaValidez <= DateTime.Now &&
									tabSS.FechaValidez <= DateTime.Now &&
									tabSol.FechaValidez <= DateTime.Now &&
									tabST.FechaValidez <= DateTime.Now
							  //orderby tabSol.Id
							  select new CasoDTO
							  {
								  Id = 0,
								  IdSolicitud = tabSol.Id,
								  IdMovimiento = 0,
								  Movimiento = "",
								  XMLRespuesta = tabSol.XMLSolicitud,
								  IdServicio = tabST.IdServicio,
								  NombreSolicitante = "",
								  Intermediario = "",
								  IdPaso = 0,
								  FechaCreacion = tabSol.FechaCreacion
							  };
			
			if (solicitudes.Count() > 0)
			{
				foreach (CasoDTO item in solicitudes)
				{
					if (!string.IsNullOrWhiteSpace(item.XMLRespuesta))
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
						
						if ((item.IdServicio == 1) || (item.IdServicio == 49))
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());
							
							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == this.Intermediario.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						else if (item.IdServicio == 33)
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAsegurado.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());
							
							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == nomContratante.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						caso.Add(item);
					}
					item.Intermediario = string.Format("{0} - {1}", item.NombreSolicitante.ToString(), item.Intermediario.ToString());
				}
			}
			
			#endregion SOLICITUDES
			
			//var solicitudesMov = movimientos.Union(solicitudes.OrderBy(m => m.IdSolicitud));
			var solicitudesMov = movimientos.Union(solicitudes);	
			
			#region CASOS
			
			var listacasos = from tab in tabCaso
							 join tabSol in tabSolicitud on tab.IdSolicitud equals tabSol.Id
							 join tabFS in tabFlujosServicio on tabSol.IdFlujoServicio equals tabFS.Id
							 join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							 join tabST in tabServiciosTpSuscriptor on tabSS.IdServicioTipoSuscriptor equals tabST.Id
							 where tabFS.IndVigente == true &&
								   tabSS.IndVigente == true &&
								   tabSol.IndVigente == true &&
								   tabST.IndVigente == true &&
								   tab.Estatus == EstatusCaso.AuditoriaCaso &&
								   tabFS.IdSuscriptor == idSuscriptor &&
								   tabSol.IdServicioSuscriptor == idServiciosuscriptor &&
								   tabFS.FechaValidez <= DateTime.Now &&
								   tabSol.FechaValidez <= DateTime.Now &&
								   tabSS.FechaValidez <= DateTime.Now &&
								   tabST.FechaValidez <= DateTime.Now
							 //orderby tab.IdSolicitud
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 IdSolicitud = tabSol.Id,
								 IdMovimiento = 0,
								 Movimiento = "",
								 XMLRespuesta = tab.XMLRespuesta,
								 IdServicio = tabST.IdServicio,
								 NombreSolicitante = "",
								 Intermediario = "",
								 IdPaso = 0,
								 FechaCreacion = tab.FechaCreacion
							 };
			
			if (listacasos.Count() > 0)
			{
				foreach (CasoDTO item in listacasos)
				{
					if (!string.IsNullOrWhiteSpace(item.XMLRespuesta))
					{
						XElement xmlRespuesta = XElement.Parse(item.XMLRespuesta);
						
						if ((item.IdServicio == 1) || (item.IdServicio == 49))
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAseg.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());
							
							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == this.Intermediario.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						else if (item.IdServicio == 33)
						{
							item.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																							where p.Attribute(atributoNombre).Value.ToUpper() == nomAsegurado.ToUpper()
																							select p.Attribute(atributoValor).Value).SingleOrDefault());
							
							item.Intermediario = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																						where p.Attribute(atributoNombre).Value.ToUpper() == nomContratante.ToUpper()
																						select p.Attribute(atributoValor).Value).SingleOrDefault());
						}
						caso.Add(item);
					}
					
					item.Intermediario = string.Format("{0} - {1}", item.NombreSolicitante.ToString(), item.Intermediario.ToString());
				}
			}
			
			#endregion CASOS
			
			//var coleccion = solicitudesMov.Union(listacasos.OrderBy(m => m.IdSolicitud));
			var coleccion = solicitudesMov.Union(listacasos);
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(caso.AsQueryable(), pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion.AsQueryable());
		}
		
		///<summary>Llena el grid de Consulta de Caso con una cantidad de supervisados</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerDtoSupervisadosMod(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, IList<int> supervisados)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string Tipdocaseg = "TIPDOCASEG";
			string Numdocaseg = "NUMDOCASEG";
			string Nomaseg = "NOMASEG";
			string SupportIncident = "SUPPORTINCIDENT";
			string CodIdExternoInt = "CODIDEXTERNOINT";
			List<CasoDTO> _caso = new List<CasoDTO>();
			
			var coleccion = from tab in tabCaso
							join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
							join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							where supervisados.Contains(tab.CreadorPor)
							orderby "it." + orden
							select new CasoDTO
							{
								Id = tab.Id,
								Ticket = tab.Ticket,
								IndEliminado = tab.IndEliminado,
								IdFlujoServicio = tab.IdFlujoServicio,
								NombreServicioSuscriptor = tabSS.Nombre,
								NombreEstatusCaso = tabLV.NombreValor,
								Estatus = tab.Estatus,
								CreadorPor = tab.CreadorPor,
								FechaCreacion = tab.FechaCreacion,
								IdSuscriptor = tabFS.IdSuscriptor,
								XMLRespuesta = tab.XMLRespuesta,
								TipDocSolicitante = "",
								NumDocSolicitante = "",
								NombreSolicitante = "",
								SupportIncident = "",
								CodIdExternoInt = ""
							};
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, false);
			
			foreach (CasoDTO caso in coleccion)
			{
				XElement xmlRespuesta = XElement.Parse(caso.XMLRespuesta);
				caso.TipDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Tipdocaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Numdocaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Nomaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.SupportIncident = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			  where p.Attribute(atributoNombre).Value.ToUpper() == SupportIncident
																			  select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.Idcasoexterno = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			where p.Attribute(atributoNombre).Value.ToUpper() == @"IDCASOEXTERNO"
																			select p.Attribute(atributoValor).Value).SingleOrDefault());
				_caso.Add(caso);
			}
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(_caso.AsQueryable(), pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		//Modificado
		public IEnumerable<CasoDTO> ObtenerDTOSupervisados(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, IList<int> supervisados)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabCasoExterno = this.udt.Sesion.CreateObjectSet<CasosExterno>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string Tipdocaseg = "TIPDOCASEG";
			string Numdocaseg = "NUMDOCASEG";
			string Nomaseg = "NOMASEG";
			string SupportIncident = "SUPPORTINCIDENT";
			string CodIdExternoInt = "CODIDEXTERNOINT";
			List<CasoDTO> _caso = new List<CasoDTO>();
			
			var coleccionHC2 = from tab in tabCaso
							   join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
							   join tabSS in tabServiciosSuscriptor on tabFS.IdServicioSuscriptor equals tabSS.Id
							   join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							   where supervisados.Contains(tab.CreadorPor)
							   orderby "it." + orden
							   select new CasoDTO
							   {
								   Id = tab.Id,
								   Ticket = tab.Ticket,
								   IndEliminado = tab.IndEliminado,
								   IdFlujoServicio = tab.IdFlujoServicio,
								   NombreServicioSuscriptor = tabSS.Nombre,
								   NombreEstatusCaso = tabLV.NombreValor,
								   Estatus = tab.Estatus,
								   CreadorPor = tab.CreadorPor,
								   FechaCreacion = tab.FechaCreacion,
								   IdSuscriptor = tabFS.IdSuscriptor,
								   XMLRespuesta = tab.XMLRespuesta,
								   IdCasoexterno2 = null,
								   TipDocSolicitante = "",
								   NumDocSolicitante = "",
								   NombreSolicitante = "",
								   SupportIncident = "",
								   CodIdExternoInt = ""
							   };
			coleccionHC2 = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccionHC2, false);
			
			foreach (CasoDTO caso in coleccionHC2)
			{
				XElement xmlRespuesta = XElement.Parse(caso.XMLRespuesta);
				caso.TipDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Tipdocaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Numdocaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Nomaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.SupportIncident = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			  where p.Attribute(atributoNombre).Value.ToUpper() == SupportIncident
																			  select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.Idcasoexterno = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			where p.Attribute(atributoNombre).Value.ToUpper() == @"IDCASOEXTERNO"
																			select p.Attribute(atributoValor).Value).SingleOrDefault());
				_caso.Add(caso);
				if (caso.Estatus == 330)
				{
					caso.Origen = "HC1";
				}
				else
				{
					caso.Origen = "HC2";
				}
			}
			
			var coleccionHC1 = from tab in tabCasoExterno
							   join tabSS in tabServiciosSuscriptor on tab.IdServicioSuscriptor equals tabSS.Id
							   join tabLV in tabListaValor on tab.IdEstatusCaso equals tabLV.Id
							   orderby "it." + orden
							   select new CasoDTO
							   {
								   Id = tab.IdCaso.Value,
								   Ticket = tab.Ticket,
								   IndEliminado = false,
								   IdFlujoServicio = 0, //ojo
								   NombreServicioSuscriptor = tabSS.Nombre,
								   NombreEstatusCaso = tabLV.NombreValor,
								   Estatus = tab.IdEstatusCaso.Value,
								   CreadorPor = 0,
								   FechaCreacion = tab.FechaCreacion.Value,
								   IdSuscriptor = tab.IdSuscriptor.Value,
								   XMLRespuesta = "",
								   Origen = tab.Origen,
								   TipDocSolicitante = tab.NacionalidadBenef,
								   NumDocSolicitante = tab.DocumentoId,
								   NombreSolicitante = tab.Asegurado,
								   SupportIncident = tab.Suport_Incident,
								   Idcasoexterno = tab.Reclamo,
								   IdCasoexterno2 = tab.Id_Experdiente_Web
							   };
			
			var coleccion = coleccionHC2.Union(coleccionHC1);
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(_caso.AsQueryable(), pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		///-
		public IEnumerable<CasoDTO> ObtenerGridDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int pend, int proc, int sol, int idSuscriptor)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabSuscriptor = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabPaso = this.udt.Sesion.CreateObjectSet<Paso>();
			var tabM = this.udt.Sesion.CreateObjectSet<Movimiento>();
			string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
			string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
			string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
			string Tipdocaseg = "TIPDOCASEG";
			string TipdocasegC = "TIPDOCBENEF";
			string Numdocaseg = "NUMDOCASEG";
			string NumdocasegC = "NUMDOCBENEF";
			string Nomaseg = "NOMASEG";
			string NomasegC = "NOMCOMPLETOBENEF";
			string SupportIncident = "SUPPORTINCIDENT";
			string CodIdExternoInt = "CODIDEXTERNOINT";
			List<CasoDTO> _caso = new List<CasoDTO>();
			
			var coleccion = from tab in tabCaso
							join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							join tab1 in tabServicioSuscriptor on tabFS.IdServicioSuscriptor equals tab1.Id
							join tba2 in tabSuscriptor on tabFS.IdSuscriptor equals tba2.Id
							where (tab.Estatus == pend || tab.Estatus == proc || tab.Estatus == sol) &&
								  tba2.Id == idSuscriptor
							orderby "it." + orden
							select new CasoDTO
							{
								Id = tab.Id,
								IdSolicitud = tab.IdSolicitud,
								IdFlujoServicio = tab.IdFlujoServicio,
								IdServicio = tab.IdServicio,
								Ticket = tab.Ticket,
								Estatus = tab.Estatus,
								ApellidoSolicitante = tab.ApellidoBeneficiario,
								MovilSolicitante = tab.MovilBeneficiario,
								EmailSolicitante = tab.EmailBeneficiario,
								ClaveSolicitante = tab.ClaveBeneficiario,
								NombreEstatusCaso = tabLV.NombreValorCorto.Trim(),
								NumPoliza = tab.NumPoliza,
								NumMvtoPoliza = tab.NumMvtoPoliza,
								NumCertificado = tab.NumCertificado,
								NumRiesgo = tab.NumRiesgo,
								TipDocAgt = tab.TipDocAgt,
								NumDocAgt = tab.NumDocAgt,
								NombreSuscriptor = tba2.Nombre.Trim(),
								NombreServicioSuscriptor = tab1.Nombre.Trim(),
								PrioridadAtencion = tab.PrioridadAtencion,
								NombreCompletoSolicitante = tab.NombreBeneficiario + " " + tab.ApellidoBeneficiario,
								IdSuscriptor = tabFS.IdSuscriptor,
								CreadorPor = tab.CreadorPor,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado,
								IndVigente = tab.IndVigente,
								XMLRespuesta = tab.XMLRespuesta,
							};
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			
			foreach (CasoDTO caso in coleccion)
			{
				XElement xmlRespuesta = XElement.Parse(caso.XMLRespuesta);
				caso.TipDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Tipdocaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				if (string.IsNullOrEmpty(caso.TipDocSolicitante))
				{
					caso.TipDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					where p.Attribute(atributoNombre).Value.ToUpper() == TipdocasegC
																					select p.Attribute(atributoValor).Value).SingleOrDefault());
				}
				caso.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Numdocaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				if (string.IsNullOrEmpty(caso.NumDocSolicitante))
				{
					caso.NumDocSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					where p.Attribute(atributoNombre).Value.ToUpper() == NumdocasegC
																					select p.Attribute(atributoValor).Value).SingleOrDefault());
				}
				caso.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == Nomaseg
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				if (string.IsNullOrEmpty(caso.NombreSolicitante))
				{
					caso.NombreSolicitante = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																					where p.Attribute(atributoNombre).Value.ToUpper() == NomasegC
																					select p.Attribute(atributoValor).Value).SingleOrDefault());
				}
				caso.SupportIncident = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			  where p.Attribute(atributoNombre).Value.ToUpper() == SupportIncident
																			  select p.Attribute(atributoValor).Value).SingleOrDefault());
				caso.Idcasoexterno = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																			where p.Attribute(atributoNombre).Value.ToUpper() == @"IDCASOEXTERNO"
																			select p.Attribute(atributoValor).Value).SingleOrDefault());
				if (string.IsNullOrEmpty(caso.Idcasoexterno))
				{
					caso.Idcasoexterno = string.Format("{0}{1}", string.Empty, (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
																				where p.Attribute(atributoNombre).Value.ToUpper() == @"IDRECLAMO"
																				select p.Attribute(atributoValor).Value).SingleOrDefault());
				}
				_caso.Add(caso);
			}
			
			if (orden.IndexOf(@"Id") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.Id) : coleccion.OrderBy(p => p.Id);
			}
			if (orden.IndexOf(@"NombreSuscriptor") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.NombreSuscriptor) : coleccion.OrderBy(p => p.NombreSuscriptor);
			}
			if (orden.IndexOf(@"NombreServicioSuscriptor") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.NombreServicioSuscriptor) : coleccion.OrderBy(p => p.NombreServicioSuscriptor);
			}
			if (orden.IndexOf(@"NumDocSolicitante") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.NumDocSolicitante) : coleccion.OrderBy(p => p.NumDocSolicitante);
			}
			if (orden.IndexOf(@"NombreEstatusCaso") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.NombreEstatusCaso) : coleccion.OrderBy(p => p.NombreEstatusCaso);
			}
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(_caso.AsQueryable(), pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerGridAgruparDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabSuscriptor = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabPaso = this.udt.Sesion.CreateObjectSet<Paso>();
			var tabM = this.udt.Sesion.CreateObjectSet<Movimiento>();
			
			var coleccion = from tab in tabCaso
							join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							join tab1 in tabServicioSuscriptor on tabFS.IdServicioSuscriptor equals tab1.Id
							join tba2 in tabSuscriptor on tabFS.IdSuscriptor equals tba2.Id
							orderby "it." + orden
							select new CasoDTO
							{
								Id = tab.Id,
								IdSolicitud = tab.IdSolicitud,
								IdFlujoServicio = tab.IdFlujoServicio,
								IdServicio = tab.IdServicio,
								Estatus = tab.Estatus,
								TipDocSolicitante = tab.TipDocBeneficiario,
								NumDocSolicitante = tab.NumDocBeneficiario,
								NombreEstatusCaso = tabLV.NombreValorCorto,
								NumRiesgo = tab.NumRiesgo,
								TipDocAgt = tab.TipDocAgt,
								NumDocAgt = tab.NumDocAgt,
								NombreSuscriptor = tba2.Nombre,
								NombreServicioSuscriptor = tab1.Nombre,
								PrioridadAtencion = tab.PrioridadAtencion,
								IdSuscriptor = tabFS.IdSuscriptor,
								CreadorPor = tab.CreadorPor
							};
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerGridDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabSuscriptor = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabPaso = this.udt.Sesion.CreateObjectSet<Paso>();
			var tabM = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var coleccion = from tab in tabCaso
							join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							join tab1 in tabServicioSuscriptor on tabFS.IdServicioSuscriptor equals tab1.Id
							join tba2 in tabSuscriptor on tabFS.IdSuscriptor equals tba2.Id
							orderby "it." + orden
							select new CasoDTO
							{
								Id = tab.Id,
								IdSolicitud = tab.IdSolicitud,
								IdFlujoServicio = tab.IdFlujoServicio,
								IdServicio = tab.IdServicio,
								Estatus = tab.Estatus,
								TipDocSolicitante = tab.TipDocBeneficiario,
								NumDocSolicitante = tab.NumDocBeneficiario,
								Cedula = tab.TipDocBeneficiario.Trim() + " " + tab.NumDocBeneficiario, //se agregoe ste campo para no dañar el filtrado por tipdoc y numdoc
								NombreSolicitante = tab.NombreBeneficiario,
								ApellidoSolicitante = tab.ApellidoBeneficiario,
								NombreEstatusCaso = tabLV.NombreValor.Trim(),
								TipDocAgt = tab.TipDocAgt,
								NumDocAgt = tab.NumDocAgt,
								NombreSuscriptor = tba2.Nombre,
								NombreServicioSuscriptor = tab1.Nombre.Trim(),
								PrioridadAtencion = tab.PrioridadAtencion,
								NombreCompletoSolicitante = tab.NombreBeneficiario.Trim() + " " + tab.ApellidoBeneficiario.Trim(),
								IdSuscriptor = tabFS.IdSuscriptor,
								CreadorPor = tab.CreadorPor
							};
			
			if (orden.IndexOf(@"Id") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.Id) : coleccion.OrderBy(p => p.Id);
			}
			if (orden.IndexOf(@"Cedula") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.Cedula) : coleccion.OrderBy(p => p.Cedula);
			}
			if (orden.IndexOf(@"NombreCompletoSolicitante") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.NombreCompletoSolicitante) : coleccion.OrderBy(p => p.NombreCompletoSolicitante);
			}
			if (orden.IndexOf(@"NombreServicioSuscriptor") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.NombreServicioSuscriptor) : coleccion.OrderBy(p => p.NombreServicioSuscriptor);
			}
			if (orden.IndexOf(@"NombreEstatusCaso") != -1)
			{
				coleccion = (orden.IndexOf(@"DESC") != -1) ? coleccion.OrderByDescending(p => p.NombreEstatusCaso) : coleccion.OrderBy(p => p.NombreEstatusCaso);
			}
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return UtilidadesDTO<CasoDTO>.EncriptarId(coleccion);
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerGridUsuarioDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int[] Idsuspervisado)
		{
			var tabUsuarioSuscriptor = this.udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
			var tabDetalleTipoSuscriptor = this.udt.Sesion.CreateObjectSet<DetallesTiposSuscriptor>();
			var tabServicioSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabDatosBase = this.udt.Sesion.CreateObjectSet<DatosBase>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabAutonomiaUsuario = this.udt.Sesion.CreateObjectSet<VW_AutonomiasUsuario>();
			var tabAutonomiaSuscriptor = this.udt.Sesion.CreateObjectSet<AutonomiasSuscriptor>();
			var tabCargosUsuario = this.udt.Sesion.CreateObjectSet<VW_CargosUsuario>();
			var tabCargosSuscriptor = this.udt.Sesion.CreateObjectSet<CargosSuscriptor>();
			var tabHabilidadesSuscriptor = this.udt.Sesion.CreateObjectSet<HabilidadesSuscriptor>();
			var tabHabilidadesUsuario = this.udt.Sesion.CreateObjectSet<VW_HabilidadesUsuario>();
			var tabSuscriptor = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabMovimiento2 = this.udt.Sesion.CreateObjectSet<Movimiento>();
			int[] estatus = { 154, 155 };
			int filtroSuscriptor = 0;
			int filtroIdUsuario = 0;
			int filtroServicio = 0;
			bool filtroEstatus = true;
			int filtroCargo = 0;
			int filtroHabilidades = 0;
			int filtroAutonomia = 0;
			
			foreach (var item in parametrosFiltro)
			{
				if (item.Campo == "Idsuscriptor")
				{
					filtroSuscriptor = int.Parse(item.Valor.ToString());
				}
				if (item.Campo == "IdUsuario")
				{
					filtroIdUsuario = int.Parse(item.Valor.ToString());
				}
				if (item.Campo == "CargosSuscriptor")
				{
					filtroCargo = int.Parse(item.Valor.ToString());
				}
				if (item.Campo == "HabilidadSuscriptor")
				{
					filtroHabilidades = int.Parse(item.Valor.ToString());
				}
				if (item.Campo == "AutonomiaSuscriptor")
				{
					filtroAutonomia = int.Parse(item.Valor.ToString());
				}
				if (item.Campo == "IdServicio")
				{
					filtroServicio = int.Parse(item.Valor.ToString());
				}
				if (item.Campo == "UsuarioVigente")
				{
					filtroEstatus = bool.Parse(item.Valor.ToString());
				}
			}
			
			var coleccion = (from tab in tabUsuarioSuscriptor
							 join tabDTS in tabDetalleTipoSuscriptor on tab.IdSuscriptor equals tabDTS.IdSuscriptor
							 join tabSS in tabServicioSuscriptor on tabDTS.Id equals tabSS.IdDetalleTipoSuscriptor
							 join tabDB in tabDatosBase on tab.IdUsuario equals tabDB.IdUsuario
							 join tabCU in tabCargosUsuario on tab.Id equals tabCU.IdUsuarioSucriptor into TABCUsuario
							 from CU in TABCUsuario.DefaultIfEmpty()
							 join tabCS in tabCargosSuscriptor on CU.IdCargoSuscriptor equals tabCS.Id into TABCS
							 from CS in TABCS.DefaultIfEmpty()
							 join tabHU in tabHabilidadesUsuario on tab.Id equals tabHU.IdUsuarioSucriptor into THS
							 from HU in THS.DefaultIfEmpty()
							 join tabHS in tabHabilidadesSuscriptor on HU.IdHabilidadSuscriptor equals tabHS.Id into tabHSuscriptor
							 from HS in tabHSuscriptor.DefaultIfEmpty()
							 join tabAU in tabAutonomiaUsuario on tab.Id equals tabAU.IdUsuarioSucriptor into TABAUsuario
							 from AU in TABAUsuario.DefaultIfEmpty()
							 join tabAS in tabAutonomiaSuscriptor on AU.IdAutonomiaSuscriptor equals tabAS.Id into tabAUSuscriptor
							 from AS in tabAUSuscriptor.DefaultIfEmpty()
							 join tabM2 in tabMovimiento2 on tab.Id equals tabM2.UsuarioAsignado into TABM
							 from M in TABM.DefaultIfEmpty()
							 join tabC in tabCaso on M.IdCaso equals tabC.Id into TABC
							 from C in TABC.DefaultIfEmpty()
							 where tabDB.Nombre1 != "" &&
								   tab.IndEliminado == false &&
								   tabDTS.IndEliminado == false &&
								   tabDTS.IndVigente == true &&
								   tabSS.IndEliminado == false &&
								   tabSS.IndVigente == true &&
								   tabDB.IndEliminado == false &&
								   tabDB.IndVigente == true && Idsuspervisado.Contains(tab.Id)
							 orderby tab.Id
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 IdUsuario = tab.Id,
								 NombreUsuario = tabDB.Nombre1 + " " + tabDB.Apellido1,
								 CargaLaboral = ((from tabM in tabMovimiento
												  join t_caso in tabCaso on tabM.IdCaso equals t_caso.Id
												  where tabM.IndEliminado == false &&
														tabM.IndVigente == true &&
														tabM.UsuarioAsignado == tab.Id &&
														tabM.FechaValidez <= DateTime.Now &&
														tabM.TipoMovimiento == 1 &&
														estatus.Contains(tabM.Estatus)
												  select tabM.UsuarioAsignado).Count()),
								 EstatusUsuario = tab.IndVigente,
								 IdSuscriptor = tab.IdSuscriptor,
								 CargosSuscriptor = CS.Id == null ? 0 : CS.Id,
								 HabilidadSuscriptor = HS.Id == null ? 0 : HS.Id,
								 AutonomiaSuscriptor = AS.Id == null ? 0 : AS.Id,
								 IdServicio = C.IdServicio == null ? 0 : C.IdServicio,
								 UsuarioVigente = tab.IndVigente
							 });
			
			if (filtroSuscriptor != 0)
			{
				coleccion = coleccion.Where(x => x.IdSuscriptor == filtroSuscriptor);
			}
			if (filtroIdUsuario != 0)
			{
				coleccion = coleccion.Where(x => x.IdUsuario == filtroIdUsuario);
			}
			if (filtroServicio != 0)
			{
				coleccion = coleccion.Where(x => x.IdServicio == filtroServicio);
			}
			if (filtroEstatus == true || filtroEstatus == false)
			{
				coleccion.Where(x => x.IndVigente == filtroEstatus);
			}
			if (filtroCargo != 0)
			{
				coleccion = coleccion.Where(x => x.CargosSuscriptor == filtroCargo);
			}
			if (filtroHabilidades != 0)
			{
				coleccion = coleccion.Where(x => x.HabilidadSuscriptor == filtroHabilidades);
			}
			if (filtroAutonomia != 0)
			{
				coleccion = coleccion.Where(x => x.AutonomiaSuscriptor == filtroAutonomia);
			}
			
			var coleccion2 = coleccion.AsEnumerable().Distinct(new Comparador());
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			IEnumerable<CasoDTO> ColeccionFiltrada = coleccion2;
			return UtilidadesDTO<CasoDTO>.EncriptarId(ColeccionFiltrada.AsQueryable());
		}
		
		#region Clase para obtenerGridUsuarioDTO
		
		// Custom comparer for the Product class
		class Comparador : IEqualityComparer<CasoDTO>
		{
			// Products are equal if their names and product numbers are equal.
			public bool Equals(CasoDTO x, CasoDTO y)
			{
				//Check whether the compared objects reference the same data.
				if (Object.ReferenceEquals(x, y))
				{
					return true;
				}
				
				//Check whether any of the compared objects is null.
				if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				{
					return false;
				}
				
				//Check whether the products' properties are equal.
				return x.IdUsuario == y.IdUsuario;
			}
			
			// If Equals() returns true for a pair of objects 
			// then GetHashCode() must return the same value for these objects.
			
			public int GetHashCode(CasoDTO user)
			{
				//Check whether the object is null
				if (Object.ReferenceEquals(user, null))
				{
					return 0;
				}
				
				//Get hash code for the Name field if it is not null.
				int hashProductName = user.IdUsuario == null ? 0 : user.IdUsuario.GetHashCode();
				
				//Get hash code for the Code field.
				int hashProductCode = user.IdUsuario.GetHashCode();
				
				//Calculate the hash code for the product.
				return hashProductName ^ hashProductCode;
			}
		}
		
		#endregion
		
		public CasoDTO ObtenerCasoConSuscriptorDTO(int IdCaso)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabCaso
							 join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							 where tab.Id == IdCaso
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 IdSolicitud = tab.IdSolicitud,
								 IdFlujoServicio = tab.IdFlujoServicio,
								 IdServicio = tab.IdServicio,
								 Ticket = tab.Ticket,
								 Estatus = tab.Estatus,
								 TipDocAgt = tab.TipDocAgt,
								 NumDocAgt = tab.NumDocAgt,
								 IdSuscriptor = tabFS.IdSuscriptor,
								 CreadorPor = tab.CreadorPor
							 }).SingleOrDefault();
			
			return coleccion;
		}
		
		public CasoDTO ObtenerMovimientoCaso(int idMovimiento)
		{
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabpaso = this.udt.Sesion.CreateObjectSet<Paso>();
			var tabTipoPaso = this.udt.Sesion.CreateObjectSet<TipoPaso>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tcaso in tabCaso
							 join tflujo in tabFlujoServicio on tcaso.IdFlujoServicio equals tflujo.Id
							 join tMov in tabMovimiento on tcaso.Id equals tMov.IdCaso
							 join tPaso in tabpaso on tMov.IdPaso equals tPaso.Id
							 join ttipo in tabTipoPaso on tPaso.IdTipoPaso equals ttipo.Id
							 where tMov.Id == idMovimiento
							 select new CasoDTO
							 {
								 IdServicio = tflujo.IdServicioSuscriptor,
								 Caso = tcaso.Id,
								 EstatusMovimiento = tMov.Estatus,
								 EstatusCaso = tcaso.Estatus,
								 Movimiento = tMov.Nombre,
								 Version = tflujo.Version,
								 TipoMovimiento = ttipo.Descripcion
							 }).SingleOrDefault();
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerDTO()
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var coleccion = from tab in tabCaso  //orderby tab.Nombre
							select new CasoDTO
							{
								Id = tab.Id,
								IndEliminado = tab.IndEliminado,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								PrioridadAtencion = tab.PrioridadAtencion,
								IdSolicitud = tab.IdSolicitud,
								IdFlujoServicio = tab.IdFlujoServicio,
								IdServicio = tab.IdServicio,
								Ticket = tab.Ticket,
								TipDocSolicitante = tab.TipDocBeneficiario,
								NumDocSolicitante = tab.NumDocBeneficiario,
								NombreSolicitante = tab.NombreBeneficiario,
								ApellidoSolicitante = tab.ApellidoBeneficiario,
								MovilSolicitante = tab.MovilBeneficiario,
								EmailSolicitante = tab.EmailBeneficiario,
								ClaveSolicitante = tab.ClaveBeneficiario,
								Estatus = tab.Estatus,
								NumPoliza = tab.NumPoliza,
								NumMvtoPoliza = tab.NumMvtoPoliza,
								NumCertificado = tab.NumCertificado,
								NumRiesgo = tab.NumRiesgo,
								TipDocAgt = tab.TipDocAgt,
								NumDocAgt = tab.NumDocAgt,
								XMLRespuesta = tab.XMLRespuesta,
								CreadorPor = tab.CreadorPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								FechaSolicitud = tab.FechaSolicitud,
								FechaAnulacion = tab.FechaAnulacion,
								FechaRechazo = tab.FechaRechazo,
								FechaEjecucion = tab.FechaEjecucion,
								FechaReverso = tab.FechaReverso,
								IdMovimiento = tab.IdMovimiento
							};
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<SuscriptorDTO> ObtenerSuscriptoresDTO()
		{
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from tab in tabSuscriptores  //orderby tab.Nombre
							select new SuscriptorDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre
							};
			
			coleccion = UtilidadesDTO<SuscriptorDTO>.FiltrarColeccionEliminacion(coleccion, false);
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public CasoDTO CasoPorID(int ID)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabSolicutudes = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var coleccion = (from tab in tabCaso
							 join tabS in tabSolicutudes on tab.IdSolicitud equals tabS.Id
							 join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							 join tabM in tabMovimiento on tab.Id equals tabM.IdCaso
							 where tab.Id == ID && tab.IndEliminado == false && tab.IndVigente == true
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 PrioridadAtencion = tab.PrioridadAtencion,
								 IdSolicitud = tab.IdSolicitud,
								 IdFlujoServicio = tab.IdFlujoServicio,
								 IdServicio = tab.IdServicio,
								 Estatus = tab.Estatus,
								 NumRiesgo = tab.NumRiesgo,
								 CreadorPor = tab.CreadorPor,
								 FechaCreacion = tab.FechaCreacion,
								 ModificadoPor = tab.ModificadoPor,
								 FechaModificacion = tab.FechaModificacion,
								 FechaSolicitud = tab.FechaSolicitud,
								 FechaAnulacion = tab.FechaAnulacion,
								 FechaRechazo = tab.FechaRechazo,
								 FechaEjecucion = tab.FechaEjecucion,
								 FechaReverso = tab.FechaReverso,
								 IdMovimiento = tab.IdMovimiento,
								 Version = tabFS.Version,
								 IdServiciosuscriptor = tabFS.IdServicioSuscriptor,
								 IdSuscriptor = tabFS.IdSuscriptor,
								 NombreMovimiento = tabM.Nombre,
								 XMLRespuesta = tab.XMLRespuesta,
								 indChat = tabFS.IndChat
							 }).FirstOrDefault();
			
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public CasoDTO CasoPorIDMovimiento(int ID)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabSolicutudes = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var coleccion = (from tabM in tabMovimiento
							 join tab in tabCaso on tabM.IdCaso equals tab.Id
							 join tabS in tabSolicutudes on tab.IdSolicitud equals tabS.Id
							 join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							 where tabM.Id == ID && tab.IndEliminado == false && tab.IndVigente == true && tabM.Estatus == 158
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 PrioridadAtencion = tab.PrioridadAtencion,
								 IdSolicitud = tab.IdSolicitud,
								 IdFlujoServicio = tab.IdFlujoServicio,
								 IdServicio = tab.IdServicio,
								 Estatus = tab.Estatus,
								 NumRiesgo = tab.NumRiesgo,
								 CreadorPor = tab.CreadorPor,
								 FechaCreacion = tab.FechaCreacion,
								 ModificadoPor = tab.ModificadoPor,
								 FechaModificacion = tab.FechaModificacion,
								 FechaSolicitud = tab.FechaSolicitud,
								 FechaAnulacion = tab.FechaAnulacion,
								 FechaRechazo = tab.FechaRechazo,
								 FechaEjecucion = tab.FechaEjecucion,
								 FechaReverso = tab.FechaReverso,
								 Version = tabFS.Version,
								 IdServiciosuscriptor = tabFS.IdServicioSuscriptor,
								 IdSuscriptor = tabFS.IdSuscriptor,
								 NombreMovimiento = tabM.Nombre,
								 IdMovimiento = tabM.Id
							 }).FirstOrDefault();
			
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public CasoDTO ObtnerIndChat(int ID)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabSolicutudes = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var coleccion = (from tabM in tabMovimiento
							 join tab in tabCaso on tabM.IdCaso equals tab.Id
							 join tabS in tabSolicutudes on tab.IdSolicitud equals tabS.Id
							 join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							 where tabM.Id == ID && tab.IndEliminado == false && tab.IndVigente == true
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 indChat = tabFS.IndChat
							 }).FirstOrDefault();
			
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> ObtenerCasosPorIDCreacion(int Id)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabLista = this.udt.Sesion.CreateObjectSet<Lista>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabSuscriptor = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from tab in tabCaso
							orderby tab.FechaCreacion ascending
							join tabFS in tabFlujosServicio on tab.IdFlujoServicio equals tabFS.Id
							join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							join tabS in tabSuscriptor on tabFS.IdSuscriptor equals tabS.Id
							orderby tabS.Nombre ascending
							where tab.CreadorPor == Id &&
								  tab.Estatus != (from tabLV2 in tabListaValor
												  join tabL in tabLista on tabLV2.IdLista equals tabL.Id
												  where tabL.Nombre == "Estatus del Caso" &&
														tabLV2.NombreValorCorto == "CERR"
												  select tabLV2.Id).FirstOrDefault()
							orderby tabS.Nombre ascending
							select new CasoDTO
							{
								Id = tab.Id,
								Status = tabLV.NombreValor,
								NombreSuscriptor = tabS.Nombre,
								FechaCreacion = tab.FechaCreacion,
								SLAToleranciaFlujoServicio = tabFS.SlaTolerancia,
								IdServiciosuscriptor = tabFS.IdServicioSuscriptor,
								IndEliminado = tab.IndEliminado,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez
							};
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public CasoDTO ObtenerCasoPorIDCompletoConMovimientos(int ID)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabSolicutudes = this.udt.Sesion.CreateObjectSet<Solicitud>();
			var tabFlujoServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var coleccion = (from tab in tabCaso
							 join tabFS in tabFlujoServicio on tab.IdFlujoServicio equals tabFS.Id
							 join tabM in tabMovimiento on tab.Id equals tabM.IdCaso
							 where tab.Id == ID && tab.IndEliminado == false && tab.IndVigente == true && tabM.FechaEnProceso != null
							 select new CasoDTO
							 {
								 Id = tab.Id,
								 IdFlujoServicio = tab.IdFlujoServicio,
								 FechaCreacion = tab.FechaCreacion,
								 FechaModificacion = tab.FechaModificacion,
								 FechaSolicitud = tab.FechaSolicitud,
								 FechaAnulacion = tab.FechaAnulacion,
								 FechaEjecucion = tab.FechaEjecucion,
								 IdMovimiento = tab.IdMovimiento,
								 FechaAtencion = tabM.FechaEnProceso,
								 sLAEstimado = tabFS.SlaPromedio
							 }).FirstOrDefault();
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<CasoDTO> MensajeCasoPorID(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idCaso)
		{
			var tabCaso = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabBuzonMensaje = this.udt.Sesion.CreateObjectSet<BuzonMensaje>();
			var tabMovimiento = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabPaso = this.udt.Sesion.CreateObjectSet<Paso>();
			var tabMensajeMetodos = this.udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabListaValor2 = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var coleccion = from tab in tabCaso
							join tabM in tabMovimiento on tab.Id equals tabM.IdCaso
							join tabBM in tabBuzonMensaje on tabM.Id equals tabBM.IdMovimiento
							where tab.Id == idCaso && tab.IndEliminado == false && tab.IndVigente == true
							select new CasoDTO
							{
								Id = tab.Id,
								IdMovimiento = tab.IdMovimiento,
								FechaCreacionBuzon = tabBM.FechaCreacion,
								Nombrepaso = tabM.Nombre,
								Status = (from tabL2 in tabListaValor2 where (tabL2.Id == (tabBM.Estatus)) select tabL2).FirstOrDefault().NombreValor
							};
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return coleccion;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<ReporteCasosDTO> ReporteCasos(int? idSuscriptor, int? idSucursal, int? idServicio, string poliza, int? numCertificado, string docSolicitante, DateTime? fechaDesde, DateTime? fechaHasta)
		{
			var tabCasos = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicios = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSucursales = this.udt.Sesion.CreateObjectSet<ServicioSucursal>();
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabSucursales = this.udt.Sesion.CreateObjectSet<Sucursal>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var casos = from caso in tabCasos
						join tfs in tabFlujosServicios on caso.IdFlujoServicio equals tfs.Id
						join tss in tabServicioSucursales on tfs.Id equals tss.IdFlujoServicio
						join s in tabSuscriptores on tfs.IdSuscriptor equals s.Id
						join tsu in tabSucursales on tss.IdSucursal equals tsu.Id
						join tlv in tabListaValor on caso.Estatus equals tlv.Id
						join tsrvs in tabServiciosSuscriptor on tfs.IdServicioSuscriptor equals tsrvs.Id
						where tfs.IdSuscriptor == idSuscriptor &&
							  tfs.IndEliminado == false &&
							  tfs.IndVigente == true &&
							  tss.IndEliminado == false &&
							  tss.IndVigente == true &&
							  s.IndEliminado == false &&
							  s.IndVigente == true &&
							  tsu.IndEliminado == false &&
							  tsu.IndVigente == true &&
							  tsrvs.IndEliminado == false &&
							  tsrvs.IndVigente == true &&
							  tlv.IndEliminado == false &&
							  tlv.IndVigente == true
						select new ReporteCasosDTO
						{
							Suscriptor = s.Nombre,
							Sucursal = tsu.Nombre,
							IdSucursal = tsu.Id,
							Servicio = tsrvs.Nombre,
							IdServicio = tsrvs.Id,
							Poliza = caso.NumPoliza,
							Certificado = caso.NumCertificado,
							CIBeneficiario = caso.NumDocBeneficiario,
							Nombre = caso.NombreBeneficiario,
							Apellido = caso.ApellidoBeneficiario,
							NCaso = caso.Id,
							Estatus = tlv.NombreValor,
							FechaCaso = caso.FechaSolicitud,
							FechaEstatus = caso.FechaModificacion,
							FechaCreacion = caso.FechaCreacion,
						};
			if (idSucursal != null)
			{
				casos = casos.Where(tc => tc.IdSucursal == idSucursal);
			}
			if (idServicio != null)
			{
				casos = casos.Where(tc => tc.IdServicio == idServicio);
			}
			if (poliza != null)
			{
				casos = casos.Where(tc => tc.Poliza == poliza);
			}
			if (numCertificado != null)
			{
				casos = casos.Where(tc => tc.Certificado == numCertificado);
			}
			if (docSolicitante != null)
			{
				casos = casos.Where(tc => tc.CIBeneficiario == docSolicitante);
			}
			if ((fechaDesde != null) && (fechaHasta != null))
			{
				casos = casos.Where(tc => (tc.FechaCreacion >= fechaDesde) && (tc.FechaCreacion <= fechaHasta));
			}
			else if ((fechaDesde != null) && (fechaHasta == null))
			{
				casos = casos.Where(tc => (tc.FechaCreacion >= fechaDesde));
			}
			
			casos = UtilidadesDTO<ReporteCasosDTO>.FiltrarColeccionEliminacion(casos, true);
			return casos;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<ReporteMovimientoDTO> ReporteMovimiento(int? idSuscriptor, int? idSucursal, int? idServicio, int? idarea, int? idproveedor, int? idusuario, int? idpais, int? iddivterr1, int? iddivterr2, int? iddivterr3, DateTime? fechaDesde, DateTime? fechaHasta, bool indDueñoFljSrv)
		{
			var tabCasos = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabMovimientos = this.udt.Sesion.CreateObjectSet<Movimiento>();
			var tabFlujosServicios = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSucursales = this.udt.Sesion.CreateObjectSet<ServicioSucursal>();
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabSucursales = this.udt.Sesion.CreateObjectSet<Sucursal>();
			var tabAreas = this.udt.Sesion.CreateObjectSet<Area>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabPaises = this.udt.Sesion.CreateObjectSet<Pais>();
			var tabDivisionesTerritoriales1 = this.udt.Sesion.CreateObjectSet<DivisionesTerritoriales1>();
			var tabDivisionesTerritoriales2 = this.udt.Sesion.CreateObjectSet<DivisionesTerritoriales2>();
			var tabDivisionesTerritoriales3 = this.udt.Sesion.CreateObjectSet<DivisionesTerritoriales3>();
			var tabUsuariosSuscriptores = this.udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
			var tabUsuario = this.udt.Sesion.CreateObjectSet<Usuario>();
			var tabDatosBase = this.udt.Sesion.CreateObjectSet<DatosBase>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabUsuariosLocalidades = this.udt.Sesion.CreateObjectSet<UsuariosLocalidad>();
			var casosmovimientos = (from Mov in tabMovimientos
									join tcs in tabCasos on Mov.IdCaso equals tcs.Id
									join tfs in tabFlujosServicios on tcs.IdFlujoServicio equals tfs.Id
									join tss in tabServicioSucursales on tfs.Id equals tss.IdFlujoServicio
									join s in tabSuscriptores on tfs.IdSuscriptor equals s.Id
									join tsu in tabSucursales on tss.IdSucursal equals tsu.Id
									join tsc in tabServiciosSuscriptor on tfs.IdServicioSuscriptor equals tsc.Id
									join ts in tabSuscriptores on Mov.IdSuscriptor equals ts.Id
									join tp in tabPaises on s.IdPais equals tp.Id
									join tdt1 in tabDivisionesTerritoriales1 on s.IdDivisionTerritorial1 equals tdt1.Id
									join tdt2 in tabDivisionesTerritoriales2 on s.IdDivisionTerritorial2 equals tdt2.Id
									join tdt3 in tabDivisionesTerritoriales3 on s.IdDivisionTerritorial3 equals tdt3.Id
									join tus in tabUsuariosSuscriptores on Mov.UsuarioAsignado equals tus.Id
									join tu in tabUsuario on tus.IdUsuario equals tu.Id
									join tdb in tabDatosBase on tu.Id equals tdb.IdUsuario
									join tlv in tabListaValor on Mov.Estatus equals tlv.Id
									join tlv2 in tabListaValor on tcs.Estatus equals tlv2.Id
									where tfs.IndEliminado == false &&
										  tfs.IndVigente == true &&
										  tss.IndEliminado == false &&
										  tss.IndVigente == true &&
										  s.IndEliminado == false &&
										  s.IndVigente == true &&
										  tsu.IndEliminado == false &&
										  tsu.IndVigente == true &&
										  ts.IndEliminado == false &&
										  ts.IndVigente == true &&
										  tlv.IndEliminado == false &&
										  tlv.IndVigente == true
									select new ReporteMovimientoDTO
									{
										Suscriptor = s.Nombre,
										IdSuscriptorDueño = s.Id,
										IdSuscriptorProveedor = Mov.IdSuscriptor,
										Sucursal = tsu.Nombre,
										IdSucursal = tsu.Id,
										Area = (from Ul in tabUsuariosLocalidades
												join Ar in tabAreas on Ul.IdArea equals Ar.Id into joinArea
												from Ar in joinArea.DefaultIfEmpty()
												where Ul.IndPpal == true &&
													  Ul.IdUsuarioSuscriptor == tus.Id &&
													  Ul.IndVigente == true &&
													  Ul.IndEliminado == false &&
													  Ar.IndVigente == true &&
													  Ar.IndEliminado == false
												select new ReporteMovimientoAreaDTO
												{
													NombreArea = Ar.Nombre,
													IdArea = Ar.Id
												}).DefaultIfEmpty(new ReporteMovimientoAreaDTO() { NombreArea = "N/D", IdArea = 0 }).FirstOrDefault(),
										Servicio = tsc.Nombre,
										IdServicio = tsc.Id,
										Ncaso = tcs.Ticket,
										EstatusCaso = tlv2.NombreValor,
										FechaEstatus = tcs.FechaModificacion,
										NumPoliza = tcs.NumPoliza,
										NumCertificado = tcs.NumCertificado,
										NombreBeneficiario = tcs.NombreBeneficiario,
										ApellidoBeneficiario = tcs.ApellidoBeneficiario,
										CIBeneficiario = tcs.NumDocBeneficiario,
										Proveedor = ts.Nombre,
										IdProveedor = ts.Id,
										Pais = tp.Nombre,
										IdPais = tp.Id,
										DivTerr1 = tdt1.NombreDivTer1,
										IdDivTerr1 = tdt1.Id,
										DivTerr2 = tdt2.NombreDivTer2,
										IdDivTerr2 = tdt1.Id,
										DivTerr3 = tdt3.NombreDivTer3,
										IdDivTerr3 = tdt1.Id,
										Usuario = tdb.Nombre1 + " " + tdb.Apellido1,
										IdUsuario = tu.Id,
										Estatus = tlv.NombreValor,
										FechaMov = tcs.FechaModificacion,
										IdCaso = tcs.Id,
										FechaCreacion = Mov.FechaCreacion,
									}).Where(tc => ((indDueñoFljSrv) ? (tc.IdSuscriptorDueño == idSuscriptor) : (tc.IdSuscriptorProveedor == idSuscriptor)));
			if (idSucursal != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdSucursal == idSucursal);
			}
			if (idServicio != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdServicio == idServicio);
			}
			if (idarea != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.Area.IdArea == idarea);
			}
			if (idproveedor != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdProveedor == idproveedor);
			}
			if (idpais != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdPais == idpais);
			}
			if (iddivterr1 != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdDivTerr1 == iddivterr1);
			}
			if (iddivterr2 != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdDivTerr2 == iddivterr1);
			}
			if (iddivterr3 != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdDivTerr3 == iddivterr1);
			}
			if (idusuario != null)
			{
				casosmovimientos = casosmovimientos.Where(tc => tc.IdUsuario == idusuario);
			}
			if ((fechaDesde != null) && (fechaHasta != null))
			{
				casosmovimientos = casosmovimientos.Where(tc => (tc.FechaCreacion >= fechaDesde) && (tc.FechaCreacion <= fechaHasta));
			}
			else if ((fechaDesde != null) && (fechaHasta == null))
			{
				casosmovimientos = casosmovimientos.Where(tc => (tc.FechaCreacion >= fechaDesde));
			}
			casosmovimientos = UtilidadesDTO<ReporteMovimientoDTO>.FiltrarColeccionEliminacion(casosmovimientos, true);
			return casosmovimientos;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<ReporteConsolidadoServicioDTO> ReporteConsolidadoServicio(int? idSuscriptor, int? idSucursal, int? idServicio, DateTime? fechaDesde, DateTime? fechaHasta)
		{
			var tabCasos = this.udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicios = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSucursales = this.udt.Sesion.CreateObjectSet<ServicioSucursal>();
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabSucursales = this.udt.Sesion.CreateObjectSet<Sucursal>();
			var tabServiciosSuscriptor = this.udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var casos = from caso in tabCasos
						join tfs in tabFlujosServicios on caso.IdFlujoServicio equals tfs.Id
						join tss in tabServicioSucursales on tfs.Id equals tss.IdFlujoServicio
						join s in tabSuscriptores on tfs.IdSuscriptor equals s.Id
						join tsu in tabSucursales on tss.IdSucursal equals tsu.Id
						join tlv in tabListaValor on caso.Estatus equals tlv.Id
						join tsrvs in tabServiciosSuscriptor on tfs.IdServicioSuscriptor equals tsrvs.Id
						where tfs.IdSuscriptor == idSuscriptor &&
							  tfs.IndEliminado == false &&
							  tfs.IndVigente == true &&
							  tss.IndEliminado == false &&
							  tss.IndVigente == true &&
							  s.IndEliminado == false &&
							  s.IndVigente == true &&
							  tsu.IndEliminado == false &&
							  tsu.IndVigente == true &&
							  tsrvs.IndEliminado == false &&
							  tsrvs.IndVigente == true &&
							  tlv.IndEliminado == false &&
							  tlv.IndVigente == true
						select new ReporteConsolidadoServicioDTO
						{
							Suscriptor = s.Nombre,
							Sucursal = tsu.Nombre,
							IdSucursal = tsu.Id,
							Servicio = tsrvs.Nombre,
							IdServicio = tsrvs.Id,
							Estatus = tlv.NombreValor,
							FechaCreacion = caso.FechaCreacion
						};
			if (idSucursal != null)
			{
				casos = casos.Where(tc => tc.IdSucursal == idSucursal);
			}
			if (idServicio != null)
			{
				casos = casos.Where(tc => tc.IdServicio == idServicio);
			}
			if ((fechaDesde != null) && (fechaHasta != null))
			{
				casos = casos.Where(tc => (tc.FechaCreacion >= fechaDesde) && (tc.FechaCreacion <= fechaHasta));
			}
			else if ((fechaDesde != null) && (fechaHasta == null))
			{
				casos = casos.Where(tc => (tc.FechaCreacion >= fechaDesde));
			}
			casos = UtilidadesDTO<ReporteConsolidadoServicioDTO>.FiltrarColeccionEliminacion(casos, true);
			return casos;
		}
		
		public IEnumerable<CasoDTO> LLenargridCasosPendientes(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, DataTable DT)
		{
			var coleccion = (from tab in DT.AsEnumerable()
							 select new CasoDTO
							 {
								 IdCaso = tab.Field<int>("IdCaso"),
								 FechaSolicitud = tab.Field<DateTime>("FechaSolicitud"),
								 IdSuscriptor = tab.Field<int>("IdSuscriptor"),
								 Actividad = tab.Field<string>("Actividad"),
								 IdFlujoServicio = tab.Field<int>("IdFlujoServicio"),
								 Asegurado = tab.Field<string>("Asegurado"),
								 Cedula = tab.Field<string>("Cedula")
							 }).AsQueryable();
			
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			coleccion = UtilidadesDTO<CasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<CasoDTO>.Conteo;
			return coleccion;
		}
		
		public void ObtenerXMLRespuesta(CasoEstatus estatus)
		{
			const int SERVICIO_SUSCRIPTOR_ID_SEGUIMIENTO_AL_MEDICO = 35;
			try
			{
				var tFlujo = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
				var tCaso = this.udt.Sesion.CreateObjectSet<Caso>();
				var respuestas = (from c in tCaso
								  join f in tFlujo on c.IdFlujoServicio equals f.Id
								  where f.IdServicioSuscriptor == SERVICIO_SUSCRIPTOR_ID_SEGUIMIENTO_AL_MEDICO &&
										c.Estatus == (int)estatus
								  select c.XMLRespuesta).ToList();
				var casosSeguimiento = new List<CasoRespuestaXML>();
				var respuestasXML = new List<XElement>();
				foreach (var r in respuestas)
				{
					if (r != null)
					{
						casosSeguimiento.Add(new CasoRespuestaXML(r));
					}
				}
				var q = from c in casosSeguimiento
						select c.Parametros;
				var q2 = from l in q
						 where l.Exists(p => p.Existe("Contratante", "Seguros la Previsora"))
						 select l.First().CasoId;
				var q3 = from l in q
						 where l.Exists(p => p.Existe("espmed", "Gastroenterologo"))
						 select l.First().CasoId;
				var q4 = q3.Intersect(q2);
				var qResult = from c in casosSeguimiento
							  where (q4.ToArray<string>()).Contains(c.CasoId)
							  select new
							  {
								  casoId = c.CasoId,
								  aseguradoNombre = c.ObtenerParametroValor("nomaseg") ?? "",
								  aseguradoDocumentoNumero = c.ObtenerParametroValor("numdocaseg") ?? "",
								  medicoDocumentoTipo = c.ObtenerParametroValor("tipodocmed") ?? "",
								  medicoDocumentoNumero = c.ObtenerParametroValor("numdocmed") ?? "",
								  medicoNombre = c.ObtenerParametroValor("nommed") ?? "",
								  medicoEspecialidad = c.ObtenerParametroValor("espmed") ?? "",
								  medicoTelefono = c.ObtenerParametroValor("tlfmed") ?? ""
							  };
				Debug.WriteLine(qResult.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		
		public int[] BuscarNumeroActividadesPendientes(int? idSuscriptorConectado, int[] listaPoteMov)
		{
			XElement xml = this.convertirArrayIntEnXml(listaPoteMov);
			List<ListaPoteConsultaCasos> listaCasos = new List<ListaPoteConsultaCasos>();
			DateTime fechaInicial = DateTime.Now.AddMonths(-6);
			string fechadesde = string.Format("{0}/{1}/2000", fechaInicial.Month.ToString(), fechaInicial.Day.ToString());
			DateTime fechafinal = DateTime.Now.AddDays(1);
			string fechahasta = string.Format("{0}/{1}/{2}", fechafinal.Month.ToString(), fechafinal.Day.ToString(), fechafinal.Year.ToString());
			int[] fre = new int[3];
			
			using (BD_HC_Tramitador dataBase = new BD_HC_Tramitador())
			{
				listaCasos = dataBase.pa_PoteConsultaCasos(null, null, true, null, null, null, null, null, null, null, null, 3, fechadesde, fechahasta, xml.ToString(), 1, 1000, null, idSuscriptorConectado).ToList();
			}
			
			if (listaCasos.Count() > 0)
			{
				var casoshc1 = listaCasos.First();
				if (casoshc1 != null)
				{
					if (casoshc1.TotalCols != null)
					{
						if (casoshc1.TotalCols.Value > 1000)
						{
							fre[0] = 1000;
							fre[1] = 1;
						}
						else
						{
							if (casoshc1.TotalCols != null)
							{
								fre[0] = casoshc1.TotalCols.Value;
							}
							fre[1] = 0;
						}
						if (casoshc1.IdMovimiento != null)
						{
							fre[2] = casoshc1.IdMovimiento.Value;
						}
						else
						{
							fre[2] = 0;
						}
					}
				}
			}
			return fre;
		}
		
		public string AtributoNombre
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoNombreGenerales"];
			}
		}
		
		public string AtributoValor
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoValor"];
			}
		}
		
		public string IdSusIntermediaro
		{
			get
			{
				return @"IDSUSINTERMEDIARIO";
			}
		}
		
		public string Intermediario
		{
			get
			{
				return @"Intermediario";
			}
		}
		
		public string Asegurado
		{
			get
			{
				return @"NomAseg";
			}
		}
		
		public string AseguradoC
		{
			get
			{
				return @"NOMCOMPLETOBENEF";
			}
		}
		
		public string TipoDocumentoAseg
		{
			get
			{
				return @"TIPDOCASEG";
			}
		}
		
		public string NumDocT
		{
			get
			{
				return @"NUMDOCTIT";
			}
		}
		
		public string TipoDocumentoBenef
		{
			get
			{
				return @"TIPDOCBENEF";
			}
		}
		
		public string NumDocAseg
		{
			get
			{
				return @"NUMDOCASEG";
			}
		}
		
		public string NumDocBenef
		{
			get
			{
				return @"NUMDOCBENEF";
			}
		}
		
		public string CasoExternoAseg
		{
			get
			{
				return @"IDCASOEXTERNO";
			}
		}
		
		public string CasoExternoBenef
		{
			get
			{
				return @"RECLAMO";
			}
		}

		#endregion DTO
	}
}