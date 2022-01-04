using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    public interface IReporteCartaAvalMI: InterfazBase
    {
        //string Errores { set; }
        int idCarta { get; }
        int idSuscriptor { get; }
    }
}
