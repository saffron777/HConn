using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IReporteFacturas : InterfazBase
	{
		string ConexionString { get; } 
		string Usuario { get;}
		string Errores { set; }
	}
}