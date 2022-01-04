using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Datos;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using HConnexum.Infraestructura;
using System.Web.Configuration;
using System.Web;
using System.Web.UI.WebControls;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
    public class ConfiguracionXmlGeneralesPresentador : PresentadorDetalleBase<ConfiguracionXmlGeneralesPresentador>
    {

        readonly IConfiguracionXmlGenerales vista;
        FlujosServicio _FlujosServicio;
        FlujosServicioDTO __FlujosServicio;

        UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ConfiguracionXmlGeneralesPresentador(IConfiguracionXmlGenerales vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        ///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
        ///<param name="pagina">Indica el número de página del conjunto de registros.</param>
        ///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
        ///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
        public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int idFlujoservicio)
        {
            try
            {
                FlujosServicioRepositorio repositorioMaestro = new FlujosServicioRepositorio(udt);
                __FlujosServicio = repositorioMaestro.ObtenerDTOServicioFlujoServicio(vista.Id);
                DataSet Ds = new DataSet();
                if (__FlujosServicio != null && !String.IsNullOrEmpty(__FlujosServicio.XLMEstructura))
                {
                    XElement xmlContactos = XElement.Load(XDocument.Parse(__FlujosServicio.XLMEstructura).CreateReader());
                    Ds.Tables.Add(TablaGenerales(xmlContactos));
                    Ds.Tables.Add(TablaPasos());
                    vista.StrXmlEstructura = __FlujosServicio.XLMEstructura;
                }
                else
                {
                    Ds.Tables.Add(CrearTablaGenerales());
                    Ds.Tables.Add(CrearTablaPasos());
                }
                vista.Datos = Ds.Tables[0];
                vista.Pasos = Ds.Tables[1];
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                if (e.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        
        private DataTable TablaGenerales(XElement xmlContactos)
        {
            DataTable table = CrearTablaGenerales();
            try
            {
                IEnumerable<string> sol = from b in xmlContactos.Descendants("SOLICITUD").Elements("PARAMETROS").Elements("PARAMETRO")
                                          select b.Attribute("NOMBRE").Value.ToString();

                var contactosTelPers = (from c in xmlContactos.Descendants("GENERALES").Elements("PARAMETRO").AsEnumerable()
                                        select c);   
                
                foreach (var item in contactosTelPers)
                {
                    DataRow row = table.NewRow();
                    row["NOMBRE"] = item.Attribute("NOMBRE").Value.ToString();
                    row["VISIBLE"] = item.Attribute("VISIBLE").Value.ToString();
                    row["ETIQUETA"] = item.Attribute("ETIQUETA").Value.ToString();
                    row["ORDEN"] = item.Attribute("ORDEN").Value.ToString();
                    row["LISTA"] = item.Attribute("LISTA").Value.ToString();
                    row["CrearSolicitud"] = (sol.Contains(item.Attribute("NOMBRE").Value.ToString())) ? true : false;
                    table.Rows.Add(row);
                }
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                if (e.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            return table;
        }
        
        private DataTable TablaPasos()
        {
            DataTable dt = CrearTablaPasos();
            PasoRepositorio repositorioPaso = new PasoRepositorio(udt);
            IEnumerable<PasoDTO> pasoDto = repositorioPaso.ObtenerDTOPasosXml(vista.Id);
            foreach (var paso in pasoDto)
            {
                DataRow row = dt.NewRow();
                row["ID"] = paso.Id;
                row["NOMBRE"] = paso.Nombre;
                row["NOMBRETIPOPASO"] = paso.NombreTipoPaso;
                row["ORDEN"] = paso.Orden;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public void CargarParametrosVinculaciones2(ref string xmlEstructura, string idPaso)
        {
            
            try
            {                                                
                var sol = (from b in XDocument.Parse(xmlEstructura).Descendants("PASO").AsEnumerable()
                           where b.Attribute("ID").Value == idPaso
                           select b);

                IEnumerable<XElement> param = (from p in sol.Elements("PARAMETROS").Elements("PARAMETRO") select p);
                DataTable dtparametro = CrearTablaParametros();
                foreach (var item in param)
                {
                    DataRow row = dtparametro.NewRow();
                    row["NOMBRE"] = item.Attribute("NOMBRE").Value.ToString();
                    row["AMBITO"] = item.Attribute("AMBITO").Value.ToString();
                    dtparametro.Rows.Add(row);
                }

                IEnumerable<XElement> vincu = (from v in sol.Elements("VINCULACIONES").Elements("VINCULACION") select v);
                DataTable dtvinculacion = CrearTablaVinculaciones();
                foreach (var item in vincu)
                {
                    DataRow row = dtvinculacion.NewRow();
                    row["NOMBRE"] = item.Attribute("NOMBRE").Value.ToString();
                    row["ORIGEN"] = item.Attribute("ORIGEN").Value.ToString();
                    row["DESTINO"] = item.Attribute("DESTINO").Value.ToString();
                    dtvinculacion.Rows.Add(row);
                }

                vista.Parametros = dtparametro;
                vista.Vinculaciones = dtvinculacion;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                if (e.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void GuardarCambios(int idFlujoServicio, string xmlEstructura)
        {
            try
            {
                udt.IniciarTransaccion();
                FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(udt);

                _FlujosServicio = repositorio.ObtenerPorId(idFlujoServicio);
                _FlujosServicio.XLMEstructura = xmlEstructura;
                udt.MarcarModificado(_FlujosServicio);

                udt.Commit();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];


            }
        }

        public void CargarParametrosVinculaciones(int idFlujoservicio, string idPaso)
        {
            DataSet Ds = new DataSet();
            try
            {
                FlujosServicioRepositorio repositorioMaestro = new FlujosServicioRepositorio(udt);
                __FlujosServicio = repositorioMaestro.ObtenerDTOServicioFlujoServicio(idFlujoservicio);

                if (!String.IsNullOrEmpty(__FlujosServicio.XLMEstructura))
                {
                    XElement xmlContactos = XElement.Load(XDocument.Parse(__FlujosServicio.XLMEstructura).CreateReader());
                    var vinculacionesParametros = from c in xmlContactos.Elements("PASO")
                                                  where c.Attribute("ID").Value == idPaso
                                                  select c;

                    Ds.Tables.Add(LeerXmlRecursivo(vinculacionesParametros.Elements("PARAMETROS"), CrearTablaParametros()));
                    Ds.Tables.Add(LeerXmlRecursivo(vinculacionesParametros.Elements("VINCULACIONES"), CrearTablaVinculaciones()));
                }
                else
                {
                    Ds.Tables.Add(CrearTablaParametros());
                    Ds.Tables.Add(CrearTablaVinculaciones());
                }
                vista.Parametros = Ds.Tables[0];
                vista.Vinculaciones = Ds.Tables[1];
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        private static void ReEnumeracion(ref DataTable dt, ref string xmlEstructura)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Orden"] = (i + 1).ToString();
            }

            XElement xmlContactos = XElement.Load(XDocument.Parse(xmlEstructura).CreateReader());


            List<XElement> recontarGenerales = xmlContactos.Descendants("GENERALES").Elements("PARAMETRO").ToList();
            for (int i = 0; i < recontarGenerales.Count; i++)
            {
                recontarGenerales[i].Attribute("ORDEN").Value = (i + 1).ToString();
            }
        }


        #region CreadoParametrosVinculaciones

        //private DataTable CrearTablaPasos(IEnumerable<XElement> generalesParametro)
        //{
        //    DataTable dt = CrearTablaPasos();
        //    foreach (var item in generalesParametro)
        //    {
        //        if (item.HasAttributes)
        //        {
        //            IEnumerable<XAttribute> atributos = item.Attributes();
        //            CrearTabla(dt, atributos.ToList());
        //        }
        //    }
        //    return dt;
        //} 

        private DataTable LeerXmlRecursivo(IEnumerable<XElement> generalesParametros, DataTable NombreTabla)
        {
            DataTable dt = NombreTabla;
            var generales = from G in generalesParametros.Elements()
                            select G;

            foreach (var items in generales)
            {
                if (items.Elements().Count() > 0)
                {
                    foreach (var item in items.Elements())
                    {
                        CrearFilasYColumnas(dt, item);
                    }
                }
                else
                {
                    CrearFilasYColumnas(dt, items);
                }
            }

            return dt;
        }

        private static void CrearFilasYColumnas(DataTable dt, XElement items)
        {
            var atributos = items.Attributes().ToList();
            CrearTabla(dt, atributos);
        }

        private static void CrearTabla(DataTable dt, List<XAttribute> atributos)
        {
            if (atributos.Count > 0)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < atributos.Count; i++)
                {
                    if (!dt.Columns.Contains(atributos[i].Name.ToString()))
                    {
                        DataColumn dc = new DataColumn(atributos[i].Name.ToString());
                        dt.Columns.Add(dc);
                    }
                    dr[i] = atributos[i].Value.ToString();
                }
                dt.Rows.Add(dr);
            }
        }

        #endregion

        #region * XML: creacion, edicion y eliminacion *
        public void CrearXml(DataTable dt, string xmlEstructura, string pasoId)
        {
            try
            {
                string sXML = "";
                int rowIndex = dt.Rows.Count - 1;
                if (String.IsNullOrEmpty(xmlEstructura))
                {
                    #region CargaGenerales
                    XElement root = new XElement("CONFIGURACION", new XAttribute("SERVICIOID", "130"),
                                                             new XAttribute("NOMBRE", "SEGUIMIENTO OPINION MEDICA"),
                                                             new XAttribute("VERSION", "1.00"));
                    //Generales y solicitud
                    string[] nombre = { "NOMBRE", "tipdocmed" };
                    root.Add(new XElement(dt.TableName.ToString(),
                                new XElement("PARAMETRO",
                                            new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString()),
                                            new XAttribute("VISIBLE", dt.Rows[rowIndex]["VISIBLE"].ToString()),
                                            new XAttribute("ETIQUETA", dt.Rows[rowIndex]["ETIQUETA"].ToString()),
                                            new XAttribute("ORDEN", dt.Rows[rowIndex]["ORDEN"].ToString()),
                                            new XAttribute("LISTA", dt.Rows[rowIndex]["LISTA"].ToString())
                                            )));
                    sXML = root.ToString();
                    #endregion
                }
                else
                {
                    XElement xmlContactos = XElement.Load(XDocument.Parse(xmlEstructura).CreateReader());
                    string nombreNodo = dt.TableName.ToString();
                    switch (nombreNodo)
                    {
                        #region TipoAccionAInsertar
                        case "GENERALES":
                            XElement parametrosGenerales = xmlContactos.Element("GENERALES");
                            parametrosGenerales.Add(new XElement("PARAMETRO",
                                                    new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString()),
                                                    new XAttribute("VISIBLE", dt.Rows[rowIndex]["VISIBLE"].ToString()),
                                                    new XAttribute("ETIQUETA", dt.Rows[rowIndex]["ETIQUETA"].ToString()),
                                                    new XAttribute("ORDEN", dt.Rows[rowIndex]["ORDEN"].ToString()),
                                                    new XAttribute("LISTA", dt.Rows[rowIndex]["LISTA"].ToString())
                                                    ));
                            bool bSolicitud = Convert.ToBoolean(dt.Rows[rowIndex]["CrearSolicitud"].ToString());
                            if (bSolicitud)
                            {
                                CrearSolicitudParametro(dt, rowIndex, xmlContactos);
                            }

                            sXML = parametrosGenerales.ToString();
                            break;
                        case "SOLICITUD":
                            CrearSolicitudParametro(dt, rowIndex, xmlContactos);
                            break;
                        case "PASOS":
                            xmlContactos.Add(new XElement("PASO",
                                           new XAttribute("ID", dt.Rows[rowIndex]["ID"].ToString()),
                                           new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString())));
                            break;
                        case "PARAMETROS":
                            XElement pasosParametros = xmlContactos.Elements("PASO").First(c => c.Attribute("ID").Value == pasoId).Element("PARAMETROS");
                            if (pasosParametros == null)
                            {
                                XElement pasosParametro = xmlContactos.Descendants("PASO").First(c => c.Attribute("ID").Value == pasoId);
                                pasosParametro.Add(new XElement("PARAMETROS",
                                                       new XElement("PARAMETRO",
                                                       new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString()),
                                                       new XAttribute("AMBITO", dt.Rows[rowIndex]["AMBITO"].ToString())
                                                       )));
                            }
                            else
                            {
                                pasosParametros.Add(new XElement("PARAMETRO",
                                                        new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString()),
                                                        new XAttribute("AMBITO", dt.Rows[rowIndex]["AMBITO"].ToString())));

                            }
                            break;
                        case "VINCULACIONES":
                            XElement pasosVinculaciones = xmlContactos.Elements("PASO").Where(c => c.Attribute("ID").Value == pasoId).First().Element("VINCULACIONES");
                            if (pasosVinculaciones == null)
                            {
                                XElement pasosVinculacion = xmlContactos.Descendants("PASO").First(c => c.Attribute("ID").Value == pasoId);
                                pasosVinculacion.Add(new XElement("VINCULACIONES",
                                                         new XElement("VINCULACION",
                                                             new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString()),
                                                             new XAttribute("ORIGEN", dt.Rows[rowIndex]["ORIGEN"].ToString()),
                                                             new XAttribute("DESTINO", dt.Rows[rowIndex]["DESTINO"].ToString())
                                                  )));
                            }
                            else
                            {
                                pasosVinculaciones.Add(new XElement("VINCULACION",
                                                            new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString()),
                                                            new XAttribute("ORIGEN", dt.Rows[rowIndex]["ORIGEN"].ToString()),
                                                            new XAttribute("DESTINO", dt.Rows[rowIndex]["DESTINO"].ToString())
                                                      ));
                            }
                            break;
                        #endregion
                    }
                    sXML = xmlContactos.ToString();
                }
                vista.StrXmlEstructura = sXML;
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }

        }

        private static void CrearSolicitudParametro(DataTable dt, int rowIndex, XElement xmlContactos)
        {
            XElement generalesSol = xmlContactos.Element("SOLICITUD");
            if (generalesSol == null)
            {
                XElement afterGenerales = xmlContactos.Element("GENERALES");
                afterGenerales.AddAfterSelf(new XElement("SOLICITUD",
                                                new XElement("PARAMETROS",
                                                    new XElement("PARAMETRO",
                                                        new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString())
                                                        ))));
            }
            else
            {
                XElement generalesSolicitud = xmlContactos.Element("SOLICITUD").Element("PARAMETROS");
                generalesSolicitud.Add(new XElement("PARAMETRO",
                                            new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString())
                                            ));
            }
        }

        public void EditarNodo(DataTable dt, string xmlEstructura, string valorAtributo, string pasoId, int rowIndex)
        {
            XElement xmlContactos = XElement.Load(XDocument.Parse(xmlEstructura).CreateReader());
            try
            {
                #region TipoAccionAInsertar
                string nombreNodo = dt.TableName.ToString();

                switch (nombreNodo)
                {
                    case "GENERALES":

                        //editar en GENERALES
                        var afterGenerales = xmlContactos.Descendants("GENERALES").Elements("PARAMETRO").Where(c => c.Attribute("NOMBRE").Value == valorAtributo).Single();
                        afterGenerales.Attribute("NOMBRE").Value = dt.Rows[rowIndex]["NOMBRE"].ToString();
                        afterGenerales.Attribute("VISIBLE").Value = dt.Rows[rowIndex]["VISIBLE"].ToString();
                        afterGenerales.Attribute("ETIQUETA").Value = dt.Rows[rowIndex]["ETIQUETA"].ToString();
                        afterGenerales.Attribute("ORDEN").Value = dt.Rows[rowIndex]["ORDEN"].ToString();
                        afterGenerales.Attribute("LISTA").Value = dt.Rows[rowIndex]["LISTA"].ToString();

                        //editar en SOLICITUD  

                        var solicitud = (from l in xmlContactos.Descendants("SOLICITUD").Elements("PARAMETROS").Elements("PARAMETRO")
                                         where l.Attribute("NOMBRE").Value.ToUpper() == valorAtributo.ToUpper()
                                         select l).SingleOrDefault();

                        bool bCrearNodoSolicitud = Convert.ToBoolean(dt.Rows[rowIndex]["CrearSolicitud"].ToString());

                        if (bCrearNodoSolicitud && solicitud != null)
                        {
                            solicitud.Attribute("NOMBRE").Value = dt.Rows[rowIndex]["NOMBRE"].ToString();
                        }

                        if (bCrearNodoSolicitud && solicitud == null)
                        {
                            XElement generalesSolicitud = xmlContactos.Element("SOLICITUD").Element("PARAMETROS");
                            generalesSolicitud.Add(new XElement("PARAMETRO",
                                                        new XAttribute("NOMBRE", dt.Rows[rowIndex]["NOMBRE"].ToString())
                                                        ));
                        }
                        else if (!bCrearNodoSolicitud && solicitud != null)
                        {
                            solicitud.Remove();
                        }
                        break;
                    case "SOLICITUD":
                        break;
                    case "PASOS":
                        int pasos = xmlContactos.Elements("PASO").First(c => c.Attribute("ID").Value == pasoId).Elements().Count();
                        if (pasos == 0)
                        {
                            XElement paso = xmlContactos.Elements("PASO").First(c => c.Attribute("ID").Value == pasoId);
                            paso.Attribute("ID").Value = dt.Rows[rowIndex]["NOMBRE"].ToString();
                            paso.Attribute("NOMBRE").Value = dt.Rows[rowIndex]["VISIBLE"].ToString();
                        }
                        break;
                    case "PARAMETROS":
                        XElement pasosParametros = xmlContactos.Elements("PASO").First(c => c.Attribute("ID").Value == pasoId)
                            .Element("PARAMETROS").Elements("PARAMETRO").First(c => c.Attribute("NOMBRE").Value == valorAtributo);
                        pasosParametros.Attribute("NOMBRE").Value = dt.Rows[rowIndex]["NOMBRE"].ToString();
                        pasosParametros.Attribute("AMBITO").Value = dt.Rows[rowIndex]["AMBITO"].ToString();

                        break;
                    case "VINCULACIONES":
                        XElement pasosVinculaciones = xmlContactos.Descendants("PASO").First(c => c.Attribute("ID").Value == pasoId)
                      .Elements("VINCULACIONES").Elements("VINCULACION").First(c => c.Attribute("NOMBRE").Value == valorAtributo);
                        pasosVinculaciones.Attribute("NOMBRE").Value = dt.Rows[rowIndex]["NOMBRE"].ToString();
                        pasosVinculaciones.Attribute("ORIGEN").Value = dt.Rows[rowIndex]["ORIGEN"].ToString();
                        pasosVinculaciones.Attribute("DESTINO").Value = dt.Rows[rowIndex]["DESTINO"].ToString();

                        break;
                }
                vista.StrXmlEstructura = xmlContactos.ToString().ToString();
                #endregion
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }

        }

        /// <summary>
        /// Eliminar un nodo del XMLEstructura
        /// </summary>
        /// <param name="dt">DataTable Source del Radgrid</param>
        /// <param name="xmlEstructura">Contiene toda la Estructura del XML</param>
        /// <param name="valorAtributo">parámetro cuando se va eliminar un Parámetro o una Vinculacion de un paso (pasar null cuando se
        /// vaya eliminar un registro de Generales)</param>
        /// <param name="pasoId">parámetro cuando se va eliminar un paso, parámetro y/o vinculacion de un paso</param>
        public void EliminarNodo(DataTable dt, string xmlEstructura, string valorAtributo, string pasoId, ref string mensaje)
        {
            XElement xmlContactos = XElement.Load(XDocument.Parse(xmlEstructura).CreateReader());
            try
            {
                #region TipoAccionAInsertar

                string nombreNodo = dt.TableName.ToString();
                switch (nombreNodo)
                {
                    case "GENERALES":
                        XElement afterGenerales = xmlContactos.Descendants("GENERALES").Elements("PARAMETRO").First(c => c.Attribute("NOMBRE").Value == valorAtributo);
                        afterGenerales.Remove();
                        XElement sol = xmlContactos.Descendants("SOLICITUD").Elements("PARAMETROS")
                                .Elements("PARAMETRO").FirstOrDefault(c => c.Attribute("NOMBRE").Value == valorAtributo);
                        if (sol != null)
                        {
                            sol.Remove();
                        }
                        break;
                    case "SOLICITUD":
                        XElement solicitud = xmlContactos.Descendants("SOLICITUD").Elements("PARAMETRO").First(c => c.Attribute("NOMBRE").Value == valorAtributo);

                        solicitud.Remove();
                        break;
                    case "PASOS":
                        int parametrosCount = xmlContactos.Descendants("PASO").First(c => c.Attribute("ID").Value == pasoId)
                         .Element("PARAMETROS").Elements("PARAMETRO").Count();

                        int vinculacionesCount = xmlContactos.Descendants("PASO").First(c => c.Attribute("ID").Value == pasoId)
                            .Element("VINCULACIONES").Elements("VINCULACION").Count();


                        if (parametrosCount <= 0 || vinculacionesCount <= 0)
                        {
                            XElement paso = xmlContactos.Elements("PASO").First(c => c.Attribute("ID").Value == pasoId);
                            paso.RemoveAll();
                        }
                        else
                        {
                            mensaje = "El paso '" + valorAtributo + "' contiene parámetros y vinculaciones asociadas. \nDebe eliminarlas primero ";
                        }

                        break;
                    case "PARAMETROS":
                        XElement pasosParametros = xmlContactos.Descendants("PASO").First(c => c.Attribute("ID").Value == pasoId)
                          .Element("PARAMETROS").Elements("PARAMETRO").First(b => b.Attribute("NOMBRE").Value == valorAtributo);
                        pasosParametros.Remove();
                        break;
                    case "VINCULACIONES":
                        XElement pasosVinculaciones = xmlContactos.Descendants("PASO").First(c => c.Attribute("ID").Value == pasoId)
                            .Element("VINCULACIONES").Elements("VINCULACION").First(b => b.Attribute("NOMBRE").Value == valorAtributo);
                        pasosVinculaciones.Remove();
                        break;
                }
                vista.StrXmlEstructura = xmlContactos.ToString().ToString();
                #endregion
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        #endregion

        #region CREACION_TABLAS
        private DataTable CrearTablaGenerales()
        {
            DataTable table = new DataTable("GENERALES");
            try
            {
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Visible", typeof(string));
                table.Columns.Add("Etiqueta", typeof(string));
                table.Columns.Add("Orden", typeof(string));
                table.Columns.Add("Lista", typeof(string));
                table.Columns.Add("CrearSolicitud", typeof(bool));
                //table.Rows.Add("EDITAR", "EDITAR", "EDITAR", 1, "EDITAR", true);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            return table;
        }

        private DataTable CrearTablaPasos()
        {
            DataTable table = new DataTable("PASOS");
            try
            {
                table.Columns.Add("ID", typeof(string));
                table.Columns.Add("NOMBRE", typeof(string));
                table.Columns.Add("NOMBRETIPOPASO", typeof(string));
                table.Columns.Add("ORDEN", typeof(string));
                //table.Rows.Add("agregar nuevo paso", "agregar nuevo paso");
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            return table;
        }

        private DataTable CrearTablaParametros()
        {
            DataTable table = new DataTable("PARAMETROS");
            try
            {
                table.Columns.Add("NOMBRE", typeof(string));
                table.Columns.Add("AMBITO", typeof(string));

                //table.Rows.Add("EDITAR", "EDITAR");
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            return table;
        }

        private DataTable CrearTablaVinculaciones()
        {
            DataTable table = new DataTable("VINCULACIONES");
            try
            {
                table.Columns.Add("NOMBRE", typeof(string));
                table.Columns.Add("ORIGEN", typeof(string));
                table.Columns.Add("DESTINO", typeof(string));
                //table.Rows.Add("EDITAR", "EDITAR");
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            return table;
        }




        #endregion

    }
}