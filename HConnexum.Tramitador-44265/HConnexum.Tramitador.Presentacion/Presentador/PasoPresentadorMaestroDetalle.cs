using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Configuration;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase PasoPresentadorMaestroDetalle.</summary>
	public class PasoPresentadorMaestroDetalle : PresentadorListaBase<Paso>
	{
		///<summary>Variable vista de la interfaz IPasoMaestroDetalle.</summary>
		readonly IPasoMaestroDetalle vista;

		///<summary>Variable de la entidad Paso.</summary>
		Paso _Paso;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de
		///la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PasoPresentadorMaestroDetalle(IPasoMaestroDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType(); 
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, bool cargaGrid)
		{
			try
			{
				if (cargaGrid == true)
				{
					parametrosFiltro.Add(new Infraestructura.Filtro { Campo = "IdPaso", Operador = "EqualTo", Tipo = typeof(int), Valor = vista.Id });
					PasosRepuestaRepositorio repositorioDetalle = new PasosRepuestaRepositorio(unidadDeTrabajo);
					IEnumerable<PasosRepuestaDTO> datos = repositorioDetalle.ObtenerDTO(orden, pagina, tamañoPagina, parametrosFiltro);
					vista.NumeroDeRegistros = repositorioDetalle.Conteo;
					vista.Datos = datos;
				}
				else
				{
					PasoRepositorio repositorioMaestro = new PasoRepositorio(udt);
					_Paso = repositorioMaestro.ObtenerPorId(vista.Id);
					PresentadorAVista();
				}
			}
			catch (Exception e)
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
				PasosRepuestaRepositorio repositorio = new PasosRepuestaRepositorio(unidadDeTrabajo);
				foreach (string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					PasosRepuesta pasosRepuesta = repositorio.ObtenerPorId(idDesencriptado);
					if (!repositorio.EliminarLogico(pasosRepuesta, pasosRepuesta.Id))
						this.vista.Errores = "Ya Existen registros asociados a esta  _" + pasosRepuesta.IdPaso + "";
				}
				unidadDeTrabajo.Commit();
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
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
					PasoRepositorio repositorio = new PasoRepositorio(udt);
					if (accion == AccionDetalle.Agregar)
					{
						_Paso = new Paso();
						VistaAPresentador(accion);
						udt.MarcarNuevo(_Paso);
					}
					else
					{
						_Paso = repositorio.ObtenerPorId(vista.Id);
						if (bool.Parse(vista.IndVigente) == true && _Paso.IndVigente == false)
						{
							string res = ValidarVigenia();
							if (res == "")
							{
								VistaAPresentador(accion);
								udt.MarcarModificado(_Paso);
							}
							else
								errores = res;
						}
						else
						{
							VistaAPresentador(accion);
							udt.MarcarModificado(_Paso);
						}
					}
					udt.Commit();
					ActualizoSla(_Paso.IdEtapa);
				}
				else
					vista.Errores = errores;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void ActualizoSla(int idEtapa)
		{
			udt.IniciarTransaccion();
			PasoRepositorio repositoriopaso = new PasoRepositorio(udt);
			EtapaRepositorio repositorioetapa = new EtapaRepositorio(udt);
			Etapa _etapa = new Etapa();
			_etapa = repositorioetapa.ObtenerPorId(idEtapa);
			_etapa.SlaPromedio = repositoriopaso.ObtenerSLA(idEtapa);
			udt.MarcarModificado(_etapa);
			udt.Commit();
		}

		private string ValidarVigenia()
		{
			if (bool.Parse(vista.IndAgendable) == true)
				if (_Paso.ParametrosAgenda.Count(a => a.IndVigente == true && a.IndEliminado == false) == 0)
					return "No existe un registro en Agenda vigente para este Paso";
			if (_Paso.ChadePaso.Count(a => a.IndEliminado == false) != 0)
				if (_Paso.ChadePaso.Count(a => a.IndVigente == true) == 0)
					return "No existe un CHA vigente para este Paso";
			if (_Paso.MensajesMetodosDestinatario.Count(a => a.IndEliminado == false) != 0)
				if (_Paso.MensajesMetodosDestinatario.Count(a => a.IndVigente == true) == 0)
					return "No existe una configuración de Email o de SMS vigente para este Paso";
			if (_Paso.PasosBloque.Count(a => a.IndEliminado == false) != 0)
				if (_Paso.PasosBloque.Count(a => a.IndVigente == true) == 0)
					return "No existe un bloque vigente para este Paso";
			if (_Paso.DocumentosPaso.Count(a => a.IndEliminado == false) != 0)
				if (_Paso.DocumentosPaso.Count(a => a.IndVigente == true) == 0)
					return "No existe un documento vigente para este Paso";
			return "";
		}

		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{
				vista.Id = _Paso.Id;
				vista.IdTipoPaso = _Paso.IdTipoPaso.ToString();
				vista.IdEstatusInicial = _Paso.IdEstatusInicial.ToString();
				vista.IdSubServicio = _Paso.IdSubServicio.ToString();
				vista.IdAlerta = _Paso.IdAlerta.ToString();
				vista.Nombre = _Paso.Nombre;
				vista.Observacion = _Paso.Observacion;
				vista.URL = _Paso.URL;
				vista.Metodo = _Paso.Metodo;
				vista.PgmObtieneRespuestas = _Paso.PgmObtieneRespuestas;
				vista.IndObligatorio = _Paso.IndObligatorio.ToString();
				vista.CantidadRepeticion = string.Format("{0:N0}", _Paso.CantidadRepeticion);
				vista.IndRequiereRespuesta = _Paso.IndRequiereRespuesta.ToString();
				vista.IndCerrarEtapa = _Paso.IndCerrarEtapa.ToString();
				vista.SlaTolerancia = _Paso.SlaTolerancia.ToString();
				vista.IndIniciaEtapa = _Paso.IndIniciaEtapa.ToString();
				vista.IndSeguimiento = _Paso.IndSeguimiento.ToString();
				vista.IndAgendable = _Paso.IndAgendable.ToString();
				vista.IndCerrarServicio = _Paso.IndCerrarServicio.ToString();
				vista.IndEncadenado = _Paso.IndEncadenado.ToString();
				vista.Reintentos = string.Format("{0:N0}", _Paso.Reintentos);
				vista.IndSegSubServicio = _Paso.IndSegSubServicio.ToString();
				vista.EtiqSincroIn = _Paso.EtiqSincroIn;
				vista.EtiqSincroOut = _Paso.EtiqSincroOut;
				vista.PorcSlaCritico = string.Format("{0:N0}", _Paso.PorcSlaCritico);
				vista.OrdenPaso = string.Format("{0:N0}", _Paso.Orden);
				vista.MetodoAsignacion = _Paso.MetodoAsignacion;
				vista.MetodoAsignacionCha = _Paso.MetodoAsignacionCha;
				vista.IndAnulacion = _Paso.IndAnulacion.ToString();
				CargarPublicacion();
				CargarAuditoria();
			}
			catch (Exception e)
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
				if (accion == AccionDetalle.Agregar)
					_Paso.IdEtapa = vista.Id;
				_Paso.IdTipoPaso = int.Parse(vista.IdTipoPaso);
				if (!string.IsNullOrWhiteSpace(vista.IdEstatusInicial))
					_Paso.IdEstatusInicial = int.Parse(vista.IdEstatusInicial);
				if (vista.IdSubServicio == "")
					_Paso.IdSubServicio = null;
				else
					_Paso.IdSubServicio = int.Parse(vista.IdSubServicio);
				if (vista.IdAlerta == "")
					_Paso.IdAlerta = null;
				else
					_Paso.IdAlerta = int.Parse(vista.IdAlerta);
				_Paso.Nombre = vista.Nombre;
				_Paso.Observacion = vista.Observacion;
				_Paso.URL = vista.URL;
				_Paso.Metodo = vista.Metodo;
				_Paso.PgmObtieneRespuestas = vista.PgmObtieneRespuestas;
				_Paso.EtiqSincroIn = vista.EtiqSincroIn;
				_Paso.EtiqSincroOut = vista.EtiqSincroOut;
				_Paso.IndObligatorio = bool.Parse(vista.IndObligatorio);
				if (vista.CantidadRepeticion == "")

					_Paso.CantidadRepeticion = 0;
				else
					_Paso.CantidadRepeticion = short.Parse(vista.CantidadRepeticion);
				_Paso.IndRequiereRespuesta = bool.Parse(vista.IndRequiereRespuesta);
				_Paso.IndCerrarEtapa = bool.Parse(vista.IndCerrarEtapa);
				_Paso.SlaTolerancia = int.Parse(vista.SlaTolerancia);
				_Paso.IndIniciaEtapa = bool.Parse(vista.IndIniciaEtapa);
				_Paso.IndSeguimiento = bool.Parse(vista.IndSeguimiento);
				_Paso.IndAgendable = bool.Parse(vista.IndAgendable);
				_Paso.IndCerrarServicio = bool.Parse(vista.IndCerrarServicio);
				_Paso.IndEncadenado = bool.Parse(vista.IndEncadenado);
				if (vista.Reintentos == "")
					_Paso.Reintentos = 0;
				else
					_Paso.Reintentos = byte.Parse(vista.Reintentos);
				_Paso.IndSegSubServicio = bool.Parse(vista.IndSegSubServicio);
				if (vista.PorcSlaCritico == "")
					_Paso.PorcSlaCritico = 0;
				else
					_Paso.PorcSlaCritico = int.Parse(vista.PorcSlaCritico);
				_Paso.Orden = int.Parse(vista.OrdenPaso);
				_Paso.MetodoAsignacion = vista.MetodoAsignacion;
				_Paso.MetodoAsignacionCha = vista.MetodoAsignacionCha;
				_Paso.IndAnulacion = bool.Parse(vista.IndAnulacion);
				AsignarAuditoria(accion);
				AsignarPublicacion();
			}
			catch (Exception e)
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
				Metadata<Paso> metadata = new Metadata<Paso>();
				errores.AppendWithBreak(metadata.ValidarPropiedad("Id", vista.Id));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Nombre", vista.Nombre));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Observacion", vista.Observacion));
				errores.AppendWithBreak(metadata.ValidarPropiedad("URL", vista.URL));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Metodo", vista.Metodo));
				errores.AppendWithBreak(metadata.ValidarPropiedad("PgmObtieneRespuestas", vista.PgmObtieneRespuestas));
				errores.AppendWithBreak(metadata.ValidarPropiedad("CantidadRepeticion", vista.CantidadRepeticion));
				errores.AppendWithBreak(metadata.ValidarPropiedad("SlaTolerancia", vista.SlaTolerancia));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Reintentos", vista.Reintentos));
				errores.AppendWithBreak(metadata.ValidarPropiedad("PorcSlaCritico", vista.PorcSlaCritico));
				errores.AppendWithBreak(metadata.ValidarPropiedad("Orden", vista.OrdenPaso));
				return errores.ToString();
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return errores.ToString();
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCabecera(AccionDetalle accion)
		{
			try
			{
				int idServicioSuscriptor = 0;
				int versionFlujoServicio = 0;
				bool indVigenteFlujoServicio = true;
				string nombreEtapa = "";
				if (accion == AccionDetalle.Agregar)
				{
					EtapaRepositorio etapaRepositorio = new EtapaRepositorio(udt);
					EtapaDTO etapa = etapaRepositorio.ObtenerPorIdEtapa(vista.Id);
					idServicioSuscriptor = etapa.IdServicioSuscriptor;
					versionFlujoServicio = etapa.VersionFlujoServicio;
					indVigenteFlujoServicio = etapa.IndVigenteFlujoServicio;
					nombreEtapa = etapa.Nombre;
				}
				else
				{
					PasoRepositorio pasoRepositorio = new PasoRepositorio(udt);
					PasoDTO paso = pasoRepositorio.ObtenerPorIdPaso(vista.Id);
					idServicioSuscriptor = paso.IdServicioSuscriptor;
					versionFlujoServicio = paso.VersionFlujoServicio;
					indVigenteFlujoServicio = paso.IndVigenteFlujoServicio;
					nombreEtapa = paso.NombreEtapa;
				}
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				try
				{
					DataSet ds = servicio.ObtenerServicioSuscriptorPorId(idServicioSuscriptor);
					if (ds.Tables[@"Error"] != null)
						throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if (ds.Tables[0].Rows.Count > 0)
						vista.NombreServicio = ds.Tables[0].Rows[0]["Nombre"].ToString();
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
				if (indVigenteFlujoServicio == true)
				{
					vista.NombreEstatus = "Activo";
					if (accion == AccionDetalle.Modificar)
						vista.ErroresCustomEditar = "El registro seleccionado no puede ser Editado debido a que el Servicio asociado está actualmente Activo";
				}
				else
					vista.NombreEstatus = "Inactivo";
				vista.NombreVersion = versionFlujoServicio.ToString();
				vista.NombreEtapa = nombreEtapa;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboTipoPaso()
		{
			try
			{
				TipoPasoRepositorio repositorioTipoPaso = new TipoPasoRepositorio(udt);
				vista.ComboIdTipoPaso = repositorioTipoPaso.ObtenerDTO();
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboEstatusInicial()
		{
			try
			{
				ListasValorRepositorio listasValorRepositorio = new ListasValorRepositorio(udt);
				vista.ComboIdEstatusInicial = listasValorRepositorio.ObtenerDTOByNombreLista(ConfigurationManager.AppSettings[@"ListaEstatusMovimiento"]);
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboAlerta(AccionDetalle accion)
		{
			try
			{
				int idSuscriptor = 0;
				if (accion == AccionDetalle.Agregar)
				{
					EtapaRepositorio etapaRepositorio = new EtapaRepositorio(udt);
					EtapaDTO etapa = etapaRepositorio.ObtenerPorIdEtapa(vista.Id);
					idSuscriptor = etapa.IdSuscriptor;
				}
				else
				{
					PasoRepositorio pasoRepositorio = new PasoRepositorio(udt);
					PasoDTO paso = pasoRepositorio.ObtenerPorIdPaso(vista.Id);
					idSuscriptor = paso.IdSuscriptor;
				}
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				try
				{
					DataSet ds = servicio.ObtenerAlertasPorIdSuscriptor(idSuscriptor);
					if (ds.Tables[@"Error"] != null)
						throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if (ds.Tables[0].Rows.Count > 0)
						vista.ComboIdAlerta = ds.Tables[0];
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
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboSubServicio(AccionDetalle accion)
		{
			try
			{
				DataTable listadoIdSubServicio = null;
				int idSuscriptor = 0;
				int idFlujoServicio = 0;
				if (accion == AccionDetalle.Agregar)
				{
					EtapaRepositorio etapaRepositorio = new EtapaRepositorio(udt);
					EtapaDTO etapa = etapaRepositorio.ObtenerPorIdEtapa(vista.Id);
					idSuscriptor = etapa.IdSuscriptor;
					idFlujoServicio = etapa.IdFlujoServicio;
				}
				else
				{
					PasoRepositorio pasoRepositorio = new PasoRepositorio(udt);
					PasoDTO paso = pasoRepositorio.ObtenerPorIdPaso(vista.Id);
					idSuscriptor = paso.IdSuscriptor;
					idFlujoServicio = paso.IdFlujoServicio;
				}
				FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(udt);
				IEnumerable<FlujosServicioDTO> listadoFlujoServicio = null;
				listadoFlujoServicio = repositorioFlujosServicio.ObtenerDTO(idSuscriptor, idFlujoServicio);
				List<FlujosServicioDTO> listadoFlujoServicioA = listadoFlujoServicio.ToList();
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				try
				{
					DataSet ds = servicio.ObtenerServiciosPorIdSuscriptor(idSuscriptor);
					if (ds.Tables[@"Error"] != null)
						throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if (ds.Tables[0].Rows.Count > 0)
						listadoIdSubServicio = ds.Tables[0];
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
				for (int i = 0; i < listadoFlujoServicioA.Count; i++)
				{
					int j;
					bool find = false;
					for (j = 0; j < listadoIdSubServicio.Rows.Count && find == false; j++)
						if (listadoFlujoServicioA[i].IdServicioSuscriptor == int.Parse(listadoIdSubServicio.Rows[j]["Id"].ToString()))
							find = true;
					if (find == true)
						listadoFlujoServicioA[i].NombreServicioSuscriptor = listadoIdSubServicio.Rows[j - 1]["Nombre"].ToString();
					else
						listadoFlujoServicioA[i].NombreServicioSuscriptor = "";
				}
				vista.ComboIdSubServicio = listadoFlujoServicioA;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboPasoInicial(AccionDetalle accion)
		{
			try
			{
				int idFlujoServicio = 0;
				int? idPasoInicial = 0;
				if (accion == AccionDetalle.Agregar)
				{
					EtapaRepositorio etapaRepositorio = new EtapaRepositorio(udt);
					EtapaDTO etapa = etapaRepositorio.ObtenerPorIdEtapa(vista.Id);
					idFlujoServicio = etapa.IdFlujoServicio;
				}
				else
				{
					PasoRepositorio pasoRepositorio = new PasoRepositorio(udt);
					PasoDTO paso = pasoRepositorio.ObtenerPorIdPaso(vista.Id);
					idFlujoServicio = paso.IdFlujoServicio;
					idPasoInicial = paso.IdPasoInicial != null ? paso.IdPasoInicial : 0;
				}
				//PasoRepositorio repositorioPaso = new PasoRepositorio(udt);
				//if(accion == AccionDetalle.Agregar)
				//    vista.ComboIdPasoInicial = repositorioPaso.ObtenerPasosInicialDTO(idFlujoServicio);
				//else
				//    vista.ComboIdPasoInicial = repositorioPaso.ObtenerPasosInicialDTOVerEditar(idFlujoServicio, idPasoInicial);
			}
			catch (Exception e)
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
				vista.IndVigente = _Paso.IndVigente.ToString();
				vista.FechaValidez = _Paso.FechaValidez.ToString();
			}
			catch (Exception e)
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
				vista.CreadoPor = this.ObtenerNombreUsuario(_Paso.CreadoPor);
				vista.FechaCreacion = _Paso.FechaCreacion.ToString();
				vista.ModificadoPor = this.ObtenerNombreUsuario(_Paso.ModificadoPor);
				vista.IndEliminado = _Paso.IndEliminado.ToString();
			}
			catch (Exception e)
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
				if (string.IsNullOrEmpty(vista.FechaValidez))
					_Paso.FechaValidez = null;
				else
					_Paso.FechaValidez = DateTime.Parse(vista.FechaValidez);
				_Paso.IndVigente = bool.Parse(vista.IndVigente);
			}
			catch (Exception e)
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
				if (accion == AccionDetalle.Agregar)
				{
					_Paso.IndEliminado = false;
					_Paso.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_Paso.FechaCreacion = DateTime.Now;
					_Paso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_Paso.FechaModificacion = DateTime.Now;
				}
				else if (accion == AccionDetalle.Modificar)
				{
					_Paso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_Paso.FechaModificacion = DateTime.Now;
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}