using System.Collections.Generic;
using System.Data;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    /// <summary>Interfaz del control Web de usuario 'SolicitudNuevoMovimiento'.</summary>
    public interface ISolicitudNuevoMovimiento : InterfazBaseBloques
    {
        #region M I E M B R O S   P Ú B L I C O S
        ///<summary>Tipo del bloque.</summary>
        string BloqueTipo { get; }
        ///<summary>Identificador del paso asociado al movimiento.</summary>
        int PasoId { get; set; }
		///<summary>Cadena de conexión del suscriptor intermediario.</summary>
		string SuscriptorIntermediarioConnectionString { get; set; }
		///<summary>Identificador del intermediario en la aplicación cliente.</summary>
		int IntermediarioId { get; }
		///<summary>Identificador del proveedor en la aplicación cliente.</summary>
		int ProveedorId { get; }
		///<summary>Identificador del diagnóstico.</summary>
		int DiagnosticoId { get; }
		///<summary>Identificador del procedimiento.</summary>
		int ProcedimientoId { get; }
        /// <summary>Identificador del tipo de movimiento.</summary>
        string MovimientoTipoId { get; }
        ///<summary>Estado del último movimiento del caso.</summary>
        string MovimientoEstado { get; set; }
        ///<summary>Lista de tipos de diagnóstico.</summary>
		DataTable ListaDiagnosticoTipos { set; }
        ///<summary>Lista de tipos de procedimiento.</summary>
		DataTable ListaProcedimientoTipos { set; }
        ///<summary>Lista de tipos de movimiento.</summary>
        IEnumerable<ListasValorDTO> ListaMovimientoTipos { set; }
        ///<summary>Lista de modos de movimiento.</summary>
        IEnumerable<ListasValorDTO> ListaMovimientoModos { set; }
        #endregion
    }
}