using System;
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
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class ListadoMovimientosPresentador : BloquesPresentadorBase
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
		private const string _TRACE_WARN_CATEGORY_ERROR_NOMBRE = @"Error";
		/// <summary>Mensaje de error genérico.</summary>
		private string _errorMensajeGenerico = WebConfigurationManager.AppSettings["MensajeExcepcion"];
		/// <summary>Nombre de la aplicación bajo la cual registrar un evento en el Registro de Eventos de Windows 
		/// (event log).</summary>
		private const string _EVENTO_REGISTRO_APLICACION_NOMBRE = @"HC-Tramitador";
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string _NAMESPACE = @"HConnexum.Tramitador.Presentacion.Presentador.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string _mensaje = null;
		/// <summary>Cadena de conexión a la base de datos de Gestor.</summary>
		private string _conexionCadena = null;
		/// <summary>Objeto para manejo de conexión a la BD de la aplicación 'Gestor'. </summary>
		private ConexionADO _connGestor = new ConexionADO();
		/// <summary>Código del servicio 'Clave de Emergencia' en la aplicación 'Gestor'. </summary>
		private string _servicioClaveDeEmergenciaCodigo = "Clave";
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Vista asociada al presentador.</summary>
		readonly IListadoMovimientos vista;
		#endregion

		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public ListadoMovimientosPresentador(IListadoMovimientos vista)
		{
			try
			{
				this.vista = vista;
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

		public IQueryable<ListaMovimientosDTO> ConvertToEntity(DataTable coleccion, string orden)
		{
			return (from Tabcoleccion in coleccion.AsEnumerable()
							  orderby "it." + orden
							  select new ListaMovimientosDTO
							  {
								  Movimiento = Tabcoleccion.Field<string>(@"Tipo_Movimiento"),
								  Cobertura = Tabcoleccion.Field<string>(@"Cobertura"),
								  FechaSolicitud = Tabcoleccion.Field<DateTime?>(@"Fecha_Movimiento"),
								  HoraSolicitud = Tabcoleccion.Field<DateTime?>(@"Hora_Registro"),
								  Estatus = Tabcoleccion.Field<string>(@"STATUS"),
								  NombreOperador = Tabcoleccion.Field<string>(@"Nombre_operador_web"),
								  Resultado = Tabcoleccion.Field<string>(@"Resultado"),
							  }).AsQueryable();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(string orden)
		{
			try
			{
				string sConnectionString = ObtenerConexionString(int.Parse(vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"]));
				_conexionCadena = sConnectionString;
				int idExpedienteWeb = int.Parse(vista.ParametrosEntrada["IDEXPEDIENTEWEB"].ToString());
				if((idExpedienteWeb != null) && (!string.IsNullOrWhiteSpace(_conexionCadena)))
				{
					DataTable dtMovimientos = ListadeMovimientos(idExpedienteWeb);
					this.vista.Datos = ConvertToEntity(dtMovimientos, orden);
					this.vista.NumeroDeRegistros = dtMovimientos.Rows.Count;
				}
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				this.vista.Errores = _errorMensajeGenerico;
			}
		}

		/// <summary>Evalúa si un asegurado no existente tiene una solicitud de afiliación ya creada.</summary>
		public DataTable ListadeMovimientos(int idcasoExterno)
		{
			var dt = new DataTable();
			var connGestor = new ConexionADO();
			string sP = "pa_movimientos_ce_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ListadeMovimientos(idcasoExterno = {1})", _NAMESPACE, idcasoExterno);
				var parametros = new List<SqlParameter>();
				parametros.Add(new SqlParameter()
				{
					ParameterName = "@criterio",
					SqlDbType = SqlDbType.Int,
					Value = idcasoExterno
				});
				DataSet ds = connGestor.EjecutaStoredProcedure(sP, _conexionCadena, parametros.ToArray());
				if(ds != null && dt.Rows.Count >= 0)
					dt = ds.Tables[0];
				else
					return null;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return dt;
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