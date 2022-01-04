using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase DocumentosPasoPresentadorLista.</summary>
    public class DocumentosPasoPresentadorLista : PresentadorListaBase<DocumentosPaso>
    {
        ///<summary>Variable vista de la interfaz IDocumentosPasoLista.</summary>
        readonly IDocumentosPasoLista vista;
		
		///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public DocumentosPasoPresentadorLista(IDocumentosPasoLista vista)
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
               // parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = "IdPaso", Operador = "EqualTo", Tipo = typeof(int), Valor = vista.IdPaso });
	            DocumentosPasoRepositorio repositorio = new DocumentosPasoRepositorio(unidadDeTrabajo);
                
                IEnumerable<DocumentosPasoDTO> datos = repositorio.ObtenerDTOListaDocumentosPasos(orden, pagina, tamañoPagina, parametrosFiltro, vista.IdPaso);
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
	            DocumentosPasoRepositorio repositorio = new DocumentosPasoRepositorio(unidadDeTrabajo);
	            foreach(string id in ids)
	            {
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
	                DocumentosPaso _DocumentosPaso = repositorio.ObtenerPorId(idDesencriptado);
	                if(!repositorio.EliminarLogico(_DocumentosPaso, _DocumentosPaso.Id))
                    	this.vista.Errores = "Ya Existen registros asociados a esta  _" + _DocumentosPaso + "";
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

        public bool bObtenerRolIndEliminado()
        {
            DocumentosPasoRepositorio repositorio = new DocumentosPasoRepositorio(this.unidadDeTrabajo);
            return repositorio.obtenerRolIndEliminado();
        }

        public void activarEliminado(IList<string> ids)
        {
            try
            {
                this.unidadDeTrabajo.IniciarTransaccion();
                DocumentosPasoRepositorio repositorio = new DocumentosPasoRepositorio(this.unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    DocumentosPaso _Aplicacion = repositorio.ObtenerPorId(idDesencriptado);
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