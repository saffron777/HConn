using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: FlujosEjecucionMetadata.</summary>
	internal interface IFlujosEjecucionMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Integer]
		int? IdPasoDestino{ get; set; }
				
		[Integer]
		int? Condicion{ get; set; }

		[Required]
		[Integer]
		int CreadoPor{ get; set; }

		[Required]
		[Date]
		DateTime FechaCreacion{ get; set; }

		[Integer]
		int? ModificadoPor{ get; set; }

		[Date]
		DateTime? FechaModicacion{ get; set; }

		[Required]
		bool IndVigente{ get; set; }

		[Date]
		DateTime? FechaValidez{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }

        [Required]
        bool IndReinicioRepeticion { get; set; }

        [Integer]
        int? IdPasoDesborde { get; set; }

	}
	
	///<summary>Clase: FlujosEjecucion.</summary>
	[MetadataType(typeof(IFlujosEjecucionMetadata))]
	public partial class FlujosEjecucion:IFlujosEjecucionMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}