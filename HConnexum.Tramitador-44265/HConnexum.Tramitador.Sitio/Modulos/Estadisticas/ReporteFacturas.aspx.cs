using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Collections.Generic;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class ReporteFacturas : PaginaBase, IReporteFacturas
	{
		#region "Variables Locales"
		string usuario;
		string estatus;
		string fechainicial;
		string fechafinal;
		string intermediario;
		int idintermediario;
		string compania;
		string nReclamo;
		string nFactura;
		string nCedula;
		string conexionstring;
		int idcodexterno;
		ReporteFacturasPresentador presentador;
		#endregion "Variables Locales"

		#region "Propiedades de Presentación"
		public string Usuario
		{
			set
			{
				usuario = value;
			}
			get
			{
				return usuario;
			}
		}

		public string ConexionString
		{
			get
			{
				if (conexionstring != null)
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
				if (idintermediario != null)
					return idintermediario;
				return 0;
			}
		}

		public int IdCodExterno
		{
			get
			{
				if (idcodexterno != null)
					return idcodexterno;
				return 0;
			}
		}
		
		public int? Nreclamo
		{
			get
			{
				this.nReclamo = this.ExtraerDeViewStateOQueryString(@"nReclamo");
				if (string.IsNullOrWhiteSpace(nReclamo))
					return 0;
				else
					return Convert.ToInt32(this.nReclamo);
			}
		}
		
		public string Nfactura
		{
			get
			{
				this.nFactura = this.ExtraerDeViewStateOQueryString(@"nFactura");
				return this.nFactura;
			}
		}

		public string Ncedula
		{
			get
			{
				this.nCedula = this.ExtraerDeViewStateOQueryString(@"nCedula");
				
				if (string.IsNullOrWhiteSpace(nCedula))
					return string.Empty;
				else
					return this.nCedula;
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
				if (!String.IsNullOrWhiteSpace(this.fechainicial = this.ExtraerDeViewStateOQueryString(@"fechainicial")))
					return Convert.ToDateTime(this.fechainicial);
				else
					return null;
			}
		}
		
		public DateTime? FechaFinal
		{
			get
			{
				if (!String.IsNullOrWhiteSpace(this.fechafinal = this.ExtraerDeViewStateOQueryString(@"fechafinal")))
					return Convert.ToDateTime(this.fechafinal);
				else
					return null;
			}
		}
		
		public string Compania
		{
			get
			{
				this.compania = this.ExtraerDeViewStateOQueryString(@"compañia");
				return this.compania;
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
				presentador = new ReporteFacturasPresentador(this);
				base.Page_Init(sender, e);
				idintermediario = Convert.ToInt32(ExtraerDeViewStateOQueryString(@"intermediario"));
				conexionstring = presentador.ObtenerConexionString(IdIntermediario);
				idcodexterno = Convert.ToInt32(ExtraerDeViewStateOQueryString(@"idcodexterno"));
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
				
				if (!IsPostBack)
				{
					ViewState["IdCodExternoUsuarioActual"] = ExtraerDeViewStateOQueryString(@"idcodexterusuact");
					var idCodExternoUsuarioActual = ViewState["IdCodExternoUsuarioActual"];
					
					DisenoReporteFactura report = new DisenoReporteFactura();
					report.ReportParameters["ConexionString"].Value = conexionstring;
					report.ReportParameters["idSuscriptor"].Value = Convert.ToInt32(idCodExternoUsuarioActual);
					report.ReportParameters["IdCodExterno"].Value = idcodexterno;
					report.ReportParameters["estatus"].Value = Estatus;
					report.ReportParameters["fechaInicial"].Value = FechaInicial;
					report.ReportParameters["fechaFinal"].Value = FechaFinal;
					report.ReportParameters["nReclamo"].Value = Nreclamo;
					report.ReportParameters["nCedula"].Value = Ncedula;
					report.ReportParameters["nFactura"].Value = Nfactura;
					report.ReportParameters["TipoProveedor"].Value = TipoProveedor;
					report.ReportParameters["Compania"].Value = Compania;
					ReporteFactura.Report = report;
					ReporteFactura.RefreshReport();
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public IEnumerable<FacturasDTO> LlenarReporteFacturas(string conexionstring, int idSuscriptor, int idCodExterno, string estatus, DateTime? fechaInicial, DateTime? fechaFinal, string nFactura, int? nReclamo, string nCedula)
		{
			ReporteFacturasPresentador presentador = new ReporteFacturasPresentador(this);
			return presentador.GenerarReporte(conexionstring, WebConfigurationManager.AppSettings[@"StoredProceduresListaFacturas"], idSuscriptor, idCodExterno, estatus, fechaInicial, fechaFinal, nFactura, nReclamo, nCedula);
		}
	}
}