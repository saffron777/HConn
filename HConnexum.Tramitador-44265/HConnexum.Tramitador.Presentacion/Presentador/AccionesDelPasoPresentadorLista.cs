using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Configuration;
using System.Data;

///<summary>Namespace que engloba el presentador Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase FlujosEjecucionPresentadorLista.</summary>
    public class AccionesDelPasoPresentadorLista : PresentadorListaBase<FlujosEjecucion>
    {
        ///<summary>Variable vista de la interfaz IAccionesDelPasoLista.</summary>
        IAccionesDelPasoLista vista;
        readonly FlujosServicio _FlujosServicio;
		
		///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public AccionesDelPasoPresentadorLista(IAccionesDelPasoLista vista)
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
	            FlujosEjecucionRepositorio repositorio = new FlujosEjecucionRepositorio(unidadDeTrabajo); 
	            IEnumerable<FlujosEjecucionDTO> datos = repositorio.ObtenerDTOListarAccionesDelPaso(orden, pagina, tamañoPagina, parametrosFiltro, vista.Id);
				vista.NumeroDeRegistros = repositorio.Conteo;
	            vista.Datos = datos;
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
	            unidadDeTrabajo.IniciarTransaccion();
	            FlujosEjecucionRepositorio repositorio = new FlujosEjecucionRepositorio(unidadDeTrabajo);
	            foreach(string id in ids)
	            {
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
	                FlujosEjecucion _FlujosEjecucion = repositorio.ObtenerPorId(idDesencriptado);
	                repositorio.EliminarLogico(_FlujosEjecucion);
	            }
	            unidadDeTrabajo.Commit();
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
        }

        public void ServicioParametrizador()
        {

            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                FlujosServicioRepositorio repositorioFlujoServicioEtapa = new FlujosServicioRepositorio(unidadDeTrabajo);
                FlujosServicioDTO _FlujosServicio = repositorioFlujoServicioEtapa.ObtenerDTOServicioFlujoServicio(vista.Id);
                DataSet ds = servicio.ObtenerServicioSuscriptorPorId(_FlujosServicio.IdServicioSuscriptor);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    vista.Servicio = ds.Tables[0].Rows[0][1].ToString();
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

        ///<summary>Método encargado de llenar los textbox a partir de la seleccion de un item del combo.</summary>
        public void LlenarTextBox()
        {
            try
            {

                FlujosServicioRepositorio repositorioFlujoServicio = new FlujosServicioRepositorio(unidadDeTrabajo);
                FlujosServicioDTO _FlujosServicio = repositorioFlujoServicio.ObtenerDTOServicioFlujoServicio(vista.Id);
                //vista.Servicio = "Servicio";
                vista.Version = _FlujosServicio.Version.ToString();
                if (_FlujosServicio.IndVigente== true)
                {
                    vista.Estatus = "Vigente";
                }
                    else
                    {
                     vista.Estatus = "Inactivo";
                    }
               }
                
            
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public bool bObtenerRolIndEliminado()
        {
            FlujosEjecucionRepositorio repositorio = new FlujosEjecucionRepositorio(this.unidadDeTrabajo);
            return repositorio.obtenerRolIndEliminado();
        }

        public void activarEliminado(IList<string> ids)
        {
            try
            {
                this.unidadDeTrabajo.IniciarTransaccion();
                FlujosEjecucionRepositorio repositorio = new FlujosEjecucionRepositorio(this.unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    FlujosEjecucion _Aplicacion = repositorio.ObtenerPorId(idDesencriptado);
                    if (!repositorio.activarEliminarLogico(_Aplicacion, _Aplicacion.Id))
                        this.vista.Errores = "Ya Existen registros asociados a esta Aplicacion";
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