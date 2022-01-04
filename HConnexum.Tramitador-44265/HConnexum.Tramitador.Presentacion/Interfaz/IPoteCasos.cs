using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
   public  interface IPoteCasos:InterfazBase
    {
        string Suscriptor { get; }
        DataTable ComboSuscriptores { set; }

        string Asegurado { get; }
        string Intermediario { get; }
        int idIntermediario { get; set; }
        string FechaDesde { get; }
        string FechaHasta { get; }
        string TipoFiltro { get; }
        string Filtro { get; }
        string Servicio { get; }
        IEnumerable<FlujosServicioDTO> ComboServicios { set; }
        IEnumerable<PoteCasoDTO> Datos { set; }
        int NumeroDeRegistros { get; set; }
        bool BIndSimulado { get; set; }       
        string Errores { set; }
    }
    
}
