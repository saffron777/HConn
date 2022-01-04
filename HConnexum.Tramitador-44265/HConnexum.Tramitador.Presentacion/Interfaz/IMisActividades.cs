using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface IMisActividades.</summary>
	public interface IMisActividades : InterfazBase
	{
		int Id { get; set; }
		string NombreUsuario { get; set; }
		string NombreSuscriptor { get; set; }
        IList<FlujosServicioDTO> DatosFlujos { set; }
		IList<MovimientoDTO> DatosMovimientos { set; }
        IEnumerable<MovimientoDTO> DatosGrid { set; }
		string Errores { set; }
		bool BIndSimulado { get; set; }
		string ContadorActDisMasSimulados { get; set; }
        string getSessionSkin { get; }
	}
}