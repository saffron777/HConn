using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
    ///<summary>Clase: PasosRepuestaRepositorio.</summary>	
    public sealed class PasosRepuestaRepositorio : RepositorioBase<PasosRepuesta>
    {
        #region "ConstructoreS"
        ///<summary>Constructor de la clase PasosRepuestaRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public PasosRepuestaRepositorio(IUnidadDeTrabajo udt)
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
        ///<returns>Retorna IEnumerablePasosRepuestaDTO.</returns>
        public IEnumerable<PasosRepuestaDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            var tabPasosRepuesta = udt.Sesion.CreateObjectSet<PasosRepuesta>();
            var coleccion = (from tab in tabPasosRepuesta
                            orderby "it." + orden
                            select new PasosRepuestaDTO
                            {
                                Id = tab.Id,
                                NombreRespuesta = tab.ValorRespuesta,
                                IdPaso = tab.IdPaso,
                                Orden = tab.Orden,
                                IndCierre = tab.IndCierre,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            }).Distinct();
            coleccion = UtilidadesDTO<PasosRepuestaDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<PasosRepuestaDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<PasosRepuestaDTO>.Conteo;
            return UtilidadesDTO<PasosRepuestaDTO>.EncriptarId(coleccion);
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasosRepuestaDTO.</returns>
        public IEnumerable<PasosRepuestaDTO> ObtenerDTO()
        {
            var tabPasosRepuesta = udt.Sesion.CreateObjectSet<PasosRepuesta>();
            var coleccion = from tab in tabPasosRepuesta
                            orderby tab.IdPaso
                            select new PasosRepuestaDTO
                            {
                                Id = tab.Id,
                                ValorRespuesta = tab.ValorRespuesta,
                                IdPaso = tab.IdPaso,
                                Orden = tab.Orden,
                                IndCierre = tab.IndCierre,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<PasosRepuestaDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }


        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasosRepuestaDTO.</returns>
        public IEnumerable<PasosRepuestaDTO> ObtenerDTOPasoRespuesta(int idPaso)
        {
            var tabPasosRepuesta = udt.Sesion.CreateObjectSet<PasosRepuesta>();
            
            var coleccion = (from tabPR in tabPasosRepuesta
                              where tabPR.IdPaso == idPaso
                             select new PasosRepuestaDTO
                             {
                                 Id = tabPR.Id
         ,
                                 ValorRespuesta = tabPR.ValorRespuesta
         ,
                                 CreadoPor = tabPR.CreadoPor
         ,
                                 FechaCreacion = tabPR.FechaCreacion
         ,
                                 ModificadoPor = tabPR.ModificadoPor
         ,
                                 FechaModificacion = tabPR.FechaModificacion
         ,
                                 IndVigente = tabPR.IndVigente
         ,
                                 FechaValidez = tabPR.FechaValidez
         ,
                                 IndEliminado = tabPR.IndEliminado
                             }).Distinct();
            coleccion = UtilidadesDTO<PasosRepuestaDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        #endregion DTO
    }
}
