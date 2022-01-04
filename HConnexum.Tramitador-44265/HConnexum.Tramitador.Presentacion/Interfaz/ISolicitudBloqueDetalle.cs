using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface SolicitudBloqueDetalle.</summary>
	public interface ISolicitudBloqueDetalle : InterfazBase
	{
		int Id { get; set; }
		string IdBloque { get; set; }
		IEnumerable<BloqueDTO> ComboIdBloque { set; }
		string Orden { get; set; }
		string CreadoPor { get; set; }
		string FechaCreacion { get; set; }
		string ModificadoPor { get; set; }
		string FechaModificacion { get; set; }
		string IndVigente { get; set; }
		string FechaValidez { get; set; }
		string IndEliminado { get; set; }
		string IndCierre { get; set; }
		string IdTipoControl { get; set; }
		IEnumerable<ListasValorDTO> ComboIdTipoControl { set; }
		string TituloBloque { get; set; }
		string IndActualizable { get; set; }
		string KeyCampoXML { get; set; }
		string Errores { set; }
	}
}