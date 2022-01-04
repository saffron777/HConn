using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Data;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
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
                    if (UsuarioActual.SuscriptorSeleccionado.Nombre.ToUpper() == "NUBISE")
                    {
                        presentador.buscarTodosSuscriptores();
                        ddlServicio.Enabled = false;
                    }
                    else 
                    {
                        this.presentador.LlenarSuscriptor(UsuarioActual.SuscriptorSeleccionado.Id);
                        this.presentador.LlenarComboServicios(IdComboSuscriptores);
                    } 
                 
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
                this.RadGridMaster.Rebind();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
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
                    case @"Refrescar":
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

        ///<summary>Evento de comando que se dispara cuando se hace selecciona un elemento del combo ddlSuscriptor.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void ddlSuscriptor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int res=0;
                if (int.TryParse(ddlSuscriptor.SelectedValue, out res))
                {
                    this.presentador.LlenarComboServicios(IdComboSuscriptores);
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

        protected void cmdLimpiar_Click(object sender, EventArgs e)
        {
            txtCaso.Text = "";
            txtFechaHasta.Clear();
            txtFechaDesde.Clear();
            ddlSuscriptor.SelectedIndex = 0;
            ddlServicio.ClearSelection();
            this.presentador.LlenarComboServicios(IdComboSuscriptores);

        }
        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"
       

        public IEnumerable<SuscriptorDTO> ComboSuscriptores
        {
            set
            {
                              
                this.ddlSuscriptor.DataSource = value;
                ddlSuscriptor.DataBind();
                ddlSuscriptor.SelectedIndex = 0;
              
            }
        }


        public IEnumerable<FlujosServicioDTO> ComboServicios
        {
            set
            {
                ddlServicio.DataSource = value;
                ddlServicio.DataBind();
            }
        }


        public int IdComboSuscriptores
        {
            get
            {
              
                    return int.Parse(ddlSuscriptor.SelectedValue);
            }
        }

       
        public string IdComboServicios
        {
            get { return ddlServicio.SelectedValue.ToString(); }
        }

        public string NumCaso
        {
            get { return txtCaso.Text; }
        }

        public string FechaDesde
        {
            get { return txtFechaDesde.SelectedDate.ToString(); }
        }

        public string FechaHasta
        {
            get { return txtFechaHasta.SelectedDate.ToString(); }
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
        #endregion "Propiedades de Presentación"

       
    }
}