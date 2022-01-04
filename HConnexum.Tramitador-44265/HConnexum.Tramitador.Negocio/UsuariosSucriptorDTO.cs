using System;

namespace HConnexum.Tramitador.Negocio
{
	public class UsuariosSucriptorDTO
	{
		public int Id { get; set; }			
        public string NombreApellido { get; set; }
        public DateTime? FechaValidez { get; set; }
        public bool IndVigente { get; set; }
        public bool IndEliminado { get; set; }
        public string IdEncriptado { get; set; }
	}
}
