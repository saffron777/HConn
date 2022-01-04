using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IReporteDetalleMovimiento:InterfazBase
	{
		string Errores { set; }
		string Usuario { set; get; }
	}
}
