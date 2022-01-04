using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web;

namespace HConnexum.Tramitador.Sitio.Modulos.Reportes
{
	public partial class ReporteOpMed : PaginaBase, IReporteOpMed
	{
		ReporteOpMedPresentador presentador;

		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
				presentador = new ReporteOpMedPresentador(this);
				if (!IsPostBack)
				{
					Report3 report = new Report3();
					ReportViewer1.Report = report;
					ReportViewer1.RefreshReport();
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public ReporteOpMedDTO LlenarReporteDetalleSolicitud()
		{
			IdMovimiento = HttpContext.Current.Session["reporteImpIdMovimiento"] != null ? int.Parse(HttpContext.Current.Session["reporteImpIdMovimiento"].ToString()) : 0;
			Antecedentes = HttpContext.Current.Session["reporteImpAntecedentes"] != null ? HttpContext.Current.Session["reporteImpAntecedentes"].ToString() : string.Empty;
			Cicatrices = HttpContext.Current.Session["reporteImpCicatrices"] != null ? HttpContext.Current.Session["reporteImpCicatrices"].ToString() : string.Empty;
			Peso = HttpContext.Current.Session["reporteImpPeso"] != null ? HttpContext.Current.Session["reporteImpPeso"].ToString() : string.Empty;
			Talla = HttpContext.Current.Session["reporteImpTalla"] != null ? HttpContext.Current.Session["reporteImpTalla"].ToString() : string.Empty;
			TensionArterial = HttpContext.Current.Session["reporteImpTensionArterial"] != null ? HttpContext.Current.Session["reporteImpTensionArterial"].ToString() : string.Empty;
			Observacionmed = HttpContext.Current.Session["reporteImpObservacionmed"] != null ? HttpContext.Current.Session["reporteImpObservacionmed"].ToString() : string.Empty;
			OpMed = HttpContext.Current.Session["reporteImpOpMed"] != null ? HttpContext.Current.Session["reporteImpOpMed"].ToString() : string.Empty;

			ReporteOpMedPresentador presentador = new ReporteOpMedPresentador(this);

			Session["reporteImpIdMovimiento"] = null;
			Session["reporteImpAntecedentes"] = null;
			Session["reporteImpCicatrices"] = null;
			Session["reporteImpPeso"] = null;
			Session["reporteImpTalla"] = null;
			Session["reporteImpTensionArterial"] = null;
			Session["reporteImpObservacionmed"] = null;
			Session["reporteImpOpMed"] = null;

			return presentador.ObtenerReporteOpMed(this);
		}

		#region "Propiedades de Presentación"

		public int IdMovimiento { get; set; }
		public string Antecedentes { get; set; }
		public string Cicatrices { get; set; }
		public string Peso { get; set; }
		public string Talla { get; set; }
		public string TensionArterial { get; set; }
		public string Observacionmed { get; set; }
		public string OpMed { get; set; }
		public string Errores { get; set; }
		
		#endregion "Propiedades de Presentación"
	}
}