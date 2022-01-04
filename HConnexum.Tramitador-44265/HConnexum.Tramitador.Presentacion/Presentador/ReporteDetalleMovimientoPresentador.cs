using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ReporteDetalleMovimientoPresentador: PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IReporteDetalleMovimiento vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public ReporteDetalleMovimientoPresentador(IReporteDetalleMovimiento vista)
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

		public bool IndicadorDueñoFlujoServicio(int idSuscriptor)
		{
			SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
			return suscriptorRepositorio.IndicarDuenoFlujoServicio(idSuscriptor);
		}

		public IEnumerable<ReporteMovimientoDTO> GenerarConsultaReporte(int? idSuscriptor, int? idSucursal, int? idServicio, int? idarea, int? idproveedor, int? idusuario, int? idpais, int? iddivterr1, int? iddivterr2, int? iddivterr3, DateTime? fechaDesde, DateTime? fechaHasta, bool indDueñoFljSrv)
		{
			CasoRepositorio consultaReporte = new CasoRepositorio(udt);
			return consultaReporte.ReporteMovimiento(idSuscriptor, idSucursal, idServicio, idarea, idproveedor, idusuario, idpais, iddivterr1, iddivterr2, iddivterr3, fechaDesde, fechaHasta, indDueñoFljSrv);
		}
	}
}
