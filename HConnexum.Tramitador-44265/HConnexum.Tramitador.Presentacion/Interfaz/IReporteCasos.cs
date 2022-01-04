using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IReporteCasos:InterfazBase
	{
		string Errores { set; }
		string Usuario { set; get; }
	}
}
