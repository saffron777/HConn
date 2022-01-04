using System;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class DatosGeneralesPresentador : BloquesPresentadorBase
	{
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		readonly IDatosGenerales vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public DatosGeneralesPresentador(IDatosGenerales vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de buscar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				if(String.IsNullOrWhiteSpace(vista.PServicio))
				{
					if(!string.IsNullOrWhiteSpace(vista.PIdCaso))
					{
						CasoRepositorio casoRepositorio = new CasoRepositorio(udt);
						CasoDTO caso = casoRepositorio.CasoPorID(int.Parse(vista.PIdCaso));
						FlujosServicioRepositorio flujosServicioRepositorio = new FlujosServicioRepositorio(udt);
						FlujosServicioDTO flujo = flujosServicioRepositorio.ObtenerDtoFlujosServicioPorId(caso.IdFlujoServicio);
						vista.PServicio = flujo.NombreServicioSuscriptor;
					}
				}
				if(string.IsNullOrWhiteSpace(vista.PServicio))
				{
					Movimiento movimiento = ObtenerMovimiento(vista.IdMovimiento);
					if(movimiento != null)
					{
						int? idServicioSuscriptor = movimiento.Caso1.Solicitud.IdServicioSuscriptor;
						if(idServicioSuscriptor != null)
						{
							ServiciosSuscriptorRepositorio serviciosSuscriptorRepositorio = new ServiciosSuscriptorRepositorio(udt);
							ServiciosSuscriptor serviciosSuscriptor = serviciosSuscriptorRepositorio.ObtenerPorId(movimiento.Caso1.Solicitud.IdServicioSuscriptor.Value);
							vista.PServicio = serviciosSuscriptor.Nombre;
						}
					}
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

		///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>
		public void ValidarDatos()
		{
			StringBuilder errores = new StringBuilder();
			try
			{
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			vista.Errores = errores.ToString();
		}
	}
}