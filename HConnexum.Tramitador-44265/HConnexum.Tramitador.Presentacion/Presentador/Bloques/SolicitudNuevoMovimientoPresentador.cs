using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using System.Diagnostics;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	///<summary>Presentador para el manejo del control Web de usuario 'SolicitudNuevoMovimiento'.</summary>
	public class SolicitudNuevoMovimientoPresentador : BloquesPresentadorBase
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
		private const string _TRAZA_CATEGORIA_ERROR = "Error";
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string _NAMESPACE = @"HConnexum.Tramitador.Presentacion.Presentador.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string _mensaje = null;
		string _configValor = null;
		/// <summary>Constante de nulo de entero.</summary>
		private const int _NULL_INT = -1;
		/// <summary>Objeto para manejo de conexión a la BD de la aplicación 'Gestor'. </summary>
		private ConexionADO _connGestor = new ConexionADO();
		private string _SP = null;
		private List<SqlParameter> _parametros = new List<SqlParameter>();
		private SqlParameter _paramIntermediarioId = new SqlParameter("@intermediario", SqlDbType.Int);
		private SqlParameter _paramProveedorId = new SqlParameter("@proveedor", SqlDbType.Int);
		private SqlParameter _paramDiagnosticoId = new SqlParameter("@idDiagnostico", SqlDbType.Int);
		private DataSet _ds = new DataSet();
		private DataTable _dt = new DataTable();
		/// <summary>Identificador del movimiento 'ingreso ambulatorio' en la aplicación del cliente.</summary>
		private string _MOVIMIENTO_INGRESO_AMBULATORIO_ID = null;
		/// <summary>Identificador del movimiento 'ingreso hospitalario' en la aplicación del cliente.</summary>
		private string _MOVIMIENTO_INGRESO_HOSPITALARIO_ID = null;
		/// <summary>Identificador del movimiento 'anulación de ingreso' en la aplicación del cliente.</summary>
		private string _MOVIMIENTO_INGRESO_ANULACION_ID = null;
		/// <summary>Identificador del movimiento 'extensión' en la aplicación del cliente.</summary>
		private string _MOVIMIENTO_EXTENSION_ID = null;
		/// <summary>Nombre del tipo de caso 'ambulatorio' en la aplicación del cliente.</summary>
		private string _CASO_TIPO_AMBULATORIO_NOMBRE = null;
		/// <summary>Nombre del tipo de caso 'hospitalario' en la aplicación del cliente.</summary>
		private string _CASO_TIPO_HOSPITALARIO_NOMBRE = null;
		#endregion

		#region M É T O D O S   P R I V A D O S
		/// <summary>Registra la auditoría en la aplicación.</summary>
		/// <param name="pException">Excepción a registar.</param>
		/// <param name="pMensaje">Mensaje a registrar.</param>
		private void Auditoria(Exception pException, string pMensaje)
		{
			/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
			string TRAZA_CATEGORIA_ERROR = "Error";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.Auditoria(pException = {1}, pMensaje = {2})", _NAMESPACE, pException
					, pMensaje);

				Debug.WriteLine(_mensaje);
				Errores.ManejarError(pException, pMensaje);
				if(pException.InnerException != null)
					HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR
						, pMensaje + "Error en la capa origen: " + pException.InnerException.Message);
				HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR, pMensaje, pException);
				this.Vista.Errores = MostrarMensaje(TiposMensaje.Error_Generico);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				Errores.ManejarError(ex, _mensaje);
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR
						, _mensaje + "Error en la capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR, _mensaje, ex);
				this.Vista.Errores = MostrarMensaje(TiposMensaje.Error_Generico);
			}
		}
		/// <summary>Obtiene un dato particular determinado de un suscriptor determinado.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		/// <param name="pDatoParticular">Nombre del dato particular a consultar.</param>
		/// <returns>Valor del dato particular.</returns>
		private string ObtenerSuscriptorDatoParticular(int pSuscriptorId, string pDatoParticular)
		{
			string result = null;
			try
			{
				var datosParticulares = new ServicioParametrizadorClient();
				_ds = datosParticulares.ObtenerDatosParticularesPorSuscriptor(pSuscriptorId);
				if(_ds == null)
					return null;
				_dt = _ds.Tables[0];
				if(_dt == null || _dt.Rows.Count <= 0)
					return null;
				if(_ds.Tables["Error"] != null)
					throw new Exception(_ds.Tables["Error"].Rows[0]["UserMessage"].ToString());
				var lista = _dt.AsEnumerable().Where(r => r["Nombre"].ToString().ToUpper() == pDatoParticular.ToUpper());
				if(lista == null)
					return null;
				var tabla = lista.CopyToDataTable();
				if(tabla != null && tabla.Rows.Count > 0)
					result = tabla.Rows[0]["Valor"].ToString();
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return result;
		}
		/// <summary>Obtener el identificador del paso asociado a un movimiento.</summary>
		private int ObtenerPasoID()
		{
			MovimientoDTO dtoMovimiento = new MovimientoDTO();
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ObtenerPasoID()", _NAMESPACE);

				var repositorioMovimiento = new MovimientoRepositorio(udt);
				dtoMovimiento = repositorioMovimiento.ObtenerDTO(this.Vista.IdMovimiento);
				if(dtoMovimiento == null)
					return _NULL_INT;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return dtoMovimiento.IdPaso;
		}
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		///<summary>Vista asociada al presentador.</summary>
		readonly ISolicitudNuevoMovimiento Vista;
		/// <summary>Identificador del movimiento 'ingreso ambulatorio' en la aplicación del cliente.</summary>
		public string MOVIMIENTO_INGRESO_AMBULATORIO_ID { get; set; }
		/// <summary>Identificador del movimiento 'ingreso hospitalario' en la aplicación del cliente.</summary>
		public string MOVIMIENTO_INGRESO_HOSPITALARIO_ID { get; set; }
		/// <summary>Identificador del movimiento 'anulación de ingreso' en la aplicación del cliente.</summary>
		public string MOVIMIENTO_INGRESO_ANULACION_ID { get; set; }
		/// <summary>Identificador del movimiento 'extensión' en la aplicación del cliente.</summary>
		public string MOVIMIENTO_EXTENSION_ID { get; set; }
		/// <summary>Identificador del movimiento 'egreso' en la aplicación del cliente.</summary>
		public string MOVIMIENTO_EGRESO_ID { get; set; }
		/// <summary>Identificador del movimiento 'anulación de egreso' en la aplicación del cliente.</summary>
		public string MOVIMIENTO_EGRESO_ANULACION_ID { get; set; }
		/// <summary>Valor del estado 'aprobado' del movimiento en la aplicación del cliente.</summary>
		public string MOVIMIENTO_ESTADO_APROBADO { get; set; }
		/// <summary>Valor del estado 'rechazado' del movimiento en la aplicación del cliente.</summary>
		public string MOVIMIENTO_ESTADO_RECHAZADO { get; set; }
		/// <summary>Nombre del tipo de caso 'ambulatorio' en la aplicación del cliente.</summary>
		public string CASO_TIPO_AMBULATORIO_NOMBRE { get; set; }
		/// <summary>Nombre del tipo de caso 'hospitalario' en la aplicación del cliente.</summary>
		public string CASO_TIPO_HOSPITALARIO_NOMBRE { get; set; }
		#endregion

		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public SolicitudNuevoMovimientoPresentador(ISolicitudNuevoMovimiento vista)
		{
			try
			{
				this.Vista = vista;
				this.MOVIMIENTO_ESTADO_APROBADO = "APROBADO";
				this.MOVIMIENTO_ESTADO_RECHAZADO = "RECHAZO";
				if(string.IsNullOrWhiteSpace(this.MOVIMIENTO_INGRESO_AMBULATORIO_ID))
				{
					_configValor = ConfigurationManager.AppSettings["ListaValorIdIngresoAmbulatorio"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.MOVIMIENTO_INGRESO_AMBULATORIO_ID = _configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MOVIMIENTO_INGRESO_HOSPITALARIO_ID))
				{
					_configValor = ConfigurationManager.AppSettings["ListaValorIdIngresoHospitalario"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.MOVIMIENTO_INGRESO_HOSPITALARIO_ID = _configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MOVIMIENTO_INGRESO_ANULACION_ID))
				{
					_configValor = ConfigurationManager.AppSettings["ListaValorIdIngresoAnulacion"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.MOVIMIENTO_INGRESO_ANULACION_ID = _configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MOVIMIENTO_EXTENSION_ID))
				{
					_configValor = ConfigurationManager.AppSettings["ListaValorIdExtension"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.MOVIMIENTO_EXTENSION_ID = _configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MOVIMIENTO_EGRESO_ID))
				{
					_configValor = ConfigurationManager.AppSettings["ListaValorIdEgreso"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.MOVIMIENTO_EGRESO_ID = _configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MOVIMIENTO_EGRESO_ANULACION_ID))
				{
					_configValor = ConfigurationManager.AppSettings["ListaValorIdEgresoAnulacion"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.MOVIMIENTO_EGRESO_ANULACION_ID = _configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.CASO_TIPO_AMBULATORIO_NOMBRE))
				{
					_configValor = ConfigurationManager.AppSettings["CasoTipoAmbulatorioNombre"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.CASO_TIPO_AMBULATORIO_NOMBRE = _configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.CASO_TIPO_HOSPITALARIO_NOMBRE))
				{
					_configValor = ConfigurationManager.AppSettings["CasoTipoHospitalarioNombre"];
					if(!string.IsNullOrWhiteSpace(_configValor.Trim()))
						this.CASO_TIPO_HOSPITALARIO_NOMBRE = _configValor.Trim();
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		#region M É T O D O S   P Ú B L I C O S
		/// <summary>Obtiene una cadena de conexión hacia la base de datos de la aplicación del cliente según suscriptor determinado.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		/// <returns>Cadena de conexión hacia la base de datos.</returns>
		public string ObtenerBDConexionCadena(int pSuscriptorId)
		{
			var listaValor = new ListasValorDTO();
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ObtenerBDConexionCadena(pSuscriptorId = {1})", _NAMESPACE, pSuscriptorId);

				string configBDOrigen = ConfigurationManager.AppSettings["ListaOrigenDB"];
				if(string.IsNullOrWhiteSpace(configBDOrigen))
					return null;
				string listaItemNombre = ObtenerSuscriptorDatoParticular(pSuscriptorId, configBDOrigen);
				if(string.IsNullOrWhiteSpace(listaItemNombre))
					return null;
				var repositorio = new ListasValorRepositorio(this.unidadDeTrabajo);
				listaValor = repositorio.ObtenerListaValoresDTO(configBDOrigen, listaItemNombre);
				if(listaValor == null)
					return null;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return listaValor.Valor;
		}
		///<summary>Maneja la presentación de los datos en la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.MostrarVista()", _NAMESPACE);

				if(this.Vista.ParametrosEntrada != null)
				{
					if(string.IsNullOrWhiteSpace(this.Vista.SuscriptorIntermediarioConnectionString))
					{
						int id = _NULL_INT;
						int.TryParse(this.Vista.ParametrosEntrada["IDSUSINTERMEDIARIO"], out id);
						if(id > 0)
							this.Vista.SuscriptorIntermediarioConnectionString = ObtenerBDConexionCadena(id);
					}
					int intermediarioId = _NULL_INT;
					int.TryParse(this.Vista.ParametrosEntrada["IDINTERMEDIARIO"], out intermediarioId);
					int proveedorId = _NULL_INT;
					int.TryParse(this.Vista.ParametrosEntrada["IDPROVEEDOR"], out proveedorId);
					if(intermediarioId > 0 && proveedorId > 0)
						this.Vista.ListaDiagnosticoTipos = ObtenerDiagnosticoTipos(intermediarioId, proveedorId);
				}
				this.Vista.PasoId = this.Vista.IdMovimiento;//ObtenerPasoID();
				switch(this.Vista.BloqueTipo.ToUpper())
				{
					case "INGRESO":
						_configValor = ConfigurationManager.AppSettings["ListaTipoMovimientoIngreso"];
						break;
					case "EXTENSION":
					case "EGRESO":
						_configValor = ConfigurationManager.AppSettings["ListaTipoMovimientoEgreso"];
						break;
				}
				if(!string.IsNullOrWhiteSpace(_configValor))
					this.Vista.ListaMovimientoTipos = ObtenerListaValor(_configValor);
				_configValor = ConfigurationManager.AppSettings["ListaModoMovimiento"];
				if(!string.IsNullOrWhiteSpace(_configValor))
					this.Vista.ListaMovimientoModos = ObtenerListaValor(_configValor);
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}
		/// <summary>Obtiene los tipos de diagnóstico.</summary>
		/// <param name="pIntermediarioId">Identificador del intermediario a consultar.</param>
		/// <param name="pProveedorId">Identificador del proveedor a consultar.</param>
		/// <returns>Lista de tipos de diagnóstico.</returns>
		public DataTable ObtenerDiagnosticoTipos(int pIntermediarioId, int pProveedorId)
		{
			_SP = "pa_ListadoDiagnosticos_Filtrado_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ObtenerDiagnosticoTipos(pIntermediarioId = {1}, pProveedorId = {2})"
					, _NAMESPACE, pIntermediarioId, pProveedorId);

				_paramIntermediarioId.Value = pIntermediarioId;
				_paramProveedorId.Value = pProveedorId;
				_parametros.Clear();
				_parametros.Add(_paramIntermediarioId);
				_parametros.Add(_paramProveedorId);
				_ds = _connGestor.EjecutaStoredProcedure(_SP, this.Vista.SuscriptorIntermediarioConnectionString, _parametros.ToArray());
				if(_ds == null)
					return null;
				_dt = _ds.Tables[0];

				// Valor por defecto de la lista.
				DataTable dt = _dt.Clone();
				DataRow row = dt.NewRow();
				row[0] = "Otros";
				row[1] = DBNull.Value;
				dt.Rows.Add(row);
				_dt.Merge(dt);
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return _dt;
		}
		/// <summary>Obtiene los tipos de procedimiento.</summary>
		/// <param name="pDiagnosticoId">Identificador del diagnóstico a consultar.</param>
		/// <param name="pIntermediarioId">Identificador del intermediario a consultar.</param>
		/// <param name="pProveedorId">Identificador del proveedor a consultar.</param>
		/// <returns>Lista de tipos de procedimiento.</returns>
		public DataTable ObtenerProcedimientoTipos(int pDiagnosticoId, int pIntermediarioId, int pProveedorId)
		{
			_SP = "pa_ListadoTratamientos_Filtrado_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format(
					"{0}.ObtenerProcedimientoTipos(pDiagnosticoId = {1}, pIntermediarioId = {2}, pProveedorId = {3})"
					, _NAMESPACE, pDiagnosticoId, pIntermediarioId, pProveedorId);

				_paramDiagnosticoId.Value = pDiagnosticoId;
				_paramIntermediarioId.Value = pIntermediarioId;
				_paramProveedorId.Value = pProveedorId;
				_parametros.Clear();
				_parametros.Add(_paramDiagnosticoId);
				_parametros.Add(_paramIntermediarioId);
				_parametros.Add(_paramProveedorId);
				_ds = _connGestor.EjecutaStoredProcedure(_SP, this.Vista.SuscriptorIntermediarioConnectionString, _parametros.ToArray());
				if(_ds == null)
					return null;
				_dt = _ds.Tables[0];

				// Valor por defecto de la lista.
				DataTable dt = _dt.Clone();
				DataRow row = dt.NewRow();
				row[0] = "Otros";
				row[1] = DBNull.Value;
				dt.Rows.Add(row);
				_dt.Merge(dt);
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return _dt;
		}

		///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
		/// <returns>Devuelve mensaje(s) con los datos validados.</returns>
		public void ValidarDatos()
		{
			string errores = null;
			try
			{
				Vista.MovimientoEstado = @"Pendiente";
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			this.Vista.Errores = errores;
		}

		/// <summary>Rutina para manejo de mensajes.</summary>
		/// <param name="pTipo">Tipo de mensaje a mostrar.</param>
		/// <returns>Contenido del mensaje.</returns>
		public string MostrarMensaje(TiposMensaje pTipo)
		{
			string result = null;
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.MostrarMensaje(pTipo = {1})", _NAMESPACE, pTipo);

				switch(pTipo)
				{
					case TiposMensaje.PorDefecto:
						result = "[Mensaje por configurar]";
						break;
					case TiposMensaje.Error_Generico:
						result = WebConfigurationManager.AppSettings["MensajeExcepcion"];
						break;
					case TiposMensaje.Validacion_CampoRequerido:
						result = "Campo obligatorio.";
						break;
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return result;
		}
		#endregion
	}
}