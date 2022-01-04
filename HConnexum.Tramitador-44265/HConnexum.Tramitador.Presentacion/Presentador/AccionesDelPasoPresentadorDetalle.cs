using System;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase FlujosEjecucionPresentadorDetalle.</summary>
    public class AccionesDelPasoPresentadorDetalle : PresentadorDetalleBase<FlujosEjecucion>
    {
        ///<summary>Variable vista de la interfaz IFlujosEjecucionDetalle.</summary>
        readonly IAccionesDelPasoDetalle vista;
        ///<summary>Variable de la entidad FlujosEjecucion.</summary>
        FlujosEjecucion _FlujosEjecucion;
        //FlujosServicioDTO _FlujosServicio;
        //PasoDTO _Paso;
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public AccionesDelPasoPresentadorDetalle(IAccionesDelPasoDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(AccionDetalle accion)
        {
            try
            {
                FlujosEjecucionRepositorio repositorio = new FlujosEjecucionRepositorio(udt);
                _FlujosEjecucion = repositorio.ObtenerPorId(vista.Id);
                vista.IdEtapa = _FlujosEjecucion.PasosRepuesta.Paso.Etapa.Id;
                vista.IdPasoOrigen = _FlujosEjecucion.PasosRepuesta.IdPaso;
                PresentadorAVista();
                if (accion == AccionDetalle.Modificar && _FlujosEjecucion.PasosRepuesta.Paso.Etapa.FlujosServicio.IndVigente == true)
                    vista.ErroresCustomEditar = "El registro seleccionado no puede ser Editado debido a que el Flujo asociado está actualmente Activo";
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
		
        ///<summary>Método encargado de guardar los cambios en BD.</summary>
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        public void GuardarCambios(AccionDetalle accion)
        {
            try
            {
                string errores = ValidarDatos();
                if (errores.Length == 0)
                {
                    udt.IniciarTransaccion();
                    FlujosEjecucionRepositorio repositorio = new FlujosEjecucionRepositorio(udt);
					
                    if (accion == AccionDetalle.Agregar)
                    {
                        _FlujosEjecucion = new FlujosEjecucion();
                        VistaAPresentador(accion);
                        udt.MarcarNuevo(_FlujosEjecucion);
                    }
                    else
                    {
                        _FlujosEjecucion = repositorio.ObtenerPorId(vista.Id);
                        VistaAPresentador(accion);
                        udt.MarcarModificado(_FlujosEjecucion);
                    }
                    udt.Commit();
                }
                else
                    vista.Errores = errores;
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
				
        ///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
        private void PresentadorAVista()
        {
            try
            {
                vista.IdPasoRespuesta = _FlujosEjecucion.IdPasoRespuesta;
                vista.IdPasoDestino = _FlujosEjecucion.IdPasoDestino.Value;
                vista.Condicion = string.Format("{0:N0}", _FlujosEjecucion.Condicion);
                if (_FlujosEjecucion.IdPasoDesborde!=null)
                vista.IdPasoDesborde = _FlujosEjecucion.IdPasoDesborde.Value;
                vista.IndReinicioRepeticion = _FlujosEjecucion.IndReinicioRepeticion.ToString();
                CargarPublicacion();
                CargarAuditoria();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
		
        ///<summary>Método encargado de asignar valores a propiedades de la entidad desde la vista.</summary>	
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        private void VistaAPresentador(AccionDetalle accion)
        {
            try
            {
                if (vista.IdPasoDestino != 0)
                    _FlujosEjecucion.IdPasoDestino = vista.IdPasoDestino;
                else _FlujosEjecucion.IdPasoDestino = null;
                
                if (vista.IdPasoRespuesta != 0)
                _FlujosEjecucion.IdPasoRespuesta = vista.IdPasoRespuesta;
                
                if (vista.Condicion != "")
                    _FlujosEjecucion.Condicion = int.Parse(vista.Condicion);

                if (vista.IdPasoDesborde != 0)
                    _FlujosEjecucion.IdPasoDesborde = vista.IdPasoDesborde;
                else _FlujosEjecucion.IdPasoDesborde = null;

                _FlujosEjecucion.IndReinicioRepeticion = bool.Parse(vista.IndReinicioRepeticion);
                AsignarAuditoria(accion);
                AsignarPublicacion();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
		
        ///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
        /// <returns>Devuelve mensaje(s) con los datos validados.</returns>
        protected string ValidarDatos()
        {
            StringBuilder errores = new StringBuilder(); 
            try
            {
                Metadata<FlujosEjecucion> metadata = new Metadata<FlujosEjecucion>();
                errores.AppendWithBreak(metadata.ValidarPropiedad("Condicion", vista.Condicion));
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            return errores.ToString();
        }
		
        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarCombos(string idFlujosServicio)
        {
            try
            {
                EtapaRepositorio repositorioEtapa = new EtapaRepositorio(udt);
                vista.ComboIdEtapa = repositorioEtapa.ObtenerDTOEtapa(int.Parse(idFlujosServicio));
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        public void LlenarCombosddlIdPasoOrigen(int idEtapa, int idFlujoServicio, AccionDetalle accion)
        {
            try
            {
                PasoRepositorio repositorioPaso = new PasoRepositorio(udt);
                vista.ComboIdPasoOrigen = repositorioPaso.ObtenerDTOPasosEtapa(idEtapa, idFlujoServicio);
                vista.ComboIdPasoDesborde = repositorioPaso.ObtenerDTOPasoDesborde(idFlujoServicio);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        public void LlenarCombosddlIdTipoPaso(int idFlujoServicio)
        {
            try
            {
                TipoPasoRepositorio repositorioTipoPaso = new TipoPasoRepositorio(udt);
                vista.ComboIdTipoPaso = repositorioTipoPaso.ObtenerDTOTipoPaso(idFlujoServicio);
                
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        public void LlenarCombosddlIdTipoPasoModificar(int idFlujoServicio)
        {
            try
            {
                TipoPasoRepositorio repositorioTipoPaso = new TipoPasoRepositorio(udt);
                vista.ComboIdTipoPaso = repositorioTipoPaso.ObtenerDTOTipoPasoModificar(idFlujoServicio);

            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }


        public void LlenarCombosddlIdPasoDestino(int idTipoPaso, int idFlujoServicio, AccionDetalle accion)
        {
            try
            {
                PasoRepositorio RepositorioPaso = new PasoRepositorio(udt);
                vista.ComboIdPasoDestino = RepositorioPaso.ObtenerDTOProximoPaso(idFlujoServicio, idTipoPaso);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        public void LlenarCombosddlIdPasoDestinoModificar(int idPasoDestino)
        {
            try
            {
                PasoRepositorio RepositorioPaso = new PasoRepositorio(udt);
                vista.ComboIdPasoDestino = RepositorioPaso.ObtenerDTOProximoPasoModificarAccionDelPaso(idPasoDestino);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        public void LlenarCombosddlIdRespuesta(int idPasoOrigen, AccionDetalle accion)
        {
            try
            {
                PasosRepuestaRepositorio repositorioRespuesta = new PasosRepuestaRepositorio(udt);
                vista.ComboddlIdPasoRespuesta = repositorioRespuesta.ObtenerDTOPasoRespuesta(idPasoOrigen);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        ///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
        private void CargarPublicacion()
        {
            try
            {
                vista.IndVigente = _FlujosEjecucion.IndVigente.ToString();
                vista.FechaValidez = _FlujosEjecucion.FechaValidez.ToString();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        ///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
        private void CargarAuditoria()
        {
            try
            {
                vista.CreadoPor = this.ObtenerNombreUsuario(_FlujosEjecucion.CreadoPor);
                vista.FechaCreacion = _FlujosEjecucion.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_FlujosEjecucion.ModificadoPor);
                vista.FechaModificacion = _FlujosEjecucion.FechaModicacion.ToString();
                vista.IndEliminado = _FlujosEjecucion.IndEliminado.ToString();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
		
        ///<summary>Método encargado de asignar valores de publicación a la entidad.</summary>
        public void AsignarPublicacion()
        {
            try
            {
                if (string.IsNullOrEmpty(vista.FechaValidez))
                    _FlujosEjecucion.FechaValidez = null;
                else
                    _FlujosEjecucion.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _FlujosEjecucion.IndVigente = bool.Parse(vista.IndVigente);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        ///<summary>Método encargado de asignar valores de auditoria a la entidad.</summary>
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        private void AsignarAuditoria(AccionDetalle accion)
        {
            try
            {
                if (accion == AccionDetalle.Agregar)
                {
                    _FlujosEjecucion.IndEliminado = false;
                    _FlujosEjecucion.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _FlujosEjecucion.FechaCreacion = DateTime.Now;
                    _FlujosEjecucion.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _FlujosEjecucion.FechaModicacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _FlujosEjecucion.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _FlujosEjecucion.FechaModicacion = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
    }
}