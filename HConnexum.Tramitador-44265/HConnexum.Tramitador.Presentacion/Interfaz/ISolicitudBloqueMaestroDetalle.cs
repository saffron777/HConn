using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface FlujosServicioMaestroDetalle.</summary>
	public interface ISolicitudBloqueMaestroDetalle : InterfazBase
	{
		int Id { get; set; }
		string IndPublico { get; set; }
		string IdSuscriptor { get; set; }
		string IdServicioSuscriptor { get; set; }
		string Version { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModicacion { get; set; }
		string FechaValidez { get; set; }
		string IndVigente { get; set; }
		string IndEliminado { get; set; }
		IEnumerable<SolicitudBloqueDTO> Datos { set; }
		int NumeroDeRegistros { get; set; }
		string Errores { set; }
	}
}