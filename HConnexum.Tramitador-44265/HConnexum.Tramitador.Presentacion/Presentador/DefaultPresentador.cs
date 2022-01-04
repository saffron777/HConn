using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HConnexum.Tramitador.Datos;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class DefaultPresentador : PresentadorListaBase<PaginasModulo>
	{
		readonly IDefault vista;

		public DefaultPresentador(IDefault vista)
		{
			this.vista = vista;
		}

		public void MostrarVista(int IdAplicacion)
		{
			ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();
			try
			{
				DataSet ds = servicio.ObtenerMenu(IdAplicacion.ToString().Encriptar(), vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar());
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				this.vista.Menu = ds.Tables[0];
                if (vista.UsuarioActual.SuscriptorSeleccionado.Nombre == vista.UsuarioActual.Sucursal.Nombre)
                {
                    this.vista.tbUsuario = vista.UsuarioActual.DatosBase.Nombre1 + " " + vista.UsuarioActual.DatosBase.Apellido1;
                    this.vista.tbSuscritor = vista.UsuarioActual.SuscriptorSeleccionado.Nombre + " - ";
                  
                }
                else
                {
                    
                    this.vista.tbUsuario = vista.UsuarioActual.DatosBase.Nombre1 + " " + vista.UsuarioActual.DatosBase.Apellido1;
                    this.vista.tbSuscritor = "<b>Suscriptor:</b> " + vista.UsuarioActual.SuscriptorSeleccionado.Nombre + " ";
                    this.vista.tbSucursal = "<b>Sucursal:</b> " + vista.UsuarioActual.Sucursal.Nombre + " - ";
                }
               

			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
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
