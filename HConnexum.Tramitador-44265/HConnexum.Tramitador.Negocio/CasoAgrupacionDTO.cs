using System;
using System.Collections.Generic;

///<summary>Namespace que engloba la clase DTO de la capa de negocio HC_Configurador.</summary>
namespace HConnexum.Tramitador.Negocio
{
    ///<summary>Clase CasoAgrupacionDTO.</summary>
    public class CasoAgrupacionDTO
    {
        public int Id { get; set; }
        public int IdCaso { get; set; }
        public int IdAgrupacion { get; set; }
        public int IdServicio { get; set; }
        public int IdSolicitud { get; set; }
        public int IdSuscriptor { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int CreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? ModificadoPor { get; set; }
        public bool IndVigente { get; set; }
        public bool IndEliminado { get; set; }
        public string Tomado { get; set; }
        public string UsuarioTomado { get; set; }
        public string NombreAgrupacion { get; set; }
    }
}
