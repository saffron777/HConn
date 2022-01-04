using HConnexum.Tramitador.Negocio;
using HConnexum.Seguridad;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface InterfazBase
	{
		UsuarioActual UsuarioActual { get; }
		string NombreTabla { get; set; }
		string Errores { set; }
	}
}