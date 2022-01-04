using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using HConnexum.Tramitador.Negocio;


namespace HConnexum.Tramitador.Presentacion.Interfaz
{
	public interface IDefault : InterfazBase
	{
		DataTable Menu { set; }
		string Errores { set; }
        string tbUsuario { get; set; }
        string tbSuscritor { get; set; }
        string tbSucursal { get; set; }
	}
}