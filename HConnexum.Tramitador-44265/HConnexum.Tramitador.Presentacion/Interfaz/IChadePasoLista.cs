using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz 
{
	///<summary>Interface ChadePasoLista.</summary>
    public interface IChadePasoLista: InterfazBase
	{
		///<summary>Propiedad para asignar el datasource al RadGrid.</summary>
		IEnumerable<ChadePasoDTO> Datos { set; }
		
		///<summary>Propiedad para asignar errores desde BD.</summary>
		string Errores { set; }
		
		///<summary>Propiedad para obtener o asignar el nro de registros.</summary>
		int NumeroDeRegistros { get; set; }
        string Version { get; set; }
        string Estatus { get; set; }

        string NombreEtapa { get; set; }
        string Servicio { get; set; }
        string Paso { get; set; }
        int IdPasos { get ; }

	}
}