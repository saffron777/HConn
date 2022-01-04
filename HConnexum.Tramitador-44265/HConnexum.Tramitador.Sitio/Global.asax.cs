using System;
using System.Configuration;
using System.Web;

namespace HConnexum.Tramitador.Sitio
{
	public class Global : System.Web.HttpApplication
	{
		void Application_Start(object sender, EventArgs e)
		{
			// Código que se ejecuta al iniciarse la aplicación
		}

		void Application_End(object sender, EventArgs e)
		{
			//  Código que se ejecuta cuando se cierra la aplicación
		}

		void Application_Error(object sender, EventArgs e)
		{
			string additionalmess = string.Empty;
			if(HttpContext.Current != null)
			{
				Exception ex = HttpContext.Current.Server.GetLastError();
				if(((HttpApplication)sender).Context != null)
					additionalmess = ((HttpApplication)sender).Context.Request.Url.ToString();
				try
				{
					HConnexum.Infraestructura.Errores.ManejarError(ex, additionalmess);
				}
				catch(Exception exc)
				{
					HttpContext.Current.Trace.Warn("Application_Error", "Uncatchable error", exc);
				}
			}
		}

		void Session_Start(object sender, EventArgs e)
		{
			string tema = ConfigurationManager.AppSettings["AplicacionTema"];
			if(tema != null)
				Session[@"UsuarioTema"] = tema;
			string skin = ConfigurationManager.AppSettings["Telerik.Skin"];
			if(skin != null)
				Session[@"SkinGlobal"] = skin;
		}

		void Session_End(object sender, EventArgs e)
		{
			// Código que se ejecuta cuando finaliza una sesión.
			// Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
			// se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
			// o SQLServer, el evento no se genera.
		}
	}
}
