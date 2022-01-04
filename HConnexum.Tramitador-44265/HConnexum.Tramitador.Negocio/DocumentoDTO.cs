using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase DocumentoDTO.</summary>
    public class DocumentoDTO
	{
	public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdRutaArchivo { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
