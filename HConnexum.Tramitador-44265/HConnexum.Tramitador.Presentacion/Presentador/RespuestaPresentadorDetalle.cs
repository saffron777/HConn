using System;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase RespuestaPresentadorDetalle.</summary>
    public class RespuestaPresentadorDetalle : PresentadorDetalleBase<RespuestaMovimiento>
    {
        ///<summary>Variable vista de la interfaz IRespuestaDetalle.</summary>
        readonly IRespuestaDetalle vista;
		
		///<summary>Variable de la entidad Respuesta.</summary>
        RespuestaMovimientoDTO _Respuesta;
        
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
		///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public RespuestaPresentadorDetalle(IRespuestaDetalle vista)
        {
            this.vista = vista;       
        }
		
		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista()
        {
		 	try
            {

                RespuestaMovimientoRepositorio repositorio = new RespuestaMovimientoRepositorio(udt);
	            _Respuesta = repositorio.ObtenerDTO(27);
				PresentadorAVista();
			}
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
		
				
		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
            {
			vista.Nombre= _Respuesta.OMA;

				
			}
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
		}
		
		
    }
}