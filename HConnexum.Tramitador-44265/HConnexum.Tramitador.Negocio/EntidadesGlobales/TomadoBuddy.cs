using System;
using System.ComponentModel.DataAnnotations;

using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: TomadoMetadata.</summary>
	internal interface ITomadoMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[Integer]
		int IdPaginaModulo { get; set; }

		[Required]
		string Tabla { get; set; }

		[Required]
		[Integer]
		int IdRegistro { get; set; }

		[Required]
		[Integer]
		int IdSesionUsuario { get; set; }

		[Required]
		[Date]
		DateTime FechaTomado { get; set; }
	}

	[MetadataType(typeof(ITomadoMetadata))]
	public partial class Tomado : ITomadoMetadata
	{
		public string IdEncriptado { get; set; }
	}
}