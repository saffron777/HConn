using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Presentacion.Interfaz;
///<summary>Namespace que engloba la interfaz Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Configurador.Presentacion.Interfaz 
{
	///<summary>Interface AgrupacionLista.</summary>
    public interface IAgrupacionLista: InterfazBase
	{
		///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
        IEnumerable<HConnexum.Tramitador.Negocio.Agrupacion> Datos { set; }
		
		///<summary>Propiedad para asignar errores desde BD.</summary>
		string Errores { set; }
		
		///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
		int NumeroDeRegistros { get; set; }
	}
}