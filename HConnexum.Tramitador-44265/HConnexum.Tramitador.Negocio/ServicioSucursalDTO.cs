using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase ServicioSucursalDTO.</summary>
    public class ServicioSucursalDTO
	{
	public int Id { get; set; }
		public int IdFlujoServicio { get; set; }
        public string Servicio { get; set; }
		public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool? IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool? IndEliminado { get; set; }
		public string Tomado { get; set; }
        public string IdEncriptado { get; set; }
		public string UsuarioTomado { get; set; }
        public string NombreServicio { get; set; }
        public int IdServicioSuscriptor { get; set; }
        public int IdSuscriptor { get; set; }
        public int Version { get; set; }
        public string NombreSucursal { get; set; }
	}
}
