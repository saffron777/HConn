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
    ///<summary>Clase PasosRepuestaPresentadorDetalle.</summary>
    public class PasosRepuestaPresentadorDetalle : PresentadorDetalleBase<PasosRepuesta>
    {
        ///<summary>Variable vista de la interfaz IPasosRepuestaDetalle.</summary>
        IPasosRepuestaDetalle vista;
        ///<summary>Variable de la entidad PasosRepuesta.</summary>
        PasosRepuesta _PasosRepuesta;
        UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public PasosRepuestaPresentadorDetalle(IPasosRepuestaDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(AccionDetalle accion)
        {
            try
            {
                PasosRepuestaRepositorio repositorio = new PasosRepuestaRepositorio(udt);
                _PasosRepuesta = repositorio.ObtenerPorId(vista.Id);
                PresentadorAVista();
                if (accion == AccionDetalle.Modificar && _PasosRepuesta.Paso.Etapa.FlujosServicio.IndVigente == true)
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
                    PasosRepuestaRepositorio repositorio = new PasosRepuestaRepositorio(udt);
                    if (accion == AccionDetalle.Agregar)
                    {
                        _PasosRepuesta = new PasosRepuesta();
                        VistaAPresentador(accion);
                        udt.MarcarNuevo(_PasosRepuesta);
                    }
                    else
                    {
                        _PasosRepuesta = repositorio.ObtenerPorId(vista.Id);
                        VistaAPresentador(accion);
                        udt.MarcarModificado(_PasosRepuesta);
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
                vista.ValorRespuesta = _PasosRepuesta.ValorRespuesta;
                vista.DescripcionRespuesta = _PasosRepuesta.DescripcionRespuesta;
                vista.Secuencia = _PasosRepuesta.Orden;
                vista.IndCierre = _PasosRepuesta.IndCierre.ToString();
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
                _PasosRepuesta.ValorRespuesta = vista.ValorRespuesta;
                _PasosRepuesta.DescripcionRespuesta = vista.DescripcionRespuesta;
                _PasosRepuesta.Orden = vista.Secuencia;
                _PasosRepuesta.IndCierre = bool.Parse(vista.IndCierre);
                if (accion == AccionDetalle.Agregar)
                    _PasosRepuesta.IdPaso = vista.Id;
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
                Metadata<PasosRepuesta> metadata = new Metadata<PasosRepuesta>();
                errores.AppendWithBreak(metadata.ValidarPropiedad("ValorRespuesta", vista.ValorRespuesta));
                errores.AppendWithBreak(metadata.ValidarPropiedad("DescripcionRespuesta", vista.DescripcionRespuesta));
                errores.AppendWithBreak(metadata.ValidarPropiedad("Orden", vista.Secuencia));
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
		
        ///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
        private void CargarPublicacion()
        {
            try
            {
                vista.IndVigente = _PasosRepuesta.IndVigente.ToString();
                vista.FechaValidez = _PasosRepuesta.FechaValidez.ToString();
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
                vista.CreadoPor = this.ObtenerNombreUsuario(_PasosRepuesta.CreadoPor);
                vista.FechaCreacion = _PasosRepuesta.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_PasosRepuesta.ModificadoPor);
                vista.FechaModificacion = _PasosRepuesta.FechaModificacion.ToString();
                vista.IndEliminado = _PasosRepuesta.IndEliminado.ToString();
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
                    _PasosRepuesta.FechaValidez = null;
                else
                    _PasosRepuesta.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _PasosRepuesta.IndVigente = bool.Parse(vista.IndVigente);
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
                    _PasosRepuesta.IndEliminado = false;
                    _PasosRepuesta.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _PasosRepuesta.FechaCreacion = DateTime.Now;
                    _PasosRepuesta.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _PasosRepuesta.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _PasosRepuesta.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _PasosRepuesta.FechaModificacion = DateTime.Now;
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