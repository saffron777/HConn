using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface MovimientoDetalle.</summary>
    public interface IMovimientoDetalle : InterfazBase
	{
	    int Id { get; set; }
        string IdServicio { get; set; }
		string IdMovimiento { get; set; }
        string IdCaso { get; set; }		
		
		string Version { get; set; }
		string TipoMovimiento { get; set; }
		string EstatusCaso { get; set; }
        string EstatusMovimiento { get; set; }
       
        string Errores { set; }
	}
}