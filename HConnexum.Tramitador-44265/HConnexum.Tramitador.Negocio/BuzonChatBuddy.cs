using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: BuzonChatMetadata.</summary>
	internal interface IBuzonChatMetadata
	{
		[Required]
		[Integer]
		int Id { get; set; }

		[Required]
		[StringLength(800)]
		string Mensaje { get; set; }

		[Required]
		bool IndLeido { get; set; }

		[Required]
		int IdCaso { get; set; }

		[Integer]
		int? IdMovimiento { get; set; }

		[Integer]
		int? IdSuscriptorEnvio { get; set; }

		[StringLength(100)]
		string Remitente { get; set; }

		[Integer]
		int? IdSuscriptorRecibe { get; set; }

		[StringLength(100)]
		string LeidoPor { get; set; }

		[Integer]
		int? CreadoPor { get; set; }

		[Date]
		DateTime? FechaCreacion { get; set; }

		[Integer]
		int? ModificadoPor { get; set; }

		[Date]
		DateTime? FechaModificacion { get; set; }

		[Date]
		DateTime? FechaValidacion { get; set; }

		bool? IndValido { get; set; }

		bool? IndEliminado { get; set; }
	}

	///<summary>Clase: BuzonChat.</summary>
	[MetadataType(typeof(IBuzonChatMetadata))]
	public partial class BuzonChat:IBuzonChatMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }

		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }

		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }
	} 
}