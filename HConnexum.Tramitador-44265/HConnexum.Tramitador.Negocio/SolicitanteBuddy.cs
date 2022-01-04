using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: SolicitanteMetadata.</summary>
	internal interface ISolicitanteMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		[StringLength(80)]
		string Nombre{ get; set; }

		[StringLength(80)]
		string Apellido{ get; set; }

		[Required]
		[StringLength(20)]
		string TipDoc{ get; set; }

		[Required]
		[StringLength(20)]
		string NumDoc{ get; set; }

		[StringLength(30)]
		string Email{ get; set; }

        [StringLength(50)]
		string Movil{ get; set; }

		[StringLength(20)]
		string Token{ get; set; }

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
	
	///<summary>Clase: Solicitante.</summary>
	[MetadataType(typeof(ISolicitanteMetadata))]
	public partial class Solicitante:ISolicitanteMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}