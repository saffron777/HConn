using System;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class PoteCasoDTO
	{
		public int IdCaso { get; set; }
		public int Id { get; set; }
		public string Ticket { get; set; }
		public int IdSuscriptor { get; set; }
		public int IdServicioSuscriptor { get; set; }
		public string NombreAsegurado { get; set; }
		public string TipDocAsegurado { get; set; }
		public string NumDocAsegurado { get; set; }
        public string NumDocTit { get; set; }
		public int IdFlujoServicio { get; set; }
		public DateTime FechaSolicitud { get; set; }
		public string NombreSuscriptor { get; set; }
		public string NombreServicioSuscriptor { get; set; }
		public bool IndEliminado { get; set; }
		public string Actividad { get; set; }
		public string XMLRespuesta { get; set; }
		public string Intermediario { get; set; }
		public string IdEncriptado { get; set; }
		public string SupportIncident { get; set; }
		public string Idcasoexterno { get; set; }
		public DateTime FechaCreacionMov { get; set; }
		public bool? indChat { get; set; }
		public string ImgChat { get; set; }
		public string OrigenesDB { get; set; }
		public string origen { get; set; }
		public string IdExp_Web { get; set; }
		public string Reclamo { get; set; }
        public int Registros { get; set; }
		public int Estatus { get; set; }
        public int total { get; set; }
        
        public string NombreEstatusCaso { get; set; }
        public int? CreadoPor { get; set; }
        public int? IdMovimiento { get; set; }
        public string NombreMovimiento { get; set; }

	}
}