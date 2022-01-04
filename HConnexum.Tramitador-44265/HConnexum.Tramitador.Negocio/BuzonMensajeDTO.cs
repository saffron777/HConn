using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase BuzonMensajeDTO.</summary>
	public class BuzonMensajeDTO
	{
	public int Id { get; set; }
		public int IdMovimiento { get; set; }
		public int IdPasoMensaje { get; set; }
		public string Destinatario { get; set; }
		public int TipoEnvio { get; set; }
		public int Estatus { get; set; }
		public int IdMetodoEnvio { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
