using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase SolicitudBloqueDTO.</summary>
	public class SolicitudBloqueDTO
	{
		public int Id { get; set; }
		public int? IdFlujoServicio { get; set; }
		public string NombreBloque { get; set; }
		public int? IdBloque { get; set; }
		public int Orden { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public bool IndCierre { get; set; }
		public string NombreTipoControl { get; set; }
		public int? IdTipoControl { get; set; }
		public string TituloBloque { get; set; }
		public bool IndActualizable { get; set; }
		public string KeyCampoXML { get; set; }
		public string NombrePrograma { get; set; }
		public int? IdListaValor { get; set; }
		public string IdEncriptado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
