﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion
{
    public interface IReporteCartaAvalIbero : InterfazBase
    {
        int idCarta { get; }
        int idSuscriptor { get; }
    }
}
