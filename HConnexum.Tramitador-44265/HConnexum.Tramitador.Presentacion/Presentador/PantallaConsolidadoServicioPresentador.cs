using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Datos;
using System.Web.Configuration;
using System.Data;
using System.Web;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PantallaConsolidadoServicioPresentador : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IPantallaConsolidadoservicioPresentador.</summary>
		readonly IPantallaConsolidadoServicio vista;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public PantallaConsolidadoServicioPresentador(IPantallaConsolidadoServicio vista)
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

	}
}
