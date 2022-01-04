using System;
using System.Web;
using System.Data;
using HConnexum.Infraestructura;
using HConnexum.Seguridad;
using System.Web.Security;
using System.Configuration;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Sitio
{
    public partial class Inicio : PaginaBase, IContingenciaLogin
	{
        ContingenciaLoginPresentador presentador;
        bool sw = false;

        ///<summary>Evento de inicialización de la página.</summary>
        ///<param name="sender">Instancia de la página.</param>
        ///<param name="e">Instancia con parámetros de argumento.</param>
        ///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
        protected void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            presentador = new ContingenciaLoginPresentador(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["user"] != null)
            {                
                sw = true;
                var x = new ControlUsuarioEventArgs();
                x.Login = ExtraerDeViewStateOQueryString("user");
                x.Password = ExtraerDeViewStateOQueryString("password");
                x.IdUsuarioSuscriptorSeleccionado = Convert.ToInt32(ExtraerDeViewStateOQueryString("idUsuarioSuscriptorSeleccionado"));
                ContingenciaLoginControl_IniciarSesionClicked(sender, x);
            }
            else
                this.ContingenciaLoginControl.IniciarSesionClicked += new LoginControl.IniciarSesionEventHandler(ContingenciaLoginControl_IniciarSesionClicked);
        }

		void ContingenciaLoginControl_IniciarSesionClicked(object sender, ControlUsuarioEventArgs e)
		{
            UsuarioActual usr = null;
            try
            {
                ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();
                bool bandera = false;
                string ip = GetIpAddress();

                if (!sw)
                {
                    DataSet ds = servicio.AutenticarUsuario(e.Login.Encriptar(), e.Password.Encriptar(), Session.SessionID.Encriptar(), ip.Encriptar());
                    if (ds.Tables[@"Error"] != null)
                        throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());

                    usr = new UsuarioActual(ds);
                }
                else
                {
                    usr = (UsuarioActual)Session["UsuarioActual"];
                }
                Trace.Warn("Usuario Actual: " + usr.DatosBase.Nombre1 + " IdUsuarioSuscriptorSeleccionado " + usr.IdUsuarioSuscriptorSeleccionado.ToString());
                this.Session["Cultura"] = "es-VE";
                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1, usr.DatosBase.Nombre1, DateTime.Now, DateTime.Now.AddMinutes(double.Parse(ConfigurationManager.AppSettings[@"TiempoSession"])), false, "");
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                ck.Path = FormsAuthentication.FormsCookiePath;
                this.Response.Cookies.Add(ck);
                usr.IdUsuarioSuscriptorSeleccionado = (e.IdUsuarioSuscriptorSeleccionado != 0 ? e.IdUsuarioSuscriptorSeleccionado : usr.Suscriptores[0].IdUsuarioSuscriptor);
                DataSet dsd = servicio.ObtenerDatosUsuarioSuscriptor(usr.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar());

                if (dsd.Tables[@"Error"] != null)
                    throw new Exception(dsd.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (UsuarioActual == null)
                {
                    usr.ActualizarUsuario(dsd);
                    Session[@"UsuarioActual"] = usr;
                }
                servicio.Close();
              
                if (!UsuarioActual.VerificarRolesPorAplicacion(IdAplicacion) || presentador.CargarRoles(IdAplicacion))
                {
                     this.Response.Redirect("~/Default.aspx?idUsuario=" + usr.Id.ToString() + "&AppOrigen=" + Request.QueryString["AppOrigen"], false);
                }
            }
            catch (Exception ex)
            {

                Errores = ex.Message;
            }
		}

        public string GetIpAddress()
        {
            string stringIpAddress;
            stringIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (stringIpAddress == null)
                stringIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            return stringIpAddress;
        }
    }
}
