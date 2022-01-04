using System;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase MovimientoPresentadorDetalle.</summary>
    public class ResultadosTabMovimientoPresentadorDetalle : PresentadorDetalleBase<Movimiento>
    {
        ///<summary>Variable vista de la interfaz IMovimientoDetalle.</summary>
        readonly IRespuestaTabMovimiento Vista;

        ///<summary>Variable de la entidad Movimiento.</summary>
        RespuestaMovimientoDTO _Movimiento;

        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ResultadosTabMovimientoPresentadorDetalle(IRespuestaTabMovimiento vista)
        {
            this.Vista = vista;
           this.Vista.NombreTabla = GetType();
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(int idmov)
        {
            try
            {
                RespuestaMovimientoRepositorio repositorio = new RespuestaMovimientoRepositorio(udt);
                _Movimiento = repositorio.ObtenerDTO(idmov);
                PresentadorAVista();
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

        

        ///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
        private void PresentadorAVista()
        {
            try
            {
                
                this.Vista.Oma= string.Format("{0:N0}", this._Movimiento.OMA);
                
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