using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IReporteCartaAval : InterfazBase
	{
		IEnumerable<ConsultaCartaAvalDTO> Datos { set; }
		int NumeroDeRegistros { get; set; }
		string Errores { set; }
        string Intermediario { get; }
        string FechaDesde { get; }
        string FechaHasta { get; }
        string TipoFiltro { get; }
        string Filtro { get; }
		string Asegurado { get; set; }

	}
}
