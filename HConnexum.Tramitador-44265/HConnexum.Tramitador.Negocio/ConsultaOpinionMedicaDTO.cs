using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Negocio
{
    public class ConsultaOpinionMedicaDTO
    {
        public int Id { get; set; }
        public string XMLRespuesta { get; set; }
        public string Diagnostico { get; set; }
        public bool IndEliminado { get; set; }
        public string Fechasolicitud { get; set; }
        public string nomAseg { get; set; }
        public int SuscriptorFlujoServicio { get; set; }
        public int IdMovimiento { get; set; }
        public int IdCaso { get; set; }
        public string IdEncriptado { get; set; }
        public int? SuscriptorMovimiento { get; set; }
        public string SupportIncident { get; set; }
        public string NumDocSolicitante { get; set; }
		public DateTime FechaCreacion { get; set; }
    }
}
