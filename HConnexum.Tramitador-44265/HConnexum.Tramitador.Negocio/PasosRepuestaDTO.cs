using System;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
    ///<summary>Clase PasosRepuestaDTO.</summary>
    public class PasosRepuestaDTO
    {
        public int Id { get; set; }
        public string ValorRespuesta { get; set; }
        public string NombreRespuesta { get; set; }
        public int IdPaso { get; set; }
        public int Orden { get; set; }
        public bool IndCierre { get; set; }
        public int CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool IndVigente { get; set; }
        public DateTime? FechaValidez { get; set; }
        public bool IndEliminado { get; set; }
        public string Tomado { get; set; }
        public string UsuarioTomado { get; set; }
        public string IdEncriptado { get; set; }
    }
}