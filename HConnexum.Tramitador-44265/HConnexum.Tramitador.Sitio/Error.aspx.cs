using System;
using System.Linq;

namespace HConnexum.Tramitador.Sitio
{
	public partial class Error : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblPagina.Text = this.Request["aspxerrorpath"];
		}
	}
}