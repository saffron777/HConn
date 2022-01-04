using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PresentadorReporteCartaAval : PresentadorListaBase<Paso>
	{
		///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
		readonly IReporteCartaAval vista;
		IEnumerable<ConsultaCartaAvalDTO> datos;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		bool supervisor = false;
		IList<int> idsSupervisados = new List<int>();
		readonly string fechadesde = "";
		readonly string fechahasta = "";
		readonly DateTime fechaInicial = DateTime.Now.AddMonths(-6);
		readonly DateTime fechafinal = DateTime.Now.AddDays(1);

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PresentadorReporteCartaAval(IReporteCartaAval vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = this.GetType();
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
				int numpagina = pagina;
				int cantregistros = tamañoPagina;
				Filtro tes = new Filtro();

				if (this.vista.TipoFiltro.Trim() != "" && !string.IsNullOrEmpty(this.vista.Filtro))
				{
					
					#region Filtros
					
					if (this.vista.TipoFiltro == "Id")
					{
						tes.Campo = "Id";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(int);
						tes.Valor = int.Parse(this.vista.Filtro);
						parametrosFiltro.Add(tes);
					}
					else if (this.vista.TipoFiltro == "SupportIncident")
					{
						tes.Campo = "SupportIncident";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(string);
						tes.Valor = this.vista.Filtro;
						parametrosFiltro.Add(tes);
					}
					else if (this.vista.TipoFiltro == "cedula")
					{
						tes.Campo = "NumDocSolicitante";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(string);
						tes.Valor = this.vista.Filtro;
						parametrosFiltro.Add(tes);
					}

					#endregion
				
				}
					
				if (!string.IsNullOrEmpty(this.vista.FechaDesde))
				{
					tes.Campo = "Fechasolicitud";
					tes.Operador = "GreaterThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(this.vista.FechaDesde);
					parametrosFiltro.Add(tes);
				}
					
				if (!string.IsNullOrEmpty(this.vista.FechaHasta))
				{
					tes.Campo = "Fechasolicitud";
					tes.Operador = "LessThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(this.vista.FechaHasta).AddDays(+1);
					parametrosFiltro.Add(tes);
				}
					
				if (!string.IsNullOrEmpty(this.vista.Asegurado))
				{
					tes.Campo = "nomAseg";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(string);
					tes.Valor = (this.vista.Asegurado);
					parametrosFiltro.Add(tes);
				}
					
				if (!string.IsNullOrEmpty(this.vista.Intermediario))
				{
					tes.Campo = "Intermediario";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(string);
					tes.Valor = (this.vista.Intermediario);
					parametrosFiltro.Add(tes);
				}
				
				CasoRepositorio repositorioCasos = new CasoRepositorio(this.udt);
				this.datos = repositorioCasos.ObtenerCasosCartaAvalDTO(orden, pagina, tamañoPagina, parametrosFiltro, this.vista.UsuarioActual.SuscriptorSeleccionado.Id);
				this.vista.NumeroDeRegistros = repositorioCasos.Conteo;
				this.vista.Datos = this.datos;
			}
			catch (Exception e)
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
				return string.Format("{0}{1}", string.Empty, valor);
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
			return string.Empty;
		}
			
		public void ComprobarSupervisor(int idUsuario, int idUsuarioSuscriptor)
		{
			DataTable supervisados = this.BuscarSupervisados(idUsuarioSuscriptor);
			if (supervisados != null)
			{
				this.idsSupervisados = this.ConvertToIList(supervisados);
				this.supervisor = true;
			}
			else
			{
				UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio(this.udt);
				UsuarioDTO usuario = usuarioRepositorio.ObtenerDTO(idUsuario);
			}
		}
			
		private IList<int> ConvertToIList(DataTable dataTable)
		{
			IList<int> lista = new List<int>();
			foreach (DataRow row in dataTable.Rows)
			{
				lista.Add(int.Parse(row["Id"].ToString()));
			}
			return lista;
		}
			
		private DataTable BuscarSupervisados(int idUsuarioSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerSupervisados(idUsuarioSuscriptor);
				if (ds.Tables[@"Error"] != null)
				{
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				}
				if (ds.Tables[0].Rows.Count > 0)
				{
					return ds.Tables[0];
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
			return null;
		}
			
		public string ObtenerUrlReporteCartaAval(int idintermediario, string datoParticular)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			string url = servicio.ObtenerUrlReporteCartaAval(idintermediario, datoParticular).Tables[0].Rows[0][@"Url"].ToString();
			if (!string.IsNullOrWhiteSpace(url))
			{
				return url;
			}
			return string.Empty;
		}
			
		public Caso ObtenerCAso(int idcaso) 
		{
			CasoRepositorio repositorio = new CasoRepositorio(this.udt);
			Caso CasoCA = new Caso();
			CasoCA = repositorio.ObtenerPorId(idcaso);
			return CasoCA;
		}
	}
}