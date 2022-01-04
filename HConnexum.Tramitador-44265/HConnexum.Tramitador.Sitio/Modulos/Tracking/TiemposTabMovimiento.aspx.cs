using System;
using System.Linq;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web;

namespace HConnexum.Tramitador.Sitio.Modulos.Tracking
{
    public partial class TiemposTabMovimiento : PaginaDetalleBase, ITiemposTabMovimiento
    {
        #region "Variables Locales"
        ///<summary>Variable presentador MovimientoPresentadorDetalle.</summary>
        TiemposTabMovimientoPresentadorDetalle presentador;
        //manejo Variable de session
        int idmov ;
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
                presentador = new TiemposTabMovimientoPresentadorDetalle(this);
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
                
                if (!this.Page.IsPostBack)
                {
                    idmov = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString["idmov"])).Desencriptar());
                    presentador.MostrarVista(idmov);
                                       
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }
        #region Propiedades de PResentacion
        
        public string FechaCreacion
        {
            get { return txtFechaCreacion.Text; }
            set { txtFechaCreacion.Text = string.Format("{0:N0}", value); }
        }
        public string Atencion
        {
            get { return txtAtencion.CantidadTotalEnSegundos; }
            set { txtAtencion.CantidadTotalEnSegundos=  value; }
        }
        public string FechaAtencion
        {
            get { return txtFechaAtencion.Text; }
            set { txtFechaAtencion.Text = string.Format("{0:N0}", value); }
        }
        public string Ejecucion
        {
            get { return txtEjecucion.CantidadTotalEnSegundos; }
            set { txtEjecucion.CantidadTotalEnSegundos=  value; }
        }
        public string FechaEjecucion
        {
            get { return txtFechaEjecucion.Text; }
            set { txtFechaEjecucion.Text = string.Format("{0:N0}", value); }
        }
        public string TiempoEstimado
        {
            get { return txtTiempoEstimado.Text; }
            set{txtTiempoEstimado.Text = value; }
        }

        #endregion
    }
}