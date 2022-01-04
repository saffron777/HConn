using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Negocio
{
	internal interface IUsuarioMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[StringLength(150)]
		string PregSeg1 { get; set; }

		[Required]
		[StringLength(150)]
		string RespSeg1 { get; set; }

		[Required]
		[StringLength(150)]
		string PregSeg2 { get; set; }

		[Required]
		[StringLength(150)]
		string RespSeg2 { get; set; }

		[Required]
		[Integer]
		int IntentoAccesos { get; set; }

		[Required]
		[StringLength(50)]
		string LoginUsuario { get; set; }

		[Required]
		[StringLength(50)]
		string Clave { get; set; }

		[Required]
		[Date]
		DateTime FechaUltimoLogin { get; set; }

		[Required]
		[Date]
		DateTime FechaUltimoCambioClave { get; set; }

		bool? IndBloqueo { get; set; }

		[Integer]
		int? DiasAutoBloqueo { get; set; }

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

		[Required]
		bool IndVigente { get; set; }

		[Date]
		DateTime? FechaValidez { get; set; }

		[Required]
		bool IndEliminado { get; set; }

        [Required]
        [StringLength(255)]
        string UsuarioCms { get; set; }

        [Required]
        [StringLength(255)]
        string ClaveCms { get; set; }
	}

	[MetadataType(typeof(IUsuarioMetadata))]
	public partial class Usuario : IUsuarioMetadata
	{
		public string IdEncriptado { get; set; }
		public int IdSuscriptor { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
	}
}