using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary> 
namespace HConnexum.Tramitador.Sitio.Modulos.Tracking
{
    ///<summary>Clase CasoLista.</summary>
    public partial class CasoDetalle : HConnexum.Tramitador.Sitio.PaginaDetalleBase, ICasoDetalle
    {
        #region "Variables Locales"
        ///<summary>Variable presentador CasoPresentadorDetalle.</summary>
        CasoPresentadorDetalle presentador;
        public string IDCASO;
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
                this.presentador = new CasoPresentadorDetalle(this);
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
                IDCASO = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(base.Id.ToString().Encriptar()));
                this.RadPageView4.ContentUrl = "DatosGeneralesCasoDetalle.aspx?idcaso=" + IDCASO + "&IdMenu=" + IdMenu.ToString().Encriptar();
                this.RadPageView1.ContentUrl = "MovimientoCasoLista.aspx?IdMenu=" + HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&idcaso=" + IDCASO
                    + "&EstatusCaso=" + HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(Estatus.ToString().Encriptar()));
                //this.RadPageView2.ContentUrl = "MensajeCasoLista.aspx?idcaso=" + IDCASO + "&IdMenu=" + IdMenu.ToString().Encriptar();
                //this.RadPageView3.ContentUrl = "TiempoDelCasoDetalle.aspx?idcaso=" + IDCASO + "&IdMenu=" + IdMenu.ToString().Encriptar();
                //this.RadPageView4.ContentUrl = "DatosGeneralesCasoDetalle.aspx?idcaso=" + IDCASO;
                //this.RadPageView1.ContentUrl = "MovimientoCasoLista.aspx?idcaso=" + IDCASO;
                this.RadPageView2.ContentUrl = "MensajeCasoLista.aspx?idcaso=" + IDCASO;
                this.RadPageView3.ContentUrl = "TiempoDelCasoDetalle.aspx?idcaso=" + IDCASO;
                if (!this.Page.IsPostBack)
                {
                    presentador.MostrarVista();
                    Session["Servicio"] = Servicio;
                    int mensaje = presentador.BuscaMensajesPendienteChat(this.Id);
                    Master.FindControl("rsmMigas").Visible = false;
                    if (mensaje > 0)
                    {
                        cmdChat.Text = @"Chat Mensajes (" + mensaje + @")";
                    }
                    else
                    {
                        cmdChat.Text = @"Chat";
                    }

                    controlChat.CasoId = this.Id;
                    controlChat.EnvioSuscriptorId = UsuarioActual.SuscriptorSeleccionado.Id;
                    controlChat.Remitente = string.Format("{0} {1}", UsuarioActual.DatosBase.Nombre1, UsuarioActual.DatosBase.Apellido1).Trim();
                    controlChat.CreacionUsuario = UsuarioActual.IdUsuarioSuscriptorSeleccionado;
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
        public string PrioridadAtencion
        {
            set
            {
                txtPrioridadAtencion.Text = string.Format("{0:N2}", value);
            }
        }
        public string IdSolicitud
        {
            set
            {
                txtIdSolicitud.Text = value;
            }
        }

        public string Servicio
        {
            set
            {
                txtServicio.Text = value;
                
            }
            get 
            {
                return txtServicio.Text; 
            }
        }

        public string caso
        {
            set
            {
                txtCasoNumero.Text = value;
            }
        }
        public string Version
        {
            set
            {
                txtVersion.Text = value;
            }
        }
        public string TipoDoc
        {
            set
            {
                txtTipoDoc.Text = value;
            }
        }

        public string Estatus
        {
            get
            {
                return txtEstatus.Text;
            }
            set
            {
                txtEstatus.Text = string.Format("{0:N0}", value);
            }
        }

        public string version
        {
            set
            {
                txtVersion.Text = value;
            }
        }

        public string Suscriptor
        {
            set
            {
                txtSuscriptor.Text = value;
            }
        }

        public string NumDoc
        {
            set
            {
                txtDocSolicitante.Text = value;
            }
        }

        public string CreadorPor
        {
            set
            {
                txtCreadorPor.Text = string.Format("{0:N0}", value);
            }
        }
        public string FechaSolicitud
        {
            set
            {
                txtFechaSolicitud.Text = value;
            }
        }
        public string FechaAnulacion
        {
            set
            {
                txtFechaAnulacion.Text = value;
            }
        }
        public string FechaRechazo
        {
            set
            {
                txtFechaRechazo.Text = value;
            }
        }
        public string FechaCreacion2
        {
            set
            {
                txtFechaCreacion2.Text = value;
            }
        }
        public string Modificado
        {
            set
            {
                txtModificado.Text = value;
            }
        
        }

        public bool indChat
        {
            set
            {
                cmdChat.Visible = value;
            }

        }

        #endregion "Propiedades de Presentación"
    }
}