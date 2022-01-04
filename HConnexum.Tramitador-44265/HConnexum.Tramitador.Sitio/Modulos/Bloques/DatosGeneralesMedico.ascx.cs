using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class DatosGeneralesMedico : UserControlBase, IDatosGeneralesMedico
	{
		DatosGeneralesMedicoPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new DatosGeneralesMedicoPresentador(this);
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
				if(!Page.IsPostBack)
					presentador.MostrarVista();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public string ValidarDatos()
		{
			try
			{
				presentador.ValidarDatos();
				if(Errores.Length > 0)
					return Errores;
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
			return string.Empty;
		}

		#region Propiedades
		public string Tipdocmed
		{
			get
			{
				return tipdocmed.Text;
			}
			set
			{
				tipdocmed.Text = value;
			}
		}

		public string Numdocmed
		{
			get
			{
				return numdocmed.Text;
			}
			set
			{
				numdocmed.Text = value;
			}
		}

		public string Nommed
		{
			get
			{
				return nommed.Text;
			}
			set
			{
				nommed.Text = value;
			}
		}

		public string Tlfmed
		{
			get
			{
				return tlfmed.NumeroTlfCompletoGuion;
			}
			set
			{
				tlfmed.NumeroTlfCompletoGuion = value;
			}
		}

		public string Espmed
		{
			get
			{
				return espmed.Text;
			}
			set
			{
				espmed.Text = value;
			}
		}
		#endregion
	}
}