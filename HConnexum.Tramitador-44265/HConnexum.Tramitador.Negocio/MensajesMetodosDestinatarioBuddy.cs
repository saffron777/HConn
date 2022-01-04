using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: MensajesMetodosDestinatarioMetadata.</summary>
	internal interface IMensajesMetodosDestinatarioMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Integer]
		int? IdPaso{ get; set; }

		[Integer]
		int? IdMetodo{ get; set; }

		[Required]
		[Integer]
		int IdMensaje{ get; set; }

		[Integer]
		int? IdTipoBusquedaDestinatario{ get; set; }

		[Required]
		[StringLength(100)]
		string ValorBusqueda{ get; set; }

		[Integer]
		int? IdTipoPrivacidad{ get; set; }

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

		[Required]
		bool IndEliminado{ get; set; }

        [Date]
        DateTime? FechaValidez { get; set; }

	}
	
	///<summary>Clase: MensajesMetodosDestinatario.</summary>
	[MetadataType(typeof(IMensajesMetodosDestinatarioMetadata))]
	public partial class MensajesMetodosDestinatario:IMensajesMetodosDestinatarioMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}