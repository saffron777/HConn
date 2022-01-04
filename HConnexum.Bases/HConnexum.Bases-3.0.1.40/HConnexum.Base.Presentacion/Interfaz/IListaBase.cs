using System.Collections;
using System.Collections.Generic;

namespace HConnexum.Base.Presentacion.Interfaz
{
	public interface IListaBase : IBase
	{
		///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
		IEnumerable Datos { set; }
		///<summary>Propiedad para obtener o asignar el Nro de registros.</summary>
		int NumeroDeRegistros { get; set; }
		string Orden { get; }
		int NumeroPagina { get; }
		int TamanoPagina { get; }
		/// <summary>
		/// Se eliminará en futuras versiones, no se recomienda su uso
		/// </summary>
		IList<HConnexum.Infraestructura.Filtro> ParametrosFiltroOriginal { get; }
		string ParametrosFiltro { get; set; }
		bool IndRolEliminado { get; }
		IList<string> IdsEliminar { get; }
	}
}