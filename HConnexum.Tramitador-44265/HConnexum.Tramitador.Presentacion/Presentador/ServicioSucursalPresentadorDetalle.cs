using System;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Data;
using System.Configuration;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase ServicioSucursalPresentadorDetalle.</summary>
    public class ServicioSucursalPresentadorDetalle : PresentadorDetalleBase<ServicioSucursal>
    {
        ///<summary>Variable vista de la interfaz IServicioSucursalDetalle.</summary>
        IServicioSucursalDetalle vista;
		
        ///<summary>Variable de la entidad ServicioSucursal.</summary>
        ServicioSucursal _ServicioSucursal;
        
        UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ServicioSucursalPresentadorDetalle(IServicioSucursalDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(AccionDetalle accion)
        {
            try
            {
                ServicioSucursalRepositorio repositorio = new ServicioSucursalRepositorio(udt);
                _ServicioSucursal = repositorio.ObtenerPorId(vista.Id);
                PresentadorAVista();
                if (accion == AccionDetalle.Modificar && _ServicioSucursal.FlujosServicio.IndVigente == true)
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
                ServicioSucursalRepositorio repositorio = new ServicioSucursalRepositorio(udt);
                if (accion == AccionDetalle.Agregar)
                {
                    _ServicioSucursal = new ServicioSucursal();
                    VistaAPresentador(accion);
                    udt.MarcarNuevo(_ServicioSucursal);
                }
                else
                {
                    _ServicioSucursal = repositorio.ObtenerPorId(vista.Id);
                    VistaAPresentador(accion);
                    udt.MarcarModificado(_ServicioSucursal);
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
                string servicio = Buscoservicio();
                string suscriptor = Buscosuscriptor();
                Servicio_DatosparaEditar(int.Parse(suscriptor), int.Parse(servicio));
                vista.IdSucursal = string.Format("{0:N0}", _ServicioSucursal.IdSucursal);
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

        public void Servicio_DatosparaEditar(int suscriptor, int servicio)
        {
            ServicioParametrizadorClient service = new ServicioParametrizadorClient();
            try
            {
                DataSet ds1 = service.ObtenerSucursal(suscriptor);
                if (ds1.Tables[@"Error"] != null)
                    throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds1.Tables[0].Rows.Count > 0)
                    vista.ComboIdSucursal = ds1.Tables[0];
                DataSet ds2 = service.ObtenerServicioSuscriptorPorId(servicio);
                if (ds2.Tables[@"Error"] != null)
                    throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds2.Tables[0].Rows.Count > 0)
                    vista.Servicio = ds2.Tables[0].Rows[0]["Nombre"].ToString();
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
                if (service.State != CommunicationState.Closed)
                    service.Close();
            }
        }

        public string Buscoservicio() 
        {
            HConnexum.Tramitador.Datos.ServicioSucursalRepositorio SS = new HConnexum.Tramitador.Datos.ServicioSucursalRepositorio(udt);
            string servico = SS.ObtenerServicioPorIdservicioSucursal(this.vista.Id);
            return servico;
        }

        public string Buscosuscriptor()
        {
            HConnexum.Tramitador.Datos.ServicioSucursalRepositorio SS = new HConnexum.Tramitador.Datos.ServicioSucursalRepositorio(udt);
            string suscriptor = SS.ObtenerSuscriptorIdservicioSucursal(this.vista.Id);
            return suscriptor;
        }

        ///<summary>Método encargado de asignar valores a propiedades de la entidad desde la vista.</summary>	
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        private void VistaAPresentador(AccionDetalle accion)
        {
            try
            {
                if (accion == AccionDetalle.Agregar) 
                    _ServicioSucursal.IdFlujoServicio = vista.Id;
                _ServicioSucursal.IdSucursal = int.Parse(vista.IdSucursal);
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
                HConnexum.Tramitador.Datos.FlujosServicioRepositorio FS = new FlujosServicioRepositorio(udt);
                FlujosServicioDTO fsdto = FS.ObtenerServicioySuscriptorPorFlujosservicio(vista.Id);
                Servicio_DatosparaEditar(fsdto.IdSuscriptor, fsdto.IdServicioSuscriptor);
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
                vista.IndVigente = _ServicioSucursal.IndVigente.ToString();
                vista.FechaValidez = _ServicioSucursal.FechaValidez.ToString();
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
                vista.CreadoPor = this.ObtenerNombreUsuario(_ServicioSucursal.CreadoPor);
                ;
                vista.FechaCreacion = _ServicioSucursal.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_ServicioSucursal.ModificadoPor);
                ;
                vista.FechaModificacion = _ServicioSucursal.FechaModificacion.ToString();
                vista.IndEliminado = _ServicioSucursal.IndEliminado.ToString();
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
                    _ServicioSucursal.FechaValidez = null;
                else
                    _ServicioSucursal.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _ServicioSucursal.IndVigente = bool.Parse(vista.IndVigente);
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
                    _ServicioSucursal.IndEliminado = false;
                    _ServicioSucursal.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _ServicioSucursal.FechaCreacion = DateTime.Now;
                    _ServicioSucursal.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _ServicioSucursal.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _ServicioSucursal.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _ServicioSucursal.FechaModificacion = DateTime.Now;
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