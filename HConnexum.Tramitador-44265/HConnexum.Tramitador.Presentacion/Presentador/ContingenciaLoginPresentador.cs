using System.Configuration;
using System.Data;
using System.ServiceModel;
using HConnexum.Tramitador.Datos;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System;
using System.Web;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ContingenciaLoginPresentador 
	{
		readonly IContingenciaLogin vista;

		UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public ContingenciaLoginPresentador(IContingenciaLogin vista)
		{
			this.vista = vista;
		}

		public bool CargarRoles(int pIdAplicacion)
		{
			ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();
			try
			{
                HttpContext.Current.Trace.Warn(@"IdUsuarioSuscriptorSeleccionado: [" + vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado + @"], IdAplicacion: [" + pIdAplicacion.ToString() + "]");
				DataSet ds = servicio.ObtenerRolesUsuarioPorAplicacion(vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar(), pIdAplicacion.ToString().Encriptar());
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				HttpContext.Current.Trace.Warn(@"Cantidad de Roles: [" + ds.Tables[0].Rows.Count + @"]");
				if(ds.Tables[0].Rows.Count > 0)
				{
					vista.UsuarioActual.ActualizarRolesPorAplicacion(pIdAplicacion, ds);
					return true;
				}
				else
				{
					vista.Errores = @"No posee roles en esta aplicación";
					return false;
				}
			}
			catch(Exception e)
			{
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                if (e.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
                return false;
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}
      
        public string AppOrigen(string Aplicacion)
        {
            ListasValorRepositorio repositorio = new ListasValorRepositorio(udt);
            string RutaOrigen = repositorio.ObtengoValorPorListaValor(Aplicacion, "Aplicaciones");
            if (!string.IsNullOrEmpty(RutaOrigen))
                return RutaOrigen;
            else
            {
                vista.Errores = "No se pudo cargar  la URL de la aplicacion Origen";
                return "";
            }
        }
	}
}
