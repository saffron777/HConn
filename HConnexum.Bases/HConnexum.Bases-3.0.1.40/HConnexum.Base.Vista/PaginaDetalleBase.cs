using System;
using System.Linq;
using System.Web.UI.WebControls;
using HConnexum.Base.ControlAuditoria;
using HConnexum.Base.ControlPublicacion;
using HConnexum.Base.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using Telerik.Web.UI;
using System.ServiceModel;
using System.Configuration;
using System.Web;

namespace HConnexum.Base.Vista
{
	/// <summary>
	/// Actúa como clase base para las páginas tipo Detalle, heredando las funcionalidades de la clase PaginaBase. Esta clase proporciona los métodos y propiedades necesarios para el funcionamiento de las páginas tipo Detalle. 
	/// </summary>
	public class PaginaDetalleBase<T> : PaginaBase<T>, IDetalleBase
	{
		#region Eventos
		
		/// <summary>Evento de inicialización de la página.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected new void Page_Init(object sender, EventArgs e)
		{
			this.Presentador = this.CrearInstanciaPresentador();
			
			if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"DRSB"))
				this.ClientScript.RegisterClientScriptInclude(typeof(string), @"DRSB", this.ResolveClientUrl(@"~/Scripts/DetalleRadScriptBlock1.js"));
			
			RadInputManager radInputManager = this.Page.Controls.FindAll<RadInputManager>().FirstOrDefault();
			
			if (radInputManager != null)
				this.CultureNumericInput(this.Page.Controls.FindAll<RadInputManager>().FirstOrDefault());
			
			this.CultureDatePicker();
			base.Page_Init(sender, e);
		}
		
		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected new void Page_Load(object sender, EventArgs e)
		{
			this.Page_Load_1(sender, e);
			
			if (!this.Page.IsPostBack)
			{
				this.RutaPadreEncriptada = this.ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect;
				this.EjecutarMetodoPresentador(@"MostrarVista", null);
			}
		}
		
		internal void Page_Load_1(object sender, EventArgs e)
		{
			base.Page_Load(sender, e);
		}
		
		/// <summary>Evento previo al renderizado de la this.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected new void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
			
			if (this.Accion == AccionDetalle.Ver)
				this.BloquearControles(true);
			
			this.MostrarBotones(this.Page.Controls.FindAll<Button>().Where(a => a.ID == @"cmdGuardar").FirstOrDefault(), this.Page.Controls.FindAll<Button>().Where(a => a.ID == @"cmdGuardaryAgregar").FirstOrDefault(), this.Page.Controls.FindAll<Button>().Where(a => a.ID == @"cmdLimpiar").FirstOrDefault(), accion);
		}
		
		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void CmdGuardar_Click(object sender, EventArgs e)
		{
			this.Errores = string.Empty;
			this.Mensaje = string.Empty;
			this.EjecutarMetodoPresentador(@"GuardarCambios", null);
			
			if (this.Errores == string.Empty && this.Mensaje == string.Empty)
				this.AbandonarPagina();
		}
		
		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar y agregar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void CmdGuardaryAgregar_Click(object sender, EventArgs e)
		{
			SwebErrorClient swebErrorClient = new SwebErrorClient();
			try
			{
				this.Errores = string.Empty;
				this.Mensaje = string.Empty;
				this.EjecutarMetodoPresentador(@"GuardarCambios", null);
				
				if (this.Errores == string.Empty && this.Mensaje == string.Empty)
				{
					this.LimpiarControles();
					this.TipoMensaje = ErrorType.OperacionExitosa.ToString();
					this.Mensaje = swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"CmdGuardaryAgregar_Click", UsuarioActual.Login, @"0025", Session.SessionID, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString);
				}
			}
			finally
			{
				if(swebErrorClient.State != CommunicationState.Closed)
					swebErrorClient.Close();
			}
		}
		
		#endregion
		
		#region Métodos
		
		/// <summary>
		/// Método que cierra o redirecciona la página actual al ser abandonada
		/// </summary>
		private void AbandonarPagina()
		{
			if (this.ArbolPaginas.ArbolPaginaActualIsNode())
				this.Response.Redirect(@"~" + this.ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
			else
				this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), @"CerrarVentana", @"setTimeout('cerrarVentana()', 0);", true);
		}
		
		/// <summary>Metodo que permite visualar los botones o ocultarlos dependiendo de la accion.</summary>
		/// <param name="guardar">Button. Boton Guardar.</param>
		/// <param name="agregarOtro">Button. Boton AgregarOtro.</param>
		/// <param name="limpiar">Button. Boton Limpiar. </param>
		/// <param name="accion">AccionDetalle. Accion ejecutada sobre la this.</param>
		private void MostrarBotones(Button guardar, Button agregarOtro, Button limpiar, AccionDetalle accion)
		{
			switch (accion)
			{
				case AccionDetalle.Agregar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = agregarOtro.Visible == false ? false : true;
					limpiar.Visible = limpiar.Visible == false ? false : true;
					break;
				case AccionDetalle.Modificar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				case AccionDetalle.Ver:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				default:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
			}
		}
		
		#endregion
		
		#region Propiedades
		
		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		private int id = 0;
		
		/// <summary>Variable que contiene la acción ejecutada sobre una this.</summary>
		private AccionDetalle accion = AccionDetalle.NoEstablecida;
		
		/// <summary>Propiedad que obtiene o asigna el Id Encriptado.</summary>
		protected string RutaPadreEncriptada { get; private set; }
		
		/// <summary>Propiedad que obtiene o asigna el Id Encriptado.</summary>
		protected string IdEncriptado
		{
			get
			{
				return this.Request.QueryString[@"id"];
			}
		}
		
		/// <summary>Propiedad que obtiene o asigna la accion.</summary>
		public AccionDetalle Accion
		{
			get
			{
				if (this.accion == AccionDetalle.NoEstablecida)
				{
					string sAccion = this.ExtraerDeViewStateOQueryString(@"accion");
					
					switch (sAccion.ToLower())
					{
						case @"agregar":
							this.accion = AccionDetalle.Agregar;
							break;
						case @"modificar":
							this.accion = AccionDetalle.Modificar;
							break;
						case @"ver":
							this.accion = AccionDetalle.Ver;
							break;
						default:
							this.accion = AccionDetalle.NoEstablecida;
							break;
					}
				}
				return this.accion;
			}
			set
			{
				switch (value)
				{
					case AccionDetalle.Agregar:
						this.ViewState[@"accion"] = @"agregar";
						break;
					case AccionDetalle.Modificar:
						this.ViewState[@"accion"] = @"modificar";
						break;
					case AccionDetalle.Ver:
						this.ViewState[@"accion"] = @"ver";
						break;
					default:
						this.ViewState[@"accion"] = @"ver";
						break;
				}
				this.accion = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		public int Id
		{
			get
			{
				if (this.id == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString(@"id");
					
					if (sId != @"")
						this.id = Convert.ToInt32(sId);
				}
				return this.id;
			}
			set
			{
				this.id = Convert.ToInt32(value);
				this.ViewState[@"id"] = this.id;
			}
		}
		
		///<summary>Propiedad de auditoria que asigna u obtiene el indicador de eliminado.</summary>
		public string IndEliminado
		{
			get
			{
				return (this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().IndEliminado;
			}
			set
			{
				(this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().IndEliminado = value;
			}
		}
		
		///<summary>Propiedad de auditoria que asigna u obtiene el usuario creador del regitros.</summary>
		public string CreadoPor
		{
			get
			{
				return (this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().CreadoPor;
			}
			set
			{
				(this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().CreadoPor = value;
			}
		}
		
		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de creación.</summary>
		public string FechaCreacion
		{
			get
			{
				return (this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().FechaCreacion;
			}
			set
			{
				(this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().FechaCreacion = value;
			}
		}
		
		///<summary>Propiedad de auditoria que asigna u obtiene el usuario que modificó el regitros.</summary>
		public string ModificadoPor
		{
			get
			{
				return (this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().ModificadoPor;
			}
			set
			{
				(this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().ModificadoPor = value;
			}
		}
		
		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de modificación.</summary>
		public string FechaModificacion
		{
			get
			{
				return (this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().FechaModificacion;
			}
			set
			{
				(this.Page.Controls.FindAll<Auditoria>()).FirstOrDefault().FechaModificacion = value;
			}
		}
		
		///<summary>Propiedad de publicación que asigna u obtiene la fecha de validez.</summary>
		public string FechaValidez
		{
			get
			{
				return (this.Page.Controls.FindAll<Publicacion>()).FirstOrDefault().FechaValidez;
			}
			set
			{
				(this.Page.Controls.FindAll<Publicacion>()).FirstOrDefault().FechaValidez = value;
			}
		}
		
		///<summary>Propiedad de publicación que asigna u obtiene el indicador de vigencia.</summary>
		public string IndVigente
		{
			get
			{
				return (this.Page.Controls.FindAll<Publicacion>()).FirstOrDefault().IndVigente;
			}
			set
			{
				(this.Page.Controls.FindAll<Publicacion>()).FirstOrDefault().IndVigente = value;
			}
		}
		
		#endregion
	}
}