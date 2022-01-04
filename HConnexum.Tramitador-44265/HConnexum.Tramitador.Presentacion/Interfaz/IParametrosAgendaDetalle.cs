using System;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface ParametrosAgendaDetalle.</summary>
	public interface IParametrosAgendaDetalle : InterfazBase
	{
		int Id { get; set; }
		string Paso { get; set; }
		string KeyFechaEjec { get; set; }
		string KeyHoraEjec { get; set; }
		string IndInmediato { get; set; }
		string Cantidad { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string FechaValidez { get; set; }
		string IndVigente { get; set; }
		string IndEliminado { get; set; }
		string Errores { set; }
		string ErroresCustomEditar { set; }
	}
}