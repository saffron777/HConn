using System;
using System.Collections.Generic;
using System.Data;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz 
{
    ///<summary>Interface CasoLista.</summary>
    public interface IPriorizarCasoLista : InterfazBase
    {
        ///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
        IEnumerable<CasoDTO> Datos { set; }
		
        ///<summary>Propiedad para asignar errores desde BD.</summary>
        string Errores { set; }
		
        ///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
        int NumeroDeRegistros { get; set; }
        string IdServicio { get; set; }
        IEnumerable<FlujosServicioDTO> ComboIdServicio { set; }
        string IdTipodoc { get; set; }
        DataTable ComboIdTipodoc { set; }
        string Caso { get; set; }
        string CodDocumento { get; set; }
        string IdPrioridad { get; set; }
        DataTable ComboIdPrioridad { set; }
        string Nombre { get; set; }
        string IdEstatus { get; set; }
        DataTable ComboIdEstatus { set; }
        string Apellido { get; set; }
    }
}