using System;

namespace HConnexum.Tramitador.Negocio
{
	public class SesionesUsuarioDTO
	{
		public int Id { get; set; }
		public int IdUsuario { get; set; }
		public DateTime FechaInicio { get; set; }
		public DateTime? FechaFin { get; set; }
		public bool IndActiva { get; set; }
		public string IpConexion { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool? IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool? IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
