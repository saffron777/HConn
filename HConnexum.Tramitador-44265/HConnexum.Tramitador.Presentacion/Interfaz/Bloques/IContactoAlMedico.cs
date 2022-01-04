using System;
using System.Linq;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    /// <summary>Interfaz del control Web de usuario 'Observacion'.</summary>
    public interface IContactoAlMedico : InterfazBaseBloques
    {
        /// <summary>Identificador único del control Web de usuario.</summary>
        IEnumerable<ListasValorDTO> ComboMedicoContacto { set; }
    }
}