using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IPantallaDetalleMovimientoRemesa:InterfazBase
	{
		string Asegurado { set; get; }
		string CedulaTitular { set; get; }
		string Sexo { set; get; }
		string FechaNacimiento { set; get; }
		string Estado { set; get; }
		string PacienteAsegurado { set; get; }
		string CedulaAsegurado { set; get; }
		string SexoPaciente { set; get; }
		string FechaNacPaciente { set; get; }
		string EstadoPaciente { set; get; }
		string Parentesco { set; get; }
		string ContratantePoliza { set; get; }
		string Poliza { set; get; }
		string Certificado { set; get; }
		string FechaDesde { set; get; }
		string FechaHasta { set; get; }
		string ContratanteSolicitud { set; get; }
		string Proveedor { set; get; }
		string Diagnostico { set; get; }
		string CodClave { set; get; }
		string FechaOcurrencia { set; get; }
		string FechaNotificacion { set; get; }
		string FechaLiquidadoEsperaPago { set; get; }
		string FechaEmisionFactura { set; get; }
		string FechaRecepcionFactura { set; get; }
		string Status { set; get; }
		string SubCategoria { set; get; }
		string NumeroControl { set; get; }
		string NumeroFactura { set; get; }
		string NumeroPoliza { set; get; }
		string CertificadoSolicitud { set; get; }
		string MontoPresupuestoIncial { set; get; }
		string Deducible { set; get; }
		string MontoCubierto { set; get; }
		string GastosnoCubiertos { set; get; }
		string MontoSujetoRetencion { set; get; }
		string GastosClinicos { set; get; }
		string GastosMedicos { set; get; }
		string PorcentajeRetencion { set; get; }
		string Retencion { set; get; }
		string ParentescoSolicitud { set; get; }
		string Liquidador { set; get; }
		int Nremesa { get; }
		string ConexionString { get; }
		int IdCodExterno { get; }
		int IdIntermediario { get; }
		string Errores { set; }
	}
}
