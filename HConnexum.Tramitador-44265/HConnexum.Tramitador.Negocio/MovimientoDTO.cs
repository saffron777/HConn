using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Clase MovimientoDTO.</summary>
	public class MovimientoDTO
	{
		public int Id { get; set; }
		public int IdServicio { get; set; }
		public int IdMovimiento { get; set; }
		public string Movimiento { get; set; }
		public int IdCaso { get; set; }
		public int? IdCasoRelacionado { get; set; }
		public string Caso { get; set; }
		public int Version { get; set; }
		public int TipoMovimiento { get; set; }
		public int EstatusCaso { get; set; }
		public int EstatusMovimiento { get; set; }
		public int CreadoPor { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime? FechaEjecucion { get; set; }
		public DateTime? FechaOmision { get; set; }
		public DateTime FechaAtencion { get; set; }
		public int? ModificadoPor { get; set; }
		public int TiempoEstimado { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
        
        public int IdUsuarioAsignado { get; set; }
        public string UsuarioAsignado{get;set;}

		public string NombrePaso { get; set; }
		public string Estatus { get; set; }
		public string IdEncriptado { get; set; }
		public string TipoP { get; set; }
		public string NombreServicio { get; set; }
		public DateTime? FechaSolicitud { get; set; }
		public string Solicitante { get; set; }
		public string MovilSolicitante { get; set; }
		public string NombreEstatusCaso { get; set; }
		public string NombreTipoMovimiento { get; set; }
		public string NombreEstatusMovimiento { get; set; }
		public string UsuarioCreacion { get; set; }
        public string DescripcionPaso { get; set; }
		public string NombreCreadoPor { get; set; }
		public string NombreModificadoPor { get; set; }
		public DateTime? FechaEnProceso { get; set; }
		public int IdServiciosuscriptor { get; set; }
		public string NombreSuscriptor { get; set; }
		public int SLAToleranciaPaso { get; set; }
		public bool IndObligatorio { get; set; }
		public int IdSolicitud { get; set; }
		public string NombreMovimiento { get; set; }
		public int IdFlujoServicio { get; set; }
		public string NombreServicioSuscriptor { get; set; }
		public int? PrioridadAtencion { get; set; }
		public string Nombrepaso { get; set; }
		public int? NumRiesgoCaso { get; set; }
		public int Idsuscriptor { get; set; }
		public string CreadorPorCaso { get; set; }
        public string Intermediario { get; set; }
        /// <summary>Cadena de texto con la respuesta del caso en formato XML.</summary>
        public string CasoRespuestaXML { get; set; }
        /// <summary>Identificador del paso asociado al movimiento.</summary>
        public int IdPaso { get; set; }
        public bool? indChat { get; set; }
       //datos del asegurado para el grid de consulta de movimiento por Suscriptor
        public string NombreAsegurado { get; set; }
        public string DocumentoAsegurado{get; set;}
        public int IdServicioSuscriptor { get; set; }
        public string ImgChat { get; set; }
	}
}