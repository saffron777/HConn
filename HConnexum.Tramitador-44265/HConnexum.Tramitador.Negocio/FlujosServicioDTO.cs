using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	[Serializable]
	///<summary>Clase FlujosServicioDTO.</summary>
	public class FlujosServicioDTO
	{
		public int Id { get; set; }
		public bool IndPublico { get; set; }
		public int IdSuscriptor { get; set; }
		public string NombreSuscriptor { get; set; }
		public int IdOrigen { get; set; }
		public int IdServicioSuscriptor { get; set; }
		public int? IdPasoInicial { get; set; }
		public string Tipo { get; set; }
		public int IdTipo { get; set; }
		public string NombreServicioSuscriptor { get; set; }
		public int SlaTolerancia { get; set; }
		public int? SlaPromedio { get; set; }
		public int Prioridad { get; set; }
		public int Version { get; set; }
		public bool IndCms { get; set; }
		public bool IndBloqueGenericoSolicitud { get; set; }
		public string MetodoPreSolicitud { get; set; }
		public string MetodoPostSolicitud { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModicacion { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
		public int? IdEtapa { get; set; }
		public string NombrePaso { get; set; }
		public string XLMEstructura { get; set; }
		public string IdEncriptado { get; set; }
		public int IdFlujoServicio { get; set; }
		public int IdPaso { get; set; }
		public string SuscriptorFaxNumero { get; set; }
        public string NomPrograma { get; set; }
        public bool? IndChat { get; set; }
        public bool IndSimulable { get; set; }
	}
}
