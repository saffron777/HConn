using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase MensajesMetodosDestinatarioDTO.</summary>
    [Serializable]
    public class MensajesMetodosDestinatarioDTO
	{
	public int Id { get; set; }
		public int? IdPaso { get; set; }
		public int? IdMetodo { get; set; }
		public int IdMensaje { get; set; }
		public int? TipoBusquedaDestinatario { get; set; }
		public string ValorBusqueda { get; set; }
		public int? TipoPrivacidad { get; set; }
        public bool IndVigenteFlujoServicio { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public DateTime? FechaValidez { get; set; }
	}
}
