using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase CasoPresentadorDetalle.</summary>
	public class CasoPresentadorDetalle : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz ICasoDetalle.</summary>
		readonly ICasoDetalle vista;

		///<summary>Variable de la entidad Caso.</summary>
		Caso _Caso;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public CasoPresentadorDetalle(ICasoDetalle vista)
		{
			this.vista = vista;
            this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
                int Idestatus;
                int? IdPrioridad;
                int IdServicioSuscriptor;
                int Idsuscriptor;
				CasoRepositorio repositorio = new CasoRepositorio(udt);
                CasoDTO CasoDTO = new CasoDTO();
                CasoDTO = repositorio.CasoPorID(vista.Id);
                if (CasoDTO != null)
                {
                    Idestatus = CasoDTO.Estatus;
                    IdServicioSuscriptor = CasoDTO.IdServiciosuscriptor;
                    Idsuscriptor = CasoDTO.IdSuscriptor;
                    IdPrioridad = CasoDTO.PrioridadAtencion;
                    
                    LlamaServicio(Idestatus, IdServicioSuscriptor, Idsuscriptor, IdPrioridad);

                    string nodisponible = "No Disponible";

                    this.vista.caso = CasoDTO.Id.ToString();
                    this.vista.IdSolicitud = CasoDTO.IdSolicitud.ToString();
                    this.vista.FechaSolicitud = (!String.IsNullOrEmpty(CasoDTO.FechaSolicitud.ToString()) ? CasoDTO.FechaSolicitud.ToString() : nodisponible);
                    this.vista.FechaAnulacion = (!String.IsNullOrEmpty(CasoDTO.FechaAnulacion.ToString()) ? CasoDTO.FechaAnulacion.ToString() : nodisponible);
                    this.vista.FechaRechazo = (!String.IsNullOrEmpty(CasoDTO.FechaRechazo.ToString()) ? CasoDTO.FechaAnulacion.ToString() : nodisponible);
                    this.vista.FechaCreacion2 = CasoDTO.FechaCreacion.ToString();
                    this.vista.CreadorPor = ObtenerNombreUsuario(CasoDTO.CreadorPor);
                    this.vista.version = CasoDTO.Version.ToString();
                    this.vista.Modificado = nodisponible;
                    this.vista.indChat = (CasoDTO.indChat != null) ? (bool)CasoDTO.indChat : false;
                }
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

        public int BuscaMensajesPendienteChat(int IdCaso)
        {
            try
            {
                BuzonChatRepositorio Buzon = new BuzonChatRepositorio(udt);
                int mensajes = Buzon.ObtenerMensajesNoLeidoDTO(IdCaso, vista.UsuarioActual.SuscriptorSeleccionado.Id);
                return mensajes;

            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
                return 0;
            }
        }

        public void LlamaServicio(int Idestatus, int IdServicioSuscriptor, int Idsuscriptor, int? IdPrioridad)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {


                DataSet ds1 = servicio.ObtenerSuscriptorPorIDCompleto(Idsuscriptor);
                if (ds1.Tables[@"Error"] != null)
                    throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    vista.Suscriptor = ds1.Tables[0].Rows[0]["Nombre"].ToString();
                    vista.NumDoc = ds1.Tables[0].Rows[0]["NumeroDoc"].ToString();
                    vista.TipoDoc = ds1.Tables[0].Rows[0]["TipoDoc"].ToString();
                }
                DataSet ds2 = servicio.ObtenerServicioSuscriptorPorId(IdServicioSuscriptor);
                if (ds2.Tables[@"Error"] != null)
                    throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds2.Tables[0].Rows.Count > 0)
                    vista.Servicio = ds2.Tables[0].Rows[0]["Nombre"].ToString();

                DataSet ds3 = servicio.ObtenerNombreValorPorID(Idestatus);
                if (ds3.Tables[@"Error"] != null)
                    throw new Exception(ds3.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    vista.Estatus = ds3.Tables[0].Rows[0]["NombreValor"].ToString();
                }

                if (IdPrioridad != null)
                {
                    DataSet ds4 = servicio.ObtenerNombreValorPorID((int)IdPrioridad);
                    if (ds4.Tables[@"Error"] != null)
                        throw new Exception(ds4.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds4.Tables[0].Rows.Count > 0)
                    {
                        vista.PrioridadAtencion = ds4.Tables[0].Rows[0]["NombreValor"].ToString();
                    }
                }

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
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }

        }
	}
}