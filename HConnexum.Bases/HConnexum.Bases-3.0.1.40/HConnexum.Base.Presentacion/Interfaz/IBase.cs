using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Seguridad;

namespace HConnexum.Base.Presentacion.Interfaz
{
	public interface IBase
	{
		UsuarioActual UsuarioActual { get; }
		string TipoMensaje { get; set; }
		string NombreTabla { get; set; }
		string Errores { set; }
		string Mensaje { set; }
		string Notificacion { set; }
		int IdPaginaModulo { get; }
		AuditoriaDto AuditoriaDto { get; }
	}
}