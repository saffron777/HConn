using System;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Datos;
using System.Collections.Generic;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ReporteCasosPresentadorDetalle: PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IReporteCasos vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public ReporteCasosPresentadorDetalle(IReporteCasos vista)
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

		public IEnumerable<ReporteCasosDTO> GenerarConsultaReporte(int? idSuscriptor, int? idSucursal, int? idServicio, string poliza, int? numCertificado, string docbeneficiario, DateTime? fechaDesde, DateTime? fechaHasta)
		{
			CasoRepositorio consultaReporte = new CasoRepositorio(udt);
			return consultaReporte.ReporteCasos(idSuscriptor, idSucursal, idServicio, poliza, numCertificado, docbeneficiario, fechaDesde, fechaHasta);
		}
	}
}
