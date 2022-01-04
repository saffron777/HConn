using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: FlujosServicioMetadata.</summary>
	internal interface IFlujosServicioMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		bool IndPublico{ get; set; }

		[Required]
		[Integer]
		int IdSuscriptor{ get; set; }
        
        [Integer]
        int? IdPasoInicial { get; set; }
		
        [Required]
		[Integer]
		int IdOrigen{ get; set; }

		[Required]
		[Integer]
		int IdServicioSuscriptor{ get; set; }

		[Required]
		[Integer]
		int SlaTolerancia{ get; set; }

		[Integer]
		int? SlaPromedio{ get; set; }

		[Required]
		[Integer]
		int Prioridad{ get; set; }

		[Required]
		[Integer]
		int Version{ get; set; }

		[Required]
		bool IndCms{ get; set; }

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

		[Date]
		DateTime? FechaValidez{ get; set; }

		[Required]
		bool IndVigente{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }
       
        string XLMEstructura { get; set; }

        bool? IndChat { get; set; }

        string NomPrograma { get; set; }
	}
	
	///<summary>Clase: FlujosServicio.</summary>
	[MetadataType(typeof(IFlujosServicioMetadata))]
	public partial class FlujosServicio:IFlujosServicioMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

    } 
}
