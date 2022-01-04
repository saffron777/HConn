using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Utilitarios
{
	///<summary>Clase FlujosEjecucionLista.</summary>
	public partial class TransporteFlujoServicio : PaginaBase, ITransporteFlujoServicio
	{
		#region "Variables Locales"
		///<summary>Variable presentador TransporteFlujoServicioPresentador.</summary>
		TransporteFlujoServicioPresentador presentador;
		#endregion "Variables Locales"

		#region "Eventos de la Página"
		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new TransporteFlujoServicioPresentador(this);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
				if(!Page.IsPostBack)
					this.presentador.LlenarCombos();
				if(chkContrasena.Checked)
					txtContrasena.TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
				else
					txtContrasena.TextMode = System.Web.UI.WebControls.TextBoxMode.Password;
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Evento pre visualización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
		}

		public void ddlFlujoServicio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
				ddlVersion.ClearSelection();
				presentador.LlenarVersion();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void ddlVersion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
				presentador.ObtenerDatosFlujoServico();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void btnProbarConexion_OnClick(object sender, EventArgs e)
		{
			try
			{
				presentador.ProbarConexion();
				RadWindowManager windowManager = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                windowManager.RadAlert(Mensaje, 380, 50, @"Estatus Conexión", "");
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdGenerar_Click(object sender, EventArgs e)
		{
			try
			{
				RadWindowManager windowManager = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				if(presentador.GenerarFlujoServico())
                    windowManager.RadAlert(!string.IsNullOrEmpty(Mensaje) ? Mensaje : @"Flujo servicio generado exitosamente", 380, 50,@"Generación exitosa", "");
				else
                    windowManager.RadAlert(Mensaje, 380, 50, @"Error", "");
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		public string IdFlujoServicio
		{
			get
			{
				return this.ddlFlujoServicio.SelectedValue;
			}
			set
			{
				this.ddlFlujoServicio.SelectedValue = value;
			}
		}

		public IEnumerable<string> ComboFlujoServicio
		{
			set
			{
				this.ddlFlujoServicio.DataSource = value;
				this.ddlFlujoServicio.DataBind();
			}
		}

		public string IdVersion
		{
			get
			{
				return this.ddlVersion.SelectedValue;
			}
			set
			{
				this.ddlVersion.SelectedValue = value;
			}
		}

		public IEnumerable<FlujosServicioDTO> ComboVersion
		{
			set
			{
				this.ddlVersion.DataSource = value;
				this.ddlVersion.DataBind();
			}
		}

		public IList<FlujosServicioDTO> FlujoServicios
		{
			set
			{
				ViewState["FlujoServicios"] = value;
			}
			get
			{
				return ViewState["FlujoServicios"] as IList<FlujosServicioDTO>;
			}
		}

		public string Servidor
		{
			get
			{
				return txtServidor.Text;
			}
			set
			{
				txtServidor.Text = value;
			}
		}

		public string Instancia
		{
			get
			{
				return txtInstancia.Text;
			}
			set
			{
				txtInstancia.Text = value;
			}
		}

		public string BD
		{
			get
			{
				return txtBD.Text;
			}
			set
			{
				txtBD.Text = value;
			}
		}

		public string Usuario
		{
			get
			{
				return txtUsuario.Text;
			}
			set
			{
				txtUsuario.Text = value;
			}
		}

		public string Contrasena
		{
			get
			{
				return txtContrasena.Text;
			}
			set
			{
				txtContrasena.Text = value;
			}
		}

		public string CantidadServiciosSucursales
		{
			get
			{
				return lblServiciosSucursales.Text;
			}
			set
			{
				lblServiciosSucursales.Text = value;
			}
		}

		public string CantidadAlcanceGeografico
		{
			get
			{
				return lblAlcanceGeografico.Text;
			}
			set
			{
				lblAlcanceGeografico.Text = value;
			}
		}

		public string CantidadEtapas
		{
			get
			{
				return lblEtapas.Text;
			}
			set
			{
				lblEtapas.Text = value;
			}
		}

		public string CantidadPasos
		{
			get
			{
				return lblPasos.Text;
			}
			set
			{
				lblPasos.Text = value;
			}
		}

		public string CantidadSolicitudBloques
		{
			get
			{
				return lblSolicitudBloques.Text;
			}
			set
			{
				lblSolicitudBloques.Text = value;
			}
		}

		public string CantidadPasosBloques
		{
			get
			{
				return lblPasosBloques.Text;
			}
			set
			{
				lblPasosBloques.Text = value;
			}
		}

		public string CantidadBloques
		{
			get
			{
				return lblBloques.Text;
			}
			set
			{
				lblBloques.Text = value;
			}
		}

		public string CantidadPasosRespuestas
		{
			get
			{
				return lblPasosRespuestas.Text;
			}
			set
			{
				lblPasosRespuestas.Text = value;
			}
		}

		public string CantidadParametrosAgenda
		{
			get
			{
				return lblParametrosAgenda.Text;
			}
			set
			{
				lblParametrosAgenda.Text = value;
			}
		}

		public string CantidadChaPasos
		{
			get
			{
				return lblCHAPasos.Text;
			}
			set
			{
				lblCHAPasos.Text = value;
			}
		}

		public string CantidadMensajeMetodosDestinatarios
		{
			get
			{
				return lblMensajeMetodosDestinatarios.Text;
			}
			set
			{
				lblMensajeMetodosDestinatarios.Text = value;
			}
		}

		public string CantidadFlujoEjecucion
		{
			get
			{
				return lblFlujoEjecucion.Text;
			}
			set
			{
				lblFlujoEjecucion.Text = value;
			}
		}

		public string CantidadTipoPasos
		{
			get
			{
				return lblTipoPasos.Text;
			}
			set
			{
				lblTipoPasos.Text = value;
			}
		}

		public string CantidadDocumentosServicios
		{
			get
			{
				return lblDocumentosServicios.Text;
			}
			set
			{
				lblDocumentosServicios.Text = value;
			}
		}

		public string CantidadDocumentosPasos
		{
			get
			{
				return lblDocumentosPasos.Text;
			}
			set
			{
				lblDocumentosPasos.Text = value;
			}
		}

		public string CantidadDocumentos
		{
			get
			{
				return lblDocumentos.Text;
			}
			set
			{
				lblDocumentos.Text = value;
			}
		}

		public string CantidadCamposIndexacion
		{
			get
			{
				return lblCamposIndexacion.Text;
			}
			set
			{
				lblCamposIndexacion.Text = value;
			}
		}

		public string CantidadAtributosArchivo
		{
			get
			{
				return lblAtributosArchivo.Text;
			}
			set
			{
				lblAtributosArchivo.Text = value;
			}
		}

		public string Mensaje
		{
			get
			{
				if(ViewState["FluMensajejoServicios"] != null)
					return ViewState["FluMensajejoServicios"].ToString();
				return string.Empty;
			}
			set
			{
				ViewState["FluMensajejoServicios"] = value.Replace("'", "\"").Replace("\r\n", "<br/>");
			}
		}
		#endregion "Propiedades de Presentación"
	}
}