using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Xml.Linq;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: MovimientoRepositorio.</summary>	
	public sealed class MovimientoRepositorio : RepositorioBase<Movimiento>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase MovimientoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public MovimientoRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}

		#endregion "Constructores"

		#region DTO
		public IEnumerable<MovimientoDTO> ObtenerMovimientosDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idCaso)
		{
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var tabListaValor = udt.Sesion.CreateObjectSet<ListaValor>();
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var coleccion = from tab in tabMovimiento
							join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
							join tabTP in tabTipoPaso on tab.TipoMovimiento equals tabTP.Id
							where tab.IdCaso == idCaso && tab.IndVigente == true && tab.IndEliminado == false &&
								tab.FechaValidez <= DateTime.Now
							select new MovimientoDTO
							{
								Id = tab.Id
									,
								IdCaso = tab.IdCaso
									,
								Movimiento = tab.Nombre
									,
								NombreEstatusCaso = tabLV.NombreValor
									 ,
								NombreTipoMovimiento = tabTP.Descripcion
									   ,
								IndVigente = tab.IndVigente
									,
								FechaValidez = tab.FechaValidez
									,
								IndEliminado = tab.IndEliminado
							};

			coleccion = UtilidadesDTO<MovimientoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			Conteo = UtilidadesDTO<MovimientoDTO>.Conteo;
			return UtilidadesDTO<MovimientoDTO>.EncriptarId(coleccion);
		}
        /// <summary>Meotdo que llena el grid de CONSULTA DE MOVIMIENTO POR SUSCRIPTORES Y USUARIOS ASIGNADOS</summary>
        /// <param name="orden"></param>
        /// <param name="pagina"></param>
        /// <param name="registros"></param>
        /// <param name="parametrosFiltro"></param>
        /// <returns></returns>
        public IEnumerable<MovimientoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            int[] estatus = { 154, 155 };
            var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
            var tabListaValor = udt.Sesion.CreateObjectSet<ListaValor>();
            var tabCaso = udt.Sesion.CreateObjectSet<Caso>();
            var TabFlujoServicio=udt.Sesion.CreateObjectSet<FlujosServicio>();
            string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
            string atributoNombre = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
            string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
            var coleccion = from tab in tabMovimiento
                            join tabLV in tabListaValor on tab.Estatus equals tabLV.Id
                            join tcaso in tabCaso on tab.IdCaso equals tcaso.Id
                            join tFlujoS in TabFlujoServicio on tcaso.IdFlujoServicio equals tFlujoS.Id
                            where tab.IndVigente == true && tab.IndEliminado == false &&
                                tab.FechaValidez <= DateTime.Now && tab.TipoMovimiento==1 
                                && estatus.Contains(tab.Estatus)
                            select new MovimientoDTO
                            {
                                Id = tab.Id,
                                IdCaso = tab.IdCaso,
                                Movimiento = tab.Nombre,
                                Estatus = tabLV.NombreValor,                                 
                                Idsuscriptor = tab.IdSuscriptor == null ? 0 : tab.IdSuscriptor.Value,
                                IdUsuarioAsignado = tab.UsuarioAsignado == null ? 0 : tab.UsuarioAsignado.Value,
                                IndVigente = tab.IndVigente,
                                FechaCreacion=tab.FechaCreacion,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado,
                                CasoRespuestaXML=tcaso.XMLRespuesta,
                                IdServicioSuscriptor=tFlujoS.IdServicioSuscriptor
                            };
            
            
            coleccion = UtilidadesDTO<MovimientoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            Conteo = UtilidadesDTO<MovimientoDTO>.Conteo;

            foreach (MovimientoDTO item in coleccion)
                    {
                        UsuarioRepositorio repositorioUsuario = new UsuarioRepositorio(udt);
                     if (item.IdUsuarioAsignado != 0)
                            item.UsuarioAsignado = repositorioUsuario.ObtenerNombreUsuario(item.IdUsuarioAsignado);
                        else item.UsuarioAsignado = "";

                     XElement xmlRespuesta = XElement.Parse(item.CasoRespuestaXML);

                     item.NombreAsegurado = string.Empty + (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
                                                            where p.Attribute(atributoNombre).Value.ToUpper() == @"NOMASEG"
                                                              select p.Attribute(atributoValor).Value).SingleOrDefault();
                     item.DocumentoAsegurado = string.Empty + (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
                                                               where p.Attribute(atributoNombre).Value.ToUpper() == @"TIPDOCASEG"
                                                               select p.Attribute(atributoValor).Value).SingleOrDefault() +"-"+ 
                                                               (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
                                                               where p.Attribute(atributoNombre).Value.ToUpper() == @"NUMDOCASEG"
                                                              select p.Attribute(atributoValor).Value).SingleOrDefault();
                     item.Intermediario = string.Empty + (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
                                                          where p.Attribute(atributoNombre).Value.ToUpper() == @"INTERMEDIARIO"
                                                              select p.Attribute(atributoValor).Value).SingleOrDefault();
                     
                    }
            return UtilidadesDTO<MovimientoDTO>.EncriptarId(coleccion);
        }
        
        public string ObtenerUsuarioPorIdUsuarioSuscriptor(int idUsuarioSuscriptor)
        {
            var tabUsuario = this.udt.Sesion.CreateObjectSet<Usuario>();
            var tabDatosBase = this.udt.Sesion.CreateObjectSet<DatosBase>();
            var tabUsuarioSuscriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
            var coleccion = (from db in tabDatosBase
                             join u in tabUsuario on
                             db.IdUsuario equals u.Id
                             join us in tabUsuarioSuscriptor
                             on u.Id equals us.IdUsuario
                             where us.Id == idUsuarioSuscriptor
                             orderby u.LoginUsuario
                             select new UsuarioDTO
                             {
                                 Nombre = db.Nombre1+" "+db.Apellido1,
                                
                             }).SingleOrDefault();
            return coleccion.Nombre;
        }
        
		public ListasValorDTO ObtenerIdListaValor(string NombreValorCorto, string NombreLista)
		{
			var tabListaValor = udt.Sesion.CreateObjectSet<ListaValor>();
			var tabLista = udt.Sesion.CreateObjectSet<Lista>();
			var IdListaValor = (from tLV in tabListaValor
								join tl in tabLista on tLV.IdLista equals tl.Id
								where tLV.NombreValorCorto == NombreValorCorto && tl.Nombre == NombreLista
								select new ListasValorDTO
								{
									Id = tLV.Id
								}).SingleOrDefault();
			
			return IdListaValor;
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableMovimientoDTO.</returns>
		public MovimientoDTO ObtenerDTO(int id)
		{
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var tabCaso = udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujoServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabListaValor = udt.Sesion.CreateObjectSet<ListaValor>();
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
			var tabUsuarioSuscriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
			var tabUsuario = udt.Sesion.CreateObjectSet<Usuario>();
			var tabDatosBase = udt.Sesion.CreateObjectSet<DatosBase>();
			var coleccion = (from tab in tabMovimiento
							 orderby tab.Nombre
							 join tcaso in tabCaso
							 on tab.IdCaso equals tcaso.Id
							 join tflujo in tabFlujoServicio
							 on tcaso.IdFlujoServicio equals tflujo.Id
							 join tsersus in tabServicioSuscriptor
							 on tflujo.IdServicioSuscriptor equals tsersus.Id
							 join tlisval1 in tabListaValor
							 on tcaso.Estatus equals tlisval1.Id
							 join tpaso in tabPaso
							 on tab.IdPaso equals tpaso.Id
							 join ttipop in tabTipoPaso
							 on tpaso.IdTipoPaso equals ttipop.Id
							 join tlisval in tabListaValor
							 on tab.Estatus equals tlisval.Id
							 join tusus in tabUsuarioSuscriptor.DefaultIfEmpty()
							 on tab.CreadoPor equals tusus.Id
							 join tusu in tabUsuario.DefaultIfEmpty()
							 on tusus.IdUsuario equals tusu.Id
							 join tdbase in tabDatosBase.DefaultIfEmpty()
							 on tusu.Id equals tdbase.IdUsuario
							 join tusus2 in tabUsuarioSuscriptor on tab.ModificadoPor equals tusus2.Id into tab_tusus2
							 from tabUsus2 in tab_tusus2.DefaultIfEmpty()
							 join TabUsu in tabUsuario on tabUsus2.IdUsuario equals TabUsu.Id into tab_TabUsu
							 from tUsu in tab_TabUsu.DefaultIfEmpty()
							 join TabBase in tabDatosBase on tUsu.Id equals TabBase.IdUsuario into TabUsu_TabBase
							 from tBase in TabUsu_TabBase.DefaultIfEmpty()
							 where tab.Id == id
							 select new MovimientoDTO
							 {
								 Id = tab.Id,
								 NombreServicio = tsersus.Nombre,
								 IdCaso = tcaso.Id,
								 NombreEstatusCaso = tlisval1.NombreValor,
								 FechaSolicitud = tcaso.FechaSolicitud,
								 Solicitante = tcaso.NombreBeneficiario + " " + tcaso.ApellidoBeneficiario,
								 MovilSolicitante = tcaso.MovilBeneficiario,
								 NombreTipoMovimiento = ttipop.Descripcion,
								 NombreEstatusMovimiento = tlisval.NombreValor,
								 NombrePaso = tpaso.Nombre,
								 FechaCreacion = tcaso.FechaCreacion,
								 NombreCreadoPor = tdbase.Nombre1 + " " + tdbase.Apellido1,
								 Movimiento = tab.Nombre,
                                 DescripcionPaso = tpaso.Observacion,
								 FechaModificacion = tcaso.FechaModificacion,
								 NombreModificadoPor = tBase != null ? tBase.Nombre1 + " " + tBase.Apellido1 : string.Empty,
								 FechaEnProceso = tab.FechaEnProceso,
								 IndObligatorio = !tpaso.IndObligatorio,
                                 IdPaso = tab.IdPaso,
                                 indChat = tflujo.IndChat,
                                 Estatus = tlisval.NombreValor
							 }).SingleOrDefault();

			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<MovimientoDTO> ObtenerMovimientosPorUsuarioAsignado(int Id)
		{
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var tabCaso = udt.Sesion.CreateObjectSet<Caso>();
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabListaValor = udt.Sesion.CreateObjectSet<ListaValor>();
			var tabLista = udt.Sesion.CreateObjectSet<Lista>();
			var tabSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var coleccion = from tab in tabMovimiento
							join tabC in tabCaso
							on tab.IdCaso equals tabC.Id
							join tabFS in tabFlujosServicio
							on tabC.IdFlujoServicio equals tabFS.Id
							join tabLV in tabListaValor
							on tab.Estatus equals tabLV.Id
							join tabS in tabSuscriptor
							on tabFS.IdSuscriptor equals tabS.Id
							join tabP in tabPaso
							on tab.IdPaso equals tabP.Id
							where tab.UsuarioAsignado == Id
							&& tab.TipoMovimiento == (from tabTP in tabTipoPaso
													  where tabTP.Descripcion == "Pantalla"
													  select tabTP.Id).FirstOrDefault()
							&& (tab.Estatus == (from tabLV2 in tabListaValor
												join tabL in tabLista
												on tabLV2.IdLista equals tabL.Id
												where tabL.Nombre == "Estatus del Movimiento"
												&& tabLV2.NombreValorCorto == "EN PROCESO"
												select tabLV2.Id).FirstOrDefault()
							|| tab.Estatus == (from tabLV2 in tabListaValor
											   join tabL in tabLista
											   on tabLV2.IdLista equals tabL.Id
											   where tabL.Nombre == "Estatus del Movimiento"
											   && tabLV2.NombreValorCorto == "PENDIENTE"
											   select tabLV2.Id).FirstOrDefault())
							&& (tabC.Estatus == (from tabLV2 in tabListaValor
												 join tabL in tabLista
												 on tabLV2.IdLista equals tabL.Id
												 where tabL.Nombre == "Estatus del Caso"
												 && tabLV2.NombreValorCorto == "PROC"
												 select tabLV2.Id).FirstOrDefault()
							|| tabC.Estatus == (from tabLV2 in tabListaValor
												join tabL in tabLista
												on tabLV2.IdLista equals tabL.Id
												where tabL.Nombre == "Estatus del Caso"
												&& tabLV2.NombreValorCorto == "PEND"
												select tabLV2.Id).FirstOrDefault())
							&& tabC.IndVigente == true
							&& tabC.IndEliminado == false
							&& tabC.FechaValidez <= DateTime.Now
							select new MovimientoDTO
							{
								Id = tab.Id,
								IdCaso = tabC.Id,
								Movimiento = tab.Nombre,
								NombreEstatusMovimiento = tabLV.NombreValor,
								NombreSuscriptor = tabS.Nombre,
                                Solicitante = tabC.NombreBeneficiario + " " + tabC.ApellidoBeneficiario,
								FechaCreacion = tab.FechaCreacion,
								SLAToleranciaPaso = tabP.SlaTolerancia,
								IdServiciosuscriptor = tabFS.IdServicioSuscriptor,
								IndEliminado = tab.IndEliminado,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
                                CasoRespuestaXML = tabC.XMLRespuesta
							};
			coleccion = UtilidadesDTO<MovimientoDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		public MovimientoDTO ObternerPorMovimiento(int idmov)
		{
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();

			var coleccion = (from tab in tabMovimiento
							 join tPaso in tabPaso on tab.IdPaso equals tPaso.Id
							 where tab.Id == idmov
							 select new MovimientoDTO
							 {
								 FechaCreacion = tab.FechaCreacion,
								 FechaModificacion = tab.FechaModificacion,
								 FechaEjecucion = tab.FechaEjecutado,
								 TiempoEstimado = tPaso.SlaTolerancia
							 }).SingleOrDefault();
			return coleccion;
		}

		public MovimientoDTO ObtenerAuditoria(int idmov)
		{
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var coleccion = (from tab in tabMovimiento
							 where tab.Id == idmov
							 select new MovimientoDTO
							 {
								 CreadoPor = tab.CreadoPor,
								 FechaCreacion = tab.FechaCreacion,
								 ModificadoPor = tab.ModificadoPor,
								 FechaModificacion = tab.FechaModificacion,
								 FechaOmision = tab.FechaOmitido,
								 FechaEjecucion = tab.FechaEjecutado
							 }).SingleOrDefault();
			return coleccion;
		}


        public MovimientoDTO ObtenerMovimiento(int idmov)
        {
            var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
            var coleccion = (from tab in tabMovimiento
                             where tab.Id == idmov
                             select new MovimientoDTO
                             {
                                 CreadoPor = tab.CreadoPor,
                                 FechaCreacion = tab.FechaCreacion,
                                 ModificadoPor = tab.ModificadoPor,
                                 FechaModificacion = tab.FechaModificacion,
                                 FechaOmision = tab.FechaOmitido,
                                 FechaEjecucion = tab.FechaEjecutado,
                                 EstatusMovimiento = tab.Estatus
                             }).SingleOrDefault();
            return coleccion;
        }

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableMovimientoDTO.</returns>
		public IEnumerable<MovimientoDTO> ObtenerMovimientoPorcasoDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idCaso)
		{
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var tabListavalor = udt.Sesion.CreateObjectSet<ListaValor>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var tabUsuario = udt.Sesion.CreateObjectSet<Usuario>();
            var tabDatosBase = udt.Sesion.CreateObjectSet<DatosBase>();
            var tabUsuarioSuscriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();

            var coleccion = from tab in tabMovimiento
                            join tabP in tabPaso on tab.IdPaso equals tabP.Id
                            join tabTP in tabTipoPaso on tabP.IdTipoPaso equals tabTP.Id
                            join tabLv in tabListavalor on tab.Estatus equals tabLv.Id
                            join tusus2 in tabUsuarioSuscriptor on tab.UsuarioAsignado equals tusus2.Id into tab_tusus2
                            from tabUsus2 in tab_tusus2.DefaultIfEmpty()
                            join TabUsu in tabUsuario on tabUsus2.IdUsuario equals TabUsu.Id into tab_TabUsu
                            from tUsu in tab_TabUsu.DefaultIfEmpty()
                            join TabBase in tabDatosBase on tUsu.Id equals TabBase.IdUsuario into TabUsu_TabBase
                            from tBase in TabUsu_TabBase.DefaultIfEmpty()
                            where tab.IdCaso == idCaso && tab.IndVigente == true && tab.IndEliminado == false &&
								tab.FechaValidez <= DateTime.Now && tabP.IndSeguimiento == true
                            select new MovimientoDTO
                            {
                                Id = tab.Id,
                                FechaCreacion = tab.FechaCreacion,
                                NombrePaso = tabP.Nombre,
                                Estatus = tabLv.NombreValor,
                                TipoP = tabTP.Descripcion,
                                UsuarioAsignado = tBase.Nombre1 + " " + tBase.Apellido1
                            };
			coleccion = UtilidadesDTO<MovimientoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<MovimientoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<MovimientoDTO>.Conteo;
			return UtilidadesDTO<MovimientoDTO>.EncriptarId(coleccion);
		}
		public int? ValidarMovimientoTipoPasoServicio(int idmov)
		{
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
			var Movimiento = (from M in tabMovimiento
							  join P in tabPaso on M.IdPaso equals P.Id
							  where M.Id == idmov & P.IdTipoPaso == 3 & M.IndSeguimiento == true & P.IndSegSubServicio == true
							  select new { IdCasoRelacionado = M.IdCasoRelacionado }
							   ).SingleOrDefault();
			if(Movimiento != null)
				return Movimiento.IdCasoRelacionado;
			else return null;
		}
		#endregion DTO

		public bool ValidarReverso(int idCaso, int idMovimiento)
		{
			string nombreListaEstatusMov = ConfigurationManager.AppSettings[@"ListaEstatusMovimiento"];
			string[] estatusPadre = ConfigurationManager.AppSettings[@"EstatusMovimientoSeleccionReverso"].Split(';');
			var tabListasValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabListas = this.udt.Sesion.CreateObjectSet<Lista>();
			var idEstatusMov = from lv in tabListasValor
							   join l in tabListas
							   on lv.IdLista equals l.Id
							   where l.Nombre == nombreListaEstatusMov &&
									 estatusPadre.Contains(lv.NombreValorCorto)
							   select lv.Id;
			string tipoPantalla = ConfigurationManager.AppSettings[@"TipoPasoPantalla"];
			var tabTipoPaso = this.udt.Sesion.CreateObjectSet<TipoPaso>();
			var idTipoPantalla = (from tp in tabTipoPaso
								  where tp.Descripcion == tipoPantalla
								  select tp.Id).SingleOrDefault();
			string estatusEjecutado = ConfigurationManager.AppSettings[@"ListaValorEstatusEjecutado"];
			var idEstatusEjecutado = (from lvep in tabListasValor
									  join lep in tabListas
									  on lvep.IdLista equals lep.Id
									  where lep.Nombre == nombreListaEstatusMov &&
											 lvep.NombreValorCorto == estatusEjecutado
									  select lvep.Id).SingleOrDefault();
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var movimientosPadres = from mnrp in tabMovimiento
									where mnrp.IdCaso == idCaso &&
										mnrp.IndVigente == true &&
										mnrp.IndEliminado == false &&
										mnrp.FechaValidez <= DateTime.Now &&
										mnrp.TipoMovimiento == idTipoPantalla &&
										idEstatusMov.Contains(mnrp.Estatus)
									select mnrp.IdMovimientoPadre;
			var movimientosTodos = from mt in tabMovimiento
								   orderby mt.Id descending
								   where mt.IdCaso == idCaso &&
									   mt.IndVigente == true &&
									   mt.IndEliminado == false &&
									   mt.FechaValidez <= DateTime.Now &&
									   mt.Id == idMovimiento
								   select new
								   {
									   Id = mt.Id,
									   Nombre = mt.Nombre,
									   TipoMovimiento = mt.TipoMovimiento,
									   Estatus = mt.Estatus,
									   Reversable = false
								   };
			var movimientosReversables = from mr in tabMovimiento
										 orderby mr.Id descending
										 where mr.IdCaso == idCaso &&
											 mr.IndVigente == true &&
											 mr.IndEliminado == false &&
											 mr.TipoMovimiento == idTipoPantalla &&
											 mr.Estatus == idEstatusEjecutado &&
											 mr.FechaValidez <= DateTime.Now &&
											 (movimientosPadres).Contains(mr.Id) &&
											 mr.Id == idMovimiento
										 select new
										 {
											 Id = mr.Id,
											 Nombre = mr.Nombre,
											 TipoMovimiento = mr.TipoMovimiento,
											 Estatus = mr.Estatus,
											 Reversable = true
										 };
			var union = movimientosTodos.Union(movimientosReversables).OrderByDescending(m => m.Id);
			var movimientosNoReversables = from mnr in tabMovimiento
										   orderby mnr.Id descending
										   where mnr.IdCaso == idCaso &&
											   mnr.IndVigente == true &&
											   mnr.IndEliminado == false &&
											   mnr.TipoMovimiento == idTipoPantalla &&
											   mnr.Estatus == idEstatusEjecutado &&
											   mnr.FechaValidez <= DateTime.Now &&
											   (movimientosPadres).Contains(mnr.Id) &&
											   mnr.Id == idMovimiento
										   select new
										   {
											   Id = mnr.Id,
											   Nombre = mnr.Nombre,
											   TipoMovimiento = mnr.TipoMovimiento,
											   Estatus = mnr.Estatus,
											   Reversable = false
										   };
			var except = union.Except(movimientosNoReversables).OrderByDescending(m => m.Id);
			return except.First().Reversable;
		}

		/// <summary>
		/// Devuelve todos los movimientos activos, según suscriptor determinado.
		/// </summary>
		/// <param name="suscriptorId">Identificador único del suscriptor a consultar.</param>
		/// <param name="movimientoTipos">Lista de tipos de movimiento a consultar.</param>
		/// <param name="casoTipos">Lista de tipos de caso a consultar.</param>
		/// <returns>Lista de movimientos activos.</returns>
		public List<MovimientoDTO> ObtenerPorSuscriptor(int suscriptorId, string movimiento, List<string> movimientoTipos, List<string> casoTipos)
		{
			var Movimientos = udt.Sesion.CreateObjectSet<Movimiento>();
			var Casos = udt.Sesion.CreateObjectSet<Caso>();
			var ListaValores = udt.Sesion.CreateObjectSet<ListaValor>();
			var q = from m in Movimientos
					join c in Casos on m.IdCaso equals c.Id
					join e in ListaValores on m.Estatus equals e.Id
					join e2 in ListaValores on c.Estatus equals e2.Id
					where m.IndVigente == true && m.IndEliminado == false && m.FechaValidez <= DateTime.Now
					&& c.IndVigente == true && c.IndEliminado == false && c.FechaValidez <= DateTime.Now
					&& m.IdSuscriptor == (int?)suscriptorId
					&& m.Nombre == movimiento
					&& movimientoTipos.Contains(e.NombreValor)
					&& casoTipos.Contains(e2.NombreValor)
					orderby 1, 2
					select new MovimientoDTO
					{
						Idsuscriptor = (int)m.IdSuscriptor,
						Id = m.Id,
						NombreMovimiento = m.Nombre,
						EstatusMovimiento = m.Estatus,
						NombreEstatusMovimiento = e.NombreValor,
						IdCaso = c.Id,
						EstatusCaso = c.Estatus,
						NombreEstatusCaso = e2.NombreValor
					};
			return q.ToList();
		}

		public IEnumerable<MovimientoDTO> BuscarCasosHijos(int idCaso)
		{
			int tipoMovimiento = int.Parse(ConfigurationManager.AppSettings[@"PasoServicio"]);
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			var tabCasos = udt.Sesion.CreateObjectSet<Caso>();
			var coleccion = from m in tabMovimiento
							join c in tabCasos on m.IdCasoRelacionado equals c.Id
							where m.IdCaso == idCaso &&
								m.TipoMovimiento == tipoMovimiento &&
								m.IndVigente == true && 
								m.IndEliminado == false &&
								m.FechaValidez <= DateTime.Now &&
								c.IndVigente == true && 
								c.IndEliminado == false &&
								c.FechaValidez <= DateTime.Now
							select new MovimientoDTO
							{
								Id = m.Id,
								IdCaso = m.IdCaso,
								IdCasoRelacionado = m.IdCasoRelacionado,
								Movimiento = m.Nombre,
								IndVigente = m.IndVigente,
								FechaValidez = m.FechaValidez,
								IndEliminado = m.IndEliminado
							};
			return coleccion;
		}
        public int Count_Movimientos(int idCaso) 
        {
            var Movimientos = udt.Sesion.CreateObjectSet<Movimiento>();
         
            var q = (from m in Movimientos                    
                    where m.IdCaso == idCaso 
                    & m.Estatus!=154
                    select m).Count();
        return q;
        }
		#region "Propiedades"

		#endregion "Propiedades"
	}
}
