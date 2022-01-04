using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase Buddy de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
    ///<summary>Interface: CasoAgrupacionMetadata.</summary>
    internal interface ICasoAgrupacionMetadata
    {
        [Required]
        [Integer]
        int Id { get; set; }

        [Required]
        [Integer]
        int IdCaso { get; set; }

        [Required]
        [Integer]
        int IdAgrupacion { get; set; }

        [Required]
        [Integer]
        int IdServicio { get; set; }

        [Required]
        [Integer]
        int IdSolicitud { get; set; }

        [Required]
        [Integer]
        int IdSuscriptor { get; set; }

        [Date]
        DateTime? FechaCreacion { get; set; }

        [Required]
        [Integer]
        int CreadoPor { get; set; }

        [Date]
        DateTime? FechaModificacion { get; set; }

        [Integer]
        int? ModificadoPor { get; set; }

        [Required]
        bool IndVigente { get; set; }

        [Required]
        bool IndEliminado { get; set; }
    }

    ///<summary>Clase: CasoAgrupacion.</summary>
    [MetadataType(typeof(ICasoAgrupacionMetadata))]
    public partial class CasoAgrupacion : ICasoAgrupacionMetadata
    {
        ///<summary>Identificador de la tabla encriptado.</summary>
        public string IdEncriptado { get; set; }
 		
        ///<summary>Identificador para registro tomado.</summary>
        public string Tomado { get; set; }
         
        ///<summary>Identificador del usuario que ha tomado un registro.</summary>
        public string UsuarioTomado { get; set; }
    }
}