using System;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class DatosGenerales : UserControlBase, IDatosGenerales
	{
		DatosGeneralesPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new DatosGeneralesPresentador(this);
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
		public string PTipDocASeg
		{
			get
			{
				return tipdocaseg.Text;
			}
			set
			{
				tipdocaseg.Text = value;
			}
		}

		public string PNumDocASeg
		{
			get
			{
				return numdocaseg.Text;
			}
			set
			{
				numdocaseg.Text = value;
			}
		}

		public string PNomAseg
		{
			get
			{
				return nomaseg.Text;
			}
			set
			{
				nomaseg.Text = value;
			}
		}

		public string PContratante
		{
			get
			{
				return contratante.Text;
			}
			set
			{
				contratante.Text = value;
			}
		}

		public string PNomClinica
		{
			get
			{
				return nomclinica.Text;
			}
			set
			{
				nomclinica.Text = value;
			}
		}

		public string PServicio
		{
			get
			{
				return servicio.Text;
			}
			set
			{
				servicio.Text = value;
			}
		}

		public string PDiagnostico
		{
			get
			{
				return diagnostico.Text;
			}
			set
			{
				diagnostico.Text = value;
			}
		}

		public string PIdExterno
		{
			get
			{
				return IdCasoExterno.Text;
			}
			set
			{
				IdCasoExterno.Text = value;
			}
		}

		public string PEspecialidad
		{
			get
			{
				return fechasolicitud.SelectedDate.ToString();
			}
			set
			{
				fechasolicitud.DbSelectedDate = ExtensionesString.ConvertirFecha(value);
			}
		}

		public string PTlfAseg
		{
			get
			{
				return tlfaseg.Text;
			}
			set
			{
				tlfaseg.Text = value;
			}
		}

		public string PIdCaso
		{
			get
			{
				return IdCasoHc.Text;
			}
			set
			{
				IdCasoHc.Text = value;
			}
		}
		#endregion Propiedades
	}
}