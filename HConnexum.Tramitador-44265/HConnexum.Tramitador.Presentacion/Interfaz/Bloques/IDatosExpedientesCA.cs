using System;
using System.Linq;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
	public interface IDatosExpedientesCA : InterfazBaseBloques
	{
        string Sintomas { get; set; }
		string Clave { get; set; }
		string UltimoMovimientoHecho { get; set; }
		string Categoria { get; set; }
		string Responsable { get; set; }
		string DiasHospitalizacion { get; set; }
		string Observaciones { get; set; }
		string ObervacionesProcesadas { get; set; }
		string DocumentosFaxSolicitados { get; set; }
		string FechaSolicitud { get; set; }
		string FechaNotificacion { get; set; }
		string FechaVencimiento { get; set; }
		string FechaOcurrencia { get; set; }
		string MedicoTratante { get; set; }
		string Errores { get; set; }
        IEnumerable<PolizasDTO> Datos { set; }
	}
}
