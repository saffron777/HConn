using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using HConnexum.Tramitador.Negocio;
using System.Data;
using Telerik.Web.UI;
using System.Configuration;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
    public partial class SelectorCasos : UserControlListaBase, ISelectorCasos
    {
        #region MIEMBROS PRIVADOS
        SelectorCasosPresentador presentador;
        private bool _seleccionarSuscriptor = true;
        private RadGrid _gridAfectado;
        private const int ItemsPerRequest = 10;
        private IEnumerable<SuscriptorDTO> _ComboSuscriptor;
        private bool bvalidarRol;
        private RadGrid RadGridMaster;
        int IdIntermediario = 0;
        bool Inicio = false;
        #endregion
        public bool limpiar = false;
        public bool Buscar = false;
        #region EVENTOS DEL CONTROL
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                base.Page_Init(sender, e);
                presentador = new SelectorCasosPresentador(this);
                Inicio = true;
               
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Page_Load(sender, e);
                ddlSuscriptor.Filter = RadComboBoxFilter.StartsWith;
                if (!Page.IsPostBack)
                {
                    bool nubise = presentador.ComprobarNubise(UsuarioActual.Id);

                    this.presentador.LlenarSuscriptor(UsuarioActual.SuscriptorSeleccionado.Id);
                    ddlSuscriptor.Visible = true;
                    ddlSuscriptor.SelectedValue = UsuarioActual.SuscriptorSeleccionado.Id.ToString();

                    ddlServicio.ClearSelection();

                    this.presentador.LlenarComboServicios(int.Parse(ddlSuscriptor.SelectedValue));

                    if (SeleccionarSuscriptor && RolSimulador)
                    {
                        ddlSuscriptorASimular.Visible = true;
                        presentador.LlenarComboSuscriptorASimular(UsuarioActual.SuscriptorSeleccionado.Id);
                    }
                    else
                    {
                        // this.presentador.LlenarComboServicios(int.Parse(ddlSuscriptor.SelectedValue));
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

                //GridAfectado.PageIndexChanged += new GridPageChangedEventHandler(GridAfectado_PageIndexChanged);
                GridAfectado.PageSizeChanged += new GridPageSizeChangedEventHandler(GridAfectado_PageSizeChanged);
                GridAfectado.NeedDataSource += new GridNeedDataSourceEventHandler(GridAfectado_NeedDataSource);
               
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        //protected void GridAfectado_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        //{
        //    this.NumeroPagina = e.NewPageIndex + 1;
        //    this.GridAfectado.Rebind();
        //}

        protected void GridAfectado_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.NumeroPagina = 1;
            this.GridAfectado.Rebind();
        }
        void GridAfectado_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
           
                int numpagina = 0;
                int tamanoPagina = 0;
                if (Buscar)
                {
                    numpagina = 1;
                    this.NumeroPagina = 1;
                }
                else
                    numpagina = ValidarBusqueda(numpagina);
                tamanoPagina = ValidarTamanoPagina(tamanoPagina);
                Datos = null;
                string valor = ddlSuscriptorASimular.SelectedValue;

                this.presentador.MostrarVista(this.Orden, numpagina, tamanoPagina, this.ParametrosFiltro);
                Buscar = false;
                    
            
        }
        private int ValidarTamanoPagina(int TamanoPagina)
        {
            if (Session["TamanoPagina"] != null)
                TamanoPagina = Convert.ToInt32(Session["TamanoPagina"].ToString());
            else
                TamanoPagina = this.TamanoPagina;
            return TamanoPagina;
        }

        private int ValidarBusqueda(int numpagina)
        {
            if (Session["NumeroPagina"] != null)// && (Filtro == "" && FechaDesde == "" && FechaHasta == "" && IdComboServicios == "" && Asegurado == ""))
                numpagina = Convert.ToInt32(Session["NumeroPagina"].ToString());
            else
                numpagina = this.NumeroPagina;
            return numpagina;
        }

        protected void CmdBuscarClick(object sender, EventArgs e)
        {
            try
            {
                Buscar = true;

              
                this.NumeroPagina = 1;
                this.GridAfectado.Rebind();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
                Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
            }
        }

        public void RefrescarGridClick(object sender, EventArgs e)
        {
            this.GridAfectado.Rebind();
        }

        protected void DdlSuscriptorSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int res = 0;

                if (RolSimulador)
                {
                    if (int.TryParse(ddlSuscriptor.SelectedValue, out res))
                    {
                        this.presentador.LlenarComboServicios(res);

                    }
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


        protected void DdlSuscriptorSimuladoSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void DdlServicioSimuladoSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //presentador.LlenarComboSuscriptorASimular(ddlServicioSimulado.SelectedValue);
        }

        private static string GetStatusMessage(int offset, int total)
        {
            if (total <= 0)
                return "No Encontrado";

            return String.Format("Items <b>1</b>-<b>{0}</b> de <b>{1}</b>", offset, total);
        }

        protected void DdlSuscriptorSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
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
            ddlSuscriptor.SelectedValue = UsuarioActual.SuscriptorSeleccionado.Id.ToString();

            ddlServicio.ClearSelection();
            ddlServicio.Enabled = true;
            this.presentador.LlenarComboServicios(int.Parse(ddlSuscriptor.SelectedValue));
            
            ddlFiltro.ClearSelection();
            ddlEstatus.ClearSelection();
            ddlSuscriptorASimular.ClearSelection();
            txtAsegurado.Text = "";
            txtFechaDesde.Clear();
            txtFechaHasta.Clear();
            txtIntermediario.Text = "";
            txtFiltro.Text = "";
            txtFiltro.Enabled = false;
            Inicio = true;
            this.GridAfectado.Rebind();
        }
        #endregion

        #region "Propiedades de Presentación"
       
        public DataTable ComboSuscriptores 
        {
            set
            {
                this.ddlSuscriptor.DataSource = value;
                ddlSuscriptor.DataBind();
            }

        }
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
            set
            {
                bvalidarRol = value;
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

        public string NombreServicioSuscriptor
        {
            get { return ddlServicio.Text; }           
        }                                                        

        ///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IEnumerable<CasoDTO> Datos
        {
            
            set
            {
                
                this.GridAfectado.DataSource = value;
            }
        }

        ///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
        public int NumeroDeRegistros
        {
            get
            {
                return this.GridAfectado.VirtualItemCount;
            }
            set
            {
                this.GridAfectado.VirtualItemCount = value;
            }
        }

        public string NombreTabla
        {
            get;
            set;
        }

        public bool SeleccionarSuscriptor
        {
            get { return _seleccionarSuscriptor; }
            set { _seleccionarSuscriptor = value; }
        }

        public RadGrid GridAfectado
        {
            get { return _gridAfectado; }
            set { _gridAfectado = value; }
        }
       
        public IEnumerable<ServiciosSimuladoDTO> ComboServicioSuscriptor
        {
            set
            {
                ddlSuscriptorASimular.DataSource = value;
            }
        }

        public DataTable ComboSuscriptorASimular
        {
            set
            {
                this.ddlSuscriptorASimular.DataSource = value;
                this.ddlSuscriptorASimular.DataBind();
            }
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
            set 
            {
                IdIntermediario = value;
            }
        }
        public bool inicio
        {
            get { return Inicio; }
        }
       


        #endregion "Propiedades de Presentación"
     
    }
}