using System;

namespace HConnexum.Tramitador.Negocio
{
	public class ListaDTO
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public int Estatus { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public int? IdSuscriptor { get; set; }
		public string Nombre { get; set; }
		public string IdEncriptado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
