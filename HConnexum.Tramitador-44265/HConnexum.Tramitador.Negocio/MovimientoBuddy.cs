using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: MovimientoMetadata.</summary>
	internal interface IMovimientoMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Integer]
		int? IdMovimientoPadre{ get; set; }

		[Required]
		[Integer]
		int IdPaso{ get; set; }

		[Required]
		[Integer]
		int IdCaso{ get; set; }

		[Integer]
		int? IdCasoRelacionado{ get; set; }

		[Integer]
		int? IdSuscriptor{ get; set; }

		[StringLength(200)]
		string Nombre{ get; set; }

		[Required]
		[Integer]
		int TipoMovimiento{ get; set; }

		[Required]
		[Integer]
		int Estatus{ get; set; }

		[StringLength(10)]
		string EtiqSincroIn{ get; set; }

		[StringLength(10)]
		string EtiqSincroOut{ get; set; }

		decimal? NumSincroPend{ get; set; }

		[Required]
		bool IndSeguimiento{ get; set; }

		[Required]
		bool IndAgendable{ get; set; }

		[Integer]
		int? NumRepRestantes{ get; set; }

		[Required]
		bool IndRequiereParametrosIN{ get; set; }

		[Required]
		bool IndRequiereParametrosOUT{ get; set; }

		[Date]
		DateTime? FechaOmitido{ get; set; }

		[Date]
		DateTime? FechaEnProceso{ get; set; }

		[Date]
		DateTime? FechaEjecutado{ get; set; }

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

		[Required]
		[Integer]
		int ReintentosRestantes{ get; set; }

		[Integer]
		int? UsuarioAsignado{ get; set; }

	}
	
	///<summary>Clase: Movimiento.</summary>
	[MetadataType(typeof(IMovimientoMetadata))]
	public partial class Movimiento:IMovimientoMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}