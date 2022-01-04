using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    public interface IActualizacionDeContacto : InterfazBaseBloques
    {
        string Ptlfaseg { get; set; }
        string IdNuevoContacto { get; set; }
        IEnumerable<ListasValorDTO> ComboNuevoContacto { set; }
    }
}