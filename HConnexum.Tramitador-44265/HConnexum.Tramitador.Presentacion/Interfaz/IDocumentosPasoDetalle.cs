using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface DocumentosPasoDetalle.</summary>
    public interface IDocumentosPasoDetalle : InterfazBase
	{
	    int Id { get; set; }
		string IdDocumentoServicio { get; set; }
		IEnumerable<DocumentosServicioDTO> ComboIdDocumentoServicio { set; }
        string Servicio { set; }
        int Version { set; }
        string Estatus {set;}
        string Etapa { set; }
        string Paso { set; }
        int IdFlujoServicio { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
		string Errores { set; }
        string ErroresCustomEditar { set; }
	}
}