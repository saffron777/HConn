using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: PasoMetadata.</summary>
	internal interface IPasoMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Integer]
		int IdEtapa{ get; set; }

		[Integer]
		int IdTipoPaso{ get; set; }

		[Integer]
		int? IdSubServicio{ get; set; }

		[Integer]
		int? IdAlerta{ get; set; }

		[Required]
		[StringLength(50)]
		string Nombre{ get; set; }

        [Required]
		[StringLength(255)]
		string Observacion{ get; set; }

        [StringLength(2500)]
        string URL { get; set; }

        [StringLength(10)]
        string EtiqSincroIn { get; set; }

        [StringLength(10)]
        string EtiqSincroOut { get; set; }

        [StringLength(2500)]
        string Metodo { get; set; }

		bool IndObligatorio{ get; set; }

		[Integer]
		Int16 CantidadRepeticion{ get; set; }

		bool IndRequiereRespuesta{ get; set; }

		bool IndCerrarEtapa{ get; set; }

		[Integer]
		int SlaTolerancia{ get; set; }

		bool IndSeguimiento{ get; set; }

		bool IndAgendable{ get; set; }

		bool IndCerrarServicio{ get; set; }

		[Integer]
		byte Reintentos{ get; set; }

		bool IndSegSubServicio{ get; set; }

		[Integer]
		int PorcSlaCritico{ get; set; }

		[Required]
		[Integer]
		int CreadoPor{ get; set; }

		[Required]
		[Date]
		DateTime FechaCreacion{ get; set; }

		[Integer]
		int? ModificadoPor{ get; set; }

        [Date]
        DateTime? FechaModificacion { get; set; }		

		[Required]
		bool IndVigente{ get; set; }

		[Date]
		DateTime? FechaValidez{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }

        [Required]
        [Integer]
        int Orden { get; set; }
	}
	
	///<summary>Clase: Paso.</summary>
	[MetadataType(typeof(IPasoMetadata))]
	public partial class Paso:IPasoMetadata
	{
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

	} 
}