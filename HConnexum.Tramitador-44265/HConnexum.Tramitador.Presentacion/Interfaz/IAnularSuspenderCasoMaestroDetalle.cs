using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface CasoMaestroDetalle.</summary>
	public interface IAnularSuspenderCasoMaestroDetalle : InterfazBase
	{
		int Id { get; set; }
		string PrioridadAtencion { set; }
		string IdSolicitud { set; }
		string Estatus { set; }
		string CreadorPor { set; }
		string FechaCreacion2 { set; }
		string FechaSolicitud { set; }
		string FechaAnulacion { set; }
		string FechaRechazo { set; }
		string caso { set; }
		string version { set; }
		string Suscriptor { set; }
		string Servicio { set; }
		string NumDoc { set; }
		string TipoDoc { set; }
		string Modificado { set; }
		IEnumerable<MovimientoDTO> Datos { set; }
		int NumeroDeRegistros { get; set; }
		string Errores { set; }
		string ObservacionReverso { get; set; }
        bool AnularSuspender { get; set; } 
	}
}
