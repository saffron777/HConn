using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    ///<summary>Interface PasoMaestroDetalle.</summary>
    public interface IConsultaCasosOpinionMedica : InterfazBase
    {
        IEnumerable<ConsultaOpinionMedicaDTO> Datos { set; }
        int NumeroDeRegistros { get; set; }
        string Errores { set; }
		string Asegurado { get; set; }
		string FechaInicial { get; }
		string  FechaFinal { get; }
		string TipoFiltro { get; }
		string Filtro { get; }
    }
}