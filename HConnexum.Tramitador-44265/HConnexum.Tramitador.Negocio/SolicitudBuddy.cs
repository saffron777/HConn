using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: SolicitudMetadata.</summary>
	internal interface ISolicitudMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		[Integer]
		int IdSolicitante{ get; set; }

		[Integer]
		int? IdServicioSuscriptor{ get; set; }

		[Required]
		[Integer]
		int IdPais{ get; set; }

		[Integer]
		int? IdDivisionTerritorial1{ get; set; }

		[Date]
		DateTime? IdDivisionTerritorial2{ get; set; }

		[Integer]
		int? IdDivisionTerritorial3{ get; set; }

		[Required]
		[Integer]
		int Estatus{ get; set; }

		[Required]
		[Integer]
		int IdFlujoServicio{ get; set; }

		[Required]
		[Integer]
		int CreadoPor{ get; set; }

		[Required]
		[Date]
		DateTime FechaCreacion{ get; set; }

		[Integer]
		int? ModificadoPor{ get; set; }

		[Date]
		DateTime? FechaModificacion{ get; set; }

		[Date]
		DateTime? FechaValidez{ get; set; }

		[Required]
		bool IndVigente{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }

		[Integer]
		int? IdCasoHC{ get; set; }

		string XMLSolicitud{ get; set; }

		[StringLength(100)]
		string IdCasoExterno{ get; set; }

		[StringLength(100)]
		string Origen{ get; set; }

		[StringLength(100)]
		string IdCasoExterno2{ get; set; }

	}
	
	///<summary>Clase: Solicitud.</summary>
	[MetadataType(typeof(ISolicitudMetadata))]
	public partial class Solicitud:ISolicitudMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}