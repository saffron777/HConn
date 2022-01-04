using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: EtapaMetadata.</summary>
	internal interface IEtapaMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		[Integer]
		int IdFlujoServicio{ get; set; }

		[Required]
		[StringLength(50)]
		string Nombre{ get; set; }

		[Required]
		[Integer]
		int Orden{ get; set; }

		[Required]
		[Integer]
		int SlaPromedio{ get; set; }

		[Required]
		[Integer]
		int SlaTolerancia{ get; set; }

		[Required]
		bool IndObligatorio{ get; set; }

		[Required]
		bool IndRepeticion{ get; set; }

		[Required]
		bool IndSeguimiento{ get; set; }

		[Required]
		bool IndInicioServ{ get; set; }

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
		bool IndCierre{ get; set; }

		[Required]
		bool IndVigente{ get; set; }

		[Date]
		DateTime? FechaValidez{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }

	}
	
	///<summary>Clase: Etapa.</summary>
	[MetadataType(typeof(IEtapaMetadata))]
	public partial class Etapa:IEtapaMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}