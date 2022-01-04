using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class DetalleFacturaDTO
	{
		public string Logo { get; set; }
		public int Reclamo { get; set; }
		public DateTime? FechaPago { get; set; }
		public double? TotalRetencion { get; set; }
		public string Status { get; set; }
		public string StatusGrid { get; set; }
		public string NroControl { get; set; }
		public DateTime? FechaRecepcionFactura { get; set; }
		public DateTime? FechaEmisionFactura { get; set; }
		public double? TotalSujetoRet { get; set; }
		public double? MontoImpMunicipal { get; set; }
		public string BeneficiarioProveedor { get; set; }
		public string RifProveedor { get; set; }
		public int Expediente { get; set; }
		public string NroReclamo { get; set; }
		public string A02NoDocasegurado { get; set; }
		public string Factura { get; set; }
		public double? RetIsrl { get; set; }
		public double? TotalImp { get; set; }
		public double? MontoCubierto { get; set; }
		public double? MontoCubiertoGrid { get; set; }
		public double? ImpIva { get; set;}
		public int CodigoBarra { get; set; }
		public Decimal? MontoPagoSap { get; set; }
		public string NumOrden { get; set; }
		public string NumReferencia { get; set; }
	}

	public class DetalleFacturaGridDTO
	{
		public string NReclamosEncriptado { get; set; }
		public string NroReclamo { get; set; }
		public string Reclamo { get; set; }
		public string Status { get; set; }
		public string DocAsegurado { get; set; }
		public string Factura { get; set; }
		public DateTime? FechaPago { get; set; }
		public double? Monto { get; set; }
		public Decimal? MontoPagoSap { get; set; }
		public string NumOrden { get; set; }
		public string NumReferencia { get; set; }
	}

}