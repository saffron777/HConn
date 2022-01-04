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
    public class TiemposTabMovimientoPresentadorDetalle : PresentadorDetalleBase<Movimiento>
    {
        ///<summary>Variable vista de la interfaz IMovimientoDetalle.</summary>
        readonly ITiemposTabMovimiento Vista;

        ///<summary>Variable de la entidad Movimiento.</summary>
        MovimientoDTO _Movimiento;

        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public TiemposTabMovimientoPresentadorDetalle(ITiemposTabMovimiento vista)
        {
            this.Vista = vista;  
           this.Vista.NombreTabla = GetType();
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(int idmov)
        {
            try
            {
                MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
                _Movimiento = repositorio.ObternerPorMovimiento(idmov);
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
                Vista.FechaCreacion = _Movimiento.FechaCreacion.ToString();
                Vista.FechaAtencion = _Movimiento.FechaModificacion.ToString();
                Vista.FechaEjecucion = _Movimiento.FechaEjecucion.ToString();
				TimeSpan segundos = new TimeSpan(0, 0, _Movimiento.TiempoEstimado);
				Vista.TiempoEstimado = segundos.Days.ToString() + " Dias";
                //Llenando texbox de tiempo transcurrido hasta la Atencion
                if (!string.IsNullOrEmpty(_Movimiento.FechaCreacion.ToString()) & !string.IsNullOrEmpty(_Movimiento.FechaAtencion.ToString())) 
                {
                    Vista.Atencion = System.Math.Floor(_Movimiento.FechaModificacion.Value.Subtract(_Movimiento.FechaCreacion).TotalSeconds).ToString();
                    
                }
                //Llenando texbox de tiempo transcurrido hasta la Ejecucion
                if (!string.IsNullOrEmpty(_Movimiento.FechaCreacion.ToString()) & !string.IsNullOrEmpty(_Movimiento.FechaEjecucion.ToString())) 
                {
                    Vista.Ejecucion = System.Math.Floor(_Movimiento.FechaEjecucion.Value.Subtract(_Movimiento.FechaCreacion).TotalSeconds).ToString();
                }
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