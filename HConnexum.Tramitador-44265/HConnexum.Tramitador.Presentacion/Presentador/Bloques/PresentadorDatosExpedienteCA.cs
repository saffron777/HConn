using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class PresentadorDatosExpedienteCA : BloquesPresentadorBase
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>

		private const string TraceWarnCategoryErrorNombre = @"Error";
		/// <summary>Mensaje de error genérico.</summary>
		private readonly string errorMensajeGenerico = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
		/// <summary>Nombre de la aplicación bajo la cual registrar un evento en el Registro de Eventos de Windows 
		/// (event log).</summary>
		//private const string EVentoRegistroAplicacionNombre = @"HC-Tramitador";
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string Namespace = @"HConnexum.Tramitador.Presentacion.Presentador.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string mensaje = null;
		/// <summary>Cadena de conexión a la base de datos de Gestor.</summary>
		private string conexionCadena = null;
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		///<summary>vista asociada al presentador.</summary>
		readonly IDatosExpedientesCA vista;
		#endregion

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PresentadorDatosExpedienteCA(IDatosExpedientesCA vista)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				mensaje = string.Format("{0}.DatosExpedientesCEPresentador()", Namespace);
				this.vista = vista;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn(@"Error", @"Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
				this.vista.Errores = errorMensajeGenerico;
			}
		}

		/// <summary>
		/// Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.
		/// </summary>
		public void MostrarVista()
		{
			try
			{
				conexionCadena = ObtenerConexionString(int.Parse(vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"]));
				mensaje = string.Format("{0}.Mostrarvista()", Namespace);
				int idexp = 0;
				if(!string.IsNullOrEmpty((vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"].ToString())))
					idexp = int.Parse(vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"].ToString());
				ObtenerDatosMovimientos(idexp);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn(@"Error", @"Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
				this.vista.Errores = errorMensajeGenerico;
			}
		}

		public void ObtenerPolizas(string orden)
		{
			var ds = new DataSet();
			var connGestor = new ConexionADO();
			string sp = @"PA_Busca_Saldo_Reclamo_HC2";
			int idExpWeb = 0;
			if(!string.IsNullOrEmpty((vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"].ToString())))
				idExpWeb = int.Parse(vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"].ToString());
			try
			{
				if(string.IsNullOrEmpty(conexionCadena))
					conexionCadena = ObtenerConexionString(int.Parse(vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"]));
				mensaje = string.Format("{0}.ObtenerPolizas(idExpWeb = {1})", Namespace, idExpWeb);
				SqlParameter[] parametros = new SqlParameter[1];
				parametros[0] = new SqlParameter("@idExpedienteWeb", idExpWeb);
				ds = connGestor.EjecutaStoredProcedure(sp, conexionCadena, parametros.ToArray());
				if(ds != null && ds.Tables[0].Rows.Count > 0)
					this.vista.Datos = ConvertToEntity(ds.Tables[0], orden);
				else
					this.vista.Datos = new List<PolizasDTO>();
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				HttpContext.Current.Trace.Warn(TraceWarnCategoryErrorNombre, mensaje, ex);
				Errores.ManejarError(ex, mensaje);
				this.vista.Errores = errorMensajeGenerico;
			}
		}

		public IQueryable<PolizasDTO> ConvertToEntity(DataTable coleccion, string orden)
		{
			return (from Tabcoleccion in coleccion.AsEnumerable()
					orderby "it." + orden
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

		public void ObtenerDatosMovimientos(int idExpWeb)
		{
			var connGestor = new ConexionADO();
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				mensaje = string.Format("{0}.ObtenerDatosMovimientos(idExpWeb = {1})", Namespace, idExpWeb);
				SqlParameter[] parametros = new SqlParameter[1];
				parametros[0] = new SqlParameter("@IdExpWeb", idExpWeb);
				DataSet detalleMovimiento = connGestor.EjecutaStoredProcedure(ConfigurationManager.AppSettings[@"SpDetalleMovimientoCaHC2"], conexionCadena, parametros);
				if(detalleMovimiento != null && detalleMovimiento.Tables[0].Rows.Count > 0)
				{
					DataRow tipoMov = (from dm in detalleMovimiento.Tables[0].AsEnumerable()
									   orderby dm.Field<DateTime>(@"Hora_Registro") descending
									   select dm).FirstOrDefault();
					if(tipoMov != null)
					{
						Solicitud solicitud = ObtenerMovimiento(vista.IdMovimiento).Caso1.Solicitud;
						StringBuilder sb = new StringBuilder();
						if(vista.ParametrosEntrada[@"TIPOMOV"].ToUpper() == @"VERIFICACION")
							sb.Append(solicitud.IdCasoExterno2);
						else
						{
							sb.Append(string.IsNullOrEmpty(solicitud.IdCasoExterno3) ? string.Empty : solicitud.IdCasoExterno3);
							sb.Append(string.IsNullOrEmpty(solicitud.IdCasoExterno) ? string.Empty : @" - " + solicitud.IdCasoExterno);
						}
						vista.Clave = sb.ToString();
						vista.Responsable = tipoMov[@"Responsable"].ToString();
						vista.Observaciones = tipoMov[@"Observaciones"].ToString();
						vista.ObervacionesProcesadas = tipoMov[@"Observadef"].ToString();
                        vista.DocumentosFaxSolicitados = tipoMov[@"Documentos_Fax_Adicionales"].ToString() + " " + tipoMov[@"Cita_Post_Comentarios"].ToString();
						vista.FechaOcurrencia = tipoMov[@"Fec_Ocurrencia"].ToString();
						vista.FechaNotificacion = tipoMov[@"Fecha_Notificacion"].ToString();
						vista.FechaVencimiento = tipoMov[@"Fecha_Vencimiento"].ToString();
						vista.FechaSolicitud = tipoMov[@"Fecha_solicitud"].ToString();
						vista.Sintomas = tipoMov[@"Sintomas"].ToString();
						if(tipoMov[@"Medico_Tratante"] == null || string.IsNullOrEmpty(tipoMov[@"Medico_Tratante"].ToString()))
							vista.MedicoTratante = vista.ParametrosEntrada[@"NOMMEDICO"];
						else
							vista.MedicoTratante = tipoMov[@"Medico_Tratante"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				HttpContext.Current.Trace.Warn(TraceWarnCategoryErrorNombre, mensaje, ex);
				Errores.ManejarError(ex, mensaje);
				this.vista.Errores = errorMensajeGenerico;
			}
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
