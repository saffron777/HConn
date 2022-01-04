using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
	public interface IListadoMovimientos : InterfazBaseBloques
	{
		///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
		IEnumerable<ListaMovimientosDTO> Datos { get; set; }
		///<summary>Propiedad para asignar errores desde BD.</summary>
		string Errores { set; }
		///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
		int NumeroDeRegistros { get; set; }
	}
}