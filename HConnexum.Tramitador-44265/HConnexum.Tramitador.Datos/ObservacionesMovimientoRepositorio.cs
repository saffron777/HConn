using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Datos
{
    public sealed class ObservacionesMovimientoRepositorio:RepositorioBase<ObservacionesMovimiento>
    {
        #region "ConstructoreS"
		///<summary>Constructor de la clase MovimientoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
        public ObservacionesMovimientoRepositorio(IUnidadDeTrabajo udt)
            : base(udt)
        {
			
        }
		#endregion "Constructores"
        #region DTO

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableMovimientoDTO.</returns>
        public IEnumerable<ObservacionesMovimientosDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro,int idmov)
        {
            var tabObservaciones = udt.Sesion.CreateObjectSet<ObservacionesMovimiento>();
            var tabDatosBase = udt.Sesion.CreateObjectSet<DatosBase>();
            var tabUsuarioSuscriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
            var coleccion = from tab in tabObservaciones
                            join tabUser in tabUsuarioSuscriptor on tab.CreadoPor equals tabUser.Id
                            join tdb in tabDatosBase on tabUser.IdUsuario equals tdb.IdUsuario
                            where tab.IdMovimiento == idmov
                            select new ObservacionesMovimientosDTO
                            {
                                Id=tab.Id,
                                usuario = tdb.Nombre1 + " " + tdb.Apellido1,
                                Observacion = tab.TxtObservacion,
                                Fecha = tab.FechaCreacion,
                                IndEliminado = tab.IndEliminado,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez
                            };
            coleccion = UtilidadesDTO<ObservacionesMovimientosDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<ObservacionesMovimientosDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<ObservacionesMovimientosDTO>.Conteo;
            return UtilidadesDTO<ObservacionesMovimientosDTO>.EncriptarId(coleccion);
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableMovimientoDTO.</returns>
        public IEnumerable<ObservacionesMovimientosDTO> ObtenerDTO(int idmov)
        {
            var tabObservaciones = udt.Sesion.CreateObjectSet<ObservacionesMovimiento>();
            var coleccion = from tab in tabObservaciones
                            where tab.IdMovimiento == idmov
                            select new ObservacionesMovimientosDTO
                            {
                                Observacion = tab.TxtObservacion,
                                IndEliminado = tab.IndEliminado,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez
                            };
            coleccion = UtilidadesDTO<ObservacionesMovimientosDTO>.FiltrarColeccionAuditoria(coleccion, true);
            return coleccion;
        }
        #endregion DTO

    }
}
