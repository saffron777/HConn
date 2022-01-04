using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Orquestador
{
	public partial class PantallaContenedora2 : PaginaDetalleBase, IPantallaContenedora2
	{
		#region "Variables Locales"
		///<summary>Variable presentador PasosBloquePresentadorDetalle.</summary>
		PantallaContenedora2Presentador presentador;
		#endregion "Variables Locales"

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if(ViewState[@"controsladded"] == null)
				presentador.CrearControles(this.Master);
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
				presentador = new PantallaContenedora2Presentador(this);

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
				if(!Page.IsPostBack)
				{
					PanelMaster.Visible = presentador.VerificarBloquePrincipal();
					presentador.LlenarCombos();
					presentador.MostrarVista();
					presentador.CrearControles(this.Master);
					ViewState[@"controlsadded"] = true;
					base.ManejoNavegacionArbolPagina();
                    ClearControls(this);
				}
				pCasoRelacionado.Visible = UsuarioActual != null;
                
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

		protected void ddlTipDoc_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			BuscarSolicitante();
		}

		protected void txtNumDoc_TextChanged(object sender, EventArgs e)
		{
			BuscarSolicitante();
		}

		private void BuscarSolicitante()
		{
			bool respuesta = false;
			Telefono = Email = Apellidos = txtNombres.Text = string.Empty;
			if(!string.IsNullOrEmpty(TipDoc) && !string.IsNullOrEmpty(NumDoc))
			{
				respuesta = presentador.BuscarSolicitante();
				txtTelefono.Enabled = txtCorreo.Enabled = txtApellidos.Enabled = txtNombres.Enabled = !respuesta;
			}
			RadInputManager1.Enabled = !respuesta;
		}

		protected void cmdGuardar_Click(object sender, EventArgs e)
		{
			if(ValidarControles())
			{
				RadWindowManager windowManager = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				if(presentador.GuardarCambios(this.Master))
				{
					string funcion = string.Empty;
					if(ArbolPaginas.ArbolPaginaActualIsNode())
						funcion = @"Redirect";
					else
						funcion = @"CerrarVentana";
                    windowManager.Localization.OK = "Aceptar";
                    windowManager.RadAlert(Mensaje, 380, 50, "Solicitud de Servicio", funcion);
				}
				else
				{
                    if (!string.IsNullOrEmpty(Mensaje))
                    {
                        windowManager.Localization.OK = "Aceptar";
                        windowManager.RadAlert(Mensaje, 380, 50, "Error en la solicitud", "");
                    }
				}
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
		public string Suscriptor
		{
			get { return txtSuscriptor.Text; }
			set { txtSuscriptor.Text = value; }
		}

		public string Servicio
		{
			get { return txtServicio.Text; }
			set { txtServicio.Text = value; }
		}

		public string FechaSolicitud
		{
			get { return txtFechaSolicitud.Text; }
			set { txtFechaSolicitud.Text = value; }
		}

		public IEnumerable<ListasValorDTO> ComboTipDoc
		{
			set
			{
				this.ddlTipDoc.DataSource = value;
				this.ddlTipDoc.DataBind();
			}
		}

		public string TipDoc
		{
			get { return this.ddlTipDoc.SelectedValue; }
			set { this.ddlTipDoc.SelectedValue = value; }
		}

		public string NumDoc
		{
			get { return txtNumDoc.Text; }
			set { txtNumDoc.Text = value; }
		}

		public string Nombres
		{
			get { return txtNombres.Text; }
			set { txtNombres.Text = value; }
		}

		public string Apellidos
		{
			get { return txtApellidos.Text; }
			set { txtApellidos.Text = value; }
		}

		public string Email
		{
			get { return txtCorreo.Text; }
			set { txtCorreo.Text = value; }
		}

		public string Telefono
		{
			get { return txtTelefono.NumeroTlfCompletoGuion; }
			set { txtTelefono.NumeroTlfCompletoGuion = value; }
		}

		public string CasoRelacionado
		{
			get { return txtCasoRelacionado.Text; }
			set { txtCasoRelacionado.Text = value; }
		}

		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		int idflujoservicio = 0;

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public int IdFlujoServicio
		{
			get
			{
				if(this.idflujoservicio == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString(@"idflujoservicio");
					if(sId != "")
						this.idflujoservicio = Convert.ToInt32(sId);
				}
				return this.idflujoservicio;
			}
			set
			{
				this.idflujoservicio = Convert.ToInt32(value);
				this.ViewState["idflujoservicio"] = this.idflujoservicio;
			}
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

		public string Mensaje
		{
			get
			{
				return string.Empty + this.ViewState[@"Mensaje"];
			}
			set
			{
				this.ViewState[@"Mensaje"] = value;
			}
		}

		public string UrlRedirect
		{
			get
			{
				return "../Estructura/MisActividades.aspx?IdMenu=" + IdMenuEncriptado;
			}
		}
		#endregion
	}
}