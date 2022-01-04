using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface AlcanceGeograficoDetalle.</summary>
    public interface IAlcanceGeograficoDetalle : InterfazBase
	{
	    int Id { get; set; }
        string IdFlujoServicio { get; set; }
        string IdServicioSucursal { get; set; }
        List<ServicioSucursalDTO> ComboServicio { set; }
        string IdSucursal { get; set; }
        List<ServicioSucursalDTO> ComboSucursal { set; }
        string IdPais { get; set; }
        DataTable ComboPais { set; }
		string IdDiv1 { get; set; }
        DataTable ComboDiv1 { set; }
		string IdDiv2 { get; set; }
        DataTable ComboDiv2 { set; }
		string IdDiv3 { get; set; }
        DataTable ComboDiv3 { set; }
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