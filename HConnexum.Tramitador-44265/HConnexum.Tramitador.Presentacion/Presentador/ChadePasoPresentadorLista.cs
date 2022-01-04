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
	///<summary>Clase ChadePasoPresentadorLista.</summary>
    public class ChadePasoPresentadorLista : PresentadorListaBase<ChadePaso>
    {
		///<summary>Variable vista de la interfaz IChadePasoLista.</summary>
        IChadePasoLista vista;
		
		///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ChadePasoPresentadorLista(IChadePasoLista vista)
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

	            ChadePasoRepositorio repositorio = new ChadePasoRepositorio(unidadDeTrabajo); 
	            IEnumerable<ChadePasoDTO> datos = repositorio.ObtenerDTOLista(orden, pagina, tamañoPagina, parametrosFiltro, vista.IdPasos);
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
	            ChadePasoRepositorio repositorio = new ChadePasoRepositorio(unidadDeTrabajo);
	            foreach(string id in ids)
	            {
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
	                ChadePaso _ChadePaso = repositorio.ObtenerPorId(idDesencriptado);
	                if(!repositorio.EliminarLogico(_ChadePaso, _ChadePaso.Id))
                    	this.vista.Errores = "Ya Existen registros asociados a esta " + _ChadePaso + "";
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
                //FlujosServicioRepositorio repositorioFlujoServicioEtapa = new FlujosServicioRepositorio(unidadDeTrabajo);
                //FlujosServicioDTO _FlujosServicio = repositorioFlujoServicioEtapa.ObtenerDTOServicio(vista.IdPasos);
                PasoRepositorio repositoriopasoEtapa = new PasoRepositorio(unidadDeTrabajo);
                PasoDTO _PasoEtapaDTO = repositoriopasoEtapa.ObtenerPorIdPaso(vista.IdPasos);
                DataSet ds = servicio.ObtenerServicioSuscriptorPorId(_PasoEtapaDTO.IdServicioSuscriptor);
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

        public void LlenarTextBox()
        {
            try
            {
                //FlujosServicioRepositorio repositorioFlujoServicio = new FlujosServicioRepositorio(unidadDeTrabajo);
                //FlujosServicioDTO _FlujosServicio = repositorioFlujoServicio.ObtenerDTOServicio(vista.IdPasos);
                PasoRepositorio repositoriopasoEtapa = new PasoRepositorio(unidadDeTrabajo);
                PasoDTO _PasoEtapaDTO = repositoriopasoEtapa.ObtenerPorIdPaso(vista.IdPasos);
                vista.NombreEtapa = _PasoEtapaDTO.NombreEtapa;    
                //vista.Servicio = "Servicio";
                vista.Version = _PasoEtapaDTO.VersionFlujoServicio.ToString();
                vista.Paso = _PasoEtapaDTO.Nombre;
                if (_PasoEtapaDTO.IndVigenteFlujoServicio == true)
                {
                    vista.Estatus = "Vigente";
                }
                else
                {
                    vista.Estatus = "Inactivo";
                }
                //FlujosServicioRepositorio repositorioFlujoSerEtaPas = new FlujosServicioRepositorio(unidadDeTrabajo);
                //FlujosServicioDTO _FlujosServicioEN = repositorioFlujoSerEtaPas.ObtenerDTOServicio(vista.IdPasos);
                //vista.Paso = _FlujosServicioEN.NombrePaso;
          
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
            ChadePasoRepositorio repositorio = new ChadePasoRepositorio(this.unidadDeTrabajo);
            return repositorio.obtenerRolIndEliminado();
        }

        public void activarEliminado(IList<string> ids)
        {
            try
            {
                this.unidadDeTrabajo.IniciarTransaccion();
                ChadePasoRepositorio repositorio = new ChadePasoRepositorio(this.unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    ChadePaso _Aplicacion = repositorio.ObtenerPorId(idDesencriptado);
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