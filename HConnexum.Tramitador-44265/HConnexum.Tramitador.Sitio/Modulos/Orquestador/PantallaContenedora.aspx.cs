using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using System.Linq;

namespace HConnexum.Tramitador.Sitio.Modulos.Orquestador
{
	public partial class PantallaContenedora : PaginaDetalleBase, IPantallaContenedora
	{
		#region "Variables Locales"
		///<summary>Variable presentador PasosBloquePresentadorDetalle.</summary>
		PantallaContenedoraPresentador presentador;
		bool _swNotPendingStatus = false;
		#endregion "Variables Locales"

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if(ViewState[@"controsladded"] == null)
				presentador.CrearControles(IdMovimiento, this.Master, Session["Servicio"].ToString());
		}

		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new PantallaContenedoraPresentador(this);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if(!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"CFS"))
					this.Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"CFS", this.ResolveClientUrl(@"~/Scripts/jquery.coolfieldset.js"));
				if(!Page.IsPostBack)
				{
					presentador.ValidarEstatusMovimiento(IdMovimiento);
					presentador.ValidaPadreHijoNavegacion(IdMovimiento);
					presentador.CrearControles(IdMovimiento, this.Master, Session["Servicio"].ToString());
					ViewState[@"controlsadded"] = true;
					base.ManejoNavegacionArbolPagina();
					if(IdCaso != null)
					{
						controlChat.CasoId = IdCaso;
						controlChat.IdMovimiento = IdMovimiento;
						controlChat.EnvioSuscriptorId = UsuarioActual.SuscriptorSeleccionado.Id;
						controlChat.Remitente = string.Format("{0} {1}", UsuarioActual.DatosBase.Nombre1, UsuarioActual.DatosBase.Apellido1).Trim();
						controlChat.CreacionUsuario = UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						int mensaje = presentador.BuscaMensajesPendienteChat(IdCaso);
						if(mensaje > 0)
							cmdChat.Text = @"Chat Mensajes (" + mensaje + @")";
						else
							cmdChat.Text = @"Chat";
					}
                    ClearControls(this);
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
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

		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_LoadComplete(object sender, EventArgs e)
		{
			try
			{
				if(!Page.IsPostBack)
					presentador.CargarDatosControles(IdMovimiento, this.Master);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void cmdGuardar_Click(object sender, EventArgs e)
		{
			if(ValidarControles())
			{
				bool? bInterventor = false;
				bInterventor = IntervieneMovimiento(bInterventor);
				presentador.GuardarCambios(IdMovimiento, this.Master, false, bInterventor);
				if(swNotPendingStatus)
					this.Errores = "Este movimiento ha sido actualizado por otro usuario, por favor oprima botón de CANCELAR e intente de nuevo.";
				else
				{
					if(ArbolPaginas.ArbolPaginaActualIsNode())
						this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre(1).UrlRedirect);
					else
						this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"setTimeout('cerrarVentana()', 0);", true);
				}
			}
		}

		protected void cmdGuardaryContinuar_Click(object sender, EventArgs e)
		{
			if(ValidarControles())
			{
				bool? bInterventor = false;
				bInterventor = IntervieneMovimiento(bInterventor);
				int idMovimientoNuevo = presentador.GuardarCambios(IdMovimiento, this.Master, true, bInterventor);
				if(swNotPendingStatus)
					this.Errores = "Este movimiento ha sido actualizado por otro usuario, por favor oprima botón de CANCELAR e intente de nuevo.";
				else
				{
					if(idMovimientoNuevo == 0)
					{
						if(ArbolPaginas.Count == 0)
							this.Response.Redirect(@"~/Modulos/Estructura/ConsultaCasos.aspx?IdMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())), false);
						else
						{
							if(ArbolPaginas.ArbolPaginaActualIsNode())
								this.Response.Redirect(@"~/Modulos/Estructura/MisActividades.aspx?IdMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())));
							else
								this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"setTimeout('cerrarVentana()', 0);", true);
						}
					}
					else
					{
						List<ArbolPagina> arbolPaginaTemp = ArbolPaginas;
						ArbolPagina padre = arbolPaginaTemp.ObtenerArbolPaginaPadre();
						if(padre != null)
						{
							padre.Hija = null;
							ArbolPaginas = arbolPaginaTemp;
						}
						this.Response.Redirect(@"~/Modulos/Orquestador/PantallaContenedora.aspx?IdMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&idmovimiento=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(idMovimientoNuevo.ToString().Encriptar())));
					}
				}
			}
		}

		private bool? IntervieneMovimiento(bool? bInterventor)
		{
			if(Intervencion == "FCF")
				bInterventor = true;
			if(Intervencion == "F")
				bInterventor = false;
			if(Intervencion == "")
				bInterventor = null;
			return bInterventor;
		}

		public void cmdcancelar_Click(object sender, EventArgs e)
		{
			if(ArbolPaginas.Count == 0)
				this.Response.Redirect(@"~/Modulos/Estructura/ConsultaCasos.aspx?IdMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())), false);
			else
			{
				if(ArbolPaginas.ArbolPaginaActualIsNode())
					this.Response.Redirect(@"~/Modulos/Estructura/MisActividades.aspx?IdMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())));
				else
					this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"setTimeout('cerrarVentana()', 0);", true);
			}
		}

		public bool ValidarControles()
		{
			StringBuilder errores = new StringBuilder();
			IEnumerable<Control> controlesEntrada = pContenedor.Controls.FindAll<UserControl>();
			foreach(Control control in controlesEntrada)
			{
				Type controlType = control.GetType();
				MethodInfo controlMethod = controlType.GetMethod(@"ValidarDatos");
				if(controlMethod != null)
				{
					string error = (controlMethod.Invoke(control, new object[] { })).ToString();
					if(error.Length > 0)
						errores.AppendWithBreak(error);
				}
			}
			if(errores.Length > 0)
			{
				Errores = errores.ToString();
				return false;
			}
			return true;
		}

		#region "Propiedades de Presentación"
		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		int id = 0;

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public int IdMovimiento
		{
			get
			{
				if(this.id == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString(@"idmovimiento");
					if(sId != "")
						this.id = Convert.ToInt32(sId);
				}
				return this.id;
			}
			set
			{
				this.id = Convert.ToInt32(value);
				this.ViewState["idmovimiento"] = this.id;
			}
		}

		/// <summary>
		/// Contiene el id del movimiento padre, utilizado por el hijo al momento de usar el "Atras"
		/// </summary>
		public string IdMovimientoPadre
		{
			get { return this.ViewState["idMovimientoPadre"].ToString(); }
			set { this.ViewState["idMovimientoPadre"] = value; }
		}

		/// <summary>
		/// Contiene el Id del movimiento hijo que referencia al padre mediante el boton "Atras"
		/// </summary>
		public string IdMovimientoHijo
		{
			get { return this.ExtraerDeViewStateOQueryString(@"idMovimientoHijo"); }
			set { this.ViewState["idMovimientoHijo"] = value; }
		}

		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		int idMenu = 0;

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public int IdMenu
		{
			get
			{
				if(this.idMenu == 0)
				{
					string sIdMenu = this.ExtraerDeViewStateOQueryString(@"IdMenu");
					if(sIdMenu != "")
						this.idMenu = Convert.ToInt32(sIdMenu);
				}
				return this.idMenu;
			}
			set
			{
				this.idMenu = Convert.ToInt32(value);
				this.ViewState[@"IdMenu"] = this.idMenu;
			}
		}

		/// <summary>Titulo de la pagina.</summary>
		public string NombrePagina
		{
			get { return this.Title; }
			set { this.Title = value; }
		}

		public int IdCaso
		{
			get { int idcaso = int.Parse(this.ViewState[@"IdCaso"].ToString()); return idcaso; }
			set { this.ViewState[@"IdCaso"] = value; }
		}

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public string Intervencion
		{
			get
			{
				string sIntervencion = this.ExtraerDeViewStateOQueryString(@"Intervencion");
				return sIntervencion;
			}
			set
			{
				this.ViewState["Intervencion"] = value;
			}
		}

		public bool swNotPendingStatus
		{
			get { return this._swNotPendingStatus; }
			set { this._swNotPendingStatus = value; }
		}
		#endregion
	}
}
