using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class ReporteDetalleSolicitud : PaginaBase, IDetalleSolicitud
	{
		#region "Variables Locales"
		string usuario;
		int nremesa;
		int idintermediario;
		string conexionstring;
		PresentadorReporteSolicitud presentador;
		#endregion "Variables Locales"

		#region "Propiedades de Presentación"
		public string Usuario
		{
			set { usuario = value; }
			get { return usuario; }
		}

		public string ConexionString
		{
			get
			{
				if(conexionstring != null)
					return conexionstring;
				return string.Empty;
			}
		}

		public int IdIntermediario
		{
			get
			{
				if(idintermediario != null)
					return idintermediario;
				return 0;
			}
		}

		public int Nremesa
		{
			get
			{
				nremesa = int.Parse(this.ExtraerDeViewStateOQueryString("nremesa"));
				return nremesa;
			}
		}

		public int IdCodExterno
		{
			get
			{
				return int.Parse(this.ExtraerDeViewStateOQueryString("idCodExterno"));
			}
		}
		#endregion "Propiedades de Presentación"

		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new PresentadorReporteSolicitud(this);
				idintermediario = Convert.ToInt32(ExtraerDeViewStateOQueryString(@"intermediario"));
				conexionstring = presentador.ObtenerConexionString(IdIntermediario);
			}
			catch(Exception ex)
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
				if(!IsPostBack)
				{
					DisenoReporteDetalleSolicitud report = new DisenoReporteDetalleSolicitud();
					report.ReportParameters["nRemesa"].Value = Nremesa;
					report.ReportParameters["conexionString"].Value = ConexionString;
					report.ReportParameters["idCodExterno"].Value = IdCodExterno;
					ReportDetalleSolicitud.Report = report;
					ReportDetalleSolicitud.RefreshReport();
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public IEnumerable<DatosSolicitudDTO> LlenarReporteDetalleSolicitud(int idCodExterno, int nRemesa, string conexionString)
		{
			PresentadorReporteSolicitud presentador = new PresentadorReporteSolicitud(this);
			return presentador.GenerarConsultaReporte(idCodExterno, nRemesa, conexionString);
		}
	}
}