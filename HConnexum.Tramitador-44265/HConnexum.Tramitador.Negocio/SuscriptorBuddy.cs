using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: SuscriptorMetadata.</summary>
	internal interface ISuscriptorMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[StringLength(20)]
		string NumDoc { get; set; }

		[Required]
		[StringLength(100)]
		string Nombre { get; set; }

		DateTime? FechaValidez { get; set; }

		[Required]
		bool IndVigente { get; set; }

		[Required]
		bool IndEliminado { get; set; }
	}

	///<summary>Clase: Suscriptor.</summary>
	[MetadataType(typeof(ISuscriptorMetadata))]
	public partial class Suscriptor : ISuscriptorMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }

		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }

		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }
	}
}