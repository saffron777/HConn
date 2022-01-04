using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: SesionesUsuarioMetadata.</summary>
	internal interface ISesionesUsuarioMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[Integer]
		int IdUsuario { get; set; }

		[Required]
		[Date]
		DateTime FechaInicio { get; set; }

		[Date]
		DateTime? FechaFin { get; set; }

		[Required]
		bool IndActiva { get; set; }

		[StringLength(30)]
		string IpConexion { get; set; }

		[Required]
		[Integer]
		int CreadoPor { get; set; }

		[Required]
		[Date]
		DateTime FechaCreacion { get; set; }

		[Integer]
		int? ModificadoPor { get; set; }

		[Date]
		DateTime? FechaModificacion { get; set; }

		bool? IndVigente { get; set; }

		[Date]
		DateTime? FechaValidez { get; set; }

		bool? IndEliminado { get; set; }
	}

	[MetadataType(typeof(ISesionesUsuarioMetadata))]
	public partial class SesionesUsuario : ISesionesUsuarioMetadata
	{
		public string IdEncriptado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}