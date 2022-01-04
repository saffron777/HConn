using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class ReporteDetalleFactura : PaginaBase, IReporteDetalleFactura
	{

		#region "Variables Locales"
		string usuario;
		string nFactura;
		string conexionstring;
		int idcodexterno;
		int idintermediario;
		ReporteDetalleFacturaPresentador presentador;
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

		public int IdCodExterno
		{
			get
			{
				if(idcodexterno != null)
					return idcodexterno;
				return 0;
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

		public string Nfactura
		{
			get
			{
				this.nFactura = this.ExtraerDeViewStateOQueryString(@"nFactura");
				return this.nFactura;
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
				presentador = new ReporteDetalleFacturaPresentador(this);
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
                    string IdCodExternoUsuarioActual = ExtraerDeViewStateOQueryString(@"idcodexterusuact");
					DisenoReporteDetalleFactura report = new DisenoReporteDetalleFactura();
					report.ReportParameters["idSuscriptor"].Value = IdCodExterno;
					report.ReportParameters["nFactura"].Value = Nfactura;
                    report.ReportParameters["idClinica"].Value = Convert.ToInt32(IdCodExternoUsuarioActual);
					report.ReportParameters["ConexionString"].Value = ConexionString;
					ReporteDetalleFacturas.Report = report;
					ReporteDetalleFacturas.RefreshReport();
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public IEnumerable<DetalleFacturaDTO> LlenarReporteDetalleFactura(int idSuscriptor, string nFactura, int idClinica, string conexionString)
		{
			ReporteDetalleFacturaPresentador presentador = new ReporteDetalleFacturaPresentador(this);
			return presentador.GenerarConsultaReporte(idSuscriptor, nFactura, idClinica, conexionString);
		}		
	}
}