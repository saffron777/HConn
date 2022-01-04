using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Data;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase CasoPresentadorMaestroDetalle.</summary>
	public class CasoMovimientosAuditoriaPresentadorMaestroDetalle : PresentadorListaBase<Caso>
	{
		///<summary>Variable vista de la interfaz ICasoMaestroDetalle.</summary>
        readonly ICasoMovimientosAuditoriaMaestroDetalle vista;

		///<summary>Variable de la entidad Caso.</summary>
		Caso _Caso;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
        
		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public CasoMovimientosAuditoriaPresentadorMaestroDetalle(ICasoMovimientosAuditoriaMaestroDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro)
		{
			try
			{
				this.MostrarVista();
				MovimientoRepositorio repositorioDetalle = new MovimientoRepositorio(unidadDeTrabajo);
                IEnumerable<MovimientoDTO> datos = repositorioDetalle.ObtenerMovimientosDTO(orden, pagina, tamañoPagina, parametrosFiltro, int.Parse(vista.IdCaso));
				this.vista.NumeroDeRegistros = repositorioDetalle.Conteo;
				this.vista.Datos = datos;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}


		public void AnularSuspenderCasosMovimientos(int IdUsuario, string Observaciones)
		{

            ServicioOrquestadorClient Servicio = new ServicioOrquestadorClient();
			try
			{

                bool t1 = Servicio.AnularSuspenderCasos(int.Parse(vista.IdCaso), IdUsuario, Observaciones);

			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

        public void ReanudarMovimientosMovimientos(int IdUsuario)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {

                bool t1 = servicio.ReanudarMovimiento(vista.Id, IdUsuario);

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
		public void Eliminar(IList<string> ids)
		{
			try
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				MovimientoRepositorio repositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					Movimiento _Movimiento = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.EliminarLogico(_Movimiento, idDesencriptado))
						this.vista.Errores = "Ya Existen registros asociados a la entidad _Movimiento";
				}
				this.unidadDeTrabajo.Commit();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
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
				CasoDTO = repositorio.CasoPorIDMovimiento(vista.Id);
				if(CasoDTO != null)
				{
				
                    Idestatus = CasoDTO.Estatus;
					IdServicioSuscriptor = CasoDTO.IdServiciosuscriptor;
					Idsuscriptor = CasoDTO.IdSuscriptor;
					IdPrioridad = CasoDTO.PrioridadAtencion;

					LlamaServicio(Idestatus, IdServicioSuscriptor, Idsuscriptor, IdPrioridad);
					this.vista.caso = CasoDTO.Id.ToString();
					this.vista.IdSolicitud = CasoDTO.IdSolicitud.ToString();
					this.vista.FechaSolicitud = CasoDTO.FechaSolicitud.ToString();
					this.vista.FechaAnulacion = CasoDTO.FechaAnulacion.ToString();
					this.vista.FechaRechazo = CasoDTO.FechaRechazo.ToString();

					this.vista.FechaCreacion2 = CasoDTO.FechaCreacion.ToString();
					this.vista.CreadorPor = ObtenerNombreUsuario(CasoDTO.CreadorPor);
					this.vista.version = CasoDTO.Version.ToString();
                    this.vista.Movimiento = CasoDTO.NombreMovimiento.ToString();
                    this.vista.IdCaso = CasoDTO.Id.ToString();
					if(!string.IsNullOrEmpty(CasoDTO.ModificadoPor.ToString()))
						this.vista.Modificado = ObtenerNombreUsuario(CasoDTO.ModificadoPor);

				}
			}
			catch(Exception ex)
			{
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void LlamaServicio(int Idestatus, int IdServicioSuscriptor, int Idsuscriptor, int? IdPrioridad)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{


				DataSet ds1 = servicio.ObtenerSuscriptorPorIDCompleto(Idsuscriptor);
				if(ds1.Tables[@"Error"] != null)
					throw new Exception(ds1.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds1.Tables[0].Rows.Count > 0)
				{
					vista.Suscriptor = ds1.Tables[0].Rows[0]["Nombre"].ToString();
					vista.NumDoc = ds1.Tables[0].Rows[0]["NumeroDoc"].ToString();
					vista.TipoDoc = ds1.Tables[0].Rows[0]["TipoDoc"].ToString();
				}
				DataSet ds2 = servicio.ObtenerServicioSuscriptorPorId(IdServicioSuscriptor);
				if(ds2.Tables[@"Error"] != null)
					throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds2.Tables[0].Rows.Count > 0)
					vista.Servicio = ds2.Tables[0].Rows[0]["Nombre"].ToString();

				DataSet ds3 = servicio.ObtenerNombreValorPorID(Idestatus);
				if(ds3.Tables[@"Error"] != null)
					throw new Exception(ds3.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds3.Tables[0].Rows.Count > 0)
				{
					vista.Estatus = ds3.Tables[0].Rows[0]["NombreValor"].ToString();
				}

				if(IdPrioridad != null)
				{
					DataSet ds4 = servicio.ObtenerNombreValorPorID((int)IdPrioridad);
					if(ds4.Tables[@"Error"] != null)
						throw new Exception(ds4.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if(ds4.Tables[0].Rows.Count > 0)
					{
						vista.PrioridadAtencion = ds4.Tables[0].Rows[0]["NombreValor"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}

		}

	}
}
