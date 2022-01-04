using System;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Data;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class BlqNotRespInmCAActivacionPresentador : BloquesPresentadorBase
	{
		readonly IBlqNotRespInmCAActivacion vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public BlqNotRespInmCAActivacionPresentador(IBlqNotRespInmCAActivacion vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de buscar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				#region ..:: [ VARIABLES ] ::..
				ConexionADO servicio = new ConexionADO();
				int codigoIntermediario = 0;
				string sConnectionString = ObtenerConexionString(int.Parse(vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"]));
				int idExpedienteWeb = int.Parse(vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"]);
				var movimientoRepositorio = new MovimientoRepositorio(unidadDeTrabajo);
				var movimiento = movimientoRepositorio.ObtenerPorId(vista.IdMovimiento);
				int supportIncidentId = 0;
				if(!string.IsNullOrEmpty(movimiento.Caso1.Solicitud.IdCasoExterno3))
					supportIncidentId = int.Parse(movimiento.Caso1.Solicitud.IdCasoExterno3);
				int reclamoHcmId = 0;
				if(!string.IsNullOrEmpty(movimiento.Caso1.Solicitud.IdCasoExterno))
					reclamoHcmId = int.Parse(movimiento.Caso1.Solicitud.IdCasoExterno);
				string sEstado_Solicitudes = "PC";
				string strMensaje = string.Empty;
				StringBuilder mensaje = new StringBuilder();
				string indMvtoAutomatico = vista.ParametrosEntrada[@"INDMVTOAUTOMATICO"].ToUpper() == @"ACTIVO" ? @"A" : @"M";
				string tipoMov = string.Empty;
				string tipoMovimiento = vista.ParametrosEntrada[@"TIPOMOV"].ToUpper();
				string estatusMovimientoWeb = vista.ParametrosEntrada[@"ESTATUSMOVIMIENTOWEB"].ToUpper();
				string valorMensajeHC2 = string.Empty;
				#endregion

				#region ..:: [ TIPO MOVIMIENTO ] ::..
				if(tipoMovimiento == @"EXTENSIÓN")
					tipoMovimiento = @"EXTENSION";
				else if(tipoMovimiento == @"ANULACIÓN")
					tipoMovimiento = @"ANULACION";
				else if(tipoMovimiento == @"ACTIVACIÓN")
					tipoMovimiento = @"ACTIVACION";
				switch(tipoMovimiento.ToUpper())
				{
					case @"ACTIVACION":
					tipoMov = @"ACT";
					break;
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
				#endregion

				#region ..:: [ CODIGO DEL INTERMEDIARIO ] ::..
				SqlParameter[] parameters = new SqlParameter[1];
				parameters[0] = new SqlParameter("@seguro", int.Parse(vista.ParametrosEntrada[@"IDINTERMEDIARIO"]));
				using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_codigo_intermediario_HC2", sConnectionString, parameters))
					if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
						codigoIntermediario = int.Parse(ds.Tables[0].Rows[0]["codigo"].ToString());
				#endregion

				switch(tipoMovimiento.ToUpper())
				{
					case @"ANULACION":
					vista.Mensaje = "La solicitud de anulación se ha realizado satisfactoriamente";
					break;
					case @"ACTIVACION":
					#region ..:: [ ACTIVAR CARTA AVAL ] ::..
					SqlParameter[] parametersBotonActivar = new SqlParameter[1];
					parametersBotonActivar[0] = new SqlParameter("@valor", reclamoHcmId);
					using(DataSet dsBA = servicio.EjecutaStoredProcedure(@"pa_boton_activar_HC2", sConnectionString, parametersBotonActivar))
						if(dsBA != null && dsBA.Tables.Count > 0 && dsBA.Tables[0].Rows.Count > 0)
						{
							SqlParameter[] parametersCartaAval = new SqlParameter[16];
							parametersCartaAval[0] = new SqlParameter("@usuario", DBNull.Value);
							parametersCartaAval[1] = new SqlParameter("@soporte", supportIncidentId);
							parametersCartaAval[2] = new SqlParameter("@fecha_ocurrencia", DateTime.Now); //DateTime.Parse(vista.ParametrosEntrada[@"FECOCURRENCIA"]));
							parametersCartaAval[3] = new SqlParameter("@fecha_solicitud", DateTime.Parse(vista.ParametrosEntrada[@"FECSOLICITUD"]));
							parametersCartaAval[4] = new SqlParameter("@intermediario", int.Parse(vista.ParametrosEntrada[@"IDINTERMEDIARIO"]));
							parametersCartaAval[5] = new SqlParameter("@asegurado", int.Parse(vista.ParametrosEntrada[@"IDBENEF"]));
							parametersCartaAval[6] = new SqlParameter("@proveedor", int.Parse(vista.ParametrosEntrada[@"IDPROVEEDOR"]));
							parametersCartaAval[7] = new SqlParameter("@estado", sEstado_Solicitudes);
							parametersCartaAval[8] = new SqlParameter("@reclamo", reclamoHcmId);
							parametersCartaAval[9] = new SqlParameter("@codinter", codigoIntermediario);
							parametersCartaAval[10] = new SqlParameter("@nombreOperadorWeb", vista.UsuarioActual.DatosBase.Nombre1 + @" " + vista.UsuarioActual.DatosBase.Apellido1);
							parametersCartaAval[11] = new SqlParameter("@idOperadorWeb", vista.UsuarioActual.Id);
							parametersCartaAval[12] = new SqlParameter("@indOrigen", 1);
							parametersCartaAval[13] = new SqlParameter("@idusuario", DBNull.Value);
							parametersCartaAval[14] = new SqlParameter("@estacion_id", int.Parse(vista.ParametrosEntrada[@"ESTACION"]));
							parametersCartaAval[15] = new SqlParameter("@idweb", idExpedienteWeb);
							using(servicio.EjecutaStoredProcedure(@"pa_activar_carta_aval_HC2", sConnectionString, parametersCartaAval))
								mensaje.AppendInNewLine("La Carta Aval ha sido activada satisfactoriamente.");
							vista.PActivacionCa = "ACTIVO";
							vista.PEstatusMovimientoWeb = "Pendiente";

						}
						else
						{
							vista.Mensaje = "La activación de la Carta Aval posee una condición especial que no permite su activación, se sugiere contactar a su compañía de seguro.";
							vista.PActivacionCa = "NOACTIVO";
							vista.PEstatusMovimientoWeb = "Pendiente";
						}
					#endregion
					break;
					case @"EXTENSION":
					case @"EGRESO":
					SqlParameter[] parametersMovimientos = new SqlParameter[1];
					parametersMovimientos[0] = new SqlParameter("@expediente", idExpedienteWeb);
					using(DataSet dsValMov = servicio.EjecutaStoredProcedure(@"pa_validaMovimientos_ca_HC2", sConnectionString, parametersMovimientos))
						if(dsValMov != null && dsValMov.Tables.Count > 0 && dsValMov.Tables[0].Rows.Count > 0)
						{
							#region ..:: [ EJECUCION DE SP pa_nuevo_movimiento_HC2 ] ::..
							SqlParameter[] parameterNuevoMov = new SqlParameter[19];
							parameterNuevoMov[0] = new SqlParameter("@reclamo", reclamoHcmId);
							parameterNuevoMov[1] = new SqlParameter("@idexpediente", vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"]);
							parameterNuevoMov[2] = new SqlParameter("@tipom", tipoMovimiento);
							parameterNuevoMov[3] = new SqlParameter("@fecha_solicitud", DateTime.Parse(vista.ParametrosEntrada[@"FECSOLICITUD"]));
							parameterNuevoMov[4] = new SqlParameter("@medico", vista.ParametrosEntrada[@"NOMMEDICO"]);
							parameterNuevoMov[5] = new SqlParameter("@clase", vista.ParametrosEntrada[@"TIPOCASO"]);
							parameterNuevoMov[6] = new SqlParameter("@dias", vista.ParametrosEntrada[@"NUMDIASHOSP"]);
							parameterNuevoMov[7] = new SqlParameter("@monto", vista.ParametrosEntrada[@"MONTOPRESUP"].Replace(@",", @"."));
							parameterNuevoMov[8] = new SqlParameter("@documentos", vista.ParametrosEntrada[@"DOCUMENTOS"].Replace(@";", @", ") + vista.ParametrosEntrada[@"OBSERVACIONESDOCUMENTOS"]);
							parameterNuevoMov[9] = new SqlParameter("@observa", vista.ParametrosEntrada[@"OBSDIAGNOSTICO"]);
							parameterNuevoMov[10] = new SqlParameter("@nombreOperadorWeb", vista.UsuarioActual.DatosBase.Nombre1 + @" " + vista.UsuarioActual.DatosBase.Apellido1);
							parameterNuevoMov[11] = new SqlParameter("@idOperadorWeb", vista.UsuarioActual.Id);
							parameterNuevoMov[12] = new SqlParameter("@indOrigen", 1);
							parameterNuevoMov[13] = new SqlParameter("@marcaAutomatica", indMvtoAutomatico);
							parameterNuevoMov[14] = new SqlParameter("@modo", vista.ParametrosEntrada[@"NOMMODOMOV"]);
							parameterNuevoMov[15] = new SqlParameter("@UltimoMov", vista.ParametrosEntrada[@"ULTIMOTIPOMOV"] == @"EXTENSIÓN" ? @"EXTENSION" : vista.ParametrosEntrada[@"ULTIMOTIPOMOV"]);
							if(vista.ParametrosEntrada[@"IDDIAGNOSTICO"] == @"0")
								parameterNuevoMov[16] = new SqlParameter("@diagnostico", DBNull.Value);
							else
								parameterNuevoMov[16] = new SqlParameter("@diagnostico", vista.ParametrosEntrada[@"IDDIAGNOSTICO"]);
							if(vista.ParametrosEntrada[@"IDPROCEDIMIENTO"] == @"0")
								parameterNuevoMov[17] = new SqlParameter("@tratamiento", DBNull.Value);
							else
								parameterNuevoMov[17] = new SqlParameter("@tratamiento", vista.ParametrosEntrada[@"IDPROCEDIMIENTO"]);
							parameterNuevoMov[18] = new SqlParameter("@estacion_id", vista.ParametrosEntrada[@"ESTACION"]);
							using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_nuevo_movimiento_HC2", sConnectionString, parameterNuevoMov))
								if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
								{
									if((ds.Tables[0].Rows[0][@"idrecibido"]) != @"1")
									{
										vista.PActivacionCa = "ACTIVO";
										vista.PEstatusMovimientoWeb = "Pendiente";
									}
									else
									{
										vista.PActivacionCa = "NOACTIVO";
										vista.PEstatusMovimientoWeb = "Pendiente";
									}
								}
							#endregion
						}
					break;
					default:
					break;
				}

                // GET SuporIncidentID
                HttpContext.Current.Trace.Warn(@"Inicio pa_movimiento_actual_HC2");
                SqlParameter[] parameterMovimientoActualCaa = new SqlParameter[1];
                parameterMovimientoActualCaa[0] = new SqlParameter("@expediente", idExpedienteWeb);
                using (DataSet ds = servicio.EjecutaStoredProcedure(@"pa_movimiento_actual_SI_HC2", sConnectionString, parameterMovimientoActualCaa))
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        vista.PIdSupportIncident = string.Empty + ds.Tables[0].Rows[0][@"suport_incident_id"].ToString();
                    }

				#region Ejecucion de SP pa_mensaje_2_HC2
				if(string.IsNullOrEmpty(mensaje.ToString()))
				{
					if(tipoMovimiento == "VERIFICACION" && estatusMovimientoWeb == "APROBADO")
						valorMensajeHC2 = @"CAAPROB";
					else if(tipoMovimiento == "VERIFICACION" && estatusMovimientoWeb == "RECHAZADO")
						valorMensajeHC2 = @"CARECHAZ";
					else if(tipoMovimiento == "VERIFICACION" && estatusMovimientoWeb == "DOCUMENTOS")
						valorMensajeHC2 = @"CADOCPEND";
					else if(tipoMovimiento == "EXTENSION" && estatusMovimientoWeb == "APROBADO")
						valorMensajeHC2 = @"EXTAPROB";
					else if(tipoMovimiento == "EXTENSION" && estatusMovimientoWeb == "RECHAZADO")
						valorMensajeHC2 = @"EXTRECHAZ";
					else if(tipoMovimiento == "EGRESO" && estatusMovimientoWeb == "APROBADO")
						valorMensajeHC2 = @"EGREAPROB";
					else if(tipoMovimiento == "EGRESO" && estatusMovimientoWeb == "RECHAZADO")
						valorMensajeHC2 = @"EGRERECHAZ";
					else if((tipoMovimiento == "EXTENSION" || tipoMovimiento == "EGRESO") && estatusMovimientoWeb == "DOCUMENTOS")
						valorMensajeHC2 = @"DOCPEND";
					else if(tipoMovimiento == "EGRESO" && indMvtoAutomatico == "ACTIVO" && estatusMovimientoWeb == "PENDIENTE")
						valorMensajeHC2 = @"SOLEGREAPROBAUT";
					else if(tipoMovimiento == "VERIFICACION" && estatusMovimientoWeb == "PENDIENTE")
						valorMensajeHC2 = @"SOLCA";
					else if(tipoMovimiento == "EXTENSION" && estatusMovimientoWeb == "PENDIENTE")
						valorMensajeHC2 = @"SOLEXT";
					else if(tipoMovimiento == "EGRESO" && indMvtoAutomatico == "ACTIVO" && estatusMovimientoWeb == "APROBADO")
						valorMensajeHC2 = @"EGREAPROBAUT";
					else if(tipoMovimiento == "EGRESO" && estatusMovimientoWeb == "PENDIENTE")
						valorMensajeHC2 = @"SOLEGRE";
					else if(tipoMovimiento == "ANULACION")
						valorMensajeHC2 = @"SOLANULA";
					else if(tipoMovimiento == "ACTIVACION" && vista.PActivacionCa.ToUpper() == "ACTIVO")
						valorMensajeHC2 = @"CAACTIVADA";
					else if(tipoMovimiento == "ACTIVACION" && vista.PActivacionCa.ToUpper() == "NOACTIVO")
						valorMensajeHC2 = @"CANOACTIVADA";

					SqlParameter[] parameterMensaje = new SqlParameter[1];
					parameterMensaje[0] = new SqlParameter("@Valor_HC2", valorMensajeHC2);
					using(DataSet ds = servicio.EjecutaStoredProcedure(@"pa_mensaje_2_HC2", sConnectionString, parameterMensaje))
						if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
						{
							mensaje.AppendInNewLine(string.Empty + ds.Tables[0].Rows[0][@"txt_significado"].ToString().ToUpper());
							mensaje.AppendInNewLine(string.Empty + ds.Tables[0].Rows[0][@"txt_ayuda"].ToString());
							mensaje.Append(Environment.NewLine);
						}
					if(string.IsNullOrEmpty(vista.Mensaje))
						vista.Mensaje = mensaje.ToString();
                    
				}
				else
				{
					vista.Mensaje = mensaje.ToString();
				}
				//vista.MostrarImprimir = vista.PActivacionCa == @"ACTIVO";
                EstatusMovimiento(tipoMovimiento);
				#endregion
			}
			catch(Exception ex)
			{
				vista.ParametrosEntrada[@"ACTIVACIONCA"] = "NOACTIVO";
				vista.Mensaje = "La activación de la Carta Aval no puede ser procesada en este momento, por favor intente más tarde.";
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void EstatusMovimiento( String _tipoMovimiento)
		{
            if (_tipoMovimiento == @"ACTIVACION" && vista.PActivacionCa == @"ACTIVO")
                vista.MostrarImprimir = true;
            else if (_tipoMovimiento == @"EXTENSION" || _tipoMovimiento == @"EGRESO")
            {
                if (vista.ParametrosEntrada[@"ESTATUSMOVIMIENTOWEB"].ToUpper() == @"APROBADO")
                    vista.MostrarImprimir = true;
                else
                    vista.MostrarImprimir = false;
            }
            else
                vista.MostrarImprimir = false;
			
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

		public string ObtenerUrlReporteCartaAval(int idSuscriptor, string origen)
		{
			try
			{
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				string url = servicio.ObtenerUrlReporteCartaAval(idSuscriptor, origen).Tables[0].Rows[0][@"Url"].ToString();
				if(!string.IsNullOrWhiteSpace(url))
					return url;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return string.Empty;
		}
	}
}
