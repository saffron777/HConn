using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;
using HConnexum.Tramitador.Presentacion;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Sitio
{
    public partial class ContingenciaLogin : PaginaBase, IContingenciaLogin
    {
        #region "Variables Locales"
        ContingenciaLoginPresentador presentador;
        #endregion "Variables Locales"

		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{


            if (!string.IsNullOrEmpty(Request.QueryString["AppOrigen"]))
            {    //se carga en el cookie porque no se mantiene con query string ni con viewstate, ni en sesion porque ya se perdio la sesion   
                HttpCookie cookie = new HttpCookie("AppOrigen", Request.QueryString["AppOrigen"]);
                Response.Cookies.Add(cookie);
            } 
			base.Page_Init(sender, e);
			presentador = new ContingenciaLoginPresentador(this);


		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
                Response.Buffer = true;
                if (Request.QueryString["user"] != null)
                    this.Response.Redirect("~/inicio.aspx?user=" + Request.QueryString["user"] + "&password=" + Request.QueryString["password"] + "&idUsuarioSuscriptorSeleccionado=" + Request.QueryString["idUsuarioSuscriptorSeleccionado"] + "&AppOrigen=" + Request.QueryString["AppOrigen"], false);
                else if (!UsuarioActual.VerificarRolesPorAplicacion(IdAplicacion) || presentador.CargarRoles(IdAplicacion))
                   this.Response.Redirect("~/Default.aspx", true);   
               // this.Server.TransferRequest("~/Default.aspx");         
			}
			catch(Exception exc)
			{
                if (UsuarioActual != null)
                    this.PrintMessage(exc.Message, "Error", 400, 100, this.singleton);
            }
        }
    }
}
