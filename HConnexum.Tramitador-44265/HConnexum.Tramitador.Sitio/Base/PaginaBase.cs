using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Infraestructura;
using HConnexum.Seguridad;
using Telerik.Web.UI;
using System.Threading;
using System.ComponentModel;
using HConnexum.ObjetoSessiones;

namespace HConnexum.Tramitador.Sitio
{
	public class PaginaBase : Page
	{
		public string NombreTabla { get; set; }
        public string app = string.Empty;      
        public string idUsuario = string.Empty;
        private int paginaUsaSesion = 1;

		/// <summary>Evento de inicialización de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"JQ"))
                this.ClientScript.RegisterClientScriptInclude(typeof(string), @"JQ", this.ResolveClientUrl(@"~/Scripts/jquery-1.7.1.min.js"));
            if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Util"))
                this.ClientScript.RegisterClientScriptInclude(typeof(string), @"Util", this.ResolveClientUrl(@"~/Scripts/Utilitarios.js"));
            if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Mask"))
                this.ClientScript.RegisterClientScriptInclude(typeof(string), @"Mask", this.ResolveClientUrl(@"~/Scripts/jquery.maskedinput.js"));

            // this.DisableDoubleClickButtons();

            if (!VerificarPaginaSinSesion(sender.ToString()) && UsuarioActual == null)
            {
                Trace.Warn("AppOrigen: " + Request.QueryString["AppOrigen"]);
                Trace.Warn("idUsuario: " + Request.QueryString["idUsuario"]);
                HttpCookie appOrigen1 = Request.Cookies["AppOrigen"];
                string appOrigen = (appOrigen1 == null ? string.Empty : appOrigen1.Value);

                if (!string.IsNullOrEmpty(appOrigen))
                {
                    idUsuario = Request.QueryString["idUsuario"];
                    if (appOrigen == "Portal")
                        app = "/Portal";
                    else if (appOrigen == "Contingencia")
                        app = "/Contingencia";
                    ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();

                    if (!string.IsNullOrEmpty(idUsuario))
                        servicio.CerrarSesionUsuario(idUsuario.Encriptar(), Session.SessionID.Encriptar());
                    servicio.Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "function Redirect(arg) { top.location ='" + app + "'; }", true);
                    RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                    windowManagerTemp.Localization.OK = "Aceptar";
                    if (windowManagerTemp != null)
                        windowManagerTemp.RadAlert("Su sesión expiró.", 380, 50, "Información", @"Redirect");
                }
                else
                {
                    string urlLogin = ConfigurationManager.AppSettings[@"UrlLogin"];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "function Redirect(arg) { location.href = '" + urlLogin + "'; }", true);
                    RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                    windowManagerTemp.Localization.OK = "Aceptar";
                    if (windowManagerTemp != null)
                        windowManagerTemp.RadAlert("Su sesión expiró.", 380, 50, "Información", "");               
                }
            }
        }

       
		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
                this.Title = this.ObtenerTitulo();
				ManejoNavegacionArbolPagina();
                ClearControls(this);
			}
		}

        /// <summary>
        /// Función que valida las paginas que no requieren sesión dentro de la aplicación
        /// </summary>
        /// <param name="paginaPadre"></param>
        /// <returns></returns>
        private bool VerificarPaginaSinSesion(string paginaPadre)
        {
            string[] paginas = ConfigurationManager.AppSettings[@"paginasSinSesion"].ToString().Split(',');
            if (paginas.Count() > 0 && !paginas[0].ToString().Equals(""))
            {
                for (int i = 0; i < paginas.Length; i++)
                {
                    if (paginaPadre.Contains(paginas[i].ToString()))
                    {
                        paginaUsaSesion = 0;
                        return true;
                    }
                }
            }

            return false;
        }

        public static void ClearControls(Control c)
        {
            foreach (Control Ctrl in c.Controls)
            {
                switch (Ctrl.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)Ctrl).Attributes.Add("onBlur", "espaciosCampoX(this)");
                        break;
                    default:
                        if (Ctrl.Controls.Count > 0)
                            ClearControls(Ctrl);
                        break;
                }
            }
        }

		public void ManejoNavegacionArbolPagina()
		{
			List<ArbolPagina> arbolPaginaTemp = ArbolPaginas;
			if(!arbolPaginaTemp.Any(ap => ap.IdMaster == IdMenu))
				arbolPaginaTemp.Add(new ArbolPagina(IdMenu, Request.Url.PathAndQuery, this.Title, this.NombreTabla));
			else
			{
				ArbolPagina actual = arbolPaginaTemp.ObtenerArbolPaginaActual();
				if(actual == null)
				{
					ArbolPagina padre = arbolPaginaTemp.ObtenerArbolPaginaPadre();
					if(padre != null)
						padre.Hija = new ArbolPagina(IdMenu, Request.Url.PathAndQuery, this.Title, this.NombreTabla);
					else
						if(!arbolPaginaTemp.Any(ap => ap.Url == Request.Url.PathAndQuery))
						{
							ArbolPagina master = arbolPaginaTemp.Where(ap => ap.IdMaster == IdMenu).SingleOrDefault();
							arbolPaginaTemp.Remove(master);
							arbolPaginaTemp.Add(new ArbolPagina(IdMenu, Request.Url.PathAndQuery, this.Title, this.NombreTabla));
						}
				}
				else
				{
					ElimitarRegistrosTomadosAnidado(actual.Hija);
					actual.Hija = null;
				}
			}
			ArbolPaginas = arbolPaginaTemp;
		}

		public void ElimitarRegistrosTomadosAnidado(ArbolPagina arbolPagina)
		{
			if(arbolPagina != null)
			{
				Uri url = new Uri(@"http:/" + arbolPagina.Url);
				string sIdRegistro = HttpUtility.ParseQueryString(url.Query).Get(@"id");
				if(!string.IsNullOrEmpty(sIdRegistro))
				{
					int idRegistro = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdRegistro)).Desencriptar());
					string sAccion = HttpUtility.ParseQueryString(url.Query).Get(@"accion");
					if(!string.IsNullOrEmpty(sAccion))
					{
						sAccion = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sAccion)).Desencriptar();
						if(sAccion.ToLower() == AccionDetalle.Modificar.ToString().ToLower())
							PaginaBasePresentador.EliminarRegistroTomado(arbolPagina.NombreTabla, idRegistro, UsuarioActual.IdSesion);
					}
				}
				if(arbolPagina.Hija != null)
					ElimitarRegistrosTomadosAnidado(arbolPagina.Hija);
			}
		}

		protected override void InitializeCulture()
		{
			if(this.Session["Cultura"] == null)
				this.Session["Cultura"] = "es-VE";
			CultureInfo info = new CultureInfo(this.Session["Cultura"].ToString());
			Thread.CurrentThread.CurrentCulture = info;
			Thread.CurrentThread.CurrentUICulture = info;
			base.InitializeCulture();
		}

		/// <summary>Evento previo al renderizado de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.AplicarPermisosTransacciones();
			#region Seguridad (Descomentar)
			VerificarPermisosPagina();
			#endregion
            RadWindowManager  wnd = Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
            if (wnd != null)
            {
                wnd.Width = int.Parse(ConfigurationManager.AppSettings[@"RadWindowManagerWidth"]);
                wnd.Height = int.Parse(ConfigurationManager.AppSettings[@"RadWindowManagerHeight"]);
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            if (Session[@"UsuarioTema"] == null)
            {
                string tema = ConfigurationManager.AppSettings["AplicacionTema"];
                if (tema != null)
                    Session[@"UsuarioTema"] = tema;
            }
            this.Theme = Session[@"UsuarioTema"].ToString();
            base.OnPreInit(e);
        }

		#region "Propiedades"
		/// <summary>Propiedad para mostrar los errores a travez de un alert javascript.</summary>
		public string Errores
		{
            set
            {
                if ((paginaUsaSesion == 0) || UsuarioActual != null)
                {
                    if (value.Length > 0)
                    {
                        RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                        windowManagerTemp.Localization.OK = "Aceptar";
                        if (windowManagerTemp != null)
                            windowManagerTemp.RadAlert("Se encontraron los siguientes errores: " + value, 380, 50, "Error", "");
                    }
                }
            }
		}


        public string UrlRedirectLogin
        {
            get
            {
                return "http://localhost:90/Contingencia/ContingenciaLogin.aspx";
            }
        }

		//TODO: Eliminar
		/// <summary>Propiedad para obtener y asignar los argumentos que se enviaran al RadAjaxManager Padre.</summary>
		public string AjaxArgs
		{
			get
			{
				return ((HiddenField)Master.FindControl("txtHidenAjaxArgs")).Value;
			}
			set
			{
				((HiddenField)Master.FindControl("txtHidenAjaxArgs")).Value = value;
			}
		}

		/// <summary>Lista que contiene los Controles RadDatePicker de la Pagina.</summary>
		IList<string> idControls = new List<string>();

		/// <summary>Propiedad para obtener y asignar la lista de Controles.</summary>
		public IList<string> IdControls
		{
			get
			{
				return this.idControls;
			}
			set
			{
				this.idControls = value;
			}
		}

		public UsuarioActual UsuarioActual
		{
			get
			{
				if(this.Session[@"UsuarioActual"] != null)
					return (UsuarioActual)this.Session[@"UsuarioActual"];

                //#region Seguridad (Descomentar)
                //FormsAuthentication.SignOut();
                //this.Session.RemoveAll();
                //this.Response.Redirect(@"~/ContingenciaLogin.aspx");
                //#endregion

				return null;
			}
		}

		public int IdAplicacion
		{
			get
			{
				return int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
			}
		}

		public int IdPaginaModulo
		{
			get
			{
				if(SiteMap.CurrentNode == null)
					return 0;
				return string.IsNullOrEmpty(SiteMap.CurrentNode[@"id"]) ? 0 : int.Parse(SiteMap.CurrentNode[@"id"]);
			}
		}

		public List<ArbolPagina> ArbolPaginas
		{
			get
			{
                if (Session[@"ArbolPaginasV1"] != null)
                    return (List<ArbolPagina>)ObjetoSession.LeerVariableSession("ArbolPaginasV1");
				return new List<ArbolPagina>();
			}
			set
			{
                ObjetoSession.GrabarVariableSession("ArbolPaginasV1", value);
			}
		}

		public int IdMenu
		{
			get
			{
				string idMenu = ExtraerDeViewStateOQueryString(@"IdMenu");
				if(!string.IsNullOrEmpty(idMenu))
					return int.Parse(idMenu);
				return 0;
			}
		}

		public string IdMenuEncriptado
		{
			get { return HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())); }
		}
		#endregion "Propiedades"

		#region "Metodos comunes"
		/// <summary>Metodo que recorre los botones para agregarle el metodo JS y deshabilitarlo al hacer click.</summary>
        //protected void DisableDoubleClickButtons()
        //{
        //    foreach (Button button in this.Page.Controls.FindAll<Button>())
        //    {
        //        button.UseSubmitBehavior = false;
        //        this.DisableButtonOnClick(button);
        //    }
        //}

		/// <summary>Agrega al boton una funcion JS para deshabilitarlo al hacer click</summary>
		/// <param name="ButtonControl">Button. Control que se desea aplicar la funcion.</param>
        //public void DisableButtonOnClick(Button buttonControl)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("if(typeof(Page_ClientValidate) == 'function') { ");
        //    if(string.IsNullOrEmpty(buttonControl.ValidationGroup))
        //        sb.Append("if(!Page_ClientValidate()) { return false; } } ");
        //    else
        //        sb.Append("if(!Page_ClientValidate('" + buttonControl.ValidationGroup + "')) { return false; } } ");
        //    sb.Append("this.disabled = true;");
        //    buttonControl.Attributes.Add("onclick", sb.ToString());
        //}

		/// <summary>Metodo que recorre los controles de una pagina para Bloquearlos o Desbloquearlos.</summary>
		/// <param name="bloquear">Bool. Varible que indica si los controles seran bloqueados o desbloqueados.</param>
		protected void BloquearControles(bool bloquear)
		{
			this.Page.Controls.BlockControls(bloquear);
		}

		/// <summary>Metodo que recorre los controles de una pagina para Limpiarlos.</summary>
		protected void LimpiarControles()
		{
			this.Page.Controls.ClearControls();
		}

		///<summary>Agrega la cultura a los Controles NumericTextBoxSetting dentro de un RadInputManager de Telerik.</summary>
		///<param name="InputManager">RadInputManager. Control que se le asignara la cultura.</param>
		protected void CultureNumericInput(RadInputManager inputManager)
		{
			for(int x = 0; x < inputManager.InputSettings.Count; x++)
			{
				if(typeof(NumericTextBoxSetting) == inputManager.InputSettings[x].GetType())
				{
					CultureInfo cultura = Thread.CurrentThread.CurrentCulture;
					((NumericTextBoxSetting)inputManager.InputSettings[x]).Culture = cultura;
				}
			}
		}

		/// <summary>Agrega la cultura a los Controles RadDatePicker de Telerik.</summary>
		protected void CultureDatePicker()
		{
			foreach(RadDatePicker control in this.Page.Controls.FindAll<RadDatePicker>())
				this.CultureDatePicker(control);
			if(!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"JSMDP"))
				this.ClientScript.RegisterClientScriptBlock(typeof(string), @"JSMDP", this.JSMaskDatePicker());
		}

		/// <summary>Extra una propiedad del ViewState o de un QueryString.</summary>
		/// <param name="propiedad">String. Nombre de la propiedad que se desea extraer.</param>
		/// <returns>String. Valor de la propiedad extraida.</returns>
		protected string ExtraerDeViewStateOQueryString(string propiedad)
		{
			string valor = string.Empty;
			if(this.ViewState[propiedad] != null && this.ViewState[propiedad].ToString() != string.Empty)
				valor = this.ViewState[propiedad].ToString();
			else if(!string.IsNullOrEmpty(this.Request.QueryString[propiedad]))
			{
				valor = System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(this.Request.QueryString[propiedad])).Desencriptar();
				this.ViewState[propiedad] = valor;
			}
			return valor;
		}

		/// <summary>
		/// Imprime un mensaje en la pagina a la que se le envia como referencia el RadWindowManager
		/// </summary>
		/// <param name="message">Mensaje a imprimir</param>
		/// <param name="title">Titulo de la Ventana</param>
		/// <param name="width">Ancho de la Ventana</param>
		/// <param name="height">Alto de la Ventana</param>
		/// <param name="manager">Referencia al Window Manager</param>
		protected void PrintMessage(string message, string title, int? width, int? height, RadWindowManager manager)
		{
			manager.RadAlert(message.Replace("'", "\"").Replace("\r\n", "<br/>").Replace("\n", "<br/>"), width, height, title, string.Empty);
		}

		/// <summary>
		/// Imprime un mensaje en la pagina a la que se le envia como referencia el RadWindowManager
		/// </summary>
		/// <param name="message">Mensaje a imprimir</param>
		/// <param name="title">Titulo de la Ventana</param>
		/// <param name="width">Ancho de la Ventana</param>
		/// <param name="height">Alto de la Ventana</param>
		/// <param name="manager">Referencia al Window Manager</param>
		protected void PrintMessage(string message, string title, int? width, int? height, RadWindowManager manager, string funtion)
		{
			manager.RadAlert(message.Replace("'", "\"").Replace("\r\n", "<br/>").Replace("\n", "<br/>"), width, height, title, funtion);
            this.ClientScript.RegisterStartupScript(typeof(string), @"JQ", @"fixRadAlert()", true);
        }

		/// <summary>
		/// Imprime un mensaje de confirmacion en la pagina
		/// </summary>
		/// <param name="message">Mensaje a imprimir</param>
		/// <param name="title">Titulo de la Ventana</param>
		/// <param name="confirmCallBackFn">Funcion que sera disparada en javascript al darle a los botones del Confirm</param>
		/// <param name="width">Ancho de la Ventana</param>
		/// <param name="height">Alto de la Ventana</param>
		/// <param name="manager">Referencia al Window Manager</param>
		protected void PrintConfirmMessage(string message, string title, string confirmCallBackFn, int width, int height, RadWindowManager manager)
		{
			manager.RadConfirm(message.Replace("'", "\"").Replace("\r\n", "<br/>").Replace("\n", "<br/>"), confirmCallBackFn, width, height, string.Empty, title);
		}






        /// <summary>
        /// Imprime un mensaje en la pagina a la que se le envia como referencia el RadWindowManager
        /// </summary>
        /// <param name="message">Mensaje a imprimir</param>
        /// <param name="title">Titulo de la Ventana</param>
        /// <param name="width">Ancho de la Ventana</param>
        /// <param name="height">Alto de la Ventana</param>
        /// <param name="manager">Referencia al Window Manager</param>
        protected void PrintInfoMessage(string message, string title, int? width, int? height, RadWindowManager manager)
        {
            manager.RadAlert(message, width, height, "Info", string.Empty, "Imagenes/info.png");
            this.ClientScript.RegisterStartupScript(typeof(string), @"JQ", @"fixRadAlert()", true);


        }

        /// <summary>
        /// Imprime un mensaje en la pagina a la que se le envia como referencia el RadWindowManager
        /// </summary>
        /// <param name="message">Mensaje a imprimir</param>
        /// <param name="title">Titulo de la Ventana</param>
        /// <param name="width">Ancho de la Ventana</param>
        /// <param name="height">Alto de la Ventana</param>
        /// <param name="manager">Referencia al Window Manager</param>
        protected void PrintErrorMessage(string message, string title, int? width, int? height, RadWindowManager manager)
        {
            manager.RadAlert(message, width, height, "Error", string.Empty, "Imagenes/error.png");
            this.ClientScript.RegisterStartupScript(typeof(string), @"JQ", @"fixRadAlert()", true);

        }

        /// <summary>
        /// Imprime un mensaje en la pagina a la que se le envia como referencia el RadWindowManager
        /// </summary>
        /// <param name="message">Mensaje a imprimir</param>
        /// <param name="title">Titulo de la Ventana</param>
        /// <param name="width">Ancho de la Ventana</param>
        /// <param name="height">Alto de la Ventana</param>
        /// <param name="manager">Referencia al Window Manager</param>
        protected void PrintCheckMessage(string message, string title, int? width, int? height, RadWindowManager manager)
        {
            manager.RadAlert(message, width, height, "Ok", string.Empty, "Imagenes/ok.png");
            this.ClientScript.RegisterStartupScript(typeof(string), @"JQ", @"fixRadAlert()", true);

        }

		#endregion "Metodos comunes"

		#region "Metodos Privados"
		/// <summary>Metodo para agregar la cultura al control RadDatePicker.</summary>
		/// <param name="control">RadDatePicker. Control que se le coloraca la cultura.</param>
		private void CultureDatePicker(RadDatePicker control)
		{
			CultureInfo cultura = Thread.CurrentThread.CurrentCulture;
			control.Culture = cultura;
			this.IdControls.Add(control.Controls[0].ClientID);
		}

		/// <summary>
		/// Agrega dinamicamente a las pagians que contenga un control de fecha una mascara con formato dd/mm/yyyy
		/// </summary>
		/// <returns>String. Script colocando la mascara a los controles de Fecha.</returns>
		private string JSMaskDatePicker()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<script type=\"text/javascript\">");
			sb.AppendLine("	jQuery(function ($)");
			sb.AppendLine("	{");
			for(int x = 0; x < this.IdControls.Count; x++)
				sb.AppendLine("		$('#" + this.IdControls[x] + "_text').mask('99/99/9999');");
			sb.AppendLine("	});");
			sb.AppendLine("</script>");
			return sb.ToString();
		}

		private void AplicarPermisosTransacciones()
		{
			if(this.UsuarioActual != null)
			{
				if(SiteMap.CurrentNode != null)
				{
					int idPaginaModulo = string.IsNullOrEmpty(SiteMap.CurrentNode["id"]) ? 0 : int.Parse(SiteMap.CurrentNode["id"]);
					if(idPaginaModulo != 0)
					{
						IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> transaccionesPaginas = PaginaBasePresentador.ObtenerTransaccionesPaginasModulos(idPaginaModulo);
						IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> transaccionesPaginasUsuarioActual = this.UsuarioActual.TransaccionesPaginas;
						if(!this.UsuarioActual.ObtenerTransaccionesPaginaUsuarioActual(idPaginaModulo, ref transaccionesPaginasUsuarioActual))
						{
							transaccionesPaginasUsuarioActual = (List<HConnexum.Seguridad.TraModAppPagModAppTraModApp>)PaginaBasePresentador.ObtenerTransaccionesUsuario(idPaginaModulo, this.UsuarioActual.ObtenerIdRoles(IdAplicacion));
							this.UsuarioActual.ActualizarTransaccionesPaginasUsuarioActual(transaccionesPaginasUsuarioActual);
						}
						string[] transaccionesApp = WebConfigurationManager.AppSettings["TransaccionesApp"].Split(';');
						for(int i = 0; i < transaccionesApp.Length; i++)
						{
							string tempNombreTran = transaccionesApp.GetValue(i).ToString();
							string nombreControl = SiteMap.CurrentNode[tempNombreTran];
							if(!string.IsNullOrEmpty(nombreControl))
							{
								List<Control> ctrls = this.Page.Controls.FindAll().Where(control => control.ID == nombreControl).ToList();
								HConnexum.Seguridad.TraModAppPagModAppTraModApp transaccionPagina = transaccionesPaginas.Where(tranPag => tranPag.NombreTransaccion == tempNombreTran).FirstOrDefault();
								if(transaccionPagina != null)
								{
									if(ctrls.Count > 0)
									{
                                        if (transaccionesPaginasUsuarioActual.Where(t => t.Id == transaccionPagina.Id && t.IndEliminado == false).Count() > 0)
										{
                                            if (ctrls[0] is Button || ctrls[0] is RadToolBarItem || ctrls[0] is Publicacion || ctrls[0] is Auditoria)
												ctrls[0].Visible = true;
										}
										else
										{
                                            if (ctrls[0] is Button || ctrls[0] is RadToolBarItem || ctrls[0] is Publicacion || ctrls[0] is Auditoria)
											{
												ctrls[0].Visible = false;
												this.ClearRowDblClickEvent(tempNombreTran);
											}
										}
									}
								}
								else
								{
									if(ctrls.Count > 0)
									{
                                        if (ctrls[0] is Button || ctrls[0] is RadToolBarItem || ctrls[0] is Publicacion || ctrls[0] is Auditoria)
										{
											ctrls[0].Visible = false;
											this.ClearRowDblClickEvent(tempNombreTran);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void VerificarPermisosPagina()
		{
			if(UsuarioActual != null)
				if(SiteMap.CurrentNode != null)
				{
					int idPaginaModulo = string.IsNullOrEmpty(SiteMap.CurrentNode["id"]) ? 0 : int.Parse(SiteMap.CurrentNode["id"]);
					if(idPaginaModulo != 0)
					{
						IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> transaccionesPaginasUsuarioActual = this.UsuarioActual.TransaccionesPaginas;
						if(this.UsuarioActual.ObtenerTransaccionesPaginaUsuarioActual(idPaginaModulo, ref transaccionesPaginasUsuarioActual))
							if(!SiteMap.CurrentNode.Url.Contains(@"Default.aspx"))
								this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "GetRadWindow().Close()", true);
					}
					else
						if(!SiteMap.CurrentNode.Url.Contains(@"Default.aspx"))
							this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "GetRadWindow().Close()", true);
				}
		}

		private void ClearRowDblClickEvent(string tempNombreTran)
		{
			if(tempNombreTran.Equals(WebConfigurationManager.AppSettings[@"TransaccionEditar"]))
			{
				RadGrid gridTemp = this.Page.Controls.FindAll<RadGrid>().FirstOrDefault();
				if(gridTemp != null)
					gridTemp.ClientSettings.ClientEvents.OnRowDblClick = string.Empty;
			}
		}

		/// <summary>
		/// Metodo encargado de tomar el nombre de las paginas, para ser colocado en el title de las mismas
		/// </summary>
		/// <returns></returns>
		private string ObtenerTitulo()
		{
			if(this.IdPaginaModulo != 0)
			{
				PaginasModulo pagina = PaginaBasePresentador.ObtenerPaginasModulos(this.IdPaginaModulo);
				if(pagina != null)
					return pagina.NombrePagina;
			}
			if(!string.IsNullOrEmpty(this.Title))
				return this.Title;
			return ConfigurationManager.AppSettings[@"PageDefaultName"];
		}
		#endregion "Metodos Privados"
	}
}
