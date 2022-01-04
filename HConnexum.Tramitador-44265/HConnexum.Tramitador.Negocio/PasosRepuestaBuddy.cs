using System;
using System.ComponentModel.DataAnnotations;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
    ///<summary>Interface: PasosRepuestaMetadata.</summary>
    internal interface IPasosRepuestaMetadata
    {
        [Required]
        [Integer]
        int Id { get; set; }
		
        [Required]
        [Integer]
        int IdPaso { get; set; }

        [Required]
        [Integer]
        int Orden { get; set; }

        [Required]
        [StringLength(100)]
        string ValorRespuesta { get; set; }

        [Required]
        bool IndCierre { get; set; }

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

    }

    ///<summary>Clase: PasosRepuesta.</summary>
    [MetadataType(typeof(IPasosRepuestaMetadata))]
    public partial class PasosRepuesta : IPasosRepuestaMetadata
    {
        ///<summary>Identificador de la tabla encriptado.</summary>
        public string IdEncriptado { get; set; }

        ///<summary>Identificador para registro tomado.</summary>
        public string Tomado { get; set; }

        ///<summary>Identificador del usuario que ha tomado un registro.</summary>
        public string UsuarioTomado { get; set; }

    }
}