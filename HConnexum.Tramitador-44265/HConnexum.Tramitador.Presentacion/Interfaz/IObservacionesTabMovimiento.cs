using System;
using System.Linq;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    public interface IObservacionesTabMovimiento: InterfazBase
    {
        ///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
        IEnumerable<ObservacionesMovimientosDTO> Datos { set; }

        ///<summary>Propiedad para asignar errores desde BD.</summary>
        string Errores { set; }

        ///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
        int NumeroDeRegistros { get; set; }
    }
}
