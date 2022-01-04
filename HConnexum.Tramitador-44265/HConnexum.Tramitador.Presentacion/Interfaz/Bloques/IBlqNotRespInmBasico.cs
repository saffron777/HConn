///<summary>Namespace que engloba la interfaz de Bloques de la capa presentación HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface NotifRespInmNueMov.</summary>
	public interface IBlqNotRespInmBasico : InterfazBaseBloques
	{
		string Mensaje { get; set; }
		bool MostrarImprimir { get; set; }
	}
}