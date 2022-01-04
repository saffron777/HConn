using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class ReporteConsolidadoServicio : PaginaBase, IReporteConsolidadoServicio
	{
		#region "Variables Locales"
		string usuario;
		string suscriptor;
		int? idsuscriptor;
		int? idsucursal;
		int? idservicio;
		int? idusuario;
		DateTime? fechadesde;
		DateTime? fechahasta;
		string grupoempresarial;
		#endregion "Variables Locales"

		#region "Propiedades de Presentación"
		public string GrupoEmpresarial
		{
			get
			{
				this.grupoempresarial = this.ExtraerDeViewStateOQueryString("GE");
				return this.grupoempresarial;
			}
		}

		public string Suscriptor
		{
			get
			{
				this.suscriptor = this.ExtraerDeViewStateOQueryString("suscriptor");
				return this.suscriptor;
			}
		}

		public int? IdSuscriptor
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("idsuscriptor");
				if (sId != "")
					this.idsuscriptor = Convert.ToInt32(sId);
				return this.idsuscriptor;
			}
		}

		public int? IdSucursal
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("idsucursal");
				if (sId != "")
					this.idsucursal = Convert.ToInt32(sId);
				return this.idsucursal;
			}
		}

		public int? IdServicio
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("idservicio");
				if (sId != "")
					this.idservicio = Convert.ToInt32(sId);
				return this.idservicio;
			}
		}

		public DateTime? FechaDesde
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("fechadesde");
				if (sId != "")
					this.fechadesde = Convert.ToDateTime(sId);
				return this.fechadesde;
			}
		}

		public DateTime? FechaHasta
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("fechahasta");
				if (sId != "")
					this.fechahasta = Convert.ToDateTime(sId);
				return this.fechahasta;
			}
		}

				public string Usuario
		{
			set { usuario = value; }
			get { return usuario; }
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
				ReporteConsolidadoServicioPresentador presentador = new ReporteConsolidadoServicioPresentador(this);
				Usuario = presentador.ObtenerUsuarioActual();
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
					DisenoReporteConsolidadoServicio report = new DisenoReporteConsolidadoServicio();
					report.ReportParameters["UsuarioActual"].Value = Usuario;
					report.ReportParameters["GrupoEmpresarial"].Value = GrupoEmpresarial;
					report.ReportParameters["Suscriptor"].Value = Suscriptor;
					report.ReportParameters["IdSuscriptor"].Value = IdSuscriptor;
					report.ReportParameters["IdSucursal"].Value = IdSucursal;
					report.ReportParameters["IdServicio"].Value = IdServicio;
					report.ReportParameters["FechaDesde"].Value = FechaDesde;
					report.ReportParameters["FechaHasta"].Value = FechaHasta;
					ReportCasos.Report = report;
					ReportCasos.RefreshReport();
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public IEnumerable<ReporteConsolidadoServicioDTO> LlenarReporteCosolidadoServicio(int? idSuscriptor, int? idSucursal, int? idServicio, DateTime? fechaDesde, DateTime? fechaHasta)
		{
			ReporteConsolidadoServicioPresentador presentador = new ReporteConsolidadoServicioPresentador(this);
			return presentador.GenerarConsultaReporte(idSuscriptor, idSucursal, idServicio, fechaDesde, fechaHasta);
		}
	}
}