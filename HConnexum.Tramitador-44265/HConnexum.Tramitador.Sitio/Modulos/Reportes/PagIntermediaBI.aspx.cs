using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HConnexum.Tramitador.Sitio.Modulos.Reportes
{
    public partial class PagIntermediaBI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie appOrigen1 = Request.Cookies["AppOrigen"];
            string appOrigen = (appOrigen1 == null ? string.Empty : appOrigen1.Value);
            this.Response.Redirect("/BI/Bienvenida.aspx?AppOrigen=" + appOrigen);
        }
    }
}