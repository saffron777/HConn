using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Threading;
using System.Web.UI.WebControls;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
    ///<summary>Clase PasoMaestroDetalle.</summary>
    public partial class PasoMaestroDetalleBloques : PaginaMaestroDetalleBase, IPasoMaestroDetalleBloques
    {
        #region "Variables Locales"
        ///<summary>Variable presentador PasoPresentadorMaestroDetalle.</summary>
        PasoPresentadorMaestroDetalleBloques presentador;
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
                this.presentador = new PasoPresentadorMaestroDetalleBloques(this);
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
            if (!this.presentador.bObtenerRolIndEliminado())
            {
                GridColumn columna = RadGridMaster.MasterTableView.GetColumnSafe("IndEliminado");
                if (columna != null) columna.Visible = false;
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
                    case @"Eliminar":
                        if (((CheckBox)e.Item.OwnerTableView.Items[RadGridMaster.SelectedIndexes[0]]["IndEliminado"].Controls[0]).Checked == false)
                        {
                            IList<string> Eliminadas = new List<string>();
                            foreach (GridDataItem item in this.RadGridMaster.Items)
                                if (item.Selected) Eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex][@"IdEncriptado"].ToString());
                            this.presentador.Eliminar(Eliminadas);
                            this.RadGridMaster.Rebind();
                        }
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

        protected void btnActivarEliminado_Click(object sender, EventArgs e)
        {
            string indice = null;
            IList<string> regEliminado = new List<string>();
            foreach (GridDataItem item in this.RadGridMaster.Items)
            {
                if (item.Selected)
                {
                    indice = item.ItemIndex.ToString();
                    regEliminado.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdEncriptado"].ToString());
                }
            }
            this.presentador.activarEliminado(regEliminado);
            this.RadGridMaster.Rebind();
            string radalertscript = "<script language='javascript'>function f(){changeTextRadAlert();radalert('REGISTRO ACTIVADO...<br/><br/>Seleccione el registro que desea editar para ver el detalle', 380, 50,";
            radalertscript += "'Ver detalle de Registro'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", radalertscript);

        }
        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"
        public int Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }
        public string Nombre
        {
            get { return txtNombre.Text; }
            set { txtNombre.Text = value; }
        }
        ///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IEnumerable<PasosBloqueDTO> Datos
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