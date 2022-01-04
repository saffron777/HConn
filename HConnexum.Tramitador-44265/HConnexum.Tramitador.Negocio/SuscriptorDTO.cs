using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase SuscriptorDTO.</summary>
    public class SuscriptorDTO
	{
		public int Id { get; set; }
        public int TipoDetalleId { get; set; }
		public int IndTipDoc { get; set; }
        public string DocumentoTipo { get; set; }
		public string NumDoc { get; set; }
		public string Nombre { get; set; }
        public int IdTipo { get; set; }
        public string Tipo { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaInactivacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int EstatusId { get; set; }
        public string Estatus { get; set; }
        public string CodigoExternoId { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int DivisionTerritorial1Id { get; set; }
        public string DivisionTerritorial1 { get; set; }
        public int DivisionTerritorial2Id { get; set; }
        public string DivisionTerritorial2 { get; set; }
        public int DivisionTerritorial3Id { get; set; }
        public string DivisionTerritorial3 { get; set; }
        public int CasosPendientesCantidad { get; set; }
        public string Especialidad { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
        public string IdEncriptado { get; set; }
        public string Redes { get; set; }
	}
}
