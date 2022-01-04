using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Infraestructura;
using System.Data;
using Telerik.Web.UI;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
	///<summary>Clase ChadePasoLista.</summary>
    public partial class ChadePasoDetalle : PaginaDetalleBase, IChadePasoDetalle
    {
        #region "Variables Locales"
		///<summary>Variable presentador ChadePasoPresentadorDetalle.</summary>
        ChadePasoPresentadorDetalle presentador;
        public string RutaPadreEncriptada;
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
	            presentador = new ChadePasoPresentadorDetalle(this);
				CultureNumericInput(RadInputManager1);
				CultureDatePicker();
			}
			catch (Exception ex)
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
	            {
                    RutaPadreEncriptada = ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect;
                    presentador.ServicioParametrizador(Accion);
	                if(Accion == AccionDetalle.Ver || Accion == AccionDetalle.Modificar)
	                    presentador.MostrarVista(Accion);
					if(this.Accion == AccionDetalle.Modificar)
                    	this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
	            }
			}
			catch (Exception ex)
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
            if(Accion == AccionDetalle.Ver) BloquearControles(true);
            MostrarBotones(cmdGuardar, cmdGuardaryAgregar, cmdLimpiar, Accion);
        }
		
		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
			try
			{
	            presentador.GuardarCambios(Accion);
                if (ArbolPaginas.ArbolPaginaActualIsNode())
                    this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
                else
                    this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "setTimeout('cerrarVentana()', 0);", true);
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
        }
		
		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar y agregar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        protected void cmdGuardaryAgregar_Click(object sender, EventArgs e)
        {
			try
			{
	            presentador.GuardarCambios(Accion);
				LimpiarControles();
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
        }
        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"
		public int Id
		{
		    get { return base.Id; }
		    set { base.Id = value; }
		}

        public string IdCargosuscriptor
        {
            get { return ddlIdCargosuscriptor.SelectedValue; }
            set { ddlIdCargosuscriptor.SelectedValue = value; }
        }
        public DataTable ComboIdCargosuscriptor
        {
            set
            {
                ddlIdCargosuscriptor.DataSource = value;
                ddlIdCargosuscriptor.DataBind();
            }
        }

        public string IdHabilidadSuscriptor
        {
            get { return ddlIdHabilidadSuscriptor.SelectedValue; }
            set { ddlIdHabilidadSuscriptor.SelectedValue = value; }
        }
        public DataTable ComboIdHabilidadSuscriptor
        {
            set
            {
                ddlIdHabilidadSuscriptor.DataSource = value;
                ddlIdHabilidadSuscriptor.DataBind();
            }
        }

        public string IdAutonomiaSuscriptor
        {
            get { return ddlIdAutonomiaSuscriptor.SelectedValue; }
            set { ddlIdAutonomiaSuscriptor.SelectedValue = value; }
        }
        public DataTable ComboIdAutonomiaSuscriptor
        {
            set
            {
                ddlIdAutonomiaSuscriptor.DataSource = value;
                ddlIdAutonomiaSuscriptor.DataBind();
            }
        }
		
		///<summary>Propiedad de auditoria que asigna u obtiene el indicador de eliminado.</summary>
		public string IndEliminado
		{
			get { return Auditoria.IndEliminado.ToString(); }
			set { Auditoria.IndEliminado = value; }
		}
		
		///<summary>Propiedad de auditoria que asigna u obtiene el usuario creador del regitros.</summary>
		public string CreadoPor
		{
			get { return Auditoria.CreadoPor; }
			set { Auditoria.CreadoPor = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de creación.</summary>
		public string FechaCreacion
		{
			get { return Auditoria.FechaCreacion; }
			set { Auditoria.FechaCreacion = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el usuario que modificó el regitros.</summary>
		public string ModificadoPor
		{
			get { return Auditoria.ModificadoPor; }
			set { Auditoria.ModificadoPor = value; }
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de modificación.</summary>
		public string FechaModificacion
		{
			get { return Auditoria.FechaModificacion; }
			set { Auditoria.FechaModificacion = value; }
		}

		///<summary>Propiedad de publicación que asigna u obtiene la fecha de validez.</summary>
		public string FechaValidez
		{
			get { return Publicacion.FechaValidez; }
			set { Publicacion.FechaValidez = value; }
		}

		///<summary>Propiedad de publicación que asigna u obtiene el indicador de vigencia.</summary>
		public string IndVigente
		{
			get { return Publicacion.IndVigente; }
			set { Publicacion.IndVigente = value; }
		}

		///<summary>Propiedad que asigna la cadena de errores devuelta desde el presenter.</summary>
        public string Errores
        {
            set
            {
                if(value.Length > 0)
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Errores", "alert('Se encontraron los siguientes errores: \\n\\n" + value + "')", true);
            }
        }

        ///<summary>Propiedad que asigna la cadena de errores o información personalizada devuelta desde el presenter.</summary>
        public string ErroresCustomEditar
        {
            set
            {
                RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                if (windowManagerTemp != null)
                    windowManagerTemp.RadAlert(value, 380, 50, "Mensaje", "IrAnterior");
            }
        }
        #endregion "Propiedades de Presentación"
    }
}