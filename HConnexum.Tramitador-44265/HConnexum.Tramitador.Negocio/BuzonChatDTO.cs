using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase BuzonChatDTO.</summary>
	public class BuzonChatDTO
	{
		public int Id { get; set; }
		public string Mensaje { get; set; }
		public bool IndLeido { get; set; }
		public int IdCaso { get; set; }
		public int? IdMovimiento { get; set; }
		public int? IdSuscriptorEnvio { get; set; }
		public string NombreSuscriptorEnvio { get; set; }
		public string Remitente { get; set; }
		public int? IdSuscriptorRecibe { get; set; }
        public string NombreSuscriptorRecibe { get; set; }
		public string LeidoPor { get; set; }
		public int? CreadoPor { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime? FechaValidacion { get; set; }
		public bool? IndValido { get; set; }
		public bool? IndEliminado { get; set; }
	}
}