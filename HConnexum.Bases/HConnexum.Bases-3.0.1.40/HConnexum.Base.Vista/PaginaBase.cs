using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Base.ControlAuditoria;
using HConnexum.Base.ControlPublicacion;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Base.Presentacion.Interfaz;
using HConnexum.Base.Presentacion.Presentador;
using HConnexum.Infraestructura;
using HConnexum.Seguridad;
using Telerik.Web.UI;

namespace HConnexum.Base.Vista
{
	/// <summary>
	/// Actúa como clase base para las páginas, controlando principalmente temas de permisología, navegación, temas, cultura y scripts comunes. Esta clase proporciona los métodos y propiedades comunes para todos las páginas. 
	/// </summary>
	public class PaginaBase<P> : Page, IBase
	{
		#region Eventos
		
		/// <summary>Evento de inicialización de la página.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_Init(object sender, EventArgs e)
		{
			if (!this.VerificarPaginaSinSesion(sender.ToString()) && this.UsuarioActual == null)
				throw new CustomException("SinSession", ErrorType.Advertencia.ToString());
			else
			{
				this.presentadorPaginaBase = new PresentadorPaginaBase(this.AuditoriaDto);
				
				if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"JQ"))
					this.ClientScript.RegisterClientScriptInclude(typeof(string), @"JQ", this.ResolveClientUrl(@"~/Scripts/jquery-1.7.1.min.js"));
				
				if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Util"))
					this.ClientScript.RegisterClientScriptInclude(typeof(string), @"Util", this.ResolveClientUrl(@"~/Scripts/Utilitarios.js"));
				
				if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Mask"))
					this.ClientScript.RegisterClientScriptInclude(typeof(string), @"Mask", this.ResolveClientUrl(@"~/Scripts/jquery.maskedinput.js"));
				
				this.DisableDoubleClickButtons();
			}
		}
		
		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				this.Title = this.ObtenerTitulo();
				this.ManejoNavegacionArbolPagina();
				ClearControls(this);
			}
		}
		
		/// <summary>
		/// Establece Culture y UICulture para el subproceso actual de la página.
		/// </summary>
		protected override void InitializeCulture()
		{
			if (this.Session[@"Cultura"] == null)
				this.Session[@"Cultura"] = @"es-VE";
			
			CultureInfo info = new CultureInfo(this.Session[@"Cultura"].ToString());
			Thread.CurrentThread.CurrentCulture = info;
			Thread.CurrentThread.CurrentUICulture = info;
			base.InitializeCulture();
		}
		
		/// <summary>Evento previo al renderizado de la página.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.AplicarPermisosTransacciones();
			
			#region Seguridad (Descomentar)
			
			this.VerificarPermisosPagina();
			
			#endregion
			
			RadWindowManager wnd = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
			
			if (wnd != null)
			{
				wnd.Width = int.Parse(ConfigurationManager.AppSettings[@"RadWindowManagerWidth"]);
				wnd.Height = int.Parse(ConfigurationManager.AppSettings[@"RadWindowManagerHeight"]);
			}
		}
		
		protected override void OnPreInit(EventArgs e)
		{
			if (this.Session[@"UsuarioTema"] == null)
			{
				string tema = ConfigurationManager.AppSettings["AplicacionTema"];
				
				if (tema != null)
					this.Session[@"UsuarioTema"] = tema;
			}
			this.Theme = this.Session[@"UsuarioTema"].ToString();
			base.OnPreInit(e);
		}
		
		#endregion
		
		#region Propiedades
		
		/// <summary>Lista que contiene los Controles RadDatePicker de la Pagina.</summary>
		private readonly IList<string> idControls = new List<string>();
		
		private int IdMenu
		{
			get
			{
				string idMenu = this.ExtraerDeViewStateOQueryString(@"IdMenu");
				
				if (!string.IsNullOrEmpty(idMenu))
					return int.Parse(idMenu);
				
				return 0;
			}
		}
		
		protected Type clase;
		
		protected PresentadorPaginaBase presentadorPaginaBase;
		
		public P Presentador;
		
		protected int IdAplicacion
		{
			get
			{
				return int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
			}
		}
		
		protected IList<ArbolPagina> ArbolPaginas
		{
			get
			{
				if (this.Session[@"ArbolPaginas"] != null)
					return (IList<ArbolPagina>)this.Session[@"ArbolPaginas"];
				
				return new List<ArbolPagina>();
			}
			set
			{
				this.Session[@"ArbolPaginas"] = value;
			}
		}
		
		public object Interfaz;
		
		/// <summary>
		/// Propiedad no usada actualmente desde los preosentadores
		/// </summary>
		public string NombreTabla { get; set; }
		
		public string TipoMensaje
		{
			get
			{
				return this.Session[@"TipoMensaje"] == null ? string.Empty : this.Session[@"TipoMensaje"].ToString();
			}
			set
			{
				this.Session[@"TipoMensaje"] = value;
			}
		}
		
		/// <summary>Propiedad para mostrar los errores a travez de un alert javascript.</summary>
		public string Errores
		{
			get
			{
				return this.Session[@"Errores"] == null ? string.Empty : this.Session[@"Errores"].ToString();
			}
			set
			{
				if (this.UsuarioActual != null)
				{
					this.Session[@"Errores"] = value;
					
					if (value != string.Empty)
						this.MostrarMensaje(string.Format("<b>Se encontraron los siguientes errores:</b> {0}", value), this.TipoMensaje == string.Empty ? ErrorType.Error.ToString() : this.TipoMensaje);
				}
			}
		}
		
		public string Mensaje
		{
			get
			{
				return this.Session[@"Mensaje"] == null ? string.Empty : this.Session[@"Mensaje"].ToString();
			}
			set
			{
				if (this.UsuarioActual != null)
				{
					this.Session[@"Mensaje"] = value;
					
					if (value != string.Empty)
						this.MostrarMensaje(value, this.TipoMensaje == string.Empty ? ErrorType.Advertencia.ToString() : this.TipoMensaje);
				}
			}
		}
		
		public string Notificacion
		{
			set
			{
				this.MostrarMensaje(value, @"Mensaje");
			}
		}
		
		public UsuarioActual UsuarioActual
		{
			get
			{
				if (this.Session[@"UsuarioActual"] != null)
					return (UsuarioActual)this.Session[@"UsuarioActual"];
				
				return null;
			}
			set
			{
				this.Session[@"UsuarioActual"] = value;
			}
		}
		
		public AuditoriaDto AuditoriaDto
		{
			get
			{
				AuditoriaDto auditoriaDto = new AuditoriaDto();
				auditoriaDto.IpUsuario = HttpContext.Current.Request.UserHostAddress;
				auditoriaDto.HostName = HttpContext.Current.Request.Url.OriginalString;
				
				if (this.UsuarioActual != null)
				{
					auditoriaDto.IdSesion = this.Session.SessionID;
					auditoriaDto.LoginUsuario = this.UsuarioActual.Login;
				}
				return auditoriaDto;
			}
		}
		
		public int IdPaginaModulo
		{
			get
			{
				if (SiteMap.CurrentNode == null)
					return 0;
				
				return string.IsNullOrEmpty(SiteMap.CurrentNode[@"id"]) ? 0 : int.Parse(SiteMap.CurrentNode[@"id"]);
			}
		}
		
		protected string IdMenuEncriptado
		{
			get
			{
				return HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(this.IdMenu.ToString().Encriptar()));
			}
		}
		
		#endregion
		
		#region Métodos comunes
		
		protected static void ClearControls(Control c)
		{
			foreach (Control ctrl in c.Controls)
			{
				switch (ctrl.GetType().ToString())
				{
					case @"System.Web.UI.WebControls.TextBox":
						((TextBox)ctrl).Attributes.Add(@"onBlur", @"espaciosCampoX(this)");
						break;
					default:
						if (ctrl.Controls.Count > 0)
							ClearControls(ctrl);
						break;
				}
			}
		}
		
		protected void ManejoNavegacionArbolPagina()
		{
			IList<ArbolPagina> arbolPaginaTemp = this.ArbolPaginas;
			
			if (!arbolPaginaTemp.Any(ap => ap.IdMaster == this.IdMenu))
				arbolPaginaTemp.Add(new ArbolPagina(this.IdMenu, this.Request.Url.PathAndQuery, this.Title, this.NombreTabla));
			else
			{
				ArbolPagina actual = arbolPaginaTemp.ObtenerArbolPaginaActual();
				
				if (actual == null)
				{
					ArbolPagina padre = arbolPaginaTemp.ObtenerArbolPaginaPadre();
					
					if (padre != null)
						padre.Hija = new ArbolPagina(this.IdMenu, this.Request.Url.PathAndQuery, this.Title, this.NombreTabla);
					else if (!arbolPaginaTemp.Any(ap => ap.Url.Split('?')[0] == this.Request.Url.PathAndQuery.Split('?')[0]))
					{
						ArbolPagina master = arbolPaginaTemp.Where(ap => ap.IdMaster == this.IdMenu).SingleOrDefault();
						arbolPaginaTemp.Remove(master);
						arbolPaginaTemp.Add(new ArbolPagina(this.IdMenu, this.Request.Url.PathAndQuery, this.Title, this.NombreTabla));
					}
				}
				else
				{
					this.ElimitarRegistrosTomadosAnidado(actual.Hija);
					actual.Hija = null;
				}
			}
			this.ArbolPaginas = arbolPaginaTemp;
		}
		
		protected void ElimitarRegistrosTomadosAnidado(ArbolPagina arbolPagina)
		{
			if (arbolPagina != null)
			{
				Uri url = new Uri(string.Format("{0}{1}", @"http:/", arbolPagina.Url));
				string sIdRegistro = HttpUtility.ParseQueryString(url.Query).Get(@"id");
				
				if (!string.IsNullOrEmpty(sIdRegistro))
				{
					int idRegistro = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdRegistro)).Desencriptar());
					string sAccion = HttpUtility.ParseQueryString(url.Query).Get(@"accion");
					
					if (!string.IsNullOrEmpty(sAccion))
					{
						sAccion = Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sAccion)).Desencriptar();
						
						if (sAccion.ToLower() == AccionDetalle.Modificar.ToString().ToLower())
							this.presentadorPaginaBase.EliminarRegistroTomado(arbolPagina.NombreTabla, idRegistro);
					}
				}
				if (arbolPagina.Hija != null)
					this.ElimitarRegistrosTomadosAnidado(arbolPagina.Hija);
			}
		}
		
		/// <summary>Agrega al boton una funcion JS para deshabilitarlo al hacer click</summary>
		/// <param name="ButtonControl">Button. Control que se desea aplicar la funcion.</param>
		protected void DisableButtonOnClick(Button buttonControl)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("if(typeof(Page_ClientValidate) == 'function') { ");
			
			if (string.IsNullOrEmpty(buttonControl.ValidationGroup))
				sb.Append("if(!Page_ClientValidate()) { return false; } } ");
			else
				sb.AppendFormat("if(!Page_ClientValidate('{0}')) {{ return false; }} }} ", buttonControl.ValidationGroup);
			
			sb.Append("this.disabled = true;");
			buttonControl.Attributes.Add("onclick", sb.ToString());
		}
		
		/// <summary>Metodo que recorre los botones para agregarle el metodo JS y deshabilitarlo al hacer click.</summary>
		protected void DisableDoubleClickButtons()
		{
			foreach (Button button in this.Page.Controls.FindAll<Button>())
			{
				button.UseSubmitBehavior = false;
				this.DisableButtonOnClick(button);
			}
		}
		
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
			if (inputManager.InputSettings != null)
			{
				for (int x = 0; x < inputManager.InputSettings.Count; x++)
				{
					if (typeof(NumericTextBoxSetting) == inputManager.InputSettings[x].GetType())
					{
						CultureInfo cultura = Thread.CurrentThread.CurrentCulture;
						((NumericTextBoxSetting)inputManager.InputSettings[x]).Culture = cultura;
					}
				}
			}
		}
		
		/// <summary>Agrega la cultura a los Controles RadDatePicker de Telerik.</summary>
		protected void CultureDatePicker()
		{
			foreach (RadDatePicker control in this.Page.Controls.FindAll<RadDatePicker>())
			{
				this.CultureDatePicker(control);
			}
			if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"JSMDP"))
				this.ClientScript.RegisterClientScriptBlock(typeof(string), @"JSMDP", this.JSMaskDatePicker());
		}
		
		/// <summary>Extra una propiedad del ViewState o de un QueryString.</summary>
		/// <param name="propiedad">String. Nombre de la propiedad que se desea extraer.</param>
		/// <returns>String. Valor de la propiedad extraida.</returns>
		protected string ExtraerDeViewStateOQueryString(string propiedad)
		{
			string valor = string.Empty;
			
			if (this.ViewState[propiedad] != null && this.ViewState[propiedad].ToString() != string.Empty)
				valor = this.ViewState[propiedad].ToString();
			else if (!string.IsNullOrEmpty(this.Request.QueryString[propiedad]))
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
		
		internal P CrearInstanciaPresentador()
		{
			this.clase = typeof(P);
			object[] parametros = new object[1];
			parametros[0] = this.Interfaz;
			return (P)Activator.CreateInstance(this.clase, parametros);
		}
		
		protected void EjecutarMetodoPresentador(string nomberMetodo, object[] parametros)
		{
			try
			{
				object result = (this.clase.GetMethod(nomberMetodo).Invoke(this.Presentador, parametros));
				
				if (result != null)
					result.ToString(); //Para evitar el warning del VS o JustCode...
			}
			catch (TargetInvocationException ex)
			{
				if (this.Errores == string.Empty && this.Mensaje == string.Empty)
					ex.Rethrow();
			}
		}
		
		#endregion
		
		#region Métodos privados
		
		/// <summary>
		/// Función que valida las páginas que no requieren sesión dentro de la aplicación
		/// </summary>
		/// <param name="paginaPadre"></param>
		/// <returns></returns>
		private bool VerificarPaginaSinSesion(string paginaPadre)
		{
			string[] paginas = ConfigurationManager.AppSettings[@"paginasSinSesion"].ToString().Split(',');
			
			if (paginas.Count() > 0 && !paginas[0].ToString().Equals(@""))
			{
				for (int i = 0; i < paginas.Length; i++)
				{
					if (paginaPadre.Contains(paginas[i].ToString()))
						return true;
				}
			}
			return false;
		}
		
		private void MostrarMensaje(string mensaje, string errorType)
		{
			if (mensaje.Length > 0)
			{
				RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				windowManagerTemp.Localization.OK = @"Aceptar";
				
				if (windowManagerTemp != null)
					this.PrintMessage(mensaje, windowManagerTemp, errorType);
			}
		}
		
		private void PrintMessage(string message, RadWindowManager manager, string customErrorType)
		{
			switch(customErrorType)
			{
				case @"Informacion":
					this.PrintInfoMessage(message, manager);
					break;
				case @"Error":
					this.PrintErrorMessage(message, manager);
					break;
				case @"OperacionExitosa":
					this.PrintCheckMessage(message, manager);
					break;
				case @"Advertencia":
					this.PrintAdvertenciaMessage(message, manager);
					break;
			}
		}
		
		/// <summary>
		/// Imprime un mensaje en la pagina a la que se le envia como referencia el RadWindowManager
		/// </summary>
		/// <param name="message">Mensaje a imprimir</param>
		/// <param name="title">Titulo de la Ventana</param>
		/// <param name="width">Ancho de la Ventana</param>
		/// <param name="height">Alto de la Ventana</param>
		/// <param name="manager">Referencia al Window Manager</param>
		private void PrintInfoMessage(string message, RadWindowManager manager)
		{
			string[] imagen = HttpContext.Current.Request.Path.ToString().Split('/');
			
			if(imagen.Length > 0)
				manager.RadAlert(message, 380, 50, @"Información", string.Empty, @"/" + imagen[1] + @"/Imagenes/info.png");
			
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
		private void PrintErrorMessage(string message, RadWindowManager manager)
		{
			string[] imagen = HttpContext.Current.Request.Path.ToString().Split('/');
			
			if(imagen.Length > 0)
				manager.RadAlert(message, 380, 50, @"Error", string.Empty, @"/" + imagen[1] + @"/Imagenes/error.png");
			
			this.ClientScript.RegisterStartupScript(typeof(string), @"JQ", @"fixRadAlert()", true);
		}
		
		/// <summary>
		/// Imprime un mensaje en la página a la que se le envía como referencia el RadWindowManager
		/// </summary>
		/// <param name="message">Mensaje a imprimir</param>
		/// <param name="title">Título de la Ventana</param>
		/// <param name="width">Ancho de la Ventana</param>
		/// <param name="height">Alto de la Ventana</param>
		/// <param name="manager">Referencia al Window Manager</param>
		private void PrintCheckMessage(string message, RadWindowManager manager)
		{
			string[] imagen = HttpContext.Current.Request.Path.ToString().Split('/');
			
			if (imagen.Length > 0)
				manager.RadAlert(message, 380, 50, @"Operación Exitosa", string.Empty, @"/" + imagen[1] + @"/Imagenes/ok.png");
			
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
		private void PrintAdvertenciaMessage(string message, RadWindowManager manager)
		{
			manager.RadAlert(message, 380, 50, @"Advertencia", string.Empty);
			this.ClientScript.RegisterStartupScript(typeof(string), @"JQ", @"fixRadAlert()", true);
		}
		
		/// <summary>Agrega la cultura al control RadDatePicker.</summary>
		/// <param name="control">RadDatePicker. Control que se le coloraca la cultura.</param>
		private void CultureDatePicker(RadDatePicker control)
		{
			CultureInfo cultura = Thread.CurrentThread.CurrentCulture;
			control.Culture = cultura;
			this.idControls.Add(control.Controls[0].ClientID);
		}
		
		/// <summary>
		/// Agrega dinamicamente a las páginas que contenga un control de fecha una mascara con formato dd/mm/yyyy
		/// </summary>
		/// <returns>String. Script colocando la mascara a los controles de Fecha.</returns>
		private string JSMaskDatePicker()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<script type=\"text/javascript\">");
			sb.AppendLine("jQuery(function ($)");
			sb.AppendLine("{");
			
			for (int x = 0; x < this.idControls.Count; x++)
			{
				sb.AppendLine(string.Format("$('#{0}_text').mask('99/99/9999');", this.idControls[x]));
			}
			sb.AppendLine("});");
			sb.AppendLine("</script>");
			return sb.ToString();
		}
		
		private void AplicarPermisosTransacciones()
		{
			if (this.UsuarioActual != null)
			{
				if (SiteMap.CurrentNode != null)
				{
					int idPaginaModulo = string.IsNullOrEmpty(SiteMap.CurrentNode[@"id"]) ? 0 : int.Parse(SiteMap.CurrentNode[@"id"]);
					
					if (idPaginaModulo != 0)
					{
						IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> transaccionesPaginas = this.presentadorPaginaBase.ObtenerTransaccionesPaginasModulos(idPaginaModulo);
						IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> transaccionesPaginasUsuarioActual = this.UsuarioActual.TransaccionesPaginas;
						
						if (!this.UsuarioActual.ObtenerTransaccionesPaginaUsuarioActual(idPaginaModulo, ref transaccionesPaginasUsuarioActual))
						{
							transaccionesPaginasUsuarioActual = (List<HConnexum.Seguridad.TraModAppPagModAppTraModApp>)this.presentadorPaginaBase.ObtenerTransaccionesUsuario(idPaginaModulo, this.UsuarioActual.ObtenerIdRoles(this.IdAplicacion));
							this.UsuarioActual.ActualizarTransaccionesPaginasUsuarioActual(transaccionesPaginasUsuarioActual);
						}
						string[] transaccionesApp = WebConfigurationManager.AppSettings[@"TransaccionesApp"].Split(';');
						
						for (int i = 0; i < transaccionesApp.Length; i++)
						{
							string tempNombreTran = transaccionesApp.GetValue(i).ToString();
							string nombreControl = SiteMap.CurrentNode[tempNombreTran];
							
							if (!string.IsNullOrEmpty(nombreControl))
							{
								List<Control> ctrls = this.Page.Controls.FindAll().Where(control => control.ID == nombreControl).ToList();
								HConnexum.Seguridad.TraModAppPagModAppTraModApp transaccionPagina = transaccionesPaginas.Where(tranPag => tranPag.NombreTipoTransaccion == tempNombreTran).FirstOrDefault();
								
								if (transaccionPagina != null)
								{
									if (ctrls.Count > 0)
									{
										if (transaccionesPaginasUsuarioActual.Where(t => t.Id == transaccionPagina.Id && t.IndEliminado == false).Count() > 0)
										{
											if (ctrls[0] is Button || ctrls[0] is RadToolBarItem || ctrls[0] is Publicacion || ctrls[0] is Auditoria)
												ctrls[0].Visible = true;
										}
										else
										{
											if (ctrls[0] is Button || ctrls[0] is RadToolBarItem || ctrls[0] is Publicacion || ctrls[0].GetType().Name == "Auditoria")
											{
												ctrls[0].Visible = false;
												this.ClearRowDblClickEvent(tempNombreTran);
											}
										}
									}
								}
								else
								{
									if (ctrls.Count > 0)
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
			if (this.UsuarioActual != null)
			{
				if (SiteMap.CurrentNode != null)
				{
					int idPaginaModulo = string.IsNullOrEmpty(SiteMap.CurrentNode[@"id"]) ? 0 : int.Parse(SiteMap.CurrentNode[@"id"]);
					
					if (idPaginaModulo != 0)
					{
						IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> transaccionesPaginasUsuarioActual = this.UsuarioActual.TransaccionesPaginas;
						
						if (this.UsuarioActual.ObtenerTransaccionesPaginaUsuarioActual(idPaginaModulo, ref transaccionesPaginasUsuarioActual))
							if (!SiteMap.CurrentNode.Url.Contains(@"Default.aspx"))
								this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"GetRadWindow().Close()", true);
					}
					else if (!SiteMap.CurrentNode.Url.Contains(@"Default.aspx"))
						this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"GetRadWindow().Close()", true);
				}
			}
		}
		
		private void ClearRowDblClickEvent(string tempNombreTran)
		{
			if (tempNombreTran.Equals(WebConfigurationManager.AppSettings[@"TransaccionEditar"]))
			{
				RadGrid gridTemp = this.Page.Controls.FindAll<RadGrid>().FirstOrDefault();
				
				if (gridTemp != null)
					gridTemp.ClientSettings.ClientEvents.OnRowDblClick = string.Empty;
			}
		}
		
		/// <summary>
		/// Toma el nombre de las paginas para ser colocado en el title de las mismas
		/// </summary>
		/// <returns></returns>
		private string ObtenerTitulo()
		{
			if (this.IdPaginaModulo != 0)
			{
				string nombrePagina = this.presentadorPaginaBase.ObtenerPaginasModulos(this.IdPaginaModulo);
				
				if (!string.IsNullOrEmpty(nombrePagina))
					return nombrePagina;
			}
			if (!string.IsNullOrEmpty(this.Title))
				return this.Title;
			
			return ConfigurationManager.AppSettings[@"PageDefaultName"];
		}
		
		#endregion
	}
}