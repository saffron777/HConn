using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase SolicitudDTO.</summary>
	public class SolicitudDTO
	{
	public int Id { get; set; }
		public int IdSolicitante { get; set; }
		public int? IdServicioSuscriptor { get; set; }
		public int IdPais { get; set; }
		public int? IdDivisionTerritorial1 { get; set; }
		public DateTime? IdDivisionTerritorial2 { get; set; }
		public int? IdDivisionTerritorial3 { get; set; }
		public int Estatus { get; set; }
		public int IdFlujoServicio { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndVigente { get; set; }
		public bool IndEliminado { get; set; }
		public int? IdCasoHC { get; set; }
		public string XMLSolicitud { get; set; }
		public string IdCasoExterno { get; set; }
        public string IdCasoExterno2 { get; set; }
        public string IdCasoExterno3 { get; set; }
		public string Origen { get; set; }
		public string IdCasoExterior { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}
