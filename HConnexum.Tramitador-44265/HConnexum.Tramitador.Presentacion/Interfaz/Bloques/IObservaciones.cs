using System;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    /// <summary>Interfaz del control Web de usuario 'Observacion'.</summary>
    public interface IObservaciones : InterfazBaseBloques
    {
        /// <summary>Identificador único del control Web de usuario.</summary>
        string Id { get; set; }
    }
}