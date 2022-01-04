using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Reporting.Processing;

namespace HConnexum.Tramitador.Sitio.Modulos.Reportes
{
	public partial class ComprobanteMovimientoCA : PaginaBase, IComprobanteMovimiento
	{
		
		#region "Variables Locales"
		
		#endregion "Variables Locales"
		
		#region "Propiedades de Presentación"
			
		public string NomTipoMov
		{
			get
			{
				return (this.ExtraerDeViewStateOQueryString(@"nomtipomov"));
			}
		}
			
		public int IdExpWeb
		{
			get
			{
				return Convert.ToInt32(this.ExtraerDeViewStateOQueryString(@"idexpweb"));
			}
		}
			
		public int IdSusIntermediario
		{
			get
			{
				return Convert.ToInt32(this.ExtraerDeViewStateOQueryString(@"idsusintermediario"));
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
				PresentadorComprobanteMovimiento presentador = new PresentadorComprobanteMovimiento(this);
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
					DisenoComprobanteMovCA report = new DisenoComprobanteMovCA();
					this.ComprobanteMovimiento.Report = report;
					report.ReportParameters["idexpweb"].Value = this.IdExpWeb;
					report.ReportParameters["nomtipomov"].Value = this.NomTipoMov;
					report.ReportParameters["idsusintermediario"].Value = this.IdSusIntermediario;
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
			
		public IEnumerable<ComprobanteMovimientoCADTO> LlenarComprobante(int idexpweb, string nomtipomov, int idsusintermediario)
		{
			try
			{
				PresentadorComprobanteMovimiento presentador = new PresentadorComprobanteMovimiento(this);
				return presentador.GenerarConsultaComprobante(nomtipomov, idexpweb, idsusintermediario);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}