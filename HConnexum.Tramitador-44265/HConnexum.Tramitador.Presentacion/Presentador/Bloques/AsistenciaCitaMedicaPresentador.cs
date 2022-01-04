using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class AsistenciaCitaMedicaPresentador : BloquesPresentadorBase
	{
		readonly IAsistenciaCitaMedica vista;

		public AsistenciaCitaMedicaPresentador(IAsistenciaCitaMedica vista)
		{
			this.vista = vista;
		}

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

		public void LlenarCombo()
		{
			vista.ComboAfiliadoContacto = ObtenerListaValor(WebConfigurationManager.AppSettings["ListaContacto"]);
			vista.ComboCambioDeMedico = ObtenerListaValor(WebConfigurationManager.AppSettings["ListaDecisionSimple"]);
			vista.ComboAsistencia = ObtenerListaValor(WebConfigurationManager.AppSettings["ListaDecisionSimple"]);
			vista.ComboAccion = ObtenerListaValor(WebConfigurationManager.AppSettings["ListaAccion"]);
		}
	}
}