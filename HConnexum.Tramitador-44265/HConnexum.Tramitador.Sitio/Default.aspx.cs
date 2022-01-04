using System;
using System.Data;
using System.Text;
using System.Web;
using HConnexum.Infraestructura;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Collections.Generic;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Sitio
{
	public partial class Default : PaginaBase, IDefault
	{
		#region "Variables Locales"
		DefaultPresentador presentador;
        Label _Usuario;
        Label _Suscriptor;
        Label _Sucursal;

		#endregion "Variables Locales"

		#region Propiedades Publicas para Alertas
		/// <summary>
		/// Width of the Alert Window
		/// </summary>
		public Unit Width { get; set; }

		/// <summary>
		/// Title of the Alert Window
		/// </summary>
		public String Title { get; set; }
		#endregion

		#region "Eventos de la Página"
		protected void Page_Init(object sender, EventArgs e)
		{
                    
			base.Page_Init(sender, e);
            
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.presentador = new DefaultPresentador(this);
			this.presentador.MostrarVista(IdAplicacion);

            if (!IsPostBack)
                RadNotification1.ShowInterval = ((Session.Timeout - 1) * 60000) + 25000;

		}

		/// <summary>
		/// Evento que se dispara por un intervalo de tiempo para buscar en BD y mostrar un mensaje al usuario con la cantidad de alertas que tiene el buzon de alertas
		/// </summary>
		/// <param name="sender">Referencia al Objeto que disapara el evento</param>
		/// <param name="e">Argumentos del Evento</param>
		protected void OnCallbackUpdate(object sender, RadNotificationEventArgs e)
		{
		}
		protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(e.Argument))
				{
					int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(e.Argument)).Desencriptar());
					ArbolPagina padre = ArbolPaginas.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
					ElimitarRegistrosTomadosAnidado(padre);
					ArbolPaginas.Remove(padre);
				}
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
		public DataTable Menu
		{
			set
			{
				this.tbMenu.MenuDataSource = value;
			}
		}

        public string tbUsuario
        {
            get
            {
                return _Usuario.Text;
            }
            set {

                ((Label)((RadToolBar)((RadToolBar)tbMenu.Controls[0])).Controls[4].FindControl("lblUsuario")).Text = value;
            }
        }

        public string tbSuscritor
        {
            get
            {
                return _Suscriptor.Text;
            }
            set {

                ((Label)((RadToolBar)((RadToolBar)tbMenu.Controls[0])).Controls[4].FindControl("lblSuscriptor")).Text = value;
            }
        }
        public string tbSucursal
        {
            get
            {
                return _Sucursal.Text;
            }
            set
            {
                ((Label)((RadToolBar)((RadToolBar)tbMenu.Controls[0])).Controls[4].FindControl("lblSucursal")).Text = value;
            }
        }
       


		public string Alertas { get; set; }

		#endregion "Propiedades de Presentación"
	}
}
