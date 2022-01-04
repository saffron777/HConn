using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Configurador.Presentacion.Interfaz
{

	///<summary>Interface CasoAgrupacionDetalle.</summary>
    public interface ICasoAgrupacionDetalle : InterfazBase
	{
	int Id { get; set; }

        IList<CasoAgrupacionDTO> ListBoxCasosAgrupacionesNoAsociados { set; }
        IList<CasoAgrupacionDTO> ListBoxCasosAgrupacionesAsociados { set; }
        IList<CasoAgrupacionDTO> CasoAgrupacion { get; set; }
        Dictionary<int, string> CasosAsociados { get; }
		string Errores { set; }
	}
}