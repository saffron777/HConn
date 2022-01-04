using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	///<summary>Presentador para el manejo del control Web de usuario 'SeleccionAsegurado'.</summary>
	public class SeleccionAseguradoPresentador : BloquesPresentadorBase
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string _NAMESPACE = @"HConnexum.Tramitador.Presentacion.Presentador.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string _mensaje = null;
		/// <summary>Nombre de la aplicación bajo la cual registrar un evento en el Registro de Eventos de Windows 
		/// (event log).</summary>
		private const string _EVENTO_REGISTRO_APLICACION_NOMBRE = @"HC-Tramitador";
		/// <summary>Cadena de conexión a la base de datos de Gestor.</summary>
		private string _conexionCadena = null;
		/// <summary>Objeto para manejo de conexión a la BD de la aplicación 'Gestor'. </summary>
		private ConexionADO _connGestor = new ConexionADO();
		private string _SP = null;
		private List<SqlParameter> _parametros = new List<SqlParameter>();
		private SqlParameter _paramBeneficiarioCedula;
		private SqlParameter _paramIntermediarioId;
		private SqlParameter _paramServicio;
		private DataSet _ds = new DataSet();
		private DataTable _dt = new DataTable();
		///// <summary>Código del servicio 'Clave de Emergencia' en la aplicación 'Gestor'. </summary>
		//private const string _SERVICIO_CLAVE_DE_EMERGENCIA_CODIGO = "Clave";
		private const int _NULL_INT = -1;
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
				_mensaje = string.Format("{0}.Auditoria(pException = {1}, pMensaje = {2})", _NAMESPACE, pException, pMensaje);
				Debug.WriteLine(pException.ToString());
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

		private SqlParameter ObtenerParametroBeneficiarioCedula()
		{
			return new SqlParameter("@cedula", SqlDbType.VarChar, 50);
		}

		private SqlParameter ObtenerParametroIntermediarioId()
		{
			return new SqlParameter("@id_intermediario", SqlDbType.Int);
		}

		private SqlParameter ObtenerParametroServicio()
		{
			return new SqlParameter("@servicio", SqlDbType.VarChar, 20);
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

		/// <summary>Obtiene una cadena de conexión hacia la base de datos de la aplicación del cliente según suscriptor determinado.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		/// <returns>Cadena de conexión hacia la base de datos.</returns>
		private string ObtenerBDConexionCadena(int pSuscriptorId)
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

		/// <summary>Devuelve un identificador en la aplicación del cliente según un suscriptor y código de enlace determinados.</summary>
		/// <param name="pConexionBDSuscriptorId">Identificador del suscriptor a utilizar para crear la conexión a la base de datos.</param>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		/// <param name="pEnlaceCodigo">Código de enlace a consultar.</param>
		/// <returns>Identificador en la aplicación 'Gestor'</returns>
		private int ObtenerGestorId(int pConexionBDSuscriptorId, int pSuscriptorId, int pEnlaceCodigo)
		{
			HttpContext.Current.Trace.Warn(@"Inicio ObtenerGestorId");
			HttpContext.Current.Trace.Warn(@"pConexionBDSuscriptorId: [" + pConexionBDSuscriptorId + @"]");
			HttpContext.Current.Trace.Warn(@"pSuscriptorId: [" + pSuscriptorId + @"]");
			HttpContext.Current.Trace.Warn(@"pEnlaceCodigo: [" + pEnlaceCodigo + @"]");
			HttpContext.Current.Trace.Warn(@"sp: [pa_busca_Id_Enlace_HC2]");

			int result = _NULL_INT;
			_SP = "pa_busca_Id_Enlace_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ObtenerGestorId(pConexionBDSuscriptorId = {1}, pSuscriptorId = {2}, pEnlaceCodigo = {3})", _NAMESPACE, pConexionBDSuscriptorId, pSuscriptorId, pEnlaceCodigo);
				_conexionCadena = ObtenerBDConexionCadena(pConexionBDSuscriptorId);
				if(string.IsNullOrWhiteSpace(_conexionCadena))
					return _NULL_INT;
				_parametros.Clear();
				_parametros.Add(new SqlParameter()
				{
					ParameterName = "@CodEnlace",
					SqlDbType = SqlDbType.Int,
					Value = pEnlaceCodigo
				});
				_ds = _connGestor.EjecutaStoredProcedure(_SP, _conexionCadena, _parametros.ToArray());
				if(_ds == null)
					return _NULL_INT;
				_dt = _ds.Tables[0];
				if(_dt == null || _dt.Rows.Count <= 0)
					return _NULL_INT;
				int.TryParse(_dt.Rows[0]["Company_Id"].ToString(), out result);
				HttpContext.Current.Trace.Warn(@"result: [" + result + @"]");
				HttpContext.Current.Trace.Warn(@"Fin ObtenerGestorId");
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return result;
		}

		/// <summary>Obtiene el código de estacion de un proveedor para un intermediario determinado.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		/// <param name="pIntermediarioId">Identificador del intermediario a consultar.</param>
		/// <param name="pProveedorId">Identificador del proveedor a consultar.</param>
		/// <returns>Código de la estación.</returns>
		private string ObtenerProveedorEstacionCodigo(int pSuscriptorId, int pIntermediarioId, int pProveedorId)
		{
			HttpContext.Current.Trace.Warn(@"Inicio ObtenerProveedorEstacionCodigo");
			HttpContext.Current.Trace.Warn(@"pSuscriptorId: [" + pSuscriptorId + @"]");
			HttpContext.Current.Trace.Warn(@"pIntermediarioId: [" + pIntermediarioId + @"]");
			HttpContext.Current.Trace.Warn(@"pProveedorId: [" + pProveedorId + @"]");
			HttpContext.Current.Trace.Warn(@"sp: [pa_BuscaEstacion_HC2]");

			string result = null;
			_SP = "pa_BuscaEstacion_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ObtenerEstacionCodigo(pSuscriptorId = {1}, pIntermediarioId = {2}, pProveedorId = {3})", _NAMESPACE, pSuscriptorId, pIntermediarioId, pProveedorId);
				ListasValorRepositorio listasValorRepositorio = new ListasValorRepositorio(udt);
				ListasValorDTO lv = listasValorRepositorio.ObtenerListaValoresDTO(ConfigurationManager.AppSettings["ListaOrigenDB"], ConfigurationManager.AppSettings["ListaValorRM"]);
				_conexionCadena = lv.Valor; // ObtenerBDConexionCadena(pSuscriptorId);
				if(string.IsNullOrWhiteSpace(_conexionCadena))
					return null;
				_parametros.Clear();
				_paramIntermediarioId = ObtenerParametroIntermediarioId();
				_paramIntermediarioId.ParameterName = "@IdIntermediario";
				_paramIntermediarioId.Value = pIntermediarioId;
				_parametros.Add(_paramIntermediarioId);
				_parametros.Add(new SqlParameter()
				{
					ParameterName = "@IdProveedor",
					SqlDbType = SqlDbType.Int,
					Value = pProveedorId
				});
				_ds = _connGestor.EjecutaStoredProcedure(_SP, _conexionCadena, _parametros.ToArray());
				if(_ds == null)
					return null;
				_dt = _ds.Tables[0];
				if(_dt == null || _dt.Rows.Count <= 0)
					return null;
				result = _dt.Rows[0]["estacion_id"].ToString();
				HttpContext.Current.Trace.Warn(@"result: [" + result + @"]");
				HttpContext.Current.Trace.Warn(@"Fin ObtenerProveedorEstacionCodigo");
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return result;
		}

		/// <summary>Evalúa si un intermediario permite o no la condición de que se pueda ingresar los datos de un afiliado 
		/// en caso de que no exista.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		/// <param name="pIntermediarioId">Identificador del intermediario a consultar.</param>
		private bool EvaluarIntermediarioCondicionAseguradoNoEncontrado(int pSuscriptorId, int pIntermediarioId)
		{
			HttpContext.Current.Trace.Warn(@"Inicio EvaluarIntermediarioCondicionAseguradoNoEncontrado");
			HttpContext.Current.Trace.Warn(@"pSuscriptorId: [" + pSuscriptorId + @"]");
			HttpContext.Current.Trace.Warn(@"pIntermediarioId: [" + pIntermediarioId + @"]");
			HttpContext.Current.Trace.Warn(@"sp: [pa_permite_ane_HC2]");
			HttpContext.Current.Trace.Warn(@"Fin EvaluarIntermediarioCondicionAseguradoNoEncontrado");

			_SP = "pa_permite_ane_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.EvaluarIntermediarioCondicionAseguradoNoEncontrado(pSuscriptorId = {1}, pIntermediarioId = {2})", _NAMESPACE, pSuscriptorId, pIntermediarioId);
				_conexionCadena = ObtenerBDConexionCadena(pSuscriptorId);
				if(string.IsNullOrWhiteSpace(_conexionCadena))
					return false;
				_paramIntermediarioId = ObtenerParametroIntermediarioId();
				_paramIntermediarioId.ParameterName = "@seguro";
				_paramIntermediarioId.Value = pIntermediarioId;
				_parametros.Clear();
				_parametros.Add(_paramIntermediarioId);
				_ds = _connGestor.EjecutaStoredProcedure(_SP, _conexionCadena, _parametros.ToArray());
				if(_ds == null)
					return false;
				_dt = _ds.Tables[0];
				if(_dt == null || _dt.Rows.Count <= 0)
					return false;
				int paa = -1;
				if(int.TryParse(_dt.Rows[0]["paa"].ToString(), out paa))
					if(paa != 0)
						return true;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return false;
		}
		/// <summary>Evalúa si el beneficiario ya tiene una solicitud de servicio abierta.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		private bool EvaluaSolicitudAbierta(int pSuscriptorId, int pBeneficiarioId, int pProveedorId)
		{
			HttpContext.Current.Trace.Warn(@"Inicio EvaluaSolicitudAbierta");
			HttpContext.Current.Trace.Warn(@"pSuscriptorId: [" + pSuscriptorId + @"]");
			HttpContext.Current.Trace.Warn(@"pBeneficiarioId: [" + pBeneficiarioId + @"]");
			HttpContext.Current.Trace.Warn(@"pProveedorId: [" + pProveedorId + @"]");
			HttpContext.Current.Trace.Warn(@"sp: [pa_solicitud_abierta_HC2]");
			HttpContext.Current.Trace.Warn(@"Fin EvaluaSolicitudAbierta");

			_SP = "pa_solicitud_abierta_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format(
					"{0}.EvaluaSolicitudAbierta(pSuscriptorId = {1}, pBeneficiarioId = {2}, pProveedorId = {3})", _NAMESPACE, pSuscriptorId, pBeneficiarioId, pProveedorId);
				_conexionCadena = ObtenerBDConexionCadena(pSuscriptorId);
				if(string.IsNullOrWhiteSpace(_conexionCadena))
					return false;
				_parametros.Clear();
				_parametros.Add(new SqlParameter()
				{
					ParameterName = "@asegurado",
					SqlDbType = SqlDbType.Int,
					Value = pBeneficiarioId
				});
				_parametros.Add(new SqlParameter()
				{
					ParameterName = "@clinica",
					SqlDbType = SqlDbType.Int,
					Value = pProveedorId
				});
				_ds = _connGestor.EjecutaStoredProcedure(_SP, _conexionCadena, _parametros.ToArray());
				if(_ds == null)
					return false;
				_dt = _ds.Tables[0];
				if(_dt == null || _dt.Rows.Count <= 0)
					return false;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return true;
		}
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		///<summary>Vista asociada al presentador.</summary>
		readonly ISeleccionAsegurado Vista;
		#endregion

		#region M É T O D O S   P Ú B L I C O S
		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public SeleccionAseguradoPresentador(ISeleccionAsegurado vista)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.SeleccionAseguradoPresentador()", _NAMESPACE);
				this.Vista = vista;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		///<summary>Maneja la presentación de los datos en la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.MostrarVista()", _NAMESPACE);
				this.Vista.SuscriptorProveedorId = Vista.SuscriptorProveedor; // ;
				int enlaceCodigo = _NULL_INT;
				var dtoSuscriptor = new SuscriptorRepositorio(udt);
				Suscriptor suscriptorProveedor = dtoSuscriptor.ObtenerPorId(Vista.SuscriptorProveedor);
				HttpContext.Current.Trace.Warn(@"suscriptorProveedor.Id: [" + suscriptorProveedor.Id + @"]");
				HttpContext.Current.Trace.Warn(@"suscriptorProveedor.CodIdExterno: [" + suscriptorProveedor.CodIdExterno + @"]");
				if(int.TryParse(suscriptorProveedor.CodIdExterno, out enlaceCodigo))
					this.Vista.ProveedorGestorId = (!String.IsNullOrEmpty(suscriptorProveedor.CodIdExterno)) ? int.Parse(suscriptorProveedor.CodIdExterno) : 0;
				Suscriptor suscriptorIntermediario = dtoSuscriptor.ObtenerPorId(this.Vista.SuscriptorIntermediarioId);
				if(suscriptorIntermediario != null)
				{
					this.Vista.IntermediarioNombre = suscriptorIntermediario.Nombre;
                    this.Vista.IntermediarioFaxNumero = ObtenerFax(suscriptorProveedor.CodIdExterno, suscriptorIntermediario.CodIdExterno, suscriptorIntermediario.Id);
					if(suscriptorIntermediario.CodIdExterno != null)
					{
						enlaceCodigo = _NULL_INT;
						if(int.TryParse(suscriptorIntermediario.CodIdExterno, out enlaceCodigo))
							this.Vista.IntermediarioGestorId = ObtenerGestorId(this.Vista.SuscriptorIntermediarioId, this.Vista.SuscriptorIntermediarioId, enlaceCodigo);
					}
				}
				this.Vista.ProveedorEstacionCodigo = ObtenerProveedorEstacionCodigo(this.Vista.SuscriptorIntermediarioId, this.Vista.IntermediarioGestorId, this.Vista.ProveedorGestorId);
				int solicitudOcurrenciaFechaMaximoDiasAtras = -1;
				string configValor = ConfigurationManager.AppSettings["SolicitudOcurrenciaFechaMaximoDiasAtras"];
				if(!string.IsNullOrWhiteSpace(configValor))
					if(int.TryParse(configValor, out solicitudOcurrenciaFechaMaximoDiasAtras))
						this.Vista.SolicitudOcurrenciaFechaValorMinimo = DateTime.Now.Date.AddDays(solicitudOcurrenciaFechaMaximoDiasAtras * -1);
				this.Vista.IntermediarioPermiteAseguradoNoEncontrado = EvaluarIntermediarioCondicionAseguradoNoEncontrado(this.Vista.SuscriptorIntermediarioId, this.Vista.IntermediarioGestorId);
				this.Vista.TitularNoEncontradoNacimientoFechaValorMaximo = DateTime.Now.Date.AddYears(-18);
				this.Vista.BeneficiarioNoEncontradoNacimientoFechaValorMaximo = DateTime.Now.Date;
				this.Vista.SolicitudFecha = DateTime.Now.Date.ToString("dd/MM/yyyy");
				this.Vista.SolicitudOcurrenciaFechaValorMaximo = DateTime.Now.Date;
				configValor = ConfigurationManager.AppSettings["ListaTipoDocumento"];
				if(!string.IsNullOrWhiteSpace(configValor))
					this.Vista.ListaDocumentoTipos = ObtenerListaValor(configValor);
				configValor = ConfigurationManager.AppSettings["ListaSexo"];
				if(!string.IsNullOrWhiteSpace(configValor))
					this.Vista.ListaSexoTipos = ObtenerListaValor(configValor);
				configValor = ConfigurationManager.AppSettings["ListaParentesco"];
				if(!string.IsNullOrWhiteSpace(configValor))
					this.Vista.ListaParentescoTipos = ObtenerListaValor(configValor);
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}
        private string ObtenerFax(string idProveedor, string idIntermediario, int idsuscriptor)
        {
            string stringConexion = ObtenerConexionString(idsuscriptor);
            if (!string.IsNullOrEmpty(stringConexion))
            {
                ConexionADO _connGestor = new ConexionADO();
                if (!string.IsNullOrEmpty(idProveedor.ToString()) && !string.IsNullOrEmpty(idIntermediario.ToString()))
                {
                    SqlParameter[] parametros = new SqlParameter[4];
                    parametros[0] = new SqlParameter("@Intermediario ", (int.Parse(idIntermediario)));
                    parametros[1] = new SqlParameter("@Proveedor   ", (int.Parse(idProveedor)));
                    parametros[2] = new SqlParameter("@TipoServicio  ", "Clave");
                    parametros[3] = new SqlParameter("@respuesta  ", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 1, 1, string.Empty, DataRowVersion.Current, 1);
                    using (DataSet ds = _connGestor.EjecutaStoredProcedure(@"pa_FaxProveedorAfiliado_HC2", stringConexion, parametros))
                        if (!string.IsNullOrEmpty(parametros[3].Value.ToString()))
                        {
                            return parametros[3].Value.ToString();
                        }
                        else //hubo algun error
                            return "";
                }
            }
            else
                throw new Exception("El suscriptor Intermediario no tiene configurado Un string de conexion");
            return "";
        }

        public string ObtenerConexionString(int idSuscriptor)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                using (DataSet ds = servicio.ObtenerConexionStringListaValor(idSuscriptor, ConfigurationManager.AppSettings[@"ListaOrigenDB"]))
                {
                    if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                        return ds.Tables[0].Rows[0][@"ConexionString"].ToString();
                }
            }
            catch (Exception e)
            {
                Errores.EscribirTraza(e, @"HC Tramitador", @"ObtenerConexionString");
            }

            return string.Empty;
        }

		/// <summary>Obtienen las pólizas asociadas a un afiliado.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		/// <param name="pCedula">Número de cédula de identidad del afiliado que se desea consultar.</param>
		/// <param name="pIntermediarioId">Identificador del intermediario a consultar.</param>
		public bool ObtenerAfiliadoPolizas(int pSuscriptorId, string pCedula, int pIntermediarioId)
		{
			HttpContext.Current.Trace.Warn(@"Inicio ObtenerAfiliadoPolizas");
			HttpContext.Current.Trace.Warn(@"pSuscriptorId: [" + pSuscriptorId + @"]");
			HttpContext.Current.Trace.Warn(@"pCedula: [" + pCedula + @"]");
			HttpContext.Current.Trace.Warn(@"pIntermediarioId: [" + pIntermediarioId + @"]");
			HttpContext.Current.Trace.Warn(@"sp: [pa_busqueda_poliza_integrado_HC2]");
			HttpContext.Current.Trace.Warn(@"Fin ObtenerAfiliadoPolizas");

			_SP = "pa_busqueda_poliza_integrado_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ObtenerAfiliadoPolizas(pSuscriptorId = {1}, pCedula = {2}, pIntermediarioId = {3})", _NAMESPACE, pSuscriptorId, pCedula, pIntermediarioId);
				_conexionCadena = ObtenerBDConexionCadena(pSuscriptorId);
				if(string.IsNullOrWhiteSpace(_conexionCadena))
					return false;
				_paramIntermediarioId = ObtenerParametroIntermediarioId();
				_paramIntermediarioId.Value = pIntermediarioId;
				_paramBeneficiarioCedula = ObtenerParametroBeneficiarioCedula();
				_paramBeneficiarioCedula.Size = 20;
				_paramBeneficiarioCedula.Value = pCedula;
				_paramServicio = ObtenerParametroServicio();
				_paramServicio.Value = this.Vista.ServicioCodigo;
				_parametros.Clear();
				_parametros.Add(_paramIntermediarioId);
				_parametros.Add(_paramBeneficiarioCedula);
				_parametros.Add(_paramServicio);
				_ds = _connGestor.EjecutaStoredProcedure(_SP, _conexionCadena, _parametros.ToArray());
				if(_ds == null)
					return false;
				_dt = _ds.Tables[0];
				if(_dt == null || _dt.Rows.Count <= 0)
					return false;
				this.Vista.AfiliadoPolizasRegistrosCantidad = _dt.Rows.Count;
				this.Vista.AfiliadoPolizasDatos = _dt;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return true;
		}

		/// <summary>Obtiene el grupo familiar asociado a una póliza.</summary>
		/// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
		public bool ObtenerPolizaGrupoFamiliar(int pSuscriptorId, int pIntermediarioId, string pCedula, int pPolizaId, string pRamo)
		{
			HttpContext.Current.Trace.Warn(@"Inicio ObtenerPolizaGrupoFamiliar");
			HttpContext.Current.Trace.Warn(@"pSuscriptorId: [" + pSuscriptorId + @"]");
			HttpContext.Current.Trace.Warn(@"pIntermediarioId: [" + pIntermediarioId + @"]");
			HttpContext.Current.Trace.Warn(@"pCedula: [" + pCedula + @"]");
			HttpContext.Current.Trace.Warn(@"pPolizaId: [" + pPolizaId + @"]");
			HttpContext.Current.Trace.Warn(@"pRamo: [" + pRamo + @"]");
			HttpContext.Current.Trace.Warn(@"sp: [pa_busqueda_asegurado_integrado_HC2]");
			HttpContext.Current.Trace.Warn(@"Fin ObtenerPolizaGrupoFamiliar");

			_SP = "pa_busqueda_asegurado_integrado_HC2";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ObtenerPolizaGrupoFamiliar(pSuscriptorId = {1}, pIntermediarioId = {2}, pCedula = {3}, pPolizaId = {4}, pRamo = {5})", _NAMESPACE, pSuscriptorId, pIntermediarioId, pCedula, pPolizaId, pRamo);
				_conexionCadena = ObtenerBDConexionCadena(pSuscriptorId);
				if(string.IsNullOrWhiteSpace(_conexionCadena))
					return false;
				_paramIntermediarioId = ObtenerParametroIntermediarioId();
				_paramIntermediarioId.Value = pIntermediarioId;
				_paramBeneficiarioCedula = ObtenerParametroBeneficiarioCedula();
				_paramBeneficiarioCedula.Size = 20;
				_paramBeneficiarioCedula.Value = pCedula;
				_paramServicio = ObtenerParametroServicio();
				_paramServicio.Value = this.Vista.ServicioCodigo;
				_parametros.Clear();
				_parametros.Add(_paramIntermediarioId);
				_parametros.Add(_paramBeneficiarioCedula);
				_parametros.Add(_paramServicio);
				_parametros.Add(new SqlParameter()
				{
					ParameterName = "@id_poliza",
					SqlDbType = SqlDbType.Int,
					Value = pPolizaId
				});
				_parametros.Add(new SqlParameter()
				{
					ParameterName = "@ramo",
					SqlDbType = SqlDbType.VarChar,
					Size = 10,
					Value = pRamo
				});
				_ds = _connGestor.EjecutaStoredProcedure(_SP, _conexionCadena, _parametros.ToArray());
				if(_ds == null)
					return false;
				_dt = _ds.Tables[0];
				if(_dt == null || _dt.Rows.Count <= 0)
					return false;
				this.Vista.PolizaGrupoFamiliarRegistrosCantidad = _dt.Rows.Count;
				this.Vista.PolizaGrupoFamiliarDatos = _dt;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return true;
		}

		public void ValidarDatos()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ValidarDatos()", _NAMESPACE);
				if(string.IsNullOrEmpty(Vista.ProveedorEstacionCodigo))
				{
					this.Vista.Errores = MostrarMensaje(TiposMensaje.NoExisteEstacion);
					return;
				}
				if(!this.Vista.BeneficiarioExiste && !this.Vista.PanelSolicitudDatosVisible)
				{
					this.Vista.Errores = MostrarMensaje(TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_RegistroSolicitudRequerido);
					return;
				}
				if(this.Vista.BeneficiarioExiste && !this.Vista.PanelAfiliadoPolizaVisible)
				{
					this.Vista.Errores = MostrarMensaje(TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_ConsultaAfiliadoRequerida);
					return;
				}
				if(this.Vista.BeneficiarioExiste && !this.Vista.PanelPolizaGrupoFamiliarVisible)
				{
					this.Vista.Errores = MostrarMensaje(TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_ConsultaGrupoFamiliarRequerida);
					return;
				}
				if(!this.Vista.PanelSolicitudDatosVisible)
				{
					this.Vista.Errores = MostrarMensaje(TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_RegistroSolicitudRequerido);
					return;
				}
				if(this.Vista.BeneficiarioExiste)
					if(EvaluaSolicitudAbierta(this.Vista.SuscriptorIntermediarioId, Convert.ToInt32(this.Vista.BeneficiarioId), this.Vista.ProveedorGestorId))
					{
						this.Vista.Errores = MostrarMensaje(TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_SolicitudAbierta);
						return;
					}
				if(Vista.BeneficiarioParentesco.ToUpper().Contains("HIJO"))
				{
					int edad = (DateTime.MinValue + (DateTime.Now - Vista.BeneficiarioHijoNacimientoFecha.Value)).Year - 1;
					if(edad.ToString().Trim() != Vista.BeneficiarioHijoEdad.Trim())
					{
						this.Vista.Errores = MostrarMensaje(TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_EdadIgualFechaNacimiento);
						return;
					}
				}
				if(!string.IsNullOrWhiteSpace(this.Vista.TitularNoExistenteCedulaNumero))
					this.Vista.TitularCedulaNumero = this.Vista.TitularNoExistenteCedulaNumero.Trim();
				if(!string.IsNullOrWhiteSpace(this.Vista.BeneficiarioNoExistenteCedulaNumero))
					this.Vista.BeneficiarioCedulaNumero = this.Vista.BeneficiarioNoExistenteCedulaNumero.Trim();
				if(!string.IsNullOrWhiteSpace(this.Vista.TitularNoExistenteContratanteNombre))
					this.Vista.TitularContratanteNombre = this.Vista.TitularNoExistenteContratanteNombre.Trim();
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
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
					case TiposMensaje.NoExisteEstacion:
						result = @"Favor comunicarse al 5050539 e indicar al operador que le cree una estación de trabajo para poder culminar su caso.";
						break;
					case TiposMensaje.Error_Generico:
						result = WebConfigurationManager.AppSettings["MensajeExcepcion"];
						break;
					case TiposMensaje.Validacion_CampoRequerido:
						result = "Campo obligatorio.";
						break;
					case TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_Parentesco_Recaudos:
						result = "Para poder procesar su verificación recuerde enviar previamente los documentos que demuestren el parentesco del asegurado.";
						break;
					case TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_SolicitudAbierta:
						result = "Ya existe una solicitud pendiente para este paciente.";
						break;
					case TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_ConsultaAfiliadoRequerida:
						result = "Debe consultar un afiliado.";
						break;
					case TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_ConsultaGrupoFamiliarRequerida:
						result = "Debe consultar el grupo familiar asociado al afiliado.";
						break;
					case TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_RegistroSolicitudRequerido:
						result = "Debe registrar los datos de la solicitud.";
						break;
					case TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_AfiliadoNoExiste:
                        result = "Si el paciente es beneficiario de la póliza, realice la búsqueda por la cédula del titular.";
						break;
					case TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_EdadIgualFechaNacimiento:
						result = "La edad y la fecha de nacimiento no concuerdan.";
						break;
					case TiposMensaje.PolizaVencida:
						result = "La póliza se encuentra vencida, se sugiere contactar a su compañía aseguradora.";
						break;
					case TiposMensaje.PolizaInactiva:
						result = "La póliza se encuentra inactiva, se sugiere contactar a su compañía aseguradora.";
						break;
					case TiposMensaje.AseguradoInactivo:
						result = "El asegurado se encuentra inactivo, se sugiere contactar a su compañía aseguradora.";
						break;
                    case TiposMensaje.AseguradoSinCobertura:
                        result = "El asegurado no posee cobertura asociada, se sugiere contactar a la compañía aseguradora.";
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