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
	///<summary>Clase ParametrosAgendaPresentadorDetalle.</summary>
	public class ParametrosAgendaPresentadorDetalle : PresentadorDetalleBase<ParametrosAgenda>
	{
		///<summary>Variable vista de la interfaz IParametrosAgendaDetalle.</summary>
		readonly IParametrosAgendaDetalle vista;
		///<summary>Variable de la entidad ParametrosAgenda.</summary>
		ParametrosAgenda parametrosAgenda;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public ParametrosAgendaPresentadorDetalle(IParametrosAgendaDetalle vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(AccionDetalle accion)
		{
			try
			{
				ParametrosAgendaRepositorio repositorio = new ParametrosAgendaRepositorio(udt);
				parametrosAgenda = repositorio.ObtenerPorId(vista.Id);
				PresentadorAVista();
				if(accion == AccionDetalle.Modificar && parametrosAgenda.Paso.Etapa.FlujosServicio.IndVigente == true)
					vista.ErroresCustomEditar = "El registro seleccionado no puede ser Editado debido a que el Flujo asociado está actualmente Activo";
			}
			catch(Exception ex)
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
					ParametrosAgendaRepositorio repositorio = new ParametrosAgendaRepositorio(udt);
					if(accion == AccionDetalle.Agregar)
					{
						parametrosAgenda = new ParametrosAgenda();
						VistaAPresentador(accion);
						udt.MarcarNuevo(parametrosAgenda);
					}
					else
					{
						parametrosAgenda = repositorio.ObtenerPorId(vista.Id);
						VistaAPresentador(accion);
						udt.MarcarModificado(parametrosAgenda);
					}
					udt.Commit();
				}
				else
					vista.Errores = errores;
			}
			catch(Exception ex)
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
				vista.KeyFechaEjec = parametrosAgenda.KeyFechaEjec;
				vista.KeyHoraEjec = parametrosAgenda.KeyHoraEjec;
				vista.IndInmediato = parametrosAgenda.IndInmediato.ToString();
				vista.Cantidad = parametrosAgenda.Cantidad.ToString();
				CargarPublicacion();
				CargarAuditoria();
			}
			catch(Exception ex)
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
				if(accion == AccionDetalle.Agregar)
					parametrosAgenda.IdPaso = vista.Id;
				parametrosAgenda.KeyFechaEjec = vista.KeyFechaEjec;
				parametrosAgenda.KeyHoraEjec = vista.KeyHoraEjec;
				parametrosAgenda.IndInmediato = bool.Parse(vista.IndInmediato);
				parametrosAgenda.Cantidad = int.Parse(vista.Cantidad.ToString());
				AsignarAuditoria(accion);
				AsignarPublicacion();
			}
			catch(Exception ex)
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
				Metadata<ParametrosAgenda> metadata = new Metadata<ParametrosAgenda>();
				errores.AppendWithBreak(metadata.ValidarPropiedad("KeyFechaEjec", vista.KeyFechaEjec));
				errores.AppendWithBreak(metadata.ValidarPropiedad("KeyHoraEjec", vista.KeyHoraEjec));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Cantidad", vista.Cantidad));
			}
			catch(Exception ex)
			{
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return errores.ToString();
		}

		public void LlenarTextBox(AccionDetalle accion)
		{
			try
			{
				PasoRepositorio repositorioPaso = new PasoRepositorio(unidadDeTrabajo);
				PasoDTO paso = new PasoDTO();
				if(accion == AccionDetalle.Agregar)
					paso = repositorioPaso.ObtenerPorIdPaso(vista.Id);
				else
					paso = repositorioPaso.ObtenerPorIdPaso(parametrosAgenda.IdPaso);
				vista.Paso = paso.Nombre;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
		private void CargarPublicacion()
		{
			try
			{
				vista.FechaValidez = parametrosAgenda.FechaValidez.ToString();
				vista.IndVigente = parametrosAgenda.IndVigente.ToString();
			}
			catch(Exception ex)
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
				vista.CreadoPor = this.ObtenerNombreUsuario(parametrosAgenda.CreadoPor);
				vista.FechaCreacion = parametrosAgenda.FechaCreacion.ToString();
				vista.ModificadoPor = this.ObtenerNombreUsuario(parametrosAgenda.ModificadoPor);
				vista.FechaModificacion = parametrosAgenda.FechaModificacion.ToString();
				vista.IndEliminado = parametrosAgenda.IndEliminado.ToString();
			}
			catch(Exception ex)
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
					parametrosAgenda.FechaValidez = null;
				else
					parametrosAgenda.FechaValidez = DateTime.Parse(vista.FechaValidez);
				parametrosAgenda.IndVigente = bool.Parse(vista.IndVigente);
			}
			catch(Exception ex)
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
					parametrosAgenda.IndEliminado = false;
					parametrosAgenda.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					parametrosAgenda.FechaCreacion = DateTime.Now;
					parametrosAgenda.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					parametrosAgenda.FechaModificacion = DateTime.Now;
				}
				else if(accion == AccionDetalle.Modificar)
				{
					parametrosAgenda.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					parametrosAgenda.FechaModificacion = DateTime.Now;
				}
			}
			catch(Exception ex)
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