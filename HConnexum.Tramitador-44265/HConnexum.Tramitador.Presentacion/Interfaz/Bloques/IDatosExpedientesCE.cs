using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
	///<summary>Interface DatosExpedientesCE.</summary>
	public interface IDatosExpedientesCE : InterfazBaseBloques
	{
		string Clave { get; set; }
		string FechaOcurrencia { get; set; }
		string UltimoMovimientoHecho { get; set; }
		string Categoria { get; set; }
		string Responsable { get; set; }
		string Sintomas { get; set; }
		string DiasHospitalizacion { get; set; }
		string Observaciones { get; set; }
		string ObervacionesProcesadas { get; set; }
		string DocumentosFaxSolicitados { get; set; }
		string Errores { get; set; }
		IEnumerable<PolizasDTO> Datos { set; }
	}
}
