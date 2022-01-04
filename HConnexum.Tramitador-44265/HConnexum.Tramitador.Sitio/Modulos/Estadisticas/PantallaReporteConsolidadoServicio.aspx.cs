﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class PantallaReporteConsolidadoServicio : PaginaDetalleBase, IPantallaConsolidadoServicio
	{
		#region "Variables Locales"
		///<summary>Variable presentador PantallaConsolidadoServicioPresentador.</summary>
		PantallaConsolidadoServicioPresentador presentador;
		#endregion "Variables Locales"

		#region "Propiedades Presentador"
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
		#endregion "Propiedades Presentador"

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new PantallaConsolidadoServicioPresentador(this);
				ddlSucursal.Enabled = false;
				ddlServicio.Enabled = false;
				ddlSuscriptor.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
				ddlSucursal.Filter = Telerik.Web.UI.RadComboBoxFilter.StartsWith;
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
					if (!presentador.CargarComboGrupoEmpresariales())
					{
						ddlGrupoEmpresarial.Enabled = false;
						presentador.CargarComboSuscriptorXUsuarioLogeado();
						ddlSuscriptor.Enabled = false;
						ddlSuscriptor.Items[0].Selected = true;
						presentador.LlenarComboSuscursales(int.Parse(IdComboSuscriptor));
						presentador.LlenarComboServiciosPorIdSuscriptor(int.Parse(IdComboSuscriptor));
						ddlSucursal.Enabled = true;
						ddlServicio.Enabled = true;
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
			try
			{
				if (string.IsNullOrEmpty(ddlGrupoEmpresarial.SelectedValue))
				{
					ddlSuscriptor.ClearSelection();
					ddlSuscriptor.Items.Clear();
					ddlSuscriptor.Enabled = false;
				}
				else
				{
					ddlSuscriptor.ClearSelection();
					ddlSuscriptor.Items.Clear();
					int idGrupoEmpresarial = Convert.ToInt32(ddlGrupoEmpresarial.SelectedValue);
					presentador.LlenarComboSusCriptorXIdGrupoEmpresarial(idGrupoEmpresarial);
					ddlSuscriptor.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void ddlSuscriptor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(ddlSuscriptor.SelectedValue))
				{
					ddlSucursal.ClearSelection();
					ddlSucursal.Items.Clear();
					ddlSucursal.Enabled = false;

					ddlServicio.ClearSelection();
					ddlServicio.Items.Clear();
					ddlServicio.Enabled = false;
				}
				else
				{
					int idSuscriptor = Convert.ToInt32(ddlSuscriptor.SelectedItem.Value);
					ddlSucursal.ClearSelection();
					ddlSucursal.Items.Clear();
					ddlServicio.ClearSelection();
					ddlServicio.Items.Clear();
					presentador.LlenarComboSuscursales(idSuscriptor);
					presentador.LlenarComboServiciosPorIdSuscriptor(idSuscriptor);
					ddlSucursal.Enabled = true;
					ddlServicio.Enabled = true;
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
			try
			{
				int idSuscriptor = Convert.ToInt32(ddlSuscriptor.SelectedValue);
				int idSucursal = Convert.ToInt32(ddlSucursal.SelectedValue);
				if (string.IsNullOrEmpty(ddlSucursal.SelectedValue))
				{
					presentador.LlenarComboServiciosPorIdSuscriptor(idSuscriptor);
				}
				else
				{
					ddlServicio.ClearSelection();
					ddlServicio.Enabled = true;
					presentador.LlenarComboServiciosXIdSuscriptorXidSucursal(idSuscriptor, idSucursal);
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void ButtonProcesar_Click(object sender, EventArgs e)
		{
			StringBuilder url = new StringBuilder();

			url.Append(@"~/Modulos/Estadisticas/ReporteConsolidadoServicio.aspx?IdMenu=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&idsuscriptor=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdComboSuscriptor.Encriptar())) + "&idsucursal=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdComboSucursal.Encriptar())) + "&idservicio=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdComboServicio.Encriptar())) + "&fechadesde=");
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