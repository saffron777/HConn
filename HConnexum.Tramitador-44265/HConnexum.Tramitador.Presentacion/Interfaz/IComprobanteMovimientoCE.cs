using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IComprobanteMovimientoCE : InterfazBase
	{
		int IdMovimiento { get; }
		int Seguro { get; }
		string Errores { set; }
	}
}
