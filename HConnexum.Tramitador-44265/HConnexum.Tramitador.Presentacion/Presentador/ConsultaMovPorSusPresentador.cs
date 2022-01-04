using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Servicios.Servicios;
using System.ServiceModel;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase ConsultaCasosPresentador.</summary>
	public class ConsultaMovPorSusPresentador : PresentadorListaBase<Movimiento>
	{
		///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
        readonly IConsultaMovPorSus vista;
      
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public ConsultaMovPorSusPresentador(IConsultaMovPorSus vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int usuarioSuscriptor, int suscriptor)
		{
			try
			{
				Filtro tes = new Filtro();
               
			
				
                if (!string.IsNullOrEmpty(vista.Suscriptor))
                {
                    tes.Campo = "Idsuscriptor";
                    tes.Operador = "EqualTo";
                    tes.Tipo = typeof(int);
                    tes.Valor = int.Parse(vista.Suscriptor);
                    parametrosFiltro.Add(tes);
                }
                if (!string.IsNullOrEmpty(vista.Servicio))
                {
                    tes.Campo = "IdServicioSuscriptor";
                    tes.Operador = "EqualTo";
                    tes.Tipo = typeof(int);
                    tes.Valor = int.Parse(vista.Servicio);
                    parametrosFiltro.Add(tes);
                }
				if(!string.IsNullOrEmpty(vista.UsuarioAsignado))
				{
					tes.Campo = "IdUsuarioAsignado";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(int);
					tes.Valor = int.Parse(vista.UsuarioAsignado);
					parametrosFiltro.Add(tes);
				}
                MovimientoRepositorio repositorioMov = new MovimientoRepositorio(udt);
				this.vista.Datos = repositorioMov.ObtenerDTO(orden, pagina, tamañoPagina, parametrosFiltro);
				this.vista.NumeroDeRegistros = repositorioMov.Conteo;
				
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

        /// <summary>
        /// LLenar el combo de suscriptores con todos los suscriptores del sistema
        /// </summary>
        public void LlenarSuscriptor() 
        {
            SuscriptorRepositorio repositorio = new SuscriptorRepositorio(udt);
            vista.ComboSuscriptor = repositorio.ObtenerSuscriptoresDTO();

        }
        public void LlenarSuscriptor(int id) 
        {
            SuscriptorRepositorio repositorio = new SuscriptorRepositorio(udt);
            vista.ComboSuscriptor = repositorio.ObtenerSuscriptorPorId(id);
        }
        public void LlenarCombosUsuario(int idSuscriptor) 
        {
            ServicioParametrizadorClient service = new ServicioParametrizadorClient();
            try
            {
               DataSet ds2 = service.ObtenerUsuariosPorIdSuscriptor(idSuscriptor);
                if (ds2.Tables[@"Error"] != null)
                    throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds2.Tables[0].Rows.Count > 0)
                    vista.ComboUsuarioAsignado = ds2.Tables[0];
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (service.State != CommunicationState.Closed)
                    service.Close();
            }


        
        }
        public void LlenarCombosServicio(int idSuscriptor)
        {
            ServicioParametrizadorClient service = new ServicioParametrizadorClient();
            try
            {
                DataSet ds1 = service.ObtenerServiciosPorIdSuscriptor(idSuscriptor);
                if (ds1.Tables[@"Error"] != null)
                    throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds1.Tables[0].Rows.Count > 0)
                    vista.ComboServicio = ds1.Tables[0];

                
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (service.State != CommunicationState.Closed)
                    service.Close();
            }



        }
        ///<summary>Método encargado de eliminar registros del conjunto.</summary>
        ///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
        public void EliminarUsuarioAsignado(IList<string> ids, int UsuarioActual)
        {
            try
            {                
                foreach (string id in ids)
                { 
                    udt.IniciarTransaccion();
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    MovimientoRepositorio repositorio= new MovimientoRepositorio(udt);
                    Movimiento mov = new Movimiento();
                    mov =repositorio.ObtenerPorId(idDesencriptado);                    
                    int AntiguoUsuario = mov.UsuarioAsignado.Value;
                    mov.UsuarioAsignado = null;
                    mov.Estatus = 154;
                    udt.MarcarModificado(mov);
                    ObservacionesMovimiento ObservacionMov = new ObservacionesMovimiento();
                    ObservacionMov.IdMovimiento = idDesencriptado;
                    ObservacionMov.FechaModificacion = DateTime.Now;
                    ObservacionMov.TxtObservacion = "Se desasigna del movimiento el usuario: " + AntiguoUsuario;
                    ObservacionMov.ModificadoPor = UsuarioActual;
                    ObservacionMov.CreadoPor = UsuarioActual;
                    ObservacionMov.FechaCreacion = DateTime.Now;
                    ObservacionMov.IndVigente = true;
                    udt.MarcarNuevo(ObservacionMov);

                  udt.Commit();
                }
                 
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        ///<summary>Método encargado de eliminar registros del conjunto.</summary>
        ///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
        public void AsignarUsuario(IList<string> ids,int idUsuario, int UsuarioActual)
        {
            try
            {
                foreach (string id in ids)
                {string descripcion=string.Empty;
                     udt.IniciarTransaccion();
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
                    Movimiento mov = repositorio.ObtenerPorId(idDesencriptado);
                    
                    if (mov.UsuarioAsignado!=null)
                    { 
                        int AntiguoUsuario = mov.UsuarioAsignado.Value;
                        descripcion= "Cambio de Usuario de: " + AntiguoUsuario + " a: " + idUsuario;
                    }else 
                        descripcion ="Se asigno el Usuario: "+idUsuario;

                    mov.UsuarioAsignado = idUsuario;                   
                    udt.MarcarModificado(mov);

                    ObservacionesMovimiento ObservacionMov = new ObservacionesMovimiento();
                    ObservacionMov.IdMovimiento = idDesencriptado;
                    ObservacionMov.FechaModificacion = DateTime.Now;
                    ObservacionMov.TxtObservacion = descripcion;
                    ObservacionMov.ModificadoPor = UsuarioActual;
                    ObservacionMov.CreadoPor = UsuarioActual;
                    ObservacionMov.FechaCreacion = DateTime.Now;
                    ObservacionMov.IndVigente = true;
                    udt.MarcarNuevo(ObservacionMov);

                    udt.Commit();
                }
               
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        /// <summary>
        /// LLena el usuario asigndo si llega por query string un id usuariosuscriptor
        /// </summary>
        /// <param name="id"></param>
        public void LlenarUsuarioAsignado(int id) 
        {
            UsuarioRepositorio repositorio = new UsuarioRepositorio(udt);
            vista.ComboUsuarioAsignado = repositorio.ObtenerPorIdUsuarioSuscriptor(id);
        }
        public void LlenarServicio(int id) 
        {
            ServiciosSuscriptorRepositorio repositorio = new ServiciosSuscriptorRepositorio(udt);
            vista.ComboServicio = repositorio.ObtenerporId(id);
        }

	}
}