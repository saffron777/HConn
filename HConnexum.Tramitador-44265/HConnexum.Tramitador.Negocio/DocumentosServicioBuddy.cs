using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: DocumentosServicioMetadata.</summary>
	internal interface IDocumentosServicioMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		[Integer]
		int IdFlujoServicio{ get; set; }

		[Required]
		[Integer]
		int IdDocumento{ get; set; }

		[Required]
		bool IndDocObligatorio{ get; set; }

		[Required]
		bool IndVisibilidad{ get; set; }

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

		[Required]
		bool IndVigente{ get; set; }

		[Date]
		DateTime? FechaValidez{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }

	}
	
	///<summary>Clase: DocumentosServicio.</summary>
	[MetadataType(typeof(IDocumentosServicioMetadata))]
	public partial class DocumentosServicio:IDocumentosServicioMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}