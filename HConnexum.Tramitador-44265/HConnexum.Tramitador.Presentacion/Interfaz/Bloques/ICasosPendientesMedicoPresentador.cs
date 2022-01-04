using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Negocio;
using System.Data;

namespace HConnexum.Tramitador.Presentacion.Interfaz.Bloques
{
    public interface ICasosPendientesMedicoPresentador
    {
        ///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
        DataTable Datos { set; }

        ///<summary>Propiedad para asignar errores desde BD.</summary>
        string Errores { set; }

        ///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
        int NumeroDeRegistros { get; set; }
    }
}
