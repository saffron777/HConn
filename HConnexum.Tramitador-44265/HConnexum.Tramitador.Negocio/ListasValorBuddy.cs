using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Negocio
{
	internal interface IListaValorMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[Integer]
		int IdLista { get; set; }

		[Required]
		[StringLength(80)]
		string NombreValor { get; set; }

		[StringLength(10)]
		string NombreValorCorto { get; set; }

		[Integer]
		int? Posicion { get; set; }

		[Date]
		DateTime? FechaValidez { get; set; }

		[Required]
		bool IndVigente { get; set; }

		[Required]
		bool IndEliminado { get; set; }

	}
	[MetadataType(typeof(IListaValorMetadata))]
	public partial class ListaValor : IListaValorMetadata
	{
		public string IdEncriptado { get; set; }
		public string Tomado { get; set; }
		public string UsuarioTomado { get; set; }
    }
}