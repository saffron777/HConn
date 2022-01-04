using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	///<summary>Interface PasoMaestroDetalle.</summary>
	public interface IConsultaCasosAuditoria : InterfazBase
	{
		IEnumerable<SuscriptorDTO> ComboSuscriptores { set; }
		int IdComboSuscriptores { get; }
		IEnumerable<FlujosServicioDTO> ComboServicios { set; }
		int IdComboServicios { get; }
		string TipoFiltro { get; }
		string Filtro { get; }
		string FechaDesde { get; }
		string FechaHasta { get; }
		//DateTime? FechaDesde{ get; }
		//DateTime? FechaHasta{ get; }
		IEnumerable<CasoDTO> Datos { set; }
		int NumeroDeRegistros { get; set; }
		string Errores { set; }
	}
}