using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IReporteRelaciones:InterfazBase
	{
		string ConexionString { get; } 
		string Usuario { get;}
		//string Relaciones { get; set; }
		//string Casos { get; set; }
		//string MontoCubierto { get; set; }
		//string Retenido { get; set; }
		//string ImpMunicipal { get; set; }
		//string MontoTotal { get; set; } 
		string Errores { set; }
	}
}
