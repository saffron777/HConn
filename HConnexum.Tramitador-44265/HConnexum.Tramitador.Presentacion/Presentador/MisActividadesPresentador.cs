using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Configuration;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase MisActividadesPresentador.</summary>
	public class MisActividadesPresentador : PresentadorDetalleBase<Movimiento>
	{
		///<summary>Variable vista de la interfaz IMisActividades.</summary>
		readonly IMisActividades vista;
		int[] actividades = new int[2];
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public MisActividadesPresentador(IMisActividades vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
			actividades = ObtenerListaSiguienteActividad();
		}

		public int[] ObtenerListaSiguienteActividad()
		{
			ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
			try
			{
				int idAplicacion = int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
				foreach(HConnexum.Seguridad.RolesUsuario Rol in vista.UsuarioActual.AplicacionActual(idAplicacion).Roles)
					if(Rol.NombreRol == ConfigurationManager.AppSettings[@"RolSimulaProveedor"].ToString())
					{
						vista.BIndSimulado = true;
						break;
					}
				string FiltrosBusqueda = " WHERE (TB_CasosExternos.IdSuscriptorProv = " + vista.UsuarioActual.SuscriptorSeleccionado.Id + " OR TB_CasosExternos.IdSuscriptor = " + vista.UsuarioActual.SuscriptorSeleccionado.Id + ")";
				FiltrosBusqueda += " AND TB_CasosExternos.IdEstatusMovimiento = 154 ";
				return servicio.ObtenerListaSiguienteActividad(vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado, vista.UsuarioActual.SuscriptorSeleccionado.Id, vista.BIndSimulado, "", 1, 10, FiltrosBusqueda);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return null;
		}

		///<summary>Método encargado de paginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				var tabBuzonChat = udt.Sesion.CreateObjectSet<BuzonChat>();
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				DataSet ds = servicio.ObtenerMovPorUsuarioAsignado(vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado, vista.UsuarioActual.SuscriptorSeleccionado.Id, vista.BIndSimulado);
				if(ds.Tables.Count > 0)
				{
					IList<MovimientoDTO> listaMovimientos = (from l in ds.Tables[0].AsEnumerable()
															 select new MovimientoDTO
															 {
																 Id = int.Parse(l.Field<int>("Id").ToString()),
																 IdCaso = int.Parse(l.Field<int>("IdCaso").ToString()),
																 IdServiciosuscriptor = int.Parse(l.Field<int>("IdServiciosuscriptor").ToString()),
																 Solicitante = l.Field<string>("Solicitante").ToString(),
																 Movimiento = l.Field<string>("Movimiento").ToString(),
																 FechaCreacion = l.Field<DateTime>("FechaCreacion"),
																 SLAToleranciaPaso = int.Parse(l.Field<int>("SLAToleranciaPaso").ToString()),
																 NombreEstatusMovimiento = l.Field<string>("NombreEstatusMovimiento").ToString(),
                                                                 NombreServicioSuscriptor=l.Field<string>("NombreServicioSuscriptor").ToString(),
																 CasoRespuestaXML = l.Field<string>("XMLRespuesta").ToString()
															 }).ToList();
					vista.DatosMovimientos = listaMovimientos;
					if(listaMovimientos.Count() > 0)
					{
						foreach(var item in listaMovimientos)
						{
							var buzonChat = tabBuzonChat.Where(bc => bc.IdMovimiento == item.Id && bc.IdCaso == item.IdCaso);
							if(buzonChat != null && buzonChat.Count() != 0)
								item.indChat = buzonChat.Any(bc => bc.IndLeido == false && bc.IdSuscriptorEnvio != vista.UsuarioActual.SuscriptorSeleccionado.Id);
							if(item.indChat == true) //chat No pendiente
								item.ImgChat = "P";
							else if(item.indChat == false)//chat pendiente
								item.ImgChat = "NP";
							else
								item.ImgChat = "NoImage";
							item.Solicitante = ObtenerAsegurado(item.CasoRespuestaXML).PadRight(120, ' ');
							item.Intermediario = ObtenerIntermediario(item.CasoRespuestaXML).PadRight(120, ' ');
						}
                        vista.DatosGrid = listaMovimientos.AsEnumerable();
                     
                        
					}
					vista.NombreUsuario = vista.UsuarioActual.DatosBase.Nombre1 + " " + vista.UsuarioActual.DatosBase.Apellido1;
					vista.NombreSuscriptor = vista.UsuarioActual.SuscriptorSeleccionado.Nombre;
				}
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
        public void CargoGrid() 
        {
        
        
        }
       
		/// <summary>Obtiene los datos del asegurado desde la respuesta (en XML) del caso.</summary>
		/// <param name="respuestaXML">Respuesta del caso a consultar.</param>
		/// <returns>Datos del asegurado.</returns>
		private string ObtenerAsegurado(string respuestaXml)
		{
			try
			{
				string asegurado = string.Format("{0}", new CasoRespuestaXML(respuestaXml).ObtenerParametroValor("NOMASEG") ?? string.Empty);
				if(string.IsNullOrEmpty(asegurado))
					asegurado = string.Format("{0}", new CasoRespuestaXML(respuestaXml).ObtenerParametroValor("NOMCOMPLETOBENEF") ?? string.Empty);
				return asegurado;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return null;
		}

		private string ObtenerIntermediario(string respuestaXml)
		{
			try
			{
				string intermediario = string.Format("{0}", new CasoRespuestaXML(respuestaXml).ObtenerParametroValor("INTERMEDIARIO") ?? string.Empty);
				if(string.IsNullOrEmpty(intermediario))
					intermediario = string.Format("{0}", new CasoRespuestaXML(respuestaXml).ObtenerParametroValor("NOMSUSINTERMED") ?? string.Empty);
				return intermediario;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return null;
		}

		public int[] NumeroActividadesPendientes()
		{
			CasoRepositorio repositorio = new CasoRepositorio(udt);
			return repositorio.BuscarNumeroActividadesPendientes(vista.UsuarioActual.SuscriptorSeleccionado.Id, actividades);
		}
	}
}
