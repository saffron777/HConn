using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Negocio
{
	internal interface IListaMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[StringLength(255)]
		string Descripcion { get; set; }

		[Required]
		bool IndVigente { get; set; }

		[Date]
		DateTime? FechaValidez { get; set; }

		[Required]
		bool IndEliminado { get; set; }

		[Integer]
		int? IdSuscriptor { get; set; }

		[StringLength(150)]
		string Nombre { get; set; }

	}
	[MetadataType(typeof(IListaMetadata))]
	public partial class Lista : IListaMetadata
	{
		public string IdEncriptado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
    }
}