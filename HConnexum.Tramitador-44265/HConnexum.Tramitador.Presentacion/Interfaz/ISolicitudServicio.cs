using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface ISolicitudServicio : InterfazBase
	{
		DataSet Datos { get; set; }
		string IdSuscriptor { get; set; }
        DataTable ComboSuscriptor { set; }
		string IdServicio { get; set; }
        DataTable ComboServicio { set; }
        DataTable ComboProveeServSimulados { set; }
	}
}
