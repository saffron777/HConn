using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
	///<summary>Clase ConsultaCasos.</summary>
	public partial class ConsultaCasos : PaginaMaestroDetalleBase, IConsultaCasos
	{
		#region "Variables Locales"
		///<summary>Variable presentador ConsultaCasosPresentador.</summary>
		ConsultaCasosPresentador presentador;
        public bool Inicio = false;
        private bool bvalidarRol;
        private bool _seleccionarSuscriptor = true;
        int IdIntermediario = 0;
        public bool busca = false;
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
				this.presentador = new ConsultaCasosPresentador(this);
				this.Orden = "Id";
                Inicio = true;
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
                ddlSuscriptor.Filter = RadComboBoxFilter.StartsWith;
                if (!Page.IsPostBack)
                {
                    this.CultureDatePicker();
                    this.RadGridMaster.Skin = Session[@"SkinGlobal"].ToString();
                    bool nubise = presentador.ComprobarNubise(UsuarioActual.Id);
                    if (RolSimulador)
                    {
                        ddlSuscriptorASimular.Visible = true;
                        lblSuscriptorSimu.Visible = true;
                        presentador.LlenarComboSuscriptorASimular(UsuarioActual.SuscriptorSeleccionado.Id);
                        this.presentador.LlenarSuscriptor(UsuarioActual.SuscriptorSeleccionado.Id);
                        
                        ddlSuscriptor.Visible = true;
                        ddlServicio.ClearSelection();
                    }
                    else
                    {
                        this.presentador.LlenarSuscriptor(UsuarioActual.SuscriptorSeleccionado.Id);
                        ddlSuscriptor.Visible = true;                       
                        ddlServicio.ClearSelection();                    
                        this.presentador.ComprobarSupervisor(UsuarioActual.Id, UsuarioActual.IdUsuarioSuscriptorSeleccionado);
                    }
                    Session["NumeroPagina"] = null;
                    this.presentador.LlenarComboEstatus();
                    this.txtFiltro.Enabled = false;
                }
                else
                {
                    Inicio = false;
                }
                this.NumeroPagina = 1;
                Master.FindControl("rsmMigas").Visible = false;
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
			RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
		}

        ///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.NumeroPagina = e.NewPageIndex + 1;
            this.RadGridMaster.Rebind();
            if (RadGridMaster.Items.Count.CompareTo(6) > 0)
            {
                RadGridMaster.MasterTableView.Style.Remove("height");
                RadGridMaster.MasterTableView.Style.Add("height", "100%");
            }
            else
            {
                RadGridMaster.MasterTableView.Style.Remove("height");
            }
        }

        ///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (e != null)
            {
                this.Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? @" DESC" : @" ASC");
                this.RadGridMaster.Rebind();
            }
        }

        ///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.NumeroPagina = 1;
            this.TamanoPagina = e.NewPageSize;
            this.RadGridMaster.Rebind();
            if (RadGridMaster.Items.Count.CompareTo(6) > 0)
            {
                RadGridMaster.MasterTableView.Style.Remove("height");
                RadGridMaster.MasterTableView.Style.Add("height", "100%");
            }
            else
            {
                RadGridMaster.MasterTableView.Style.Remove("height");
            }
        }

        ///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
        ///<param name="sender">Referencia al objeto que provocó el evento.</param>
        ///<param name="e">Argumentos del evento.</param>
        protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro);
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
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
					case @"Refrescar":
						this.RadGridMaster.Rebind();
						break;
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{
		}

		///<summary>Evento de comando que se dispara cuando se hace selecciona un elemento del combo ddlSuscriptor.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>

        protected void CmdBuscarClick(object sender, EventArgs e)
        {
            try
            {
                this.NumeroPagina = 1;
                RadGridMaster.CurrentPageIndex = 0;
                this.RadGridMaster.Rebind();
                if (RadGridMaster.Items.Count.CompareTo(6) > 0)
                {
                    RadGridMaster.MasterTableView.Style.Remove("height");
                    RadGridMaster.MasterTableView.Style.Add("height", "100%");
                }
                else 
                {
                    RadGridMaster.MasterTableView.Style.Remove("height");
                }
                
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        /// <summary>
        /// Metodo NOMBRADO PERO NO UTILIZADO 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DdlSuscriptorSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int res = 0;
                if (RolSimulador)
                {
                    if (int.TryParse(ddlSuscriptor.SelectedValue, out res))
                        this.presentador.LlenarComboServicios(res);
                    else
                        this.presentador.LlenarComboServicioSimulado();
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        protected void DdlFiltroSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.NumeroPagina = 1;
            this.txtFiltro.Text = "";
            this.txtFiltro.Enabled = true;
        }

        private static string GetStatusMessage(int offset, int total)
        {
            if (total <= 0)
                return "No Encontrado";

            return String.Format("Items <b>1</b>-<b>{0}</b> de <b>{1}</b>", offset, total);
        }
        /// <summary>
        /// Metodo que se ejecuta al cambiar la seleccion del combo de suscriptor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DdlSuscriptorSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            ddlServicio.ClearSelection();
            ddlServicio.Items.Clear();
            ddlServicio.Enabled = true;
            if(!string.IsNullOrEmpty(ddlSuscriptor.SelectedValue))
            this.presentador.LlenarComboServicios(int.Parse(ddlSuscriptor.SelectedValue));
        }

        protected void DdlSuscriptorSimSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlSuscriptor.ClearSelection();
            ddlSuscriptor.Items.Clear();
            ddlServicio.ClearSelection();
            ddlServicio.Items.Clear();
            ddlServicio.ClearSelection();
          
            this.presentador.LlenarSuscriptor(int.Parse(ddlSuscriptorASimular.SelectedValue));
            if (!(SeleccionarSuscriptor && RolSimulador))
            {
                ddlServicio.ClearSelection();
                ddlServicio.Enabled = true;
                this.presentador.LlenarComboServicios(int.Parse(ddlSuscriptor.SelectedValue));
            }
        }

        protected void cmdLimpiar_Click(object sender, EventArgs e)
        {
            ddlSuscriptor.ClearSelection();
            this.presentador.LlenarSuscriptor(UsuarioActual.SuscriptorSeleccionado.Id);
            ddlSuscriptor.Visible = true;
            //ddlSuscriptor.SelectedValue = UsuarioActual.SuscriptorSeleccionado.Id.ToString();
            ddlServicio.ClearSelection();
            ddlServicio.Enabled = true;
            ddlFiltro.ClearSelection();
            ddlEstatus.ClearSelection();
            ddlSuscriptorASimular.ClearSelection();
            txtAsegurado.Text = "";
            txtFechaDesde.Clear();
            txtFechaHasta.Clear();
            txtIntermediario.Text = "";
            txtFiltro.Text = "";
            txtFiltro.Enabled = false;
            RadGridMaster.DataSource = new string[] { };
            NumeroDeRegistros = 0;
            NumeroPagina = 1;
            RadGridMaster.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem item in RadGridMaster.Items)
                {
                    if (item.Selected)
                        presentador.actualizarBuzonChatHC1(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id"].ToString());
                }
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        #endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        ///
        public bool RolSimulador
        {
            get
            {
                int idAplicacion = int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
                foreach (HConnexum.Seguridad.RolesUsuario Rol in UsuarioActual.AplicacionActual(idAplicacion).Roles)
                    if (Rol.NombreRol == ConfigurationManager.AppSettings[@"RolSimulaProveedor"].ToString())
                    {
                        bvalidarRol = true;
                        break;
                    }
                return bvalidarRol;
            }
            set { bvalidarRol = value;}
        }

        public bool SeleccionarSuscriptor
        {
            get { return _seleccionarSuscriptor; }
            set { _seleccionarSuscriptor = value; }
        }
		public IEnumerable<CasoDTO> Datos
		{
			set { this.RadGridMaster.DataSource = value; }
		}

		///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
		public int NumeroDeRegistros
		{
			get { return this.RadGridMaster.VirtualItemCount; }
			set { this.RadGridMaster.VirtualItemCount = value; }
		}

		public string Suscriptor
		{
			set { throw new NotImplementedException(); }
		}

		public string TipoDoc
		{
			set { throw new NotImplementedException(); }
		}

		public string DocSolicitante
		{
			set { throw new NotImplementedException(); }
		}

		public DataTable ComboSuscriptores 
        {
            set
            {
                this.ddlSuscriptor.DataSource = value;
                ddlSuscriptor.DataBind();
                ddlSuscriptor.SelectedIndex = 0;
                presentador.LlenarComboServicios(int.Parse(ddlSuscriptor.SelectedValue));
            }
		}

	    public IEnumerable<FlujosServicioDTO> ComboServicios
        {
            set
            {
                ddlServicio.DataSource = value;
                ddlServicio.DataBind();
            }
        }

        public DataTable ComboEstatus
        {
            set
            {
                ddlEstatus.DataSource = value;
                ddlEstatus.DataBind();
            }
        }

        public string IdComboSuscriptores
        {
            get
            {
                if (ddlSuscriptor.Visible == false)
                    return UsuarioActual.SuscriptorSeleccionado.Id.ToString();
                else
                    return ddlSuscriptor.SelectedValue.ToString();
            }
        }

        public string IdComboSuscriptorASimular
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlSuscriptorASimular.SelectedValue))
                    return ddlSuscriptorASimular.SelectedValue.ToString();
                else return "";
            }
        }
		
        public string IdComboServicios
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlServicio.SelectedValue))
                    return ddlServicio.SelectedValue.ToString();
                else return "";
            }
        }

		public string IdServicioSuscriptor
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlSuscriptorASimular.SelectedValue))
                    return ddlSuscriptorASimular.SelectedValue.ToString();
                else return "";
            }
        }

        public string TipoFiltro
        {
            get
            {
                if (!string.IsNullOrEmpty(ddlFiltro.SelectedValue))
                    return ddlFiltro.SelectedValue.ToString();
                else return "";

            }
        }
        public string Filtro
        {
            get { return txtFiltro.Text; }
        }

        public string FechaDesde
        {
            get { return txtFechaDesde.SelectedDate.ToString(); }
        }

        public string FechaHasta
        {
            get { return txtFechaHasta.SelectedDate.ToString(); }
        }

        public string IdComboEstatus
        {
            get { return ddlEstatus.SelectedValue.ToString(); }
        }

		public string urlCartaAval
		{
			get { return presentador.BuscaUrlCasosExternos("CA_HC1"); }
		}

        public string NombreServicioSuscriptor
        {
            get { return ddlServicio.Text; }
        }                                                        

		public string urlClave
		{
			get { return presentador.BuscaUrlCasosExternos("CE_HC1"); }
        }

        public bool inicio
        {
            get { return Inicio; }
        }

        public string Asegurado
        {
            get { return txtAsegurado.Text; }
        }

        public string Intermediario
        {
            get { return txtIntermediario.Text; }
        }

        public int idIntermediario
        {
            get
            {
                if (!string.IsNullOrEmpty(Intermediario))
                    this.presentador.ObtenerIdSuscriptor(Intermediario);
                return IdIntermediario;
            }
            set { IdIntermediario = value; }
        }

        public DataTable ComboSuscriptorASimular
        {
            set
            {
                this.ddlSuscriptorASimular.DataSource = value;
                this.ddlSuscriptorASimular.DataBind();
            }
        }

        public string ServicioCartaAval
        {
            get
            {
                return this.presentador.BuscarValorServicio(int.Parse(ConfigurationManager.AppSettings[@"ListaValorServicioCartaAval"].ToString()));
            }
        }

        public string ServicioClaveEmergencia
        {
            get
            {
                return this.presentador.BuscarValorServicio(int.Parse(ConfigurationManager.AppSettings[@"ListaValorServicioClaveEmergencia"].ToString()));
            }
        }
		
        #endregion "Propiedades de Presentación"

    }
}
