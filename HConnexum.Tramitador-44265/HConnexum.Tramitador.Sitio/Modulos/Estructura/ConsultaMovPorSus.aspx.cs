using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Web.UI;
using System.Data;
using System.Web;


namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
    public partial class ConsultaMovPorSus : PaginaListaBase, IConsultaMovPorSus
	{
        #region "Variables Locales"
        ///<summary>Variable presentador ConsultaCasosPresentador.</summary>
        ConsultaMovPorSusPresentador presentador;
      

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
                this.presentador = new ConsultaMovPorSusPresentador(this);
                this.Orden = "Id";
            }
            catch (Exception ex)
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

                if (!Page.IsPostBack)
                {
                     this.CultureDatePicker();
                   
                    /*verificar si llega algo por url else*/
                     string IdUsuarioSuscriptror =System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString["IdUsuarioSuscriptor"])).Desencriptar();
                    string IdSuscriptror=string.Empty ;
                    string IdServicio = string.Empty;

                    if(!string.IsNullOrEmpty( this.Request.QueryString["IdSuscriptor"]))
                      IdSuscriptror = System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString["IdSuscriptor"])).Desencriptar();

                    if (!string.IsNullOrEmpty(this.Request.QueryString["IdServicio"])) 
                        IdServicio = System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString["IdServicio"])).Desencriptar();
                    

                     if (!string.IsNullOrEmpty(IdSuscriptror))
                     {
                         presentador.LlenarSuscriptor(int.Parse(IdSuscriptror));
                         ddlSuscriptor.Enabled = false;
                         ddlSuscriptor.SelectedIndex = 0;
                     }
                     else
                     {
                         ddlServicio.Enabled = false;
                         ddlUsuarioAsignado.Enabled = false;
                         presentador.LlenarSuscriptor();
                     }
                     if (!string.IsNullOrEmpty(IdUsuarioSuscriptror))
                     {
                         presentador.LlenarUsuarioAsignado(int.Parse(IdUsuarioSuscriptror));
                         ddlUsuarioAsignado.Enabled = false;
                         ddlUsuarioAsignado.SelectedIndex = 0;
                     }
                     else if (!string.IsNullOrEmpty(IdSuscriptror)) presentador.LlenarUsuarioAsignado(int.Parse(IdSuscriptror));

                     if (!string.IsNullOrEmpty(IdServicio))
                     {
                         presentador.LlenarServicio(int.Parse(IdServicio));
                         ddlServicio.Enabled = false;
                         ddlServicio.SelectedIndex = 0;
                     }
                     else if (!string.IsNullOrEmpty(IdSuscriptror)) presentador.LlenarCombosServicio(int.Parse(IdSuscriptror));
                     
                    this.RadGridMaster.Rebind();
                }
            }
            catch (Exception ex)
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
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }
        ///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
        {

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
                this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro, UsuarioActual.IdUsuarioSuscriptorSeleccionado, UsuarioActual.SuscriptorSeleccionado.Id);
            }
            catch (Exception ex)
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
                switch (e.CommandName)
                {
                    case "Refrescar":
                        this.RadGridMaster.Rebind();
                        break;
                    case @"Desasignar":
                         IList<string> Eliminadas = new List<string>();
                            foreach (GridDataItem item in this.RadGridMaster.Items)
                                if (item.Selected && !string.IsNullOrEmpty(item.OwnerTableView.DataKeyValues[item.ItemIndex][@"UsuarioAsignado"].ToString()))
                                    Eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex][@"IdEncriptado"].ToString());
                            this.presentador.EliminarUsuarioAsignado(Eliminadas, UsuarioActual.IdUsuarioSuscriptorSeleccionado);
                            this.RadGridMaster.Rebind();                        
                        break;
                    
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnAsignar_Click(object sender, EventArgs e)
        {
            int idUsuarioSeleccionado  = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(HidenIdUsuario.Value.ToString())).Desencriptar());
            IList<string> Asignadas = new List<string>();
            foreach (GridDataItem item in this.RadGridMaster.Items)
                if (item.Selected)
                    Asignadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex][@"IdEncriptado"].ToString());
            this.presentador.AsignarUsuario(Asignadas, idUsuarioSeleccionado, UsuarioActual.IdUsuarioSuscriptorSeleccionado);
            this.RadGridMaster.Rebind(); 
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
                NumeroPagina = 1;
                ParametrosFiltro = ExtraerParametrosFiltro(RadFilterMaster);
                this.RadGridMaster.Rebind();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        protected void ddlSuscriptor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlSuscriptor.SelectedValue))
            {
                presentador.LlenarCombosServicio(int.Parse(ddlSuscriptor.SelectedValue));
                presentador.LlenarCombosUsuario(int.Parse(ddlSuscriptor.SelectedValue));
                ddlServicio.Enabled = true;
                ddlUsuarioAsignado.Enabled = true;
            }
            else
            {
                ddlServicio.Enabled = false;
                ddlUsuarioAsignado.Enabled = false;
            }
        }
        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"




        public string Suscriptor
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlSuscriptor.SelectedValue))
                    return ddlSuscriptor.SelectedValue;
                else return "";
            }
           
        }        
        public IEnumerable<SuscriptorDTO> ComboSuscriptor
        {
            set
            {
                this.ddlSuscriptor.DataSource = value;
                ddlSuscriptor.DataBind();
            }
        }
        public string Servicio
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlServicio.SelectedValue))
                    return ddlServicio.SelectedValue;
                else return "";
            }
        }
        public DataTable ComboServicio
        {
            set
            {
                this.ddlServicio.DataSource = value;
                ddlServicio.DataBind();
            }
        }
     
        public string UsuarioAsignado
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlUsuarioAsignado.SelectedValue))
                    return ddlUsuarioAsignado.SelectedValue;
                else return "";
            }
        }
        public DataTable ComboUsuarioAsignado
        {
            set
            {
                this.ddlUsuarioAsignado.DataSource = value;
                ddlUsuarioAsignado.DataBind();
            }
        }

       

        ///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IEnumerable<MovimientoDTO> Datos
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

        #endregion "Propiedades de Presentación"

        protected void Actualizar_Click(object sender, EventArgs e)
        {
            this.RadGridMaster.Rebind(); 
        }

        
	}
}