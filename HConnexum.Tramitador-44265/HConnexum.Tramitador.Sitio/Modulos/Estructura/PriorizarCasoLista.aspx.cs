using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using Telerik.Web.UI;
using System.Web.Configuration;
using System.Web.UI;
using System.Threading;
using System.Data;

///<summary>Namespace que engloba la vista lista de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
    ///<summary>Clase CasoLista.</summary>
    public partial class PriorizarCasoLista : PaginaListaBase, IPriorizarCasoLista
    {
        ///<summary>Variable presentador CasoPresentadorLista.</summary>
        PriorizarCasoPresentadorLista presentador;
        public DataTable ComboDropDown;
        #region "Eventos de la Pagina"
        ///<summary>Evento de inicialización de la página.</summary>
        ///<param name="sender">Instancia de la página.</param>
        ///<param name="e">Instancia con parámetros de argumento.</param>
        ///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                base.Page_Init(sender, e);
                presentador = new PriorizarCasoPresentadorLista(this);
                Orden = "Id";
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
                ComboDropDown = presentador.LlenarCombos2();

                if (!Page.IsPostBack)
                {
                    presentador.LlenarCombos();
                    this.NumeroPagina = 1;
                    this.RadGridMaster.Rebind();
                }
                   
                this.RadFilterMaster.Culture = Thread.CurrentThread.CurrentCulture;
                this.RadFilterMaster.RecreateControl();
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
            if (filtro != null)
                if (this.RadFilterMaster.RootGroup.Expressions.Count > 0)
                    filtro.CssClass = @"rtbTextNeg";
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

        ///<summary>Evento del rad filter del grid que se dispara cuando se agrega o modifica una busqueda.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected void RadFilterMaster_ItemCommand(object sender, RadFilterCommandEventArgs e)
        {
            if (e != null)
            {
                switch(e.CommandName)
                {
                    case @"RemoveExpression":
                        break;
                    case @"AddGroup":
                        this.LblMessege.Text = @"La Opcion de Agregar Grupo no esta Disponible y no ejecuta ninguna acción";
                        break;
                    case @"AddExpression":
                        this.LblMessege.Text = string.Empty;
                        break;
                }
            }
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
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
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
                    case @"Eliminar":
                        IList<string> Eliminadas = new List<string>();
                        foreach (GridDataItem item in this.RadGridMaster.Items)
                            if (item.Selected)
                                Eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex][@"IdEncriptado"].ToString());
                        this.presentador.Eliminar(Eliminadas);
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


        ///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
        {
        }

        protected void RadGridMaster_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            if (e.Item is GridDataItem)
            {      
                GridDataItem item = (GridDataItem)e.Item;
                string itemValue = item["PrioridadAtencion"].Text;
                RadComboBox ddl = (RadComboBox)item["Temp"].FindControl("RadComboBox1");
                
                    ddl.Attributes.Add("IdCaso", item["Id"].Text);
                    ddl.DataSource = ComboDropDown;
                    ddl.DataTextField = "NombreValor";
                    ddl.DataValueField = "Id";
                    ddl.SelectedValue = itemValue;
                    ddl.DataBind();

            }
        }


        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                RadComboBox ddl2 = (RadComboBox)sender;
                int idUsuarioActual = UsuarioActual.Id;
                int idCaso = int.Parse(ddl2.Attributes["IdCaso"].ToString());
                int prioridadNueva = int.Parse(ddl2.SelectedValue.ToString());
                presentador.GuardarCambiosModificacion(idCaso, prioridadNueva, idUsuarioActual);

                
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        #endregion "Eventos de la Pagina"

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
        public string IdServicio
        {
            get
            {
                return ddlIdServicio.SelectedValue;
            }
            set
            {
                ddlIdServicio.SelectedValue = value;
            }
        }
        public IEnumerable<FlujosServicioDTO> ComboIdServicio
        {
            set
            {
                ddlIdServicio.DataSource = value;
                ddlIdServicio.DataBind();
            }
        
        }
        public string Nombre
        {
            get
            {
                return txtNombre.Text;
            }
            set
            {
                txtNombre.Text = value;
            }
        }
        public string Apellido
        {
            get
            {
                return txtApellido.Text;
            }
            set
            {
                txtApellido.Text = value;
            }
        }
        public string IdEstatus
        {
            get
            {
                return ddlEstatus.SelectedValue;
            }
            set
            {
                ddlEstatus.SelectedValue = value;

            }
        }
        public DataTable ComboIdEstatus
        {
            set
            {
                ddlEstatus.DataSource = value;
                ddlEstatus.DataBind();
            }
        }
        public string IdTipodoc
        {
            get
            {
                return ddlIdTipoDoc.SelectedValue;
            }
            set
            {
                ddlIdTipoDoc.SelectedValue = value;
               
            }
        }
        public DataTable ComboIdTipodoc
        {
            set
            {
                ddlIdTipoDoc.DataSource = value;
                ddlIdTipoDoc.DataBind();
            }
        }
        public string IdPrioridad
        {
            get
            {
                return ddlPrioridad.SelectedValue;
            }
            set
            {
                ddlPrioridad.SelectedValue = value;

            }
        }
        public DataTable ComboIdPrioridad
        {
            set
            {
                ddlPrioridad.DataSource = value;
                ddlPrioridad.DataBind();
            }
        }
        public string Caso
        {
             get
            {
                return txtCasoNumero.Text;
            }
            set
            {
                txtCasoNumero.Text = value;
            }
        }
        public string CodDocumento
        {
            get
            {
                return txtCodDoc.Text;
            }
            set
            {
                txtCodDoc.Text = value;
            }
        }
        #endregion "Propiedades de Presentación"

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.RadGridMaster.Rebind();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.LimpiarControles();
            RadGridMaster.Rebind();
        }
    }
}