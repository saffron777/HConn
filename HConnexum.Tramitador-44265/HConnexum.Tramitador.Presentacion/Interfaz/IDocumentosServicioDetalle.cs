using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface DocumentosServicioDetalle.</summary>
    public interface IDocumentosServicioDetalle : InterfazBase
	{
	    int Id { get; set; }
        string IdFlujoServicio { get; set; }
		string IdDocumento { get; set; }
		IEnumerable<DocumentoDTO>ComboIdDocumento { set; } 
		string IndDocObligatorio { get; set; }
		string IndVisibilidad { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
        string Servicio { set; }
        int Version { set; }
        string Estatus { set; }
        string ErroresCustomEditar { set; }
        string Errores { set; }
	}
}