using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
    ///<summary>Clase: ParametrosAgendaRepositorio.</summary>	
    public sealed class ParametrosAgendaRepositorio : RepositorioBase<ParametrosAgenda>
    {
        #region "ConstructoreS"
        ///<summary>Constructor de la clase ParametrosAgendaRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public ParametrosAgendaRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
        }

        #endregion "Constructores"
		
        #region DTO

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
        ///<param name="pagina">Nro pagina en el RadGrid.</param>
        ///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
        ///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
        ///<returns>Retorna IEnumerableParametrosAgendaDTO.</returns>
        public IEnumerable<ParametrosAgendaDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idPaso)
        {
            var tabParametrosAgenda = udt.Sesion.CreateObjectSet<ParametrosAgenda>();
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var coleccion = from tab in tabParametrosAgenda
                            join tabP in tabPaso on tab.IdPaso equals tabP.Id
                            where tab.IdPaso == idPaso
                            select new ParametrosAgendaDTO
                            {
                                Id = tab.Id,
                                IdPaso = tab.IdPaso,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                IndVigente = tab.IndVigente,
                                IndEliminado = tab.IndEliminado,
                                Cantidad = tab.Cantidad,
                                IndInmediato = tab.IndInmediato,
                                Nombre = tabP.Nombre
                            };

            coleccion = UtilidadesDTO<ParametrosAgendaDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<ParametrosAgendaDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<ParametrosAgendaDTO>.Conteo;
            return UtilidadesDTO<ParametrosAgendaDTO>.EncriptarId(coleccion);
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableParametrosAgendaDTO.</returns>
        public IEnumerable<ParametrosAgendaDTO> ObtenerDTO()
        {
            var tabParametrosAgenda = udt.Sesion.CreateObjectSet<ParametrosAgenda>();
            var coleccion = from tab in tabParametrosAgenda orderby tab.Id
                            select new ParametrosAgendaDTO
                            {
                                Id = tab.Id,
                                IdPaso = tab.IdPaso,
                                Cantidad = tab.Cantidad,
                                IndInmediato = tab.IndInmediato,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                IndVigente = tab.IndVigente,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<ParametrosAgendaDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion; 
        }
        #endregion DTO
    }
}