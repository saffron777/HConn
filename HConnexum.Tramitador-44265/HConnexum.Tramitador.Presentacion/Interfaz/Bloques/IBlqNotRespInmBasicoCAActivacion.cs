///<summary>Namespace que engloba la interfaz de Bloques de la capa presentación HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface BlqNotRespInmCAActivacion.</summary>
	public interface IBlqNotRespInmBasicoCAActivacion : InterfazBaseBloques
	{
		string Mensaje { get; set; }
		bool MostrarImprimir { get; set; }
		string PActivacionCa { get; set; }
		string PEstatusMovimientoWeb { get; set; }
	}
}
