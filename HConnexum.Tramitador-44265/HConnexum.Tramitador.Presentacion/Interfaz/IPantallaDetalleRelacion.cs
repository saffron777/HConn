using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IPantallaDetalleRelacion:InterfazBase
	{
		string NRelacion { set; get; }
		string FechaCreacion { set; get; }
		string Banco { set; get; }
		string FechaPago { set; get; }
		string Status { set; get; }
		string Referencia { set; get; }
		string FormaPago { set; get; }
		string MontoCubierto { set; get; }
		string TotalRetenido { set; get; }
		string MontoSujetoRetencion { set; get; }
		string MontoImpMunicipal { set; get; }
		string TotalPagar { set; get; }
		string NumeroCasos { set; get; }
		int NumeroDeRegistros { set; get; }
		int Nremesa{ get; }
		string ConexionString { get; }
		int IdCodExterno { get; }
		int IdIntermediario { get; }
		bool Imprimir { set; }
		string Errores { set; }
	}
}
