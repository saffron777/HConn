using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase MovimientoPresentadorDetalle.</summary>
    public class ObservacionesTabMovimientoPresentadorDetalle : PresentadorDetalleBase<Movimiento>
    {
        ///<summary>Variable vista de la interfaz IMovimientoDetalle.</summary>
        readonly IObservacionesTabMovimiento Vista;

        ///<summary>Variable de la entidad Movimiento.</summary>
        ObservacionesMovimientosDTO _ObservacionesMovimientos;

        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ObservacionesTabMovimientoPresentadorDetalle(IObservacionesTabMovimiento vista)
        {
            this.Vista = vista;
            this.Vista.NombreTabla = GetType();
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int idmov)
        {
            try
            {
                ObservacionesMovimientoRepositorio repositorio = new ObservacionesMovimientoRepositorio(udt);
                IEnumerable<ObservacionesMovimientosDTO> datos = repositorio.ObtenerDTO(orden, pagina, tamañoPagina, parametrosFiltro, idmov);
                Vista.NumeroDeRegistros = repositorio.Conteo;
                Vista.Datos = datos;
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }


    }
     
}