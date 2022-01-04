using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Configurador.Presentacion.Interfaz
{

	///<summary>Interface AgrupacionDetalle.</summary>
    public interface IAgrupacionDetalle : InterfazBase
	{
	int Id { get; set; }
		string Nombre { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string FechaValidez { get; set; }
		string IndVigente { get; set; }
		string IndEliminado { get; set; }
		  
		string Errores { set; }
	}
}