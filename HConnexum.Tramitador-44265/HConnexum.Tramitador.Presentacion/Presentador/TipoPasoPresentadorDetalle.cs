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
	///<summary>Clase TipoPasoPresentadorDetalle.</summary>
    public class TipoPasoPresentadorDetalle : PresentadorDetalleBase<TipoPaso>
    {
        ///<summary>Variable vista de la interfaz ITipoPasoDetalle.</summary>
        readonly ITipoPasoDetalle vista;
		
		///<summary>Variable de la entidad TipoPaso.</summary>
		TipoPaso _TipoPaso;
        
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
		///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public TipoPasoPresentadorDetalle(ITipoPasoDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista()
        {
		 	try
            {
	            TipoPasoRepositorio repositorio = new TipoPasoRepositorio(udt);
	            _TipoPaso = repositorio.ObtenerPorId(vista.Id);
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
	            if(errores.Length == 0)
	            {
	                udt.IniciarTransaccion();
	                TipoPasoRepositorio repositorio = new TipoPasoRepositorio(udt);
					
					if(accion == AccionDetalle.Agregar)
	                {
						_TipoPaso = new TipoPaso();
						VistaAPresentador(accion);
	                    udt.MarcarNuevo(_TipoPaso);
	                }
	                else
	                {
						_TipoPaso = repositorio.ObtenerPorId(vista.Id);
						VistaAPresentador(accion);
	                    udt.MarcarModificado(_TipoPaso);
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
               
                vista.Descripcion= _TipoPaso.Descripcion;
                
                if (!string.IsNullOrEmpty(_TipoPaso.Programa)) 
                  vista.Programa= _TipoPaso.Programa;
                	
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
                _TipoPaso.Descripcion = vista.Descripcion;
                
                if (!string.IsNullOrEmpty(vista.Programa))
                    _TipoPaso.Programa = vista.Programa;
                else _TipoPaso.Programa = null;

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
			Metadata<TipoPaso> metadata = new Metadata<TipoPaso>();
			errores.AppendWithBreak(metadata.ValidarPropiedad("Id", vista.Id));
            errores.AppendWithBreak(metadata.ValidarPropiedad("Descripcion", vista.Descripcion));
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
		public void  LlenarCombos()
		{
			try
            {
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
			vista.IndVigente= _TipoPaso.IndVigente.ToString();
			vista.FechaValidez= _TipoPaso.FechaValidez.ToString();

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
			vista.CreadoPor= this.ObtenerNombreUsuario(_TipoPaso.CreadoPor);
			vista.FechaCreacion= _TipoPaso.FechaCreacion.ToString();
			vista.ModificadoPor= this.ObtenerNombreUsuario(_TipoPaso.ModificadoPor);
			vista.FechaModificacion= _TipoPaso.FechaModificacion.ToString();
			vista.IndEliminado= _TipoPaso.IndEliminado.ToString();

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
				if(string.IsNullOrEmpty(vista.FechaValidez))
					_TipoPaso.FechaValidez = null;
				else
					_TipoPaso.FechaValidez = DateTime.Parse(vista.FechaValidez);
				_TipoPaso.IndVigente = bool.Parse(vista.IndVigente);
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
				if(accion == AccionDetalle.Agregar)
				{
					_TipoPaso.IndEliminado = false;
					_TipoPaso.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_TipoPaso.FechaCreacion = DateTime.Now;
					_TipoPaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_TipoPaso.FechaModificacion = DateTime.Now;
				}
				else if(accion == AccionDetalle.Modificar)
				{
					_TipoPaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_TipoPaso.FechaModificacion = DateTime.Now;
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