using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    ///<summary>Interface PasoMaestroDetalle.</summary>
    public interface IConsultaCasos : InterfazBase
    {
        string Suscriptor { set; }
        string TipoDoc { set; }
        string DocSolicitante { set; }
      //  IEnumerable<Suscriptor> ComboSuscriptores { set; }
        DataTable ComboSuscriptores { set; }
        IEnumerable<FlujosServicioDTO> ComboServicios { set; }
        DataTable ComboEstatus { set; }
        string IdComboSuscriptores { get; }
        string IdComboSuscriptorASimular { get; }
        string IdComboServicios { get; }
        string IdServicioSuscriptor { get; }
        string NombreServicioSuscriptor { get; }
        string TipoFiltro { get; }
        string Filtro { get; }
        string FechaDesde { get; }
        string FechaHasta { get; }
        string IdComboEstatus { get; }
        IEnumerable<CasoDTO> Datos { set; }
        int NumeroDeRegistros { get; set; }
        string Asegurado { get; }
        int idIntermediario { get; set; }
        string Intermediario { get; }
        bool inicio { get; }
        DataTable ComboSuscriptorASimular { set; }
        string Errores { set; }
    }
}