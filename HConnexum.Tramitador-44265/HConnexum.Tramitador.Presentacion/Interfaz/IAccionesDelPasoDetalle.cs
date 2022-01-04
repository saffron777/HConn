using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface FlujosEjecucionDetalle.</summary>
    public interface IAccionesDelPasoDetalle : InterfazBase
	{
	    int Id { get; set; }
        int IdEtapa { get; set; }
        IEnumerable<EtapaDTO> ComboIdEtapa { set; }
		int IdPasoOrigen { get; set; }
		IEnumerable<PasoDTO>ComboIdPasoOrigen { set; }
        int IdPasoRespuesta { get; set; }
        IEnumerable<PasosRepuestaDTO> ComboddlIdPasoRespuesta { set; }
        int IdTipoPaso { get; set; }
        IEnumerable<TipoPasoDTO> ComboIdTipoPaso { set; }
		int IdPasoDestino { get; set; }
		IEnumerable<PasoDTO>ComboIdPasoDestino { set; }
        string Condicion { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
        string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
        string ErroresCustomEditar { set; }
        int IdPasoDesborde { get; set; }
        IEnumerable<PasoDTO> ComboIdPasoDesborde { set; }
        string IndReinicioRepeticion { get; set; }
		string Errores { set; }
	}
}