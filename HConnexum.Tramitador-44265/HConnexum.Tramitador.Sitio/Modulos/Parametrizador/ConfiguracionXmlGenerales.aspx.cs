using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using System.Web.Configuration;
using System.Data;
using System.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web;
using System.Text;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
    public partial class ConfiguracionXmlGenerales : PaginaMaestroDetalleBase, IConfiguracionXmlGenerales
    {
        #region "Variables Locales"
        ///<summary>Variable presentador ConfiguracionPasoPresentadorDetalle.</summary>
        ConfiguracionXmlGeneralesPresentador presentador;
        public string RutaPadreEncriptada;
        public string IdPaso;
        #endregion "Variables Locales"

        #region "eventos de la pagina"
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
                    if (Session["ConfiguracionPV"] == null)
                    {
                        this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro, this.Id);
                        DTXmlEstructura = this.Datos;
                        DTXmlPasos = this.Pasos;
                    }
                    else
                    {
                        NumeroPagina = 1;
                        BindGridMaster();
                        BindGridPasos();
                    }
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
            RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
            if (filtro != null)
                if (this.RadFilterMaster.RootGroup.Expressions.Count > 0)
                    filtro.CssClass = @"rtbTextNeg";
        }

        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("ConfiguracionPV");
                presentador.GuardarCambios(Id, StrXmlEstructura);
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
                presentador.GuardarCambios(Id, StrXmlEstructura);

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
            if (ArbolPaginas.ArbolPaginaActualIsNode())
                this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
            else
                this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"setTimeout('cerrarVentana()', 0);", true);
        }

        #region RadGridGenerales
        ///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro, this.Id);
                    DTXmlEstructura = this.Datos;
                    DTXmlPasos = this.Pasos;
                }
                else
                {
                    BindGridMaster();
                }

            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                //this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        private void BindGridMaster()
        {
            this.Datos = DTXmlEstructura;
            this.RadGridMaster.Rebind();
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
        protected void RadGridMaster_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Cancel":
                        this.RadGridMaster.Rebind();
                        break;
                    case "Refrescar":
                        this.RadGridMaster.Rebind();
                        break;
                    case "Update":
                        GridDataItem item2 = (GridDataItem)e.Item;

                        int columna = 0;
                        for (int i = 0; i < item2.Cells.Count; i++)
                        {
                            columna = i + 2;
                            if (columna == 7) break;
                            if (columna == 3)
                                DTXmlEstructura.Rows[e.Item.DataSetIndex][i] = ((RadComboBox)item2.Cells[columna].Controls[1]).SelectedValue;
                            else
                                DTXmlEstructura.Rows[e.Item.DataSetIndex][i] = ((TextBox)item2.Cells[columna].Controls[0]).Text;
                        }
                        DTXmlEstructura.Rows[e.Item.DataSetIndex]["CrearSolicitud"] = (item2["CrearSolicitud"].Controls[0] as CheckBox).Checked;

                        string valorAtributoUp = hfIdPaso.Value;
                        presentador.EditarNodo(DTXmlEstructura, StrXmlEstructura, valorAtributoUp, null, e.Item.DataSetIndex);
                        BindGridMaster();
                        break;

                    case "Eliminar":
                        string nombreColumna = "NOMBRE";
                        string valorAtributo = RadGridMaster.Items[RadGridMaster.SelectedIndexes[0]]["NOMBRE"].Text;

                        string mensaje = "";
                        presentador.EliminarNodo(DTXmlEstructura, StrXmlEstructura, valorAtributo, null,  ref mensaje);
                        
                        DTXmlEstructura.AsEnumerable().First(c => c.Field<string>(nombreColumna).ToString() == valorAtributo).Delete();
                        DTXmlEstructura.AcceptChanges();

                        ReEnumeracion(DTXmlEstructura, StrXmlEstructura);

                        BindGridMaster();
                        break;
                    case "PerformInsert":
                        break;
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                //this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        protected void RadGridMaster_InsertCommand(object source, GridCommandEventArgs e)
        {                                           
            var editableItem = ((GridDataItem)e.Item);
            DataTable dt = DTXmlEstructura;         
            string nombre = ((TextBox)editableItem.Cells[2].Controls[0]).Text;
            bool bExiste = dt.AsEnumerable().Any(c => c.Field<string>("NOMBRE").ToString().Contains(nombre));

            if (!bExiste)
            {
                DataRow dr3 = dt.NewRow();
                ModificarCrearColumna(editableItem, dr3);
                DTXmlEstructura.Rows.Add(dr3);
                e.Canceled = true;
                e.Item.OwnerTableView.IsItemInserted = false;
                presentador.CrearXml(dt, StrXmlEstructura, IdPaso);
                BindGridMaster();
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
                if (columna == 7) break;
                if (columna == 3)
                    dr[i] = ((RadComboBox)item2.Cells[columna].Controls[1]).SelectedValue;
                else
                    dr[i] = ((TextBox)item2.Cells[columna].Controls[0]).Text;
            }
            dr["CrearSolicitud"] = (item2["CrearSolicitud"].Controls[0] as CheckBox).Checked;

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

        protected void RadGridMaster_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                //CheckBox cb = new CheckBox();
                //cb.ID = "CheckBox2";
                //cb.Checked = false;
                //cb.Attributes.Add("onclick", "disableAll();");
                //   e.Item.Cells[6].Controls.Add(cb);

                //Label lb = new Label();
                //lb.ID = "lbHeaderSolicitud";
                //lb.Text = "CrearSolicitud";
                //e.Item.Cells[7].Controls.Add(lb);
            }

            if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
            {
                //GridDataItem item = (GridDataItem)e.Item;
                //CheckBox cb = (CheckBox)e.Item.Cells[6].FindControl("CheckBox1");
                //cb.Checked = false;
                //((CheckBox)item["CrearSolicitud"]).Checked = true;
                //item("CustomerID").Text = "Telerik";
            }



        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
            int rowIndex = ((sender as CheckBox).NamingContainer as GridItem).RowIndex;
            //string valor = RadGridMaster.Items[RadGridMaster.SelectedIndexes[0]]["NOMBRE"].Text;
            DTXmlEstructura.Rows[rowIndex]["CrearSolicitud"] = (sender as CheckBox).Checked;

            BindGridMaster();

        }
        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in RadGridMaster.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }
        #endregion

        #region RadGridPASOS
        protected void RadGridPasos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (DTXmlPasos == null)
                {
                    this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro, this.Id);
                    DTXmlPasos = this.Pasos;
                }
                else
                    this.Pasos = DTXmlPasos;

            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                //this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        ///<summary>Evento del rad filter del grid que se dispara cuando se agrega o modifica una busqueda.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected void RadFilterPasos_ItemCommand(object sender, RadFilterCommandEventArgs e)
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
        protected void RadGridPasos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Cancel":
                        this.RadGridPasos.Rebind();
                        break;
                    case "Refrescar":
                        this.RadGridPasos.Rebind();
                        break;
                    case "Update":
                        GridDataItem item2 = (GridDataItem)e.Item;
                        int columna = 0;
                        for (int i = 0; i < item2.Cells.Count; i++)
                        {
                            columna = i + 2;
                            if (columna == 4) break;
                            DTXmlPasos.Rows[e.Item.DataSetIndex][i] = ((TextBox)item2.Cells[columna].Controls[0]).Text;
                        }
                        string pasId = hfIdPaso.Value;
                        presentador.EditarNodo(DTXmlPasos, StrXmlEstructura, null, pasId, e.Item.DataSetIndex);

                        BindGridPasos();
                        break;
                    case "Eliminar":
                        string nombreColumna = "ID";
                        string pasoId = RadGridPasos.Items[RadGridPasos.SelectedIndexes[0]]["ID"].Text;
                        string valorAtributo = RadGridPasos.Items[RadGridPasos.SelectedIndexes[0]]["NOMBRE"].Text;

                        string mensaje = "";
                        presentador.EliminarNodo(DTXmlPasos, StrXmlEstructura, valorAtributo, pasoId, ref mensaje);
                        if (String.IsNullOrEmpty(mensaje))
                        {
                            DTXmlPasos.AsEnumerable().First(c => c.Field<string>(nombreColumna).ToString() == pasoId).Delete();
                            DTXmlPasos.AcceptChanges();
                            BindGridPasos();
                        }
                        else
                        {
                            RadWindowManager windowManager = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                            windowManager.RadAlert(mensaje, 380, 50, "Configuracion XML", "");
                        }
                      
                        break;
                    case "parametrosvinculaciones":
                        break;
                    case "PerformInsert":

                        break;
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                //this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        private static void ModificarCrearColumnaPasos(GridDataItem item2, DataRow dr)
        {
            int columna = 0;
            for (int i = 0; i < item2.Cells.Count; i++)
            {
                columna = i + 2;
                if (columna == 4) break;
                dr[i] = ((TextBox)item2.Cells[columna].Controls[0]).Text;
            }
        }

        protected void RadGridPasos_InsertCommand(object source, GridCommandEventArgs e)
        {
            var editableItem = ((GridDataItem)e.Item);
            DataRow dr3 = DTXmlPasos.NewRow();
            ModificarCrearColumnaPasos(editableItem, dr3);
            DTXmlPasos.Rows.Add(dr3);
            e.Canceled = true;
            e.Item.OwnerTableView.IsItemInserted = false;

            presentador.CrearXml(DTXmlPasos, StrXmlEstructura, null);
            BindGridPasos();
        }

        protected void RadToolBar2_ButtonClick1(object sender, RadToolBarEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                //this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        ///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridPasos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            NumeroPagina = e.NewPageIndex + 1;
            this.RadGridPasos.Rebind();
        }

        ///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridPasos_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (e != null)
            {
                Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? " DESC" : " ASC");
                this.RadGridPasos.Rebind();
            }
        }

        ///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridPasos_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            NumeroPagina = 1;
            TamanoPagina = e.NewPageSize;
            this.RadGridPasos.Rebind();
        }

        protected void RadGridPasos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
            }

            if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
            {
            }
        }

        private void BindGridPasos()
        {
            this.Pasos = DTXmlPasos;
            this.RadGridPasos.Rebind();
        }
        #endregion

        private void EliminarRegistroNodo(RadGrid radgrid, DataTable dt, string nombreColumna, string valor)
        {
            if (radgrid.SelectedIndexes.Count > 0)
            {
                string mensaje = "";
                presentador.EliminarNodo(dt, StrXmlEstructura, valor, null, ref mensaje);
                dt.AsEnumerable().First(c => c.Field<string>(nombreColumna).ToString() == valor).Delete();
                dt.AcceptChanges();

            }
        }

        private void ReEnumeracion(DataTable dt, string xmlEstructura)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Orden"] = (i + 1).ToString();
            }

            XElement xmlContactos = XElement.Load(XDocument.Parse(xmlEstructura).CreateReader());

            List<XElement> recontarGenerales = xmlContactos.Descendants("GENERALES").Elements("PARAMETRO").ToList();
            for (int i = 0; i < recontarGenerales.Count; i++)
            {
                recontarGenerales[i].Attribute("ORDEN").Value = (i + 1).ToString();
            }
            StrXmlEstructura = xmlContactos.ToString().ToString();

        }
        #endregion

        #region "Propiedades de Presentación"

        public int Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public string IdPaso1
        {
            get { return hfIdPaso.Value; }
            set { hfIdPaso.Value = value; }
        }

        public DataTable Datos
        {
            get
            {
                return (DataTable)RadGridMaster.DataSource;
            }
            set
            {
                RadGridMaster.DataSource = value;
            }
        }

        public DataTable Pasos
        {
            get
            {
                return (DataTable)RadGridPasos.DataSource;
            }
            set
            {
                RadGridPasos.DataSource = value;
            }
        }

        public DataTable Parametros
        {
            get
            {
                return new DataTable();
            }
            set
            {
            }
        }

        public DataTable Vinculaciones
        {
            get
            {
                return new DataTable();
            }
            set
            {
            }
        }

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

        public DataTable DTXmlEstructura
        {
            get
            {
                return (DataTable)Session["XmlEstructura"];
            }
            set
            {
                Session["XmlEstructura"] = value;
            }
        }

        public DataTable DTXmlPasos
        {
            get
            {
                return (DataTable)Session["XmlPasos"];
            }
            set
            {
                Session["XmlPasos"] = value;
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

        public IEnumerable<DataRow> DTXmlVinculacionres
        {
            get
            {
                return (IEnumerable<DataRow>)Session["XmlVinculacionres"];
            }
            set
            {
                Session["XmlVinculacionres"] = value;
            }
        }

        #endregion "Propiedades de Presentación"

    }
}