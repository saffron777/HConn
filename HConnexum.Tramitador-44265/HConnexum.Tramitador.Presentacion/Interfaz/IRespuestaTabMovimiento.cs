using System;
using System.Linq;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    public interface IRespuestaTabMovimiento: InterfazBase
    {
        
        string Oma { get; set; }
       
        string Errores { set; }
    }
}
