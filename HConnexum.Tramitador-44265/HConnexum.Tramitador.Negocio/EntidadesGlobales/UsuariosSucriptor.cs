using System;
using System.Data.Objects.DataClasses;

namespace HConnexum.Tramitador.Negocio
{
	public class UsuariosSucriptor : EntityObject
	{
		public int Id { get; set; }
		public int IdSuscriptor { get; set; }
		public string NombreSuscriptor { get; set; }
		public int IdUsuario { get; set; }
		public string NombreUsuario { get; set; }
		public int Estatus { get; set; }
		public string Telefofi { get; set; }
		public string Extension { get; set; }
		public string ECorreo { get; set; }
		public int? IdJefeInmediato { get; set; }
		public bool? IndSuplantado { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool? IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool? IndEliminado { get; set; }
		public string LoginUsuario { get; set; }
		public string IdEncriptado { get; set; }
	}
}
