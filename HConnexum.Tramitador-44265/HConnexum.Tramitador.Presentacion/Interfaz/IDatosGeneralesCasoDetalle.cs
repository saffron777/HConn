using System;
using System.Data;
using System.Collections.Generic;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la interfaz Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface CasoDetalle.</summary>
    public interface IDatosGeneralesCasoDetalle : InterfazBase
	{
        int Id { get; set; }
        DataTable Datos { get; set; }
        string Errores { set; }
    //    string IndVigente { get; set; }
    //    string FechaValidez { get; set; }
    //    string PrioridadAtencion { get; set; }
    //    string IdSolicitud { get; set; }
    //    string IdFlujoServicio { get; set; }
    //    IEnumerable<FlujosServicioDTO>ComboIdFlujoServicio { set; }
    //    string IdServicio { get; set; }
    //    string Ticket { get; set; }
    //    string TipDocSolicitante { get; set; }
    //    string NumDocSolicitante { get; set; }
    //    string NombreSolicitante { get; set; }
    //    string ApellidoSolicitante { get; set; }
    //    string MovilSolicitante { get; set; }
    //    string EmailSolicitante { get; set; }
    //    string ClaveSolicitante { get; set; }
    //    string Estatus { get; set; }
    //    string NumPoliza { get; set; }
    //    string NumMvtoPoliza { get; set; }
    //    string NumCertificado { get; set; }
    //    string NumRiesgo { get; set; }
    //    string TipDocAgt { get; set; }
    //    string NumDocAgt { get; set; }
    //    string XMLRespuesta { get; set; }
    //    string CreadorPor { get; set; }
    //    string FechaCreacion { get; set; }
    //    string ModificadoPor { get; set; }
    //    string FechaModificacion { get; set; }
    //    string FechaSolicitud { get; set; }
    //    string FechaAnulacion { get; set; }
    //    string FechaRechazo { get; set; }
    //    string FechaEjecucion { get; set; }
    //    string FechaReverso { get; set; }
    //    string IdMovimiento { get; set; }
    //    IEnumerable<MovimientoDTO>ComboIdMovimiento { set; }
    }
}
