using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface ServiciosSimuladoDetalle.</summary>
    public interface IServiciosSimuladoDetalle : InterfazBase
	{
	int Id { get; set; }
		string IdServicioSuscriptor { get; set; }
		string IdSuscriptorASimular { get; set; }
		string FechaInicio { get; set; }
		string FechaFin { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
		string IndVigente { get; set; }

        IEnumerable<ServiciosSimuladoDTO> ComboIdServicioSuscriptor { set; }
        DataTable ComboIdSuscriptorASimular { set; }

		string Errores { set; }
	}
}