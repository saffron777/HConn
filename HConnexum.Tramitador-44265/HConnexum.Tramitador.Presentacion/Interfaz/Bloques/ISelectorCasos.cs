using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;
using System.Data;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    public interface ISelectorCasos : InterfazBase
	{
        //string Suscriptor { set; }
        //string TipoDoc { set; }
        //string DocSolicitante { set; }
        DataTable ComboSuscriptores { set; }

        IEnumerable<FlujosServicioDTO> ComboServicios { set; }
        DataTable ComboEstatus { set; }
        string IdComboSuscriptores { get; }
        string IdComboSuscriptorASimular { get; }
        string IdComboServicios { get; }
        string IdServicioSuscriptor { get; }
        string TipoFiltro { get; }
        string Filtro { get; }
        string FechaDesde { get; }
        string FechaHasta { get; }
        string IdComboEstatus { get; }
        string Asegurado { get; }
        string Intermediario { get; }
        int idIntermediario { get; set; }
        IEnumerable<CasoDTO> Datos { set; }
        int NumeroDeRegistros { get; set; }
        string NombreServicioSuscriptor { get; }
        bool inicio { get; }

        IEnumerable<ServiciosSimuladoDTO> ComboServicioSuscriptor { set; }
        DataTable ComboSuscriptorASimular { set; }

        string Errores { set; }
	}
}
