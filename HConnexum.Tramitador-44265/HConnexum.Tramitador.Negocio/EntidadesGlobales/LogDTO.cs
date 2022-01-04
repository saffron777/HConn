using System;

namespace HConnexum.Tramitador.Negocio
{
	public class LogDTO
	{
		public int Id { get; set; }
		public int? IdSesion { get; set; }
		public DateTime FechaLog { get; set; }
		public string SpEjecutado { get; set; }
		public string Tabla { get; set; }
		public string Accion { get; set; }
		public string IdRegistro { get; set; }
		public string RegistroXML { get; set; }
		public bool TransaccionExitosa { get; set; }
		public string Mensaje { get; set; }
		public string IpUsuario { get; set; }
		public string HostName { get; set; }
		public string HostProcess { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
