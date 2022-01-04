using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
	///<summary>Clase ConsultaCasos.</summary>
	public partial class PoteCasos : PaginaMaestroDetalleBase, IPoteCasos
	{
		
		#region "Variables Locales"
		
		///<summary>Variable presentador ConsultaCasosPresentador.</summary>
		PoteCasosPresentador presentador;
		int IdIntermediario = 0;

		#endregion "Variables Locales"

		#region "Eventos de la Página"
		
		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new PoteCasosPresentador(this);
				this.Orden = "Id";
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
				if (!this.Page.IsPostBack)
				{
					this.presentador.LlenarSuscriptor();
					this.CultureDatePicker();
					this.txtFiltro.Enabled = false;
					this.RadGridMaster.Rebind();
					if (this.RadGridMaster.Items.Count.CompareTo(6) > 0)
					{
						this.RadGridMaster.MasterTableView.Style.Remove("height");
						this.RadGridMaster.MasterTableView.Style.Add("height", "100%");
					}
					else
					{
						this.RadGridMaster.MasterTableView.Style.Remove("height");
					}
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		///<summary>Evento pre visualización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
			RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
		}
		
		///<summary>Evento de comando que se dispara cuando se hace click en el boton buscar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdBuscar_Click(object sender, EventArgs e)
		{
			try
			{
				this.NumeroPagina = 1;
				this.RadGridMaster.Rebind();
				if (this.RadGridMaster.Items.Count.CompareTo(6) > 0)
				{
					this.RadGridMaster.MasterTableView.Style.Remove("height");
					this.RadGridMaster.MasterTableView.Style.Add("height", "100%");
				}
				else
				{
					this.RadGridMaster.MasterTableView.Style.Remove("height");
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
		{
			this.NumeroPagina = e.NewPageIndex + 1;
			this.RadGridMaster.Rebind();
			if (this.RadGridMaster.Items.Count.CompareTo(6) > 0)
			{
				this.RadGridMaster.MasterTableView.Style.Remove("height");
				this.RadGridMaster.MasterTableView.Style.Add("height", "100%");
			}
			else
			{
				this.RadGridMaster.MasterTableView.Style.Remove("height");
			}
		}
		
		///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
		{
			if (e != null)
			{
				this.Orden = string.Format("{0}{1}", e.SortExpression, e.NewSortOrder == GridSortOrder.Descending ? @" DESC" : @" ASC");
				this.RadGridMaster.Rebind();
			}
		}
		
		///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
		{
			this.NumeroPagina = 1;
			this.TamanoPagina = e.NewPageSize;
			this.RadGridMaster.Rebind();
			if (this.RadGridMaster.Items.Count.CompareTo(6) > 0)
			{
				this.RadGridMaster.MasterTableView.Style.Remove("height");
				this.RadGridMaster.MasterTableView.Style.Add("height", "100%");
			}
			else
			{
				this.RadGridMaster.MasterTableView.Style.Remove("height");
			}
		}
		
		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
				int idmovimiento = 0;
				if (this.Request.QueryString["id"] != null)
				{
					idmovimiento = Convert.ToInt32(this.ExtraerDeViewStateOQueryString("id"));
				}
				this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro,
					this.UsuarioActual.IdUsuarioSuscriptorSeleccionado, this.UsuarioActual.SuscriptorSeleccionado.Id, idmovimiento);
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_ItemCommand(object sender, GridCommandEventArgs e)
		{
			try
			{
				switch(e.CommandName)
				{
					case @"Refrescar":
						this.RadGridMaster.Rebind();
						break;
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		protected void Button1_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (GridDataItem item in this.RadGridMaster.Items)
				{
					if (item.Selected)
					{
						this.presentador.actualizarBuzonChatHC1(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdCaso"].ToString());
					}
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		protected void RadGridMaster_ItemDataBound(object sender, GridItemEventArgs e)
		{
			if (e.Item is GridDataItem)
			{
				GridDataItem item = (GridDataItem)e.Item;
				Image img = (Image)e.Item.FindControl("imgChat");
				if (img.ImageUrl == "")
				{
					img.ImageUrl = "~/Imagenes/NoImage.png";
				}
			}
		}
		
		///<summary>Evento de comando que se dispara cuando se hace selecciona un elemento del combo ddlSuscriptor.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void ddlSuscriptor_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.ddlSuscriptor.SelectedValue))
				{
					this.presentador.LlenarComboServicios(int.Parse(this.ddlSuscriptor.SelectedValue));
					this.ddlServicio.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		protected void Limpiar(object sender, EventArgs e)
		{
			this.ddlSuscriptor.ClearSelection();
			this.ddlServicio.ClearSelection();
			this.ddlServicio.Enabled = false;
			this.ddlFiltro.ClearSelection();
			this.txtAsegurado.Text = "";
			this.txtIntermediario.Text = "";
			this.txtFiltro.Text = "";
			this.txtFechaDesde.Clear();
			this.txtFechaHasta.Clear();
			this.txtFiltro.Enabled = false;
			this.RadGridMaster.DataSource = new string[] { };
			this.NumeroDeRegistros = 0;
			this.NumeroPagina = 1;
			this.RadGridMaster.DataBind();
		}
		
		protected void ddlFiltro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.txtFiltro.Text = "";
			this.txtFiltro.Enabled = true;
		}

		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		
		public string TipoFiltro
		{
			get
			{
				if (!string.IsNullOrEmpty(this.ddlFiltro.SelectedValue))
				{
					return this.ddlFiltro.SelectedValue.ToString();
				}
				else
				{
					return "";
				}
			}
		}
		
		public string Filtro
		{
			get
			{
				return this.txtFiltro.Text;
			}
		}
		
		public string Suscriptor
		{
			get
			{
				if (!string.IsNullOrEmpty(this.ddlSuscriptor.SelectedValue))
				{
					return this.ddlSuscriptor.SelectedValue;
				}
				else
				{
					return "";
				}
			}
		}
		
		public string Asegurado
		{
			get
			{
				return this.txtAsegurado.Text;
			}
		}
		
		public string Intermediario
		{
			get
			{
				return this.txtIntermediario.Text;
			}
		}
		
		public int idIntermediario
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Intermediario))
				{
					this.presentador.ObtenerIdSuscriptor(this.Intermediario);
				}
				return this.IdIntermediario;
			}
			set
			{
				this.IdIntermediario = value;
			}
		}
		
		public string Servicio
		{
			get
			{
				return this.ddlServicio.SelectedValue;
			}
		}
		
		public DataTable ComboSuscriptores
		{
			set
			{
				this.ddlSuscriptor.DataSource = value;
				this.ddlSuscriptor.DataBind();
			}
		}
		
		public IEnumerable<FlujosServicioDTO> ComboServicios
		{
			set
			{
				this.ddlServicio.DataSource = value;
				this.ddlServicio.DataBind();
			}
		}
		
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable<PoteCasoDTO> Datos
		{
			set
			{
				this.RadGridMaster.DataSource = value;
			}
		}
		
		public string FechaDesde
		{
			get
			{
				return this.txtFechaDesde.SelectedDate.ToString();
			}
		}
		
		public string FechaHasta
		{
			get
			{
				return this.txtFechaHasta.SelectedDate.ToString();
			}
		}
		
		///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
		public int NumeroDeRegistros
		{
			get
			{
				return this.RadGridMaster.VirtualItemCount;
			}
			set
			{
				this.RadGridMaster.VirtualItemCount = value;
			}
		}
		
		public string urlCartaAval
		{
			get
			{
				return this.presentador.BuscaUrlCasosExternos("CA_HC1");
			}
		}
		
		public string urlClave
		{
			get
			{
				return this.presentador.BuscaUrlCasosExternos("CE_HC1");
			}
		}
		
		public string ServicioCartaAval
		{
			get
			{
				return this.presentador.BuscarValorServicio(int.Parse(ConfigurationManager.AppSettings[@"ListaValorServicioCartaAval"].ToString()));
			}
		}
		
		public string ServicioClaveEmergencia
		{
			get
			{
				return this.presentador.BuscarValorServicio(int.Parse(ConfigurationManager.AppSettings[@"ListaValorServicioClaveEmergencia"].ToString()));
			}
		}
		
		public bool BIndSimulado { get; set; }

		#endregion "Propiedades de Presentación"

	}
}