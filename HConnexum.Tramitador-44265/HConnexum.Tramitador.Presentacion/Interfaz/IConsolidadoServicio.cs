using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public partial interface IPantallaConsolidadoServicio : InterfazBase
	{
		DataTable GrupoEmpresarial { set; }
		string IdGrupoEmpresarial { get; }
		DataTable ComboSuscriptorGrupoEmpresarial { set; }
		DataTable ComboSuscriptorXUsuarioLogeado { set; }
		IEnumerable<SuscriptorDTO> ComboSuscriptor { set; }
		string ValorComboSuscriptor { get; set; }
		string IdComboSuscriptor { get;set; }
		DataTable ComboSucursal { set; }
		string IdComboSucursal { get; }
		DataTable ComboServicio { set; }
		string IdComboServicio { get; }
		string FechaDesde { get; }
		string FechaHasta { get; }
		string Errores { set; }
	}
}
