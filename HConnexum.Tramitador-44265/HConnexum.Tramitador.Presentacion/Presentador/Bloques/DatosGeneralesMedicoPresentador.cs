using System;
using System.Text;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using System.Web;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class DatosGeneralesMedicoPresentador : BloquesPresentadorBase
	{
		readonly IDatosGeneralesMedico vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public DatosGeneralesMedicoPresentador(IDatosGeneralesMedico vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de buscar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
			}
			catch(Exception ex)
			{
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>
		public void ValidarDatos()
		{
			StringBuilder errores = new StringBuilder();
			try
			{
			}
			catch(Exception ex)
			{
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			vista.Errores = errores.ToString();
		}
	}
}
