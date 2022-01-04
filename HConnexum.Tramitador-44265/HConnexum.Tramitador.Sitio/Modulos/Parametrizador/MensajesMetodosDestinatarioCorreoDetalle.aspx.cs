using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Data;
using Telerik.Web.UI;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
    ///<summary>Clase MensajesMetodosDestinatarioLista.</summary>
    public partial class MensajesMetodosDestinatarioCorreoDetalle : HConnexum.Tramitador.Sitio.PaginaDetalleBase, IMensajesMetodosDestinatarioCorreoDetalle
    {
        #region "Variables Locales"
        ///<summary>Variable presentador MensajesMetodosDestinatarioPresentadorDetalle.</summary>
        MensajesMetodosDestinatarioCorreoPresentadorDetalle presentador;
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
                presentador = new MensajesMetodosDestinatarioCorreoPresentadorDetalle(this);
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
                    presentador.LlenarListaValor();
                    presentador.LlenarCombos();
                    presentador.LlenarListBox();
                    presentador.LlenarConstantes();
                    presentador.LlenarClase();
                    presentador.BuscarRutina();
                    presentador.MostrarVista();
                    if (Accion == AccionDetalle.Ver)
                        this.BloquearControles(true);
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

        protected void cmdPara_Click(object sender, EventArgs e)
        {
            foreach (var items in rlbListaCasosMovimientos.SelectedValue)
            {
                string TextoActual = MultilineCounterPara.Text;
                string Selccion = rlbListaCasosMovimientos.SelectedItem.Text + "; ";
                MultilineCounterPara.Text = TextoActual + Selccion;
            }
        }

        protected void cmdCCO_Click(object sender, EventArgs e)
        {
            foreach (var items in rlbListaCasosMovimientos.SelectedValue)
            {
                string TextoActual = MultilineCounterCCO.Text;
                string Selccion = rlbListaCasosMovimientos.SelectedItem.Text + "; ";
                MultilineCounterCCO.Text = TextoActual + Selccion;
            }
        }

        protected void cmdCC_Click1(object sender, EventArgs e)
        {
            foreach (var items in rlbListaCasosMovimientos.SelectedValue)
            {
                string TextoActual = MultilineCounterCC.Text;
                string Selccion = rlbListaCasosMovimientos.SelectedItem.Text + "; ";
                MultilineCounterCC.Text = TextoActual + Selccion;
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
        public string IdPaso
        {
            get
            {
                return ddlIdPaso.SelectedValue;
            }
            set
            {
                ddlIdPaso.SelectedValue = value;
            }
        }
        public IEnumerable<PasoDTO> ComboIdPaso
        {
            set
            {
                ddlIdPaso.DataSource = value;
                ddlIdPaso.DataBind();
            }
        }
        public string IdMensaje
        {
            get
            {
                return ddlIdMensaje.SelectedValue;
            }
            set
            {
                ddlIdMensaje.SelectedValue = value;
            }
        }

        public DataTable ComboIdMensaje
        {
            set
            {
                ddlIdMensaje.DataSource = value;
                ddlIdMensaje.DataBind();
            }
        }
        public string TipoBusquedaDestinatario
        {
            get
            {
                return MultilineCounterCCO.Text;
            }
            set
            {
                MultilineCounterCCO.Text = string.Format("{0:N0}", value);
            }
        }

        public string Rutina
        {
            get
            {
                return txtRutina.Text;
            }
            set
            {
                txtRutina.Text = string.Format("{0:N0}", value);
            }
        }
        public string TipoBusquedaDestinatarioPara
        {
            get
            {
                return MultilineCounterPara.Text;
            }
            set
            {
                MultilineCounterPara.Text = string.Format("{0:N0}", value);
            }
        }
        public string TipoBusquedaDestinatarioPara1
        {
            get
            {
                return MultilineCounterPara1.Text;
            }
            set
            {
                MultilineCounterPara1.Text = string.Format("{0:N0}", value);
            }
        }
        public string TipoBusquedaDestinatarioCC
        {
            get
            {
                return MultilineCounterCC.Text;
            }
            set
            {
                MultilineCounterCC.Text = string.Format("{0:N0}", value);
            }
        }
        public string TipoBusquedaDestinatarioCC1
        {
            get
            {
                return MultilineCounterCC1.Text;
            }
            set
            {
                MultilineCounterCC1.Text = string.Format("{0:N0}", value);
            }
        }
        public string TipoBusquedaDestinatarioCCO
        {
            get
            {
                return MultilineCounterCCO.Text;
            }
            set
            {
                MultilineCounterCCO.Text = string.Format("{0:N0}", value);
            }
        }

        public string TipoBusquedaDestinatarioCCO1
        {
            get
            {
                return MultilineCounterCCO1.Text;
            }
            set
            {
                MultilineCounterCCO1.Text = string.Format("{0:N0}", value);
            }
        }
        public DataTable ListBoxCasosMovimientos
        {
            set
            {
                this.rlbListaCasosMovimientos.DataSource = value;
                this.rlbListaCasosMovimientos.DataBind();
            }
        }

        public string ValorBusqueda
        {
            get
            {
                return txtRutina.Text;
            }
            set
            {
                txtRutina.Text = value;
            }
        }
        public string TipoPrivacidad
        {
            get
            {
                return txtRutina.Text;
            }
            set
            {
                txtRutina.Text = value;
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