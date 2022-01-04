using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using System.Diagnostics;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class SolicitudNuevoMovimientoCAPresentador : BloquesPresentadorBase
	{
		#region M I E M B R O S   P R I V A D O S
		///// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
		//private const string TRazaCategoriaError = "Error";
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string NAmespace = @"HConnexum.Tramitador.Presentacion.Presentador.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string mensaje = null;
		string configValor = null;
		/// <summary>Constante de nulo de entero.</summary>
		private const int NUllInt = -1;
		/// <summary>Objeto para manejo de conexión a la BD de la aplicación 'Gestor'. </summary>
		private readonly ConexionADO connGestor = new ConexionADO();
		private string sP = null;
		private readonly List<SqlParameter> parametros = new List<SqlParameter>();
		private readonly SqlParameter paramIntermediarioId = new SqlParameter("@intermediario", SqlDbType.Int);
		private readonly SqlParameter paramProveedorId = new SqlParameter("@proveedor", SqlDbType.Int);
		private readonly SqlParameter paramDiagnosticoId = new SqlParameter("@idDiagnostico", SqlDbType.Int);
		private DataSet ds = new DataSet();
		private DataTable dt = new DataTable();

		#endregion

		#region M É T O D O S   P R I V A D O S
		/// <summary>Registra la auditoría en la aplicación.</summary>
		/// <param name="pException">Excepción a registar.</param>
		/// <param name="pMensaje">Mensaje a registrar.</param>
		private void Auditoria(Exception pException, string pMensaje)
		{
			/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
			string trazaCategoriaError = "Error";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				mensaje = string.Format("{0}.Auditoria(pException = {1}, pMensaje = {2})", NAmespace, pException
					, pMensaje);

				Debug.WriteLine(mensaje);
				Errores.ManejarError(pException, pMensaje);
				if(pException.InnerException != null)
					HttpContext.Current.Trace.Warn(trazaCategoriaError
						, pMensaje + "Error en la capa origen: " + pException.InnerException.Message);
				HttpContext.Current.Trace.Warn(trazaCategoriaError, pMensaje, pException);
				this.vista.Errores = MostrarMensaje(TiposMensaje.Error_Generico);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				Errores.ManejarError(ex, mensaje);
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn(trazaCategoriaError
						, mensaje + "Error en la capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn(trazaCategoriaError, mensaje, ex);
				this.vista.Errores = MostrarMensaje(TiposMensaje.Error_Generico);
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
				ds = datosParticulares.ObtenerDatosParticularesPorSuscriptor(pSuscriptorId);
				if(ds == null)
					return null;
				dt = ds.Tables[0];
				if(dt == null || dt.Rows.Count <= 0)
					return null;
				if(ds.Tables["Error"] != null)
					throw new Exception(ds.Tables["Error"].Rows[0]["UserMessage"].ToString());
				var lista = dt.AsEnumerable().Where(r => r["Nombre"].ToString().ToUpper() == pDatoParticular.ToUpper());
				if(lista == null)
					return null;
				var tabla = lista.CopyToDataTable();
				if(tabla != null && tabla.Rows.Count > 0)
					result = tabla.Rows[0]["Valor"].ToString();
			}
			catch(Exception ex)
			{
				Auditoria(ex, mensaje);
			}
			return result;
		}
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		//readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		///<summary>Vista asociada al presentador.</summary>
		readonly ISolicitudNuevoMovimientoCA vista;
		/// <summary>Identificador del movimiento 'ingreso ambulatorio' en la aplicación del cliente.</summary>
		public string MovimientoIngresoActivacionCaId { get; set; }
		/// <summary>Identificador del movimiento 'ingreso hospitalario' en la aplicación del cliente.</summary>
		public string MovimientoIngresoRefecharCaId { get; set; }
		/// <summary>Identificador del movimiento 'anulación de ingreso' en la aplicación del cliente.</summary>
		public string MovimientoIngresoAnulacionId { get; set; }
		/// <summary>Identificador del movimiento 'extensión' en la aplicación del cliente.</summary>
		public string MovimientoExtensionId { get; set; }
		/// <summary>Identificador del movimiento 'egreso' en la aplicación del cliente.</summary>
		public string MovimientoEgresoId { get; set; }
		/// <summary>Identificador del movimiento 'anulación de egreso' en la aplicación del cliente.</summary>
		public string MovimientoEgresoAnulacionId { get; set; }
		/// <summary>Valor del estado 'aprobado' del movimiento en la aplicación del cliente.</summary>
		public string MovimientoEstadoAprobado { get; set; }
		/// <summary>Valor del estado 'rechazado' del movimiento en la aplicación del cliente.</summary>
		public string MovimientoEstadoRechazado { get; set; }
		/// <summary>Nombre del tipo de caso 'ambulatorio' en la aplicación del cliente.</summary>
		public string CasoTipoAmbulatorioNombre { get; set; }
		/// <summary>Nombre del tipo de caso 'hospitalario' en la aplicación del cliente.</summary>
		public string CasoTipoHospitalarioNombre { get; set; }
		#endregion

		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public SolicitudNuevoMovimientoCAPresentador(ISolicitudNuevoMovimientoCA vista)
		{
			try
			{
				this.vista = vista;
				this.MovimientoEstadoAprobado = "APROBADO";
				this.MovimientoEstadoRechazado = "RECHAZO";
				if(string.IsNullOrWhiteSpace(this.MovimientoIngresoActivacionCaId))
				{
					configValor = ConfigurationManager.AppSettings[@"ListaValorIdIngresoActivacionCA"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.MovimientoIngresoActivacionCaId = configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MovimientoIngresoRefecharCaId))
				{
					configValor = ConfigurationManager.AppSettings[@"ListaValorIdRefecharCA"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.MovimientoIngresoRefecharCaId = configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MovimientoIngresoAnulacionId))
				{
					configValor = ConfigurationManager.AppSettings[@"ListaValorIdIngresoAnulacionCA"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.MovimientoIngresoAnulacionId = configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MovimientoExtensionId))
				{
					configValor = ConfigurationManager.AppSettings[@"ListaValorIdExtension"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.MovimientoExtensionId = configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MovimientoEgresoId))
				{
					configValor = ConfigurationManager.AppSettings[@"ListaValorIdEgreso"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.MovimientoEgresoId = configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.MovimientoEgresoAnulacionId))
				{
					configValor = ConfigurationManager.AppSettings[@"ListaValorIdEgresoAnulacion"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.MovimientoEgresoAnulacionId = configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.CasoTipoAmbulatorioNombre))
				{
					configValor = ConfigurationManager.AppSettings[@"CasoTipoAmbulatorioNombre"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.CasoTipoAmbulatorioNombre = configValor.Trim();
				}
				if(string.IsNullOrWhiteSpace(this.CasoTipoHospitalarioNombre))
				{
					configValor = ConfigurationManager.AppSettings[@"CasoTipoHospitalarioNombre"];
					if(!string.IsNullOrWhiteSpace(configValor.Trim()))
						this.CasoTipoHospitalarioNombre = configValor.Trim();
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, mensaje);
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
				mensaje = string.Format("{0}.ObtenerBDConexionCadena(pSuscriptorId = {1})", NAmespace, pSuscriptorId);

				string configBDOrigen = ConfigurationManager.AppSettings[@"ListaOrigenDB"];
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
				Auditoria(ex, mensaje);
			}
			return listaValor.Valor;
		}
		///<summary>Maneja la presentación de los datos en la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				mensaje = string.Format("{0}.MostrarVista()", NAmespace);

				if(this.vista.ParametrosEntrada != null)
				{
					if(string.IsNullOrWhiteSpace(this.vista.SuscriptorIntermediarioConnectionString))
					{
						int id = NUllInt;
						int.TryParse(this.vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"], out id);
						if(id > 0)
							this.vista.SuscriptorIntermediarioConnectionString = ObtenerBDConexionCadena(id);
					}
					int intermediarioId = NUllInt;
					int.TryParse(this.vista.ParametrosEntrada[@"IDINTERMEDIARIO"], out intermediarioId);
					int proveedorId = NUllInt;
					int.TryParse(this.vista.ParametrosEntrada[@"IDPROVEEDOR"], out proveedorId);
					if(intermediarioId > 0 && proveedorId > 0)
						this.vista.ListaDiagnosticoTipos = ObtenerDiagnosticoTipos(intermediarioId, proveedorId);
				}
				this.vista.PasoId = this.vista.IdMovimiento;
				switch(this.vista.BloqueTipo.ToUpper())
				{
					case "INGRESOCA":
						configValor = ConfigurationManager.AppSettings[@"ListaTipoMovimientoIngresoCA"];
						break;
					case "EGRESO":
						configValor = ConfigurationManager.AppSettings[@"ListaTipoMovimientoEgreso"];
						break;
				}
				if(!string.IsNullOrWhiteSpace(configValor))
				{
					IEnumerable<ListasValorDTO> listaTiposMovimientos = ObtenerListaValor(configValor);
					if((this.vista.BloqueTipo.ToUpper() == @"EGRESO") && (this.vista.ParametrosEntrada[@"ESTATUSMOVIMIENTOWEB"].ToUpper() == @"RECHAZADO"))
						listaTiposMovimientos.Where(l => l.NombreValor != "Extensión");
					this.vista.ListaMovimientoTipos = listaTiposMovimientos;
				}
				configValor = ConfigurationManager.AppSettings[@"ListaModoMovimiento"];
				if(!string.IsNullOrWhiteSpace(configValor))
					this.vista.ListaMovimientoModos = ObtenerListaValor(configValor);
			}
			catch(Exception ex)
			{
				Auditoria(ex, mensaje);
			}
		}
		/// <summary>Obtiene los tipos de diagnóstico.</summary>
		/// <param name="pIntermediarioId">Identificador del intermediario a consultar.</param>
		/// <param name="pProveedorId">Identificador del proveedor a consultar.</param>
		/// <returns>Lista de tipos de diagnóstico.</returns>
		public DataTable ObtenerDiagnosticoTipos(int pIntermediarioId, int pProveedorId)
		{
			sP = "pa_ListadoDiagnosticos_Filtrado_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				mensaje = string.Format("{0}.ObtenerDiagnosticoTipos(pIntermediarioId = {1}, pProveedorId = {2})", NAmespace, pIntermediarioId, pProveedorId);
				paramIntermediarioId.Value = pIntermediarioId;
				paramProveedorId.Value = pProveedorId;
				parametros.Clear();
				parametros.Add(paramIntermediarioId);
				parametros.Add(paramProveedorId);
				ds = connGestor.EjecutaStoredProcedure(sP, this.vista.SuscriptorIntermediarioConnectionString, parametros.ToArray());
				if(ds == null)
					return null;
				dt = ds.Tables[0];

				// Valor por defecto de la lista.
				DataTable dtNew = dt.Clone();
				DataRow row = dtNew.NewRow();
				row[0] = "Otros";
				row[1] = DBNull.Value;
				dtNew.Rows.Add(row);
				dt.Merge(dtNew);
			}
			catch(Exception ex)
			{
				Auditoria(ex, mensaje);
			}
			return dt;
		}
		/// <summary>Obtiene los tipos de procedimiento.</summary>
		/// <param name="pDiagnosticoId">Identificador del diagnóstico a consultar.</param>
		/// <param name="pIntermediarioId">Identificador del intermediario a consultar.</param>
		/// <param name="pProveedorId">Identificador del proveedor a consultar.</param>
		/// <returns>Lista de tipos de procedimiento.</returns>
		public DataTable ObtenerProcedimientoTipos(int pDiagnosticoId, int pIntermediarioId, int pProveedorId)
		{
			sP = "pa_ListadoTratamientos_Filtrado_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				mensaje = string.Format(
					"{0}.ObtenerProcedimientoTipos(pDiagnosticoId = {1}, pIntermediarioId = {2}, pProveedorId = {3})"
					, NAmespace, pDiagnosticoId, pIntermediarioId, pProveedorId);

				paramDiagnosticoId.Value = pDiagnosticoId;
				paramIntermediarioId.Value = pIntermediarioId;
				paramProveedorId.Value = pProveedorId;
				parametros.Clear();
				parametros.Add(paramDiagnosticoId);
				parametros.Add(paramIntermediarioId);
				parametros.Add(paramProveedorId);
				ds = connGestor.EjecutaStoredProcedure(sP, this.vista.SuscriptorIntermediarioConnectionString, parametros.ToArray());
				if(ds == null)
					return null;
				dt = ds.Tables[0];

				// Valor por defecto de la lista.
				DataTable dtnew = dt.Clone();
				DataRow row = dtnew.NewRow();
				row[0] = "Otros";
				row[1] = DBNull.Value;
				dt.Rows.Add(row);
				dt.Merge(dt);
			}
			catch(Exception ex)
			{
				Auditoria(ex, mensaje);
			}
			return dt;
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
				mensaje = string.Format("{0}.MostrarMensaje(pTipo = {1})", NAmespace, pTipo);

				switch(pTipo)
				{
					case TiposMensaje.PorDefecto:
						result = "[Mensaje por configurar]";
						break;
					case TiposMensaje.Error_Generico:
						result = WebConfigurationManager.AppSettings["MensajeExcepcion"];
						break;
					case TiposMensaje.Validacion_CampoRequerido:
						result = "Campo Obligatorio.";
						break;
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, mensaje);
			}
			return result;
		}
		#endregion

		/// <summary>Valida masivamente la página al ejecutar la solicitud.</summary>
		public void ValidarDatos()
		{
			try
			{
				vista.MovimientoEstado = @"Pendiente";
			}
			catch(Exception ex)
			{
				Auditoria(ex, mensaje);
			}
		}
	}
}
