// Example header text. Can be configured in the options.
using System;
using System.Collections;
using System.Linq;

///<summary> Clase que contiene el set de registros y los datos necesarios para realizar el paginado de registros </summary>
namespace HConnexum.Base.Dtos.ObjetosUtilitariosDto
{
	public class DatosMultiRegistroDto
	{
		public IEnumerable ColeccionRegistros { get; set; }
		public int PaginaSeleccionada { get; set; }
		public int CantidadTotalRegistros { get; set; }
		public int CantidadTotalPaginas { get; set; }
	}
}