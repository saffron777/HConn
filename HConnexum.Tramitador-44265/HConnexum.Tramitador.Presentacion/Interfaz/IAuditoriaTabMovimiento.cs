using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
    public interface IAuditoriaTabMovimiento: InterfazBase
    {
        string CreadoPor { get; set; }
        string FechaCreacion { get; set; }
        string ModificadoPor { get; set; }
        string FechaModificacion { get; set; }
        string FechaEjecucion { get; set; }
        string FechaOmision { get; set; }
        string Errores { set; }
    }
}
