using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public partial interface IPantallaConsolidadoMovimiento : InterfazBase
	{
		DataTable GrupoEmpresarial { set; }
		string IdGrupoEmpresarial { get; }
		DataTable ComboSuscriptorGrupoEmpresarial { set; }
		IEnumerable<SuscriptorDTO> ComboSuscriptor { set; }
		DataTable ComboSuscriptorXUsuarioLogeado { set; }
		string ValorComboSuscriptor { get; set; }
		string IdComboSuscriptor { get; set; }
		DataTable ComboSucursal { set; }
		string IdComboSucursal { get; }
		DataTable ComboServicio { set; }
		string IdComboServicio { get; }
		DataTable Area { set; }
		string IdArea { get; }
		DataTable Usuario { set; }
		string IdUsuario { get; }
		string IdProveedor { get; }
		DataTable Pais { set; }
		string IdPais { get; }
		DataTable DivTerr1 { set; }
		string IdDivTerr1 { get; }
		DataTable DivTerr2 { set; }
		string IdDivTerr2 { get; }
		DataTable DivTerr3 { set; }
		string IdDivTerr3 { get; }
		string FechaDesde { get; }
		string FechaHasta { get; }
		string Errores { set; }
	}
}
