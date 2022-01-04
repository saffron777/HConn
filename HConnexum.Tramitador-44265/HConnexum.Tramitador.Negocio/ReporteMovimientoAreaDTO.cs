using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class ReporteMovimientoAreaDTO
	{
		public string NombreArea { get; set; }
		public int IdArea { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
	}
}
