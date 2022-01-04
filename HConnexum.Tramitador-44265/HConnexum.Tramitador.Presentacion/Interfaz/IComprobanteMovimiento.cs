using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IComprobanteMovimiento : InterfazBase
	{
		string Errores { set; }
	}
}
