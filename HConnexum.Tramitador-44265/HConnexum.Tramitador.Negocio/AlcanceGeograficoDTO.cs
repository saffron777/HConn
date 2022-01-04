using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase AlcanceGeograficoDTO.</summary>
    public class AlcanceGeograficoDTO
	{
	public int Id { get; set; }
		public int IdServicioSucursal { get; set; }
        public int IdFlujoServicio { get; set; }
        public int IdSucursal { get; set; }
        public string Servicio { get; set; }
        public string Sucursal { get; set; }
		public int IdPais { get; set; }
        public string Pais { get; set; }
		public int? IdDiv1 { get; set; }
        public string Div1 { get; set; }
        public int? IdDiv2 { get; set; }
        public string Div2 { get; set; }
		public int? IdDiv3 { get; set; }
        public string Div3 { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool? IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool? IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string IdEncriptado { get; set; }
        public int Version { get; set; }
	}
}
