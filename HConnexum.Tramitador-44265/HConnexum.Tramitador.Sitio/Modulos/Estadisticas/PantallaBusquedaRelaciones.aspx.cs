using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class PantallaBusquedaRelaciones : PaginaMaestroDetalleBase, IBusquedaRelaciones
	{
		#region "Variables Locales"
		
		///<summary>Variable presentador Búsqueda de Relaciones.</summary>
		PresentadorBusquedaRelaciones presentador;
		string nRemesa;
		int idIntermediario;
		int idCodexterno;
		string conexionString;
		string estatus = string.Empty;
		
		#endregion "Variables Locales"
		
		#region "Propiedades de Presentación"
		
		#region Url para doble click
		
		public string IdInterEncriptado
		{
			get
			{
				if (this.ViewState[@"idIntermediario"] != null)
					return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.ViewState[@"idIntermediario"].ToString().Encriptar()));
				
				return string.Empty;
			}
		}
		
		public string IdProveeEncriptado
		{
			get
			{
				if (this.ViewState[@"idProveedor"] != null)
					return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.ViewState[@"idProveedor"].ToString().Encriptar()));
				
				return string.Empty;
			}
		}
		
		public string IdCodExternoEncriptado
		{
			get
			{
				if (this.ViewState[@"IdCodExterno"] != null)
					return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.ViewState[@"IdCodExterno"].ToString().Encriptar()));
				
				return string.Empty;
			}
		}
		
		#endregion
		
		public enum CodTipoProveedor
		{
			Aseguradora = 5
		}

		public enum IdSuscriptotGrupoPronto
		{
			IdSuscriptotGrupoPronto = 15
		}
		
		public DataTable ComboIntermediario
		{
			set
			{
				this.ddlIntermediarios.DataSource = value;
				this.ddlIntermediarios.DataBind();
			}
		}
		
		public int IdIntermediario
		{
			get
			{
				if (this.ViewState[@"idIntermediario"] != null)
					return Convert.ToInt32(this.ViewState[@"idIntermediario"]);
				
				return 0;
			}
		}
		
		public string Intermediario
		{
			get
			{
				return this.ddlIntermediarios.SelectedItem.Text;
			}
		}
		
		public DataTable ComboTipoProveedor
		{
			set
			{
				this.ddlTipoProveedor.DataSource = value;
				this.ddlTipoProveedor.DataBind();
			}
		}
		
		public DataTable ComboProveedor
		{
			set
			{
				this.ddlProveedor.DataSource = value;
				this.ddlProveedor.DataBind();
			}
		}
		
		public string TipoProveedor
		{
			get
			{
				return this.ddlTipoProveedor.SelectedItem.Text;
			}
		}
		
		public string IdTipoProveedor
		{
			get
			{
				return this.ddlTipoProveedor.SelectedItem.Value;
			}
		}
		
		public int IdSuscriptor
		{
			get
			{
				return int.Parse(this.UsuarioActual.SuscriptorSeleccionado.Id.ToString());
			}
		}
		
		public string IdProveedor
		{
			get
			{
				if (this.ddlProveedor.SelectedIndex >= 0)
					return this.ddlProveedor.SelectedItem.Value;
				else
					return this.UsuarioActual.Sucursal.CodIdExterno.ToString();
			}
		}
		
		public string RadioButtonEstatus
		{
			get
			{
				return this.RadioButtonGroup.SelectedItem.Value;
			}
		}
		
		public DateTime? FechaInicial
		{
			get
			{
				if (this.txtFechaInicial.SelectedDate != null)
					return this.txtFechaInicial.SelectedDate.Value;
				else
					return null;
			}
		}
		
		public DateTime? FechaFinal
		{
			get
			{
				if (this.txtFechaFinal.SelectedDate != null)
					return this.txtFechaFinal.SelectedDate.Value;
				else
					return null;
			}
		}
		
		public int? NumeroRelacion
		{
			get
			{
				if (!string.IsNullOrEmpty(this.BusquedaXNumeroRelacion.Text))
					return int.Parse(this.BusquedaXNumeroRelacion.Text);
				else
					return null;
			}
		}
		
		public string NumeroFactura
		{
			get
			{
				if (!string.IsNullOrEmpty(this.BusquedaXNumeroFactura.Text))
					return this.BusquedaXNumeroFactura.Text;
				else
					return null;
			}
		}
		
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
		
		public string Relaciones
		{
			set
			{
				this.relaciones.Text = value;
			}
		}
		
		public int Casos
		{
			set
			{
				this.casos.Text = value.ToString();
			}
		}
		
		public string MontoCubierto
		{
			set
			{
				this.montocubierto.Text = value;
			}
		}
		
		public string Retenido
		{
			set
			{
				this.retenido.Text = value;
			}
		}
		
		public string ImpMunicipal
		{
			set
			{
				this.impmunicipal.Text = value;
			}
		}
		
		public string MontoTotal
		{
			set
			{
				this.montototal.Text = value;
			}
		}
		
		public string ConexionString
		{
			get
			{
				if (this.ViewState[@"ConexionString"] != null)
					return this.ViewState[@"ConexionString"].ToString();
				
				return string.Empty;
			}
		}
		
		public int IdCodExterno
		{
			get
			{
				if (this.ViewState[@"IdCodExterno"] != null)
					return Convert.ToInt32(this.ViewState[@"IdCodExterno"]);
				
				return 0;
			}
		}
		
		public bool ImprimirVisible
		{
			set
			{
				this.ButtonImprimir.Visible = value;
			}
		}
		
		public bool Totales
		{
			set
			{
				this.PanelTotales.Visible = value;
			}
		}
		
		#endregion "Propiedades de Presentación"
		
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new PresentadorBusquedaRelaciones(this);
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
					string idsuscriptor = this.UsuarioActual.SuscriptorSeleccionado.Id.ToString();
					bool habilitarProveedor = presentador.LlenarComboProveedor(idsuscriptor);
					this.presentador.LlenarComboTipoProveedor();
					if (habilitarProveedor)
					{
						ddlProveedor.Enabled = true;
						ddlProveedor.Visible = true;
						lblProveedor.Visible = true;
						RFVddlProveedor.Enabled = true;
						ddlTipoProveedor.Enabled = false;
						
						if ((this.UsuarioActual.SuscriptorSeleccionado.Nombre  !="Administración Grupo Pronto, S.A.."))
						{
							this.presentador.LlenarCombodelIntermediario(5, this.IdSuscriptor);
							this.ddlTipoProveedor.Enabled = true;
							for (int index=0; index < this.ddlTipoProveedor.Items.Count(); index++)
							{
								this.ddlTipoProveedor.SelectedIndex = index;
								if (this.ddlTipoProveedor.SelectedValue == "5")
									index = this.ddlTipoProveedor.Items.Count();
							}
							this.ddlTipoProveedor.Enabled = false;
							this.ddlIntermediarios.SelectedIndex = 0;
							this.ViewState["ConexionString"] = this.presentador.ObtenerConexionString(int.Parse(this.ddlIntermediarios.SelectedItem.Value));
							this.ViewState["IdCodExterno"] = this.presentador.ObtenerIdCodExterno(int.Parse(this.ddlIntermediarios.SelectedItem.Value));
							this.ViewState["idIntermediario"] = int.Parse(this.ddlIntermediarios.SelectedItem.Value);
						}
						else
							this.presentador.LlenarComboIntermediario(this.IdSuscriptor);
					}
					else
					{
						ddlProveedor.Enabled = false;
						ddlProveedor.Visible = false;
						lblProveedor.Visible = false;
						RFVddlProveedor.Enabled = false;
						ddlTipoProveedor.Enabled = true;
					}
					this.NumeroPagina = 1;
                    
                    this.txtFechaInicial.SelectedDate = null;
                    this.txtFechaFinal.SelectedDate = null;
				}

                if (RadioButtonList1.Items.Count>0)
                    if (RadioButtonList1.Items[0].Selected)
                    {
                        txtFechaInicial.Enabled = true;
                        txtFechaFinal.Enabled = true;
                    }

                this.RFVtxtFechaFinal.Enabled = txtFechaInicial.Enabled && txtFechaFinal.Enabled;
				this.RadFilterMaster.Culture = Thread.CurrentThread.CurrentCulture;
				this.RadFilterMaster.RecreateControl();
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
			
			if (filtro != null)
				if (this.RadFilterMaster.RootGroup.Expressions.Count > 0)
					filtro.CssClass = @"rtbTextNeg";
		}
		
		///<summary>Evento del radgrid que se dispara para obtener el valor del objeto datakey del la fila selecionada.</summary>
		///<param name="dataKey">Referencia al nombre del Objeto DataKey.</param>
		///<returns>Valor de tipo string que contenido en el Objeto DataKey</returns>
		protected string ObtenerDataKeyValue(string dataKey)
		{
			string datakeyvalue = this.RadGridMaster.SelectedValues[dataKey].ToString();
			return datakeyvalue;
		}
		
		protected void ButtonBuscar_Click(object sender, EventArgs e)
		{
            //if (txtFechaInicial.SelectedDate != null)
            //{
            //    SelectedDate.Text = txtFechaInicial.SelectedDate.ToString();
            //}
            //else
            //{
            //    txtFechaInicial.Text = "null";
            //}


			if ((this.RadioButtonGroup.SelectedIndex >= 0) || (this.RadioButtonList1.SelectedIndex >= 0) || !string.IsNullOrWhiteSpace(this.BusquedaXNumeroRelacion.Text) || !string.IsNullOrWhiteSpace(this.BusquedaXNumeroFactura.Text))
			{
				long lnumeroRelacion = 0;
				if (!string.IsNullOrEmpty(this.BusquedaXNumeroRelacion.Text))
					lnumeroRelacion = long.Parse(this.BusquedaXNumeroRelacion.Text);
				
				if (lnumeroRelacion >= Int32.MinValue && lnumeroRelacion <= Int32.MaxValue)
				{

					this.PanelGrid.Visible = true;
					this.NumeroPagina = 1;
					this.RadGridMaster.Rebind();

				}
				else
					Singleton.RadAlert("El número de Relación no puede ser mayor de " + Int32.MaxValue.ToString(), 380, 50, "Advertencia", "");
				
				
			}
			else
				Singleton.RadAlert("Debe seleccionar Búsqueda por Estatus o Búsqueda de Número de Relación/Factura", 380, 50, "Advertencia", "");
		}
		
		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
				if (this.RadioButtonGroup.SelectedIndex >= 0)
					this.estatus = this.RadioButtonGroup.SelectedItem.Value;
				else if ((this.RadioButtonList1.SelectedIndex >= 0))
					this.estatus = this.RadioButtonList1.SelectedItem.Value;
				
				if (this.UsuarioActual.SuscriptorSeleccionado.CodIdExterno == string.Empty)
					this.UsuarioActual.SuscriptorSeleccionado.CodIdExterno = "0";
				
				string idCodigoExternoSucursal = this.ObtenerCodExterno();
				
				this.RadGridMaster.DataSource = this.presentador.LlenarGridDetalleRemesa(Convert.ToInt32(idCodigoExternoSucursal),
					this.ConexionString, WebConfigurationManager.AppSettings[@"StoredProceduresDetalleRemesas"],
					this.estatus == null ? string.Empty : this.estatus,
					this.FechaInicial, this.FechaFinal, this.NumeroRelacion, 1, this.ParametrosFiltro, this.NumeroPagina, this.TamanoPagina, this.NumeroFactura);
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
		
		protected void DdlTipoProveedorSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.IdTipoProveedor))
			{
				this.ddlIntermediarios.ClearSelection();
				this.ddlIntermediarios.Items.Clear();
				this.ddlIntermediarios.Enabled = false;
			}
			else
			{
				this.ddlIntermediarios.ClearSelection();
				this.ddlIntermediarios.Items.Clear();
				
				if ((this.UsuarioActual.SuscriptorSeleccionado.Nombre == "Administración Grupo Pronto, S.A.."))
					this.presentador.LlenarComboIntermediario(Convert.ToInt32(this.IdTipoProveedor));
				else
				{
					this.presentador.LlenarComboIntermediario(Convert.ToInt32(this.IdTipoProveedor));
					this.ddlProveedor.Enabled = false;
				}
				this.ddlIntermediarios.Enabled = true;
			}
		}
		
		protected void DdlProveedorSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.IdProveedor))
			{
				if (this.UsuarioActual.SuscriptorSeleccionado.Nombre == "Administración Grupo Pronto, S.A..")
				{
					this.ddlIntermediarios.Enabled = false;
					this.ddlTipoProveedor.Enabled = false;
					this.ddlTipoProveedor.ClearSelection();
					this.ddlIntermediarios.ClearSelection();
					this.PanelGrid.Visible = false;
					this.PanelTotales.Visible = false;
					this.ButtonImprimir.Visible = false;
				}
				else
				{
					this.PanelGrid.Visible = false;
					this.PanelTotales.Visible = false;
					this.ButtonImprimir.Visible = false;
				}
			}
			else
			{
				if (this.UsuarioActual.SuscriptorSeleccionado.Nombre == "Administración Grupo Pronto, S.A..")
				{
					this.ddlTipoProveedor.ClearSelection();
					this.ddlIntermediarios.ClearSelection();
					this.ddlIntermediarios.Enabled = false;
					this.ddlTipoProveedor.Enabled = true;
					this.PanelGrid.Visible = false;
					this.PanelTotales.Visible = false;
					this.ButtonImprimir.Visible = false;
				}
				else
				{
					this.PanelGrid.Visible = false;
					this.PanelTotales.Visible = false;
					this.ButtonImprimir.Visible = false;
				}
			}
		}
		
		public string ObtenerCodExterno()
		{
            if (this.ddlProveedor.SelectedIndex >= 0)
            {
                int idCodExternoSucursalext = this.presentador.ObtenerIdCodExterno(Convert.ToInt32(this.IdProveedor));
                this.ViewState["idProveedor"] = idCodExternoSucursalext;
                return idCodExternoSucursalext.ToString();
            }
            else
            {
                int idCodExternoSucursalext = Convert.ToInt32(this.IdProveedor);
                this.ViewState["idProveedor"] = idCodExternoSucursalext;
                return idCodExternoSucursalext.ToString();
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
		}
		
		///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
		{
			this.NumeroPagina = e.NewPageIndex + 1;
			this.RadGridMaster.Rebind();
		}
		
		///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
		{
			if (e != null)
			{
				this.Orden = string.Format("{0}{1}", e.SortExpression, e.NewSortOrder == GridSortOrder.Descending ? " DESC" : " ASC");
				this.RadGridMaster.Rebind();
			}
		}
		
		///<summary>Evento del rad filter del grid que se dispara cuando se agrega o modifica una busqueda.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected void RadFilterMaster_ItemCommand(object sender, RadFilterCommandEventArgs e)
		{
			if (e != null)
			{
				switch (e.CommandName)
				{
					case "RemoveExpression":
						break;
					case "AddGroup":
						this.LblMessege.Text = "La Opcion de Agregar Grupo no esta Disponible y no ejecuta ninguna acción";
						break;
					case "AddExpression":
						this.LblMessege.Text = string.Empty;
						break;
				}
			}
		}
		
		///<summary>Evento del rad filter del grid que se dispara cuando se hace click en aceptar en los filtros.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void ApplyButton_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				this.NumeroPagina = 1;
				this.ParametrosFiltro = this.ExtraerParametrosFiltro(this.RadFilterMaster);
				this.RadGridMaster.Rebind();
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
		protected void LevantarPantallaDetallerelacion()
		{
			string idCodExternoUsuarioActual = this.ObtenerCodExterno();
			StringBuilder value = new StringBuilder();
			value.Append(@"~/Modulos/Estadisticas/PantallaDetalleRelacion.aspx?IdMenu=");
			value.AppendFormat("{0}&intermediario=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.IdMenu.ToString().Encriptar())));
			value.AppendFormat("{0}&nremesa=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.IdIntermediario.ToString().Encriptar())));
			value.AppendFormat("{0}&idcodexterusuact=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.ObtenerDataKeyValue("RelacionReclamo").Encriptar())));
			value.AppendFormat("{0}&idcodexterno=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(idCodExternoUsuarioActual.ToString().Encriptar())));
			value.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.IdCodExterno.ToString().Encriptar())));
			this.Response.Redirect(value.ToString(), false);
		}
		
		///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_ItemCommand(object sender, GridCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Refrescar":
						this.RadGridMaster.Rebind();
						break;
					case "PostBack":
						if (this.RadGridMaster.SelectedItems.Count > 0)
							this.LevantarPantallaDetallerelacion();
						else
							ScriptManager.RegisterStartupScript(this, this.GetType(), "radalert", "radalert('Seleccione el registro a mostrar', 380, 50, 'Ver detalle de Registro')", true);
						break;
				}
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
		
		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{
		}
		
		protected void DdlIntermediariosSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.ViewState["ConexionString"] = this.presentador.ObtenerConexionString(int.Parse(this.ddlIntermediarios.SelectedItem.Value));
			this.ViewState["IdCodExterno"] = this.presentador.ObtenerIdCodExterno(int.Parse(this.ddlIntermediarios.SelectedItem.Value));
			this.ViewState["idIntermediario"] = int.Parse(this.ddlIntermediarios.SelectedItem.Value);

			int codigoExterno = presentador.ObtenerIdCodExterno(int.Parse(ddlIntermediarios.SelectedItem.Value));

			if (codigoExterno == 3024473)
			{
				this.Singleton.RadAlert("Esta página contiene solo pagos emitidos a partir del 01/04/2014, para pagos anteriores a la fecha debe ingresar a través de <a href=http://www.previsora.com target=_blank>www.previsora.com</a>", 380, 50, "Información", "");
			}
		}
		
		protected void ButtonImprimir_Click(object sender, EventArgs e)
		{
			string idCodExternoUsuarioActual = this.ObtenerCodExterno();
			
			if (this.RadioButtonGroup.SelectedIndex >= 0)
				this.estatus = this.RadioButtonGroup.SelectedItem.Value.ToString().Encriptar();
			else if ((this.RadioButtonList1.SelectedIndex >= 0))
				this.estatus = this.RadioButtonList1.SelectedItem.Value.ToString().Encriptar();
			
			StringBuilder url = new StringBuilder();
			url.Append(@"~/Modulos/Estadisticas/ReporteRelaciones.aspx?IdMenu=");
			url.AppendFormat("{0}&intermediario=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.IdMenu.ToString().Encriptar())));
			url.AppendFormat("{0}&estatus=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.IdIntermediario.ToString().Encriptar())));
			url.AppendFormat("{0}&fechainicial=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.estatus)));
			url.AppendFormat("{0}&fechafinal=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.FechaInicial.ToString().Encriptar())));
			url.AppendFormat("{0}&tipoproveedor=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.FechaFinal.ToString().Encriptar())));
			url.AppendFormat("{0}&compañia=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.TipoProveedor.ToString().Encriptar())));
			url.AppendFormat("{0}&idcodexterusuact=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.Intermediario.ToString().Encriptar())));
			url.AppendFormat("{0}&idcodexterno=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(idCodExternoUsuarioActual.ToString().Encriptar())));
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.IdCodExterno.ToString().Encriptar())));
			this.Response.Redirect(url.ToString());
		}
	}
}
