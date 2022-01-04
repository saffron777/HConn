using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Configuration;

///<summary>Namespace que engloba el presentador Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase CasoPresentadorLista.</summary>
	public class AgruparCasoPresentadorLista : PresentadorListaBase<Caso>
	{
		///<summary>Variable vista de la interfaz ICasoLista.</summary>
		readonly IAgruparCasoLista vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public AgruparCasoPresentadorLista(IAgruparCasoLista vista)
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
				Filtro tes = new Filtro();
				if(vista.Caso != "")
				{
					tes.Campo = "Id"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.Caso);
					parametrosFiltro.Add(tes);
				}
				if(vista.CodDocumento != "")
				{
					tes.Campo = "NumDocSolicitante"; tes.Operador = "EqualTo"; tes.Tipo = typeof(string); tes.Valor = vista.CodDocumento;
					parametrosFiltro.Add(tes);
				}
				if(vista.IdSuscriptor != "")
				{
					tes.Campo = "Idsuscriptor"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdSuscriptor);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdEstatus != "")
				{
					tes.Campo = "Estatus"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdEstatus);
					parametrosFiltro.Add(tes);
				}
				if(vista.FechaDesde.Trim() != "")
				{
					tes.Campo = "FechaCreacion"; tes.Operador = "GreaterThanOrEqualTo"; tes.Tipo = typeof(DateTime); tes.Valor = DateTime.Parse(vista.FechaDesde);
					parametrosFiltro.Add(tes);
				}
				if(vista.FechaHasta != "")
				{
					tes.Campo = "FechaCreacion"; tes.Operador = "LessThanOrEqualTo"; tes.Tipo = typeof(DateTime); tes.Valor = DateTime.Parse(vista.FechaHasta);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdCreadoPor != "")
				{
					tes.Campo = "CreadorPor"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdCreadoPor);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdServicio != "")
				{
					tes.Campo = "IdFlujoServicio"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdServicio);
					parametrosFiltro.Add(tes);
				}
				CasoRepositorio repositorio = new CasoRepositorio(this.unidadDeTrabajo);
				IEnumerable<CasoDTO> datos = repositorio.ObtenerGridAgruparDTO(orden, pagina, tamañoPagina, parametrosFiltro);
				this.vista.NumeroDeRegistros = repositorio.Conteo;
				this.vista.Datos = datos;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombos()
		{

			CasoRepositorio repositorio = new CasoRepositorio(this.unidadDeTrabajo);

			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds2 = servicio.ObtenerListaValorPorNombre("Estatus del Caso");
				if(ds2.Tables[@"Error"] != null)
					throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds2.Tables[0].Rows.Count > 0)
					vista.ComboIdEstatus = ds2.Tables[0];

				FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(udt);
				IEnumerable<FlujosServicioDTO> listadoFlujoServicio = repositorioFlujosServicio.ObtenerDTO();
				vista.ComboIdServicio = listadoFlujoServicio.ToList();

				vista.Suscriptor = repositorio.ObtenerSuscriptoresDTO();

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
		}

		private DataTable BuscarUsuarios(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerUsuariosPorIdSuscriptor(idSuscriptor);
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

		public void LlenarComboCreadoPor()
		{
			vista.ComboCreadoPor = BuscarUsuarios(int.Parse(vista.IdSuscriptor));
		}
		///<summary>Método encargado de eliminar registros del conjunto.</summary>
		///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
		public void Eliminar(IList<string> ids)
		{
			try
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				CasoRepositorio repositorio = new CasoRepositorio(this.unidadDeTrabajo);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					Caso _Caso = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.EliminarLogico(_Caso, _Caso.Id))
						this.vista.Errores = "Ya Existen registros asociados a la entidad Caso";
				}
				this.unidadDeTrabajo.Commit();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}