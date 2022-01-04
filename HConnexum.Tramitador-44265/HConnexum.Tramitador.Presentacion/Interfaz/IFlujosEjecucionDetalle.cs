using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    ///<summary>Interface FlujosEjecucionDetalle.</summary>
    public interface IFlujosEjecucionDetalle : InterfazBase
    {
        int Id { get; set; }
        string IdPasoOrigen { get; set; }
        IEnumerable<PasoDTO>ComboIdPasoOrigen { set; }
        string IdPasoDestino { get; set; }
        IEnumerable<PasoDTO>ComboIdPasoDestino { set; }
        string Condicion { get; set; }
        string CreadoPor { get; set; }
        string FechaCreacion { get; set; }
        string ModificadoPor { get; set; }
        string FechaModicacion { get; set; }
        string IndVigente { get; set; }
        string FechaValidez { get; set; }
        string IndEliminado { get; set; }
        string Errores { set; }
    }
}