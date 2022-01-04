using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IPantallaDetalleFactura:InterfazBase
	{
		//string NRelacion { set; get; }
		string FechaRecepcion { set; get; }
		string FechaEmision { set; get; }
		string Nfactura {set; get; }
		string Estatus {set; get; }
		string Ncontrol {set; get; }
		int NumeroDeRegistros { set; get; }
		string ConexionString { get; }
		int IdCodExterno { get; }
		int IdIntermediario { get; }
		string Errores { set; }
		string MontoCubierto { set; get; }
		string MontoSujetoRetencion { set; get; }
		string MontoImpMunicipal { set; get; }
		string TotalImpIsrl { set; get; }
		string TotalImp { set; get; }
		string ImpIva { set; get; }
		string TotalRetenido { set; get; }
		
	}
}
