using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Collections.Generic;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class ReporteRelaciones : PaginaBase, IReporteRelaciones
	{
		#region "Variables Locales"
		string usuario;
		string estatus;
		string fechainicial;
		string fechafinal;
		string intermediario;
		int idintermediario;
		string compañia;
		string conexionstring;
		int idcodexterno;
		PresentadorReporteRelaciones presentador;
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

		public string TipoProveedor
		{
			get
			{
				this.intermediario = this.ExtraerDeViewStateOQueryString(@"tipoproveedor");
				return this.intermediario;
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

		public int IdCodExterno
		{
			get
			{
				if(idcodexterno != null)
					return idcodexterno;
				return 0;
			}
		}

		public string Estatus
		{
			get
			{
				this.estatus = this.ExtraerDeViewStateOQueryString(@"estatus");
				return this.estatus;
			}
		}

		public DateTime? FechaInicial
		{
			get
			{
				if(!String.IsNullOrWhiteSpace(this.fechainicial = this.ExtraerDeViewStateOQueryString(@"fechainicial")))
					return Convert.ToDateTime(this.fechainicial);
				else
					return null;
			}
		}

		public DateTime? FechaFinal
		{
			get
			{
				if(!String.IsNullOrWhiteSpace(this.fechafinal = this.ExtraerDeViewStateOQueryString(@"fechafinal")))
					return Convert.ToDateTime(this.fechafinal);
				else
					return null;
			}
		}

		public string Compañia
		{
			get
			{
				this.compañia = this.ExtraerDeViewStateOQueryString(@"compañia");
				return this.compañia;
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
				presentador = new PresentadorReporteRelaciones(this);
				base.Page_Init(sender, e);
				idintermediario = Convert.ToInt32(ExtraerDeViewStateOQueryString(@"intermediario"));
				conexionstring = presentador.ObtenerConexionString(IdIntermediario);
                idcodexterno = Convert.ToInt32(ExtraerDeViewStateOQueryString(@"idcodexterno"));
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
                    ViewState["IdCodExternoUsuarioActual"] = ExtraerDeViewStateOQueryString(@"idcodexterusuact");
                    var IdCodExternoUsuarioActual = ViewState["IdCodExternoUsuarioActual"];
					DisenoReporteRelaciones report = new DisenoReporteRelaciones();
					report.ReportParameters["estatus"].Value = Estatus;
					report.ReportParameters["TipoProveedor"].Value = TipoProveedor;
					report.ReportParameters["Compañia"].Value = Compañia;
					report.ReportParameters["fechaInicial"].Value = FechaInicial;
					report.ReportParameters["fechaFinal"].Value = FechaFinal;
					report.ReportParameters["ConexionString"].Value = conexionstring;
                    report.ReportParameters["idSuscriptor"].Value = Convert.ToInt32(IdCodExternoUsuarioActual);
					report.ReportParameters["IdCodExterno"].Value = idcodexterno;
					ReportRelaciones.Report = report;
					ReportRelaciones.RefreshReport();
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public IEnumerable<RelacionesDTO> LlenarReporteRelaciones(int idSuscriptor, int idcodexterno, string conexionstring, string estatus, string tipoproveedor, string compañia, DateTime? fechaInicial, DateTime? fechaFinal)
		{
			PresentadorReporteRelaciones presentador = new PresentadorReporteRelaciones(this);
			return presentador.GenerarReporte(idSuscriptor, idcodexterno, WebConfigurationManager.AppSettings[@"Estatus" + estatus], conexionstring, fechaInicial, fechaFinal, estatus);
		}
	}
}