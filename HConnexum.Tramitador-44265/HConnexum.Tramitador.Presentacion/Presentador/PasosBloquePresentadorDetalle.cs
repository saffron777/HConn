using System;
using System.Data;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase PasosBloquePresentadorDetalle.</summary>
	public class PasosBloquePresentadorDetalle : PresentadorDetalleBase<PasosBloque>
	{
		///<summary>Variable vista de la interfaz IPasosBloqueDetalle.</summary>
		readonly IPasosBloqueDetalle vista;
		///<summary>Variable de la entidad PasosBloque.</summary>
		PasosBloque pasosBloque;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PasosBloquePresentadorDetalle(IPasosBloqueDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(AccionDetalle accion)
		{
			try
			{
				PasosBloqueRepositorio repositorio = new PasosBloqueRepositorio(udt);
				pasosBloque = repositorio.ObtenerPorId(vista.Id);
				PresentadorAVista();
				if(accion == AccionDetalle.Modificar && pasosBloque.Paso.Etapa.FlujosServicio.IndVigente == true)
					vista.ErroresCustomEditar = "El registro seleccionado no puede ser Editado debido a que el Flujo asociado está actualmente Activo";
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
					PasosBloqueRepositorio repositorio = new PasosBloqueRepositorio(udt);
					if(accion == AccionDetalle.Agregar)
					{
						pasosBloque = new PasosBloque();
						VistaAPresentador(accion);
						udt.MarcarNuevo(pasosBloque);
					}
					else
					{
						pasosBloque = repositorio.ObtenerPorId(vista.Id);
						VistaAPresentador(accion);
						udt.MarcarModificado(pasosBloque);
					}
					udt.Commit();
				}
				else
					vista.Errores = errores;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
				vista.IdBloque = string.Format("{0:N0}", pasosBloque.IdBloque);
				vista.Posicion = string.Format("{0:N0}", pasosBloque.Orden);
				vista.IndColapsado = string.Format("{0:N0}", pasosBloque.IndColapsado);
				vista.IndActualizable = string.Format("{0:N0}", pasosBloque.IndActualizable);
				vista.IdTipoControl = string.Format("{0:N0}", pasosBloque.IdTipoControl);
				vista.TituloBloque = string.Format("{0:N0}", pasosBloque.TituloBloque);
				CargarPublicacion();
				CargarAuditoria();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
					pasosBloque.IdPaso = vista.Id;
				pasosBloque.IdBloque = int.Parse(vista.IdBloque);
				pasosBloque.Orden = int.Parse(vista.Posicion);
				pasosBloque.IndColapsado = bool.Parse(vista.IndColapsado);
				pasosBloque.IndActualizable = bool.Parse(vista.IndActualizable);
				if(vista.IdTipoControl == "")
					pasosBloque.IdTipoControl = null;
				else
					pasosBloque.IdTipoControl = int.Parse(vista.IdTipoControl);
				pasosBloque.TituloBloque = vista.TituloBloque;
				AsignarAuditoria(accion);
				AsignarPublicacion();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
				Metadata<PasosBloque> metadata = new Metadata<PasosBloque>();
				errores.AppendWithBreak(metadata.ValidarPropiedad("Orden", vista.Posicion));
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return errores.ToString();
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombos(AccionDetalle accion)
		{
			try
			{
				BloqueRepositorio repositorioBloque = new BloqueRepositorio(udt);
				vista.ComboIdBloque = repositorioBloque.ObtenerDTO();
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				try
				{
					DataSet ds = servicio.ObtenerListaValorPorNombre("TipoSeleccionBloque");
					if(ds.Tables[@"Error"] != null)
						throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if(ds.Tables[0].Rows.Count > 0)
						vista.ComboIdTipoControl = ds.Tables[0];
				}
				catch(Exception ex)
				{
					Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
					if(ex.InnerException != null)
						HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
					HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
					this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				}
				finally
				{
					if(servicio.State != CommunicationState.Closed)
						servicio.Close();
				}
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
				vista.IndVigente = pasosBloque.IndVigente.ToString();
				vista.FechaValidez = pasosBloque.FechaValidez.ToString();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
				vista.CreadoPor = this.ObtenerNombreUsuario(pasosBloque.CreadoPor);
				vista.FechaCreacion = pasosBloque.FechaCreacion.ToString();
				vista.ModificadoPor = this.ObtenerNombreUsuario(pasosBloque.ModificadoPor);
				vista.FechaModificacion = pasosBloque.FechaModificacion.ToString();
				vista.IndEliminado = pasosBloque.IndEliminado.ToString();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
					pasosBloque.FechaValidez = null;
				else
					pasosBloque.FechaValidez = DateTime.Parse(vista.FechaValidez);
				pasosBloque.IndVigente = bool.Parse(vista.IndVigente);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
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
					pasosBloque.IndEliminado = false;
					pasosBloque.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					pasosBloque.FechaCreacion = DateTime.Now;
					pasosBloque.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					pasosBloque.FechaModificacion = DateTime.Now;
				}
				else if(accion == AccionDetalle.Modificar)
				{
					pasosBloque.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					pasosBloque.FechaModificacion = DateTime.Now;
				}
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public bool VerificarSiDinamico(string idBloque)
		{
			BloqueRepositorio repositorioBloque = new BloqueRepositorio(udt);
			if(repositorioBloque.ObtenerPorId(int.Parse(idBloque)).NombrePrograma == "")
				return true;
			return false;
		}
	}
}