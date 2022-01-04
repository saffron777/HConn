using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
//using HConnexum.Infraestructura;
using HConnexum.Infraestructura;
using Telerik.Web.UI;
using HConnexum.Tramitador.Negocio;
using System.Data;
using System.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Reportes
{
    public partial class ReporteCA : PaginaBase
    {

        string UrlPage = "";
        #region "Eventos de la Página"

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                base.Page_Init(sender, e);
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
                if (IdCasoHC2 > 0 && IdReclamo <= 0)
                    LlamaReporteIdCasoHC2(IdCasoHC2, IdSuscriptor, origen);
                else if (IdCasoHC2 <= 0 && IdReclamo > 0)
                    LlamaReporteIdReclamo(IdReclamo, IdSuscriptor);
             
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

       
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
          
        }
        
        protected void LlamaReporteIdReclamo(int IdReclamo, int IdSuscriptor)
        {
            ServicioParametrizadorClient DatosParticulares = new ServicioParametrizadorClient();
            DataSet Data = DatosParticulares.ObtenerDatosParticularesPorSuscriptor(IdSuscriptor);
            
            DataTable DT = Data.Tables[0];

            string url = "";
            string firma = "";

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                switch (DT.Rows[i]["Nombre"].ToString())
                {
                    case "UrlReporteCartaAval":
                        url = DT.Rows[i]["Valor"].ToString();
                        break;
                    case "ImagenFirmaReporteCartaAval":
                        firma = DT.Rows[i]["Valor"].ToString();
                        break;
                }
            }
            if (url != null)
            {
                UrlPage = url + "?idCarta=" + IdReclamo + "&idSuscriptor=" + IdSuscriptor + "&Firma=" + firma;
                Response.Redirect(UrlPage, false);
            }

        }
        protected void LlamaReporteIdCasoHC2(int IdCasoHC2, int IdSuscriptor, string origen)
        {

        }
        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"

        public int IdCasoHC2
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public int IdReclamo
        {
            get
            {
                return int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString["idReclamo"])).Desencriptar());
            }
            set
            {

            }
        }

        public int IdSuscriptor
        {
            get
            {
                return int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString["idSuscriptor"])).Desencriptar()); 
            }
            set
            {

            }
        }
        public string origen
        {
            get
            {
                return this.Request.QueryString["origen"].ToString();
            }
            set
            {

            }
        }
        #endregion "Propiedades de Presentación"

    }
}
