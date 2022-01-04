using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using Telerik.Web.UI;
using System.Web.Configuration;
using System.Web.UI;
using System.Threading;
using System.Data;
using System.Web;
using HConnexum.Seguridad;
using System.Text;

///<summary>Namespace que engloba la vista lista de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
    ///<summary>Clase CasoLista.</summary>
    public partial class GestionSupervisadosLista : PaginaListaBase, IGestionSupervisadosLista
    {
        ///<summary>Variable presentador CasoPresentadorLista.</summary>
        GestionSupervisadosPresentadorLista presentador;
        
        public DataTable ComboDropDown;
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
                presentador = new GestionSupervisadosPresentadorLista(this);
                Orden = "Id";
                IdUsuarioSuscriptor = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(UsuarioActual.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar()));
            }
            catch (Exception ex)
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
                ComboDropDown = presentador.LlenarCombosGrid();
               
                if (!Page.IsPostBack)
                {
                    presentador.CargarUsuarioLog();
                    presentador.LlenarCombos(UsuarioActual.IdUsuarioSuscriptorSeleccionado);
                    this.NumeroPagina = 1;
                    this.RadGridMaster.Rebind();
                    if (UsuarioActual.SuscriptorSeleccionado.Nombre != "NUBISE")
                    {
                        this.IdSuscriptor = UsuarioActual.SuscriptorSeleccionado.Id.ToString();
                        ddlSuscriptor.Enabled = false;
                        presentador.LlenarCombosServicios(int.Parse(this.ddlSuscriptor.SelectedValue.ToString()));
                    }
                    else
                    {
                        ddlAutonomia.Enabled = false;
                        ddlCargo.Enabled = false;
                        ddlHabilidad.Enabled = false;
                        ddlIdServicio.Enabled = false;
                    }
                #region Validacion de
                    Aplicacion aplicacion =new Aplicacion();
                    aplicacion = UsuarioActual.SuscriptorSeleccionado.Aplicaciones.Where(a => a.NombreAplicacion == "HC-Configurador").FirstOrDefault();
                    if (aplicacion.Id != null && aplicacion.Id!= 0)
                    {
                        ValidarUsuario();
                    }
                    else AccesoConf = false;
                #endregion
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
        /// metodo que emula la pantalla default con clic en la aplicacion configurador
        /// </summary>
        private void ValidarUsuario()
        {
            ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();
          
            DataSet dsd = servicio.ObtenerDatosUsuarioSuscriptor(UsuarioActual.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar());
            if (dsd.Tables[@"Error"] != null)
                throw new Exception(dsd.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
            UsuarioActual.ActualizarUsuario(dsd);
            Session[@"UsuarioActual"] = UsuarioActual;
            servicio.Close();
            presentador.CargarRoles(UsuarioActual.SuscriptorSeleccionado.Aplicaciones[0].Id);
            AccesoConf = true;
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
                switch (e.CommandName)
                {
                    case @"Refrescar":
                        this.RadGridMaster.Rebind();
                        break;
                   

                }
            }
            catch (Exception ex)
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

        protected void RadGridMaster_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string itemValue = item["EstatusUsuario"].Text;
                RadComboBox ddl = (RadComboBox)item["Temp"].FindControl("RadComboBox1");

                ddl.Attributes.Add("IdUsuario", item["IdUsuario"].Text);
                ddl.DataSource = ComboDropDown;
                ddl.DataTextField = "NombreValor";
                ddl.DataValueField = "Id";
                if (itemValue == "True")
                    ddl.SelectedValue = "24";
                else
                    ddl.SelectedValue = "25";
                ddl.DataBind();
            }
        }


        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                RadComboBox ddl2 = (RadComboBox)sender;
                bool Estatus;
                int idUsuario = int.Parse(ddl2.Attributes["IdUsuario"].ToString());
                int Status = int.Parse(ddl2.SelectedValue.ToString());
                if (Status == 24)
                    Estatus = true;
                else
                     Estatus = false;
                presentador.GuardarCambiosModificacion(idUsuario, Estatus, UsuarioActual.IdUsuarioSuscriptorSeleccionado);


            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
      
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.RadGridMaster.Rebind();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlIdServicio.ClearSelection();
            ddlUsuarioSupervisado.ClearSelection();
            ddlEstatusSupervisados.ClearSelection();
            ddlCargo.ClearSelection();
            ddlHabilidad.ClearSelection();
            ddlAutonomia.ClearSelection();
            RadGridMaster.Rebind();
        }

        protected void ddlSuscriptor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ddlAutonomia.Enabled = true;
            ddlCargo.Enabled = true;
            ddlHabilidad.Enabled = true;
            ddlIdServicio.Enabled = true;
            presentador.LlenarCombosServicios(int.Parse(this.ddlSuscriptor.SelectedValue.ToString()));
        }

         #endregion "Eventos de la Pagina"

        #region "Propiedades de Presentación"
        ///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IEnumerable<CasoDTO> Datos
        {
            set
            {
                this.RadGridMaster.DataSource = value;
            }
        }

        ///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
        public int NumeroDeRegistros
        {
            get
            {
                return this.RadGridMaster.VirtualItemCount;
            }
            set
            {
                this.RadGridMaster.VirtualItemCount = value;
            }
        }
        public string IdServicio
        {
            get
            {
                return ddlIdServicio.SelectedValue;
            }
            set
            {
                ddlIdServicio.SelectedValue = value;
            }
        }
        public DataTable ComboIdServicio
        {
            set
            {
                ddlIdServicio.DataSource = value;
                ddlIdServicio.DataBind();
            }

        }
        public string IdSuscriptor
        {
            get
            {
                return ddlSuscriptor.SelectedValue;
            }
            set
            {
                ddlSuscriptor.SelectedValue = value;
            }
        }

        public string UsuarioLog
        {
            set
            {
                lblUsuarioLog.Text = value;
            }
        }
        public DataTable ComboIdSuscriptor
        {
            set
            {
                ddlSuscriptor.DataSource = value;
                ddlSuscriptor.DataBind();
            }

        }
        public string IdUsuarioSupervisado
        {
            get
            {
                return ddlUsuarioSupervisado.SelectedValue;
            }
            set
            {
                ddlUsuarioSupervisado.SelectedValue = value;
            }
        }
        public DataTable ComboIdUsuarioSupervisado
        {
            set
            {
                ddlUsuarioSupervisado.DataSource = value;
                ddlUsuarioSupervisado.DataBind();
            }

        }
        public string IdEstatusSupervisados
        {
            get
            {
                return ddlEstatusSupervisados.SelectedValue;
            }
            set
            {
                ddlEstatusSupervisados.SelectedValue = value;
            }
        }
        public DataTable ComboIdEstatusSupervisados
        {
            set
            {
                ddlEstatusSupervisados.DataSource = value;
                ddlEstatusSupervisados.DataBind();
            }

        }
        public string IdCargo
        {
            get
            {
                return ddlCargo.SelectedValue;
            }
            set
            {
                ddlCargo.SelectedValue = value;
            }
        }
        public DataTable ComboIdCargo
        {
            set
            {
                ddlCargo.DataSource = value;
                ddlCargo.DataBind();
            }

        }
        public string IdHabilidad
        {
            get
            {
                return ddlHabilidad.SelectedValue;
            }
            set
            {
                ddlHabilidad.SelectedValue = value;
            }
        }
        public DataTable ComboHabilidad
        {
            set
            {
                ddlHabilidad.DataSource = value;
                ddlHabilidad.DataBind();
            }

        }
        public string IdAutonomia
        {
            get
            {
                return ddlAutonomia.SelectedValue;
            }
            set
            {
                ddlAutonomia.SelectedValue = value;
            }
        }
        public DataTable ComboAutonomia
        {
            set
            {
                ddlAutonomia.DataSource = value;
                ddlAutonomia.DataBind();
            }
        }

        public int[] IdSupervisado
        {
            get
            {
                int[] listIdSupervisado = (int[])ViewState["IdSupervisado"];
                return listIdSupervisado;
            }

            set
            {
                ViewState["IdSupervisado"] = value;
            }
        }
        public string IdservicioEnc 
        {
            get { return HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdServicio.Encriptar())); }
        }
        public string UrlConfigurador
        {
            get { return WebConfigurationManager.AppSettings[@"URLConfigurador"]; }
        }
        public string IdSuscriptorEnc
        {
            get { return HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdSuscriptor.Encriptar())); }
        }
        public bool AccesoConf{ get; set; }
        public static  string IdUsuarioSuscriptor {get;set;}
        #endregion "Propiedades de Presentación"

    }
}