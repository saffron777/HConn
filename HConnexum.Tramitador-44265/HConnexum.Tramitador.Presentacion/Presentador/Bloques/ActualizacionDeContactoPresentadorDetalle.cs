using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class ActualizacionDeContactoPresentadorDetalle : BloquesPresentadorBase
	{
		readonly IActualizacionDeContacto vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public ActualizacionDeContactoPresentadorDetalle(IActualizacionDeContacto vista)
		{
			this.vista = vista;
		}

		///<summary>Rutina para el llenado de  los listbox, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombo()
		{
			try
			{
				vista.ComboNuevoContacto = ObtenerListaValor(WebConfigurationManager.AppSettings[@"ListaDecisionSimple"]);
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>
		/// <returns>Devuelve mensaje(s) con los datos validados.</returns>
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