using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz 
{
	///<summary>Interface EtapaMaestroDetalle.</summary>
    public interface IEtapaMaestroDetalle : InterfazBase
	{
	    int Id { get; set; }
		string IdFlujoServicio { get; set; }
        IList<FlujosServicioDTO> ComboIdFlujoServicio { set; }
		string Nombre { get; set; }
		string Orden { get; set; }
		string SlaPromedio { get; set; }
		string SlaTolerancia { get; set; }
		string IndObligatorio { get; set; }
		string IndRepeticion { get; set; }
		string IndSeguimiento { get; set; }
		string IndInicioServ { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
        string ModificadoPor { get; set; }
        string FechaModificacion { get; set; }
		string IndCierre { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
		IEnumerable<PasoDTO> Datos { set; }
		int NumeroDeRegistros { get; set; }
        string ErroresCustom { set; }
        string ErroresCustomEditar { set; }
		string Errores { set; }
	}
}
