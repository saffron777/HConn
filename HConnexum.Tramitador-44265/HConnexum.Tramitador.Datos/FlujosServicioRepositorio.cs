using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: FlujosServicioRepositorio.</summary>	
	public sealed class FlujosServicioRepositorio : RepositorioBase<FlujosServicio>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase FlujosServicioRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public FlujosServicioRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}
		#endregion "Constructores"

		#region DTO
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public IEnumerable<FlujosServicioDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from tab in tabFlujosServicio
							join tab1 in tabServicioSuscriptor on tab.IdServicioSuscriptor equals tab1.Id
							join tba2 in tabSuscriptor on tab.IdSuscriptor equals tba2.Id
							orderby "it." + orden
							select new FlujosServicioDTO
							{
								Id = tab.Id,
								IdServicioSuscriptor = tab.IdServicioSuscriptor,
								NombreSuscriptor = tba2.Nombre,
								NombreServicioSuscriptor = tab1.Nombre,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado,
								Version = tab.Version
							};

			coleccion = UtilidadesDTO<FlujosServicioDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<FlujosServicioDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<FlujosServicioDTO>.Conteo;
			return UtilidadesDTO<FlujosServicioDTO>.EncriptarId(coleccion);
		}

		/// <summary>
		/// Llena combos Con el Id de FS y con el nombre del Servicio Del suscriptor.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<FlujosServicioDTO> ObtenerDTO()
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from tab in tabFlujosServicio
							join tab1 in tabServicioSuscriptor on tab.IdServicioSuscriptor equals tab1.Id
							join tba2 in tabSuscriptor on tab.IdSuscriptor equals tba2.Id
							select new FlujosServicioDTO
							{
								Id = tab.Id,
								IdServicioSuscriptor = tab.IdServicioSuscriptor,
								NombreSuscriptor = tba2.Nombre,
								NombreServicioSuscriptor = tab1.Nombre,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<FlujosServicioDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public IEnumerable<FlujosServicioDTO> ObtenerDTO(int idSuscriptor, int idFlujoServicio)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = from tab in tabFlujosServicio
							where tab.IdSuscriptor == idSuscriptor &&
								  tab.IdServicioSuscriptor != (from tabo in tabFlujosServicio
															   where tabo.Id == idFlujoServicio
															   select tabo.IdServicioSuscriptor).FirstOrDefault() &&
								  tab.Id == (from tabi in tabFlujosServicio
											 where tabi.IdServicioSuscriptor == tab.IdServicioSuscriptor &&
											 tabi.IndEliminado == false &&
											 tabi.IndVigente == true &&
											 tabi.FechaValidez <= DateTime.Now
											 orderby tab.FechaCreacion descending
											 select tabi.Id).FirstOrDefault()
							select new FlujosServicioDTO
							{
								Id = tab.Id,
								IdServicioSuscriptor = tab.IdServicioSuscriptor,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<FlujosServicioDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public IEnumerable<FlujosServicioDTO> ObtenerDTOporSuscriptor(int idSuscriptor)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = from tab in tabFlujosServicio
							where tab.IdSuscriptor == idSuscriptor &&
								  tab.Id == (from tabi in tabFlujosServicio
											 where tabi.IdServicioSuscriptor == tab.IdServicioSuscriptor &&
											 tabi.IndEliminado == false &&
											 tabi.IndVigente == true &&
											 tabi.FechaValidez <= DateTime.Now
											 orderby tab.FechaCreacion descending
											 select tabi.Id).FirstOrDefault()
							select new FlujosServicioDTO
							{
								Id = tab.Id,
								IdServicioSuscriptor = tab.IdServicioSuscriptor,
								IdSuscriptor = tab.IdSuscriptor,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<FlujosServicioDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
        public IEnumerable<FlujosServicioDTO> ObtenerDTOServicioSusporIdSuscriptor(int idSuscriptor)
        {
            var tFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var tServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
            var coleccion = from fs in tFlujosServicio 
                            join ss in tServicioSuscriptor on fs.IdServicioSuscriptor equals ss.Id
                            where fs.IdSuscriptor == idSuscriptor 
                                 && ss.Nombre!=""
                                 && ss.IndEliminado==false
                                 && ss.IndVigente==true
                                 && ss.FechaValidez<=DateTime.Now
                                 && fs.IndEliminado == false
                                 && fs.IndVigente == true
                                 && fs.FechaValidez <= DateTime.Now
                            select new FlujosServicioDTO
                            {
                                NombreServicioSuscriptor=ss.Nombre,
                                Id=ss.Id                              
                            };
           
            return coleccion;
        }
		public IEnumerable<FlujosServicioDTO> ObtenerPorSuscriptor(int idSuscriptor)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabFlujosServicio
							 where tab.IdSuscriptor == idSuscriptor
							 orderby tab.FechaValidez descending
							 select new FlujosServicioDTO
							 {
								 Id = tab.Id,
								 IdServicioSuscriptor = tab.IdServicioSuscriptor,
								 FechaValidez = tab.FechaValidez,
								 IndVigente = tab.IndVigente,
								 IndEliminado = tab.IndEliminado
							 }).GroupBy(x => x.IdServicioSuscriptor).Select(x => x.FirstOrDefault());
			coleccion = UtilidadesDTO<FlujosServicioDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}




		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public IEnumerable<FlujosServicioDTO> ObtenerDTOporSuscriptorSinAuditoria(int idSuscriptor)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = from tab in tabFlujosServicio
							where tab.IdSuscriptor == idSuscriptor &&
								  tab.Id == (from tabi in tabFlujosServicio
											 where tabi.IdServicioSuscriptor == tab.IdServicioSuscriptor
											 orderby tab.FechaCreacion descending
											 select tabi.Id).FirstOrDefault()
							select new FlujosServicioDTO
							{
								Id = tab.Id,
								IdServicioSuscriptor = tab.IdServicioSuscriptor,
								IdSuscriptor = tab.IdSuscriptor,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public IEnumerable<FlujosServicioDTO> ObtenerFlujosServicioDTO(int idFlujoServicio)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = from tab in tabFlujosServicio
							where tab.Id == idFlujoServicio
							select new FlujosServicioDTO
							{
								Id = tab.Id,
								IdServicioSuscriptor = tab.IdServicioSuscriptor,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public FlujosServicioDTO ObtenerDTOServicio(int? idPaso)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
			var coleccion = (from tab in tabFlujosServicio
							 join tabE in tabEtapa
							 on tab.Id equals tabE.IdFlujoServicio
							 join tabP in tabPaso
							 on tabE.Id equals tabP.IdEtapa
							 where tabP.Id == idPaso
							 select new FlujosServicioDTO
							 {
								 Id = tab.Id,
								 IdSuscriptor = tab.IdSuscriptor,
								 IdServicioSuscriptor = tab.IdServicioSuscriptor,
								 Version = tab.Version,
								 IdEtapa = tabP.IdEtapa,
								 IdPaso = tabP.Id,
								 NombrePaso = tabP.Nombre,
								 CreadoPor = tab.CreadoPor,
								 FechaCreacion = tab.FechaCreacion,
								 ModificadoPor = tab.ModificadoPor,
								 FechaModicacion = tab.FechaModicacion,
								 FechaValidez = tab.FechaValidez,
								 IndVigente = tab.IndVigente,
								 IndEliminado = tab.IndEliminado,
								 XLMEstructura = tab.XLMEstructura
							 }).SingleOrDefault();

			return coleccion;
		}

		public FlujosServicioDTO ObtenerServicioySuscriptorPorFlujosservicio(int idflujoservicio)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabFlujosServicio
							 where tab.Id == idflujoservicio
							 select new FlujosServicioDTO
							 {
								 IdServicioSuscriptor = tab.IdServicioSuscriptor,
								 IdSuscriptor = tab.IdSuscriptor
							 }).SingleOrDefault();
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summar y>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public FlujosServicioDTO ObtenerDTOServicioFlujoServicio(int idFlujoServicio)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabFlujosServicio
							 where tab.Id == idFlujoServicio
							 select new FlujosServicioDTO
							 {
								 Id = tab.Id,
								 IdSuscriptor = tab.IdSuscriptor,
								 IdServicioSuscriptor = tab.IdServicioSuscriptor,
								 Version = tab.Version,
								 CreadoPor = tab.CreadoPor,
								 FechaCreacion = tab.FechaCreacion,
								 ModificadoPor = tab.ModificadoPor,
								 FechaModicacion = tab.FechaModicacion,
								 FechaValidez = tab.FechaValidez,
								 IndVigente = tab.IndVigente,
								 IndEliminado = tab.IndEliminado,
								 XLMEstructura = tab.XLMEstructura
							 }).SingleOrDefault();
			return coleccion;
		}

		public FlujosServicioDTO ObtenerPorIdEtapa(int IdEtapa)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
			var coleccion = (from tab in tabFlujosServicio
							 join tabE in tabEtapa
							 on tab.Id equals tabE.IdFlujoServicio
							 where tabE.Id == IdEtapa
							 select new FlujosServicioDTO
							 {
								 Version = tab.Version,
								 IndVigente = tab.IndVigente
							 }).SingleOrDefault();
			return coleccion;
		}

		public FlujosServicioDTO ObtenerIdServicioSuscriptor(int Id)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
			var coleccion = (from tab in tabFlujosServicio
							 join tabE in tabEtapa on tab.Id equals tabE.IdFlujoServicio
							 where tabE.Id == Id
							 select new FlujosServicioDTO
							 {
								 Id = tab.Id,
								 IdFlujoServicio = tabE.IdFlujoServicio,
								 IdSuscriptor = tab.IdSuscriptor,
								 IdServicioSuscriptor = tab.IdServicioSuscriptor,
								 Version = tab.Version,
								 IndVigente = tab.IndVigente
							 }).SingleOrDefault();
			return coleccion;
		}

		public FlujosServicioDTO ObtenerIdServicioSuscriptor2(int Id)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabFlujosServicio
							 where tab.Id == Id
							 select new FlujosServicioDTO
							 {
								 Id = tab.Id,
								 IdSuscriptor = tab.IdSuscriptor,
								 IdServicioSuscriptor = tab.IdServicioSuscriptor,
								 Version = tab.Version,
								 IndVigente = tab.IndVigente
							 }).SingleOrDefault();
			return coleccion;
		}

		public FlujosServicioDTO ObtenerParaEditar(int id)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabFlujosServicio
							 where tab.Id == id
							 select new FlujosServicioDTO
							 {
								 Id = tab.Id,
								 IdSuscriptor = tab.IdSuscriptor,
								 IdOrigen = tab.IdOrigen,
								 IdPasoInicial = tab.IdPasoInicial,
								 SlaTolerancia = tab.SlaTolerancia,
								 SlaPromedio = tab.SlaPromedio,
								 Prioridad = tab.Prioridad,
								 IndCms = tab.IndCms,
								 IndBloqueGenericoSolicitud = tab.IndBloqueGenericoSolicitud,
								 MetodoPreSolicitud = tab.MetodoPreSolicitud,
								 MetodoPostSolicitud = tab.MetodoPostSolicitud,
								 IdServicioSuscriptor = tab.IdServicioSuscriptor,
								 Version = tab.Version,
								 CreadoPor = tab.CreadoPor,
								 FechaCreacion = tab.FechaCreacion,
								 ModificadoPor = tab.ModificadoPor,
								 FechaModicacion = tab.FechaModicacion,
								 FechaValidez = tab.FechaValidez,
								 IndVigente = tab.IndVigente,
								 IndEliminado = tab.IndEliminado,
								 IndPublico = tab.IndPublico,
								 XLMEstructura = tab.XLMEstructura,
                                 IndChat = tab.IndChat,
                                 NomPrograma = tab.NomPrograma
							 }).SingleOrDefault();
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
		public FlujosServicioDTO ObtenerDtoFlujosServicioPorId(int id)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var tabSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = (from fs in tabFlujosServicio
							 join ss in tabServicioSuscriptor on fs.IdServicioSuscriptor equals ss.Id
							 join s in tabSuscriptor on fs.IdSuscriptor equals s.Id
							 where fs.Id == id
							 select new FlujosServicioDTO
							 {
								 Id = fs.Id,
								 IdServicioSuscriptor = fs.IdServicioSuscriptor,
								 NombreSuscriptor = s.Nombre,
								 NombreServicioSuscriptor = ss.Nombre,
								 SuscriptorFaxNumero = s.Fax,
								 Version = fs.Version,
								 IndPublico = fs.IndPublico,
								 CreadoPor = fs.CreadoPor,
								 ModificadoPor = fs.ModificadoPor,
								 FechaCreacion = fs.FechaCreacion,
								 FechaModicacion = fs.FechaModicacion,
								 FechaValidez = fs.FechaValidez,
								 IndVigente = fs.IndVigente,
								 IndEliminado = fs.IndEliminado
							 }).SingleOrDefault();
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerable FlujosServicioDTO.</returns>
		public IEnumerable<FlujosServicioDTO> ObtenerFlujosServicios()
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
			var coleccion = from fs in tabFlujosServicio
							join ss in tabServicioSuscriptor on fs.IdServicioSuscriptor equals ss.Id
							where fs.IndVigente == true &&
								  fs.IndEliminado == false &&
								  fs.FechaValidez <= DateTime.Now &&
								  ss.IndVigente == true &&
								  ss.IndEliminado == false &&
								  ss.FechaValidez <= DateTime.Now
							select new FlujosServicioDTO
							 {
								 Id = fs.Id,
								 NombreServicioSuscriptor = ss.Nombre,
								 Version = fs.Version
							 };
			return coleccion;
		}

		public int BuscarFlujosServicio(FlujosServicio flujosServicio)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabFlujosServicio
							 where tab.IdServicioSuscriptor == flujosServicio.IdServicioSuscriptor &&
								   tab.Version == flujosServicio.Version
							 select tab).SingleOrDefault();
			if(coleccion != null)
				return coleccion.Id;
			return 0;
		}

		public int BuscarVersionFlujosServicio(FlujosServicio flujosServicio)
		{
			var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
			var coleccion = (from tab in tabFlujosServicio
							 where tab.IdServicioSuscriptor == flujosServicio.IdServicioSuscriptor
							 orderby tab.Version descending
							 select tab).First();
			if(coleccion != null)
				return coleccion.Version;
			return 0;
		}

        public FlujosServicioDTO ObtenerNombrePrograma(int idCaso)
        {
            var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var tabCaso = udt.Sesion.CreateObjectSet<Caso>();
            var coleccion = (from tab in tabFlujosServicio
                            join tab1 in tabCaso on tab.Id equals tab1.IdFlujoServicio
                            where tab1.Id == idCaso
                            select new FlujosServicioDTO
                            {
                                Id = tab.Id,
                                NomPrograma = tab.NomPrograma
                            }).FirstOrDefault();
            return coleccion; 
        }

		#endregion DTO
	}
}
