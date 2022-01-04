using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
    ///<summary>Clase: CasoAgrupacionRepositorio.</summary>	
    public sealed class CasoAgrupacionRepositorio : RepositorioBase<HConnexum.Tramitador.Negocio.CasoAgrupacion>
    {
        #region "ConstructoreS"
        ///<summary>Constructor de la clase CasoAgrupacionRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public CasoAgrupacionRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
        }

        #endregion "Constructores"
 		
        #region DTO

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
        ///<param name="pagina">Nro pagina en el RadGrid.</param>
        ///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
        ///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
        ///<returns>Retorna IEnumerableCasoAgrupacionDTO.</returns>
        public IEnumerable<CasoAgrupacionDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            var tabCasoAgrupacion = udt.Sesion.CreateObjectSet<CasoAgrupacion>();
            var coleccion = from tab in tabCasoAgrupacion
                            orderby "it." + orden
                            select new CasoAgrupacionDTO
                            {
                                Id = tab.Id,
                                IdCaso = tab.IdCaso,
                                IdAgrupacion = tab.IdAgrupacion,
                                IdServicio = tab.IdServicio,
                                IdSolicitud = tab.IdSolicitud,
                                IdSuscriptor = tab.IdSuscriptor,
                                FechaCreacion = tab.FechaCreacion,
                                CreadoPor = tab.CreadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                ModificadoPor = tab.ModificadoPor,
                                IndVigente = tab.IndVigente,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<CasoAgrupacionDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<CasoAgrupacionDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<CasoAgrupacionDTO>.Conteo;
            return coleccion; 
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableCasoAgrupacionDTO.</returns>
        public IEnumerable<CasoAgrupacionDTO> ObtenerDTO()
        {
            var tabCasoAgrupacion = udt.Sesion.CreateObjectSet<CasoAgrupacion>();
            var coleccion = from tab in tabCasoAgrupacion
                            orderby tab.Id
                            select new CasoAgrupacionDTO
                            {
                                Id = tab.Id,
                                IdCaso = tab.IdCaso,
                                IdAgrupacion = tab.IdAgrupacion,
                                IdServicio = tab.IdServicio,
                                IdSolicitud = tab.IdSolicitud,
                                IdSuscriptor = tab.IdSuscriptor,
                                FechaCreacion = tab.FechaCreacion,
                                CreadoPor = tab.CreadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                ModificadoPor = tab.ModificadoPor,
                                IndVigente = tab.IndVigente,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<CasoAgrupacionDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion; 
        }


        public CasoAgrupacion ObtenerCasoAgupacionIdGrupoIdCasoDTO(int IdCaso, int IdAgrupacion)
        {
            var tabCasoAgrupacion = udt.Sesion.CreateObjectSet<CasoAgrupacion>();
            var coleccion = (from tab in tabCasoAgrupacion
                             where (tab.IdCaso == IdCaso && tab.IdAgrupacion == IdAgrupacion)
                             select tab).FirstOrDefault();
            return coleccion;
        }

        public IEnumerable<CasoAgrupacionDTO> ObtenerCasosAgrupacionesNoAsociadosDTO(int idCaso)
        {
            var tabCasoAgrupacion = udt.Sesion.CreateObjectSet<CasoAgrupacion>();
            var tabCaso = udt.Sesion.CreateObjectSet<Caso>();
            var tabAgrupacion = udt.Sesion.CreateObjectSet<Agrupacion>();
            var coleccion = from tab in tabAgrupacion
                            where !(from tabCA in tabCasoAgrupacion 
                                    where tabCA.IdCaso == idCaso select tabCA.IdAgrupacion).Contains(tab.Id)  &&
                                    tab.IndEliminado == false &&
                                   tab.IndVigente == true &&
                                   tab.FechaValidez <= DateTime.Now
                            select new CasoAgrupacionDTO
                            {
                                Id = tab.Id,
                                NombreAgrupacion = tab.Nombre
                            };
            coleccion = UtilidadesDTO<CasoAgrupacionDTO>.FiltrarColeccionEliminacion(coleccion, false);
            return coleccion;
        }

        public IEnumerable<CasoAgrupacionDTO> ObtenerCasosAgrupacionesAsociadosDTO(int idCaso)
        {
            var tabCasoAgrupacion = udt.Sesion.CreateObjectSet<CasoAgrupacion>();
            var tabCaso = udt.Sesion.CreateObjectSet<Caso>();
            var tabAgrupacion = udt.Sesion.CreateObjectSet<Agrupacion>();
            var coleccion = from tab in tabAgrupacion
                            where (from tabCA in tabCasoAgrupacion
                                    where tabCA.IdCaso == idCaso
                                    select tabCA.IdAgrupacion).Contains(tab.Id)  &&
                                    tab.IndEliminado == false &&
                                   tab.IndVigente == true &&
                                   tab.FechaValidez <= DateTime.Now
                            select new CasoAgrupacionDTO
                            {
                                Id = tab.Id,
                                NombreAgrupacion = tab.Nombre
                            };
            coleccion = UtilidadesDTO<CasoAgrupacionDTO>.FiltrarColeccionEliminacion(coleccion, false);
            return coleccion;
        }
        #endregion DTO
    }
}