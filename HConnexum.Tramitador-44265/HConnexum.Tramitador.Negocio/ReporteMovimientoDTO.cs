using System;
using System.Collections;
using System.Linq;

namespace HConnexum.Tramitador.Negocio
{
	public class ReporteMovimientoDTO
	{
		public string Suscriptor { get; set; }
		public int IdSuscriptorDueño { get; set; }
		public int? IdSuscriptorProveedor { get; set; }
		public string Sucursal { get; set; }
		public int IdSucursal { get; set; }
		public ReporteMovimientoAreaDTO Area { get; set; }
		public string Servicio { get; set; }
		public int IdServicio { get; set; }
		public string Ncaso { get; set; }
		public string EstatusCaso { get; set; }
		public string NumPoliza { get; set; }
		public string NombreBeneficiario { get; set; }
		public string ApellidoBeneficiario { get; set; }
		public string CIBeneficiario { get; set; }
		public int? NumCertificado { get; set; }
		public string Proveedor { get; set; }
		public int IdProveedor { get; set; }
		public string Pais { get; set; }
		public int IdPais { get; set; }
		public string DivTerr1 { get; set; }
		public int IdDivTerr1 { get; set; }
		public string DivTerr2 { get; set; }
		public int IdDivTerr2 { get; set; }
		public string DivTerr3 { get; set; }
		public int IdDivTerr3 { get; set; }
		public string Estatus { get; set; }
		public string Usuario { get; set; }
		public int IdUsuario { get; set; }
		public int IdCaso { get; set; }
		public DateTime? FechaEstatus { get; set; }
		public DateTime? FechaMov { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int? ModificadoPor { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public bool IndVigente { get; set; }
		public DateTime? FechaValidez { get; set; }
		public bool IndEliminado { get; set; }
	}
}
