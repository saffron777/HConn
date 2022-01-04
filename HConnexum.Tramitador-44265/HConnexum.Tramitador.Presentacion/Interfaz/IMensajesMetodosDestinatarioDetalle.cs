using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface MensajesMetodosDestinatarioDetalle.</summary>
    public interface IMensajesMetodosDestinatarioDetalle : InterfazBase
	{
	int Id { get; set; }
		string IdPaso { get; set; }
		IEnumerable<PasoDTO>ComboIdPaso { set; }
		string IdMensaje { get; set; }
        string Rutina { get; set; }
        DataTable ListBoxCasosMovimientosNoAsociados { set; }
        DataTable ComboIdMensaje { set; }
        IList<MensajesMetodosDestinatarioDTO> ValorBusqueda { get; set; }
        IList<MensajesMetodosDestinatarioDTO> ListBoxCasosMovimientosAsociados { set; }
        Dictionary<int, string> CasosMovimientosAsociados { get; }
        string Constantes { get; set; }
        string CreadoPor { get; set; }
        string FechaCreacion { get; set; }
        string ModificadoPor { get; set; }
        string FechaModificacion { get; set; }
        string IndVigente { get; set; }
        string IndEliminado { get; set; }
        string FechaValidez { get; set; }
		string Errores { set; }
        string ErroresCustomEditar { set; }
	}
}