using System;
using System.Linq;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    /// <summary>Interfaz del control Web de usuario 'Anulacion'.</summary>
    public interface IAnulacion : InterfazBaseBloques
    {
        /// <summary>Identificador único del control Web de usuario.</summary>
        string Id { get; set; }
        string ObservacionporAnulacion { get; set; }
        /// <summary>Lista de valores que se utilizan para llenar el control Web de usuario.</summary>
        IEnumerable<ListasValorDTO> ComboAprobacion { set; }
    }
}