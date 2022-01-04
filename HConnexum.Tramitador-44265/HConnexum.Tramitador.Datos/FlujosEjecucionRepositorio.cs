using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
    ///<summary>Clase: FlujosEjecucionRepositorio.</summary>	
    public sealed class FlujosEjecucionRepositorio : RepositorioBase<FlujosEjecucion>
    {
        #region "ConstructoreS"
        ///<summary>Constructor de la clase FlujosEjecucionRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public FlujosEjecucionRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
        }
        #endregion "Constructores"
		
        #region DTO
        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
        ///<param name="pagina">Nro pagina en el RadGrid.</param>
        ///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
        ///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
        ///<returns>Retorna IEnumerableFlujosEjecucionDTO.</returns>
        public IEnumerable<FlujosEjecucionDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            var tabFlujosEjecucion = udt.Sesion.CreateObjectSet<FlujosEjecucion>();
            var coleccion = from tab in tabFlujosEjecucion
                            orderby "it." + orden
                            select new FlujosEjecucionDTO
                            {
                                Id = tab.Id,
                                IdPasoDestino = tab.IdPasoDestino,
                                Condicion = tab.Condicion,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModicacion = tab.FechaModicacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<FlujosEjecucionDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<FlujosEjecucionDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<FlujosEjecucionDTO>.Conteo;
            return coleccion; 
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableFlujosEjecucionDTO.</returns>
        public IEnumerable<FlujosEjecucionDTO> ObtenerDTO()
        {
            var tabFlujosEjecucion = udt.Sesion.CreateObjectSet<FlujosEjecucion>();
            var coleccion = from tab in tabFlujosEjecucion  //orderby tab.Nombre
                            select new FlujosEjecucionDTO
                            {
                                Id = tab.Id,
                                IdPasoDestino = tab.IdPasoDestino,
                                Condicion = tab.Condicion,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModicacion = tab.FechaModicacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<FlujosEjecucionDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion; 
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
        ///<param name="pagina">Nro pagina en el RadGrid.</param>
        ///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
        ///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
        ///<returns>Retorna IEnumerableFlujosEjecucionDTO.</returns>
        public IEnumerable<FlujosEjecucionDTO> ObtenerDTOListarAccionesDelPaso(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idSubServicio)
        {
            var tabFlujosEjecucion = udt.Sesion.CreateObjectSet<FlujosEjecucion>();
            var tabPasos = udt.Sesion.CreateObjectSet<Paso>();
            var tabPasosRespuesta = udt.Sesion.CreateObjectSet<PasosRepuesta>();
            var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();                       
            var coleccion = from tab in tabFlujosEjecucion
                            join tabP2 in tabPasos on tab.IdPasoDestino equals tabP2.Id
                            join tabTP in tabTipoPaso on tabP2.IdTipoPaso equals tabTP.Id
                            join tabPR in tabPasosRespuesta on tab.IdPasoRespuesta equals tabPR.Id
                            join tabP in tabPasos on tabPR.IdPaso equals tabP.Id
                            join tabE in tabEtapa on tabP.IdEtapa equals tabE.Id
                            where tabE.IdFlujoServicio == idSubServicio
                            select new FlujosEjecucionDTO
                            {
                                Id = tab.Id,
                                IdPasoDestino = tab.IdPasoDestino,
                                PasoOrigen = tabP.Nombre,
                                PasoDestino = tabP2.Nombre,
                                IdPasoRespuesta = tab.IdPasoRespuesta,
                                Respuesta = tabPR.ValorRespuesta,
                                Condicion = tab.Condicion,
                                TipoProximoPaso = tabTP.Descripcion,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModicacion = tab.FechaModicacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<FlujosEjecucionDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<FlujosEjecucionDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<FlujosEjecucionDTO>.Conteo;
            return UtilidadesDTO<FlujosEjecucionDTO>.EncriptarId(coleccion);
        }
        #endregion DTO
    }
}