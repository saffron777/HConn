using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Reportes
{
	public partial class ReimpresionComprobanteMovimientos : PaginaMaestroDetalleBase, IReimpresionComprobanteMovimiento
	{
		
		#region "Variables Locales"
		
		///<summary>Variable presentador ConsultaCasosPresentador.</summary>
		PresentadorReimpresionComprobanteMovimiento presentador;

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
				this.presentador = new PresentadorReimpresionComprobanteMovimiento(this);
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
					this.CultureDatePicker();
					this.RadGridMaster.Rebind();
				}
				this.Master.FindControl("rsmMigas").Visible = false;
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
		}
		
		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
				this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro);
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
					case @"imprimir":
						int IdSusIntermediario = 0;
						
						StringBuilder url = new StringBuilder();
						foreach (GridDataItem item in this.RadGridMaster.Items)
						{
							if (item.Selected)
							{
								string NomTipoServicio = item.OwnerTableView.DataKeyValues[item.ItemIndex]["NombreServicio"].ToString();
								if (!string.IsNullOrEmpty(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdSusIntermedario"].ToString()))
								{
									IdSusIntermediario = int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdSusIntermedario"].ToString());
								}
								string NomTipoMov = item.OwnerTableView.DataKeyValues[item.ItemIndex]["TipoMovimiento"].ToString();
								string IdExpedienteWeb = item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdCasoexterno2"].ToString();
								
								if (NomTipoServicio == "Clave")
								{
									url.Append(@"~/Modulos/Reportes/ComprobanteMovimientoCE.aspx?id=");
									url.AppendFormat("{0}&seguro=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdExpedienteWeb.Encriptar())));
									url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdSusIntermediario.ToString().Encriptar())));
									this.Response.Redirect(url.ToString(), false);
								}
								else if (NomTipoServicio == "Carta")
								{
									if (NomTipoMov != string.Empty)
									{
										string textoOriginal = NomTipoMov;
										string textoNormalizado = textoOriginal.Normalize(NormalizationForm.FormD);
										Regex reg = new Regex("[^a-zA-Z0-9 ]");
										string textoSinAcentos = reg.Replace(textoNormalizado, "");
										
										url.Append(@"~/Modulos/Reportes/ComprobanteMovimientoCA.aspx?idexpweb=");
										url.AppendFormat("{0}&nomtipomov=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdExpedienteWeb.Encriptar())));
										url.AppendFormat("{0}&idsusintermediario=", HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(textoSinAcentos.Encriptar())));
										url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdSusIntermediario.ToString().Encriptar())));
										this.Response.Redirect(url.ToString(), false);
									}
									else
									{
										this.Errores = "<b>El caso no posee último movimiento</b>";
										this.rbdReimpresion.VisibleOnPageLoad = false;
									}
								}
							}
						}
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
		
		protected void ddlFiltro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.txtFiltro.Text = "";
			this.txtFiltro.Enabled = true;
		}
		
		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{
		}
		
		protected void cmdLimpiar_Click(object sender, EventArgs e)
		{
			this.txtAsegurado.Text = string.Empty;
			this.ddlFiltro.ClearSelection();
			this.txtFiltro.Enabled = false;
			this.txtFiltro.Text = string.Empty;
			this.txtFechaDesde.Clear();
			this.txtFechaHasta.Clear();
			this.txtIntermediario.Text = string.Empty;
			this.RadGridMaster.DataSource = new string[] { };
			this.NumeroDeRegistros = 0;
			this.NumeroPagina = 1;
			this.RadGridMaster.DataBind();
		}

		#endregion "Eventos de la Página"
		
		#region "Propiedades de Presentación"
		
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable<CasoDTO> Datos
		{
			set
			{
				this.RadGridMaster.DataSource = value;
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
		
		///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
		public string Asegurado
		{
			get
			{
				return this.txtAsegurado.Text;
			}
			set
			{
				this.txtAsegurado.Text = value;
			}
		}
		
		public string Intermediario
		{
			get
			{
				return this.txtIntermediario.Text;
			}
			set
			{
				this.txtIntermediario.Text = value;
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
		
		public string NomTipoServicio { get; set; }
		
		public string IdExpedienteWeb { get; set; }
		
		public int IdSusIntermediario { get; set; }
		
		public string NomTipoMov { get; set; }

		#endregion "Propiedades de Presentación"

	}
}