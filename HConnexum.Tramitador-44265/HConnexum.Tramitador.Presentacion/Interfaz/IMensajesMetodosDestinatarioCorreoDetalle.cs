using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface MensajesMetodosDestinatarioDetalle.</summary>
    public interface IMensajesMetodosDestinatarioCorreoDetalle : InterfazBase
	{
	    int Id { get; set; }
		string IdPaso { get; set; }
		IEnumerable<PasoDTO>ComboIdPaso { set; }
		string IdMensaje { get; set; }
        string TipoBusquedaDestinatario { get; set; }
		string TipoBusquedaDestinatarioPara { get; set; }
        string TipoBusquedaDestinatarioCC { get; set; }
        string TipoBusquedaDestinatarioCCO { get; set; }
        string TipoBusquedaDestinatarioPara1 { get; set; }
        string TipoBusquedaDestinatarioCC1 { get; set; }
        string TipoBusquedaDestinatarioCCO1 { get; set; }
        string Rutina { get; set; }
		string ValorBusqueda { get; set; }
		string TipoPrivacidad { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
        string FechaValidez { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string IndEliminado { get; set; }
        DataTable ComboIdMensaje { set; }
        DataTable ListBoxCasosMovimientos { set; }
		string Errores { set; }
        string ErroresCustomEditar { set; }
	}
}