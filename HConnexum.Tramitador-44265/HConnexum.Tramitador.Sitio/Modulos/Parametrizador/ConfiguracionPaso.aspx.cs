using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Linq;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
    public partial class ConfiguracionPaso : HConnexum.Tramitador.Sitio.PaginaDetalleBase, IConfiguracionPasos
    {
        #region "Variables Locales"
        ///<summary>Variable presentador ConfiguracionPasoPresentadorDetalle.</summary>
        ConfiguracionPasosPresentador presentador;
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
                presentador = new ConfiguracionPasosPresentador(this);
                CultureNumericInput(RadInputManager1);
                CultureDatePicker();
                presentador.LlenarComboTipo("Configuración");
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
                    string accion;
                    accion = Request.QueryString["AccionC"].ToString();
                    presentador.LlenarComboAmbito();
                    if (ddlTipo.Text != "Parametro" || ddlTipo.Text != "Vinculación")
                    {
                        lblAmbito.Visible = false;
                        ddlAmbito.Visible = false;
                        lblAcumulado.Visible = false;
                        chkAcumulado.Visible = false;
                        lblOrigen.Visible = false;
                        ddlOrigen.Visible = false;
                        lblDestino.Visible = false;
                        ddlDestino.Visible = false;
                    }
                    if (accion == "Ver" || accion == "Modificar") if (this.Session["EstructuraXML"] != null)
                        this.tvConfiguracion.LoadXml(this.Session["EstructuraXML"].ToString());
                    else
                        this.tvConfiguracion.LoadContentFile("tree.xml");
                    else
                        this.tvConfiguracion.LoadContentFile("tree.xml");
                    if (accion == "Modificar")
                        this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
                }
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
        }

        ///<summary>Evento que elimina nodos en el control treeview</summary>
        ///<param name="sender">Instancia de la página.</param>
        ///<param name="e">Instancia con parámetros de argumento.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Page.Validate("DeleteRequiresSelection");
            if (Page.IsValid)
            {
                if (tvConfiguracion.SelectedNode.Text != "Configuración")
                    tvConfiguracion.SelectedNode.Remove();
                ddlTipo.ClearSelection();
                ddlTipo.Items.Clear();
                ddlTipo.Enabled = false;
                txtNombre.Text = "";
                ddlAmbito.ClearSelection();
                ddlAmbito.Visible = false;
                lblAmbito.Visible = false;
                lblAcumulado.Visible = false;
                chkAcumulado.Visible = false;
                lblOrigen.Visible = false;
                ddlOrigen.Visible = false;
                ddlOrigen.ClearSelection();
                lblDestino.Visible = false;
                ddlDestino.Visible = false;
                ddlDestino.ClearSelection();
            }
        }

        protected void TreeView_Click(object sender, EventArgs e)
        {
            if (tvConfiguracion.SelectedNode.Level < 3)
            {
                ddlTipo.Enabled = true;
                ddlTipo.ClearSelection();
                txtNombre.Enabled = true;
                txtNombre.Text = "";
                AddButton.Enabled = true;
                btnEliminar.Enabled = true;
                string p = tvConfiguracion.SelectedNode.Text;
                string[] split = p.Split(new Char[] { ':' });
                presentador.LlenarComboTipo(split[0]);
                if (split[0] == "Configuración")
                {
                    ddlTipo.Enabled = true;
                    txtNombre.Enabled = true;
                    btnEliminar.Enabled = false;
                    AddButton.Enabled = true;
                }
                else
                {
                    btnEliminar.Enabled = true;
                }
            }
            else 
            {
                ddlTipo.Enabled = false;
                txtNombre.Enabled = false;
                AddButton.Enabled = false;
            }
        }

        ///<summary>Evento que adjunta nodos en el control treeview</summary>
        ///<param name="sender">Instancia de la página.</param>
        ///<param name="e">Instancia con parámetros de argumento.</param>
        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (tvConfiguracion.SelectedNode.Level < 3)
            {
                IRadTreeNodeContainer target = tvConfiguracion;
                if (tvConfiguracion.SelectedNode != null)
                {
                    tvConfiguracion.SelectedNode.Expanded = true;
                    target = tvConfiguracion.SelectedNode;
                }
                RadTreeNode addedNode = new RadTreeNode(ddlTipo.Text + ": " + txtNombre.Text);
                addedNode.Attributes.Add("Tipo", ddlTipo.Text);
                addedNode.Attributes.Add("Nombre", txtNombre.Text);
                if (ddlTipo.Text == "Parámetro")
                {
                    addedNode.Attributes.Add("Ambito", ddlAmbito.Text);
                    addedNode.Attributes.Add("Acumulado", chkAcumulado.Checked.ToString());
                    if (ddlAmbito.Text == "Salida")
                        addedNode.Category = "Origen";
                    else if (ddlAmbito.Text == "Entrada")
                        addedNode.Category = "Destino";
                    else
                        addedNode.Category = "OrigenDestino";
                }
                else if (ddlTipo.Text == "Vinculación")
                {
                    addedNode.Attributes.Add("Origen", ddlOrigen.SelectedValue);
                    addedNode.Attributes.Add("Destino", ddlDestino.SelectedValue);
                }
                target.Nodes.Add(addedNode);
                txtNombre.Text = "";
                ddlTipo.ClearSelection();
                if (ddlTipo.Text != "Parametro" || ddlTipo.Text != "Vinculación")
                {
                    lblAmbito.Visible = false;
                    ddlAmbito.Visible = false;
                    lblAcumulado.Visible = false;
                    chkAcumulado.Visible = false;
                    lblOrigen.Visible = false;
                    ddlOrigen.Visible = false;
                    lblDestino.Visible = false;
                    ddlDestino.Visible = false;
                }
            }
        }
       
        ///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Session["EstructuraXML"] == null)
                    this.Session.Add("EstructuraXML", tvConfiguracion.GetXml());
                else
                    this.Session["EstructuraXML"] = tvConfiguracion.GetXml();
                presentador.Linq2XmlLeerFicheroXmlConXElement(Session["EstructuraXML"].ToString());
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

        protected void ddlTipo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlAmbito.ClearSelection();
            ddlOrigen.ClearSelection();
            ddlDestino.ClearSelection();
            if (ddlTipo.Text == "Parámetro")
            {
                lblAmbito.Visible = true;
                ddlAmbito.Visible = true;
                lblAcumulado.Visible = true;
                chkAcumulado.Visible = true;
                lblOrigen.Visible = false;
                ddlOrigen.Visible = false;
                lblDestino.Visible = false;
                ddlDestino.Visible = false;
            }
            else if (ddlTipo.Text == "Vinculación")
            {
                IList<RadTreeNode> node = this.tvConfiguracion.GetAllNodes(); 
                foreach (RadTreeNode childNode in node)
                {
                    if (childNode.Category == "Origen" || childNode.Category == "OrigenDestino")
                    {
                        RadComboBoxItem item = new RadComboBoxItem(childNode.Text, childNode.FullPath.ToString());
                        ddlOrigen.Items.Add(item);
                    }
                    if (childNode.Category == "Destino" || childNode.Category == "OrigenDestino")
                    {
                        RadComboBoxItem item = new RadComboBoxItem(childNode.Text, childNode.FullPath.ToString());
                        ddlDestino.Items.Add(item);
                    }
                }
                lblAmbito.Visible = false;
                ddlAmbito.Visible = false;
                lblAcumulado.Visible = false;
                chkAcumulado.Visible = false;
                lblOrigen.Visible = true;
                ddlOrigen.Visible = true;
                lblDestino.Visible = true;
                ddlDestino.Visible = true;
                if (ddlOrigen.Items.Count == 0)
                    ddlOrigen.Enabled = false;
                if (ddlDestino.Items.Count == 0)
                    ddlDestino.Enabled = false;
            }
            else
            {
                lblAmbito.Visible = false;
                ddlAmbito.Visible = false;
                lblAcumulado.Visible = false;
                chkAcumulado.Visible = false;
                lblOrigen.Visible = false;
                ddlOrigen.Visible = false;
                lblDestino.Visible = false;
                ddlDestino.Visible = false;
            }
        }

        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"
        public string Estructura 
        {
            get 
            {
                return tvConfiguracion.GetXml(); 
            }
            set 
            { 
                tvConfiguracion.LoadXml(value);
            }
        }

        public DataTable ComboTipo 
        {
            set
            {
                ddlTipo.DataSource = value;
                ddlTipo.DataBind();
            }
        }

        public DataTable ComboAmbito 
        {
            set 
            {
                ddlAmbito.DataSource = value;
                ddlAmbito.DataBind();
            }
        }

        ///<summary>Propiedad de auditoria que asigna u obtiene el usuario creador del regitros.</summary>
        public string Errores 
        {
            set
            {
                if (value.Length > 0)
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Errores", "alert('Se encontraron los siguientes errores: \\n\\n" + value + "')", true);
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