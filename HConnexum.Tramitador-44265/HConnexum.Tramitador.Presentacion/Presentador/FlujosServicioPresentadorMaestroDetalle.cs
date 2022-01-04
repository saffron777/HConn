using System;
using System.Collections.Generic;
using System.Text;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using HConnexum.Seguridad;
using System.ServiceModel;
using System.Configuration;
using System.Data;
using HConnexum.Tramitador.Datos;
using System.Linq;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase FlujosServicioPresentadorMaestroDetalle.</summary>
	public class FlujosServicioPresentadorMaestroDetalle : PresentadorListaBase<FlujosServicio>
	{
		///<summary>Variable vista de la interfaz IFlujosServicioMaestroDetalle.</summary>
		IFlujosServicioMaestroDetalle vista;

		///<summary>Variable de la entidad FlujosServicio.</summary>
		FlujosServicio _FlujosServicio;
		FlujosServicioDTO __FlujosServicio;

		UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public FlujosServicioPresentadorMaestroDetalle(IFlujosServicioMaestroDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, AccionDetalle accion)
		{
			try
			{
				parametrosFiltro.Add(new Infraestructura.Filtro { Campo = "IdFlujoServicio", Operador = "EqualTo", Tipo = typeof(int), Valor = vista.Id });
				FlujosServicioRepositorio repositorioMaestro = new FlujosServicioRepositorio(udt);
				__FlujosServicio = repositorioMaestro.ObtenerParaEditar(vista.Id);
				PresentadorAVista();
				if(accion == AccionDetalle.Modificar)
				{
					int casos = repositorioMaestro.ObtenerPorId(__FlujosServicio.Id).Caso.Count;
					vista.Casos = casos;
					if(casos > 0)
						vista.ErroresCustom = "Flujo con Casos reportados, imposible su edición. Si desea continuar favor copiarlo.";
					else if(__FlujosServicio.IndVigente == true)
					{
						udt.IniciarTransaccion();
						FlujosServicioRepositorio repositorioFlujo = new FlujosServicioRepositorio(udt);
						_FlujosServicio = repositorioFlujo.ObtenerPorId(__FlujosServicio.Id);
						_FlujosServicio.IndVigente = false;
						udt.MarcarModificado(_FlujosServicio);
						udt.Commit();
						vista.Confirm = "El Flujo ha sido inactivado, si desea reversar la acción presione Cancel.";
					}
				}
				EtapaRepositorio repositorioDetalle = new EtapaRepositorio(unidadDeTrabajo);
				IEnumerable<Etapa> datos = repositorioDetalle.ObtenerFiltrado(orden, pagina, tamañoPagina, parametrosFiltro);
				vista.NumeroDeRegistros = repositorioDetalle.Conteo;
				vista.Datos = datos;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de eliminar registros del conjunto.</summary>
		///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
		public void Eliminar(IList<string> ids)
		{
			try
			{
				unidadDeTrabajo.IniciarTransaccion();
				EtapaRepositorio repositorio = new EtapaRepositorio(unidadDeTrabajo);
				int idFlujoservicio = 0;
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					Etapa _Etapa = repositorio.ObtenerPorId(idDesencriptado);
					idFlujoservicio = _Etapa.IdFlujoServicio;
					if(!repositorio.EliminarLogico(_Etapa, _Etapa.Id))
						this.vista.Errores = "Ya Existen registros asociados a esta  " + _Etapa + "";
				}
				unidadDeTrabajo.Commit();
				ActualizoSlaFlujoServicio(idFlujoservicio);
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de guardar los cambios en BD.</summary>
		///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
		public bool GuardarCambios(AccionDetalle accion, bool validar)
		{
			try
			{
				string errores = ValidarDatos();
				if(errores.Length == 0)
				{
					udt.IniciarTransaccion();
					FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(udt);
					if(accion == AccionDetalle.Agregar)
					{
						_FlujosServicio = new FlujosServicio();
						VistaAPresentador(accion);
						udt.MarcarNuevo(_FlujosServicio);
					}
					else
					{
						_FlujosServicio = repositorio.ObtenerPorId(vista.Id);
						if(validar == true && bool.Parse(vista.IndVigente) == true && _FlujosServicio.IndVigente == false)
						{
							string resVal = ValidarVigencia();
							if(resVal == "")
							{
								VistaAPresentador(accion);
								udt.MarcarModificado(_FlujosServicio);
							}
							else
							{
								vista.Errores = resVal;
								return false;
							}
						}
						else
						{
							VistaAPresentador(accion);
							udt.MarcarModificado(_FlujosServicio);
						}
					}
					udt.Commit();
				}
				else
				{
					vista.Errores = errores;
					return false;
				}
				return true;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				return false;
			}
		}
		public void ActualizoSlaFlujoServicio(int idFlujoServicio)
		{
			udt.IniciarTransaccion();
			EtapaRepositorio repositorioetapa = new EtapaRepositorio(udt);
			FlujosServicioRepositorio repositorioFlujo = new FlujosServicioRepositorio(udt);
			FlujosServicio _Flujo = new FlujosServicio();
			_Flujo = repositorioFlujo.ObtenerPorId(idFlujoServicio);
			_Flujo.SlaPromedio = repositorioetapa.ObtenerSLA(idFlujoServicio);
			udt.MarcarModificado(_Flujo);
			udt.Commit();
		}
		private string ValidarVigencia()
		{
			bool cpasos = false;
			foreach(Etapa et in _FlujosServicio.Etapa)
				if(et.IndVigente == true && et.IndEliminado == false)
					if(et.Paso.Count(a => a.IndVigente == true && a.IndEliminado == false) > 0)
						cpasos = true;
					else
						return "En la etapa '" + et.Nombre + "' no existen pasos válidos";
			if(cpasos == false)
				return "No existen etapas válidas";
			if(string.IsNullOrEmpty(_FlujosServicio.XLMEstructura))
				return "No está configurado el XML";
			if(_FlujosServicio.ServicioSucursal.Count(a => a.IndVigente == true && a.IndEliminado == false) == 0)
				return "No están definidas las sucursales que atienden el servicio";
			bool calcanceg = false;
			foreach(ServicioSucursal ss in _FlujosServicio.ServicioSucursal)
				if(ss.IndVigente == true && ss.IndEliminado == false)
					if(ss.AlcanceGeografico.Count(a => a.IndVigente == true && a.IndEliminado == false) > 0)
						calcanceg = true;
			if(calcanceg == false)
				return "Existen sucursales que atienden el servicio sin alcance geográfico";
			return "";
		}

		public void LLenarCombo(int IdFlujoServicio)
		{
			PasoRepositorio pr = new PasoRepositorio(udt);
			vista.ComboIdPasoInicial = pr.ObtenerDTO(IdFlujoServicio); ;
		}

		public void LLenarCombo2()
		{
			ListasValorRepositorio LVR = new ListasValorRepositorio(udt);
			vista.ComboIdPrioridad = LVR.ObtenerListaValoresDTO("Prioridad Caso");
		}


		public void BuscarDatosparaEditar(int suscriptor, int servicio)
		{
			ServicioParametrizadorClient service = new ServicioParametrizadorClient();
			try
			{
				DataSet ds1 = service.ObtenerSuscriptor_Servicio_Tipo(suscriptor, servicio);
				if(ds1.Tables[@"Error"] != null)
					throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds1.Tables[1].Rows.Count > 0)
					vista.ComboIdServicioSuscriptor = ds1.Tables[1];
				if(ds1.Tables[0].Rows.Count > 0)
				{
					vista.IdSuscriptor = ds1.Tables[0].Rows[0][0].ToString();
					vista.Tipo = ds1.Tables[0].Rows[0][1].ToString();
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
			finally
			{
				if(service.State != CommunicationState.Closed)
					service.Close();
			}
		}

		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{
				vista.Id = __FlujosServicio.Id;
				vista.IndPublico = __FlujosServicio.IndPublico.ToString();
				vista.HiddenId = HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(__FlujosServicio.IdSuscriptor.ToString().Encriptar()));
				BuscarDatosparaEditar(__FlujosServicio.IdSuscriptor, __FlujosServicio.IdServicioSuscriptor);
				vista.IdPasoInicial = string.Format("{0:N0}", __FlujosServicio.IdPasoInicial);
				vista.IdServicioSuscriptor = __FlujosServicio.IdServicioSuscriptor.ToString();
				vista.SlaTolerancia = __FlujosServicio.SlaTolerancia.ToString();
				vista.SlaPromedio = __FlujosServicio.SlaPromedio.ToString();
				vista.IdPrioridad = __FlujosServicio.Prioridad;
				vista.Version = __FlujosServicio.Version;
                vista.IndCms = __FlujosServicio.IndCms.ToString();
                vista.IndSimulable = __FlujosServicio.IndSimulable.ToString();
				vista.IndBloqueGenericoSolicitud = __FlujosServicio.IndBloqueGenericoSolicitud.ToString();
				vista.MetodoPreSolicitud = __FlujosServicio.MetodoPreSolicitud;
				vista.MetodoPostSolicitud = __FlujosServicio.MetodoPostSolicitud;
				vista.FechaModicacion = __FlujosServicio.FechaModicacion.ToString();
				vista.XMLEstructura = __FlujosServicio.XLMEstructura;
				vista.IndChat = __FlujosServicio.IndChat.ToString();
				vista.NombrePrograma = __FlujosServicio.NomPrograma;
				CargarPublicacion();
				CargarAuditoria();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
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
					_FlujosServicio.IdOrigen = ((UsuarioActual)HttpContext.Current.Session["UsuarioActual"]).SuscriptorSeleccionado.Id;
				_FlujosServicio.Id = vista.Id;
				_FlujosServicio.IndPublico = bool.Parse(vista.IndPublico);
				_FlujosServicio.IdSuscriptor = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(vista.HiddenId)).Desencriptar());
				if(!string.IsNullOrEmpty(vista.IdPasoInicial))
					_FlujosServicio.IdPasoInicial = int.Parse(vista.IdPasoInicial);
				else
					_FlujosServicio.IdPasoInicial = null;
				_FlujosServicio.IdServicioSuscriptor = int.Parse(vista.IdServicioSuscriptor);
				if(vista.SlaTolerancia != "")
					_FlujosServicio.SlaTolerancia = int.Parse(vista.SlaTolerancia);
				_FlujosServicio.SlaPromedio = int.Parse(vista.SlaPromedio);
				if(vista.IdPrioridad != 0)
					_FlujosServicio.Prioridad = vista.IdPrioridad;
				else
					_FlujosServicio.Prioridad = 0;
				_FlujosServicio.Version = vista.Version;
				_FlujosServicio.IndCms = bool.Parse(vista.IndCms);
                __FlujosServicio.IndSimulable = bool.Parse(vista.IndSimulable);
				_FlujosServicio.IndBloqueGenericoSolicitud = bool.Parse(vista.IndBloqueGenericoSolicitud);
				_FlujosServicio.MetodoPreSolicitud = vista.MetodoPreSolicitud;
				_FlujosServicio.MetodoPostSolicitud = vista.MetodoPostSolicitud;
				if(vista.XMLEstructura != "")
					_FlujosServicio.XLMEstructura = vista.XMLEstructura;
				_FlujosServicio.IndChat = bool.Parse(vista.IndChat);
				if(!string.IsNullOrEmpty(vista.NombrePrograma))
					_FlujosServicio.NomPrograma = vista.NombrePrograma;
				else
					_FlujosServicio.NomPrograma = null;
				AsignarAuditoria(accion);
				AsignarPublicacion();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
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
				Metadata<FlujosServicio> metadata = new Metadata<FlujosServicio>();
				errores.AppendWithBreak(metadata.ValidarPropiedad("Id", vista.Id));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IndPublico", vista.IndPublico));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IdSuscriptor", System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(vista.HiddenId)).Desencriptar()));//IdSuscriptor
				errores.AppendWithBreak(metadata.ValidarPropiedad("IdServicioSuscriptor", vista.IdServicioSuscriptor));
				errores.AppendWithBreak(metadata.ValidarPropiedad("SlaTolerancia", vista.SlaTolerancia));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Version", vista.Version));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IndCms", vista.IndCms));
				errores.AppendWithBreak(metadata.ValidarPropiedad("FechaModicacion", vista.FechaModicacion));
				return errores.ToString();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return errores.ToString();
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>

		public void ObtenerServicios(string Ids, string tipo)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				string tt = System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(Ids)).Desencriptar();
				DataSet ds1 = servicio.ObtenerServiciosPorIdSuscriptoryTipo(int.Parse(tt), int.Parse(tipo));

				if(ds1.Tables[@"Error"] != null)
					throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds1.Tables[0].Rows.Count > 0)
					vista.ComboIdServicioSuscriptor = ds1.Tables[0];
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

		///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
		private void CargarPublicacion()
		{
			try
			{
				vista.FechaValidez = __FlujosServicio.FechaValidez.ToString();
				vista.IndVigente = __FlujosServicio.IndVigente.ToString();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
		private void CargarAuditoria()
		{
			try
			{
				vista.CreadoPor = this.ObtenerNombreUsuario(__FlujosServicio.CreadoPor);
				vista.FechaCreacion = __FlujosServicio.FechaCreacion.ToString();
				vista.ModificadoPor = this.ObtenerNombreUsuario(__FlujosServicio.ModificadoPor);
				vista.IndEliminado = __FlujosServicio.IndEliminado.ToString();
				vista.FechaModicacion = __FlujosServicio.FechaModicacion.ToString();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores de publicación a la entidad.</summary>
		public void AsignarPublicacion()
		{
			try
			{
				if(string.IsNullOrEmpty(vista.FechaValidez))
					_FlujosServicio.FechaValidez = null;
				else
					_FlujosServicio.FechaValidez = DateTime.Parse(vista.FechaValidez);
				_FlujosServicio.IndVigente = bool.Parse(vista.IndVigente);
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
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
					_FlujosServicio.IndEliminado = false;
					_FlujosServicio.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_FlujosServicio.FechaCreacion = DateTime.Now;
					_FlujosServicio.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_FlujosServicio.FechaModicacion = DateTime.Now;
				}
				else if(accion == AccionDetalle.Modificar)
				{
					_FlujosServicio.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_FlujosServicio.FechaModicacion = DateTime.Now;
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public bool bObtenerRolIndEliminado()
		{
			EtapaRepositorio repositorio = new EtapaRepositorio(this.unidadDeTrabajo);
			return repositorio.obtenerRolIndEliminado();
		}

		public void activarEliminado(IList<string> ids)
		{
			try
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				EtapaRepositorio repositorio = new EtapaRepositorio(this.unidadDeTrabajo);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					Etapa _Aplicacion = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.activarEliminarLogico(_Aplicacion, _Aplicacion.Id))
						this.vista.Errores = "Ya Existen registros asociados a esta Aplicacion";
				}
				this.unidadDeTrabajo.Commit();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

			}
		}
	}
}
