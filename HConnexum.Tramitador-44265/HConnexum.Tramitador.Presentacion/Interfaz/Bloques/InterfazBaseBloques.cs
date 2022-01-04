using System.Collections.Generic;
using HConnexum.Seguridad;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface InterfazBaseBloques
	{
		int IdFlujoServicio { get; set; }
		int IdMovimiento { get; set; }
		UsuarioActual UsuarioActual { get; }
		Dictionary<string, string> ParametrosEntrada { get; set; }
		string Errores { set; }
	}
}