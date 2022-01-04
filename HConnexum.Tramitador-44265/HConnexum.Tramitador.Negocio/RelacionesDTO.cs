using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class RelacionesDTO
	{
		public int? RelacionReclamo { get; set; }
		public string NRemesa { get; set; }
		public DateTime? FechaCierre { get; set; }
		public int? NCasos { get; set; }
		public double? MontoCubierto { get; set; }
		public double? Retenido { get; set; }
		public double? ImpMunicipal { get; set; }
		public double? MontoPagar { get; set; }
		public string Logo { get; set; }
	}

	public class RelacionesGridDTO
	{
		public string NRemesaEncriptado { get; set; }
		public int? RelacionReclamo { get; set; }
		public string NRemesa { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public string Estatus { get; set; }
		public DateTime? FechaPago { get; set; }
		public double? MontoPagar { get; set; }
	}

}
