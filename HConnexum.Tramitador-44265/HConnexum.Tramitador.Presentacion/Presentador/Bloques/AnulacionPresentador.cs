using System;
using System.Configuration;
using System.Linq;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	///<summary>Presentador para el manejo del control Web de usuario 'Anulación de Caso'.</summary>
	public class AnulacionPresentador : BloquesPresentadorBase
	{
		///<summary>Vista asociada al presentador.</summary>
		readonly IAnulacion vista;

		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public AnulacionPresentador(IAnulacion vista)
		{
			this.vista = vista;
		}

		///<summary>Llenado de controles tipo lista de valores (combobox).</summary>
		public void LlenarCombos()
		{
			try
			{
				this.vista.ComboAprobacion = this.ObtenerListaValor(ConfigurationManager.AppSettings[@"ListaDecisionSimple"]);
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
	}
}