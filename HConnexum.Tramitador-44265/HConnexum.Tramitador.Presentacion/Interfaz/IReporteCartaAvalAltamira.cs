using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
     public interface IReporteCartaAvalAltamira : InterfazBase
    {
        int idCarta { get; }
        int idSuscriptor { get; }
    }
}
