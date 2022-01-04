using System;
using System.Web.SessionState;
using System.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web;

namespace HConnexum.Tramitador.Sitio.Modulos.Tracking
{
    public partial class ResultadoTabMovimiento : PaginaDetalleBase, IRespuestaTabMovimiento
    {
        #region "Variables Locales"
        ///<summary>Variable presentador MovimientoPresentadorDetalle.</summary>
        ResultadosTabMovimientoPresentadorDetalle presentador;
       //manejo Variable de session
        int idmov;
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
                presentador = new ResultadosTabMovimientoPresentadorDetalle(this);
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
        public string Oma
        {
            get { return txtOma.Text; }
            set { txtOma.Text = string.Format("{0:N0}", value); }
        }
        
        #endregion
    }
}