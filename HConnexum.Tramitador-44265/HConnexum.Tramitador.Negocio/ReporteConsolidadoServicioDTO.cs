using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Negocio
{
	public class ReporteConsolidadoServicioDTO
	{
		public string Suscriptor { get; set; }
		public string Sucursal { get; set; }
		public int IdSucursal { get; set; }
		public string Servicio { get; set; }
		public int IdServicio { get; set; }
		public string Estatus { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
	}
}
