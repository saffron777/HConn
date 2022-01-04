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
	public partial class ReporteCartaAvalConsultSalud : PaginaBase, IReporteCartaConsultSalud
	{
		int idcarta;
		int idsuscriptor;
		ReporteCartaConsultSaludPresentador presentador;

		#region "Propiedades de Presentación"
		public int idCarta
		{
			get
			{
				this.idcarta = int.Parse(this.ExtraerDeViewStateOQueryString("idCarta"));
				return idcarta;
			}
		}

		public int idSuscriptor
		{
			get
			{
				this.idsuscriptor = int.Parse(this.ExtraerDeViewStateOQueryString("idSuscriptor"));
				return idsuscriptor;
			}
		}
		#endregion

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
				presentador = new ReporteCartaConsultSaludPresentador(this);
				if(!IsPostBack)
				{
                    CartaAvalConsultSalud report = new CartaAvalConsultSalud();
                    ReportViewer1.Report = report;
                    report.ReportParameters["idCarta"].Value = idCarta;
                    report.ReportParameters["idSuscriptor"].Value = idSuscriptor;
                    report.Error += delegate(object innerSender, Telerik.Reporting.ErrorEventArgs eventArgs)
                    {
                        ReportViewer1.Report = null;
                        this.Errores = eventArgs.Exception.InnerException.InnerException.Message;
                    };
                    new ReportProcessor().RenderReport("MHTML", report, null);
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public IEnumerable<ReporteCartaAvalConsultSaludDTO> LlenarReporteDetalleSolicitud(int idCarta, int idSuscriptor)
		{
            try
            {
			    ReporteCartaConsultSaludPresentador presentador = new ReporteCartaConsultSaludPresentador(this);
			    return presentador.GenerarConsultaReporte(idCarta, idSuscriptor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}
	}
}

