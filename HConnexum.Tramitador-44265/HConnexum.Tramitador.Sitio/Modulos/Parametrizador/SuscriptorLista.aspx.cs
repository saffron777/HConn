using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using Telerik.Web.UI;
using System.Web.Configuration;
using System.Web.UI;
///<summary>Namespace que engloba la vista lista de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
	///<summary>Clase SuscriptorLista.</summary>
    public partial class SuscriptorLista : PaginaListaBase, ISuscriptorLista
    {
		///<summary>Variable presentador SuscriptorPresentadorLista.</summary>
        SuscriptorPresentadorLista presentador;

        #region "Eventos de la Pagina"
		
		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
        protected void Page_Init(object sender, EventArgs e)
        {
		 	try
            {
				base.Page_Init(sender, e);
	            presentador = new SuscriptorPresentadorLista(this);
	            Orden = "Id";
			}
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
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
				Master.FindControl("rsmMigas").Visible=false;
	            if(!IsPostBack)
	            {
	                NumeroPagina = 1;
					this.RadGridMaster.Rebind();
	            }
				RadFilterMaster.Culture = new System.Globalization.CultureInfo("es-VE");
				RadFilterMaster.RecreateControl();
			}
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
		
		///<summary>Evento pre visualización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_PreRender(object sender, EventArgs e)
       	{
        	base.Page_PreRender(sender, e);
            RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
            if (filtro != null)
                if (this.RadFilterMaster.RootGroup.Expressions.Count > 0)
                    filtro.CssClass = @"rtbTextNeg";

       	}

		///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
	    protected void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            NumeroPagina=e.NewPageIndex + 1;
          	this.RadGridMaster.Rebind();
        }
		
		///<summary>Evento libreria ajax que se dispara cuando se hace peticiones en la página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
			try
            {
	           	if(!string.IsNullOrEmpty(e.Argument))
	            	this.presentador.EliminarRegistroTomadoAjax(e.Argument);
				this.RadGridMaster.Rebind();
			}
            catch (Exception ex)
            {
                   HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                   Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                   this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
		
		///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if(e != null)
            {
                Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending? " DESC" : " ASC");
               this.RadGridMaster.Rebind();
            }
        }
		
		///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
           	NumeroPagina = 1; 
            TamanoPagina = e.NewPageSize;
            this.RadGridMaster.Rebind();
        }

		///<summary>Evento del rad filter del grid que se dispara cuando se agrega o modifica una busqueda.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected void RadFilterMaster_ItemCommand(object sender, RadFilterCommandEventArgs e)
        {
            if(e != null)
            {
                switch(e.CommandName)
                {
                    case "RemoveExpression":
                        break;
                    case "AddGroup":
                        this.LblMessege.Text = "La Opcion de Agregar Grupo no esta Disponible y no ejecuta ninguna acción";
                        break;
                    case "AddExpression":
                        this.LblMessege.Text = string.Empty;
                        break;
                }
            }
        }
		
		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
            	presentador.MostrarVista(Orden, NumeroPagina, TamanoPagina, ParametrosFiltro);
			}
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
		}

		///<summary>Evento del rad filter del grid que se dispara cuando se hace click en aceptar en los filtros.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        protected void ApplyButton_Click(object sender, ImageClickEventArgs e)
        {
			try
			{
	            NumeroPagina = 1;
	            ParametrosFiltro = ExtraerParametrosFiltro(RadFilterMaster);
				this.RadGridMaster.Rebind();
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
        }

		///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_ItemCommand(object sender, GridCommandEventArgs e)
        {
			try
			{
	            switch(e.CommandName)
	            {
	                case "Refrescar":
	                    this.RadGridMaster.Rebind();
	                    break;
                    
	            }
			}
			catch (Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
        }
		
        ///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
        protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
        {

        }

        #endregion "Eventos de la Pagina"

        #region "Propiedades de Presentación"
		
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IEnumerable<SuscriptorDTO> Datos
        {
            set
            {
                 RadGridMaster.DataSource = value;
            }
        }
		
		///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
        public int NumeroDeRegistros
        {
            get
            {
                return RadGridMaster.VirtualItemCount;
            }
            set
            {
                RadGridMaster.VirtualItemCount = value;
            }
        }

        #endregion "Propiedades de Presentación"      
    }
}