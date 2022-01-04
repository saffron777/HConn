using System;
using System.ComponentModel.DataAnnotations;

using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: CasoMetadata.</summary>
	internal interface ICasoMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }

		[Required]
		bool IndVigente{ get; set; }

		[Date]
		DateTime? FechaValidez{ get; set; }

		int? PrioridadAtencion{ get; set; }

		[Required]
		[Integer]
		int IdSolicitud{ get; set; }

		[Required]
		[Integer]
		int IdFlujoServicio{ get; set; }

		[Required]
		[Integer]
		int IdServicio{ get; set; }

		[Required]
		[StringLength(20)]
		string Ticket{ get; set; }

		[Required]
		[StringLength(20)]
		string TipDocBeneficiario{ get; set; }

		[Required]
		[StringLength(20)]
		string NumDocBeneficiario{ get; set; }

		[Required]
		[StringLength(80)]
		string NombreBeneficiario{ get; set; }

		[Required]
		[StringLength(80)]
		string ApellidoBeneficiario{ get; set; }

		[StringLength(20)]
		string MovilBeneficiario{ get; set; }

		[StringLength(30)]
		string EmailBeneficiario{ get; set; }

		[Required]
		[StringLength(20)]
		string ClaveBeneficiario{ get; set; }

		[Required]
		[Integer]
		int Estatus{ get; set; }

		[StringLength(20)]
		string NumPoliza{ get; set; }

		[Integer]
		int? NumMvtoPoliza{ get; set; }

		[Integer]
		int? NumCertificado{ get; set; }

		[Integer]
		int? NumRiesgo{ get; set; }

		[StringLength(20)]
		string TipDocAgt{ get; set; }

		[StringLength(20)]
		string NumDocAgt{ get; set; }

		string XMLRespuesta{ get; set; }

		[Required]
		[Integer]
		int CreadorPor{ get; set; }

		[Required]
		[Date]
		DateTime FechaCreacion{ get; set; }

		[Integer]
		int? ModificadoPor{ get; set; }

		[Date]
		DateTime? FechaModificacion{ get; set; }

		[Required]
		[Date]
		DateTime FechaSolicitud{ get; set; }

		[Date]
		DateTime? FechaAnulacion{ get; set; }

		[Date]
		DateTime? FechaRechazo{ get; set; }

		[Date]
		DateTime? FechaEjecucion{ get; set; }

		[Date]
		DateTime? FechaReverso{ get; set; }

		[Integer]
		int? IdMovimiento{ get; set; }

	}
	
	///<summary>Clase: Caso.</summary>
	[MetadataType(typeof(ICasoMetadata))]
	public partial class Caso:ICasoMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}