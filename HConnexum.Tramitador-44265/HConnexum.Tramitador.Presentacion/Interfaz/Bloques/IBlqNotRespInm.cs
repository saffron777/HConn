///<summary>Namespace que engloba la interfaz de Bloques de la capa presentación HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface NotifRespInmNueMov.</summary>
	public interface IBlqNotRespInm : InterfazBaseBloques
	{
		string PIdReclamo { get; set; }
		string PEstatusMovimientoWeb { get; set; }
		string PIndMvtoAutomatico { get; set; }
		string Mensaje { get; set; }
		bool MostrarImprimir { get; set; }
        string PIdSupportIncident { get; set; }
	}
}