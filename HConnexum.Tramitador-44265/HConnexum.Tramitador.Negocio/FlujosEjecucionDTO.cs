using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase FlujosEjecucionDTO.</summary>
    public class FlujosEjecucionDTO
	{
	public int Id { get; set; }
		public int IdPasoOrigen { get; set; }
		public int? IdPasoDestino { get; set; }
        public int IdPasoRespuesta { get; set; }
		public int? Condicion { get; set; }
        public string TipoProximoPaso { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModicacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string PasoOrigen { get; set; }
        public string Respuesta { get; set; }
        public string PasoDestino { get; set; }
        public int? IdPasoDesborde { get; set; }
        public string IdEncriptado { get; set; }
	}
}
