using System;
using System.Configuration;
using System.Data;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase DocumentosPasoPresentadorDetalle.</summary>
    public class DocumentosPasoPresentadorDetalle : PresentadorDetalleBase<DocumentosPaso>
    {
        ///<summary>Variable vista de la interfaz IDocumentosPasoDetalle.</summary>
        readonly IDocumentosPasoDetalle vista;
		
        ///<summary>Variable de la entidad DocumentosPaso.</summary>
        DocumentosPaso _DocumentosPaso;
        
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public DocumentosPasoPresentadorDetalle(IDocumentosPasoDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(AccionDetalle accion)
        {
            try
            {
                DocumentosPasoRepositorio repositorio = new DocumentosPasoRepositorio(udt);
                _DocumentosPaso = repositorio.ObtenerPorId(vista.Id);
                PresentadorAVista();
                if (accion == AccionDetalle.Modificar && _DocumentosPaso.Paso.Etapa.FlujosServicio.IndVigente == true)
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
                DocumentosPasoRepositorio repositorio = new DocumentosPasoRepositorio(udt);
                if (accion == AccionDetalle.Agregar)
                {
                    _DocumentosPaso = new DocumentosPaso();
                    VistaAPresentador(accion);
                    udt.MarcarNuevo(_DocumentosPaso);
                }
                else
                {
                    _DocumentosPaso = repositorio.ObtenerPorId(vista.Id);
                    VistaAPresentador(accion);
                    udt.MarcarModificado(_DocumentosPaso);
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
                vista.IdDocumentoServicio = string.Format("{0:N0}", _DocumentosPaso.IdDocumentoServicio);
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
                _DocumentosPaso.IdDocumentoServicio = int.Parse(vista.IdDocumentoServicio);
                if (accion == AccionDetalle.Agregar)
                    _DocumentosPaso.IdPaso = vista.Id;
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
                DocumentosServicioRepositorio repositorioDocumentosServicio = new DocumentosServicioRepositorio(udt);
                vista.ComboIdDocumentoServicio = repositorioDocumentosServicio.ObtenerDTODocumentosServicios(vista.IdFlujoServicio); // el idFlujoServicio debe ser guardado aca
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
                PasoRepositorio repositorioPaso = new PasoRepositorio(unidadDeTrabajo);
                PasoDTO _Paso = new PasoDTO();
                if (accion == AccionDetalle.Agregar)
                    _Paso = repositorioPaso.ObtenerPorIdPaso(vista.Id);
                else
                    _Paso = repositorioPaso.ObtenerPorIdPaso(_DocumentosPaso.IdPaso);
                ServicioParametrizador(_Paso.IdServicioSuscriptor);
                vista.Version = _Paso.VersionFlujoServicio;
                if (_Paso.IndVigenteFlujoServicio == true)
                    vista.Estatus = "Vigente";
                else
                    vista.Estatus = "Inactivo";
                vista.Etapa = _Paso.NombreEtapa;
                vista.Paso = _Paso.Nombre;
                vista.IdFlujoServicio = _Paso.IdFlujoServicio;
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
                vista.IndVigente = _DocumentosPaso.IndVigente.ToString();
                vista.FechaValidez = _DocumentosPaso.FechaValidez.ToString();
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
                vista.CreadoPor = this.ObtenerNombreUsuario(_DocumentosPaso.CreadoPor);
                vista.FechaCreacion = _DocumentosPaso.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_DocumentosPaso.ModificadoPor);
                vista.FechaModificacion = _DocumentosPaso.FechaModificacion.ToString();
                vista.IndEliminado = _DocumentosPaso.IndEliminado.ToString();
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
                    _DocumentosPaso.FechaValidez = null;
                else
                    _DocumentosPaso.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _DocumentosPaso.IndVigente = bool.Parse(vista.IndVigente);
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
                    _DocumentosPaso.IndEliminado = false;
                    _DocumentosPaso.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _DocumentosPaso.FechaCreacion = DateTime.Now;
                    _DocumentosPaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _DocumentosPaso.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _DocumentosPaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _DocumentosPaso.FechaModificacion = DateTime.Now;
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