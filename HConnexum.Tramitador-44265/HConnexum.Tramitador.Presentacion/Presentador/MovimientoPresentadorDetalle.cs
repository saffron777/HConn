using System;
using System.Configuration;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Data;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase MovimientoPresentadorDetalle.</summary>
	public class MovimientoPresentadorDetalle : PresentadorDetalleBase<Movimiento>
	{
		///<summary>Variable vista de la interfaz IMovimientoDetalle.</summary>
		readonly IMovimientoDetalle vista;

		///<summary>Variable de la entidad Movimiento.</summary>
		CasoDTO casoMovimiento;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public MovimientoPresentadorDetalle(IMovimientoDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(int idmov)
		{
			try
			{
                CasoRepositorio repositorio = new CasoRepositorio(udt);
				casoMovimiento = repositorio.ObtenerMovimientoCaso(idmov);
				PresentadorAVista();
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

        public int Vercaso(int idmov) 
        {
            int? idcasoRelacionado;
            MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
            idcasoRelacionado = repositorio.ValidarMovimientoTipoPasoServicio(idmov);
            if (idcasoRelacionado !=null)
                 return idcasoRelacionado.Value;
           else return 0;
        }
      

		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{

                if (!string.IsNullOrEmpty(casoMovimiento.IdServicio.ToString()) & !string.IsNullOrEmpty(casoMovimiento.EstatusCaso.ToString()) & !string.IsNullOrEmpty(casoMovimiento.EstatusMovimiento.ToString()))
                {
                    BuscarDatos();
                }
                this.vista.IdMovimiento = string.Format("{0:N0}", this.casoMovimiento.Movimiento);
			    this.vista.IdCaso= string.Format("{0:N0}",this.casoMovimiento.Caso);			                
                this.vista.Version = string.Format("{0:N0}", this.casoMovimiento.Version);
			    this.vista.TipoMovimiento= string.Format("{0:N0}",this.casoMovimiento.TipoMovimiento);
              
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
        /// <summary>
        /// Metodo que busca a traves del servicio parametrizador valores correspondientes al nombre del servicio y los estatus 
        /// </summary>
        public void BuscarDatos()
        {
            ServicioParametrizadorClient service = new ServicioParametrizadorClient();
            try
            {
                //BUSCANDO SERVICIO
                DataSet ds1 = service.ObtenerServicioSuscriptorPorId(casoMovimiento.IdServicio);
                if (ds1.Tables[@"Error"] != null)
                    throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0][1].ToString()))
                    {
                        vista.IdServicio = ds1.Tables[0].Rows[0][1].ToString();
                    }
                }
                //BUSCANDO ESTATUS CASO
                DataSet ds2 = service.ObtenerNombreValorPorID(casoMovimiento.EstatusCaso);
                if (ds2.Tables[@"Error"] != null)
                    throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds2.Tables[0].Rows[0][1].ToString()))
                    {
                        vista.EstatusCaso = ds2.Tables[0].Rows[0][1].ToString();
                    }
                }
                //BUSCANDO ESTATUS MOVIMIENTO
                DataSet ds3 = service.ObtenerNombreValorPorID(casoMovimiento.EstatusMovimiento);
                if (ds3.Tables[@"Error"] != null)
                    throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds3.Tables[0].Rows.Count > 0) 
                {
                    if (!string.IsNullOrEmpty(ds3.Tables[0].Rows[0][1].ToString()))
                    {
                    vista.EstatusMovimiento = ds3.Tables[0].Rows[0][1].ToString();
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
                if (service.State != CommunicationState.Closed)
                    service.Close();
            }
        }

       
		
	
	}
}