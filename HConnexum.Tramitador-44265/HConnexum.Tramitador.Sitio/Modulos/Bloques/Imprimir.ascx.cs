using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;
using System.Configuration;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class Imprimir : UserControlBase, IImprimir
	{
		ImprimirPresentadorDetalle presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new ImprimirPresentadorDetalle(this);
			}
			catch (Exception ex)
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
				fechaOpMed.Value = DateTime.Now.ToString("yyyyMMdd");
				if (!Page.IsPostBack)
					presentador.LlenarCombo();
			}
			catch (Exception ex)
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
				if (Errores.Length > 0)
					return Errores;
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
			return string.Empty;
		}

		protected void btnImprimir_Click(object sender, EventArgs e)
		{
			try
			{
					Session["reporteImpIdMovimiento"] = IdMovimiento != null ? IdMovimiento : 0;
					Session["reporteImpAntecedentes"] = antecedentes.Text;
					Session["reporteImpCicatrices"] = cicatrices.Text;
					Session["reporteImpPeso"] = peso.Text;
					Session["reporteImpTalla"] = talla.Text;
					Session["reporteImpTensionArterial"] = tension.Text;
					Session["reporteImpObservacionmed"] = observacionmed.Text;
					Session["reporteImpOpMed"] = OpMed.Text.ToUpper();

					RadWindow window1 = new RadWindow();
					window1.NavigateUrl = "~/Modulos/Reportes/ReporteOpMed.aspx";
					window1.VisibleOnPageLoad = true;
					window1.Behavior = WindowBehaviors.Close | WindowBehaviors.Resize | WindowBehaviors.Move;
					window1.Width = 920;
					window1.Height = 760;
					window1.KeepInScreenBounds = true;
					this.Controls.Add(window1);
					lbMensajeImpresion.Visible = true;
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		#region "Propiedades de Presentación"

		#region "Propiedades RegistroOpinionMedico Unificacion Bloques"

		public string ValorDesicion = ConfigurationManager.AppSettings[@"ListaValorDesicionSimpleNo"].ToString();

		public string Pobservacionmed
		{
			get
			{
				return observacionmed.Text;
			}
			set
			{
				observacionmed.Text = value;
			}
		}

		public string MensajeError
		{
			get
			{
				return lblMsjError.Text;
			}
			set
			{
				lblMsjError.Text = value;
			}
		}

		public string IdDecisionSimple
		{
			get
			{
				return OpMed.SelectedValue;
			}
			set
			{
				OpMed.SelectedValue = value;
			}
		}

		public IEnumerable<ListasValorDTO> ComboDecisionSimple
		{
			set
			{
				OpMed.DataSource = value;
				OpMed.DataBind();
			}
		}

		/// <summary>Fecha de la opinión médica.</summary>
		public DateTime Fecha
		{
			get
			{
				return DateTime.Now;
			}
			set
			{
				fechaOpMed.Value = value.ToString("yyyyMMdd");
			}
		}

		#endregion "Propiedades RegistroOpinionMedico Unificacion Bloques"

		public string Pantecedentes
		{
			get
			{
				return antecedentes.Text;
			}
			set
			{
				antecedentes.Text = value;
			}
		}

		public string Pcicatrices
		{
			get
			{
				return cicatrices.Text;
			}
			set
			{
				cicatrices.Text = value;
			}
		}

		public string Ppeso
		{
			get
			{
				return peso.Text;
			}
			set
			{
				peso.Text = value;
			}
		}

		public string Ptalla
		{
			get
			{
				return talla.Text;
			}
			set
			{
				talla.Text = value;
			}
		}

		public string Ptension
		{
			get
			{
				return tension.Text;
			}
			set
			{
				tension.Text = value;
			}
		}
		
		#endregion
	}
}