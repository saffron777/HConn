using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary> 
namespace HConnexum.Tramitador.Sitio.Modulos.Tracking
{
    ///<summary>Clase CasoLista.</summary>
    public partial class TiempoDelCasoDetalle : HConnexum.Tramitador.Sitio.PaginaDetalleBase, ITiempoDelCasoDetalle
    {
        #region "Variables Locales"
        int Caso;
        ///<summary>Variable presentador CasoPresentadorDetalle.</summary>
        TiempoDelCasoPresentadorDetalle presentador;
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
                this.presentador = new TiempoDelCasoPresentadorDetalle(this);
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
                Caso = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString["idcaso"])).Desencriptar());
                base.Id = Caso;
                if (!this.Page.IsPostBack)
                {
                    this.CultureDatePicker();
                        presentador.MostrarVista();
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
        public string FechaCreacion
        {
            get
            {
                return txtFechaCreacion.ToString();
            }
            set
            {
                txtFechaCreacion.Text = value;
            }
        }
        public string FechaAtencion
        {
            get
            {
                return txtFechaAtencion.ToString();
            }
            set
            {
                txtFechaAtencion.Text = value;
            }
        }
        public string FechaCerrado
        {
            set
            {
                txtFechaCerrado.Text = value;
            }
        }

        public string Calculo2
        {
            set
            {
                txtCerrado.CantidadTotalEnSegundos = value;
            }
        }


        public string Calculo1
        {
            set
            {
                txtAtencion.CantidadTotalEnSegundos = value;
            }
        }

        
        public string SLAestimado
        {
            set
            {
                txtSLAestimado.Text = value;
            }
        }

        #endregion "Propiedades de Presentación"
    }
}