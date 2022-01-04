using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Gat
{
	///<summary>Clase ConsultaCasos.</summary>
	public partial class ConsultaCasosAuditoria : PaginaMaestroDetalleBase, IConsultaCasosAuditoria
	{
		#region "Variables Locales"
		
		///<summary>Variable presentador ConsultaCasosPresentador.</summary>
		ConsultaCasosAuditoriaPresentador presentador;
		
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
				this.presentador = new ConsultaCasosAuditoriaPresentador(this);
				this.Orden = "Id";
				this.txtFiltro.Enabled = false;
				
				if (this.UsuarioActual.SuscriptorSeleccionado.Nombre.ToUpper() == "NUBISE")
				{
					this.ddlSuscriptor.SelectedValue = this.UsuarioActual.SuscriptorSeleccionado.Id.ToString();
					this.presentador.LlenarComboServicios(int.Parse(this.ddlSuscriptor.SelectedValue));
					
					this.ddlServicio.Enabled = true;
					
				}
				else
				{
					this.ddlSuscriptor.SelectedValue = this.UsuarioActual.SuscriptorSeleccionado.Id.ToString();
					this.ddlSuscriptor.Enabled = false;
					this.presentador.LlenarSuscriptor(this.UsuarioActual.SuscriptorSeleccionado.Id);
				}
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

				//this.txtFechaDesde.MaxDate = DateTime.Now;
				//this.txtFechaHasta.MaxDate = DateTime.Now;
				
				if (!this.Page.IsPostBack)
				{
					this.CultureDatePicker();
					
					if (this.UsuarioActual.SuscriptorSeleccionado.Nombre.ToUpper() == "NUBISE")
					{
						this.presentador.BuscarTodosSuscriptores();
						this.presentador.LlenarComboServicios(this.IdComboSuscriptores);
					}
					else 
					{
						this.presentador.LlenarSuscriptor(this.UsuarioActual.SuscriptorSeleccionado.Id);
						//this.presentador.LlenarComboServicios(this.IdComboSuscriptores);
					}
					this.RadGridMaster.Rebind();
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
		protected void CmdBuscar_Click(object sender, EventArgs e)
		{
			try
			{
				if ((!string.IsNullOrWhiteSpace(this.ddlServicio.SelectedValue) || (this.ddlServicio.SelectedIndex > -1)) || (this.IdComboServicios > 0))
				{
					if ((this.txtFechaDesde.SelectedDate != null) && (this.txtFechaHasta.SelectedDate != null))
					{
						DateTime fecha = DateTime.Parse(this.FechaDesde);
						DateTime fecha1 = DateTime.Parse(this.FechaHasta);
						
						if (fecha > fecha1)
						{
							this.Singleton.RadAlert("La Fecha Desde no puede ser mayor a la Fecha Hasta", 380, 50, "Advertencia", "");
							this.RadGridMaster.DataSource = new string[] { };
							this.RadGridMaster.DataBind();
						}
						else
						{
							if (fecha > DateTime.Today)
							{
								this.Singleton.RadAlert("la Fecha Desde no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
								this.RadGridMaster.DataSource = new string[] { };
								this.RadGridMaster.DataBind();
							}
							else 
							{
								if (fecha1 > DateTime.Today)
								{
									this.Singleton.RadAlert("La Fecha Hasta no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
									this.RadGridMaster.DataSource = new string[] { };
									this.RadGridMaster.DataBind();
								}
								else
								{
									this.NumeroPagina = 1;
									this.RadGridMaster.Rebind();
								}
							}
						}
						int cuantosDias;
						TimeSpan diferencia;
						diferencia = fecha1.Date - fecha.Date;
						cuantosDias = diferencia.Days;

						if (cuantosDias > 365)
						{
							this.Singleton.RadAlert("El Rango de Búsqueda no puede ser mayor a un (1) año", 380, 50, "Advertencia", "");
							this.RadGridMaster.DataSource = new string[] { };
							this.RadGridMaster.DataBind();
						}
						else
						{
							this.NumeroPagina = 1;
							this.RadGridMaster.Rebind();
						}
					}
					else
					{
						if (this.txtFechaDesde.SelectedDate != null)
						{
							DateTime fecha = DateTime.Parse(this.FechaDesde);
							if ((fecha > DateTime.Today) && (this.txtFechaHasta.SelectedDate == null))
							{
								this.Singleton.RadAlert("La Fecha Desde no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
								this.RadGridMaster.DataSource = new string[] { };
								this.RadGridMaster.DataBind();
							}
							else
							{
								if (this.txtFechaHasta.SelectedDate != null)
								{
									DateTime fecha1 = DateTime.Parse(this.FechaHasta);
									if (fecha1 > DateTime.Today)
									{
										this.Singleton.RadAlert("La Fecha Hasta no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
										this.RadGridMaster.DataSource = new string[] { };
										this.RadGridMaster.DataBind();
									}
									else
									{
										this.NumeroPagina = 1;
										this.RadGridMaster.Rebind();
									}
								}
								else
								{
									this.txtFechaHasta.SelectedDate = DateTime.Now;
								}
							}
						}
						else
						{
							if (this.txtFechaHasta.SelectedDate != null)
							{
								
								DateTime fecha1 = DateTime.Parse(this.FechaHasta);
								if (fecha1 > DateTime.Today)
								{
									this.Singleton.RadAlert("La Fecha Hasta no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
									this.RadGridMaster.DataSource = new string[] { };
									this.RadGridMaster.DataBind();
								}
								else
								{
									this.NumeroPagina = 1;
									this.RadGridMaster.Rebind();
								}
							}
							else if (this.txtFechaDesde.SelectedDate == null)
							{
								this.Singleton.RadAlert("Las Fecha no pueden estar vacia", 380, 50, "Advertencia", "");
								this.RadGridMaster.DataSource = new string[] { };
								this.RadGridMaster.DataBind();
							}
						}
					}
					this.NumeroPagina = 1;
					this.RadGridMaster.Rebind();
				}
				else
				{
					this.Singleton.RadAlert("Debe seleccionar el servicio", 380, 50, "Advertencia", "");
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
				this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro, this.IdComboSuscriptores, this.IdComboServicios);
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				this.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{
			
			
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
				//this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
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
					case @"Refrescar":
						this.RadGridMaster.Rebind();
						break;

					case @"VerDet":
						int idcaso = 0;
						string idc = string.Empty;

						if (this.RadGridMaster.SelectedItems.Count > 0)
						{
							idcaso = int.Parse(((GridDataItem)this.RadGridMaster.SelectedItems[0])["Id"].Text);

							if (idcaso == 0)
							{
								Singleton.RadAlert("El Ítem seleccionado,no posee un caso registrado", 380, 50, "Mensaje", string.Empty);
							}
							else
							{							
								idc = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(idcaso.ToString().Encriptar()));
								this.Response.Redirect("~/Modulos/Tracking/CasoDetalle.aspx?IdMenu=" + this.IdMenuEncriptado + "&accion=" + AccionVer + "&id=" + idc, true);
							}
						}
						else
						{
							ScriptManager.RegisterStartupScript(this, this.GetType(), "radalert", "radalert('Seleccione el registro a mostrar', 380, 50, 'Ver detalle de Registro')", true);
						}
						break;

					case @"Reactivar":
						int id = 0;
						int idSolicitud = 0;
						int idMovimiento = 0;
						
						if (this.RadGridMaster.SelectedItems.Count > 0)
						{
									id = int.Parse(((GridDataItem)this.RadGridMaster.SelectedItems[0])["Id"].Text);
									idSolicitud = int.Parse(((GridDataItem)this.RadGridMaster.SelectedItems[0])["IdSolicitud"].Text);
									idMovimiento = int.Parse(((GridDataItem)this.RadGridMaster.SelectedItems[0])["IdMovimiento"].Text);
									this.presentador.ReactivarEstatus(id, idSolicitud, idMovimiento, this.UsuarioActual.Id, int.Parse(this.ddlSuscriptor.SelectedValue));
									this.RadGridMaster.Rebind();
						}
						else
						{
							ScriptManager.RegisterStartupScript(this, this.GetType(), "radalert", "radalert('Seleccione el registro a mostrar', 380, 50, 'Ver detalle de Registro')", true);
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
		
		///<summary>Evento de comando que se dispara cuando se hace selecciona un elemento del combo ddlSuscriptor.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void DdlSuscriptor_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(this.ddlSuscriptor.SelectedValue))
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
		
		protected void DdlFiltro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.txtFiltro.Text = string.Empty;
			this.txtFiltro.Enabled = true;
		}
		
		protected void CmdLimpiar_Click(object sender, EventArgs e)
		{
			this.ddlSuscriptor.ClearSelection();
			this.ddlServicio.ClearSelection();
			this.ddlServicio.Enabled = false;
			this.ddlFiltro.ClearSelection();
			this.txtFiltro.Text = string.Empty;
			this.txtFechaDesde.Clear();
			this.txtFechaHasta.Clear();
			this.txtFiltro.Enabled = false;
			//this.RadFilterMaster.DataSource = new string[] { };
			this.RadGridMaster.DataSource = new string[] { };
			this.NumeroDeRegistros = 0;
			this.NumeroPagina = 1;
			//this.RadFilterMaster.DataBind();
			this.RadGridMaster.DataBind();
		}
		
		#endregion "Eventos de la Página"
		
		#region "Propiedades de Presentación"
		
		public IEnumerable<SuscriptorDTO> ComboSuscriptores
		{
			set
			{
				this.ddlSuscriptor.DataSource = value;
				this.ddlSuscriptor.DataBind();
			}
		}
		
		public int IdComboSuscriptores
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.ddlSuscriptor.SelectedValue))
				{
					return int.Parse(this.ddlSuscriptor.SelectedValue);
				}
				else
				{
					return this.UsuarioActual.SuscriptorSeleccionado.Id;
				}
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
		
		public int IdComboServicios
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.ddlServicio.SelectedValue))
				{
					return int.Parse(this.ddlServicio.SelectedValue);
				}
				else
				{
					return 0;
				}
			}
		}
		
		public string TipoFiltro
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.ddlFiltro.SelectedValue))
				{
					return this.ddlFiltro.SelectedValue.ToString();
				}
				else
				{
					return string.Empty;
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
		
		public string FechaDesde
		{
			get
			{
				if (this.txtFechaDesde.SelectedDate != null)
				{
					return this.txtFechaDesde.SelectedDate.Value.ToShortDateString();
				}
				else
				{
					return string.Empty;
				}
			}
		}
		
		public string FechaHasta
		{
			get
			{
				if (this.txtFechaDesde.SelectedDate != null)
				{
					return this.txtFechaHasta.SelectedDate.Value.ToShortDateString();
				}
				else
				{
					return string.Empty;
				}
			}
		}		
		
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

		//public int Id
		//{
		//	get
		//	{
		//		return base.Id;
		//	}
		//	set
		//	{
		//		base.Id = value;
		//	}
		//}

		//public string IdCaso
		//{
		//	get
		//	{
		//		return HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar()));
		//	}
		//}

		//public int IdCaso
		//{
		//	get
		//	{
		//		if (this.IdCaso == 0)
		//		{
		//			string sIdCaso = this.ExtraerDeViewStateOQueryString("IdCaso");
		//			if (sIdCaso != "")
		//				this.IdCaso = Convert.ToInt32(sIdCaso);
		//		}
		//		return this.IdCaso;
		//	}
		//	set
		//	{
		//		this.IdCaso = Convert.ToInt32(value);
		//		this.ViewState["IdCaso"] = this.IdCaso;
		//	}
		//}
		#endregion "Propiedades de Presentación"
	}

}