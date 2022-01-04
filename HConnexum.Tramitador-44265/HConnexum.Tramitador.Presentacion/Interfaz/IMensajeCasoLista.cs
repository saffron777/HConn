using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz 
{
	///<summary>Interface BuzonMensajeLista.</summary>
    public interface IMensajeCasoLista: InterfazBase
	{
		///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
        IEnumerable<CasoDTO> Datos { set; }
		
		///<summary>Propiedad para asignar errores desde BD.</summary>
		string Errores { set; }
		
		///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
		int NumeroDeRegistros { get; set; }
	}
}