using System;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase MovimientoPresentadorDetalle.</summary>
	public class AuditoriaTabMovimientoPresentadorDetalle : PresentadorDetalleBase<Movimiento>
	{
		///<summary>Variable vista de la interfaz IMovimientoDetalle.</summary>
		readonly IAuditoriaTabMovimiento Vista;

		///<summary>Variable de la entidad Movimiento.</summary>
		MovimientoDTO _Movimiento;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public AuditoriaTabMovimientoPresentadorDetalle(IAuditoriaTabMovimiento vista)
		{
			this.Vista = vista;
			this.Vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(int idmov)
		{
			try
			{
				MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
				_Movimiento = repositorio.ObtenerAuditoria(idmov);
				PresentadorAVista();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}


		public bool VerificoMovimiento(int idmov)
		{
			return true;
		}
		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{
				this.Vista.CreadoPor = this.ObtenerNombreUsuario(_Movimiento.CreadoPor);
				this.Vista.FechaCreacion = _Movimiento.FechaCreacion.ToString();
				this.Vista.ModificadoPor = this.ObtenerNombreUsuario(_Movimiento.ModificadoPor);
				this.Vista.FechaModificacion = _Movimiento.FechaModificacion.ToString();
				this.Vista.FechaOmision = _Movimiento.FechaOmision.ToString();
				this.Vista.FechaEjecucion = _Movimiento.FechaEjecucion.ToString();

			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}