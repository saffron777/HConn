using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class TransporteFlujoServicioPresentador : PresentadorBase<FlujosServicio>
	{
		///<summary>Variable vista de la interfaz ITipoPasoLista.</summary>
		readonly ITransporteFlujoServicio vista;

		/// <summary>Variable Unidad de Trabajo</summary>
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public TransporteFlujoServicioPresentador(ITransporteFlujoServicio vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		public void LlenarCombos()
		{
			try
			{
				FlujosServicioRepositorio flujosServicioRepositorio = new FlujosServicioRepositorio(this.udt);
				this.vista.FlujoServicios = flujosServicioRepositorio.ObtenerFlujosServicios().ToList();
				this.vista.ComboFlujoServicio = this.vista.FlujoServicios.Select(fs => fs.NombreServicioSuscriptor).Distinct();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				if(e.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void LlenarVersion()
		{
			try
			{
				this.vista.ComboVersion = this.vista.FlujoServicios.Where(fs => fs.NombreServicioSuscriptor == vista.IdFlujoServicio);
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				if(e.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void ObtenerDatosFlujoServico()
		{
			try
			{
				#region Definicion de Contadores
				int cantidadAlcanceGeografico = 0;
				int cantidadPasos = 0;
				int cantidadSolicitudBloques = 0;
				int cantidadPasosBloques = 0;
				int cantidadPasosRespuestas = 0;
				int cantidadParametrosAgenda = 0;
				int cantidadChaPasos = 0;
				int cantidadMensajeMetodosDestinatarios = 0;
				int cantidadBloques = 0;
				int cantidadFlujoEjecucion = 0;
				int cantidadDocumentosPasos = 0;
				int cantidadAtributosArchivo = 0;
				int cantidadCamposIndexacion = 0;
				#endregion
				if(!string.IsNullOrWhiteSpace(vista.IdVersion))
				{
					FlujosServicioRepositorio flujosServicioRepositorio = new FlujosServicioRepositorio(this.udt);
					FlujosServicio flujosServicio = flujosServicioRepositorio.ObtenerPorId(int.Parse(vista.IdVersion));
					this.vista.CantidadServiciosSucursales = flujosServicio.ServicioSucursal.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now).ToString();
					foreach(var servicioSucursal in flujosServicio.ServicioSucursal.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
						cantidadAlcanceGeografico += servicioSucursal.AlcanceGeografico.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
					this.vista.CantidadAlcanceGeografico = cantidadAlcanceGeografico.ToString();
					this.vista.CantidadEtapas = flujosServicio.Etapa.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now).ToString();
					IList<int> idsPasos = new List<int>();
					IList<int> idsTipoPaso = new List<int>();
					IList<int> idsSolicitudBloque = new List<int>();
					IList<int> idsPasosBloque = new List<int>();
					IList<int> idsBloque = new List<int>();
					foreach(var etapa in flujosServicio.Etapa.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
					{
						cantidadPasos += etapa.Paso.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
						foreach(var paso in etapa.Paso.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
						{
							idsTipoPaso.Add(paso.TipoPaso.Id);
							if(!idsPasos.Contains(paso.Id))
							{
								idsPasos.Add(paso.Id);
								cantidadPasosBloques += paso.PasosBloque.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
								foreach(var pasosBloque in paso.PasosBloque.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
								{
									if(!idsPasosBloque.Contains(pasosBloque.Id))
									{
										if(pasosBloque.Bloque != null)
											if(pasosBloque.Bloque.IndEliminado == false && pasosBloque.Bloque.IndVigente == true && pasosBloque.Bloque.FechaValidez <= DateTime.Now)
											{
												if(!idsBloque.Contains(pasosBloque.Bloque.Id))
												{
													idsBloque.Add(pasosBloque.Bloque.Id);
													cantidadBloques++;
												}
											}
									}
								}
								cantidadPasosRespuestas += paso.PasosRepuesta.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
								foreach(var pasosRepuesta in paso.PasosRepuesta.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
									cantidadFlujoEjecucion += pasosRepuesta.FlujosEjecucion.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
								cantidadDocumentosPasos += paso.DocumentosPaso.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
								cantidadParametrosAgenda += paso.ParametrosAgenda.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
								cantidadChaPasos += paso.ChadePaso.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
								cantidadMensajeMetodosDestinatarios += paso.MensajesMetodosDestinatario.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
							}
						}
					}
					cantidadSolicitudBloques += flujosServicio.SolicitudBloque.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
					foreach(var solicitudBloque in flujosServicio.SolicitudBloque.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
					{
						if(!idsSolicitudBloque.Contains(solicitudBloque.Id))
						{
							if(solicitudBloque.Bloque != null)
								if(solicitudBloque.Bloque.IndEliminado == false && solicitudBloque.Bloque.IndVigente == true && solicitudBloque.Bloque.FechaValidez <= DateTime.Now)
								{
									if(!idsBloque.Contains(solicitudBloque.Bloque.Id))
									{
										idsBloque.Add(solicitudBloque.Bloque.Id);
										cantidadBloques++;
									}
								}
						}
					}
					this.vista.CantidadSolicitudBloques = cantidadSolicitudBloques.ToString();
					this.vista.CantidadPasos = cantidadPasos.ToString();
					this.vista.CantidadTipoPasos = idsTipoPaso.Distinct().Count().ToString();
					this.vista.CantidadPasosBloques = cantidadPasosBloques.ToString();
					this.vista.CantidadPasosRespuestas = cantidadPasosRespuestas.ToString();
					this.vista.CantidadParametrosAgenda = cantidadParametrosAgenda.ToString();
					this.vista.CantidadChaPasos = cantidadChaPasos.ToString();
					this.vista.CantidadMensajeMetodosDestinatarios = cantidadMensajeMetodosDestinatarios.ToString();
					this.vista.CantidadBloques = cantidadBloques.ToString();
					this.vista.CantidadFlujoEjecucion = cantidadFlujoEjecucion.ToString();
					this.vista.CantidadDocumentosPasos = cantidadDocumentosPasos.ToString();
					IList<int> idDocumentos = new List<int>();
					this.vista.CantidadDocumentosServicios = flujosServicio.DocumentosServicio.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now).ToString();
					foreach(var documentosServicio in flujosServicio.DocumentosServicio.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
						idDocumentos.Add(documentosServicio.IdDocumento);
					this.vista.CantidadDocumentos = idDocumentos.Distinct().Count().ToString();
					DocumentoRepositorio documentoRepositorio = new DocumentoRepositorio(udt);
					IEnumerable<Documento> documentos = documentoRepositorio.ObtenerDocumentosPorIds(idDocumentos);
					foreach(var documento in documentos)
					{
						cantidadAtributosArchivo += documento.AtributosArchivo.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
						cantidadCamposIndexacion += documento.CamposIndexacion.Count(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now);
					}
					this.vista.CantidadAtributosArchivo = cantidadAtributosArchivo.ToString();
					this.vista.CantidadCamposIndexacion = cantidadCamposIndexacion.ToString();
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				if(e.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public EntityObject Clonar(EntityObject nuevaEntidad, EntityObject entidad)
		{
			var type = entidad.GetType();
			foreach(var property in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.SetProperty))
			{
				if(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(EntityReference<>)) continue;
				if(property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(EntityCollection<>)) continue;
				if(property.PropertyType.IsSubclassOf(typeof(EntityObject))) continue;
				if(property.CanWrite)
					property.SetValue(nuevaEntidad, property.GetValue(entidad, null), null);
			}
			return nuevaEntidad;
		}

		public bool GenerarFlujoServico()
		{
			try
			{
				if(ProbarConexion())
				{
					#region Connexion Tramitador
					SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
					sqlBuilder.DataSource = vista.Servidor + @"\" + vista.Instancia;
					sqlBuilder.InitialCatalog = vista.BD;
					sqlBuilder.UserID = vista.Usuario;
					sqlBuilder.Password = vista.Contrasena;
					sqlBuilder.IntegratedSecurity = false;
					sqlBuilder.MultipleActiveResultSets = true;
					EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
					entityBuilder.Provider = @"System.Data.SqlClient";
					entityBuilder.ProviderConnectionString = sqlBuilder.ToString();
					entityBuilder.Metadata = @"res://*/HC_Tramitador_Modelo.csdl|res://*/HC_Tramitador_Modelo.ssdl|res://*/HC_Tramitador_Modelo.msl";
					EntityConnection connection = new EntityConnection(entityBuilder.ToString());
					IUnidadDeTrabajo udtDestino = new UnidadDeTrabajo(connection);
					#endregion
					if(!string.IsNullOrWhiteSpace(vista.IdVersion))
					{
						udtDestino.IniciarTransaccion();
						FlujosServicioRepositorio flujosServicioRepositorio = new FlujosServicioRepositorio(this.udt);
						FlujosServicioRepositorio flujosServicioRepositorioDestino = new FlujosServicioRepositorio(udtDestino);
						FlujosServicio flujosServicio = flujosServicioRepositorio.ObtenerPorId(int.Parse(vista.IdVersion));
						bool existeServicio = flujosServicioRepositorioDestino.BuscarFlujosServicio(flujosServicio) != 0;
						FlujosServicio flujosServicioDestino = Clonar(new FlujosServicio(), flujosServicio) as FlujosServicio;
						if(existeServicio)
						{
							int version = flujosServicioRepositorioDestino.BuscarVersionFlujosServicio(flujosServicio);
							flujosServicioDestino.Version = version + 1;
						}
						foreach(var servicioSucursal in flujosServicio.ServicioSucursal.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
						{
							ServicioSucursal servicioSucursalDestino = Clonar(new ServicioSucursal(), servicioSucursal) as ServicioSucursal;
							foreach(var alcanceGeografico in servicioSucursal.AlcanceGeografico.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
								servicioSucursalDestino.AlcanceGeografico.Add(Clonar(new AlcanceGeografico(), alcanceGeografico) as AlcanceGeografico);
							flujosServicioDestino.ServicioSucursal.Add(servicioSucursalDestino);
						}
						IList<int> idsPasos = new List<int>();
						IList<int> idsTipoPaso = new List<int>();
						IList<int> idsPasosBloque = new List<int>();
						IList<int> idsBloque = new List<int>();
						IList<int> idsSolicitudBloque = new List<int>();
						IList<int> idsBloquesSolicitud = new List<int>();
						foreach(var etapa in flujosServicio.Etapa.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
						{
							Etapa etapaDestino = Clonar(new Etapa(), etapa) as Etapa;
							foreach(var paso in etapa.Paso.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
							{
								Paso pasoDestino = Clonar(new Paso(), paso) as Paso;
								int idTipoPaso = 0;
								if(!idsPasos.Contains(paso.Id))
								{
									idsPasos.Add(paso.Id);
									foreach(var pasosBloque in paso.PasosBloque.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
									{
										PasosBloque pasosBloqueDestino = Clonar(new PasosBloque(), pasosBloque) as PasosBloque;
										if(!idsPasosBloque.Contains(pasosBloque.Id))
										{
											idsPasosBloque.Add(pasosBloque.Id);
											int idBloque = 0;
											if(pasosBloque.Bloque != null)
												if(pasosBloque.Bloque.IndEliminado == false && pasosBloque.Bloque.IndVigente == true && pasosBloque.Bloque.FechaValidez <= DateTime.Now)
												{
													BloqueRepositorio bloqueRepositorioDestino = new BloqueRepositorio(udtDestino);
													idBloque = bloqueRepositorioDestino.BuscarBloque(pasosBloque.Bloque);
													if(!idsBloque.Contains(pasosBloque.Bloque.Id))
													{
														idsBloque.Add(pasosBloque.Bloque.Id);
														if(idBloque == 0)
															udtDestino.MarcarNuevo(Clonar(new Bloque(), pasosBloque.Bloque) as Bloque);
													}
												}
											if(idBloque != 0)
												pasosBloqueDestino.IdBloque = idBloque;
											pasoDestino.PasosBloque.Add(pasosBloqueDestino);
										}
									}
									foreach(var pasosRepuesta in paso.PasosRepuesta.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
									{
										PasosRepuesta pasosRepuestaDestino = Clonar(new PasosRepuesta(), pasosRepuesta) as PasosRepuesta;
										foreach(var flujosEjecucion in pasosRepuesta.FlujosEjecucion.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
											pasosRepuestaDestino.FlujosEjecucion.Add(Clonar(new FlujosEjecucion(), flujosEjecucion) as FlujosEjecucion);
										pasoDestino.PasosRepuesta.Add(pasosRepuestaDestino);
									}
									foreach(var documentosPaso in paso.DocumentosPaso.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
										pasoDestino.DocumentosPaso.Add(Clonar(new DocumentosPaso(), documentosPaso) as DocumentosPaso);
									foreach(var parametrosAgenda in paso.ParametrosAgenda.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
										pasoDestino.ParametrosAgenda.Add(Clonar(new ParametrosAgenda(), parametrosAgenda) as ParametrosAgenda);
									foreach(var chadePaso in paso.ChadePaso.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
										pasoDestino.ChadePaso.Add(Clonar(new ChadePaso(), chadePaso) as ChadePaso);
									foreach(var mensajesMetodosDestinatario in paso.MensajesMetodosDestinatario.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
										pasoDestino.MensajesMetodosDestinatario.Add(Clonar(new MensajesMetodosDestinatario(), mensajesMetodosDestinatario) as MensajesMetodosDestinatario);
									if(paso.TipoPaso != null)
										if(paso.TipoPaso.IndEliminado == false && paso.TipoPaso.IndVigente == true && paso.TipoPaso.FechaValidez <= DateTime.Now)
										{
											TipoPasoRepositorio tipoPasoRepositorioDestino = new TipoPasoRepositorio(udtDestino);
											idTipoPaso = tipoPasoRepositorioDestino.BuscarTipoPaso(paso.TipoPaso);
											if(!idsTipoPaso.Contains(paso.TipoPaso.Id))
											{
												idsTipoPaso.Add(paso.TipoPaso.Id);
												if(idTipoPaso == 0)
													udtDestino.MarcarNuevo(Clonar(new TipoPaso(), paso.TipoPaso) as TipoPaso);
											}
										}
									if(idTipoPaso != 0)
										pasoDestino.IdTipoPaso = idTipoPaso;
									etapaDestino.Paso.Add(pasoDestino);
								}
							}
							flujosServicioDestino.Etapa.Add(etapaDestino);
						}
						foreach(var solicitudBloque in flujosServicio.SolicitudBloque.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
						{
							SolicitudBloque solicitudBloqueDestino = Clonar(new SolicitudBloque(), solicitudBloque) as SolicitudBloque;
							if(!idsSolicitudBloque.Contains(solicitudBloque.Id))
							{
								idsSolicitudBloque.Add(solicitudBloque.Id);
								int idBloque = 0;
								if(solicitudBloque.Bloque != null)
									if(solicitudBloque.Bloque.IndEliminado == false && solicitudBloque.Bloque.IndVigente == true && solicitudBloque.Bloque.FechaValidez <= DateTime.Now)
									{
										BloqueRepositorio bloqueRepositorioDestino = new BloqueRepositorio(udtDestino);
										idBloque = bloqueRepositorioDestino.BuscarBloque(solicitudBloque.Bloque);
										if(!idsBloquesSolicitud.Contains(solicitudBloque.Bloque.Id))
										{
											idsBloquesSolicitud.Add(solicitudBloque.Bloque.Id);
											if(idBloque == 0)
												udtDestino.MarcarNuevo(Clonar(new Bloque(), solicitudBloque.Bloque) as Bloque);
										}
									}
								if(idBloque != 0)
									solicitudBloqueDestino.IdBloque = idBloque;
								flujosServicioDestino.SolicitudBloque.Add(solicitudBloqueDestino);
							}
						}
						IList<int> idDocumentos = new List<int>();
						DocumentoRepositorio documentoRepositorioDestino = new DocumentoRepositorio(udtDestino);
						foreach(var documentosServicio in flujosServicio.DocumentosServicio.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
						{
							int idDocumento = 0;
							if(documentosServicio.Documento != null)
								if(documentosServicio.Documento.IndEliminado == false && documentosServicio.Documento.IndVigente == true && documentosServicio.Documento.FechaValidez <= DateTime.Now)
									idDocumento = documentoRepositorioDestino.BuscarDocumento(documentosServicio.Documento);
							DocumentosServicio documentosServicioDestino = Clonar(new DocumentosServicio(), documentosServicio) as DocumentosServicio;
							if(idDocumento != 0)
								documentosServicioDestino.IdDocumento = idDocumento;
							flujosServicioDestino.DocumentosServicio.Add(documentosServicioDestino);
							idDocumentos.Add(documentosServicio.IdDocumento);
						}
						udtDestino.MarcarNuevo(flujosServicioDestino);
						DocumentoRepositorio documentoRepositorio = new DocumentoRepositorio(udt);
						IEnumerable<Documento> documentos = documentoRepositorio.ObtenerDocumentosPorIds(idDocumentos);
						foreach(var documento in documentos)
						{
							int idDocumento = documentoRepositorioDestino.BuscarDocumento(documento);
							if(idDocumento == 0)
							{
								Documento documentoDestino = Clonar(new Documento(), documento) as Documento;
								foreach(var atributosArchivo in documento.AtributosArchivo.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
									documentoDestino.AtributosArchivo.Add(Clonar(new AtributosArchivo(), atributosArchivo) as AtributosArchivo);
								foreach(var camposIndexacion in documento.CamposIndexacion.Where(i => i.IndEliminado == false && i.IndVigente == true && i.FechaValidez <= DateTime.Now))
									documentoDestino.CamposIndexacion.Add(Clonar(new CamposIndexacion(), camposIndexacion) as CamposIndexacion);
								udtDestino.MarcarNuevo(documentoDestino);
							}
						}
						udtDestino.Commit();
						if(existeServicio)
							vista.Mensaje = @"El flujo servicio seleccionado ya existe en la base de datos destino.<br/><br/>Fue generado existosamente con la version " + flujosServicioDestino.Version;
						else
							vista.Mensaje = @"El flujo servicio seleccionado fue copiado exitosamente.";
						return true;
					}
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				if(e.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return false;
		}

		public bool ProbarConexion()
		{
			try
			{
				SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
				sqlBuilder.DataSource = vista.Servidor + @"\" + vista.Instancia;
				sqlBuilder.InitialCatalog = vista.BD;
				sqlBuilder.UserID = vista.Usuario;
				sqlBuilder.Password = vista.Contrasena;
				sqlBuilder.IntegratedSecurity = false;
				sqlBuilder.MultipleActiveResultSets = true;
				using(SqlConnection sqlConn = new SqlConnection(sqlBuilder.ToString()))
				{
					sqlConn.Open();
					if(sqlConn.State == ConnectionState.Open)
						vista.Mensaje = "Conexión Exitosa.";
				}
			}
			catch(SqlException e)
			{
				vista.Mensaje = e.Message;
				return false;
			}
			catch(Exception e)
			{
				vista.Mensaje = e.Message;
				return false;
			}
			return true;
		}
	}
}