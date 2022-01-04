using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
    ///<summary>Clase EtapaMaestroDetalle.</summary>
    public partial class EtapaMaestroDetalle : PaginaMaestroDetalleBase, IEtapaMaestroDetalle
    {
        #region "Variables Locales"
        ///<summary>Variable presentador EtapaPresentadorMaestroDetalle.</summary>
        EtapaPresentadorMaestroDetalle presentador;
        public string RutaPadreEncriptada;
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
                presentador = new EtapaPresentadorMaestroDetalle(this);
                Orden = "Id";
                CultureNumericInput(RadInputManager1);
                CultureDatePicker();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
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
                    RutaPadreEncriptada = ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect;
                    presentador.ServicioParametrizador(Accion);
                    if (Accion == AccionDetalle.Ver || Accion == AccionDetalle.Modificar)
                        this.RadGridMaster.Rebind();
                    else if (Accion == AccionDetalle.Agregar)
                        RadGridMaster.Visible = false;
                    if (this.Accion == AccionDetalle.Modificar)
                        this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
                }
                RadFilterMaster.Culture = new System.Globalization.CultureInfo("es-VE");
                RadFilterMaster.RecreateControl();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
 		
        ///<summary>Evento pre visualización de la página.</summary>
        ///<param name="sender">Instancia de la página.</param>
        ///<param name="e">Instancia con parámetros de argumento.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            base.Page_PreRender(sender, e);
            if (Accion == AccionDetalle.Ver)
                BloquearControles(true);
            MostrarBotones(cmdGuardar, cmdGuardaryAgregar, cmdLimpiar, Accion);
            RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
            if (filtro != null)
                if (this.RadFilterMaster.RootGroup.Expressions.Count > 0)
                    filtro.CssClass = @"rtbTextNeg";
            if (!this.presentador.bObtenerRolIndEliminado())
            {
                GridColumn columna = RadGridMaster.MasterTableView.GetColumnSafe("IndEliminado");
                if (columna != null)
                    columna.Visible = false;
            }
        }
 		
        ///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                presentador.GuardarCambios(Accion);
                if (ArbolPaginas.ArbolPaginaActualIsNode())
                    this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
                else
                    this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "setTimeout('cerrarVentana()', 0);", true);
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
 		
        ///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar y agregar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void cmdGuardaryAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                presentador.GuardarCambios(Accion);
                LimpiarControles();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
 		
        ///<summary>Evento libreria ajax que se dispara cuando se hace peticiones en la página.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            this.RadGridMaster.Rebind();
        }
 		
        ///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            NumeroPagina = e.NewPageIndex + 1;
            this.RadGridMaster.Rebind();
        }
 		
        ///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (e != null)
            {
                Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? " DESC" : " ASC");
                this.RadGridMaster.Rebind();
            }
        }
 		
        ///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            NumeroPagina = 1; 
            TamanoPagina = e.NewPageSize;
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

        ///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                presentador.MostrarVista(Orden, NumeroPagina, TamanoPagina, ParametrosFiltro, Accion);
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
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
                    case "Refrescar":
                        this.RadGridMaster.Rebind();
                        break;
                    case "Eliminar":
                        if (((CheckBox)e.Item.OwnerTableView.Items[RadGridMaster.SelectedIndexes[0]]["IndEliminado"].Controls[0]).Checked == false)
                        {
                            IList<string> Eliminadas = new List<string>();
                            foreach (GridDataItem item in RadGridMaster.Items)
                                if (item.Selected)
                                    Eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdEncriptado"].ToString());
                            presentador.Eliminar(Eliminadas);
                            this.RadGridMaster.Rebind();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
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
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }
        public string IdFlujoServicio
        {
            get
            {
                return ddlIdFlujoServicio.SelectedValue;
            }
            set
            {
                ddlIdFlujoServicio.SelectedValue = value;
            }
        }
        public IList<FlujosServicioDTO> ComboIdFlujoServicio
        {
            set
            { 
                ddlIdFlujoServicio.DataSource = value;
                ddlIdFlujoServicio.DataBind(); 
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
        public string Orden
        {
            get
            {
                return txtOrden.Text;
            }
            set
            {
                txtOrden.Text = string.Format("{0:N0}", value);
            }
        }
        public string SlaPromedio
        {
            get
            {
                return txtSlaPromedio.CantidadTotalEnSegundos;
            }
            set
            {
                txtSlaPromedio.CantidadTotalEnSegundos = value;
            }
        }
        public string SlaTolerancia
        {
            get
            {
                return txtSlaTolerancia.CantidadTotalEnSegundos;
            }
            set
            {
                txtSlaTolerancia.CantidadTotalEnSegundos = value;
            }
        }
        public string IndObligatorio
        {
            get
            {
                return chkIndObligatorio.Checked.ToString();
            }
            set
            {
                chkIndObligatorio.Checked = ExtensionesString.ConvertirBoolean(value);
            }
        }
        public string IndRepeticion
        {
            get
            {
                return chkIndRepeticion.Checked.ToString();
            }
            set
            {
                chkIndRepeticion.Checked = ExtensionesString.ConvertirBoolean(value);
            }
        }
        public string IndSeguimiento
        {
            get
            {
                return chkIndSeguimiento.Checked.ToString();
            }
            set
            {
                chkIndSeguimiento.Checked = ExtensionesString.ConvertirBoolean(value);
            }
        }
        public string IndInicioServ
        {
            get
            {
                return chkIndInicioServ.Checked.ToString();
            }
            set
            {
                chkIndInicioServ.Checked = ExtensionesString.ConvertirBoolean(value);
            }
        }
        public string IndCierre
        {
            get
            {
                return chkIndCierre.Checked.ToString();
            }
            set
            {
                chkIndCierre.Checked = ExtensionesString.ConvertirBoolean(value);
            }
        }
 		
        ///<summary>Propiedad de auditoria que asigna u obtiene el indicador de eliminado.</summary>
        public string IndEliminado
        {
            get
            {
                return Auditoria.IndEliminado.ToString();
            }
            set
            {
                Auditoria.IndEliminado = value;
            }
        }

        ///<summary>Propiedad de auditoria que asigna u obtiene el usuario creador del regitros.</summary>
        public string CreadoPor
        {
            get
            {
                return Auditoria.CreadoPor;
            }
            set
            {
                Auditoria.CreadoPor = value;
            }
        }

        ///<summary>Propiedad de auditoria que asigna u obtiene la fecha de creación.</summary>
        public string FechaCreacion
        {
            get
            {
                return Auditoria.FechaCreacion;
            }
            set
            {
                Auditoria.FechaCreacion = value;
            }
        }

        ///<summary>Propiedad de auditoria que asigna u obtiene el usuario que modificó el regitros.</summary>
        public string ModificadoPor
        {
            get
            {
                return Auditoria.ModificadoPor;
            }
            set
            {
                Auditoria.ModificadoPor = value;
            }
        }

        ///<summary>Propiedad de auditoria que asigna u obtiene la fecha de modificación.</summary>
        public string FechaModificacion
        {
            get
            {
                return Auditoria.FechaModificacion;
            }
            set
            {
                Auditoria.FechaModificacion = value;
            }
        }

        ///<summary>Propiedad de publicación que asigna u obtiene la fecha de validez.</summary>
        public string FechaValidez
        {
            get
            {
                return Publicacion.FechaValidez;
            }
            set
            {
                Publicacion.FechaValidez = value;
            }
        }

        ///<summary>Propiedad de publicación que asigna u obtiene el indicador de vigencia.</summary>
        public string IndVigente
        {
            get
            {
                return Publicacion.IndVigente;
            }
            set
            {
                Publicacion.IndVigente = value;
            }
        }
 		
        ///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IEnumerable<PasoDTO> Datos
        {
            set
            {
                RadGridMaster.DataSource = value;
            }
        }
 		
        ///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
        public int NumeroDeRegistros
        {
            get
            {
                return RadGridMaster.VirtualItemCount;
            }
            set
            {
                RadGridMaster.VirtualItemCount = value;
            }
        }

        ///<summary>Propiedad que asigna la cadena de errores o información personalizada devuelta desde el presenter.</summary>
        public string ErroresCustom
        {
            set
            {
                RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                if (windowManagerTemp != null)
                    windowManagerTemp.RadAlert(value, 380, 50, "Mensaje", "");
            }
        }

        ///<summary>Propiedad que asigna la cadena de errores o información personalizada devuelta desde el presenter.</summary>
        public string ErroresCustomEditar
        {
            set
            {
                RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                if (windowManagerTemp != null)
                    windowManagerTemp.RadAlert(value, 380, 50, "Mensaje", "IrAnterior");
            }
        }
        #endregion "Propiedades de Presentación"
    }
}