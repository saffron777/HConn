using System;
using System.Linq;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class DatosGeneralesCE : UserControlBase
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
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				if(string.IsNullOrEmpty(PNomCompletoTit))
					PNomCompletoTit = string.Format(@"{0} , {1}", ParametrosEntrada["PRIMERNOMBRETIT"], ParametrosEntrada["PRIMERAPELLIDOTIT"]);
				if(string.IsNullOrEmpty(PNomCompletoBenef))
					PNomCompletoBenef = string.Format(@"{0} , {1}", ParametrosEntrada["PRIMERNOMBREBENEF"], ParametrosEntrada["PRIMERAPELLIDOBENEF"]);
				PNumDocTit = string.Format(@"{0}{1}", string.IsNullOrEmpty(ParametrosEntrada["NACIONALIDADTIT"]) ? string.Empty : ParametrosEntrada["NACIONALIDADTIT"] + @"-", ParametrosEntrada["NUMDOCTIT"]);
				PNumDocBenef = string.Format(@"{0}{1}", string.IsNullOrEmpty(ParametrosEntrada["NACIONALIDADBENEF"]) ? string.Empty : ParametrosEntrada["NACIONALIDADBENEF"] + @"-", ParametrosEntrada["NUMDOCBENEF"]);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		#region Propiedades
		public string PNomCompletoTit
		{
			get
			{
				return NomCompletoTit.Text;
			}
			set
			{
				NomCompletoTit.Text = value;
			}
		}

		public string PNomCompletoBenef
		{
			get
			{
				return NomCompletoBenef.Text;
			}
			set
			{
				NomCompletoBenef.Text = value;
			}
		}

		public string PNumDocTit
		{
			get
			{
				return NumDocTit.Text;
			}
			set
			{
				NumDocTit.Text = value;
			}
		}

		public string PNumDocBenef
		{
			get
			{
				return NumDocBenef.Text;
			}
			set
			{
				NumDocBenef.Text = value;
			}
		}
		#endregion
	}
}