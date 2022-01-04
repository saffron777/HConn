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
	///<summary>Clase SolicitudBloquePresentadorDetalle.</summary>
	public class SolicitudBloquePresentadorDetalle : PresentadorDetalleBase<SolicitudBloque>
	{
		///<summary>Variable vista de la interfaz ISolicitudBloqueDetalle.</summary>
		readonly ISolicitudBloqueDetalle vista;

		///<summary>Variable de la entidad SolicitudBloque.</summary>
		SolicitudBloque solicitudBloque;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public SolicitudBloquePresentadorDetalle(ISolicitudBloqueDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				SolicitudBloqueRepositorio repositorio = new SolicitudBloqueRepositorio(udt);
				this.solicitudBloque = repositorio.ObtenerPorId(this.vista.Id);
				this.PresentadorAVista();
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
				string errores = this.ValidarDatos();
				if(errores.Length == 0)
				{
					this.udt.IniciarTransaccion();
					SolicitudBloqueRepositorio repositorio = new SolicitudBloqueRepositorio(udt);
					if(accion == AccionDetalle.Agregar)
					{
						this.solicitudBloque = new SolicitudBloque();
						this.VistaAPresentador(accion);
						this.udt.MarcarNuevo(solicitudBloque);
					}
					else
					{
						this.solicitudBloque = repositorio.ObtenerPorId(vista.Id);
						this.VistaAPresentador(accion);
						this.udt.MarcarModificado(solicitudBloque);
					}
					this.udt.Commit();
				}
				else
					this.vista.Errores = errores;
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
				this.vista.IdBloque = string.Format("{0:N0}", this.solicitudBloque.IdBloque);
				this.vista.Orden = string.Format("{0:N0}", this.solicitudBloque.Orden);
				this.vista.IndCierre = this.solicitudBloque.IndCierre.ToString();
				this.vista.IdTipoControl = string.Format("{0:N0}", this.solicitudBloque.IdTipoControl);
				this.vista.TituloBloque = this.solicitudBloque.TituloBloque;
				this.vista.IndActualizable = this.solicitudBloque.IndActualizable.ToString();
				this.vista.KeyCampoXML = this.solicitudBloque.KeyCampoXML;
				this.CargarPublicacion();
				this.CargarAuditoria();
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
				{
					this.solicitudBloque.Id = 0;
					this.solicitudBloque.IdFlujoServicio = this.vista.Id;
				}
				else if(accion == AccionDetalle.Modificar)
					this.solicitudBloque.Id = this.vista.Id;
				this.solicitudBloque.IdBloque = int.Parse(this.vista.IdBloque);
				this.solicitudBloque.Orden = int.Parse(this.vista.Orden);
				this.solicitudBloque.IndCierre = bool.Parse(this.vista.IndCierre);
				this.solicitudBloque.IdTipoControl = int.Parse(this.vista.IdTipoControl);
				this.solicitudBloque.TituloBloque = this.vista.TituloBloque;
				this.solicitudBloque.IndActualizable = bool.Parse(this.vista.IndActualizable);
				this.solicitudBloque.KeyCampoXML = this.vista.KeyCampoXML;
				this.AsignarAuditoria(accion);
				this.AsignarPublicacion();
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
				Metadata<SolicitudBloque> metadata = new Metadata<SolicitudBloque>();
				errores.AppendWithBreak(metadata.ValidarPropiedad("Id", this.vista.Id));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IdBloque", this.vista.IdBloque));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Orden", this.vista.Orden));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IndCierre", this.vista.IndCierre));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IdTipoControl", this.vista.IdTipoControl));
				errores.AppendWithBreak(metadata.ValidarPropiedad("TituloBloque", this.vista.TituloBloque));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IndActualizable", this.vista.IndActualizable));
				errores.AppendWithBreak(metadata.ValidarPropiedad("KeyCampoXML", this.vista.KeyCampoXML));
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

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombos()
		{
			try
			{
				BloqueRepositorio repositorioBloque = new BloqueRepositorio(this.udt);
				this.vista.ComboIdBloque = repositorioBloque.ObtenerDTO();
				ListasValorRepositorio repositorioListasValor = new ListasValorRepositorio(this.udt);
				this.vista.ComboIdTipoControl = repositorioListasValor.ObtenerDTOByNombreLista(WebConfigurationManager.AppSettings[@"ListaTipoSeleccionBloque"]);
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

		///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
		private void CargarPublicacion()
		{
			try
			{
				this.vista.IndVigente = this.solicitudBloque.IndVigente.ToString();
				this.vista.FechaValidez = this.solicitudBloque.FechaValidez.ToString();

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
				this.vista.CreadoPor = this.ObtenerNombreUsuario(this.solicitudBloque.CreadoPor);
				this.vista.FechaCreacion = this.solicitudBloque.FechaCreacion.ToString();
				this.vista.ModificadoPor = this.ObtenerNombreUsuario(this.solicitudBloque.ModificadoPor);
				this.vista.FechaModificacion = this.solicitudBloque.FechaModificacion.ToString();
				this.vista.IndEliminado = this.solicitudBloque.IndEliminado.ToString();
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
				if(string.IsNullOrEmpty(this.vista.FechaValidez))
					this.solicitudBloque.FechaValidez = null;
				else
					this.solicitudBloque.FechaValidez = DateTime.Parse(vista.FechaValidez);
				this.solicitudBloque.IndVigente = bool.Parse(vista.IndVigente);
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
					this.solicitudBloque.IndEliminado = false;
					this.solicitudBloque.CreadoPor = this.vista.UsuarioActual.Id;
					this.solicitudBloque.FechaCreacion = DateTime.Now;
					this.solicitudBloque.ModificadoPor = this.vista.UsuarioActual.Id;
					this.solicitudBloque.FechaModificacion = DateTime.Now;
				}
				else if(accion == AccionDetalle.Modificar)
				{
					this.solicitudBloque.ModificadoPor = this.vista.UsuarioActual.Id;
					this.solicitudBloque.FechaModificacion = DateTime.Now;
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