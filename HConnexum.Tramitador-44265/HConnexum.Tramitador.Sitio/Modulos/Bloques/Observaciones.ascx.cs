using System;
using System.Linq;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class Observaciones : UserControlBase
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}