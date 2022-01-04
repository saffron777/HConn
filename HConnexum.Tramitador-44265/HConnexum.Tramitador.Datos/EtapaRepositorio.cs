using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
    ///<summary>Clase: EtapaRepositorio.</summary>	
    public sealed class EtapaRepositorio : RepositorioBase<Etapa>
    {
        #region "ConstructoreS"
        ///<summary>Constructor de la clase EtapaRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public EtapaRepositorio(IUnidadDeTrabajo udt)
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
        ///<returns>Retorna IEnumerableEtapaDTO.</returns>
        public IEnumerable<EtapaDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var coleccion = from tab in tabEtapa
                            orderby "it." + orden
                            select new EtapaDTO
                            {
                                Id = tab.Id,
                                Nombre = tab.Nombre,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };

            coleccion = UtilidadesDTO<EtapaDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<EtapaDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<EtapaDTO>.Conteo;
            return UtilidadesDTO<EtapaDTO>.EncriptarId(coleccion);
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableEtapaDTO.</returns>
        public IEnumerable<EtapaDTO> ObtenerDTOEtapa(int idFlujoServicio)
        {
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var coleccion = from tab in tabEtapa
                            where tab.IdFlujoServicio == idFlujoServicio
                            orderby tab.Nombre
                            select new EtapaDTO
                            {
                                Id = tab.Id,
                                IdFlujoServicio = tab.IdFlujoServicio,
                                Nombre = tab.Nombre,
                                Orden = tab.Orden,
                                SlaPromedio = tab.SlaPromedio,
                                SlaTolerancia = tab.SlaTolerancia,
                                IndObligatorio = tab.IndObligatorio,
                                IndRepeticion = tab.IndRepeticion,
                                IndSeguimiento = tab.IndSeguimiento,
                                IndInicioServ = tab.IndInicioServ,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModicacion = tab.FechaModicacion,
                                IndCierre = tab.IndCierre,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<EtapaDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        public EtapaDTO ObtenerPorIdEtapa(int idEtapa)
        {
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var entidad = (from tab in tabEtapa
                           join tabF in tabFlujosServicio on tab.IdFlujoServicio equals tabF.Id
                           where tab.Id == idEtapa
                           select new EtapaDTO
                           {
                               IdSuscriptor = tabF.IdSuscriptor,
                               IdFlujoServicio = tab.IdFlujoServicio,
                               IdServicioSuscriptor = tabF.IdServicioSuscriptor,
                               VersionFlujoServicio = tabF.Version,
                               IndVigenteFlujoServicio = tabF.IndVigente,
                               Nombre = tab.Nombre,

                               Orden = tab.Orden,
                               SlaPromedio = tab.SlaPromedio,
                               SlaTolerancia = tab.SlaTolerancia,
                               IndObligatorio = tab.IndObligatorio,
                               IndRepeticion = tab.IndRepeticion,
                               IndSeguimiento = tab.IndSeguimiento,
                               IndInicioServ = tab.IndInicioServ,
                               IndCierre = tab.IndCierre,
                               CreadoPor = tab.CreadoPor,
                               FechaCreacion = tab.FechaCreacion,
                               ModificadoPor = tab.ModificadoPor,
                               FechaModicacion = tab.FechaModicacion,
                               IndVigente = tab.IndVigente,
                               FechaValidez = tab.FechaValidez,
                               IndEliminado = tab.IndEliminado
                           }).SingleOrDefault();
            return entidad;
        }

        public int ObtenerSLA(int IdFlujosServicio)
        {
            var tabEtapas = udt.Sesion.CreateObjectSet<Etapa>();
            var coleecion = from tab in tabEtapas
                            where tab.IdFlujoServicio == IdFlujosServicio & tab.IndEliminado == false & tab.IndVigente == true
                            select new
                            {
                                SlaPromedio = tab.SlaTolerancia
                            };


            if (coleecion.Count() <= 0)
                return 0;
            else
                return coleecion.Sum(a => a.SlaPromedio);
        }

        #endregion DTO
    }
}