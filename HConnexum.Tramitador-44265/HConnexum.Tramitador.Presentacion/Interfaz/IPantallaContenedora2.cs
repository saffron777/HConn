using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IPantallaContenedora2 : InterfazBase
	{
		string Suscriptor { get; set; }
		string Servicio { get; set; }
		string FechaSolicitud { get; set; }
		IEnumerable<ListasValorDTO> ComboTipDoc { set; }
		string TipDoc { get; set; }
		string NumDoc { get; set; }
		string Nombres { get; set; }
		string Apellidos { get; set; }
		string Email { get; set; }
		string Telefono { get; set; }
		string CasoRelacionado { get; set; }
		int IdFlujoServicio { get; set; }
		string NombrePagina { get; set; }
		string Errores { set; }
		string Mensaje { get; set; }
	}
}
