using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Data;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class BlqNotRespInmBasicoPresentador : BloquesPresentadorBase
	{
		readonly IBlqNotRespInmBasico vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public BlqNotRespInmBasicoPresentador(IBlqNotRespInmBasico vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de buscar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				#region ..:: [ VARIABLES ] ::..
				StringBuilder mensaje = new StringBuilder();
				string tipoMovCompleto = vista.ParametrosEntrada[@"TIPOMOV"].ToUpper();
				string estatusMovimientoWeb = vista.ParametrosEntrada[@"ESTATUSMOVIMIENTOWEB"].ToUpper();
				string indMvtoAutomatico = vista.ParametrosEntrada[@"INDMVTOAUTOMATICO"].ToUpper() == @"ACTIVO" ? @"A" : @"M";
				string indAutomatico = vista.ParametrosEntrada[@"INDMVTOAUTOMATICO"].ToUpper();
				string tipoProc = vista.ParametrosEntrada[@"TIPOCASO"].ToUpper();
				string modoMov = vista.ParametrosEntrada[@"NOMMODOMOV"].ToUpper();
				double montoPresupuesto = string.IsNullOrWhiteSpace(vista.ParametrosEntrada[@"MONTOPRESUP"].ToString()) ? 0 : double.Parse(vista.ParametrosEntrada[@"MONTOPRESUP"].ToString());
				string sConnectionString = ObtenerConexionString(int.Parse(vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"]));
				string tipoMov = string.Empty;
				string estatus = string.Empty;
				string estatusMov = string.Empty;
				string valorMensajeHC2 = string.Empty;
				bool marcaAutomatica = false;
				List<string> documentosBase = new List<string>();
				IList<string> documentos = new List<string>();
				ConexionADO servicio = new ConexionADO();
				#endregion

				#region ..:: [ TIPO MOVIMIENTO ] ::..
				if(tipoMovCompleto == @"EXTENSIÓN")
					tipoMovCompleto = @"EXTENSION";
				else if(tipoMovCompleto == @"ANULACIÓN")
					tipoMovCompleto = @"ANULACION";
				switch(tipoMovCompleto.ToUpper())
				{
					case @"VERIFICACION":
					tipoMov = @"VER";
					break;
					case @"INGRESO":
					tipoMov = @"ING";
					break;
					case @"EXTENSION":
					tipoMov = @"EXT";
					break;
					case @"EGRESO":
					tipoMov = @"EGR";
					break;
					case @"ANULACION":
					tipoMov = @"ANU";
					break;
					default:
					break;
				}
				if(tipoMov != @"ANU")
				{
					switch(estatusMovimientoWeb)
					{
						case @"APROBADO":
						estatusMov = @"OK";
						break;
						case @"PENDIENTE":
						estatusMov = @"PN";
						break;
						case @"EN PROCESO":
						estatusMov = @"PU";
						break;
						case @"DOCUMENTOS":
						estatusMov = @"PD";
						break;
						default:
						break;
					}
					if(indMvtoAutomatico == @"A")
					{
						if(!string.IsNullOrEmpty(vista.ParametrosEntrada[@"IDDIAGNOSTICO"]) && !string.IsNullOrEmpty(vista.ParametrosEntrada[@"IDPROCEDIMIENTO"]) && (vista.ParametrosEntrada[@"IDDIAGNOSTICO"] != @"0" && vista.ParametrosEntrada[@"IDPROCEDIMIENTO"] != @"0"))
						{
							if(tipoMov == @"ING" || (tipoMov == @"EGR" && tipoProc == @"AMBULATORIO"))
							{
								#region Ejecucion de SP pa_monto_consumido_HC2
								double montoConsumido = 0;
								SqlParameter[] parameterMontoConsumido = new SqlParameter[1];
								parameterMontoConsumido[0] = new SqlParameter("@asegurado", int.Parse(vista.ParametrosEntrada[@"IDBENEF"]));
								using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_monto_consumido_HC2", sConnectionString, parameterMontoConsumido))
									if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
										montoConsumido = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][@"Monto_consumido"].ToString()) ? 0 : double.Parse(ds.Tables[0].Rows[0][@"Monto_consumido"].ToString());
								#endregion

								#region Ejecucion de SP pa_monto_cobertura_HC2
								double montoCobertura = 0;
								SqlParameter[] parameterMontoCobertura = new SqlParameter[4];
								parameterMontoCobertura[0] = new SqlParameter("@asegurado", vista.ParametrosEntrada[@"IDBENEF"]);
								parameterMontoCobertura[1] = new SqlParameter("@intermediario", vista.ParametrosEntrada[@"IDINTERMEDIARIO"]);
								parameterMontoCobertura[2] = new SqlParameter("@ramo_aseg", vista.ParametrosEntrada[@"RAMOBENEF"]);
								parameterMontoCobertura[3] = new SqlParameter("@servicio", vista.ParametrosEntrada[@"NOMTIPOSERVICIO"]);
								using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_monto_cobertura_HC2", sConnectionString, parameterMontoCobertura))
									if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
										montoCobertura = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][@"a04Virasegurado"].ToString()) ? 0 : double.Parse(ds.Tables[0].Rows[0][@"a04Virasegurado"].ToString());
								#endregion

								#region Ejecucion de SP pa_monto_patologia_HC2
								double montoPatologia = 0;
								SqlParameter[] parameterPatologia = new SqlParameter[5];
								parameterPatologia[0] = new SqlParameter("@modo", modoMov);
								parameterPatologia[1] = new SqlParameter("@procedimiento", vista.ParametrosEntrada[@"IDPROCEDIMIENTO"]);
								parameterPatologia[2] = new SqlParameter("@diagnostico", vista.ParametrosEntrada[@"IDDIAGNOSTICO"]);
								parameterPatologia[3] = new SqlParameter("@clinica", vista.ParametrosEntrada[@"IDPROVEEDOR"]);
								parameterPatologia[4] = new SqlParameter("@proc", tipoProc);
								using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_monto_patologia_HC2", sConnectionString, parameterPatologia))
									if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
										montoPatologia = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][@"Monto_procedimiento"].ToString()) ? 0 : double.Parse(ds.Tables[0].Rows[0][@"Monto_procedimiento"].ToString());
								#endregion

								if(montoPresupuesto <= (montoCobertura - montoConsumido) && montoPresupuesto <= montoPatologia)
								{
									estatusMov = @"OK";
									estatus = @"PENDIENTE";
									marcaAutomatica = true;
								}
								else
								{
									estatusMov = @"PN";
									estatus = @"PENDIENTE";
									indMvtoAutomatico = @"M";
								}
							}
							else if(tipoMov == @"EGR")
							{
								#region Ejecucion de SP pa_monto_cubierto_egreso_HC2
								double montoCubiertoEgreso = 0;
								SqlParameter[] parameterEgreso = new SqlParameter[1];
								parameterEgreso[0] = new SqlParameter("@valor", vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"]);
								using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_monto_cubierto_egreso_HC2", sConnectionString, parameterEgreso))
									if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
										montoCubiertoEgreso = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][@"MontoCubierto"].ToString()) ? 0 : double.Parse(ds.Tables[0].Rows[0][@"MontoCubierto"].ToString());
								#endregion

								if(montoPresupuesto <= montoCubiertoEgreso + 1)
								{
									estatusMov = @"OK";
									estatus = @"PENDIENTE";
									marcaAutomatica = true;
								}
								else
								{
									estatusMov = @"PN";
									estatus = @"PENDIENTE";
									indMvtoAutomatico = @"M";
								}
							}
							else if(tipoMov == @"EGR")
							{
								#region Ejecucion de SP pa_monto_cubierto_egreso_HC2
								double montoCubiertoEgreso = 0;
								SqlParameter[] parameterEgreso = new SqlParameter[1];
								parameterEgreso[0] = new SqlParameter("@valor", vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"]);
								using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_monto_cubierto_egreso_HC2", sConnectionString, parameterEgreso))
									if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
										montoCubiertoEgreso = string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][@"MontoCubierto"].ToString()) ? 0 : double.Parse(ds.Tables[0].Rows[0][@"MontoCubierto"].ToString());
								#endregion

								if(montoPresupuesto <= montoCubiertoEgreso + 1)
								{
									estatusMov = @"OK";
									estatus = @"APROBADO";
									marcaAutomatica = true;
								}
								else
									indMvtoAutomatico = @"M";
							}
							else if(tipoMov == @"EXT")
								indMvtoAutomatico = @"M";
						}
						else
							indMvtoAutomatico = @"M";
					}
					else
						indMvtoAutomatico = @"M";
					if(tipoMov == @"ING")
					{
						#region Ejecucion de SP pa_documentos_base_ingreso_HC2
						SqlParameter[] parameterDocBaseIngreso = new SqlParameter[0];
						using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_documentos_base_ingreso_HC2", sConnectionString, parameterDocBaseIngreso))
							if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
								documentosBase = ds.Tables[0].AsEnumerable().Select(r => r[@"Documento"].ToString()).ToList();
						#endregion

						#region Ejecucion de SP pa_documentos_ingreso_HC2
						SqlParameter[] parameterDocIngreso = new SqlParameter[1];
						parameterDocIngreso[0] = new SqlParameter("@diagnostico", vista.ParametrosEntrada[@"IDDIAGNOSTICO"]);
						using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_documentos_ingreso_HC2", sConnectionString, parameterDocIngreso))
							if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
								documentos = ds.Tables[0].AsEnumerable().Select(r => r[@"Documento"].ToString()).ToList();
						#endregion
					}
					else if(tipoMov == @"EGR")
					{
						#region Ejecucion de SP pa_documentos_base_egreso_HC2
						SqlParameter[] parameterDocBaseIngreso = new SqlParameter[0];
						using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_documentos_base_egreso_HC2", sConnectionString, parameterDocBaseIngreso))
							if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
								documentosBase = ds.Tables[0].AsEnumerable().Select(r => r[@"Documento"].ToString()).ToList();
						#endregion

						#region Ejecucion de SP pa_documentos_egreso_HC2
						SqlParameter[] parameterDocIngreso = new SqlParameter[1];
						parameterDocIngreso[0] = new SqlParameter("@diagnostico", vista.ParametrosEntrada[@"IDDIAGNOSTICO"]);
						using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_documentos_egreso_HC2", sConnectionString, parameterDocIngreso))
							if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
								documentos = ds.Tables[0].AsEnumerable().Select(r => r[@"Documento"].ToString()).ToList();
						#endregion
					}
				}
				#endregion

				#region Ejecucion de SP pa_mensaje_HC2
				if(string.IsNullOrEmpty(mensaje.ToString()))
				{
					if(tipoMovCompleto == @"ANULACION")
						valorMensajeHC2 = @"SOLANULA";
					else if(indAutomatico == "NOACTIVO")
					{
						if(estatusMovimientoWeb == "APROBADO" && tipoMovCompleto == "VERIFICACION")
							valorMensajeHC2 = @"VERIFAPROB";
						else if(estatusMovimientoWeb == "RECHAZADO" && tipoMovCompleto == "VERIFICACION")
							valorMensajeHC2 = @"VERIFRECHAZ";
						else if(estatusMovimientoWeb == "DOCUMENTOS" && tipoMovCompleto == "VERIFICACION")
							valorMensajeHC2 = @"VERIFDOCPEND";
						else if(estatusMovimientoWeb == "PENDIENTE" && tipoMovCompleto == "VERIFICACION")
							valorMensajeHC2 = @"VERIFMANUAL";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "AMBULATORIO" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"INGAMBAPROB";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "AMBULATORIO" && estatusMovimientoWeb == "RECHAZADO")
							valorMensajeHC2 = @"INGAMBRECHAZ";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "AMBULATORIO" && estatusMovimientoWeb == "DOCUMENTOS")
							valorMensajeHC2 = @"DOCPEND";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "AMBULATORIO" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"SOLINGAMB";
						else if(tipoMovCompleto == "INGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"INGHOSPAPROB";
						else if(tipoMovCompleto == "INGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "RECHAZADO")
							valorMensajeHC2 = @"INGHOSPRECHAZ";
						else if(tipoMovCompleto == "INGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "DOCUMENTOS")
							valorMensajeHC2 = @"DOCPEND";
						else if(tipoMovCompleto == "INGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"SOLINGHOSP";
						else if(tipoMovCompleto == "EXTENSION" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"EXTAPROB";
						else if(tipoMovCompleto == "EXTENSION" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "RECHAZADO")
							valorMensajeHC2 = @"EXTRECHAZ";
						else if(tipoMovCompleto == "EXTENSION" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "DOCUMENTOS")
							valorMensajeHC2 = @"DOCPEND";
						else if(tipoMovCompleto == "EXTENSION" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"SOLEXT";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"EGREAPROB";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "RECHAZADO")
							valorMensajeHC2 = @"EGRERECHAZ";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "DOCUMENTOS")
							valorMensajeHC2 = @"DOCPEND";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"SOLEGRE";
					}
					else
					{
						if(tipoMovCompleto == "VERIFICACION" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"VERIFAUT";
						else if(tipoMovCompleto == "VERIFICACION" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"VERFAPROBAUT";
						else if(tipoMovCompleto == "INGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"SOLINGHOSPAPROBAUT";
						else if(tipoMovCompleto == "INGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"SOLINGHOSPAPROBAUT";
						else if(tipoMovCompleto == "INGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "RECHAZADO")
							valorMensajeHC2 = @"INGHOSPRECHAZ";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "AMBULATORIO" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"SOLINGAMBAPROBAUT";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "PENDIENTE")
							valorMensajeHC2 = @"SOLEGREAPROBAUT";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "AMBULATORIO" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"INGAMBAPROBAUT";
						else if(tipoMovCompleto == "EGRESO" && tipoProc == "HOSPITALIZACIÓN" && estatusMovimientoWeb == "APROBADO")
							valorMensajeHC2 = @"EGREAPROBAUT";
					}
					SqlParameter[] parameterMensaje = new SqlParameter[1];
					parameterMensaje[0] = new SqlParameter("@Valor_HC2", valorMensajeHC2);
					using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_mensaje_2_HC2", sConnectionString, parameterMensaje))
						if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
						{
							mensaje.AppendInNewLine(string.Empty + ds.Tables[0].Rows[0][@"txt_significado"].ToString().ToUpper());
							mensaje.AppendInNewLine(string.Empty + ds.Tables[0].Rows[0][@"txt_ayuda"].ToString());
							mensaje.Append(Environment.NewLine);
						}
				}
				#endregion

				if(marcaAutomatica)
					mensaje.AppendInNewLine(@"Monto Compromiso Aprobado: " + montoPresupuesto + @" Bs.");
				if(documentosBase.Union(documentos).Count() > 0)
				{
					mensaje.AppendInNewLine(@"Recuerde que debe consignar los siguientes Documentos:");
					foreach(string documento in documentosBase.Union(documentos))
						mensaje.AppendInNewLine(documento);
					mensaje.Append(Environment.NewLine);
				}
				if(string.IsNullOrWhiteSpace(vista.ParametrosEntrada[@"ESTACION"]))
				{
					mensaje.Clear();
					mensaje.Append(@"Favor comunicarse al 5050539 e indicarle al operador que le cree una estación de trabajo para poder culminar su caso.");
				}
				vista.Mensaje = mensaje.ToString();
				vista.MostrarImprimir = estatusMov == @"OK";
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
		public void ValidarDatos()
		{
			StringBuilder errores = new StringBuilder();
			try
			{
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			vista.Errores = errores.ToString();
		}

		public string ObtenerConexionString(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using(DataSet ds = servicio.ObtenerConexionStringListaValor(idSuscriptor, ConfigurationManager.AppSettings[@"ListaOrigenDB"]))
				{
					if((ds != null) && (ds.Tables[0].Rows.Count > 0))
						return ds.Tables[0].Rows[0][@"ConexionString"].ToString();
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return string.Empty;
		}
	}
}