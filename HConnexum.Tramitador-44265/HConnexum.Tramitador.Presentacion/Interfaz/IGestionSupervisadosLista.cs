using System;
using System.Collections.Generic;
using System.Data;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz 
{
    ///<summary>Interface CasoLista.</summary>
    public interface IGestionSupervisadosLista : InterfazBase
    {
        ///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
        IEnumerable<CasoDTO> Datos { set; }
		
        ///<summary>Propiedad para asignar errores desde BD.</summary>
        string Errores { set; }
		
        ///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
        int NumeroDeRegistros { get; set; }
        string IdServicio { get; set; }
        DataTable ComboIdServicio { set; }
        string UsuarioLog { set; }
        //DataTable ComboIdTipodoc { set; }
        //string IdPrioridad { get; set; }
        //DataTable ComboIdPrioridad { set; }
        DataTable ComboIdSuscriptor { set; }
        string IdSuscriptor { get; set; }
        //string IdEstatus { get; set; }
        //DataTable ComboIdEstatus { set; }
        DataTable ComboIdUsuarioSupervisado { set; }
        string IdUsuarioSupervisado { get; set; }
        DataTable ComboIdEstatusSupervisados { set; }
        string IdEstatusSupervisados { get; set; }
        DataTable ComboIdCargo { set; }
        string IdCargo { get; set; }
        DataTable ComboHabilidad { set; }
        string IdHabilidad { get; set; }
        DataTable ComboAutonomia { set; }
        string IdAutonomia { get; set; }

        int[] IdSupervisado { get; set; }
    }
}