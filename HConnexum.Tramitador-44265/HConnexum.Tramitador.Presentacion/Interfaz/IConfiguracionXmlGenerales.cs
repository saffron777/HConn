using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Xml.Linq;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    ///<summary>Interface ConfiguracionXmlGenerales.</summary>
    public interface IConfiguracionXmlGenerales : InterfazBase
    {
        int Id { get; set; }
        DataTable Datos { set; }
        DataTable Pasos { set; }

        DataTable Parametros { set; }
        DataTable Vinculaciones { set; }

        string StrXmlEstructura { get; set; }

        int NumeroDeRegistros { get; set; }
        string Errores { set; }
        string ErroresCustom { set; }
        string Confirm { set; }
    }
}
