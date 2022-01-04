using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ReporteConsolidadoServicioPresentador: PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCosilidadoServicio.</summary>
		readonly IReporteConsolidadoServicio vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public ReporteConsolidadoServicioPresentador(IReporteConsolidadoServicio vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		public string ObtenerUsuarioActual()
		{
			string nombre;
			nombre = vista.UsuarioActual.DatosBase.Nombre1.ToString() + " " + vista.UsuarioActual.DatosBase.Apellido1.ToString();
			return nombre;
		}

		public IEnumerable<ReporteConsolidadoServicioDTO> GenerarConsultaReporte(int? idSuscriptor, int? idSucursal, int? idServicio, DateTime? fechaDesde, DateTime? fechaHasta)
		{
			CasoRepositorio consultaReporte = new CasoRepositorio(udt);
			return consultaReporte.ReporteConsolidadoServicio(idSuscriptor, idSucursal, idServicio, fechaDesde, fechaHasta);
		}
	}
}
