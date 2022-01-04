using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase PasoPresentadorMaestroDetalle.</summary>
    public class PasoPresentadorMaestroDetalleBloques : PresentadorListaBase<Paso>
    {
        ///<summary>Variable vista de la interfaz IPasoMaestroDetalle.</summary>
        readonly IPasoMaestroDetalleBloques vista;

        ///<summary>Variable de la entidad Paso.</summary>
        Paso _Paso;

        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public PasoPresentadorMaestroDetalleBloques(IPasoMaestroDetalleBloques vista)
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
                parametrosFiltro.Add(new Infraestructura.Filtro { Campo = "IdPaso", Operador = "EqualTo", Tipo = typeof(int), Valor = this.vista.Id });
                PasoRepositorio repositorioMaestro = new PasoRepositorio(this.udt);
                this._Paso = repositorioMaestro.ObtenerPorId(this.vista.Id);
                this.PresentadorAVista();
                PasosBloqueRepositorio repositorioDetalle = new PasosBloqueRepositorio(unidadDeTrabajo);
                IEnumerable<PasosBloqueDTO> datos = repositorioDetalle.ObtenerDTO(orden, pagina, tamañoPagina, parametrosFiltro);
                vista.NumeroDeRegistros = repositorioDetalle.Conteo;
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
                this.unidadDeTrabajo.IniciarTransaccion();
                PasosBloqueRepositorio repositorio = new PasosBloqueRepositorio(this.unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    PasosBloque _PasosBloque = repositorio.ObtenerPorId(idDesencriptado);
                    if (!repositorio.EliminarLogico(_PasosBloque, idDesencriptado))
                        this.vista.Errores = "Ya Existen registros asociados a la entidad _PasosBloque";
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

        ///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
        private void PresentadorAVista()
        {
            try
            {
                this.vista.Id = this._Paso.Id;
                this.vista.Nombre = this._Paso.Nombre;
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
            PasosBloqueRepositorio repositorio = new PasosBloqueRepositorio(this.unidadDeTrabajo);
            return repositorio.obtenerRolIndEliminado();
        }

        public void activarEliminado(IList<string> ids)
        {
            try
            {
                this.unidadDeTrabajo.IniciarTransaccion();
                PasosBloqueRepositorio repositorio = new PasosBloqueRepositorio(this.unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    PasosBloque _Aplicacion = repositorio.ObtenerPorId(idDesencriptado);
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