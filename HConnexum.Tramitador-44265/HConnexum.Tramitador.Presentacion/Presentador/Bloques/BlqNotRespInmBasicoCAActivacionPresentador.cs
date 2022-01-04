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
	public class BlqNotRespInmBasicoCAActivacionPresentador : BloquesPresentadorBase
	{
		readonly IBlqNotRespInmBasicoCAActivacion vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public BlqNotRespInmBasicoCAActivacionPresentador(IBlqNotRespInmBasicoCAActivacion vista)
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
				string sConnectionString = ObtenerConexionString(int.Parse(vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"]));
				StringBuilder mensaje = new StringBuilder();
				string indMvtoAutomatico = vista.ParametrosEntrada[@"INDMVTOAUTOMATICO"].ToUpper() == @"ACTIVO" ? @"A" : @"M";
				string tipoMovimiento = vista.ParametrosEntrada[@"TIPOMOV"].ToUpper();
				string estatusMovimientoWeb = vista.ParametrosEntrada[@"ESTATUSMOVIMIENTOWEB"].ToUpper();
				string valorMensajeHC2 = string.Empty;
				string tipoMov = string.Empty;
				#endregion

				#region ..:: [ TIPO MOVIMIENTO ] ::..
				if(tipoMovimiento == @"EXTENSIÓN")
					tipoMovimiento = @"EXTENSION";
				else if(tipoMovimiento == @"ANULACIÓN")
					tipoMovimiento = @"ANULACION";
				switch(tipoMovimiento.ToUpper())
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
				#endregion

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
				if(string.IsNullOrEmpty(vista.Mensaje))
					vista.Mensaje = mensaje.ToString();
				EstatusMovimiento();
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

		public void EstatusMovimiento()
		{
			vista.MostrarImprimir = false;
			if(vista.ParametrosEntrada[@"ESTATUSMOVIMIENTOWEB"].ToUpper() == @"APROBADO")
				vista.MostrarImprimir = true;
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
