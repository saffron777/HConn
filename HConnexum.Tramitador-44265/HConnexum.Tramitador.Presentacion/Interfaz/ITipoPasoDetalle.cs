///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface TipoPasoDetalle.</summary>
    public interface ITipoPasoDetalle : InterfazBase
	{
	int Id { get; set; }
		string Descripcion { get; set; }
		string Programa { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
		  
		string Errores { set; }
	}
}