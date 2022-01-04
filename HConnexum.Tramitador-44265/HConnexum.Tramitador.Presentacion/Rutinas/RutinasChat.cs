using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Datos;
using HConnexum.Infraestructura;
using System.Web;

namespace HConnexum.Tramitador.Presentacion.Rutinas
{
    public class RutinasChat
    {
        private DataSet _ds = new DataSet();
        private DataTable _dt = new DataTable();
        private ConexionADO _connGestor = new ConexionADO();
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
        private string _SP = null;
        private string _conexionCadena = null;
        private SqlParameter[] _parametros = new SqlParameter[2];
        private string _mensaje = null;
        private const string _EVENTO_REGISTRO_APLICACION_NOMBRE = @"HC-Tramitador";
        private const string _NAMESPACE = @"HConnexum.Tramitador.Presentacion.Rutinas";

        
        public RutinasChat()
        { 
        
        }

        /// <summary>Obtiene una cadena de conexión hacia la base de datos de la aplicación del cliente según suscriptor determinado.</summary>
        /// <param name="pSuscriptorId">Identificador del suscriptor a consultar.</param>
        /// <returns>Cadena de conexión hacia la base de datos.</returns>
        private string ObtenerBDConexionCadena(string Origen)
        {
            var listaValor = new ListasValorDTO();
            try
            { 
                string configBDOrigen = ConfigurationManager.AppSettings["ListaOrigenDB"];
               
                if (string.IsNullOrWhiteSpace(configBDOrigen))
                    return null;
                string listaItemNombre = Origen;
                var repositorio = new ListasValorRepositorio(udt);
                listaValor = repositorio.ObtenerListaValoresDTO(configBDOrigen, listaItemNombre);
                if (listaValor == null)
                    return null;
            }
            catch (Exception ex)
            {
                Auditoria(ex, _mensaje);
            }
            return listaValor.Valor;
        }


        public bool EscribirMensajeAppCliente(string Origen, int IdCasoExterior, string Mensaje, int Idsuscriptor)
        {
            _SP = "pa_nuevoComentarioExpediente_HC2";
            try
            {
                _conexionCadena = ObtenerBDConexionCadena(Origen);
                if (string.IsNullOrWhiteSpace(_conexionCadena))
                    return false;

                _parametros[0] = new SqlParameter("@expediente", IdCasoExterior);
                _parametros[1] = new SqlParameter("@comentario", Mensaje);
                if (!string.IsNullOrWhiteSpace(_conexionCadena))
                {
                    _ds = _connGestor.EjecutaStoredProcedure(_SP, _conexionCadena, _parametros);
                    if (_ds == null)
                        return false;
                    _dt = _ds.Tables[0];
                    if (_dt == null || _dt.Rows.Count <= 0)
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Auditoria(ex, _mensaje);
            }
            return true;
        }

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
                if (pException.InnerException != null)
                    HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR
                        , pMensaje + "Error en la capa origen: " + pException.InnerException.Message);
                HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR, pMensaje, pException);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Errores.ManejarError(ex, _mensaje);
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR
                        , _mensaje + "Error en la capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR, _mensaje, ex);
            }
        }
    }
}
