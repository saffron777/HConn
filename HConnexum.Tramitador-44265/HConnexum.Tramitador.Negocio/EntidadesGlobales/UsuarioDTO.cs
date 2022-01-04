using System;

namespace HConnexum.Tramitador.Negocio
{
	public class UsuarioDTO
	{
		public int Id { get; set; }
		public int IdDatoBase { get; set; }
		public string PregSeg1 { get; set; }
		public string RespSeg1 { get; set; }
		public string PregSeg2 { get; set; }
		public string RespSeg2 { get; set; }
		public int IntentoAccesos { get; set; }
		public string LoginUsuario { get; set; }
		public string Clave { get; set; }
		public string UsuarioCms { get; set; }
		public string ClaveCms { get; set; }
		public DateTime FechaUltimoLogin { get; set; }
		public DateTime FechaUltimoCambioClave { get; set; }
		public int EstatusUsuario { get; set; }
		public bool? IndBloqueo { get; set; }
		public bool? IndSuplantado { get; set; }
		public int? DiasAutoBloqueo { get; set; }
		public string Telefofi { get; set; }
		public string Extencion { get; set; }
		public int? JefeInmediato { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string NombreApellido { get; set; }
		public int IndTipoDoc { get; set; }
		public string NumDoc { get; set; }
		public string IdEncriptado { get; set; }
		public int IdUsuario { get; set; }
		public int IdUsuarioSuscriptor { get; set; }
		public string Nombre1 { get; set; }
		public string Nombre2 { get; set; }
		public string Apellido1 { get; set; }
		public string Apellido2 { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
