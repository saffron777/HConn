using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PresentadorComprobanteMovimientoCE : PresentadorBase<Movimiento>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IComprobanteMovimientoCE vista;

		public PresentadorComprobanteMovimientoCE(IComprobanteMovimientoCE vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = this.GetType();
		}

		public string ObtenerUsuarioActual()
		{
			return string.Format("{0} {1}", this.vista.UsuarioActual.DatosBase.Nombre1.ToString(), this.vista.UsuarioActual.DatosBase.Apellido1.ToString());
		}

		public string ObtenerConexionString(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerConexionStringListaValor(idSuscriptor, ConfigurationManager.AppSettings[@"ListaOrigenDB"]))
				{
					if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
					{
						return ds.Tables[0].Rows[0][@"ConexionString"].ToString();
					}
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
				{
					servicio.Close();
				}
			}
			return string.Empty;
		}

		public IQueryable<PolizasDTO> ConvertToEntity(DataTable coleccion)
		{
			return (from Tabcoleccion in coleccion.AsEnumerable()
					select new PolizasDTO
					{
						Contratante = Tabcoleccion.Field<string>(@"Contratante"),
						Certificado = Tabcoleccion.Field<int>(@"Certificado"),
						Parentesco = Tabcoleccion.Field<string>(@"Parentesco"),
						Cobertura = Tabcoleccion.Field<string>(@"Cobertura"),
						Diasgnostico = Tabcoleccion.Field<string>(@"Diagnostico"),
						MontoFacturado = Tabcoleccion.Field<double>(@"Mon_Fac_Rec"),
						Deducible = Tabcoleccion.Field<double>(@"Deducible"),
						MontoCubierto = Tabcoleccion.Field<double>(@"MontoCubierto"),
					}).AsQueryable();
		}

		public IEnumerable<ComprobanteMovimientoCEDTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
		{
			IQueryable<PolizasDTO> datosReclamo = this.ConvertToEntity(dataSetColeccion.Tables[@"DatosReclamo"]);

			double montoFacturado = datosReclamo.Count() == 0 ? 0 : datosReclamo.FirstOrDefault().MontoFacturado;
			double deducible = datosReclamo.Count() == 0 ? 0 : datosReclamo.FirstOrDefault().Deducible;
			double totalGastosNoCubiertos = datosReclamo.Count() == 0 ? 0 : datosReclamo.FirstOrDefault().MontoFacturado - (datosReclamo.Sum(p => p.MontoCubierto) + datosReclamo.FirstOrDefault().Deducible);
			if (totalGastosNoCubiertos < 0)
			{
				totalGastosNoCubiertos = 0;
			}
			double gastosNoCubiertos = totalGastosNoCubiertos;
			double montoCubierto = datosReclamo.Count() == 0 ? 0 : datosReclamo.Sum(p => p.MontoCubierto);

			var iEnumerableColeccion = from tabDatosMovimiento in dataSetColeccion.Tables[@"DatosMovimiento"].AsEnumerable()
									   orderby tabDatosMovimiento.Field<DateTime>("Hora_Registro") descending
									   select new ComprobanteMovimientoCEDTO
									   {
										   Logo = (from tabDatosLogo in dataSetColeccion.Tables[@"DatosLogo"].AsEnumerable()
												   select tabDatosLogo.Field<string>(@"logo")).SingleOrDefault(),
										   DatosPaciente = (from tabDatosPaciente in dataSetColeccion.Tables["DatosPaciente"].AsEnumerable()
															select new DatosPacienteDTO
															{
																PacienteAsegurado = tabDatosPaciente.Field<string>(@"a02Asegurado"),
																CedulaAsegurado = tabDatosPaciente.Field<string>(@"a02NoDocasegurado"),
																SexoPaciente = tabDatosPaciente.Field<string>(@"a02Sexo"),
																FechaNacPaciente = tabDatosPaciente.Field<DateTime?>("Fecha_Nacimiento"),
																EstadoPaciente = tabDatosPaciente.Field<string>(@"a02Estado"),
																Parentesco = tabDatosPaciente.Field<string>("Parentesco_txt"),
																ContratantePoliza = tabDatosPaciente.Field<string>(@"a00Contratante"),
																Poliza = tabDatosPaciente.Field<int?>(@"a00NroPoliza"),
																Certificado = tabDatosPaciente.Field<int?>(@"a00Certificado"),
																FechaDesde = tabDatosPaciente.Field<DateTime?>(@"a00FechaDesde"),
																FechaHasta = tabDatosPaciente.Field<DateTime?>(@"a00FechaHasta")
															}).FirstOrDefault() ?? (from tabDatosPaciente in dataSetColeccion.Tables[@"DatosMovimiento"].AsEnumerable()
																					select new DatosPacienteDTO
																					{
																						PacienteAsegurado = tabDatosPaciente.Field<string>(@"Asegurado"),
																						CedulaAsegurado = tabDatosPaciente.Field<string>("CI_Asegurado"),
																						SexoPaciente = tabDatosPaciente.Field<string>("Sexo_Asegurado"),
																						FechaNacPaciente = tabDatosPaciente.Field<DateTime?>("Fecha_Nac_Asegurado"),
																						EstadoPaciente = string.Empty
																					}).FirstOrDefault(),
																  DatosTitular = (from tabDatosTitular in dataSetColeccion.Tables[@"DatosTitular"].AsEnumerable()
																				  select new DatosTitularMovDTO
																				  {
																					  TitularAsegurado = tabDatosTitular.Field<string>(@"a02asegurado"),
																					  CedulaTitular = tabDatosTitular.Field<string>(@"a02NoDocasegurado"),
																					  SexoTitular = tabDatosTitular.Field<string>(@"a02Sexo"),
																					  FechaNacTitular = tabDatosTitular.Field<DateTime?>("Fecha_Nacimiento"),
																					  EstadoTitular = tabDatosTitular.Field<string>(@"a02Estado")
																				  }).FirstOrDefault() ?? (from tabDatosTitular in dataSetColeccion.Tables[@"DatosTitular"].AsEnumerable()
																										  select new DatosTitularMovDTO
																										  {
																											  TitularAsegurado = tabDatosTitular.Field<string>(@"Titular"),
																											  CedulaTitular = tabDatosTitular.Field<string>("Nac_Titular") + " - " + tabDatosTitular.Field<string>("CI_Titular"),
																											  SexoTitular = tabDatosTitular.Field<string>("Sexo_Titular"),
																											  FechaNacTitular = tabDatosTitular.Field<DateTime?>("Fecha_Nac_Titular"),
																											  EstadoTitular = string.Empty
																										  }).FirstOrDefault(),
																						 DatosSolicitud = (from tabDatosSolicitud in dataSetColeccion.Tables[@"DatosSolicitud"].AsEnumerable()
																										   select new DatosSolicitudMovDTO
																										   {
																											   CodClave = tabDatosSolicitud.Field<int?>(@"clave").ToString() + " - " + tabDatosSolicitud.Field<string>("Nro_Reclamo"),
																											   Categoria = tabDatosSolicitud.Field<string>("Category_Text"),
																											   FechaOcurrencia = tabDatosSolicitud.Field<DateTime?>("Fec_Ocurrencia"),
																											   Diagnostico = tabDatosSolicitud.Field<string>("Diag_text"),
																											   Clinica = tabDatosSolicitud.Field<string>(@"Clinica"),
																											   MontoFactura = montoFacturado,
																											   MontoCubierto = montoCubierto,
																											   GastosnoCubiertos = gastosNoCubiertos
																										   }).FirstOrDefault() ?? (from tabDatosSolicitud in dataSetColeccion.Tables[@"DatosMovimiento"].AsEnumerable()
																																   select new DatosSolicitudMovDTO
																																   {
																																	   CodClave = tabDatosSolicitud.Field<int?>("Id_Exp_Web").ToString() + " - " + tabDatosSolicitud.Field<int?>("Id_Movimiento_WEB"),
																																	   Categoria = string.Empty,
																																	   FechaOcurrencia = tabDatosSolicitud.Field<DateTime?>(@"Fecha_Ocurrencia")
																																   }).FirstOrDefault(),
																												UltiMovHecho = !dataSetColeccion.Tables[@"DatosPaciente"].Equals(null) ? tabDatosMovimiento.Field<string>("Ultimo_Mov_Hecho") : string.Empty,
																												Responsable = !dataSetColeccion.Tables[@"DatosPaciente"].Equals(null) ? tabDatosMovimiento.Field<string>(@"responsable") : string.Empty,
																												MontoFacturado = montoFacturado,
																												GastosNoCubiertos = gastosNoCubiertos,
																												MontoDeducible = deducible,
																												MontoCubierto = montoCubierto,
																												DiasHosp = tabDatosMovimiento.Field<int?>("Dias_Hosp"),
																												ObservacionesProcesadas = !dataSetColeccion.Tables[@"DatosPaciente"].Equals(null) ? tabDatosMovimiento.Field<string>(@"observadef") : tabDatosMovimiento.Field<string>(@"Comentarios_Adicionales"),
																												DocumentosFaxSolicitados = !dataSetColeccion.Tables[@"DatosPaciente"].Equals(null) ? tabDatosMovimiento.Field<string>(@"Documentos_Fax_Adicionales") : tabDatosMovimiento.Field<string>(@"Documentos_Fax"),
																												TipoMovimiento = tabDatosMovimiento.Field<string>(@"Tipo_Movimiento"),
																												Sintomas = tabDatosMovimiento.Field<string>(@"sintomas"),
																												FechaOcurrencia = tabDatosMovimiento.Field<DateTime?>(@"Fecha_Ocurrencia"),
																												FechaMovimiento = tabDatosMovimiento.Field<DateTime?>(@"Fecha_Movimiento"),
																												HoraRegistro = tabDatosMovimiento.Field<DateTime?>(@"Hora_Registro"),
																												Resultado = tabDatosMovimiento.Field<string>(@"resultado"),
																												Observaciones = tabDatosMovimiento.Field<string>(@"observacionesweb"),
																												BarCod = !dataSetColeccion.Tables[@"DatosPaciente"].Equals(null) ? dataSetColeccion.Tables["DatosPaciente"].Rows[0][@"support"].ToString() : tabDatosMovimiento.Field<string>(@"support")
									   };
			return iEnumerableColeccion;
		}

		public IEnumerable<ComprobanteMovimientoCEDTO> GenerarConsultaComprobante(int idexpweb, int seguro)
		{
			string reporte = "DisenoComprobanteMovCE";
			try
			{
				string conexionString = this.ObtenerConexionString(seguro);
				if (!string.IsNullOrWhiteSpace(conexionString))
				{
					DataSet dataSetColeccion = new DataSet();
					
					#region DatosLogo
					
					UnidadDeTrabajo udt = new UnidadDeTrabajo();
					SuscriptorRepositorio suscriptorreporsitorio = new SuscriptorRepositorio(udt);
					dataSetColeccion.Tables.Add(@"DatosLogo");
					dataSetColeccion.Tables[@"DatosLogo"].Columns.Add(@"logo", typeof(string));
					string logo = suscriptorreporsitorio.ObtenerLogo(seguro, SuscriptorRepositorio.TipoId.IdSuscriptor);
					if (string.IsNullOrEmpty(logo))
					{
						try
						{
							Errores.ManejarError(new CustomException(string.Format("El método suscriptorreporsitorio.ObtenerLogo no arrojó resultados para id={0} y tipoId={1}", seguro, SuscriptorRepositorio.TipoId.IdSuscriptor.ToString())), string.Format("Reporte: {0}", reporte));
						}
						catch
						{
						}
					}
					dataSetColeccion.Tables[@"DatosLogo"].Rows.Add(logo);
					
					#endregion
					
					string sp = "";
					ConexionADO servicio = new ConexionADO();
					SqlParameter[] colleccionParameter = null;
					
					#region DatosPaciente
					
					colleccionParameter = new SqlParameter[1];
					colleccionParameter[0] = new SqlParameter("@valor", idexpweb);
					sp = ConfigurationManager.AppSettings[@"SpDetalleMovimiento"];
					if (string.IsNullOrEmpty(sp))
					{
						throw new CustomException("La clave SpDetalleMovimiento del Wev.Config no arrojó resultados", ErrorType.Other);
					}
					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0].Copy());
					dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosPaciente";
						
					#endregion
						
					if (dataSetColeccion.Tables[@"DatosPaciente"].Rows.Count == 0)
					{
						dataSetColeccion.Tables.Remove(@"DatosPaciente");
							
						#region DatosMovimiento
							
						sp = ConfigurationManager.AppSettings[@"SpDetalleMovimientoWaf"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDetalleMovimientoWaf del Wev.Config no arrojó resultados", ErrorType.Other);
						}
						dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0].Copy());
						if (dataSetColeccion.Tables[@"DatosGestor"].Rows.Count == 0)
						{
							try
							{
								Errores.ManejarError(new CustomException(string.Format("El SP {0} no arrojó resultados para valor={1}", sp, idexpweb.ToString())), string.Format("Reporte: {0}", reporte));
							}
							catch
							{
							}
						}
						dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosMovimiento";
						
						#endregion
					
					}
					else
					{
						
						#region DatosTitular
						
						colleccionParameter = new SqlParameter[3];
						colleccionParameter[0] = new SqlParameter("@Nropoliza", dataSetColeccion.Tables[@"DatosPaciente"].Rows[0][@"a00NroPoliza"]);
						colleccionParameter[1] = new SqlParameter("@Certificado", dataSetColeccion.Tables[@"DatosPaciente"].Rows[0][@"a00Certificado"]);
						colleccionParameter[2] = new SqlParameter("@Error", SqlDbType.Int, 1, ParameterDirection.Output, false, 1, 1, "", DataRowVersion.Current, 1);
						sp = ConfigurationManager.AppSettings[@"SpDatosTitular"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDatosTitular del Wev.Config no arrojó resultados", ErrorType.Other);
						}
						dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0].Copy());
						if (dataSetColeccion.Tables[@"DatosGestor"].Rows.Count == 0)
						{
							try
							{
								Errores.ManejarError(new CustomException(string.Format("El SP {0} no arrojó resultados para Nropoliza={1} y Certificado={2}", sp, dataSetColeccion.Tables[@"DatosPaciente"].Rows[0][@"a00NroPoliza"], dataSetColeccion.Tables[@"DatosPaciente"].Rows[0][@"a00Certificado"])), string.Format("Reporte: {0}", reporte));
							}
							catch
							{
							}
						}
						dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosTitular";
							
						#endregion
						
						#region DatosSolicitud
						
						colleccionParameter = new SqlParameter[2];
						colleccionParameter[0] = new SqlParameter("@Expediente_Web_id", dataSetColeccion.Tables[@"DatosPaciente"].Rows[0]["Id_Exp_Web"]);
						colleccionParameter[1] = new SqlParameter("@Error", SqlDbType.Int, 1, ParameterDirection.Output, false, 1, 1, "", DataRowVersion.Current, 1);
						sp = ConfigurationManager.AppSettings[@"SpDatosSolicitudMovCE"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDatosSolicitudMovCE del Wev.Config no arrojó resultados", ErrorType.Other);
						}
						dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0].Copy());
						if (dataSetColeccion.Tables[@"DatosGestor"].Rows.Count == 0)
						{
							try
							{
								Errores.ManejarError(new CustomException(string.Format("El SP {0} no arrojó resultados para Expediente_Web_id={1}", sp, dataSetColeccion.Tables[@"DatosPaciente"].Rows[0]["Id_Exp_Web"])), string.Format("Reporte: {0}", reporte));
							}
							catch
							{
							}
						}
						dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosSolicitud";
						
						#endregion
						
						#region DatosMovimiento
						
						colleccionParameter[0] = new SqlParameter("@Reclamos_WEB_ID", idexpweb);
						colleccionParameter[1] = new SqlParameter("@Error", SqlDbType.Int, 1, ParameterDirection.Output, false, 1, 1, "", DataRowVersion.Current, 1);
						sp = ConfigurationManager.AppSettings[@"SpDatosMovimiento"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDatosMovimiento del Wev.Config no arrojó resultados", ErrorType.Other);
						}
						DataTable spDatosMovimiento = servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0];
						if (spDatosMovimiento.Rows.Count == 0)
						{
							try
							{
								Errores.ManejarError(new CustomException(string.Format("El SP {0} no arrojó resultados para Reclamos_WEB_ID={1}", sp, idexpweb.ToString())), string.Format("Reporte: {0}", reporte));
							}
							catch
							{
							}
						}
						sp = ConfigurationManager.AppSettings[@"SpDatosMovimientoV2"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDatosMovimientoV2 del Wev.Config no arrojó resultados", ErrorType.Other);
						}
						DataTable spDatosMovimientoV2 = servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0];
						if (spDatosMovimientoV2.Rows.Count == 0)
						{
							try
							{
								Errores.ManejarError(new CustomException(string.Format("El SP {0} no arrojó resultados para Reclamos_WEB_ID={1}", sp, idexpweb.ToString())), string.Format("Reporte: {0}", reporte));
							}
							catch
							{
							}
						}
						dataSetColeccion.Tables.Add(spDatosMovimiento.Copy());
						dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosMovimiento";
						dataSetColeccion.Tables[@"DatosMovimiento"].Merge(spDatosMovimientoV2.Clone());
						if (spDatosMovimiento.Rows.Count != 0 && spDatosMovimientoV2.Rows.Count != 0)
						{
							int j;
							for (int t = 0; t <= (spDatosMovimiento.Rows.Count - 1); t++)
							{
								j = 0;
								for (int i = spDatosMovimiento.Columns.Count; i < (spDatosMovimiento.Columns.Count + spDatosMovimientoV2.Columns.Count); i++)
								{
									dataSetColeccion.Tables[@"DatosMovimiento"].Rows[t][i] = spDatosMovimientoV2.Rows[t][j];
									j++;
								}
							}
						}
					
						#endregion
						
						#region DatosReclamo
						
						colleccionParameter = new SqlParameter[1];
						colleccionParameter[0] = new SqlParameter("@idExpedienteWeb", idexpweb);
						sp = ConfigurationManager.AppSettings[@"SpBuscaReclamo"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpBuscaReclamo del Wev.Config no arrojó resultados", ErrorType.Other);
						}
						dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0].Copy());
						if (dataSetColeccion.Tables[@"DatosGestor"].Rows.Count == 0)
						{
							try
							{
								Errores.ManejarError(new CustomException(string.Format("El SP {0} no arrojó resultados para idExpedienteWeb={1}", sp, idexpweb.ToString())), string.Format("Reporte: {0}", reporte));
							}
							catch
							{
							}
						}
						dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosReclamo";
				
						#endregion
				
					}
					this.CambiarEstado(dataSetColeccion);
					return this.ConversionDatasetToIEnumerable(dataSetColeccion);
				}
				else
				{
					throw new CustomException(string.Format("El método ObtenerConexionString no arrojó resultados para idSuscriptor={0}", seguro), ErrorType.Other);
				}
			}
			catch (CustomException ex)
			{
				Errores.ManejarError(ex, string.Format("Reporte: {0}", reporte));
				if (ex.CustomErrorType == ErrorType.Database)
				{
					throw new Exception("No se encontraron datos asociados al caso indicado");
				}
				else
				{
					throw new Exception("No se encontraron datos de configuración necesarios para cargar el reporte");
				}
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
				{
					HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", ex.InnerException.Message));
				}
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				throw ex;
			}
		}
				
		public DataSet CambiarEstado(DataSet dataSetColeccion)
		{
			for (int i = 0; i < dataSetColeccion.Tables.Count; i++)
			{
				if (dataSetColeccion.Tables[i].Columns.Contains("a02Estado"))
				{
					dataSetColeccion.Tables[i].Columns["a02Estado"].MaxLength = 10;
					for (int j = 0; j < dataSetColeccion.Tables[i].Rows.Count; j++)
					{
						switch(dataSetColeccion.Tables[i].Rows[j]["a02Estado"].ToString())
						{
							case "A":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Activada";
								break;
							case "CA":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Anulada";
								break;
							case "V":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Vencida";
								break;
							case "S":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Suspendida";
								break;
						}
					}
				}
			}
			return dataSetColeccion;
		}
	}
}