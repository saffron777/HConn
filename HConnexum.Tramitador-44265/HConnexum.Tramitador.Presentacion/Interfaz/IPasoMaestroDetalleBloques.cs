using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    ///<summary>Interface PasoMaestroDetalle.</summary>
    public interface IPasoMaestroDetalleBloques : InterfazBase
    {
        int Id { get; set; }
        string Nombre { get; set; }
        IEnumerable<PasosBloqueDTO> Datos { set; }
        int NumeroDeRegistros { get; set; }
        string Errores { set; }
    }
}