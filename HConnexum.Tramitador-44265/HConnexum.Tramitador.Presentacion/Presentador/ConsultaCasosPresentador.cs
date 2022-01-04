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

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase ConsultaCasosPresentador.</summary>
	public class ConsultaCasosPresentador : PresentadorListaBase<Paso>
	{
		///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
		readonly IConsultaCasos vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		bool supervisor = false;
		IList<int> idsSupervisados = new List<int>();
        int[] listaPoteMov = new int[0];
        int? idsuscriptor = null;
        bool incluirmvtos = false;
        int? idcaso = null;
        int? idcasoexterno = null;
        int? idcasoexterno2 = null;
        int? idcasoexterno3 = null;
        string idticket = null;
        string numdoctit = null;
        int? tipobusqueda = 3;
        DateTime fechaInicial = DateTime.Now.AddMonths(-6);
        string fechadesde = "";
        DateTime fechafinal = DateTime.Now.AddDays(1);
        string fechahasta = "";
        string asegurado = null;
        string intermediario = null;
        int? servicio = null;
        int? estatus = null;
        int idSuscriptorconectado;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public ConsultaCasosPresentador(IConsultaCasos vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
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

                    idSuscriptorconectado = this.vista.UsuarioActual.SuscriptorSeleccionado.Id;

                    if (vista.IdComboSuscriptorASimular.Trim() != "")
                    {
                        idSuscriptorconectado = int.Parse(vista.IdComboSuscriptorASimular); 
                    }  
                  if (vista.IdComboSuscriptores.Trim() != "")
                    {
                        idsuscriptor = int.Parse(vista.IdComboSuscriptores);                        
                        
                    }

                   

                    if (vista.TipoFiltro.Trim() != "" && !string.IsNullOrEmpty(vista.Filtro))
                    {
                        #region Filtros

                        if (vista.TipoFiltro == "Id")
                        {
                            idcaso = int.Parse(vista.Filtro);
                        }
                        else if (vista.TipoFiltro == "Idcasoexterno")
                        {
                            idcasoexterno = int.Parse(vista.Filtro);
                        }
                        else if (vista.TipoFiltro == "SupportIncident")
                        {
                            idcasoexterno3 = int.Parse(vista.Filtro);
                        }
                        else if (vista.TipoFiltro == "Ticket")
                        {
                            idticket = vista.Filtro;
                        }
                        else if (vista.TipoFiltro == "NumDocSolicitante")
                        {
                            numdoctit = vista.Filtro;
                        }
                        #endregion
                    }
                  
                    if (!string.IsNullOrEmpty(vista.IdComboServicios))
                    {
                       servicio = int.Parse(vista.IdComboServicios);
                       
                    }

                    if (!string.IsNullOrEmpty(vista.FechaDesde))
                    {
                        DateTime Fecha = DateTime.Parse(vista.FechaDesde);
                        fechadesde = Fecha.Month.ToString() + "/" + Fecha.Day.ToString() + "/" + Fecha.Year.ToString();
                    }
                    else
                    {
                        fechadesde = fechaInicial.Month.ToString() + "/" + fechaInicial.Day.ToString() + "/2000" ;
                    }
                    if (!string.IsNullOrEmpty(vista.FechaHasta))
                    {
                        DateTime Fecha1 = DateTime.Parse(vista.FechaHasta).AddDays(1);
                        fechahasta = Fecha1.Month.ToString() + "/" + Fecha1.Day.ToString() + "/" + Fecha1.Year.ToString();
                    }
                    else
                    {
                        fechahasta = fechafinal.Month.ToString() + "/" + fechafinal.Day.ToString() + "/" + fechafinal.Year.ToString();
                    }
                    if (!string.IsNullOrEmpty(vista.IdComboEstatus ))
                    {
                        estatus = int.Parse(vista.IdComboEstatus);
                    }
                    if (!string.IsNullOrEmpty(vista.Asegurado))
                    {
                        asegurado = vista.Asegurado;
                    }
                    if (!string.IsNullOrEmpty(vista.Intermediario))
                    {
                        intermediario = vista.Intermediario;
                    }
                    #endregion
                    CasoRepositorio repositorioCasos = new CasoRepositorio(unidadDeTrabajo);
                    this.vista.Datos = repositorioCasos.ObtenerDTO(orden, pagina, tamañoPagina, idsuscriptor, servicio, incluirmvtos, idcaso, idcasoexterno, idcasoexterno2, idcasoexterno3, idticket, numdoctit, asegurado, intermediario, tipobusqueda, fechadesde, fechahasta, listaPoteMov, estatus, idSuscriptorconectado);
                    this.vista.NumeroDeRegistros = repositorioCasos.Conteo;
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
        public void limpiaDatos()
        {
            IEnumerable<CasoDTO> _datos = Enumerable.Empty<CasoDTO>();
            vista.Datos = _datos;
            this.vista.NumeroDeRegistros = 0;
        }
        public void ObtenerIdSuscriptor(string Suscriptor)
        {
            SuscriptorRepositorio repositorio = new SuscriptorRepositorio(udt);
            IEnumerable<SuscriptorDTO> sus = repositorio.ObtenerSuscriptoresPorNombresDTO(Suscriptor);
            vista.idIntermediario = sus.First().Id;
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
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return string.Empty;
		}
        public void LlenarSuscriptor(int idSuscriptor)
        {

            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {

                DataSet ds = servicio.SuscriptoresConLosQTrabajo(vista.UsuarioActual.SuscriptorSeleccionado.Id);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds != null && ds.Tables[0] != null) 
                {
                    vista.ComboSuscriptores = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                {
                    HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", ex.InnerException.Message));
                }
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                {
                    servicio.Close();
                }
            }
            
        }
        
		public bool ComprobarNubise(int id)
		{
			UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio(udt);
			SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
			if(usuarioRepositorio.ObtenerPorId(id).LoginUsuario.ToLower() == "admin")
			{
			return true;
			}
			else
				return false;
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

		public void ComprobarSupervisor(int idUsuario, int idUsuarioSuscriptor)
		{
			DataTable supervisados = BuscarSupervisados(idUsuarioSuscriptor);
			if(supervisados != null)
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
			foreach(DataRow row in dataTable.Rows)
				lista.Add(int.Parse(row["Id"].ToString()));
			return lista;
		}

		private DataTable BuscarSupervisados(int idUsuarioSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerSupervisados(idUsuarioSuscriptor);
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0];
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return null;
		}
       /// <summary>
       /// Buscar el nombre del servicio segun id de lista valor
       /// </summary>
       /// <param name="idListaValor">Id de lista valor</param>
       /// <returns></returns>
        public string BuscarValorServicio(int idListaValor)
        {
            try
            {
                ListasValorRepositorio repositorioLV = new ListasValorRepositorio(this.udt);

                if (!string.IsNullOrEmpty(repositorioLV.TipoServicio(idListaValor)))
                    return repositorioLV.TipoServicio(idListaValor);
                else
                    throw new Exception("No existe servicio asociado a este caso");
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
                return null;
            }
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
				for(int i = 0; i < listadoFlujoServicioA.Count; i++)
				{
					int j;
					bool find = false;
					for(j = 0; j < listadoIdSubServicio.Rows.Count && find == false; j++)
						if(listadoFlujoServicioA[i].IdServicioSuscriptor == int.Parse(listadoIdSubServicio.Rows[j]["Id"].ToString()))
							find = true;
					if(find == true)
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
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
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
		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboEstatus()
		{
			try
			{
				vista.ComboEstatus = BuscarEstatus();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		private DataTable BuscarEstatus()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerListaValorPorNombre("Estatus del Caso");
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0];
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
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
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0];
				
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return null;
		}

        public string BuscaUrlCasosExternos(string Servicio) 
        {
            ListasValorRepositorio repositoriosLV = new ListasValorRepositorio(udt);
            return repositoriosLV.ObtenerUrlCasosExternos(Servicio);
        }
        public void actualizarBuzonChatHC1(string idcaso)
        {

            if (!string.IsNullOrEmpty(idcaso))
            {
                int caso = int.Parse(idcaso);
                BuzonChatRepositorio repo = new BuzonChatRepositorio(udt);

                repo.ActualizoChatPorIdCaso(caso);
            }
        }
	}
}