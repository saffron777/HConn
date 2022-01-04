using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;
using System.Data;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface PasosBloqueDetalle.</summary>
	public interface IPasosBloqueDetalle : InterfazBase
	{
		int Id { get; set; }
		string TituloBloque { get; set; }
		string IdTipoControl { get; set; }
		string IndActualizable { get; set; }
		string IndColapsado { get; set; }
		DataTable ComboIdTipoControl { set; }
		string IdBloque { get; set; }
		IEnumerable<BloqueDTO> ComboIdBloque { set; }
		string Posicion { get; set; }
		string NombrePrograma { get; set; }
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