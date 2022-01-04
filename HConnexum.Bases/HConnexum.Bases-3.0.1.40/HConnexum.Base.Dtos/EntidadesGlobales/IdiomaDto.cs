using System;

namespace HConnexum.Base.Dtos.EntidadesGlobales
{
	public class IdiomaDto
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string NombreCorto { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FecCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
	}
}