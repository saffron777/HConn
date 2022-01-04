using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web;
using System.Text;
///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary> 
namespace HConnexum.Tramitador.Sitio.Modulos.Tracking
{
    ///<summary>Clase MovimientoLista.</summary>
    public partial class MovimientoDetalle : PaginaDetalleBase, IMovimientoDetalle
    {
        #region "Variables Locales"

        ///<summary>Variable presentador MovimientoPresentadorDetalle.</summary>
        MovimientoPresentadorDetalle presentador;
        string idmovurl;
        int idmov;
        public int _IdCaso;
        public string _IdCasoEncriptado = "";

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
                presentador = new MovimientoPresentadorDetalle(this);
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
                //base.Page_Load(sender, e);
                if (!this.Page.IsPostBack)
                {
                   
                    idmovurl = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(base.Id.ToString().Encriptar()));
                    idmov = base.Id;

                    this.tabAuditoria.ContentUrl = "AuditoriaTabMovimiento.aspx?idmov=" + idmovurl + "&IdMenu=" + IdMenuEncriptado;
                    this.tabObservaciones.ContentUrl = "ObservacionesTabMovimientoLista.aspx?idmov=" + idmovurl + "&IdMenu=" + IdMenuEncriptado;
                    this.tabResultado.ContentUrl = "RespuestaTabMovimiento.aspx?idmov=" + idmovurl + "&IdMenu=" + IdMenuEncriptado;
                    this.tabTiempos.ContentUrl = "TiemposTabMovimiento.aspx?idmov=" + idmovurl + "&IdMenu=" + IdMenuEncriptado;
                    this.CultureDatePicker();
                    presentador.MostrarVista(idmov);
                    _IdCaso = presentador.Vercaso(idmov);
                    Master.FindControl("rsmMigas").Visible = false;
                    if (_IdCaso != 0)
                    {
                        _IdCasoEncriptado = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(_IdCaso.ToString().Encriptar()));
                        btnVerCaso.Visible = true;
                    }
                    else btnVerCaso.Visible = false;
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
            if (this.Accion == AccionDetalle.Ver) this.BloquearControles(true);
        }

        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"
        
        public int Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }
        public string IdServicio
        {
            get { return txtServicio.Text; }
            set { txtServicio.Text = string.Format("{0:N0}", value); }
        }
        public string Version
        {
            get { return txtVersion.Text; }
            set { txtVersion.Text = string.Format("{0:N0}", value); }
        }
        public string IdCaso
        {
            get { return txtCaso.Text; }
            set { txtCaso.Text = value; }
        }
        public string EstatusCaso
        {
            get { return txtEstatusCaso.Text; }
            set { txtEstatusCaso.Text = value; }
        }
        public string IdMovimiento
        {
            get { return txtMovimiento.Text; }
            set { txtMovimiento.Text = value; }
        }
        public string EstatusMovimiento
        {
            get { return txtEstatusMovimiento.Text; }
            set { txtEstatusMovimiento.Text = value; }
        }
        public string TipoMovimiento
        {
            get { return txtTipoMovimiento.Text; }
            set { txtTipoMovimiento.Text = string.Format("{0:N0}", value); }
        }

        #endregion "Propiedades de Presentación"
    }
}