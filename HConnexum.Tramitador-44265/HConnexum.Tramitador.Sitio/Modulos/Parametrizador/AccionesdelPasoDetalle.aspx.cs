using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
    ///<summary>Clase FlujosEjecucionLista.</summary>
    public partial class AccionesdelPasoDetalle : PaginaDetalleBase, IAccionesDelPasoDetalle
    {
        #region "Variables Locales"
        ///<summary>Variable presentador FlujosEjecucionPresentadorDetalle.</summary>
        AccionesDelPasoPresentadorDetalle presentador;
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
                presentador = new AccionesDelPasoPresentadorDetalle(this);
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
                    presentador.LlenarCombos(this.Session["Id"].ToString());
                    if (Accion == AccionDetalle.Ver || Accion == AccionDetalle.Modificar)
                    {
                        presentador.MostrarVista(Accion);
                        if (ddlIdEtapa.SelectedValue != "")
                        {
                            presentador.LlenarCombosddlIdPasoOrigen(int.Parse(ddlIdEtapa.SelectedValue), int.Parse(this.Session["Id"].ToString()), Accion);
                        }
                        if (ddlIdPasoOrigen.SelectedValue != "")
                        {
                            presentador.LlenarCombosddlIdRespuesta(int.Parse(ddlIdPasoOrigen.SelectedValue), Accion);
                            presentador.LlenarCombosddlIdPasoDestinoModificar(int.Parse(ddlIdPasoDestino.SelectedValue));
                            presentador.LlenarCombosddlIdTipoPasoModificar(int.Parse(ddlIdPasoDestino.SelectedValue));
                        }
                    }
                    else if (Accion == AccionDetalle.Agregar)
                    {
                        lblIdPasoOrigen.Enabled = false;
                        ddlIdPasoOrigen.Enabled = false;
                        lblRespuesta.Enabled = false;
                        ddlIdPasoRespuesta.Enabled = false;
                        lblIdPasoDestino.Enabled = false;
                        ddlIdPasoDestino.Enabled = false;
                        lblTipoPaso.Enabled = false;
                        ddlIdTipoPaso.Enabled = false;
                        ddlIdPasoDesborde.Enabled = false;
                        lblIdPasoDesborde.Enabled = false;
                        lblIndReinicioRepeticion.Enabled = false;
                        chkIndReinicioRepeticion.Enabled = false;
                    }            
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
                lblIdPasoOrigen.Enabled = false;
                ddlIdPasoOrigen.Enabled = false;
                lblRespuesta.Enabled = false;
                ddlIdPasoRespuesta.Enabled = false;
                lblTipoPaso.Enabled = false;
                ddlIdTipoPaso.Enabled = false;
                lblIdPasoDestino.Enabled = false;
                ddlIdPasoDestino.Enabled = false;
                ddlIdPasoDesborde.Enabled = false;
                lblIdPasoDesborde.Enabled = false;
                lblIndReinicioRepeticion.Enabled = false;
                chkIndReinicioRepeticion.Enabled = false;
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        ///<summary>Evento de comando que se dispara cuando se selecciona en el combo un suscriptor</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void ddlIdEtapa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ddlIdPasoOrigen.ClearSelection();
                presentador.LlenarCombosddlIdPasoOrigen(int.Parse(ddlIdEtapa.SelectedValue), int.Parse(this.Session["Id"].ToString()), Accion);
                if (ddlIdPasoOrigen.Items.Count == 0)
                {
                    lblIdPasoOrigen.Enabled = false;
                    ddlIdPasoOrigen.Enabled = false;
                }
                else
                {
                    lblIdPasoOrigen.Enabled = true;
                    ddlIdPasoOrigen.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        protected void ddlIdTipoPaso_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ddlIdPasoDestino.ClearSelection();
                if (ddlIdPasoOrigen.SelectedValue != "")
                {
                    presentador.LlenarCombosddlIdPasoDestino(int.Parse(ddlIdTipoPaso.SelectedValue), int.Parse(this.Session["Id"].ToString()), Accion);
                    if (ddlIdPasoDestino.Items.Count == 0)
                    {
                        lblIdPasoDestino.Enabled = false;
                        ddlIdPasoDestino.Enabled = false;
                    }
                    else
                    {
                        lblIdPasoDestino.Enabled = true;
                        ddlIdPasoDestino.Enabled = true;
                    }
                }
                else
                {
                    ddlIdTipoPaso.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        protected void ddlIdPasoOrigen_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ddlIdPasoRespuesta.ClearSelection();
                presentador.LlenarCombosddlIdRespuesta(int.Parse(ddlIdPasoOrigen.SelectedValue), Accion);
                if (ddlIdPasoRespuesta.Items.Count == 0)
                {
                    lblRespuesta.Enabled = false;
                    ddlIdPasoRespuesta.Enabled = false;
                }
                else
                {
                    lblRespuesta.Enabled = true;
                    ddlIdPasoRespuesta.Enabled = true;
                }

                ddlIdTipoPaso.ClearSelection();
                presentador.LlenarCombosddlIdTipoPaso(int.Parse(this.Session["Id"].ToString()));

                if (ddlIdTipoPaso.Items.Count == 0)
                {
                    lblTipoPaso.Enabled = false;
                    ddlIdTipoPaso.Enabled = false;
                }
                else
                {
                    lblTipoPaso.Enabled = true;
                    ddlIdTipoPaso.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        protected void ddlIdPasoDestino_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (ddlIdPasoDestino.Items.Count == 0)
                {
                    ddlIdPasoDesborde.Enabled = false;
                    lblIdPasoDesborde.Enabled = false;
                    lblIndReinicioRepeticion.Enabled = false;
                    chkIndReinicioRepeticion.Enabled = false;
                }
                else
                {
                    ddlIdPasoDesborde.Enabled = true;
                    lblIdPasoDesborde.Enabled = true;
                    lblIndReinicioRepeticion.Enabled = true;
                    chkIndReinicioRepeticion.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
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

        public int IdPasoDesborde
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlIdPasoDesborde.SelectedValue))
                    return int.Parse(ddlIdPasoDesborde.SelectedValue);
                else return 0;
            }
            set
            {
                ddlIdPasoDesborde.SelectedValue = value.ToString();
            }
        }
        public IEnumerable<PasoDTO> ComboIdPasoDesborde
        {
            set
            {
                ddlIdPasoDesborde.DataSource = value;
                ddlIdPasoDesborde.DataBind();
            }
        }
        public string IndReinicioRepeticion
        {
            get { return chkIndReinicioRepeticion.Checked.ToString(); }
            set { chkIndReinicioRepeticion.Checked = ExtensionesString.ConvertirBoolean(value); }
        }

        public int IdEtapa
        {
            get
            {
                return int.Parse(ddlIdEtapa.SelectedValue);
            }
            set
            {
                ddlIdEtapa.SelectedValue = value.ToString();
            }
        }
        public IEnumerable<EtapaDTO> ComboIdEtapa
        {
            set
            {
                ddlIdEtapa.DataSource = value;
                ddlIdEtapa.DataBind();
            }
        }
        public int IdPasoOrigen
        {
            get
            {
                return int.Parse(ddlIdPasoOrigen.SelectedValue);
            }
            set
            {
                ddlIdPasoOrigen.SelectedValue = value.ToString();
            }
        }
        public IEnumerable<PasoDTO> ComboIdPasoOrigen
        {
            set
            {
                ddlIdPasoOrigen.DataSource = value;
                ddlIdPasoOrigen.DataBind();
            }
        }

        public int IdPasoRespuesta
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlIdPasoRespuesta.SelectedValue))
                    return int.Parse(ddlIdPasoRespuesta.SelectedValue);
                else return 0;
            }
            set
            {
                ddlIdPasoRespuesta.SelectedValue = value.ToString();
            }
        }

        public IEnumerable<PasosRepuestaDTO> ComboddlIdPasoRespuesta
        {
            set
            {
                ddlIdPasoRespuesta.DataSource = value;
                ddlIdPasoRespuesta.DataBind();
            }
        }

        public int IdTipoPaso
        {
            get
            {
                return int.Parse(ddlIdTipoPaso.SelectedValue);
            }
            set
            {
                ddlIdTipoPaso.SelectedValue = value.ToString();
            }
        }
        public IEnumerable<TipoPasoDTO> ComboIdTipoPaso
        {
            set
            {
                ddlIdTipoPaso.DataSource = value;
                ddlIdTipoPaso.DataBind();
            }
        }

        public int IdPasoDestino
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlIdPasoDestino.SelectedValue))
                    return int.Parse(ddlIdPasoDestino.SelectedValue);
                else return 0;
            }
            set
            {
                ddlIdPasoDestino.SelectedValue = value.ToString();
            }
        }
        public IEnumerable<PasoDTO> ComboIdPasoDestino
        {
            set
            {
                ddlIdPasoDestino.DataSource = value;
                ddlIdPasoDestino.DataBind();
            }
        }

        public string Condicion
        {
            get
            {
                return txtCondicion.Text;
            }
            set
            {
                txtCondicion.Text = string.Format("{0:N0}", value);
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

        ///<summary>Propiedad que asigna la cadena de errores devuelta desde el presenter.</summary>
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