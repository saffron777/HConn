using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class DetalleRemesaDTO
	{
		public string Logo { get; set; }
		public int Reclamo { get; set; }
		public DateTime? RnCreateDate { get; set; }
		public DateTime? FechaPago { get; set; }
		public string Tipo { get; set; }
		public string Referencia { get; set; }
		public double? TotalRetencion { get; set; }
		public double? TotalReclamos { get; set; }
		public string Status { get; set; }
		public string OBPago { get; set; }
		public string Pagador { get; set; }
		public string NroControl { get; set; }
		public DateTime? FechaFactura { get; set; }
		public DateTime? FechaRecepcionFactura { get; set; }
		public int? NroReclamosAsociados { get; set; }
		public double? TotalFacturado { get; set; }
		public double? TotalSujetoRet { get; set; }
		public double? MontoImpMunicipal { get; set; }
		public string BeneficiarioPreoveedor { get; set; }
		public string RifProveedor { get; set; }
		public int Expediente { get; set; }
		public string NroReclamo { get; set; }
		public string A02Asegurado { get; set; }
		public string A02NoDocasegurado { get; set; }
		public string Factura { get; set; }
		public DateTime? FecOcurrencia { get; set; }
		public double? GastosNoCubiertos { get; set; }
		public double? RetIsrl { get; set; }
		public double? AhorroGastosMedicos { get; set; }
		public double? TotConDesc { get; set; }
		public double? MontoCubierto { get; set; }
		public int CodigoBarra { get; set; }
	}

	public class DetalleRemesaGridDTO
	{
		public string NReclamosEncriptado { get; set; }
		public string NroReclamo { get; set; }
		public string Reclamo { get; set; }
		public string Asegurado { get; set; }
		public string DocAsegurado { get; set; }
		public string Factura { get; set; }
		public DateTime? FechaOcurrencia { get; set; }
		public double? Monto { get; set; }
	}

}
