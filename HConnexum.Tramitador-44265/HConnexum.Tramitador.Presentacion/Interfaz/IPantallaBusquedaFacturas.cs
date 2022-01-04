using System;
using System.Linq;
using System.Data;


namespace HConnexum.Tramitador.Presentacion.Interfaz
{	
	public interface IPantallaBusquedaFacturas : InterfazBase
	{
		DataTable ComboIntermediario{ set; }
		//IEnumerable<SuscriptorDTO> ComboIntermediario { set; }
		int IdIntermediario{ get; }
		DataTable ComboTipoProveedor { set; }
        DataTable ComboProveedor { set; }
		int? IdTipoProveedor { get; }
        string IdProveedor { get; }
		string RadioButtonEstatus { get; }
		DateTime? FechaInicial{ get; }
		DateTime? FechaFinal{ get; }
		int? NumeroReclamo { get; }
		string NumeroCedula { get; }
        string NumeroFactura { get; }
		int NumeroDeRegistros{ get;set;}
		string ConexionString { get; }
		int IdCodExterno { get; }
		string Errores { set; }
	}
}
