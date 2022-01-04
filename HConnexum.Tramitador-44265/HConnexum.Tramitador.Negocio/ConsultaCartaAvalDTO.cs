using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class ConsultaCartaAvalDTO
	{
		public int Id { get; set; }

		public string XMLRespuesta { get; set; }

		public string Diagnostico { get; set; }

		public bool IndEliminado { get; set; }

		public DateTime Fechasolicitud { get; set; }

		public string nomAseg { get; set; }

		public int SuscriptorFlujoServicio { get; set; }

		public int IdMovimiento { get; set; }

		public int IdCaso { get; set; }

		public string IdEncriptado { get; set; }

		public int? SuscriptorMovimiento { get; set; }

		public string SupportIncident { get; set; }

		public string NumDocSolicitante { get; set; }

		public string SuscriptorNombre { get; set; }

		public string ActivacionCA { get; set; }
        public string Intermediario { get; set; }
        public string TipoMov { get; set; }
	}
}