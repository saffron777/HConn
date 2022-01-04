using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    public interface IContactoInicial: InterfazBaseBloques
    {

         string PAfiliadoContacto {get; set;}
         IEnumerable<ListasValorDTO> ComboAfiliadoContacto { set; }
         DateTime Pfechaproxllamada {get; set;}
         DateTime Phoraproxllamada {get; set;}
         string PCambioDeMedico {get; set;}
         IEnumerable<ListasValorDTO> ComboCambioDeMedico { set; }
         bool PSolicitudAnulacion { get; set; }
         string ObservacionporAnulacion { get; set; }

    }
}
