using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface FlujosServicioMaestroDetalle.</summary>
	public interface IFlujosServicioMaestroDetalle : InterfazBase
	{
		int Id { get; set; }
		string IndPublico { get; set; }
		string IdSuscriptor { get; set; }
		string HiddenId { get; set; }
		string HiddenTipo { get; set; }
		string Tipo { get; set; }
		string IdOrigen { get; }
		string IdPasoInicial { get; set; }
		IEnumerable<PasoDTO> ComboIdPasoInicial { set; }
		string IdServicioSuscriptor { get; set; }
		DataTable ComboIdServicioSuscriptor { set; }
		string SlaTolerancia { get; set; }
		string SlaPromedio { get; set; }
		IEnumerable<ListasValorDTO> ComboIdPrioridad { set; }
		int IdPrioridad { get; set; }
		int Version { get; set; }
		string IndCms { get; set; }
		string IndBloqueGenericoSolicitud { get; set; }
		string MetodoPreSolicitud { get; set; }
		string MetodoPostSolicitud { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModicacion { get; set; }
		string FechaValidez { get; set; }
		string IndVigente { get; set; }
		string IndEliminado { get; set; }
		IEnumerable<Etapa> Datos { set; }
		int NumeroDeRegistros { get; set; }
		string Errores { set; }
		string ErroresCustom { set; }
		string Confirm { set; }
		string XMLEstructura { get; set; }
		int Casos { set; }
        string IndChat { get; set; }
        string NombrePrograma { get; set; }
        string IndSimulable { get; set; }
        
	}
}
