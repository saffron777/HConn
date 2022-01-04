using System;
using System.Linq;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
    ///<summary>Presentador para el manejo del control Web de usuario 'Observaciones'.</summary>
    public class ObservacionesPresentador : BloquesPresentadorBase
    {
        private string configClaveExcepcionMensaje = "MensajeExcepcion";
        
        #region M I E M B R O S   P Ú B L I C O S
        readonly UnidadDeTrabajo UDT = new UnidadDeTrabajo();
        ///<summary>Vista asociada al presentador.</summary>
        readonly IObservaciones Control;
        #endregion

        ///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
        public ObservacionesPresentador(IObservaciones control)
		{
			this.Control = control;
		}

        #region E V E N T O S

        #endregion
    }
}