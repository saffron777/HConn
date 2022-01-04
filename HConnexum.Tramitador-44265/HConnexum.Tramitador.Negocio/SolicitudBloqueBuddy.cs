using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: SolicitudBloqueMetadata.</summary>
	internal interface ISolicitudBloqueMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Integer]
		int? IdFlujoServicio{ get; set; }

		[Integer]
		int? IdBloque{ get; set; }

		[Required]
		[Integer]
		int Orden{ get; set; }

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

		[Required]
		bool IndCierre{ get; set; }

		[Integer]
		int? IdTipoControl{ get; set; }

		[StringLength(80)]
		string TituloBloque{ get; set; }

		[Required]
		bool IndActualizable{ get; set; }

		[StringLength(80)]
		string KeyCampoXML{ get; set; }

	}
	
	///<summary>Clase: SolicitudBloque.</summary>
	[MetadataType(typeof(ISolicitudBloqueMetadata))]
	public partial class SolicitudBloque:ISolicitudBloqueMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}