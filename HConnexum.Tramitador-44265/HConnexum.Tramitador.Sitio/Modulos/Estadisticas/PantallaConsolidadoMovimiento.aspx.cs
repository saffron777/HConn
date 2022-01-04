using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Data;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class PantallaConsolidadoMovimiento : PaginaDetalleBase, IPantallaConsolidadoMovimiento
	{
		#region "Variables Locales"
		///<summary>Variable presentador ReporteConsolidadoMovimientosUsuarios.</summary>
		PantallaConsolidadoMovimientoPresentadorDetalle presentador;
		#endregion "Variables Locales"

		#region "Propiedades de presentación"
		public DataTable GrupoEmpresarial
		{
			set
			{
				ddlGrupoEmpresarial.DataSource = value;
				ddlGrupoEmpresarial.DataBind();
			}
		}

		public string ValorComboGrupoEmpresarial
		{
			get
			{
				if (ddlGrupoEmpresarial.SelectedItem != null)
					return ddlGrupoEmpresarial.SelectedItem.Text;
				else
					return string.Empty;
			}
		}

		public string IdGrupoEmpresarial
		{
			get
			{
					return ddlGrupoEmpresarial.SelectedValue.ToString();
			}
		}

		public DataTable ComboSuscriptorGrupoEmpresarial
		{
			set
			{
				ddlSuscriptor.DataSource = value;
				ddlSuscriptor.DataBind();
			}
		}

		public IEnumerable<SuscriptorDTO> ComboSuscriptor
		{
			set
			{
				ddlSuscriptor.DataSource = value;
				ddlSuscriptor.DataBind();
			}
		}

		public DataTable ComboSuscriptorXUsuarioLogeado
		{
			set
			{
				ddlSuscriptor.DataSource = value;
				ddlSuscriptor.DataBind();
			}
		}

		public string ValorComboSuscriptor
		{
			get
			{
				if(ddlSuscriptor.SelectedItem != null)
					return ddlSuscriptor.SelectedItem.Text;
				else
					return string.Empty;
			}
			set
			{
				ddlSuscriptor.SelectedItem.Text = value;
			}
		}

		public string IdComboSuscriptor
		{
			get
			{
				return ddlSuscriptor.SelectedValue.ToString();
			}
			set
			{
				ddlSuscriptor.SelectedItem.Value = value;
			}
		}

		public DataTable ComboSucursal
		{
			set
			{
				ddlSucursal.DataSource = value;
				ddlSucursal.DataBind();
			}
		}

		public string IdComboSucursal
		{
			get
			{
				return ddlSucursal.SelectedValue.ToString();
			}
		}

		public DataTable ComboServicio
		{
			set
			{
				ddlServicio.DataSource = value;
				ddlServicio.DataBind();
			}
		}

		public string IdComboServicio
		{
			get
			{
				return ddlServicio.SelectedValue.ToString();
			}
		}

		public DataTable Area
		{
			set
			{
				ddlArea.DataSource = value;
				ddlArea.DataBind();
			}
		}

		public string IdArea
		{
			get
			{
				return ddlArea.SelectedValue.ToString();
			}
		}

		public string IdProveedor
		{
			get
			{
				return TxtHiddenId.Value;
			}
		}

		public DataTable Usuario
		{
			set
			{
				ddlUsuario.DataSource = value;
				ddlUsuario.DataBind();
			}
		}

		public string IdUsuario
		{
			get
			{
				return ddlUsuario.SelectedValue.ToString();
			}
		}

		public DataTable Pais
		{
			set
			{
				ddlPais.DataSource = value;
				ddlPais.DataBind();
			}
		}

		public string IdPais
		{
			get
			{
				return ddlPais.SelectedValue.ToString();
			}
		}

		public DataTable DivTerr1
		{
			set
			{
				ddlDivTerr1.DataSource = value;
				ddlDivTerr1.DataBind();
			}
		}

		public string IdDivTerr1
		{
			get
			{
				return ddlDivTerr1.SelectedValue.ToString();
			}
		}

		public DataTable DivTerr2
		{
			set
			{
				ddlDivTerr2.DataSource = value;
				ddlDivTerr2.DataBind();
			}
		}

		public string IdDivTerr2
		{
			get
			{
				return ddlDivTerr2.SelectedValue.ToString();
			}
		}

		public DataTable DivTerr3
		{
			set
			{
				ddlDivTerr3.DataSource = value;
				ddlDivTerr3.DataBind();
			}
		}

		public string IdDivTerr3
		{
			get
			{
				return ddlDivTerr3.SelectedValue.ToString();
			}
		}

		public string FechaDesde
		{
			get
			{
				if (txtFechaDesde.SelectedDate != null)
					return txtFechaDesde.SelectedDate.Value.ToString();
				else
					return string.Empty;
			}
		}

		public string FechaHasta
		{
			get
			{
				if (txtFechaHasta.SelectedDate != null)
					return txtFechaHasta.SelectedDate.Value.ToString();
				else
					return string.Empty;
			}
		}
		#endregion "Propiedades de presentación"

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new PantallaConsolidadoMovimientoPresentadorDetalle(this);
				ddlSucursal.Enabled = false;
				ddlArea.Enabled = false;
				ddlServicio.Enabled = false;
				ddlDivTerr1.Enabled = false;
				ddlDivTerr2.Enabled = false;
				ddlDivTerr3.Enabled = false;
				ddlUsuario.Enabled = false;
				ddlSuscriptor.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlSucursal.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlArea.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlDivTerr1.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlDivTerr2.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlDivTerr3.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlUsuario.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlServicio.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlGrupoEmpresarial.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
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
				if (!Page.IsPostBack)
				{
					ddlPais.Enabled = true;
					presentador.LlenarComboPais();
					if (!presentador.CargarComboGrupoEmpresariales())
					{
						ddlGrupoEmpresarial.Enabled = false;
						presentador.CargarComboSuscriptorXUsuarioLogeado();
						ddlSuscriptor.Enabled = false;
						ddlSuscriptor.Items[0].Selected = true;
						presentador.LlenarComboSuscursales(int.Parse(IdComboSuscriptor));
						presentador.LlenarComboServiciosPorIdSuscriptor(int.Parse(IdComboSuscriptor));
						presentador.LlenarComboUsuarioXIdSuscriptor(int.Parse(IdComboSuscriptor));
						ddlSucursal.Enabled = true;
						ddlServicio.Enabled = true;
						ddlUsuario.Enabled = true;
					}
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void ddlGrupoEmpresarial_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(IdGrupoEmpresarial))
			{
				ddlSuscriptor.ClearSelection();
				ddlSuscriptor.Items.Clear();
				ddlSuscriptor.Enabled = false;
			}
			else
			{
				ddlSuscriptor.ClearSelection();
				ddlSuscriptor.Items.Clear();
				int idGrupoEmpresarial = Convert.ToInt32(IdGrupoEmpresarial);
				presentador.LlenarComboSusCriptorXIdGrupoEmpresarial(idGrupoEmpresarial);
				ddlSuscriptor.Enabled = true;
			}
		}

		protected void ddlSuscriptor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
				if(presentador.IndicadorDueñoFlujoServicio(int.Parse(IdComboSuscriptor)))
					Button1.Enabled = true;
				else
				{
					TxtIdSuscriptor.Text = string.Empty;
					Button1.Enabled = false;
				}

				if (string.IsNullOrEmpty(IdComboSuscriptor))
				{
					ddlSucursal.ClearSelection();
					ddlSucursal.Items.Clear();
					ddlSucursal.Enabled = false;

					ddlServicio.ClearSelection();
					ddlServicio.Items.Clear();
					ddlServicio.Enabled = false;

					ddlUsuario.ClearSelection();
					ddlUsuario.Items.Clear();
					ddlUsuario.Enabled = false;
				}
				else
				{
					int idSuscriptor = Convert.ToInt32(IdComboSuscriptor);
					ddlSucursal.ClearSelection();
					ddlSucursal.Items.Clear();
					ddlServicio.ClearSelection();
					ddlServicio.Items.Clear();
					presentador.LlenarComboSuscursales(idSuscriptor);
					presentador.LlenarComboServiciosPorIdSuscriptor(idSuscriptor);
					presentador.LlenarComboUsuarioXIdSuscriptor(idSuscriptor);
					ddlSucursal.Enabled = true;
					ddlServicio.Enabled = true;
					ddlUsuario.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void ddlSucursal_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			int idSuscriptor = Convert.ToInt32(IdComboSuscriptor);
			int idSucursal = Convert.ToInt32(IdComboSucursal);
			if (string.IsNullOrEmpty(ddlSucursal.SelectedValue))
			{
				presentador.LlenarComboServiciosPorIdSuscriptor(idSuscriptor);
			}
			else
			{
				ddlServicio.ClearSelection();
				ddlServicio.Enabled = true;
				ddlArea.ClearSelection();
				ddlArea.Enabled = true;
				ddlUsuario.ClearSelection();
				ddlUsuario.Enabled = true;
				presentador.LlenarComboArea(idSucursal);
				presentador.LlenarComboServiciosXIdSuscriptorXidSucursal(idSuscriptor, idSucursal);
				presentador.LlenarComboUsuario(idSuscriptor, idSucursal, 0);
			}
		}

		protected void ddlArea_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			int idSuscriptor = Convert.ToInt32(IdComboSuscriptor);
			int idSucursal = Convert.ToInt32(IdComboSucursal);
			int idArea = Convert.ToInt32(IdArea);
			if (string.IsNullOrEmpty(ddlArea.SelectedValue))
				presentador.LlenarComboServiciosXIdSuscriptorXidSucursal(idSuscriptor, idSucursal);
			else
				presentador.LlenarComboUsuario(idSuscriptor, idSucursal, idArea);
		}

		protected void ddlPais_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(IdPais))
			{
				ddlDivTerr1.ClearSelection();
				ddlDivTerr1.Enabled = true;
				presentador.LlenarComboDiv1(Convert.ToInt32(IdPais));
			}
			else 
			{
				ddlDivTerr1.ClearSelection();
				ddlDivTerr1.Enabled = false;
			}
		}

		protected void ddlDivTerr1_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(IdDivTerr1))
			{
				ddlDivTerr2.ClearSelection();
				ddlDivTerr2.Enabled = true;
				presentador.LlenarComboDiv2(Convert.ToInt32(IdDivTerr1));
			}
			else
			{
				ddlDivTerr2.ClearSelection();
				ddlDivTerr2.Enabled = false;
			}
		}

		protected void ddlDivTerr2_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(IdDivTerr2))
			{
				ddlDivTerr3.ClearSelection();
				ddlDivTerr3.Enabled = true;
				presentador.LlenarComboDiv3(Convert.ToInt32(IdDivTerr2));
			}
			else
			{
				ddlDivTerr3.ClearSelection();
				ddlDivTerr3.Enabled = false;
			}
		}

		protected void ButtonProcesar_Click(object sender, EventArgs e)
		{
			StringBuilder url = new StringBuilder();
			url.Append(@"~/Modulos/Estadisticas/ReporteConsolidadoMovimientosUsuario.aspx?IdMenu=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&idsuscriptor=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdComboSuscriptor.Encriptar())) + "&idsucursal=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdComboSucursal.Encriptar())) + "&idservicio=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdComboServicio.Encriptar())) + "&idarea=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdArea.Encriptar())) + "&idusuario=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdUsuario.Encriptar())) + "&idproveedor=" );
			url.Append(IdProveedor + "&idpais=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdPais.ToString().Encriptar())) + "&iddivterr1=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdDivTerr1.ToString().Encriptar())) + "&iddivterr2=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdDivTerr2.ToString().Encriptar())) + "&iddivterr3=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdDivTerr3.ToString().Encriptar())) + "&fechadesde=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(FechaDesde.ToString().Encriptar())) + "&fechahasta=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(FechaHasta.ToString().Encriptar())) + "&suscriptor=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ValorComboSuscriptor.ToString().Encriptar())) + "&GE=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ValorComboGrupoEmpresarial.Encriptar())));
			Response.Redirect(url.ToString());
		}

		protected void txtFechaDesde_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
		{
			if (txtFechaDesde.SelectedDate == null)
			{
				txtFechaHasta.Clear();
				txtFechaHasta.Enabled = false;
			}
			else
				txtFechaHasta.Enabled = true;
			
		}

	}
}