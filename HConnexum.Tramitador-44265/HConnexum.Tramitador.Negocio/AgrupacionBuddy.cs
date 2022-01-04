using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: AgrupacionMetadata.</summary>
	internal interface IAgrupacionMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		[StringLength(50)]
		string Nombre{ get; set; }

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

	}
	
	///<summary>Clase: Agrupacion.</summary>
	[MetadataType(typeof(IAgrupacionMetadata))]
	public partial class Agrupacion:IAgrupacionMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}