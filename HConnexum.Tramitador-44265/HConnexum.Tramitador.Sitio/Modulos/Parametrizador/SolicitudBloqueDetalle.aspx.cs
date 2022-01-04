using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary> 
namespace HConnexum.Tramitador.Sitio
{
	///<summary>Clase SolicitudBloqueLista.</summary>
	public partial class SolicitudBloqueDetalle : PaginaDetalleBase, ISolicitudBloqueDetalle
	{
		#region "Variables Locales"
		///<summary>Variable presentador SolicitudBloquePresentadorDetalle.</summary>
		SolicitudBloquePresentadorDetalle presentador;
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
				this.presentador = new SolicitudBloquePresentadorDetalle(this);
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
				base.Page_Load(sender, e);
				if(!this.Page.IsPostBack)
				{
					this.CultureDatePicker();
					presentador.LlenarCombos();
					if(this.Accion == AccionDetalle.Ver || this.Accion == AccionDetalle.Modificar)
						presentador.MostrarVista();
					if(this.Accion == AccionDetalle.Modificar)
						this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento pre visualización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
			if(this.Accion == AccionDetalle.Ver) this.BloquearControles(true);
			this.MostrarBotones(this.cmdGuardar, this.cmdGuardaryAgregar, this.cmdLimpiar, this.Accion);
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				this.Page.Controls.isValidSpecialCharacterEmptySpace();
				this.presentador.GuardarCambios(this.Accion);
				if(ArbolPaginas.ArbolPaginaActualIsNode())
					this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
				else
					this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "setTimeout('cerrarVentana()', 0);", true);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar y agregar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdGuardaryAgregar_Click(object sender, EventArgs e)
		{
			try
			{
				this.Page.Controls.isValidSpecialCharacterEmptySpace();
				this.presentador.GuardarCambios(this.Accion);
				this.LimpiarControles();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		public int Id
		{
			get { return base.Id; }
			set { base.Id = value; }
		}

		public int IdFlujoServicio
		{
			get { return base.Id; }
			set { base.Id = value; }
		}

		public string IdBloque
		{
			get { return ddlIdBloque.SelectedValue; }
			set { ddlIdBloque.SelectedValue = value; }
		}

		public IEnumerable<BloqueDTO> ComboIdBloque
		{
			set
			{
				ddlIdBloque.DataSource = value;
				ddlIdBloque.DataBind();
			}
		}

		public string Orden
		{
			get { return txtOrden.Text; }
			set { txtOrden.Text = string.Format("{0:N0}", value); }
		}

		public string IndCierre
		{
			get { return chkIndCierre.Checked.ToString(); }
			set { chkIndCierre.Checked = ExtensionesString.ConvertirBoolean(value); }
		}

		public string IdTipoControl
		{
			get { return ddlIdTipoControl.SelectedValue; }
			set { ddlIdTipoControl.SelectedValue = value; }
		}

		public IEnumerable<ListasValorDTO> ComboIdTipoControl
		{
			set
			{
				ddlIdTipoControl.DataSource = value;
				ddlIdTipoControl.DataBind();
			}
		}

		public string TituloBloque
		{
			get { return txtTituloBloque.Text; }
			set { txtTituloBloque.Text = value; }
		}

		public string IndActualizable
		{
			get { return chkIndActualizable.Checked.ToString(); }
			set { chkIndActualizable.Checked = ExtensionesString.ConvertirBoolean(value); }
		}

		public string KeyCampoXML
		{
			get { return txtKeyCampoXML.Text; }
			set { txtKeyCampoXML.Text = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el indicador de eliminado.</summary>
		public string IndEliminado
		{
			get { return this.Auditoria.IndEliminado.ToString(); }
			set { this.Auditoria.IndEliminado = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el usuario creador del regitros.</summary>
		public string CreadoPor
		{
			get { return this.Auditoria.CreadoPor; }
			set { this.Auditoria.CreadoPor = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de creación.</summary>
		public string FechaCreacion
		{
			get { return this.Auditoria.FechaCreacion; }
			set { this.Auditoria.FechaCreacion = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el usuario que modificó el regitros.</summary>
		public string ModificadoPor
		{
			get { return this.Auditoria.ModificadoPor; }
			set { this.Auditoria.ModificadoPor = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de modificación.</summary>
		public string FechaModificacion
		{
			get { return this.Auditoria.FechaModificacion; }
			set { this.Auditoria.FechaModificacion = value; }
		}

		///<summary>Propiedad de publicación que asigna u obtiene la fecha de validez.</summary>
		public string FechaValidez
		{
			get { return this.Publicacion.FechaValidez; }
			set { this.Publicacion.FechaValidez = value; }
		}

		///<summary>Propiedad de publicación que asigna u obtiene el indicador de vigencia.</summary>
		public string IndVigente
		{
			get { return this.Publicacion.IndVigente; }
			set { this.Publicacion.IndVigente = value; }
		}
		#endregion "Propiedades de Presentación"
	}
}