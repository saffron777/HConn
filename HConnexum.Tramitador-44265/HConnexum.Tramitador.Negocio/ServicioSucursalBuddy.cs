using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: ServicioSucursalMetadata.</summary>
	internal interface IServicioSucursalMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		[Integer]
		int IdFlujoServicio{ get; set; }

		[Required]
		[Integer]
		int IdSucursal{ get; set; }

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

		bool? IndVigente{ get; set; }

		[Date]
		DateTime? FechaValidez{ get; set; }

		bool? IndEliminado{ get; set; }

	}
	
	///<summary>Clase: ServicioSucursal.</summary>
	[MetadataType(typeof(IServicioSucursalMetadata))]
	public partial class ServicioSucursal:IServicioSucursalMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}