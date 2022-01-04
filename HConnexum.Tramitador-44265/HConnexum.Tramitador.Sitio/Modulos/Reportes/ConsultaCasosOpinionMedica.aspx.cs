using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
	///<summary>Clase ConsultaCasos.</summary>
    public partial class ConsultaCasosOpinionMedica : PaginaMaestroDetalleBase, IConsultaCasosOpinionMedica
	{
		#region "Variables Locales"
		///<summary>Variable presentador ConsultaCasosPresentador.</summary>
        ConsultaCasosOpinionMedicaPresentador presentador;
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
                this.presentador = new ConsultaCasosOpinionMedicaPresentador(this);
				this.Orden = "Id";
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
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
				if(!Page.IsPostBack) 
				{
					//this.RadGridMaster.Rebind();
					this.CultureDatePicker();
					this.RadGridMaster.Skin = Session[@"SkinGlobal"].ToString();
					Session["NumeroPagina"] = null;
					this.txtFiltro.Enabled = false;
					this.PanelGrid.Visible = true;
					this.RadGridMaster.DataSource = new string[] { };
					this.NumeroDeRegistros = 0;
					this.NumeroPagina = 1;
					this.RadGridMaster.DataBind();
				}
				this.NumeroPagina = 1;
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento pre visualización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
			RadToolBarButton Filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
		}

		protected void DdlFiltro_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			this.NumeroPagina = 1;
			this.txtFiltro.Text = "";
			this.txtFiltro.Enabled = true;
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
				this.Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? @" DESC" : @" ASC");
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
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
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
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void btnEnviarIdMov_Click(object sender, EventArgs e)
		{
			Session["reporteImpIdMovimiento"] = txtIdMov.Text;
			Response.Redirect("~/Modulos/Reportes/ReporteOpMed.aspx", false);
		}
		

		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		///
		protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{
            
		}

		protected void CmdLimpiar_Click(object sender, EventArgs e)
		{
			this.txtAsegurado.Text = string.Empty;
			this.ddlFiltro.ClearSelection();
			this.txtFiltro.Text = string.Empty;
			this.txtFiltro.Enabled = false;
			this.txtFechaFinal.Clear();
			this.txtFechaInicial.Clear();
			this.txtFiltro.Enabled = false;
			this.RadGridMaster.DataSource = new string[] { };
			this.NumeroDeRegistros = 0;
			this.NumeroPagina = 1;
			this.RadGridMaster.DataBind();
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton buscar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void CmdBuscar_Click(object sender, EventArgs e)
		{
			try
			{
				if ((this.txtFechaInicial.SelectedDate != null) && (this.txtFechaFinal.SelectedDate != null))
				{
					{
						DateTime fecha = (DateTime)this.txtFechaInicial.SelectedDate;
						DateTime fecha1 = (DateTime)this.txtFechaFinal.SelectedDate;
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
								this.Singleton.RadAlert("La Fecha Desde no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
								this.RadGridMaster.DataSource = new string[] { };
								this.RadGridMaster.DataBind();
							}
							else
								if (fecha1 > DateTime.Today)
								{
									this.Singleton.RadAlert("La Fecha Hasta no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
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
										int cuantosDias;
										TimeSpan diferencia;
										diferencia = fecha1.Date - fecha.Date;
										cuantosDias = diferencia.Days;

										if (cuantosDias > 365)
										{
											this.Singleton.RadAlert("El Rango de Búsqueda no puede ser mayor a un (1) año,de acuerdo con la Fecha del caso", 380, 50, "Advertencia", "");
											this.RadGridMaster.DataSource = new string[] { };
											this.RadGridMaster.DataBind();
										}
										else
										{
											this.PanelGrid.Visible = true;
											this.NumeroPagina = 1;
											RadGridMaster.CurrentPageIndex = 0;
											this.RadGridMaster.Rebind();
										}
									}
								}
						}

					}
				}
				else
				{
					if (this.txtFechaInicial.SelectedDate != null)
					{
						DateTime fecha = (DateTime)this.txtFechaInicial.SelectedDate;
						if ((fecha > DateTime.Today) && (this.txtFechaFinal.SelectedDate == null))
						{
							this.Singleton.RadAlert("La Fecha Desde no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
							this.RadGridMaster.DataSource = new string[] { };
							this.RadGridMaster.DataBind();
						}
						else
						{
							if (this.txtFechaFinal.SelectedDate != null)
							{
								DateTime fecha1 = (DateTime)this.txtFechaFinal.SelectedDate;
								if (fecha1 > DateTime.Today)
								{
									this.Singleton.RadAlert("La Fecha Hasta no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
									this.RadGridMaster.DataSource = new string[] { };
									this.RadGridMaster.DataBind();
								}
								else
								{
									int cuantosDias;
									TimeSpan diferencia;
									diferencia = fecha1.Date - fecha.Date;
									cuantosDias = diferencia.Days;

									if (cuantosDias > 365)
									{
										this.Singleton.RadAlert("El Rango de Búsqueda no puede ser mayor a un (1) año,de acuerdo con la Fecha del Caso", 380, 50, "Advertencia", "");
										this.RadGridMaster.DataSource = new string[] { };
										this.RadGridMaster.DataBind();
									}
									else
									{
										this.PanelGrid.Visible = true;
										this.NumeroPagina = 1;
										this.RadGridMaster.Rebind();
									}
								}
							}
							else
							{
								this.txtFechaFinal.SelectedDate = DateTime.Now;
							}
						}
					}
					else
					{
						if (this.txtFechaFinal.SelectedDate != null)
						{
							DateTime fecha = (DateTime)this.txtFechaInicial.SelectedDate;
							DateTime fecha1 = (DateTime)this.txtFechaFinal.SelectedDate;

							if (fecha1 > DateTime.Today)
							{
								this.Singleton.RadAlert("La Fecha Hasta no puede ser mayor a la Fecha Actual", 380, 50, "Advertencia", "");
								this.RadGridMaster.DataSource = new string[] { };
								this.RadGridMaster.DataBind();
							}
							else
							{
								int cuantosDias;
								TimeSpan diferencia;
								diferencia = fecha1.Date - fecha.Date;
								cuantosDias = diferencia.Days;

								if (cuantosDias > 365)
								{
									this.Singleton.RadAlert("El Rango de Búsqueda no puede ser mayor a un (1) año,de acuerdo con la Fecha del Caso ", 380, 50, "Advertencia", "");
									this.RadGridMaster.DataSource = new string[] { };
									this.RadGridMaster.DataBind();
								}
								else
								{
									this.PanelGrid.Visible = true;
									this.NumeroPagina = 1;
									this.RadGridMaster.Rebind();
								}
							}
						}
						else if (this.txtFechaFinal.SelectedDate == null)
						{

							if ((string.IsNullOrEmpty(this.txtFiltro.Text)) && (string.IsNullOrEmpty(this.txtAsegurado.Text)))
							{
								
								this.Singleton.RadAlert("Las Fechas no pueden estar vacia", 380, 50, "Advertencia", "");
								this.RadGridMaster.DataSource = new string[] { };
								this.RadGridMaster.DataBind();
							}
							else
							{
								this.PanelGrid.Visible = true;
								this.NumeroPagina = 1;
								this.RadGridMaster.Rebind();
							}
						}
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


		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IEnumerable<ConsultaOpinionMedicaDTO> Datos
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
        ///<summary>Propiedad que asigna u obtiene el nombre del Asegurado.</summary>
        public string Asegurado
        {
            get
            {
                return txtAsegurado.Text;
            }
            set
            {
                this.txtAsegurado.Text = value;
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

		public string FechaInicial
		{
			get
			{
				if (this.txtFechaInicial.SelectedDate != null)
				{
					return this.txtFechaInicial.SelectedDate.Value.ToShortDateString();
				}
				else
				{
					return string.Empty;
				}
			}
		}

		public string FechaFinal
		{
			get
			{
				if (this.txtFechaFinal.SelectedDate != null)
				{
					return this.txtFechaFinal.SelectedDate.Value.ToShortDateString();
				}
				else
				{
					return string.Empty;
				}
			}
		}
		#endregion "Propiedades de Presentación"
	}
}
