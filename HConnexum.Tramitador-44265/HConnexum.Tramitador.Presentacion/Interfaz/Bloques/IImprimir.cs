using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
	public interface IImprimir : InterfazBaseBloques
	{
		string Pantecedentes { get; set; }
		string Pcicatrices { get; set; }
		string Ppeso { get; set; }
		string Ptalla { get; set; }
		string Ptension { get; set; }
		string Pobservacionmed { get; set; }
		string IdDecisionSimple { get; set; }
		IEnumerable<ListasValorDTO> ComboDecisionSimple { set; }
		/// <summary>Fecha de la opinión médica.</summary>
		DateTime Fecha { get; set; }
	}
}