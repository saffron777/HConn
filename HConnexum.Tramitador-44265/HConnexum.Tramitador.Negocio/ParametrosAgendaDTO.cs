using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase ParametrosAgendaDTO.</summary>
    public class ParametrosAgendaDTO
	{
	public int Id { get; set; }
		public int IdPaso { get; set; }
		public string Nombre { get; set; }
		public int FrecuenciaEjecucion { get; set; }
        public DateTime FechaValidez { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string IdEncriptado { get; set; }
        public string NombreFrecuencia { get; set; }
        public bool IndInmediato { get; set; }
        public int Cantidad { get; set; }
        public bool IndPrimeraVez { get; set; }
	}
}
