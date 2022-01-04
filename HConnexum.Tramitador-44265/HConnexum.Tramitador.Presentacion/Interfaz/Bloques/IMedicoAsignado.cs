using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
///<summary>Namespace que engloba la interfaz de Bloques de la capa presentación HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    ///<summary>Interface MedicoAsignado.</summary>
    public interface IMedicoAsignado : InterfazBaseBloques
    {
       string Ptipdocmed { get; set; }
       string Pnumdocmed { get; set; }
       string Pnommed { get; set; }
       string Ptlfmed { get; set; }
       string Pespmed { get; set; }
       string Pidpaismed { get; set; }
       string Piddiv1med { get; set; }
       string Piddiv2med { get; set; }
       string Piddiv3med { get; set; }
       string Pdirmed { get; set; }
    }
}
