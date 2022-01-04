using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Reporting.Processing;

namespace HConnexum.Tramitador.Sitio.Modulos.Reportes
{
	public partial class ComprobanteMovimientoCE : PaginaBase, IComprobanteMovimientoCE
	{
		
		#region "Variables Locales"
		
		#endregion "Variables Locales"
		
		#region "Propiedades de Presentación"
			
		public int IdMovimiento
		{
			get
			{
				return int.Parse(this.ExtraerDeViewStateOQueryString(@"id"));
			}
		}
			
		public int Seguro
		{
			get
			{
				return int.Parse(this.ExtraerDeViewStateOQueryString(@"seguro"));
			}
		}
			
		public string Usuario { get; set; }
		
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
				PresentadorComprobanteMovimientoCE presentador = new PresentadorComprobanteMovimientoCE(this);
				this.Usuario = presentador.ObtenerUsuarioActual();
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
			
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
				if (!this.Page.IsPostBack)
				{
					DisenoComprobanteMovCE report = new DisenoComprobanteMovCE();
					this.ComprobanteMovimiento.Report = report;
					report.ReportParameters["seguro"].Value = this.Seguro;
					report.ReportParameters["idexpweb"].Value = this.IdMovimiento;
					report.Error += delegate(object innerSender, Telerik.Reporting.ErrorEventArgs eventArgs)
					{
						this.ComprobanteMovimiento.Report = null;
						this.Errores = eventArgs.Exception.InnerException.InnerException.Message;
					};
					new ReportProcessor().RenderReport("MHTML", report, null);
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
			
		public IEnumerable<ComprobanteMovimientoCEDTO> LlenarComprobante(int seguro, int idexpweb)
		{
			try
			{
				PresentadorComprobanteMovimientoCE presentador = new PresentadorComprobanteMovimientoCE(this);
				return presentador.GenerarConsultaComprobante(idexpweb, seguro);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}