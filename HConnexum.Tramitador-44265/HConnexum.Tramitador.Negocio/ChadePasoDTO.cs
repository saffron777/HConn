using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase ChadePasoDTO.</summary>
    public class ChadePasoDTO
	{
	public int Id { get; set; }
		public int IdPasos { get; set; }
		public int? IdCargosuscriptor { get; set; }
		public int? IdHabilidadSuscriptor { get; set; }
		public int? IdAutonomiaSuscriptor { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public int IdSuscriptor { get; set; }
        public string NombreCargo { get; set; }
        public string NombreHabilidad { get; set; }
        public string NombreAutonomia { get; set; }
        public string IdEncriptado { get; set; }
	}
}
