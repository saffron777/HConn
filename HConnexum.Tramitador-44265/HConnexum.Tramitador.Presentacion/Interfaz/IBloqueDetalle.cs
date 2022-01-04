///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
using System.Data;
using HConnexum.Tramitador.Negocio;
using System.Collections.Generic;
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface BloqueDetalle.</summary>
    public interface IBloqueDetalle : InterfazBase
	{
	int Id { get; set; }
		string Nombre { get; set; }
        string NombrePrograma { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
        string IndEliminado { get; set; }
        string IdListaValor { get; set; }
        DataTable ComboIdListaValor { set; }
		string Errores { set; }
	}
}