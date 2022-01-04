using System;
using System.Linq;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
	public interface IListadoMovimientosCA : InterfazBaseBloques
	{
		IEnumerable<ListaMovimientosDTO> Datos { get; set; }
		string Errores { set; }
		int NumeroDeRegistros { get; set; }
	}
}
