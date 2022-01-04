using System.Data;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface PasoMaestroDetalle.</summary>
	public interface IPasoMaestroDetalle : InterfazBase
	{
		int Id { get; set; }
		string NombreServicio { set; }
		string NombreEstatus { set; }
		string NombreEtapa { set; }
		string NombreVersion { set; }
		string Nombre { get; set; }
		string IdTipoPaso { get; set; }
		IEnumerable<TipoPasoDTO> ComboIdTipoPaso { set; }
		string IdEstatusInicial { get; set; }
		IEnumerable<ListasValorDTO> ComboIdEstatusInicial { set; }
		string SlaTolerancia { get; set; }
		string IdSubServicio { get; set; }
		List<FlujosServicioDTO> ComboIdSubServicio { set; }
		string CantidadRepeticion { get; set; }
		string Reintentos { get; set; }
		string Observacion { get; set; }
		string PgmObtieneRespuestas { get; set; }
		string URL { get; set; }
		string Metodo { get; set; }
		string EtiqSincroIn { get; set; }
		string EtiqSincroOut { get; set; }
		string IdAlerta { get; set; }
		DataTable ComboIdAlerta { set; }
		string PorcSlaCritico { get; set; }
		string OrdenPaso { get; set; }
		string IndIniciaEtapa { get; set; }
		string IndSeguimiento { get; set; }
		string IndAgendable { get; set; }
		string IndRequiereRespuesta { get; set; }
		string IndSegSubServicio { get; set; }
		string IndObligatorio { get; set; }
		string IndCerrarEtapa { get; set; }
		string IndCerrarServicio { get; set; }
		string IndEncadenado { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
		IEnumerable<PasosRepuestaDTO> Datos { set; }
		int NumeroDeRegistros { get; set; }
		string Errores { set; }
		string ErroresCustomEditar { set; }
		string MetodoAsignacion { get; set; }
		string MetodoAsignacionCha { get; set; }
		string IndAnulacion { get; set; }
	}
}