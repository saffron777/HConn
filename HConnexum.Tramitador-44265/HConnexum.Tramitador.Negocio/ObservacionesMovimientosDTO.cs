using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HConnexum.Tramitador.Negocio
{
 public  class ObservacionesMovimientosDTO
    {
     public int Id { get; set; }
     public string usuario { get; set; }
     public DateTime Fecha { get; set; }
     public string Observacion { get; set; }
     public string IdEncriptado { get; set; }

     public int CreadoPor { get; set; }
     public DateTime FechaCreacion { get; set; }
     public int? ModificadoPor { get; set; }
     public DateTime? FechaModificacion { get; set; }
     public bool IndVigente { get; set; }
     public DateTime? FechaValidez { get; set; }
     public bool? IndEliminado { get; set; } ///ojo noo bool?
    }
}
