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
    ///<summary>Clase BloquePresentadorDetalle.</summary>
    public class BloquePresentadorDetalle : PresentadorDetalleBase<Bloque>
    {
        ///<summary>Variable vista de la interfaz IBloqueDetalle.</summary>
        readonly IBloqueDetalle vista;
		
        ///<summary>Variable de la entidad Bloque.</summary>
        Bloque _Bloque;
        
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public BloquePresentadorDetalle(IBloqueDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista()
        {
            try
            {
                BloqueRepositorio repositorio = new BloqueRepositorio(udt);
                _Bloque = repositorio.ObtenerPorId(vista.Id);
                PresentadorAVista();
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
                    BloqueRepositorio repositorio = new BloqueRepositorio(udt);
					
                    if (accion == AccionDetalle.Agregar)
                    {
                        _Bloque = new Bloque();
                        VistaAPresentador(accion);
                        udt.MarcarNuevo(_Bloque);
                    }
                    else
                    {
                        _Bloque = repositorio.ObtenerPorId(vista.Id);
                        VistaAPresentador(accion);
                        udt.MarcarModificado(_Bloque);
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
                vista.Nombre = _Bloque.Nombre;
                vista.NombrePrograma = _Bloque.NombrePrograma;
                vista.IdListaValor = _Bloque.IdListaValor.ToString();
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
                _Bloque.Nombre = vista.Nombre;
                _Bloque.NombrePrograma = vista.NombrePrograma;
                if (vista.IdListaValor == "")
                    _Bloque.IdListaValor = null;
                else
                    _Bloque.IdListaValor = int.Parse(vista.IdListaValor);
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
                Metadata<Bloque> metadata = new Metadata<Bloque>();
                errores.AppendWithBreak(metadata.ValidarPropiedad("Id", vista.Id));
                errores.AppendWithBreak(metadata.ValidarPropiedad("Nombre", vista.Nombre));
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
        public void LlenarCombos()
        {
            try
            {
                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                try
                {
                    DataSet ds1 = servicio.ObtenerListas();
                    if (ds1.Tables[@"Error"] != null)
                        throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds1.Tables[0].Rows.Count > 0)
                        vista.ComboIdListaValor = ds1.Tables[0];
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
                vista.IndVigente = _Bloque.IndVigente.ToString();
                vista.FechaValidez = _Bloque.FechaValidez.ToString();
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
                vista.CreadoPor = this.ObtenerNombreUsuario(_Bloque.CreadoPor);
                vista.FechaCreacion = _Bloque.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_Bloque.ModificadoPor);
                vista.FechaModificacion = _Bloque.FechaModificacion.ToString();
                vista.IndEliminado = _Bloque.IndEliminado.ToString();
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
                    _Bloque.FechaValidez = null;
                else
                    _Bloque.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _Bloque.IndVigente = bool.Parse(vista.IndVigente);
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
                    _Bloque.IndEliminado = false;
                    _Bloque.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _Bloque.FechaCreacion = DateTime.Now;
                    _Bloque.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _Bloque.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _Bloque.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _Bloque.FechaModificacion = DateTime.Now;
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