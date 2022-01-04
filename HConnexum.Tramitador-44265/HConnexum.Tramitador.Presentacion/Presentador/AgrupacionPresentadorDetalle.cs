using System;
using System.Text;
using HConnexum.Configurador.Datos;
using HConnexum.Infraestructura;
using HConnexum.Configurador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase AgrupacionPresentadorDetalle.</summary>
    public class AgrupacionPresentadorDetalle : PresentadorDetalleBase<HConnexum.Tramitador.Negocio.Agrupacion>
    {
		///<summary>Variable vista de la interfaz IAgrupacionDetalle.</summary>
		readonly IAgrupacionDetalle vista;

		///<summary>Variable de la entidad Agrupacion.</summary>
        Agrupacion _Agrupacion;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public AgrupacionPresentadorDetalle(IAgrupacionDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				AgrupacionRepositorio repositorio = new AgrupacionRepositorio(udt);
				this._Agrupacion = repositorio.ObtenerPorId(this.vista.Id);
				this.PresentadorAVista();
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
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
				string errores = this.ValidarDatos();
				if(errores.Length == 0)
				{
					this.udt.IniciarTransaccion();
					AgrupacionRepositorio repositorio = new AgrupacionRepositorio(udt);
					if(accion == AccionDetalle.Agregar)
					{
                        this._Agrupacion = new HConnexum.Tramitador.Negocio.Agrupacion();
                        this.VistaAPresentador(accion);
						this.udt.MarcarNuevo(_Agrupacion);
					}
					else
					{
						this._Agrupacion = repositorio.ObtenerPorId(vista.Id);
						this.VistaAPresentador(accion);
						this.udt.MarcarModificado(_Agrupacion);
					}
					this.udt.Commit();
				}
				else
					this.vista.Errores = errores;
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{
				this.vista.Nombre= this._Agrupacion.Nombre;

				this.CargarPublicacion();
				this.CargarAuditoria();
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
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
				this._Agrupacion.Nombre = this.vista.Nombre;

				this.AsignarAuditoria(accion);
				this.AsignarPublicacion();
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
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
                Metadata<Agrupacion> metadata = new Metadata<Agrupacion>();
                errores.AppendWithBreak(metadata.ValidarPropiedad("Id", this.vista.Id));
			errores.AppendWithBreak(metadata.ValidarPropiedad("Nombre", this.vista.Nombre));
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
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
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
		private void CargarPublicacion()
		{
			try
			{
			this.vista.FechaValidez= this._Agrupacion.FechaValidez.ToString();
			this.vista.IndVigente= this._Agrupacion.IndVigente.ToString();

			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
		private void CargarAuditoria()
		{
			try
			{
			this.vista.CreadoPor= this.ObtenerNombreUsuario(this._Agrupacion.CreadoPor);;
			this.vista.FechaCreacion= this._Agrupacion.FechaCreacion.ToString();
			this.vista.ModificadoPor= this.ObtenerNombreUsuario(this._Agrupacion.ModificadoPor);;
			this.vista.FechaModificacion= this._Agrupacion.FechaModificacion.ToString();
			this.vista.IndEliminado= this._Agrupacion.IndEliminado.ToString();

			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores de publicación a la entidad.</summary>
		public void AsignarPublicacion()
		{
			try
			{
				if(string.IsNullOrEmpty(this.vista.FechaValidez))
					this._Agrupacion.FechaValidez = null;
				else
					this._Agrupacion.FechaValidez = DateTime.Parse(vista.FechaValidez);
				this._Agrupacion.IndVigente = bool.Parse(vista.IndVigente);
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
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
					this._Agrupacion.IndEliminado = false;
					this._Agrupacion.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					this._Agrupacion.FechaCreacion = DateTime.Now;
					this._Agrupacion.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					this._Agrupacion.FechaModificacion = DateTime.Now;
				}
				else if(accion == AccionDetalle.Modificar)
				{
					this._Agrupacion.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					this._Agrupacion.FechaModificacion = DateTime.Now;
				}
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}