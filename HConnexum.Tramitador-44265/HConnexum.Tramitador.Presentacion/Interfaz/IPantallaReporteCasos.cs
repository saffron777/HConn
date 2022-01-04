using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Interfaz
{

	///<summary>Interface ReporteCasos.</summary>
	public interface IPantallaReporteCasos : InterfazBase
	{
		DataTable GrupoEmpresarial { set; }
		string IdGrupoEmpresarial { get; }
		IEnumerable<SuscriptorDTO> ComboSuscriptor { set; }
		DataTable ComboSuscriptorXUsuarioLogeado{ set; }
		DataTable ComboSuscriptorGrupoEmpresarial { set; }
		string IdComboSuscriptor { get;}
		DataTable ComboSucursal { set; }
		string IdComboSucursal { get; }
		DataTable ComboServicio { set; }
		string IdComboServicio{ get; }
		string Poliza { get; }
		string Certificado { get; }
		string CIBeneficiario { get; }
		string FechaDesde { get; }
		string FechaHasta { get; }
		string Errores { set; }
	}
}
