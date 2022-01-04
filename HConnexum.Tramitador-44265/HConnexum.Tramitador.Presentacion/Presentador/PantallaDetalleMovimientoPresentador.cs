using System;
using System.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.ServiceModel;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Web;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PantallaDetalleMovimientoPresentador: PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IPantallaDetalleMovimiento.</summary>
		readonly IPantallaDetalleMovimiento vista;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public PantallaDetalleMovimientoPresentador(IPantallaDetalleMovimiento vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		public void CargarComboSuscriptorXUsuarioLogeado()
		{
			string[] datos = new string[2] { vista.UsuarioActual.SuscriptorSeleccionado.Nombre, vista.UsuarioActual.SuscriptorSeleccionado.Id.ToString() };
			DataTable datosSuscriptorSeleccionado = new DataTable();
			datosSuscriptorSeleccionado.Columns.Add("Nombre");
			datosSuscriptorSeleccionado.Columns.Add("Id");
			datosSuscriptorSeleccionado.Rows.Add(datos);
			vista.ComboSuscriptorXUsuarioLogeado = datosSuscriptorSeleccionado;
		}

		public bool CargarComboGrupoEmpresariales()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				if (WebConfigurationManager.AppSettings["NombreSuscriptor"].Split(';').Contains(vista.UsuarioActual.SuscriptorSeleccionado.Nombre.ToUpper()))
				{
					DataTable listadoGruposEmpresariales = null;
					DataSet ds = servicio.ObtenerGruposEmpresariales();
					if (ds.Tables[@"Error"] != null)
						throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if (ds.Tables[0].Rows.Count > 0)
						listadoGruposEmpresariales = ds.Tables[0];
					vista.GrupoEmpresarial = listadoGruposEmpresariales;
					LlenarComboSuscriptor();
					return true;
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return false;
		}

		public void LlenarComboSuscriptor()
		{
			SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
			vista.ComboSuscriptor = suscriptorRepositorio.ObtenerSuscriptoresDTO();
		}

		public void LlenarComboSusCriptorXIdGrupoEmpresarial(int idGrupoEmpresarial)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoSuscriptores = null;
				DataSet ds = servicio.ObtenerSuscriptoresXIdGrupoEmpresarial(idGrupoEmpresarial);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoSuscriptores = ds.Tables[0];
				vista.ComboSuscriptorGrupoEmpresarial = listadoSuscriptores;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboSuscursales(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoSucursales = null;
				DataSet ds = servicio.ObtenerSucursal(idSuscriptor);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoSucursales = ds.Tables[0];
				vista.ComboSucursal = listadoSucursales;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}

		}

		public void LlenarComboServiciosPorIdSuscriptor(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoServicios = null;
				DataSet ds = servicio.ObtenerServiciosPorIdSuscriptor(idSuscriptor);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoServicios = ds.Tables[0];
				vista.ComboServicio = listadoServicios;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboServiciosXIdSuscriptorXidSucursal(int idSuscriptor, int idSucursal)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoServicios = null;
				DataSet ds = servicio.ServiciosXIdSuscriptorXidSucursal(idSuscriptor, idSucursal);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoServicios = ds.Tables[0];
				vista.ComboServicio = listadoServicios;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboArea(int idSucursal)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoSucursales = null;
				DataSet ds = servicio.ObtenerAreas(idSucursal);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoSucursales = ds.Tables[0];
				vista.Area = listadoSucursales;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public bool IndicadorDueñoFlujoServicio(int idSuscriptor)
		{
			SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
			return suscriptorRepositorio.IndicarDuenoFlujoServicio(idSuscriptor);
		}

		public void LlenarComboUsuarioXIdSuscriptor(int idsuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoUsuarios = null;
				DataSet ds = servicio.ObtenerUsuariosPorIdSuscriptor(idsuscriptor);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoUsuarios = ds.Tables[0];
				vista.Usuario = listadoUsuarios;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboUsuario(int idsuscriptor, int idsucursal, int idarea)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoUsuarios = null;
				DataSet ds = servicio.ObtenerUsuariosPorLocalidad(idsuscriptor, idsucursal, idarea);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoUsuarios = ds.Tables[0];
				vista.Usuario = listadoUsuarios;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboPais()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoPaises = null;
				DataSet ds = servicio.ObtenerPaises();
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoPaises = ds.Tables[0];
				vista.Pais = listadoPaises;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboDiv1(int idPais)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoDiv1 = null;
				DataSet ds = servicio.ObtenerDivisionesTerritoriales1(idPais);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoDiv1 = ds.Tables[0];
				vista.DivTerr1 = listadoDiv1;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboDiv2(int idDiv1)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoDiv2 = null;
				DataSet ds = servicio.ObtenerDivisionesTerritoriales2(idDiv1);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoDiv2 = ds.Tables[0];
				vista.DivTerr2 = listadoDiv2;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public void LlenarComboDiv3(int idDiv2)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataTable listadoDiv3 = null;
				DataSet ds = servicio.ObtenerDivisionesTerritoriales3(idDiv2);
				if (ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if (ds.Tables[0].Rows.Count > 0)
					listadoDiv3 = ds.Tables[0];
				vista.DivTerr3 = listadoDiv3;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
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
