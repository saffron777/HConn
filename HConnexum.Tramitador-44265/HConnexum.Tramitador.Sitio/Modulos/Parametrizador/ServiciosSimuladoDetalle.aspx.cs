using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Data;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary> 
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
	///<summary>Clase ServiciosSimuladoLista.</summary>
	public partial class ServiciosSimuladoDetalle : PaginaDetalleBase, IServiciosSimuladoDetalle
	{
		#region "Variables Locales"
		///<summary>Variable presentador ServiciosSimuladoPresentadorDetalle.</summary>
		ServiciosSimuladoPresentadorDetalle presentador;
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
				this.presentador = new ServiciosSimuladoPresentadorDetalle(this);
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
                    if (this.Accion == AccionDetalle.Ver || this.Accion == AccionDetalle.Modificar)
                    {
                        presentador.MostrarVista();
                        presentador.LlenarComboSuscriptorASimular(ddlIdServicioSuscriptor.SelectedValue);
                    }
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
		protected void CmdGuardarClick(object sender, EventArgs e)
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
		protected void CmdGuardaryAgregarClick(object sender, EventArgs e)
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


        protected void DdlIdServicioSuscriptorSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            presentador.LlenarComboSuscriptorASimular(ddlIdServicioSuscriptor.SelectedValue);
        }

        #endregion "Eventos de la Página"

		
        #region "Propiedades de Presentación"
		public int Id
		{
		    get { return base.Id; }
		    set { base.Id = value; }
		}

        public string IdServicioSuscriptor
        {
            get { return ddlIdServicioSuscriptor.SelectedValue; }
            set { ddlIdServicioSuscriptor.SelectedValue = value; }
        }

        public string IdSuscriptorASimular
        {
            get { return ddlIdSuscriptorASimular.SelectedValue; }
            set { ddlIdSuscriptorASimular.SelectedValue = value; }
        }

        public IEnumerable<ServiciosSimuladoDTO> ComboIdServicioSuscriptor
        {
            set
            {
                ddlIdServicioSuscriptor.DataSource = value;
                ddlIdServicioSuscriptor.DataBind();
            }
        }

        public DataTable ComboIdSuscriptorASimular
        {
            set
            {
                ddlIdSuscriptorASimular.DataSource = value;
                ddlIdSuscriptorASimular.DataBind();
            }
        }

		public string FechaInicio
		{
		    get { return txtFechaInicio.SelectedDate.ToString(); }
		    set { txtFechaInicio.DbSelectedDate = ExtensionesString.ConvertirFecha(value); }
		}
		public string FechaFin
		{
		    get { return txtFechaFin.SelectedDate.ToString(); }
		    set { txtFechaFin.DbSelectedDate = ExtensionesString.ConvertirFecha(value); }
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

        

        //public string NombreServicio
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
		#endregion "Propiedades de Presentación"


        
    }
}