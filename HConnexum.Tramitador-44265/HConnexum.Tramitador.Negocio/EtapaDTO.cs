using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase EtapaDTO.</summary>
    public class EtapaDTO
	{
	public int Id { get; set; }
		public int IdFlujoServicio { get; set; }
        public int IdSuscriptor { get; set; }
        public int IdServicioSuscriptor { get; set; }
        public int VersionFlujoServicio { get; set; }
        public bool IndVigenteFlujoServicio { get; set; }
		public string Nombre { get; set; }
		public int Orden { get; set; }
		public int SlaPromedio { get; set; }
		public int SlaTolerancia { get; set; }
		public bool IndObligatorio { get; set; }
		public bool IndRepeticion { get; set; }
		public bool IndSeguimiento { get; set; }
		public bool IndInicioServ { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModicacion { get; set; }
		public bool IndCierre { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string IdEncriptado { get; set; }
        public string IdEtapa { get; set; }
	}
}
