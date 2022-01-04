using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase DocumentosPasoPresentadorDetalle.</summary>
    public class ConfiguracionPasosPresentador : PresentadorDetalleBase<ConfiguracionPasosPresentador>
    {
        ///<summary>Variable vista de la interfaz IConfiguracionPasos.</summary>
        readonly IConfiguracionPasos vista;

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ConfiguracionPasosPresentador(IConfiguracionPasos vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }

        public void LlenarComboTipo(string Tipo)
        {
            try
            {
                ServicioParametrizadorT(Tipo);
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
 
        }

        public void LlenarComboAmbito()
        {
            try
            {
                ServicioParametrizadorA();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }

        }
        public void ServicioParametrizadorA()
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerListaValorPorNombre("AmbitosXML");
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    vista.ComboAmbito = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
        }

        public void ServicioParametrizadorT(string tipo)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataTable Tipo = new DataTable();
                Tipo.Columns.Add("Id");
                Tipo.Columns.Add("Nombre");
                DataRow row = Tipo.NewRow();
               

               
                DataSet ds = servicio.ObtenerListaValorPorNombre("TipoNodoXML");
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    if (tipo == "Configuración")
                    {
                        row["Id"] = "1";
                        for (int i = 0; i < 4; i++)
                        {
                            if (ds.Tables[0].Rows[i][1].ToString() == "Etapa")
                                row["Nombre"] = ds.Tables[0].Rows[i][1].ToString();
                        }
                        Tipo.Rows.Add(row);
                    }
                    else if (tipo == "Etapa")
                    {
                        row["Id"] = "2";
                        for (int i = 0; i < 4; i++)
                        {
                            if(ds.Tables[0].Rows[i][1].ToString() == "Paso")
                                row["Nombre"] = ds.Tables[0].Rows[i][1].ToString();
                        }
                        
                        Tipo.Rows.Add(row);
                    }
                    else if (tipo == "Paso")
                    {
                        row["Id"] = "3";
                        for (int i = 0; i < 4; i++)
                        {
                            if (ds.Tables[0].Rows[i][1].ToString() == "Parámetro")
                                row["Nombre"] = ds.Tables[0].Rows[i][1].ToString();
                        }
                        Tipo.Rows.Add(row);
                        row = Tipo.NewRow();
                        row["Id"] = "4";
                        for (int i = 0; i < 4; i++)
                        {
                            if (ds.Tables[0].Rows[i][1].ToString() == "Vinculación")
                                row["Nombre"] = ds.Tables[0].Rows[i][1].ToString();
                        }
                        Tipo.Rows.Add(row);
                    }
                vista.ComboTipo = Tipo;
                    
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
        }
      
        /// <summary>
        /// Método que se encarga de leer XML archivos NOTA: este metodo no se utilizará en este requerimiento pero no se debe borrar.
        /// </summary>
        /// <param name="XMLEstructura"></param>
        public void Linq2XmlLeerFicheroXmlConXElement(string XMLEstructura)
        {
            string dir = System.Web.HttpContext.Current.Server.MapPath("..//Parametrizador/tree.xml").ToString();
            XElement xmlConfiguracion = XElement.Load(dir);
             

            //Obtener el Nombre de todos los contactos 
            var contactosAll = from c in xmlConfiguracion.Descendants("Node") select c;
                                       
            //Obtener todos los contactos que al menos uno de los Telefonos sea de Tipo Personal 
            var contactosTelPers =
              from c in xmlConfiguracion.Descendants("Node")
              where null != c.Elements("Node").Attributes("Text")
              .
              FirstOrDefault(t => t.Value == "paso")
                            
              select c;
        }
       
    }
}
