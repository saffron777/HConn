using System;
using System.Linq;
namespace HConnexum.Tramitador.Negocio
{
	public class FacturasDTO
	{		
		public string NFactura { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public double? MontoCubierto { get; set; }
		public DateTime? FechaPago { get; set; }
		public double? MontoPagar { get; set; }
		public string Estatus { get; set; }
		public string Logo { get; set; }
	}

	public class FacturasGridDTO
	{		
		public string NFacturaEncriptado { get; set; }
		public string NFactura { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaPago { get; set; }
		public string Estatus { get; set; }
		public double? MontoPagar { get; set; }
	}
}
