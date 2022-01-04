using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using HConnexum.Infraestructura;
using HConnexum.Servicios.Servicios;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase ConsultaCasosPresentador.</summary>
	public class PoteCasosPresentador : PresentadorListaBase<Caso>
	{
		///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
		readonly IPoteCasos vista;
		IEnumerable<PoteCasoDTO> datos;
		IEnumerable<PoteCasoDTO> datosSuscriptor;
		int[] listaPoteMov = new int[2];
		int? idsuscriptor = null;
		readonly bool incluirmvtos = true;
		int? idcaso = null;
		int? idcasoexterno = null;
		readonly int? idcasoexterno2 = null;
		int? idcasoexterno3 = null;
		string idticket = null;
		string numdoctit = null;
		readonly int? tipobusqueda = 3;
		readonly DateTime fechaInicial = DateTime.Now.AddMonths(-6);
		string fechadesde = "";
		readonly DateTime fechafinal = DateTime.Now.AddDays(1);
		string fechahasta = "";
		string asegurado = null;
		string intermediario = null;
		int? servicio = null;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PoteCasosPresentador(IPoteCasos vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = this.GetType();
			this.listaPoteMov = this.ObtenerListaSiguienteActividad();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int usuarioSuscriptor, int suscriptor, int idMovimiento)
		{
			try
			{
				int numpagina = pagina;
				int cantregistros = tamañoPagina;
				string BusquedaPorCedula = string.Empty;
				
				if (idMovimiento != 0)
				{
					this.listaPoteMov[0] = idMovimiento;
				}
				if (this.vista.TipoFiltro.Trim() != "" && !string.IsNullOrEmpty(this.vista.Filtro))
				{
					
					#region Filtros
					
					if (this.vista.TipoFiltro == "Id")
					{
						this.idcaso = int.Parse(this.vista.Filtro);
					}
					else if (this.vista.TipoFiltro == "Idcasoexterno")
					{
						this.idcasoexterno = int.Parse(this.vista.Filtro);
					}
					else if (this.vista.TipoFiltro == "SupportIncident")
					{
						this.idcasoexterno3 = int.Parse(this.vista.Filtro);
					}
					else if (this.vista.TipoFiltro == "Ticket")
					{
						this.idticket = this.vista.Filtro;
					}
					else if (this.vista.TipoFiltro == "cedula")
					{
						this.numdoctit = this.vista.Filtro;
					}
				
					#endregion
					
				}
				if (!string.IsNullOrEmpty(this.vista.Asegurado))
				{
					this.asegurado = this.vista.Asegurado;
				}
				if (!string.IsNullOrEmpty(this.vista.Intermediario))
				{
					this.intermediario = this.vista.Intermediario;
				}
				if (!string.IsNullOrEmpty(this.vista.Suscriptor))
				{
					this.idsuscriptor = int.Parse(this.vista.Suscriptor);
				}
				if (!string.IsNullOrEmpty(this.vista.Servicio))
				{
					this.servicio = int.Parse(this.vista.Servicio);
				}
				if (!string.IsNullOrEmpty(this.vista.FechaDesde))
				{
					DateTime Fecha = DateTime.Parse(this.vista.FechaDesde);
					this.fechadesde = string.Format("{0}/{1}/{2}", Fecha.Month.ToString(), Fecha.Day.ToString(), Fecha.Year.ToString());
				}
				else
				{
					this.fechadesde = string.Format("{0}/{1}/2000", this.fechaInicial.Month.ToString(), this.fechaInicial.Day.ToString());
				}
				if (!string.IsNullOrEmpty(this.vista.FechaHasta))
				{
					DateTime Fecha1 = DateTime.Parse(this.vista.FechaHasta).AddDays(1);
					this.fechahasta = string.Format("{0}/{1}/{2}", Fecha1.Month.ToString(), Fecha1.Day.ToString(), Fecha1.Year.ToString());
				}
				else
				{
					this.fechahasta = string.Format("{0}/{1}/{2}", this.fechafinal.Month.ToString(), this.fechafinal.Day.ToString(), this.fechafinal.Year.ToString());
				}
				CasoRepositorio repositorioCasos = new CasoRepositorio(this.udt);
				this.datos = repositorioCasos.ObtenerDTO(orden, pagina, tamañoPagina, this.idsuscriptor, this.servicio, true, this.idcaso, this.idcasoexterno, this.idcasoexterno2, this.idcasoexterno3, this.idticket, this.numdoctit, this.asegurado, this.intermediario, this.tipobusqueda, this.fechadesde, this.fechahasta, this.listaPoteMov, this.vista.UsuarioActual.SuscriptorSeleccionado.Id);
				this.vista.NumeroDeRegistros = repositorioCasos.Conteo;
				this.vista.Datos = this.datos;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
			
		public int[] ObtenerListaSiguienteActividad()
		{
			ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
			try
			{
				int idAplicacion = int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
				foreach (HConnexum.Seguridad.RolesUsuario Rol in this.vista.UsuarioActual.AplicacionActual(idAplicacion).Roles)
				{
					if (Rol.NombreRol == ConfigurationManager.AppSettings[@"RolSimulaProveedor"].ToString())
					{
						this.vista.BIndSimulado = true;
						break;
					}
				}
				string FiltrosBusqueda = string.Format(" WHERE (TB_CasosExternos.IdSuscriptorProv = {0} OR TB_CasosExternos.IdSuscriptor = {1})", this.vista.UsuarioActual.SuscriptorSeleccionado.Id, this.vista.UsuarioActual.SuscriptorSeleccionado.Id);
				FiltrosBusqueda += " AND TB_CasosExternos.IdEstatusMovimiento = 154 ";
				return servicio.ObtenerListaSiguienteActividad(this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado, this.vista.UsuarioActual.SuscriptorSeleccionado.Id, this.vista.BIndSimulado, "", 1, 10, FiltrosBusqueda);
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
				{
					HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", ex.InnerException.Message));
				}
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
				{
					servicio.Close();
				}
			}
			return null;
		}
			
		public bool AtenderActividad(int idMov)
		{
			try
			{
				this.udt.IniciarTransaccion();
				ListasValorRepositorio listasValorRepositorio = new ListasValorRepositorio(this.udt);
				MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(this.udt);
				Movimiento movimiento = movimientoRepositorio.ObtenerPorId(idMov);
				movimiento.UsuarioAsignado = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
				movimiento.IdSuscriptor = this.vista.UsuarioActual.SuscriptorSeleccionado.Id;
				movimiento.Estatus = listasValorRepositorio.ObtenerListaValoresDTO(WebConfigurationManager.AppSettings[@"ListaEstatusMovimiento"], WebConfigurationManager.AppSettings[@"ListaValorEstatusEnProceso"]).Id;
				this.udt.MarcarModificado(movimiento);
				this.udt.Commit();
				return true;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return false;
		}
			
		public DataTable XElementToDataTable(XElement x)
		{
			DataTable dt = new DataTable();
			XElement setup = (from p in x.Descendants() select p).First();
			dt.Columns.Add(new DataColumn("IdCaso", typeof(int))); // add columns to your dt
			dt.Columns.Add(new DataColumn("FechaSolicitud", typeof(DateTime)));
			dt.Columns.Add(new DataColumn("IdSuscriptor", typeof(int)));
			dt.Columns.Add(new DataColumn("Actividad", typeof(string)));
			dt.Columns.Add(new DataColumn("IdFlujoServicio", typeof(int)));
			dt.Columns.Add(new DataColumn("XMLcaso", typeof(string)));
			var all = from p in x.Descendants(setup.Name.ToString()) select p;
			foreach (XElement xe in all)
			{
				DataRow dr = dt.NewRow();
				foreach (XElement xe2 in xe.Descendants())
				{
					dr[xe2.Name.ToString()] = xe2.Value; //add in the values
				}//add in the values
				dt.Rows.Add(dr);
			}
			dt.Columns.Add(new DataColumn("Asegurado", typeof(string)));
			dt.Columns.Add(new DataColumn("Cedula", typeof(string)));
			return dt;
		}
			
		public string ObtenerValorParametroXmlRespuesta(string xml, string nombreParametro)
		{
			try
			{
				string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
				string atributoNombreRespuesta = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
				string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
				XElement xmlRespuesta = XElement.Parse(xml);
				string valor = (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
								where p.Attribute(atributoNombreRespuesta).Value.ToUpper() == nombreParametro
								select p.Attribute(atributoValor).Value).FirstOrDefault();
				return string.Format("{0}{1}", string.Empty, valor);
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
				{
					HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", ex.InnerException.Message));
				}
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return string.Empty;
		}
			
		public void ObtenerIdSuscriptor(string Suscriptor)
		{
			SuscriptorRepositorio repositorio = new SuscriptorRepositorio(this.udt);
			IEnumerable<SuscriptorDTO> sus = repositorio.ObtenerSuscriptoresPorNombresDTO(Suscriptor);
			this.vista.idIntermediario = sus.First().Id;
		}
			
		public void LlenarSuscriptor()
		{
			this.listaPoteMov = this.ObtenerListaSiguienteActividad();
			DataTable table = new DataTable();
			string filtrosBusqueda = string.Format(" WHERE (TB_CasosExternos.IdSuscriptor = {0} OR TB_CasosExternos.IdSuscriptorProv = {1})", this.vista.UsuarioActual.SuscriptorSeleccionado.Id, this.vista.UsuarioActual.SuscriptorSeleccionado.Id);
			filtrosBusqueda += " AND TB_CasosExternos.IdEstatusMovimiento = 154 ";
			CasoRepositorio repositorioCasos = new CasoRepositorio(this.udt);
			this.datosSuscriptor = repositorioCasos.ObtenerSuscriptorespaPoteDTO(this.listaPoteMov, filtrosBusqueda);
											  
			var vistaComboSuscriptores = (from S in this.datosSuscriptor
										  select new
										  {
											  IdSuscriptor = S.IdSuscriptor,
											  NombreSuscriptor = S.NombreSuscriptor
										  }).Distinct();
		
			DataView view = new DataView(LinqtoDataSetMethods.CopyToDataTable(vistaComboSuscriptores));
			table = view.ToTable(true, "IdSuscriptor", "NombreSuscriptor");
			this.vista.ComboSuscriptores = table;
		}
			
		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboServicios(int idSuscriptor)
		{
			try
			{
				FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(this.udt);
				IEnumerable<FlujosServicioDTO> listadoFlujoServicio = null;
				listadoFlujoServicio = repositorioFlujosServicio.ObtenerDTOporSuscriptorSinAuditoria(idSuscriptor);
				List<FlujosServicioDTO> listadoFlujoServicioA = listadoFlujoServicio.ToList();
				DataTable listadoIdSubServicio = this.BuscarNombreServicios(idSuscriptor);
				for (int i = 0; i < listadoFlujoServicioA.Count; i++)
				{
					int j;
					bool find = false;
					for (j = 0; j < listadoIdSubServicio.Rows.Count && find == false; j++)
					{
						if (listadoFlujoServicioA[i].IdServicioSuscriptor == int.Parse(listadoIdSubServicio.Rows[j]["Id"].ToString()))
						{
							find = true;
						}
					}
					if (find == true)
					{
						listadoFlujoServicioA[i].NombreServicioSuscriptor = listadoIdSubServicio.Rows[j - 1]["Nombre"].ToString();
					}
					else
					{
						listadoFlujoServicioA[i].NombreServicioSuscriptor = "";
					}
				}
				var todos = (from D in listadoFlujoServicioA
							 where D.NombreServicioSuscriptor != ""
							 select new FlujosServicioDTO
							 {
								 Id = D.Id,
								 IdServicioSuscriptor = D.IdServicioSuscriptor,
								 NombreServicioSuscriptor = D.NombreServicioSuscriptor
							 });
				this.vista.ComboServicios = todos;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
			
		private DataTable BuscarNombreServicios(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerServiciosPorIdSuscriptor(idSuscriptor);
				if (ds.Tables[@"Error"] != null)
				{
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				}
				if (ds.Tables[0].Rows.Count > 0)
				{
					return ds.Tables[0];
				}
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
				{
					HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", ex.InnerException.Message));
				}
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
				{
					servicio.Close();
				}
			}
			return null;
		}
			
		public string BuscaUrlCasosExternos(string Servicio)
		{
			ListasValorRepositorio repositoriosLV = new ListasValorRepositorio(this.udt);
			return repositoriosLV.ObtenerUrlCasosExternos(Servicio);
		}
			
		public string BuscarValorServicio(int idListaValor)
		{
			try
			{
				ListasValorRepositorio repositorioLV = new ListasValorRepositorio(this.udt);

				if(!string.IsNullOrEmpty(repositorioLV.TipoServicio(idListaValor)))
					return repositorioLV.TipoServicio(idListaValor);
				else
					throw new Exception("No existe servicio asociado a este caso");
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				return null;
			}
		}
			
		public void actualizarBuzonChatHC1(string idcaso)
		{
			if (!string.IsNullOrEmpty(idcaso))
			{
				int caso = int.Parse(idcaso);
				BuzonChatRepositorio repo = new BuzonChatRepositorio(this.udt);
				repo.ActualizoChatPorIdCaso(caso);
			}
		}
	}
}