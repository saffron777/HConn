using System;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface ChadePasoDetalle.</summary>
    public interface IChadePasoDetalle : InterfazBase
	{
	    int Id { get; set; }
		string IdCargosuscriptor { get; set; }
        DataTable ComboIdCargosuscriptor { set; }
		string IdHabilidadSuscriptor { get; set; }
        DataTable ComboIdHabilidadSuscriptor { set; }
		string IdAutonomiaSuscriptor { get; set; }
        DataTable ComboIdAutonomiaSuscriptor { set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
		string Errores { set; }
        string ErroresCustomEditar { set; }
	}
}