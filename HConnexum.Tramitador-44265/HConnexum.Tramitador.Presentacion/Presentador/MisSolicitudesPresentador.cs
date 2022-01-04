using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Collections.Generic;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase MisSolicitudesPresentador.</summary>
	public class MisSolicitudesPresentador : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IMisSolicitudes.</summary>
		readonly IMisSolicitudes vista;

		///<summary>Variable de la entidad Caso.</summary>
		Caso Caso;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public MisSolicitudesPresentador(IMisSolicitudes vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de paginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				CasoRepositorio repositorioCaso = new CasoRepositorio(udt);
				IList<CasoDTO> listaCasos = new List<CasoDTO>();
				listaCasos = repositorioCaso.ObtenerCasosPorIDCreacion(vista.Id).ToList();
				vista.DatosCasos = listaCasos.OrderBy(x => x.FechaCreacion).ToList();
				vista.DatosFlujos = ConstruirListaFlujos(listaCasos).OrderBy(x => x.FechaCreacion).ToList();

				vista.NombreUsuario = vista.UsuarioActual.DatosBase.Nombre1 + " " + vista.UsuarioActual.DatosBase.Apellido1;
				vista.NombreSuscriptor = vista.UsuarioActual.SuscriptorSeleccionado.Nombre;
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

		private IList<FlujosServicioDTO> ConstruirListaFlujos(IList<CasoDTO> listaCasos)
		{
			DataTable listadoIdSubServicio = null;
			listadoIdSubServicio = BuscarNombreServicios();
			IList<FlujosServicioDTO> listadoFlujoServicioA = new List<FlujosServicioDTO>();
			IList<int> idsFS = new List<int>();

			listaCasos.OrderBy(x => x.FechaCreacion);
			idsFS = listaCasos.Select(a => a.IdServiciosuscriptor).Distinct().ToList();

			for(int i = 0; i < idsFS.Count(); i++)
			{
				int j;
				bool find = false;
				for(j = 0; j < listadoIdSubServicio.Rows.Count && find == false; j++)
					if(idsFS[i] == int.Parse(listadoIdSubServicio.Rows[j]["Id"].ToString()))
						find = true;
				listadoFlujoServicioA.Add(new FlujosServicioDTO());
				if(find == true)
				{
					listadoFlujoServicioA[i].IdServicioSuscriptor = idsFS[i];
					listadoFlujoServicioA[i].NombreServicioSuscriptor = listadoIdSubServicio.Rows[j - 1]["Nombre"].ToString();
				}
				else
					listadoFlujoServicioA[i].NombreServicioSuscriptor = "";
			}
			return listadoFlujoServicioA.OrderBy(x => x.NombreServicioSuscriptor).ToList();
		}

		private DataTable BuscarNombreServicios()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerServiciosPorIdUsuarioSuscriptor(vista.Id);
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0];
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
	}
}