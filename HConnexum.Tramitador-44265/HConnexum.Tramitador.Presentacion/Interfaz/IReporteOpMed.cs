using System;
using System.Linq;

///<summary>Namespace que engloba la interfaz del Repote de Opinión Médica de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface de Repote de Opinión Médica.</summary>
	public interface IReporteOpMed //: InterfazBase
	{
		int IdMovimiento { get; set; }
		string Antecedentes { get; set; }
		string Cicatrices { get; set; }
		string Peso { get; set; }
		string Talla { get; set; }
		string TensionArterial { get; set; }
		string Observacionmed { get; set; }
		string OpMed { get; set; }
		string Errores { set; }
	}
}