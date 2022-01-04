using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class DatosSolicitudDTO
	{
		public string Logo { get; set; }
		public string PacienteAsegurado { get; set; }
		public string CedulaAsegurado { get; set; }
		public string SexoPaciente { get; set; }
		public DateTime? FechaNacPaciente { get; set; }
		public string EstadoPaciente { get; set; }
		public string Parentesco { get; set; }
		public string ContratantePoliza { get; set; }
		public string Poliza { get; set; }
		public int? Certificado { get; set; }
		public DateTime? FechaDesde { get; set; }
		public DateTime? FechaHasta { get; set; }
		public string ContratanteSolicitud { get; set; }
		public string Proveedor { get; set; }
		public string Diagnostico { get; set; }
		public string CodClave { get; set; }
		public DateTime? FechaOcurrencia { get; set; }
		public DateTime? FechaNotificacion { get; set; }
		public DateTime? FechaLiquidadoEsperaPago { get; set; }
		public DateTime? FechaEmisionFactura { get; set; }
		public DateTime? FechaRecepcionFactura { get; set; }
		public string Status { get; set; }
		public string SubCategoria { get; set; }
		public string NumeroControl { get; set; }
		public string NumeroFactura { get; set; }
		public string NumeroPoliza { get; set; }
		public int? CertificadoSolicitud { get; set; }
		public double? MontoPresupuestoIncial { get; set; }
		public double? Deducible { get; set; }
		public double? MontoCubierto { get; set; }
		public double? GastosnoCubiertos { get; set; }
		public double? MontoSujetoRetencion { get; set; }
		public double? GastosClinicos { get; set; }
		public double? GastosMedicos { get; set; }
		public double? PorcentajeRetencion { get; set; }
		public double? Retencion { get; set; }
		public string ParentescoSolicitud { get; set; }
		public string Liquidador { get; set; }
		public DatosTitularDTO DatosTitular { get; set; }
	}
}