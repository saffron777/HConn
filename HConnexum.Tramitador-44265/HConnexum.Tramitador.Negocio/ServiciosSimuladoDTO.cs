using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase ServiciosSimuladoDTO.</summary>
	public class ServiciosSimuladoDTO
	{
	public int Id { get; set; }
		public int IdServicioSuscriptor { get; set; }
		public int IdSuscriptorASimular { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public bool IndVigente { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string NombreServicio  { get; set; }
        public string NombreSuscriptor { get; set; }
        public string UsuarioIncorporador { get; set; }
        public string IdEncriptado { get; set; }        
	}
}
