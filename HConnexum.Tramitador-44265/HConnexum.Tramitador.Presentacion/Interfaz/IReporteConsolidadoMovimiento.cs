using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IReporteConsolidadoMovimiento:InterfazBase
	{
		string Errores { set; }
		string Usuario { set; get; }
	}
}
