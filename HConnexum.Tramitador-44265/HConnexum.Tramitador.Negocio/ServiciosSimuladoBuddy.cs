using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
	///<summary>Interface: ServiciosSimuladoMetadata.</summary>
	internal interface IServiciosSimuladoMetadata
	{
	[Required]
		[Integer]
		int Id{ get; set; }

		[Required]
		[Integer]
		int IdServicioSuscriptor{ get; set; }

		[Required]
		[Integer]
		int IdSuscriptorASimular{ get; set; }

		[Required]
		[Date]
		DateTime FechaInicio{ get; set; }

		[Required]
		[Date]
		DateTime FechaFin{ get; set; }

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

		[Date]
		DateTime? FechaValidez{ get; set; }

		[Required]
		bool IndEliminado{ get; set; }

		[Required]
		bool IndVigente{ get; set; }

	}
	
	///<summary>Clase: ServiciosSimulado.</summary>
    [MetadataType(typeof(IServiciosSimuladoMetadata))]
	public partial class ServiciosSimulado:IServiciosSimuladoMetadata
    {
		///<summary>Identificador de la tabla encriptado.</summary>
		public string IdEncriptado { get; set; }
		
		///<summary>Identificador para registro tomado.</summary>
		public string Tomado { get; set; }
        
		///<summary>Identificador del usuario que ha tomado un registro.</summary>
		public string UsuarioTomado { get; set; }

        public int Id { get; set; }

        public int IdServicioSuscriptor { get; set; }

        public int IdSuscriptorASimular { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int CreadoPor { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? ModificadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaValidez { get; set; }

        public bool IndEliminado { get; set; }

        public bool IndVigente { get; set; }
    }
}