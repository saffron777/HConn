using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase PasoDTO.</summary>
    public class PasoDTO
	{
	    public int Id { get; set; }
		public int? IdAutonomiaSuscriptor { get; set; }
		public int? IdEtapa { get; set; }
		public int? IdTipoPaso { get; set; }
		public int? IdSubServicio { get; set; }
		public int? IdAlerta { get; set; }
        public string EtiqSincroIn { get; set; }
        public string EtiqSincroOut { get; set; }
		public string Nombre { get; set; }
		public string Observacion { get; set; }
        public string URL { get; set; }
        public string Metodo { get; set; }
		public bool? IndObligatorio { get; set; }
		public int? Orden { get; set; }
		public int? CantidadRepeticion { get; set; }
		public bool? IndRequiereRespuesta { get; set; }
		public bool? IndCerrarEtapa { get; set; }
		public int? SlaTolerancia { get; set; }
		public bool? IndSeguimiento { get; set; }
		public bool? IndAgendable { get; set; }
		public bool? IndCerrarServicio { get; set; }
		public byte? Reintentos { get; set; }
		public bool? IndSegSubServicio { get; set; }
		public int? IdPasoInicial { get; set; }
		public int? PorcSlaCritico { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string NombreTipoPaso { get; set; }
        public string IdEncriptado { get; set; }
        public int IdSuscriptor { get; set; }
        public int IdServicioSuscriptor { get; set; }
        public int IdFlujoServicio { get; set; }
        public int VersionFlujoServicio { get; set; }
        public bool IndVigenteFlujoServicio { get; set; }
        public string NombreEtapa { get; set; }
        public bool? IndAnulacion { get; set; }
        public bool IndEncadenado { get; set; }
        public int? idMovimientoPadre { get; set; }
	}
}
