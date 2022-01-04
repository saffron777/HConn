using System;

namespace HConnexum.Base.Dtos.ObjetosUtilitariosDto
{
	public class AuditoriaDto
	{
		/// <summary>
		/// Obtiene o establece el Id de Sesiçon.
		/// </summary>
		public string IdSesion { get; set; }
		
		/// <summary>
		/// Obtiene o establece la dirección Ip del usuario.
		/// </summary>
		public string IpUsuario { get; set; }
		
		/// <summary>
		/// Obtiene o establece el nombre del Host.
		/// </summary>
		public string HostName { get; set; }
		
		/// <summary>
		/// Obtiene o establece el login del usuario
		/// </summary>
		public string LoginUsuario { get; set; }
	}
}