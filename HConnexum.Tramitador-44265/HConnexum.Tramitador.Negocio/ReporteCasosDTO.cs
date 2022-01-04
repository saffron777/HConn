using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class ReporteCasosDTO
	{
		public string Suscriptor{ get; set; }
		public string Sucursal { get; set; }
		public int IdSucursal { get; set; }
		public string Servicio { get; set; }
		public int IdServicio { get; set; }
		public string Poliza { get; set; }
		public int? Certificado { get; set; }
		public string CIBeneficiario { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public int NCaso { get; set; }
		public string Estatus { get; set; }
		public DateTime FechaCaso { get; set; }
		public DateTime? FechaEstatus { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
	}
}
