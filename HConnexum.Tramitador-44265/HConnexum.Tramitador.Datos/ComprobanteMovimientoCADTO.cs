using System;
using System.Linq;

namespace HConnexum.Tramitador.Datos
{
	public class ComprobanteMovimientoCADTO
	{
		public string Logo { get; set; }
		public DatosMovimientoDTO DatosDetalle { get; set; }
		public DatosTitularMovCADTO DatosTitular { get; set; }
		public DatosCartaAvalDTO DatosCartaAval { get; set; }
		public string PacienteAsegurado { get; set; }
		public string CedulaAsegurado { get; set; }
		public string SexoPaciente { get; set; }
		public DateTime? FechaNacPaciente { get; set; }
		public string EstadoPaciente { get; set; }
		public string Parentesco { get; set; }
		public string ContratantePoliza { get; set; }
		public string CompaniaAseguradora { get; set; }
		public int? Poliza { get; set; }
		public int? Certificado { get; set; }
		public DateTime? FechaDesde { get; set; }
		public DateTime? FechaHasta { get; set; }
		public string Estado { get; set; }
		public string SupportIncident { get; set; }
	}
	public class DatosMovimientoDTO
	{
		public string UltiMovHecho { get; set; }
		public string Responsable { get; set; }
		public double? MontoFacturado { get; set; }
		public double? GastosNoCubiertos { get; set; }
		public double? MontoDeducible { get; set; }
		public double? MontoCubierto { get; set; }
		public int? DiasHosp { get; set; }
		public string ObservacionesProcesadas { get; set; }
		public string DocumentosFaxSolicitados { get; set; }
		public string TipoMovimiento { get; set; }
		public string Sintomas { get; set; }
		public DateTime? FechaOcurrencia { get; set; }
		public DateTime? FechaMovimiento { get; set; }
		public DateTime? HoraRegistro { get; set; }
		public string Resultado { get; set; }
		public string Observaciones { get; set; }
	}
	public class DatosTitularMovCADTO
	{
		public string TitularAsegurado { get; set; }
		public string CedulaTitular { get; set; }
		public string SexoTitular { get; set; }
		public DateTime? FechaNacTitular { get; set; }
		public string EstadoTitular { get; set; }
	}
	public class DatosCartaAvalDTO
	{
		public DateTime? FechaEmision { get; set; }
		public DateTime? FechaSolicitud { get; set; }
		public DateTime? FechaVencimiento { get; set; }
		public string Medico { get; set; }
		public string Diagnostico { get; set; }
		public string Procedimiento { get; set; }
		public double? Presupuesto { get; set; }
		public string ObservacionesCA { get; set; }
	}
}
