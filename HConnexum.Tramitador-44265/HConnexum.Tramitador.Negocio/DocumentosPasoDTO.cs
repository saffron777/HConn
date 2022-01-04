using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase DocumentosPasoDTO.</summary>
    public class DocumentosPasoDTO
	{
	public int Id { get; set; }
		public int IdDocumentoServicio { get; set; }
		public int IdPaso { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string DocumentoServicio { get; set; }
        public string IdEncriptado { get; set; }
	}
}
