using System;
using System.Linq;
using System.Data;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IBusquedaRelaciones : InterfazBase
	{
		DataTable ComboIntermediario{ set; }
		int IdIntermediario{ get; }
		DataTable ComboTipoProveedor { set; }
        DataTable ComboProveedor { set; }
		string IdTipoProveedor { get; }
        string IdProveedor { get; }
		string RadioButtonEstatus { get; }
		DateTime? FechaInicial{ get; }
		DateTime? FechaFinal{ get; }
		int? NumeroRelacion { get; }
        string NumeroFactura { get; }
		int NumeroDeRegistros{ get;set;}
		string Relaciones { set; }
		int Casos { set; }
		string ImpMunicipal { set; }
		string MontoCubierto { set; }
		string Retenido { set; }
		string MontoTotal { set; }
		string ConexionString { get; }
		int IdCodExterno { get; }
		bool ImprimirVisible { set; }
		bool Totales { set; }
		string Errores { set; }
	}
}
