using System;
using System.Data.Objects.DataClasses;

namespace HConnexum.Base.Dtos.EntidadesGlobales
{
	public class TomadoDto : EntityObject
	{
		public int Id { get; set; }
		public int IdPaginaModulo { get; set; }
		public string NombrePagina { get; set; }
		public string Tabla { get; set; }
		public int IdRegistro { get; set; }
		public string IdSesionUsuario { get; set; }
		public int IdSuscriptor { get; set; }
		public string LoginUsuario { get; set; }
		public string NombreUsuario { get; set; }
		public DateTime FechaTomado { get; set; }
		public string IdEncriptado { get; set; }
	}
}