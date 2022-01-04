using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Linq;
using Telerik.Web.UI;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
    ///<summary>Clase ChadePasoLista.</summary>
    public partial class MisSolicitudes : PaginaDetalleBase, IMisSolicitudes
    {
        #region "Variables Locales"
        ///<summary>Variable presentador CasoPresentadorDetalle.</summary>
        MisSolicitudesPresentador presentador;
        IList<CasoDTO> listaCasos;
        IList<FlujosServicioDTO> listaFlujos;
        CasoDTO casoNodo;
        int backTr;
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
                presentador = new MisSolicitudesPresentador(this);
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
                if (!Page.IsPostBack)
                {
                    backTr = 0;
                    presentador.MostrarVista();
                    foreach (FlujosServicioDTO flujo in listaFlujos)
                    {
                        RadTreeNode flujoNodo;
                        if (CuentaCasos(flujo) > 0)
                        {
                            flujoNodo = new RadTreeNode(flujo.NombreServicioSuscriptor + " (" + CuentaCasos(flujo).ToString() + ")", "flujo");
                            var casoTemplate = new CasoTemplate(casoNodo, backTr, IdMenuEncriptado, true);
                            RadTreeNode headerNodo = new RadTreeNode();
                            headerNodo.NodeTemplate = casoTemplate;
                            flujoNodo.Nodes.Add(headerNodo);
                        }
                        else
                            flujoNodo = new RadTreeNode(flujo.NombreServicioSuscriptor + " No Tiene Solicitudes Pendientes", "flujo");

                        foreach (CasoDTO caso in listaCasos)
                        {
                            if (caso.IdServiciosuscriptor == flujo.IdServicioSuscriptor)
                                flujoNodo.Nodes.Add(new RadTreeNode(caso.Id.ToString(),"caso"));
                        }

                        RadTreeView1.Nodes.Add(flujoNodo);
                    }

                    if (listaFlujos.Count < 1)
                    {
                        RadTreeNode flujoNodo = new RadTreeNode(NombreSuscriptor + " No Tiene Solicitudes Pendientes", "flujo");
                        RadTreeView1.Nodes.Add(flujoNodo);
                    }
                }

                RadTreeView1.DataBind();
            }
            catch (Exception ex)
            {
                HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        /// <summary>
        /// Retorna la cantidad de casos para un flujo en listaCasos
        /// </summary>
        /// <param name="flujo">Flujo de Servicio</param>
        /// <returns>Cantidad de casos para un flujo en listaCasos</returns>
        protected int CuentaCasos(FlujosServicioDTO flujo)
        {
            int total = 0;
            foreach (CasoDTO caso in listaCasos)
                if (caso.IdServiciosuscriptor == flujo.IdServicioSuscriptor)
                    total++;
            return total;
        }

        ///<summary>Evento pre visualización de la página.</summary>
        ///<param name="sender">Instancia de la página.</param>
        ///<param name="e">Instancia con parámetros de argumento.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            base.Page_PreRender(sender, e);
            RadTreeView1.ExpandAllNodes();
            RadTreeView1.ShowLineImages = false;
        }

        protected void RadTreeView1_TemplateNeeded(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
        {
            string value = e.Node.Value;
            if (value == "caso")
            {
                int text = int.Parse(e.Node.Text);
                foreach (CasoDTO caso in listaCasos)
                    if (caso.Id == text)
                        casoNodo = caso;
                backTr++;
                var casoTemplate = new CasoTemplate(casoNodo, backTr, IdMenuEncriptado, false);
                e.Node.NodeTemplate = casoTemplate;
            }
        }

        #endregion "Eventos de la Página"

        #region "Propiedades de Presentación"
        ///<summary>Propiedad que asigna u obtiene el Id.</summary>
        public int Id
        {
            get
            {
                return UsuarioActual.IdUsuarioSuscriptorSeleccionado;
            }
            set
            {
                base.Id = value;
            }
        }
        
        ///<summary>Propiedad que asigna u obtiene el Nombre del Usuario.</summary>
        public string NombreUsuario
        {
            get
            {
                return lbUsuario.Text;
            }
            set
            {
                lbUsuario.Text = value;
            }
        }

        ///<summary>Propiedad que asigna u obtiene el Nombre del Suscriptor.</summary>
        public string NombreSuscriptor
        {
            get
            {
                return lbSuscriptor.Text;
            }
            set
            {
                lbSuscriptor.Text = value;
            }
        }

        ///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IList<CasoDTO> DatosCasos
        {
            set
            {
                listaCasos = value;
            }
        }

        ///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
        public IList<FlujosServicioDTO> DatosFlujos
        {
            set
            {
                listaFlujos = value;
            }
        }
        #endregion "Propiedades de Presentación"

        protected void btnNuevoCaso_Click(object sender, EventArgs e)
        {

        }
    }


    class CasoTemplate : ITemplate
    {
        bool header;
        CasoDTO casoNodo;
        int backTr;
        string IdMenuEncriptado;

        public CasoTemplate(CasoDTO p_casoNodo, int p_backTr, string p_idMenuEncriptado, bool p_header)
        {
            header = p_header;
            casoNodo = p_casoNodo;
            backTr = p_backTr;
            IdMenuEncriptado = p_idMenuEncriptado;
        }
        
        public void InstantiateIn(Control container)
        {
            Table table = new Table();
            table.Width = 680;
            table.CellSpacing = 0;
            table.CellPadding = 0;
            table.HorizontalAlign = HorizontalAlign.Left;
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            HyperLink Id = new HyperLink();

            if (header || casoNodo == null)
            {
                table.BorderWidth = 1;
                cell.Text = "Caso";
                cell.Font.Bold = true;
                cell.Width = 50;
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = "Suscriptor";
                cell.Width = 220;
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = "Estatus";
                cell.Width = 130;
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = "Fecha de Solicitud";
                cell.Width = 150;
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = "Tiempo Transcurrido";
                cell.Width = 130;
                cell.Font.Bold = true;
                row.Cells.Add(cell);

                table.Rows.Add(row);
                container.Controls.Add(table);
            }
            else
            {
                Id.Text = casoNodo.Id.ToString();
                Id.NavigateUrl = "../Tracking/CasoDetalle.aspx?IdMenu=" + IdMenuEncriptado + "&id=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(casoNodo.Id.ToString().Encriptar()));
                cell.Controls.Add(Id);
                cell.Width = 50;
                cell.HorizontalAlign = HorizontalAlign.Center;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = casoNodo.NombreSuscriptor;
                cell.Width = 220;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = casoNodo.Status;
                cell.Width = 130;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = casoNodo.FechaCreacion.ToString();
                cell.Width = 150;
                row.Cells.Add(cell);
                cell = new TableCell();
                int SLACaso = int.Parse(System.Math.Truncate((DateTime.Now - casoNodo.FechaCreacion).TotalSeconds).ToString());
                if (SLACaso > casoNodo.SLAToleranciaFlujoServicio)
                    cell.ForeColor = System.Drawing.Color.Red;
                else
                    cell.ForeColor = System.Drawing.Color.Green;
                cell.Text = string.Format("{0:N0}", System.Math.DivRem(SLACaso, 3600, out SLACaso)) + ":";
                cell.Text += string.Format("{0:N0}", System.Math.DivRem(SLACaso, 60, out SLACaso)) + ":";
                cell.Text += string.Format("{0:N0}", SLACaso);
                cell.Width = 130;
                row.Cells.Add(cell);
                if (backTr % 2 == 0)
                    row.BackColor = System.Drawing.Color.Azure;
                else
                    row.BackColor = System.Drawing.Color.WhiteSmoke;

                table.Rows.Add(row);
                container.Controls.Add(table);
            }
        }
    }


}