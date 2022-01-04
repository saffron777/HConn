using System;
using System.Data;
///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface ServicioSucursalDetalle.</summary>
    public interface IServicioSucursalDetalle : InterfazBase
	{
	    int Id { get; set; }
        string Servicio { get; set; }
		string IdSucursal { get; set; }
        DataTable ComboIdSucursal { set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
        string ErroresCustomEditar { set; }
		string Errores { set; }
	}
}