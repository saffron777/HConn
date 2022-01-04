using System.Data;

namespace HConnexum.Base.Presentacion.Interfaz
{
	public interface IDefaultBase : IBase
	{
		DataTable Menu { set; }
		string TbUsuario { set; }
		string TbSuscritor { set; }
	}
}