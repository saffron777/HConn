using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface CasoDetalle.</summary>
    public interface IMisSolicitudes : InterfazBase
	{
	    int Id { get; set; }
        string NombreUsuario { get; set; }
        string NombreSuscriptor { get; set; }
        ///<summary>Propiedad para asignar el datasource al TreeList.</summary>
        IList<FlujosServicioDTO> DatosFlujos { set; }
        ///<summary>Propiedad para asignar el datasource al TreeList.</summary>
        IList<CasoDTO> DatosCasos { set; }
        ///<summary>Propiedad para asignar errores desde BD.</summary>
        string Errores { set; }
	}
}