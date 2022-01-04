using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Datos;
using HConnexum.Infraestructura;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public partial class ImprimirPresentadorDetalle : BloquesPresentadorBase
	{
		readonly IImprimir vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public ImprimirPresentadorDetalle(IImprimir vista)
		{
			this.vista = vista;
		}

		///<summary>Rutina para el llenado de  los listbox, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombo()
		{
			try
			{
				vista.ComboDecisionSimple = ObtenerListaValor(WebConfigurationManager.AppSettings[@"ListaDecisionSimple"]);
			}
			catch (Exception e)
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
				if (vista.Pobservacionmed.Length < 7)
				{
					errores.Append("*El campo Observaciones requiere mínimo 7 caracteres");
					throw new Exception("*El campo Observaciones requiere mínimo 7 caracteres");
				}
			}
			catch (Exception ex)
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