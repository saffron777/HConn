using System;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Configuration;
using System.Data;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase ChadePasoPresentadorDetalle.</summary>
    public class ChadePasoPresentadorDetalle : PresentadorDetalleBase<ChadePaso>
    {
        ///<summary>Variable vista de la interfaz IChadePasoDetalle.</summary>
        IChadePasoDetalle vista;
        ///<summary>Variable de la entidad ChadePaso.</summary>
        ChadePaso _ChadePaso;
        UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ChadePasoPresentadorDetalle(IChadePasoDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(AccionDetalle accion)
        {
            try
            {
                ChadePasoRepositorio repositorio = new ChadePasoRepositorio(udt);
                _ChadePaso = repositorio.ObtenerPorId(vista.Id);
                PresentadorAVista();
                if (accion == AccionDetalle.Modificar && _ChadePaso.Paso.Etapa.FlujosServicio.IndVigente == true)
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
                ChadePasoRepositorio repositorio = new ChadePasoRepositorio(udt);
                if (accion == AccionDetalle.Agregar)
                {
                    _ChadePaso = new ChadePaso();
                    VistaAPresentador(accion);
                    udt.MarcarNuevo(_ChadePaso);
                }
                else
                {
                    _ChadePaso = repositorio.ObtenerPorId(vista.Id);
                    VistaAPresentador(accion);
                    udt.MarcarModificado(_ChadePaso);
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
                vista.IdCargosuscriptor = _ChadePaso.IdCargosuscriptor.ToString();
                vista.IdHabilidadSuscriptor = string.Format("{0:N0}", _ChadePaso.IdHabilidadSuscriptor);
                vista.IdAutonomiaSuscriptor = string.Format("{0:N0}", _ChadePaso.IdAutonomiaSuscriptor);
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
                _ChadePaso.IdCargosuscriptor = int.Parse(vista.IdCargosuscriptor);
                if (vista.IdHabilidadSuscriptor != "")
                    _ChadePaso.IdHabilidadSuscriptor = int.Parse(vista.IdHabilidadSuscriptor);
                if (vista.IdAutonomiaSuscriptor != "")
                    _ChadePaso.IdAutonomiaSuscriptor = int.Parse(vista.IdAutonomiaSuscriptor);
                if (accion == AccionDetalle.Agregar)                
                    _ChadePaso.IdPasos = vista.Id;
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
        public void ServicioParametrizador(AccionDetalle accion)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                int idSuscriptor;
                PasoRepositorio repositorioIdSuscriptor = new PasoRepositorio(udt);
                ChadePasoRepositorio repositorio = new ChadePasoRepositorio(udt);
                if (accion == AccionDetalle.Agregar)
                    idSuscriptor = repositorioIdSuscriptor.ObtenerDTOIdSuscriptor(vista.Id).IdSuscriptor;
                else
                {
                    _ChadePaso = repositorio.ObtenerPorId(vista.Id);
                    idSuscriptor = repositorioIdSuscriptor.ObtenerDTOIdSuscriptor(_ChadePaso.IdPasos).IdSuscriptor;
                }
                DataSet ds = servicio.ObtenerCargosSuscriptorporId(idSuscriptor);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    vista.ComboIdCargosuscriptor = ds.Tables[0];

                DataSet ds1 = servicio.ObtenerHabilidadesPorId(idSuscriptor);
                if (ds1.Tables[@"Error"] != null)
                    throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds1.Tables[0].Rows.Count > 0)
                    vista.ComboIdHabilidadSuscriptor = ds1.Tables[0];

                DataSet ds2 = servicio.ObtenerAutonomiasSuscriptorPorId(idSuscriptor);
                if (ds2.Tables[@"Error"] != null)
                    throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds2.Tables[0].Rows.Count > 0)
                    vista.ComboIdAutonomiaSuscriptor = ds2.Tables[0];
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
                vista.IndVigente = _ChadePaso.IndVigente.ToString();
                vista.FechaValidez = _ChadePaso.FechaValidez.ToString();
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
                vista.CreadoPor = this.ObtenerNombreUsuario(_ChadePaso.CreadoPor);
                ;
                vista.FechaCreacion = _ChadePaso.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_ChadePaso.ModificadoPor);
                ;
                vista.FechaModificacion = _ChadePaso.FechaModificacion.ToString();
                vista.IndEliminado = _ChadePaso.IndEliminado.ToString();
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
                    _ChadePaso.FechaValidez = null;
                else
                    _ChadePaso.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _ChadePaso.IndVigente = bool.Parse(vista.IndVigente);
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
                    _ChadePaso.IndEliminado = false;
                    _ChadePaso.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _ChadePaso.FechaCreacion = DateTime.Now;
                    _ChadePaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _ChadePaso.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _ChadePaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _ChadePaso.FechaModificacion = DateTime.Now;
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