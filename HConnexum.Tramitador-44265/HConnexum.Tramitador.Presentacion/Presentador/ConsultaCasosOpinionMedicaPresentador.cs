using System;
using System.Collections.Generic;
using System.Linq;
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
	public class ConsultaCasosOpinionMedicaPresentador : PresentadorListaBase<Paso>
	{
		///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
        readonly IConsultaCasosOpinionMedica vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		bool supervisor = false;
		int? idcaso = null;
		int? idsolicitud = null;
		int? idcasoexterno3 = null;
		IList<int> idsSupervisados = new List<int>();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ConsultaCasosOpinionMedicaPresentador(IConsultaCasosOpinionMedica vista)
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
				Filtro tes = new Filtro();
				string filtrosBusqueda = "";
				DateTime fechaInicial;
				DateTime fechaFinal;

				#region Filtros de Busquedas

				if (!string.IsNullOrEmpty(vista.Asegurado))
				{
					tes.Campo = "nomAseg";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(string);
					tes.Valor = (vista.Asegurado);
					parametrosFiltro.Add(tes);
				}

				if (vista.TipoFiltro.Trim() != "" && !string.IsNullOrEmpty(vista.Filtro))
				{
					#region Filtros
					if (vista.TipoFiltro == "Id")
					{
						tes.Campo = "Id";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(int);
						tes.Valor = int.Parse(vista.Filtro);
						parametrosFiltro.Add(tes);
					}
					else if (vista.TipoFiltro == "SupportIncident")
					{

						tes.Campo = "SupportIncident";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(string);
						tes.Valor = (vista.Filtro);
						parametrosFiltro.Add(tes);
					}
					else if (vista.TipoFiltro == "NumDocSolicitante")
					{
						tes.Campo = "NumDocSolicitante";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(string);
						tes.Valor = (vista.Filtro);
						parametrosFiltro.Add(tes);
					}
					#endregion
				}

				if (vista.FechaInicial.Trim() != "" && vista.FechaFinal != "")
				{
					fechaInicial = DateTime.Parse(vista.FechaInicial);
					fechaFinal = DateTime.Parse(vista.FechaFinal);

					filtrosBusqueda = filtrosBusqueda + " and  FechaCreacion BETWEEN '" + String.Format("{0:dd/MM/yyyy}", fechaInicial) + "' AND '" + String.Format("{0:dd/MM/yyyy}", fechaFinal) + "'";
				}

				if (vista.FechaInicial.Trim() != "")
				{
					fechaInicial = DateTime.Parse(vista.FechaInicial);
					tes.Campo = "FechaCreacion";
					tes.Operador = "GreaterThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(vista.FechaInicial);
					parametrosFiltro.Add(tes);
					filtrosBusqueda = filtrosBusqueda + " and FechaCreacion = '" + String.Format("{0:dd/MM/yyyy}", fechaInicial) + "'";
				}
				if (vista.FechaFinal != "")
				{
					fechaFinal = DateTime.Parse(vista.FechaFinal);
					tes.Campo = "FechaCreacion";
					tes.Operador = "LessThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(vista.FechaFinal).AddMinutes(1439);
					parametrosFiltro.Add(tes);
					if (DateTime.Now.ToShortDateString() != vista.FechaFinal)
						filtrosBusqueda = filtrosBusqueda + " and  FechaCreacion BETWEEN '" + String.Format("{0:dd/MM/yyyy}", DateTime.Now) + "' AND '" + String.Format("{0:dd/MM/yyyy}", fechaFinal) + "'";
				}
				#endregion
				
					CasoRepositorio repositorioCasos = new CasoRepositorio(unidadDeTrabajo);
					IEnumerable<ConsultaOpinionMedicaDTO> datos = repositorioCasos.ObtenerCasosOpinionMedicaDTO(orden, pagina, tamañoPagina, parametrosFiltro, vista.UsuarioActual.SuscriptorSeleccionado.Id);
					this.vista.NumeroDeRegistros = repositorioCasos.Conteo;
					this.vista.Datos = datos;
				
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
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


		private DataTable BuscarSuscriptor(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerSuscriptorPorIDCompletoVigentesYNoVigentes(idSuscriptor);
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
	}
}