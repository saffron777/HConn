using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Servicios.Servicios;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.ServiceModel;
using System.Configuration;
using System.Xml.Linq;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Seguridad;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
    public class SelectorCasosPresentador : BloquesPresentadorBase
    {
        	///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
		readonly ISelectorCasos vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		bool supervisor = false;
		IList<int> idsSupervisados = new List<int>();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public SelectorCasosPresentador(ISelectorCasos vista)
		{
			this.vista = vista;
		}

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        ///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
        ///<param name="pagina">Indica el número de página del conjunto de registros.</param>
        ///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
        ///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
        public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro)
        {
            try
            {
                if (!vista.inicio)
                {
                    #region Filtros de Busquedas
                    orden = "ORDER BY (TB_CasosExternos.IdCaso) DESC";
                    this.vista.Datos = null;
                    string FiltrosBusqueda = "";
                    Filtro tes = new Filtro();
                    DateTime fechaDesde;
                    DateTime fechaHasta;
                    string NumDocSolTit = "";
                    if (vista.IdComboSuscriptores.Trim() != "" && vista.IdComboSuscriptorASimular.Trim() != "")
                    {
                        FiltrosBusqueda = " WHERE (TB_CasosExternos.IdSuscriptorProv = " + vista.IdComboSuscriptores + " OR TB_CasosExternos.IdSuscriptor = " + vista.IdComboSuscriptores + ")";
                        FiltrosBusqueda += " OR (TB_CasosExternos.IdSuscriptorProv = " + vista.IdComboSuscriptorASimular + " OR TB_CasosExternos.IdSuscriptor = " + vista.IdComboSuscriptorASimular + ")";
                    }
                    if (vista.IdComboSuscriptorASimular.Trim() == "")
                    {
                        FiltrosBusqueda = " WHERE (TB_CasosExternos.IdSuscriptorProv = " + vista.UsuarioActual.SuscriptorSeleccionado.Id + " OR TB_CasosExternos.IdSuscriptor = " + vista.UsuarioActual.SuscriptorSeleccionado.Id + ")";
                        if (vista.IdComboSuscriptores != vista.UsuarioActual.SuscriptorSeleccionado.Id.ToString())
                        {
                            tes.Campo = "IdSuscriptor"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdComboSuscriptores);
                            parametrosFiltro.Add(tes);
                            FiltrosBusqueda += " AND (TB_CasosExternos.IdSuscriptorProv = " + vista.IdComboSuscriptores + " OR TB_CasosExternos.IdSuscriptor = " + vista.IdComboSuscriptores + ")";
                        }
                    }
                    if (vista.IdComboSuscriptores.Trim() == "")
                    {
                        FiltrosBusqueda = " WHERE (TB_CasosExternos.IdSuscriptorProv = " + vista.IdComboSuscriptorASimular + " OR TB_CasosExternos.IdSuscriptor = " + vista.IdComboSuscriptorASimular + ")";
                    }


                    if (vista.TipoFiltro.Trim() != "")
                    {
                        string campo = "";
                        if (!String.IsNullOrEmpty(vista.Filtro))
                        {
                            
                            if (vista.TipoFiltro == "NumDocSolicitante")
                            {
                                NumDocSolTit = vista.Filtro;
                                campo = "DocumentoId";
                                FiltrosBusqueda = FiltrosBusqueda + "  and (TB_CasosExternos." + campo + " = '" + NumDocSolTit + "' or TB_CasosExternos.NumDocTit = '" + NumDocSolTit + "')"; // esta linea va para el cambio 38
                            }
                            else
                            {
                                  if (vista.TipoFiltro == "Id")
                                {

                                    tes.Campo = "Id";
                                    tes.Operador = "EqualTo";
                                    tes.Tipo = typeof(int);
                                    tes.Valor = int.Parse(vista.Filtro);

                                }

                                else
                                {
                                    tes.Campo = vista.TipoFiltro;
                                    tes.Operador = "EqualTo";
                                    tes.Tipo = typeof(string);
                                    tes.Valor = vista.Filtro;
                                }

                                parametrosFiltro.Add(tes);
                            }
                           
                        }

                       
                        if (tes.Campo == "Id")
                        {
                            campo = "IdCaso";
                            FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = " + tes.Valor;
                        }
                        else if (tes.Campo == "Idcasoexterno")
                        {
                            campo = "Reclamo";
                            FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                        }
                        else if (tes.Campo == "SupportIncident")
                        {
                            campo = "Suport_Incident";
                            FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                        }
                        else if (tes.Campo == "NumDocSolicitante")
                        {
                         
                           // FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                        }
                        else if (tes.Campo == "Ticket")
                        {
                            campo = "Ticket";
                            FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                        }
                    }
                    if (vista.NombreServicioSuscriptor != "")
                    {
                        FiltrosBusqueda = FiltrosBusqueda + " and VW_ServiciosSuscriptor.Nombre = '" + vista.NombreServicioSuscriptor + "'";
                    }
                    if (vista.IdComboServicios != "")
                    {
                        tes.Campo = "IdServiciosuscriptor"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdComboServicios);
                        parametrosFiltro.Add(tes);
                        FiltrosBusqueda = FiltrosBusqueda + " and TB_CasosExternos.IdServiciosuscriptor = " + vista.IdComboServicios;
                    }

                    if (vista.FechaDesde.Trim() != "" && vista.FechaHasta != "")
                    {
                        fechaDesde = DateTime.Parse(vista.FechaDesde);
                        fechaHasta = DateTime.Parse(vista.FechaHasta);
                       
                        FiltrosBusqueda = FiltrosBusqueda + " and  TB_CasosExternos.FechaCreacion BETWEEN '" + String.Format("{0:MM/dd/yyyy}", fechaDesde) + "' AND '" + String.Format("{0:MM/dd/yyyy}", fechaHasta) + "'";
                    }
                    if (vista.FechaDesde.Trim() != "")
                    {
                        fechaDesde = DateTime.Parse(vista.FechaDesde);
                        tes.Campo = "FechaCreacion"; tes.Operador = "GreaterThanOrEqualTo"; tes.Tipo = typeof(DateTime); tes.Valor =  DateTime.Parse(vista.FechaDesde);
                        parametrosFiltro.Add(tes);
                        FiltrosBusqueda = FiltrosBusqueda + " and TB_CasosExternos.FechaCreacion = '" + String.Format("{0:MM/dd/yyyy}", fechaDesde) + "'";
                    }
                    if (vista.FechaHasta != "")
                    {
                        fechaHasta = DateTime.Parse(vista.FechaHasta);
                        tes.Campo = "FechaCreacion"; tes.Operador = "LessThanOrEqualTo"; tes.Tipo = typeof(DateTime); tes.Valor = DateTime.Parse(vista.FechaHasta).AddMinutes(1439);
                        parametrosFiltro.Add(tes);
                        if (DateTime.Now.ToShortDateString() != vista.FechaHasta)
                            FiltrosBusqueda = FiltrosBusqueda + " and  TB_CasosExternos.FechaCreacion BETWEEN '" + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + "' AND '" + String.Format("{0:MM/dd/yyyy}", fechaHasta) + "'";
                    }
                    if (vista.IdComboEstatus != "")
                    {
                        tes.Campo = "Estatus"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdComboEstatus);
                        parametrosFiltro.Add(tes);
                        FiltrosBusqueda = FiltrosBusqueda + " and TB_CasosExternos.IdEstatusCaso = " + vista.IdComboEstatus;
                    }
                    if (vista.Asegurado != "")
                    {
                        tes.Campo = "NombreSolicitante"; tes.Operador = "EqualTo"; tes.Tipo = typeof(string); tes.Valor = vista.Asegurado;
                        parametrosFiltro.Add(tes);
                        FiltrosBusqueda = FiltrosBusqueda + " and TB_CasosExternos.Asegurado = '" + vista.Asegurado + "'";
                    }
                    if (vista.Intermediario != "")
                    {
                        tes.Campo = "Intermediario"; tes.Operador = "EqualTo"; tes.Tipo = typeof(string); tes.Valor = vista.Intermediario;
                        parametrosFiltro.Add(tes);
                        FiltrosBusqueda = FiltrosBusqueda + " and TB_CasosExternos.IdSuscriptorInt = '" + vista.idIntermediario + "'";
                    }
                    #endregion
                    CasoRepositorio repositorioCasos = new CasoRepositorio(unidadDeTrabajo);
                    IEnumerable<CasoDTO> datos;

                   // this.vista.Datos = repositorioCasos.ObtenerDTO(orden, pagina, tamañoPagina, parametrosFiltro, FiltrosBusqueda,
                    //this.vista.UsuarioActual.SuscriptorSeleccionado.Id, vista.IdComboSuscriptorASimular, NumDocSolTit);
                   // this.vista.NumeroDeRegistros = repositorioCasos.Conteo;
                }
                else
                {
                    limpiaDatos();
                }
                                                   
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        public void limpiaDatos ()
        {
            IEnumerable<CasoDTO> _datos = Enumerable.Empty<CasoDTO>();
            vista.Datos = _datos;
            this.vista.NumeroDeRegistros = 0;
        }

        public string ObtenerValorParametroXmlRespuesta(string xml, string nombreParametro)
        {
            try
            {
                string tagParametroRespuesta = ConfigurationManager.AppSettings[@"TagParametroRespuesta"].ToUpper();
                string atributoNombreRespuesta = ConfigurationManager.AppSettings[@"AtributoNombreRespuesta"].ToUpper();
                string atributoValor = ConfigurationManager.AppSettings[@"AtributoValor"].ToUpper();
                XElement xmlRespuesta = XElement.Parse(xml);
                string valor = (from p in xmlRespuesta.Descendants(tagParametroRespuesta)
                                where p.Attribute(atributoNombreRespuesta).Value.ToUpper() == nombreParametro
                                select p.Attribute(atributoValor).Value).FirstOrDefault();
                return (string.Empty + valor);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            return string.Empty;
        }
      
        public bool ComprobarNubise(int id)
        {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio(udt);
            if (usuarioRepositorio.ObtenerPorId(id).LoginUsuario.ToLower() == "admin")
                return true;
            else
                return false;
        }

        public void CargarTodosSuscriptores(string nombre)
        {
            SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
           // vista.ComboSuscriptores = suscriptorRepositorio.ObtenerSuscriptoresPorNombresDTO(nombre); 
                                            
        }

        public void ComprobarSupervisor(int idUsuario, int idUsuarioSuscriptor)
        {
            DataTable supervisados = BuscarSupervisados(idUsuarioSuscriptor);
            if (supervisados != null)
            {

                idsSupervisados = ConvertToIList(supervisados);
                supervisor = true;
            }
            else
            {
                UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio(udt);
                UsuarioDTO usuario = usuarioRepositorio.ObtenerDTO(idUsuario);

            }
        }

        private IList<int> ConvertToIList(DataTable dataTable)
        {
            IList<int> lista = new List<int>();
            foreach (DataRow row in dataTable.Rows)
                lista.Add(int.Parse(row["Id"].ToString()));
            return lista;
        }

        //public void LlenarSuscriptor(int idSuscriptor)
        //{
        //    DataRow suscriptor = BuscarSuscriptor(idSuscriptor).Rows[0];
        //    //vista.Suscriptor = suscriptor["Nombre"].ToString();
        //    //vista.TipoDoc = suscriptor["TipoDoc"].ToString();
        //    //vista.DocSolicitante = suscriptor["NumeroDoc"].ToString();
        //}
        public void ObtenerIdSuscriptor(string Suscriptor)
        {
            SuscriptorRepositorio repositorio = new SuscriptorRepositorio(udt);
            IEnumerable<SuscriptorDTO> sus = repositorio.ObtenerSuscriptoresPorNombresDTO(Suscriptor);
            vista.idIntermediario = sus.First().Id;
        }
  
        

        public void LlenarSuscriptor(int idSuscriptor)
        {
            IEnumerable<CasoDTO> datosSuscriptor;
            DataTable table = new DataTable();
            string filtrosBusqueda = " WHERE (TB_CasosExternos.IdSuscriptor = " + vista.UsuarioActual.SuscriptorSeleccionado.Id + " OR TB_CasosExternos.IdSuscriptorProv = " + vista.UsuarioActual.SuscriptorSeleccionado.Id + ")";
            CasoRepositorio repositorioCasos = new CasoRepositorio(udt);
            datosSuscriptor = repositorioCasos.ObtenerSuscriptoresPaConsultaDTO(vista.UsuarioActual.SuscriptorSeleccionado.Id, filtrosBusqueda);
            
            var vistaComboSuscriptores = (from S in datosSuscriptor
                                          select new
                                          {
                                              IdSuscriptor = S.IdSuscriptor,
                                              NombreSuscriptor = S.NombreSuscriptor
                                          }).Distinct();

            DataView view = new DataView(LinqtoDataSetMethods.CopyToDataTable(vistaComboSuscriptores));
            table = view.ToTable(true, "IdSuscriptor", "NombreSuscriptor");

            DataRow suscriptor = BuscarSuscriptor(idSuscriptor).Rows[0];
           int cont = 0;
           for (int i = 0; i < table.Rows.Count; i++)
			{
               if(table.Rows[i][0].ToString() == idSuscriptor.ToString() && table.Rows[i][1].ToString() == suscriptor["Nombre"].ToString())
                   cont = cont +1;
			 
			}
	              
            if(cont == 0)
              table.Rows.Add(idSuscriptor, suscriptor["Nombre"].ToString());
           
            vista.ComboSuscriptores = table;
        }
        
        private DataTable BuscarSuscriptor(int idSuscriptor)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerSuscriptorPorIDCompletoVigentesYNoVigentes(idSuscriptor);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
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
            return null;
        }

        private DataTable BuscarSupervisados(int idUsuarioSuscriptor)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerSupervisados(idUsuarioSuscriptor);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
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
            return null;
        }
        
        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarComboServicios(int idSuscriptor)
        {
            try
            {
                FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(udt);
                IEnumerable<FlujosServicioDTO> listadoFlujoServicio = null;
                listadoFlujoServicio = repositorioFlujosServicio.ObtenerDTOporSuscriptorSinAuditoria(idSuscriptor);
                List<FlujosServicioDTO> listadoFlujoServicioA = listadoFlujoServicio.ToList();
                DataTable listadoIdSubServicio = BuscarNombreServicios(idSuscriptor);
                for (int i = 0; i < listadoFlujoServicioA.Count; i++)
                {
                    int j;
                    bool find = false;
                    for (j = 0; j < listadoIdSubServicio.Rows.Count && find == false; j++)
                        if (listadoFlujoServicioA[i].IdServicioSuscriptor == int.Parse(listadoIdSubServicio.Rows[j]["Id"].ToString()))
                            find = true;
                    if (find == true)
                        listadoFlujoServicioA[i].NombreServicioSuscriptor = listadoIdSubServicio.Rows[j - 1]["Nombre"].ToString();
                    else
                        listadoFlujoServicioA[i].NombreServicioSuscriptor = "";
                }

                var todos = (from D in listadoFlujoServicioA
                             where D.NombreServicioSuscriptor != ""
                             select new FlujosServicioDTO
                             {
                                 Id = D.Id,
                                 IdServicioSuscriptor = D.IdServicioSuscriptor,
                                 NombreServicioSuscriptor = D.NombreServicioSuscriptor
                             });
                vista.ComboServicios = todos;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarComboEstatus()
        {
            try
            {
                vista.ComboEstatus = BuscarEstatus();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void LlenarComboSuscriptorASimular(int idsuscriptor)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ProveedoresServiciosSimulados(idsuscriptor);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    vista.ComboSuscriptorASimular = ds.Tables[0];              
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

        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarComboServicioSimulado()
        {
            try
            {
                ServiciosSimuladoRepositorio repositorio = new ServiciosSimuladoRepositorio(udt);
                vista.ComboServicios = repositorio.ObtenerServicioSuscriptor(vista.UsuarioActual.SuscriptorSeleccionado.Id);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        private DataTable BuscarEstatus()
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerListaValorPorNombre("Estatus del Caso");
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
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
            return null;
        }

        private DataTable BuscarNombreServicios(int idSuscriptor)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerServiciosPorIdSuscriptor(idSuscriptor);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];

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
            return null;
        }

    }
}
