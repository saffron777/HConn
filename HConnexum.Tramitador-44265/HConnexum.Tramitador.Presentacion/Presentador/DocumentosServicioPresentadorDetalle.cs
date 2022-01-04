using System;
using System.Configuration;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Data;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase DocumentosServicioPresentadorDetalle.</summary>
    public class DocumentosServicioPresentadorDetalle : PresentadorDetalleBase<DocumentosServicio>
    {
        ///<summary>Variable vista de la interfaz IDocumentosServicioDetalle.</summary>
        IDocumentosServicioDetalle vista;
        ///<summary>Variable de la entidad DocumentosServicio.</summary>
        DocumentosServicio _DocumentosServicio;
        UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public DocumentosServicioPresentadorDetalle(IDocumentosServicioDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(AccionDetalle accion)
        {
            try
            {
                DocumentosServicioRepositorio repositorio = new DocumentosServicioRepositorio(udt);
                _DocumentosServicio = repositorio.ObtenerPorId(vista.Id);
                PresentadorAVista();
                if (accion == AccionDetalle.Modificar && _DocumentosServicio.FlujosServicio.IndVigente == true)
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
                udt.IniciarTransaccion();
                DocumentosServicioRepositorio repositorio = new DocumentosServicioRepositorio(udt);
                if (accion == AccionDetalle.Agregar)
                {
                    _DocumentosServicio = new DocumentosServicio();
                    VistaAPresentador(accion);
                    udt.MarcarNuevo(_DocumentosServicio);
                }
                else
                {
                    _DocumentosServicio = repositorio.ObtenerPorId(vista.Id);
                    VistaAPresentador(accion);
                    udt.MarcarModificado(_DocumentosServicio);
                }
                udt.Commit();
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
                vista.IdFlujoServicio = string.Format("{0:N0}", _DocumentosServicio.IdFlujoServicio);
                vista.IdDocumento = string.Format("{0:N0}", _DocumentosServicio.IdDocumento);
                vista.IndDocObligatorio = _DocumentosServicio.IndDocObligatorio.ToString();
                vista.IndVisibilidad = _DocumentosServicio.IndVisibilidad.ToString();
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
                if (accion == AccionDetalle.Agregar)
                    _DocumentosServicio.IdFlujoServicio = int.Parse(vista.IdFlujoServicio);
                _DocumentosServicio.IdDocumento = int.Parse(vista.IdDocumento);
                _DocumentosServicio.IndDocObligatorio = bool.Parse(vista.IndDocObligatorio);
                _DocumentosServicio.IndVisibilidad = bool.Parse(vista.IndVisibilidad);
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
		
        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarCombos()
        {
            try
            {
                DocumentoRepositorio repositorioDocumento = new DocumentoRepositorio(udt);
                vista.ComboIdDocumento = repositorioDocumento.ObtenerDTO();
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

        public void LlenarTextBox(AccionDetalle accion)
        {
            try
            {
                FlujosServicioRepositorio repositorioFlujoServicio = new FlujosServicioRepositorio(unidadDeTrabajo);
                FlujosServicioDTO _FlujoServicio = new FlujosServicioDTO();
                if (accion == AccionDetalle.Agregar)
                    _FlujoServicio = repositorioFlujoServicio.ObtenerDTOServicioFlujoServicio(int.Parse(vista.IdFlujoServicio));
                else
                    _FlujoServicio = repositorioFlujoServicio.ObtenerDTOServicioFlujoServicio(_DocumentosServicio.IdFlujoServicio);
                ServicioParametrizador(_FlujoServicio.IdServicioSuscriptor);
                vista.Version = _FlujoServicio.Version;
                if (_FlujoServicio.IndVigente == true)
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

        public void ServicioParametrizador(int idServicioSuscriptor)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerServicioSuscriptorPorId(idServicioSuscriptor);
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

        ///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
        private void CargarPublicacion()
        {
            try
            {
                vista.IndVigente = _DocumentosServicio.IndVigente.ToString();
                vista.FechaValidez = _DocumentosServicio.FechaValidez.ToString();
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
                vista.CreadoPor = this.ObtenerNombreUsuario(_DocumentosServicio.CreadoPor);
                ;
                vista.FechaCreacion = _DocumentosServicio.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_DocumentosServicio.ModificadoPor);
                ;
                vista.FechaModificacion = _DocumentosServicio.FechaModificacion.ToString();
                vista.IndEliminado = _DocumentosServicio.IndEliminado.ToString();
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
                    _DocumentosServicio.FechaValidez = null;
                else
                    _DocumentosServicio.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _DocumentosServicio.IndVigente = bool.Parse(vista.IndVigente);
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
                    _DocumentosServicio.IndEliminado = false;
                    _DocumentosServicio.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _DocumentosServicio.FechaCreacion = DateTime.Now;
                    _DocumentosServicio.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _DocumentosServicio.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _DocumentosServicio.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _DocumentosServicio.FechaModificacion = DateTime.Now;
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