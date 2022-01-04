using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Infraestructura;
using System.Xml.Linq;

namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
    public partial class ConfiguracionPasosVinculaciones : PaginaMaestroDetalleBase, IConfiguracionXmlGenerales
    {

        #region "Variables Locales"
        ///<summary>Variable presentador ConfiguracionPasoPresentadorDetalle.</summary>
        ConfiguracionXmlGeneralesPresentador presentador;
        public string RutaPadreEncriptada;

        #endregion "Variables Locales"

        ///<summary>Evento de inicialización de la página.</summary>
        ///<param name="sender">Instancia de la página.</param>
        ///<param name="e">Instancia con parámetros de argumento.</param>
        ///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                base.Page_Init(sender, e);
                presentador = new ConfiguracionXmlGeneralesPresentador(this);
                Orden = "Id";
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
                if (!IsPostBack)
                {
                    //if (DTXmlParametros == null)
                    //{
                    NumeroPagina = 1;
                    string sXml = StrXmlEstructura;
                    this.presentador.CargarParametrosVinculaciones2(ref sXml, IdPaso);
                    DTXmlParametros = this.Parametros;
                    DTXmlVinculaciones = this.Vinculaciones;
                    //}
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

        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //Session["ConfiguracionPV"] = "true";
                presentador.GuardarCambios(IdFS, StrXmlEstructura);
                if (ArbolPaginas.ArbolPaginaActualIsNode())
                    this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
                else
                    this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "setTimeout('cerrarVentana()', 0);", true);
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        protected void cmdGuardaryAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                presentador.GuardarCambios(IdFS, StrXmlEstructura);

            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }
        public void CmdcancelarClick(object sender, EventArgs e)
        {
            DTXmlParametros.Clear();
            DTXmlVinculaciones.Clear();
            if (ArbolPaginas.ArbolPaginaActualIsNode())
                this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
            else
                this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"setTimeout('cerrarVentana()', 0);", true);
        }


        #region RadGridParametros
        ///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridParametros_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (DTXmlParametros == null)
                {
                    //this.presentador.CargarParametrosVinculaciones2(ref StrXmlEstructura, IdPaso);
                    //this.presentador.CargarParametrosVinculaciones(this.IdFS, IdPaso);
                    DTXmlParametros = this.Parametros;
                }
                else
                {
                    BindGridParametro();
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                //this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        private void BindGridParametro()
        {
            this.Parametros = DTXmlParametros;

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
                this.RadGridParametros.Rebind();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
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

        ///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridParametros_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Cancel":
                        this.RadGridParametros.Rebind();
                        break;
                    case "Refrescar":
                        this.RadGridParametros.Rebind();
                        break;
                    case "Update":
                        GridDataItem item2 = (GridDataItem)e.Item;
                        ModificarCrearColumna(item2, DTXmlParametros.Rows[e.Item.DataSetIndex]);
                        string valorAtributoUp = hfIdPaso.Value;
                        presentador.EditarNodo(DTXmlParametros, StrXmlEstructura, valorAtributoUp, IdPaso, e.Item.DataSetIndex);
                        BindGridParametro();
                        this.RadGridParametros.Rebind();
                        break;
                    case "Edit":
                        break;
                    case "PerformInsert":
                        break;
                    case "Eliminar":
                        string nombreColumna = "NOMBRE";
                        string valorAtributo = hfIdPaso.Value;

                        string mensaje = "";
                        presentador.EliminarNodo(DTXmlParametros, StrXmlEstructura, valorAtributo, IdPaso, ref mensaje);
                        DTXmlParametros.AsEnumerable().First(c => c.Field<string>(nombreColumna).ToString() == valorAtributo).Delete();
                        DTXmlParametros.AcceptChanges();
                        BindGridParametro();
                        this.RadGridParametros.Rebind();
                        break;
                }

            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        protected void RadGridParametros_InsertCommand(object source, GridCommandEventArgs e)
        {
            var editableItem = ((GridDataItem)e.Item);
            DataTable dt = DTXmlParametros;
            string nombre = ((RadComboBox)editableItem.Cells[2].Controls[1]).Text;
            bool bExiste = dt.AsEnumerable().Any(c => c.Field<string>("NOMBRE").ToString().Contains(nombre));

            if (!bExiste)
            {
                DataRow dr3 = DTXmlParametros.NewRow();
                ModificarCrearColumna(editableItem, dr3);
                DTXmlParametros.Rows.Add(dr3);
                e.Canceled = true;
                e.Item.OwnerTableView.IsItemInserted = false;
                presentador.CrearXml(DTXmlParametros, StrXmlEstructura, IdPaso);
                BindGridParametro();
                this.RadGridParametros.Rebind();
            }
            else
            {
                RadWindowManager windowManager = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                windowManager.RadAlert("El campo <b>" + nombre + "</b> ya existe", 380, 50, "Configuracion XML", "");
            }
        }

        private static void ModificarCrearColumna(GridDataItem item2, DataRow dr)
        {
            int columna = 0;
            for (int i = 0; i < item2.Cells.Count; i++)
            {
                columna = i + 2;
                if (columna == 4) break;
                if (columna == 3)
                    dr[i] = ((RadComboBox)item2.Cells[columna].Controls[1]).SelectedValue;
                else
                    dr[i] = ((TextBox)item2.Cells[columna].Controls[0]).Text;

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
        protected void RadGridParametros_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            NumeroPagina = e.NewPageIndex + 1;
            this.RadGridParametros.Rebind();
        }

        ///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridParametros_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (e != null)
            {
                Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? " DESC" : " ASC");
                this.RadGridParametros.Rebind();
            }
        }

        ///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridParametros_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            NumeroPagina = 1;
            TamanoPagina = e.NewPageSize;
            this.RadGridParametros.Rebind();
        }

        protected void RadGridParametros_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                RadComboBox comboParametros = (RadComboBox)item.FindControl("rcbParametros");
               
                
                IEnumerable<XElement> pasos = ParamPorPaso();
                IEnumerable<string> param = (from p in pasos.Elements("PARAMETRO")
                                             select p.Attribute("NOMBRE").Value.ToString());
                comboParametros.DataSource = param;
                comboParametros.DataBind();  

                if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                {
                    // insert item   
                }
                else
                {
                    // edit item
                    RadComboBox comboAmbito = (RadComboBox)item.FindControl("rcbAmbito");
                    comboParametros.SelectedValue = ((DataRowView)e.Item.DataItem)["NOMBRE"].ToString();
                    comboAmbito.SelectedValue = ((DataRowView)e.Item.DataItem)["AMBITO"].ToString();
                }
            }
        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        }
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in RadGridParametros.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }
        #endregion

        #region RadVinculaciones
        protected void RadGridVinculaciones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (DTXmlVinculaciones == null)
            {
                this.presentador.CargarParametrosVinculaciones(this.IdFS, IdPaso);
                DTXmlVinculaciones = this.Vinculaciones;
            }
            else
            {
                BindGridVinculaciones();
            }
        }

        ///<summary>Evento del rad filter del grid que se dispara cuando se hace click en aceptar en los filtros.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void ApplyButton3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                NumeroPagina = 1;
                ParametrosFiltro = ExtraerParametrosFiltro(RadFilterMaster);
                this.RadGridVinculaciones.Rebind();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        ///<summary>Evento del rad filter del grid que se dispara cuando se agrega o modifica una busqueda.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected void RadFilterVinculaciones_ItemCommand(object sender, RadFilterCommandEventArgs e)
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

        ///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridVinculaciones_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Cancel":
                        break;
                    case "Refrescar":
                        break;
                    case "Update":
                        GridDataItem item2 = (GridDataItem)e.Item;
                        CrearModificarFila(DTXmlVinculaciones.Rows[e.Item.DataSetIndex], item2);
                        string valorAtributoUp = hfIdPaso.Value;
                        presentador.EditarNodo(DTXmlVinculaciones, StrXmlEstructura, valorAtributoUp, IdPaso, e.Item.DataSetIndex);
                        BindGridVinculaciones();
                        break;
                    case "Edit":
                        break;
                    case "Eliminar":
                        string nombreColumna = "NOMBRE";
                        string valorAtributo = hfIdPaso.Value;
                        string mensaje = "";
                        presentador.EliminarNodo(DTXmlVinculaciones, StrXmlEstructura, valorAtributo, IdPaso, ref mensaje);
                        DTXmlVinculaciones.AsEnumerable().First(c => c.Field<string>(nombreColumna).ToString() == valorAtributo).Delete();
                        DTXmlVinculaciones.AcceptChanges();
                        break;
                }
                this.RadGridVinculaciones.Rebind();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        private void CrearModificarFila(DataRow dtRow, GridDataItem item2)
        {
            int columna = 0;
            for (int i = 0; i < item2.Cells.Count; i++)
            {
                columna = i + 2;
                if (columna == 2)
                    dtRow[i] = ((TextBox)item2.Cells[columna].Controls[0]).Text;
                if (columna == 3)
                {
                    string paramSolicitudPasos = ((RadComboBox)item2.Cells[columna].Controls[1]).Text;
                    string idsPasos = ((RadComboBox)item2.Cells[columna].Controls[3]).Text;
                    string parametros = ((RadComboBox)item2.Cells[columna].Controls[5]).Text;
                    string sParam = "PARAMETRO.";
                    string sSolicitud = "SOLICITUD.";
                    string sPaso = "PASO.";
                    string sValor = "";
                    switch (paramSolicitudPasos)
                    {
                        case "PARAMETROS":
                            sValor = "PARAMETROS." + parametros;
                            break;
                        case "SOLICITUD":
                            sValor = sSolicitud + sParam + parametros;
                            break;
                        case "PASO":
                            sValor = sPaso + idsPasos + "." + sParam + parametros;
                            break;
                        default:
                            break;
                    }
                    dtRow[i] = sValor;
                }
                if (columna == 4)
                {
                    string paramSolicitudPasos = ((RadComboBox)item2.Cells[columna].Controls[1]).Text;
                    string sParam = "PARAMETRO.";
                    dtRow[i] = sParam + paramSolicitudPasos;
                }
                if (columna == 5) break;
            }

        }

        protected void RadGridVinculaciones_InsertCommand(object source, GridCommandEventArgs e)
        {
            var editableItem = ((GridDataItem)e.Item);
            DataTable dt = DTXmlVinculaciones;

            DataRow dr3 = dt.NewRow();

            CrearModificarFila(dr3, editableItem);

            DTXmlVinculaciones.Rows.Add(dr3);
            e.Canceled = true;
            e.Item.OwnerTableView.IsItemInserted = false;
            presentador.CrearXml(dt, StrXmlEstructura, this.IdPaso);

            BindGridVinculaciones();
            this.RadGridVinculaciones.Rebind();
        }

        ///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadToolBar3_ButtonClick1(object sender, RadToolBarEventArgs e)
        {

        }

        ///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridVinculaciones_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            NumeroPagina = e.NewPageIndex + 1;
            this.RadGridVinculaciones.Rebind();
        }

        ///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridVinculaciones_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (e != null)
            {
                Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? " DESC" : " ASC");
                this.RadGridVinculaciones.Rebind();
            }
        }

        ///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridVinculaciones_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            NumeroPagina = 1;
            TamanoPagina = e.NewPageSize;
            this.RadGridVinculaciones.Rebind();
        }

        protected void RadGridVinculaciones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox comboIdPasos = (RadComboBox)item.FindControl("rcbIdPasos");
                RadComboBox comboParamOrigen = (RadComboBox)item.FindControl("rcbParametrosOrigen");
                RadComboBox comboParamDestino = (RadComboBox)item.FindControl("rcbParametrosDestino");

                IEnumerable<XElement> pasos = ParamPorPaso();
                List<string> listaIdPasos = (from b in XDocument.Parse(StrXmlEstructura).Descendants("PASO").AsEnumerable()
                                             select b.Attribute("ID").Value.ToString()).ToList();

                //IEnumerable<string> param = (from p in pasos.Elements("PARAMETROS").Elements("PARAMETRO")
                //                             select p.Attribute("NOMBRE").Value.ToString());

                IEnumerable<string> param = (from p in DTXmlVinculaciones.AsEnumerable()
                                             select p.Field<string>("Origen").ToString().Split(new char[] { '.' })[1].ToString());

                comboParamOrigen.DataSource = param;
                comboParamOrigen.DataBind();

                comboIdPasos.DataSource = listaIdPasos;
                comboIdPasos.DataBind();

                comboParamDestino.DataSource = param;
                comboParamDestino.DataBind();

                if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                {
                    // insert item   
                }
                else
                {
                    // edit item                        
                    //comboParamOrigen.SelectedValue = ((DataRowView)e.Item.DataItem)["Origen"].ToString().Split(new char[] { '.' })[1].ToString();                    
                    //comboParamDestino.SelectedValue = ((DataRowView)e.Item.DataItem)["Destino"].ToString().Split(new char[] { '.' })[1].ToString();
                }
            }
        }

        private IEnumerable<XElement> ParamPorPaso()
        {
            //IEnumerable<XElement> pasos = (from b in XDocument.Parse(StrXmlEstructura).Descendants("PASO").AsEnumerable()//.Elements("PARAMETROS").AsEnumerable()
            //                               where b.Attribute("ID").Value == IdPaso
            //                               select b);

             IEnumerable<XElement> pasos = (from b in XDocument.Parse(StrXmlEstructura).Descendants("GENERALES").AsEnumerable()
                                            select b);
            
            return pasos;
        }

        private void BindGridVinculaciones()
        {
            this.Vinculaciones = DTXmlVinculaciones;
            //this.RadGridVinculaciones.Rebind();
        }

        #endregion

        #region "Propiedades de Presentación"

        public int Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        protected int IdFS
        {
            get
            {

                return int.Parse(this.Request.QueryString["IdFS"].ToString());
            }
            set
            {
                this.IdFS = value;
                ViewState[@"idFS"] = this.IdFS;
            }
        }

        protected string IdPaso
        {
            get
            {

                return this.Request.QueryString["IdPaso"].ToString();
            }
            set
            {
                this.IdPaso = value;
                ViewState[@"IdPaso"] = this.IdPaso;
            }
        }

        public DataTable Datos
        {
            get
            {
                return new DataTable();
            }
            set
            {
            }
        }

        public DataTable Pasos
        {
            get
            {
                return new DataTable();
            }
            set
            {
            }
        }

        public DataTable Parametros
        {
            get
            {
                return (DataTable)RadGridParametros.DataSource;
            }
            set
            {
                RadGridParametros.DataSource = value;
            }
        }

        public DataTable Vinculaciones
        {
            get
            {
                return (DataTable)RadGridVinculaciones.DataSource;
            }
            set
            {
                RadGridVinculaciones.DataSource = value;
            }
        }

        public int NumeroDeRegistros
        {
            get
            {
                return RadGridParametros.VirtualItemCount;
            }
            set
            {
                RadGridParametros.VirtualItemCount = value;
            }
        }

        public string Orden
        {
            get
            {
                return this.ViewState[@"Orden"].ToString();
            }
            set
            {
                this.ViewState[@"Orden"] = value;
            }
        }

        public string ErroresCustom
        {
            set { throw new NotImplementedException(); }
        }

        public string Confirm
        {
            set { throw new NotImplementedException(); }
        }

        public DataTable DTXmlParametros
        {
            get
            {
                return (DataTable)Session["DTXmlParametros"];
            }
            set
            {
                Session["DTXmlParametros"] = value;
            }
        }

        public DataTable DTXmlVinculaciones
        {
            get
            {
                return (DataTable)Session["XmlVinculacionres"];
            }
            set
            {
                Session["XmlVinculacionres"] = value;
            }
        }

        public string StrXmlEstructura
        {
            get
            {
                return (string)Session["strXmlEstructura"];
            }
            set
            {
                Session["strXmlEstructura"] = value;
            }
        }

        #endregion "Propiedades de Presentación"
    }
}