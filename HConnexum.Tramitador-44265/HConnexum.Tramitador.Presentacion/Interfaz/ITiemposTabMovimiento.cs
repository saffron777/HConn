using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
   public  interface ITiemposTabMovimiento:InterfazBase
    {
        string FechaCreacion{ get; set; }
       
        string Atencion{ get; set; }
       
        string FechaAtencion{ get; set; }
        
        string Ejecucion{ get; set; }
        string FechaEjecucion{ get; set; }

        string TiempoEstimado { get; set; }
        string Errores { set; }
    }
}
