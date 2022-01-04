using System;

namespace HConnexum.Tramitador.Negocio
{
	public class ListasValorDTO
	{
		public int Id { get; set; }
		public int IdLista { get; set; }
		public string NombreValor { get; set; }
		public string NombreValorCorto { get; set; }
		public string Valor { get; set; }
		public int? Posicion { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
		public string IdEncriptado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
