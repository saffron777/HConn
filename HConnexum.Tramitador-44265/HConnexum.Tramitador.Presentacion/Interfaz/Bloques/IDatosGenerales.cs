///<summary>Namespace que engloba la interfaz de Bloques de la capa presentación HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface DatosGenerales.</summary>
	public interface IDatosGenerales : InterfazBaseBloques
	{
		string PTipDocASeg { get; set; }
		string PNumDocASeg { get; set; }
		string PNomAseg { get; set; }
		string PContratante { get; set; }
		string PNomClinica { get; set; }
		string PServicio { get; set; }
		string PDiagnostico { get; set; }
		string PIdExterno { get; set; }
		string PEspecialidad { get; set; }
		string PTlfAseg { get; set; }
		string PIdCaso { get; set; }
	}
}