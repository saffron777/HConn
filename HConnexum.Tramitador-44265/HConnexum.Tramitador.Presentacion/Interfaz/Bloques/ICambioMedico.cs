using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;
using System.Data;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    /// <summary>Interfaz del control Web de usuario 'CambioMedico'.</summary>
    public interface ICambioMedico : InterfazBaseBloques
    {
        #region M I E M B R O S   P Ú B L I C O S
        /// <summary>Identificador único del control Web de usuario.</summary>
        string Id { get; set; }
        /// <summary>Nombre del médico.</summary>
        string MedicoNombre { get; set; }
        /// <summary>Identificador único del país del médico.</summary>
        string MedicoPais { get; set; }
        /// <summary>Identificador único de la división territorial 1 del médico.</summary>
        string MedicoDivisionTerritorial1 { get; set; }
        /// <summary>Identificador único de la división territorial 2 del médico.</summary>
        string MedicoDivisionTerritorial2 { get; set; }
        /// <summary>Identificador único de la división territorial 3 del médico.</summary>
        string MedicoDivisionTerritorial3 { get; set; }
        /// <summary>Cantidad de casos pendientes del médico.</summary>
        int MedicoCantidadCasosPendientes { get; set; }
        ///<summary>Propiedad para asignar el datasource al grid.</summary>
        IEnumerable<SuscriptorDTO> Datos { set; }
        ///<summary>Propiedad para obtener o asignar el número de registros.</summary>
        int NumeroDeRegistros { get; set; }
        /// <summary>Identificador que se asgina a la persona a Buscar.</summary>
        string BuscaPersona { get; set; }
        string IdRed { get; set; }
        DataTable ComboIdRed { set; }
        #endregion
    }
}