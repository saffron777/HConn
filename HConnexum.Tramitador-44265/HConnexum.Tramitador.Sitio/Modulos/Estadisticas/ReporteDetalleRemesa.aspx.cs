using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class ReporteDetalleRemesa : PaginaBase, IDetalleRemesa
	{

		#region "Variables Locales"
		string usuario;
		int nremesa;
		string conexionstring;
		int idcodexterno;
		int idintermediario;
		PresentadorDetalleRemesa presentador;
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

		public int Nremesa
		{
			get
			{
				nremesa = int.Parse(this.ExtraerDeViewStateOQueryString("nremesa"));
				return nremesa;
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
				presentador = new PresentadorDetalleRemesa(this);
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
					DisenoReporteDetalleRemesa report = new DisenoReporteDetalleRemesa();
					report.ReportParameters["idSuscriptor"].Value = IdCodExterno;
					report.ReportParameters["nRemesa"].Value = Nremesa;
                    report.ReportParameters["idClinica"].Value = Convert.ToInt32(IdCodExternoUsuarioActual);
					report.ReportParameters["ConexionString"].Value = ConexionString;
					ReportDetalleRemesa.Report = report;
					ReportDetalleRemesa.RefreshReport();
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public IEnumerable<DetalleRemesaDTO> LlenarReporteDetalleRemesa(int idSuscriptor, int nRemesa, int idClinica, string conexionString)
		{
			PresentadorDetalleRemesa presentador = new PresentadorDetalleRemesa(this);
			return presentador.GenerarConsultaReporte(idSuscriptor, nRemesa, idClinica, conexionString);
		}
	}
}