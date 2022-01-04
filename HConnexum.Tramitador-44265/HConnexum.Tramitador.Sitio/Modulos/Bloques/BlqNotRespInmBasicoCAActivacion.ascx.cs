using System;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;
using HConnexum.Tramitador.Negocio;
using System.Text.RegularExpressions;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class BlqNotRespInmBasicoCAActivacion : UserControlBase, IBlqNotRespInmBasicoCAActivacion
	{
		BlqNotRespInmBasicoCAActivacionPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new BlqNotRespInmBasicoCAActivacionPresentador(this);
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
				if(MostrarImprimir) btnImprimir.Enabled = MostrarImprimir;
				if(ParametrosEntrada[@"TIPOMOV"].ToUpper() == @"ACTIVACIÓN")
					btnImprimir.Text = @"Imprimir Carta Aval";
				else
					btnImprimir.Text = @"Imprimir Comprobante";
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		#region ..:: [ PROPIEDADES ] ::..
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

		public string PActivacionCa
		{
			get
			{
				return ActivacionCa.Value;
			}
			set
			{
				ActivacionCa.Value = value;
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
		#endregion

		protected void btnImprimir_Click(object sender, EventArgs e)
		{
			if(ParametrosEntrada[@"TIPOMOV"].ToUpper() == @"ACTIVACIÓN")
			{
				string url = presentador.ObtenerUrlReporteCartaAval(int.Parse(ParametrosEntrada["IDSUSINTERMEDIARIO"]), WebConfigurationManager.AppSettings[@"DatoParticularCartaAval"]);
				Solicitud solicitud = presentador.ObtenerSolicitud(IdMovimiento);
				string idCasoExterno = string.Empty;
				if(solicitud != null)
					idCasoExterno = solicitud.IdCasoExterno;
				if(!string.IsNullOrWhiteSpace(url))
				{
					StringBuilder queryString = new StringBuilder();
					queryString.Append(@"~/Modulos/Reportes/" + url + @"?idCarta=");
					queryString.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(idCasoExterno.Encriptar())) + "&idSuscriptor=");
					queryString.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ParametrosEntrada["IDSUSINTERMEDIARIO"].Encriptar())));
					RadWindow radWindow = new RadWindow();
					radWindow.NavigateUrl = queryString.ToString();
					radWindow.VisibleOnPageLoad = true;
                    radWindow.Width = 900;
                    radWindow.Modal = true;
                    radWindow.KeepInScreenBounds = true;
                    radWindow.Height = 1100;
                    radWindow.CssClass = "Sinscrol";
                    radWindow.VisibleStatusbar = false;
                    radWindow.InitialBehavior = WindowBehaviors.Maximize;
					this.Controls.Add(radWindow);
				}
				else
				{
					Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
					Trace.Warn(@"Error", @"Error, La url del reporte carta aval es nula o esta vacia");
				}
			}
			else
			{
				string textoOriginal = ParametrosEntrada["NOMTIPOMOV"];
				string textoNormalizado = textoOriginal.Normalize(NormalizationForm.FormD);
				Regex reg = new Regex("[^a-zA-Z0-9 ]");
				string textoSinAcentos = reg.Replace(textoNormalizado, "");

				StringBuilder url = new StringBuilder();
				url.Append(@"~/Modulos/Reportes/ComprobanteMovimientoCA.aspx?idexpweb=");
				url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ParametrosEntrada["IDEXPEDIENTEWEB"].Encriptar())) + "&nomtipomov=");
				url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(textoSinAcentos.Encriptar())) + "&idsusintermediario=");
				url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ParametrosEntrada["IDSUSINTERMEDIARIO"].Encriptar())));
				
                RadWindow radWindow = new RadWindow();
				radWindow.NavigateUrl = url.ToString();
				radWindow.VisibleOnPageLoad = true;
                radWindow.Width = 900;
                radWindow.Modal = true;
                radWindow.KeepInScreenBounds = true;
                radWindow.Height = 1100;
                radWindow.CssClass = "Sinscrol";
                radWindow.VisibleStatusbar = false;
                radWindow.InitialBehavior = WindowBehaviors.Maximize;
				this.Controls.Add(radWindow);
			}
		}
	}
}