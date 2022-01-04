using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface ITransporteFlujoServicio : InterfazBase
	{
		IList<FlujosServicioDTO> FlujoServicios { get; set; }
		IEnumerable<string> ComboFlujoServicio { set; }
		string IdFlujoServicio { get; set; }
		IEnumerable<FlujosServicioDTO> ComboVersion { set; }
		string IdVersion { get; set; }
		string Servidor { get; set; }
		string Instancia { get; set; }
		string BD { get; set; }
		string Usuario { get; set; }
		string Contrasena { get; set; }
		string CantidadServiciosSucursales { get; set; }
		string CantidadAlcanceGeografico { get; set; }
		string CantidadEtapas { get; set; }
		string CantidadPasos { get; set; }
		string CantidadSolicitudBloques { get; set; }
		string CantidadPasosBloques { get; set; }
		string CantidadBloques { get; set; }
		string CantidadPasosRespuestas { get; set; }
		string CantidadParametrosAgenda { get; set; }
		string CantidadChaPasos { get; set; }
		string CantidadMensajeMetodosDestinatarios { get; set; }
		string CantidadFlujoEjecucion { get; set; }
		string CantidadTipoPasos { get; set; }
		string CantidadDocumentosServicios { get; set; }
		string CantidadDocumentosPasos { get; set; }
		string CantidadDocumentos { get; set; }
		string CantidadCamposIndexacion { get; set; }
		string CantidadAtributosArchivo { get; set; }
		string Mensaje { get; set; }
	}
}