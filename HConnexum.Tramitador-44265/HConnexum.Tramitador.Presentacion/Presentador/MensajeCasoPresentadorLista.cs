using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase BuzonMensajePresentadorLista.</summary>
	public class MensajeCasoPresentadorLista : PresentadorListaBase<BuzonMensaje>
	{
		///<summary>Variable vista de la interfaz IBuzonMensajeLista.</summary>
		readonly IMensajeCasoLista vista;
		
		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public MensajeCasoPresentadorLista(IMensajeCasoLista vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int idCaso)
		{
			try
			{
                CasoRepositorio repositorio = new CasoRepositorio(this.unidadDeTrabajo);
                IEnumerable<CasoDTO> datos = repositorio.MensajeCasoPorID(orden, pagina, tamañoPagina, parametrosFiltro, idCaso);

				this.vista.NumeroDeRegistros = repositorio.Conteo;
				this.vista.Datos = datos;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de eliminar registros del conjunto.</summary>
		///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
		public void Eliminar(IList<string> ids)
		{
			try
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				BuzonMensajeRepositorio repositorio = new BuzonMensajeRepositorio(this.unidadDeTrabajo);
				foreach(string id in ids)
				{
                    //int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    //BuzonMensaje _BuzonMensaje = repositorio.ObtenerPorId(idDesencriptado);
                    //if(!repositorio.EliminarLogico(_BuzonMensaje, _BuzonMensaje.Id))
                    //    if(Tabla2 != null)
                    //        this.vista.Errores = "Ya Existen registros asociados a la entidad BuzonMensaje";
                    //    else
                    //        this.vista.Errores = "Ya Existen registros asociados a la entidad _";
				}
				this.unidadDeTrabajo.Commit();
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}