using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class ComprobanteMovimientoCEDTO
	{
		public string Logo { get; set; }
		public DatosPacienteDTO DatosPaciente { get; set; }
		public DatosTitularMovDTO DatosTitular { get; set; }
		public DatosSolicitudMovDTO DatosSolicitud { get; set; }
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
		public string BarCod { get; set; }
	}
	public class DatosPacienteDTO
	{
		public string PacienteAsegurado { get; set; }
		public string CedulaAsegurado { get; set; }
		public string SexoPaciente { get; set; }
		public DateTime? FechaNacPaciente { get; set; }
		public string EstadoPaciente { get; set; }
		public string Parentesco { get; set; }
		public string ContratantePoliza { get; set; }
		public int? Poliza { get; set; }
		public int? Certificado { get; set; }
		public DateTime? FechaDesde { get; set; }
		public DateTime? FechaHasta { get; set; }
	}
	public class DatosTitularMovDTO
	{
		public string TitularAsegurado { get; set; }
		public string CedulaTitular { get; set; }
		public string SexoTitular { get; set; }
		public DateTime? FechaNacTitular { get; set; }
		public string EstadoTitular { get; set; }
	}
	public class DatosSolicitudMovDTO
	{
		public string CodClave { get; set; }
		public string Categoria { get; set; }
		public DateTime? FechaOcurrencia { get; set; }
		public string Diagnostico { get; set; }
		public string Clinica { get; set; }
		public double? MontoFactura { get; set; }
		public double? MontoCubierto { get; set; }
		public double? GastosnoCubiertos { get; set; }
	}
}
