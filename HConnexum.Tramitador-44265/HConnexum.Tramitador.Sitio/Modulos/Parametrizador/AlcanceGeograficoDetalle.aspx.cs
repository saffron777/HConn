using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio
{
    ///<summary>Clase AlcanceGeograficoLista.</summary>
    public partial class AlcanceGeograficoDetalle : PaginaDetalleBase, IAlcanceGeograficoDetalle
    {
        #region "Variables Locales"
        ///<summary>Variable presentador AlcanceGeograficoPresentadorDetalle.</summary>
        AlcanceGeograficoPresentadorDetalle presentador;
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
                presentador = new AlcanceGeograficoPresentadorDetalle(this);
                CultureNumericInput(RadInputManager1);
                CultureDatePicker();
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
                    presentador.LlenarComboServicio(UsuarioActual.SuscriptorSeleccionado.Id);
                    if (Accion == AccionDetalle.Ver || Accion == AccionDetalle.Modificar)
                        presentador.MostrarVista(UsuarioActual.SuscriptorSeleccionado.Id);
                    else if (Accion == AccionDetalle.Agregar)
                    {
                        ddlIdSucursal.Enabled = false;
                        ddlPais.Enabled = false;
                        ddlDiv1.Enabled = false;
                        ddlDiv2.Enabled = false;
                        ddlDiv3.Enabled = false;
                    }
                        
                    if (this.Accion == AccionDetalle.Modificar)
                        this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
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
            if (Accion == AccionDetalle.Ver)
                BloquearControles(true);
            MostrarBotones(cmdGuardar, cmdGuardaryAgregar, cmdLimpiar, Accion);
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
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
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
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        protected void ddlIdFlujoServicio_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlIdSucursal.Text = "";
            ddlPais.ClearSelection();
            ddlDiv1.ClearSelection();
            ddlDiv2.ClearSelection();
            ddlDiv3.ClearSelection();

            ddlIdSucursal.Enabled = true;
            ddlPais.Enabled = false;
            ddlDiv1.Enabled = false;
            ddlDiv2.Enabled = false;
            ddlDiv3.Enabled = false;

            ddlIdSucursal.Items.Clear();
            ddlPais.Items.Clear();
            ddlDiv1.Items.Clear();
            ddlDiv2.Items.Clear();
            ddlDiv3.Items.Clear();

            presentador.LlenarComboSucursal(int.Parse(ddlIdFlujoServicio.SelectedValue), UsuarioActual.SuscriptorSeleccionado.Id);
        }

        protected void ddlPais_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlDiv1.ClearSelection();
            ddlDiv2.ClearSelection();
            ddlDiv3.ClearSelection();

            ddlDiv1.Enabled = true;
            ddlDiv2.Enabled = false;
            ddlDiv3.Enabled = false;

            ddlDiv1.Items.Clear();
            ddlDiv2.Items.Clear();
            ddlDiv3.Items.Clear();

            presentador.LlenarComboDiv1(int.Parse(ddlPais.SelectedValue));
        }

        protected void ddlDiv1_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlDiv2.ClearSelection();
            ddlDiv3.ClearSelection();

            ddlDiv2.Enabled = true;
            ddlDiv3.Enabled = false;

            ddlDiv2.Items.Clear();
            ddlDiv3.Items.Clear();

            presentador.LlenarComboDiv2(ddlDiv1.SelectedValue.ToString());
        }

        protected void ddlDiv2_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlDiv3.ClearSelection();

            ddlDiv3.Enabled = true;

            ddlDiv3.Items.Clear();

            presentador.LlenarComboDiv3(ddlDiv2.SelectedValue.ToString());
        }

        protected void ddlIdSucursal_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlPais.ClearSelection();
            ddlDiv1.ClearSelection();
            ddlDiv2.ClearSelection();
            ddlDiv3.ClearSelection();

            ddlPais.Enabled = true;
            ddlDiv1.Enabled = false;
            ddlDiv2.Enabled = false;
            ddlDiv3.Enabled = false;

            ddlPais.Items.Clear();
            ddlDiv1.Items.Clear();
            ddlDiv2.Items.Clear();
            ddlDiv3.Items.Clear();

            presentador.LlenarComboPais();
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
        //string idServicioSucursal;
        public string IdServicioSucursal
        {
            get
            {
                return ddlIdSucursal.SelectedValue;
            }
            set
            {
                ddlIdSucursal.SelectedValue = value;
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
        public List<ServicioSucursalDTO> ComboServicio
        {
            set
            {
                foreach (ServicioSucursalDTO servicio in value)
                {
                    Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem();
                    item.Text = servicio.NombreServicio + " (V" + servicio.Version + ")";
                    item.Value = servicio.Id != null ? servicio.Id.ToString() : "0";
                    ddlIdFlujoServicio.Items.Add(item);
                }


                ddlIdFlujoServicio.DataBind();
            }
        }
        public string IdSucursal
        {
            get
            {
                return ddlIdSucursal.SelectedValue;
            }
            set
            {
                ddlIdSucursal.SelectedValue = value;
            }
        }
        public List<ServicioSucursalDTO> ComboSucursal
        {
            set
            {
                ddlIdSucursal.DataSource = value;
                ddlIdSucursal.DataBind();
            }
        }
        public string IdPais
        {
            get
            {
                return ddlPais.SelectedValue;
            }
            set
            {
                ddlPais.SelectedValue = value;
            }
        }
        public DataTable ComboPais
        {
            set
            {
                ddlPais.DataSource = value;
                ddlPais.DataBind();
            }
        }
        public string IdDiv1
        {
            get
            {
                return ddlDiv1.SelectedValue;
            }
            set
            {
                ddlDiv1.SelectedValue = value;
            }
        }
        public DataTable ComboDiv1
        {
            set
            {
                ddlDiv1.DataSource = value;
                ddlDiv1.DataBind();
            }
        }
        public string IdDiv2
        {
            get
            {
                return ddlDiv2.SelectedValue;
            }
            set
            {
                ddlDiv2.SelectedValue = value;
            }
        }
        public DataTable ComboDiv2
        {
            set
            {
                ddlDiv2.DataSource = value;
                ddlDiv2.DataBind();
            }
        }
        public string IdDiv3
        {
            get
            {
                return ddlDiv3.SelectedValue;
            }
            set
            {
                ddlDiv3.SelectedValue = value;
            }
        }
        public DataTable ComboDiv3
        {
            set
            {
                ddlDiv3.DataSource = value;
                ddlDiv3.DataBind();
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
        #endregion "Propiedades de Presentación"
    }
}