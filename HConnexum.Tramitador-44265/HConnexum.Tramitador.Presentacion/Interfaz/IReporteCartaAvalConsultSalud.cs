using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    public interface IReporteCartaConsultSalud : InterfazBase
    {
        int idCarta { get; }
        int idSuscriptor { get; }
    }
}
