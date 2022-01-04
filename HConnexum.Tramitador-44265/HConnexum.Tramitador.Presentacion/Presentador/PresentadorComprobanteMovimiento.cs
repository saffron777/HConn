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
	public class PresentadorComprobanteMovimiento : PresentadorBase<Movimiento>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IComprobanteMovimiento vista;

		public PresentadorComprobanteMovimiento(IComprobanteMovimiento vista)
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

		public IEnumerable<ComprobanteMovimientoCADTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
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

			var iEnumerableColeccion = from tabDatosPaciente in dataSetColeccion.Tables[@"DatosPaciente"].AsEnumerable()
									   select new ComprobanteMovimientoCADTO
									   {
										   Logo = (from tabDatosLogo in dataSetColeccion.Tables[@"DatosLogo"].AsEnumerable()
												   select tabDatosLogo.Field<string>(@"logo")).SingleOrDefault(),
										   DatosTitular = (from tabDatosTitular in dataSetColeccion.Tables[@"DatosTitular"].AsEnumerable()
														   select new DatosTitularMovCADTO
														   {
															   TitularAsegurado = tabDatosTitular.Field<string>(@"a02asegurado"),
															   CedulaTitular = tabDatosTitular.Field<string>(@"Tit_Nac") + " - " + tabDatosTitular.Field<string>(@"a02NoDocasegurado"),
															   SexoTitular = tabDatosTitular.Field<string>(@"a02Sexo"),
															   FechaNacTitular = tabDatosTitular.Field<DateTime?>(@"Fecha_Nacimiento"),
															   EstadoTitular = tabDatosTitular.Field<string>(@"a02Estado")
														   }).FirstOrDefault(),
										   DatosDetalle = (from tabDatosMovimiento in dataSetColeccion.Tables[@"DatosMovimiento"].AsEnumerable()
														   select new DatosMovimientoDTO
														   {
															   UltiMovHecho = tabDatosMovimiento.Field<string>(@"Ultimo_Mov_Hecho"),
															   Responsable = tabDatosMovimiento.Field<string>(@"Responsable"),
															   MontoFacturado = montoFacturado,
															   GastosNoCubiertos = gastosNoCubiertos,
															   MontoDeducible = deducible,
															   MontoCubierto = montoCubierto,
															   DiasHosp = tabDatosMovimiento.Field<int?>(@"Dias_Hosp"),
															   ObservacionesProcesadas = tabDatosMovimiento.Field<string>(@"observadef"),
															   DocumentosFaxSolicitados = tabDatosMovimiento.Field<string>(@"Documentos_Fax_Adicionales"),
															   TipoMovimiento = tabDatosMovimiento.Field<string>(@"Tipo_Movimiento"),
															   Sintomas = tabDatosMovimiento.Field<string>(@"Sintomas"),
															   FechaOcurrencia = tabDatosMovimiento.Field<DateTime?>(@"Fecha_Ocurrencia"),
															   FechaMovimiento = tabDatosMovimiento.Field<DateTime?>(@"Fecha_Movimiento"),
															   HoraRegistro = tabDatosMovimiento.Field<DateTime?>(@"Hora_Registro"),
															   Resultado = tabDatosMovimiento.Field<string>(@"Resultado"),
															   Observaciones = tabDatosMovimiento.Field<string>(@"observacionesweb"),
														   }).FirstOrDefault(),
										   DatosCartaAval = (from tabDatosCartaAval in dataSetColeccion.Tables[@"DatosCartaAval"].AsEnumerable()
															 select new DatosCartaAvalDTO
															 {
																 FechaEmision = tabDatosCartaAval.Field<DateTime?>(@"Fecha_Emision"),
																 FechaSolicitud = tabDatosCartaAval.Field<DateTime?>(@"Fecha_solicitud"),
																 FechaVencimiento = tabDatosCartaAval.Field<DateTime?>(@"Fecha_Vencimiento"),
																 Medico = tabDatosCartaAval.Field<string>(@"Medico_Tratante"),
																 Diagnostico = tabDatosCartaAval.Field<string>(@"Sintomas"),
																 Procedimiento = tabDatosCartaAval.Field<string>(@"Procedimiento"),
																 Presupuesto = tabDatosCartaAval.Field<double?>(@"Monto_Presupuestado"),
																 ObservacionesCA = tabDatosCartaAval.Field<string>(@"Observaciones_Cita_PostOp")
															 }).FirstOrDefault(),
										   PacienteAsegurado = tabDatosPaciente.Field<string>(@"a02asegurado"),
										   CedulaAsegurado = tabDatosPaciente.Field<string>(@"a02NoDocasegurado"),
										   SexoPaciente = tabDatosPaciente.Field<string>(@"a02Sexo"),
										   FechaNacPaciente = tabDatosPaciente.Field<DateTime?>(@"Fecha_Nacimiento"),
										   EstadoPaciente = tabDatosPaciente.Field<string>(@"a02Estado"),
										   Parentesco = tabDatosPaciente.Field<string>(@"Parentesco_txt"),
										   ContratantePoliza = tabDatosPaciente.Field<string>(@"a00Contratante"),
										   CompaniaAseguradora = tabDatosPaciente.Field<string>("Rn_Descriptor"),
										   Poliza = tabDatosPaciente.Field<int?>(@"a00NroPoliza"),
										   Certificado = tabDatosPaciente.Field<int?>(@"a00Certificado"),
										   FechaDesde = tabDatosPaciente.Field<DateTime?>(@"a00FechaDesde"),
										   FechaHasta = tabDatosPaciente.Field<DateTime?>(@"a00FechaHasta"),
										   SupportIncident = !dataSetColeccion.Tables[@"DatosPaciente"].Equals(null) ? dataSetColeccion.Tables["DatosPaciente"].Rows[0][@"Support_Incident_Id"].ToString() : tabDatosPaciente.Field<string>(@"Support_Incident_Id"),
										   Estado = dataSetColeccion.Tables[@"DatosPaciente"].Columns.Contains(@"es") ? tabDatosPaciente.Field<string>(@"es") : string.Empty
									   };
			return iEnumerableColeccion;
		}

		public IEnumerable<ComprobanteMovimientoCADTO> GenerarConsultaComprobante(string nomtipomov, int idexpweb, int idsusintermediario)
		{
			string reporte = "DisenoComprobanteMovCA";
			try
			{
				string conexionString = this.ObtenerConexionString(idsusintermediario);
				if (!string.IsNullOrWhiteSpace(conexionString))
				{
					DataSet dataSetColeccion = new DataSet();
					
					#region DatosLogo
					
					UnidadDeTrabajo udt = new UnidadDeTrabajo();
					SuscriptorRepositorio suscriptorreporsitorio = new SuscriptorRepositorio(udt);
					dataSetColeccion.Tables.Add(@"DatosLogo");
					dataSetColeccion.Tables[@"DatosLogo"].Columns.Add(@"logo", typeof(string));
					string logo = suscriptorreporsitorio.ObtenerLogo(idsusintermediario, SuscriptorRepositorio.TipoId.IdSuscriptor);
					if (string.IsNullOrEmpty(logo))
					{
						try
						{
							Errores.ManejarError(new CustomException(string.Format("El método suscriptorreporsitorio.ObtenerLogo no arrojó resultados para id={0} y tipoId={1}", idsusintermediario, SuscriptorRepositorio.TipoId.IdSuscriptor.ToString())), string.Format("Reporte: {0}", reporte));
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
						
					colleccionParameter = new SqlParameter[2];
					colleccionParameter[0] = new SqlParameter("@idexpweb", idexpweb);
					colleccionParameter[1] = new SqlParameter("@nomtipomov", nomtipomov);
					sp = ConfigurationManager.AppSettings[@"SpDatosPaciente"];
					if (string.IsNullOrEmpty(sp))
					{
						throw new CustomException("La clave SpDatosPaciente del Wev.Config no arrojó resultados", ErrorType.Other);
					}
					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0].Copy());
					if (dataSetColeccion.Tables[@"DatosGestor"].Rows.Count == 0)
					{
						dataSetColeccion.Tables.Remove(@"DatosGestor");
						sp = ConfigurationManager.AppSettings[@"SpDatosPaciente2"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDatosPaciente2 del Wev.Config no arrojó resultados", ErrorType.Other);
						}
						dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(sp, conexionString, colleccionParameter).Tables[0].Copy());
						if (dataSetColeccion.Tables[@"DatosGestor"].Rows.Count == 0)
						{
							try
							{
								Errores.ManejarError(new CustomException(string.Format("El SP {0} no arrojó resultados para idexpweb={1} y nomtipomov={2}", sp, idexpweb.ToString(), nomtipomov)), string.Format("Reporte: {0}", reporte));
							}
							catch
							{
							}
						}
					}
					dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosPaciente";
					
					#endregion
						
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
						
					if (dataSetColeccion.Tables[@"DatosPaciente"].Rows.Count != 0)
					{
						
						#region DatosCartaAval
						
						colleccionParameter = new SqlParameter[1];
						colleccionParameter[0] = new SqlParameter("@valor", idexpweb);
						sp = ConfigurationManager.AppSettings[@"SpDatosCartaAval"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDatosCartaAval del Wev.Config no arrojó resultados", ErrorType.Other);
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
						dataSetColeccion.Tables[@"DatosGestor"].TableName = @"DatosCartaAval";
							
						#endregion
							
						#region DatosMovimiento
						
						colleccionParameter[0] = new SqlParameter("@valor", idexpweb);
						sp = ConfigurationManager.AppSettings[@"SpDatosDetalleMovimiento"];
						if (string.IsNullOrEmpty(sp))
						{
							throw new CustomException("La clave SpDatosDetalleMovimiento del Wev.Config no arrojó resultados", ErrorType.Other);
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
					
						#region DatosReclamo
						
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
					throw new CustomException(string.Format("El método ObtenerConexionString no arrojó resultados para idSuscriptor={0}", idsusintermediario), ErrorType.Other);
				}
			}
			catch (CustomException ex)
			{
				Errores.ManejarError(ex, ex.Message);
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