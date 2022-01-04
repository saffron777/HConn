using System;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class BlqNotRespInm : UserControlBase, IBlqNotRespInm
	{
		BlqNotRespInmPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new BlqNotRespInmPresentador(this);
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

		public void btnImprimir_OnClick(object sender, EventArgs e)
		{
			StringBuilder url = new StringBuilder();
			url.Append(@"~/Modulos/Reportes/ComprobanteMovimientoCE.aspx?id=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ParametrosEntrada["IDEXPEDIENTEWEB"].Encriptar())) + "&seguro=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ParametrosEntrada["IDSUSINTERMEDIARIO"].Encriptar())));
			RadWindow radWindow = new RadWindow();
			radWindow.NavigateUrl = url.ToString();
			radWindow.VisibleOnPageLoad = true;
            radWindow.Width = 900;
            radWindow.Modal = true;
            radWindow.KeepInScreenBounds = true;
            radWindow.Height = 1100;
            radWindow.CssClass="Sinscrol";
            radWindow.VisibleStatusbar = false;
            radWindow.InitialBehavior = WindowBehaviors.Maximize;
        
			this.Controls.Add(radWindow);
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
		public string PIdReclamo
		{
			get
			{
				return IdReclamo.Value;
			}
			set
			{
				IdReclamo.Value = value;
			}
		}

        public string PIdSupportIncident
        {
            get
            {
                return IdSupportIncident.Value;
            }
            set
            {
                IdSupportIncident.Value = value;
            }
        }

		public string PEstatusMovimientoWeb
		{
			get
			{
				return EstatusMovimientoWeb.Value;
			}
			set
			{
				EstatusMovimientoWeb.Value = value;
			}
		}

		public string PIndMvtoAutomatico
		{
			get
			{
				return IndMvtoAutomatico.Value;
			}
			set
			{
				IndMvtoAutomatico.Value = value;
			}
		}

		public string Mensaje
		{
			get
			{
				return txtMensaje.Text;
			}
			set
			{
				txtMensaje.Text = value;
			}
		}

		public bool MostrarImprimir
		{
			get
			{
				return btnImprimir.Visible;
			}
			set
			{
				btnImprimir.Visible = value;
			}
		}
		#endregion Propiedades
	}
}