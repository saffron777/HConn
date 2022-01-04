using System;
using System.Linq;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web.Configuration;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class ReporteConsolidadoMovimientosUsuario : PaginaBase, IReporteConsolidadoMovimiento
	{
		#region "Variables Locales"
		string usuario;
		string suscriptor;
		int? idsuscriptor;
		int? idsucursal;
		int? idservicio;
		int? idusuario;
		int? proveedor;
		int? area;
		int? pais;
		int? iddivterr1;
		int? iddivterr2;
		int? iddivterr3;
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

		public int? IdUsuario
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("idusuario");
				if (sId != "")
					this.idusuario = Convert.ToInt32(sId);
				return this.idusuario;
			}
		}

		public int? IdArea
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("idarea");
				if (sId != "")
					this.area = Convert.ToInt32(sId);
				return this.area;
			}
		}

		public int? IdPais
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("idpais");
				if (sId != "")
					this.pais = Convert.ToInt32(sId);
				return this.pais;
			}
		}

		public int? IdDivTerr1
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("iddivterr1");
				if (sId != "")
					this.iddivterr1 = Convert.ToInt32(sId);
				return this.iddivterr1;
			}
		}

		public int? IdDivTerr2
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("iddivterr2");
				if (sId != "")
					this.iddivterr2 = Convert.ToInt32(sId);
				return this.iddivterr2;
			}
		}

		public int? IdDivTerr3
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("iddivterr3");
				if (sId != "")
					this.iddivterr3 = Convert.ToInt32(sId);
				return this.iddivterr3;
			}
		}

		public int? IdProveedor
		{
			get
			{
				string sId = this.ExtraerDeViewStateOQueryString("idproveedor");
				if (sId != "")
					this.proveedor = Convert.ToInt32(sId);
				return this.proveedor;
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
				ReporteConsolidadoMovimientoPresentadorDetalle presentador = new ReporteConsolidadoMovimientoPresentadorDetalle(this);
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
					DisenoReporteConsolidadoMovimiento report = new DisenoReporteConsolidadoMovimiento();
					report.ReportParameters["UsuarioActual"].Value = Usuario;
					report.ReportParameters["GrupoEmpresarial"].Value = GrupoEmpresarial;
					report.ReportParameters["Suscriptor"].Value = Suscriptor;
					report.ReportParameters["IdSuscriptor"].Value = IdSuscriptor;
					report.ReportParameters["IdSucursal"].Value = IdSucursal;
					report.ReportParameters["IdServicio"].Value = IdServicio;
					report.ReportParameters["IdUsuario"].Value = IdUsuario;
					report.ReportParameters["IdArea"].Value = IdArea;
					report.ReportParameters["IdProveedor"].Value = IdProveedor;
					report.ReportParameters["IdPais"].Value = IdPais;
					report.ReportParameters["IdDivTerr1"].Value = IdDivTerr1;
					report.ReportParameters["IdDivTerr2"].Value = IdDivTerr2;
					report.ReportParameters["IdDivTerr3"].Value = IdDivTerr3;
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

		public IEnumerable<ReporteMovimientoDTO> LlenarReporteCosolidadoMovimiento(int? idSuscriptor, int? idSucursal, int? idServicio, int? idarea, int? idproveedor, int? idusuario, int? idpais, int? iddivterr1, int? iddivterr2, int? iddivterr3, DateTime? fechaDesde, DateTime? fechaHasta)
		{
			ReporteConsolidadoMovimientoPresentadorDetalle presentador = new ReporteConsolidadoMovimientoPresentadorDetalle(this);
			return presentador.GenerarConsultaReporte(idSuscriptor, idSucursal, idServicio, idarea, idproveedor, idusuario, idpais, iddivterr1, iddivterr2, iddivterr3, fechaDesde, fechaHasta, presentador.IndicadorDueñoFlujoServicio(idSuscriptor.Value));
		}
	}
}