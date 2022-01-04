using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;
using System.Runtime.InteropServices;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
	///<summary>Clase ChadePasoLista.</summary>
	public partial class MisActividades : PaginaDetalleBase, IMisActividades
	{
		#region "Variables Locales"

		///<summary>Variable presentador MovimientoPresentadorDetalle.</summary>
		MisActividadesPresentador presentador;
		IList<MovimientoDTO> listaMovimientos;
		IList<FlujosServicioDTO> listaFlujos;
		MovimientoDTO movimientoNodo;
		int backTr;
		static bool indSimulado;
		string contadorActDisMasSimulados = "";
		static int? idmov = null;
		static int cantidad = 0;
		static int masdemill = 0;

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
				presentador = new MisActividadesPresentador(this);
			}
			catch(Exception ex)
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
                if (!IsPostBack)
                {
                    base.Page_Load(sender, e);
                    
                    int[] casospendientes = presentador.NumeroActividadesPendientes();
                    if (casospendientes != null)
                    {
                        cantidad = casospendientes[0];
                        masdemill = casospendientes[1];
                        idmov = casospendientes[2];
                    }
                    if (cantidad == 0)
                        btnActividadSiguiente.Enabled = false;
                    else
                        btnActividadSiguiente.Enabled = true;
                    RadGrid1.Rebind();
                    if (cantidad > 0)
                    {
                        btnActividadSiguiente.Text = string.Format(@"Pendientes {0}{1}", @"(" + cantidad + @")", masdemill == 0 ? string.Empty : @" *");
                        this.ClientScript.RegisterClientScriptBlock(typeof(string), @"JQ", @"window.onload = function () { setTimeout('RecargarPagina()', " + SegundoReload + @"); setInterval(blink, 10); }", true);
                    }
                    else
                    {
                        btnActividadSiguiente.Text = @"Pendientes";
                        this.ClientScript.RegisterClientScriptBlock(typeof(string), @"JQ", @"window.onload = function () { setTimeout('RecargarPagina()', " + SegundoReload + @"); }", true);
                    }
                   
                }
			}
			catch(Exception ex)
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
			
		}

       

		protected void btnActividadSiguiente_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Trace.Warn(@"Inicio BotonPendientes");
			HttpContext.Current.Trace.Warn(@"cantidad: [" + cantidad + @"]");
			HttpContext.Current.Trace.Warn(@"idmov: [" + idmov + @"]");
			HttpContext.Current.Trace.Warn(@"IdMetodoAtencion: [" + UsuarioActual.SuscriptorSeleccionado.IdMetodoAtencion.ToString() + @"]");
			HttpContext.Current.Trace.Warn(@"Fin BotonPendientes");
            if (this.UsuarioActual.SuscriptorSeleccionado.IdMetodoAtencion.ToString() == ConfigurationManager.AppSettings[@"PorPote"].ToString() || this.UsuarioActual.SuscriptorSeleccionado.IdMetodoAtencion.ToString() == ConfigurationManager.AppSettings[@"UnoAUno"].ToString())
            {
			    if(cantidad == 1 && idmov != 0) //si solo consigue un registro va por default a uno a uno
				    Response.Redirect("MisActividadesDetalle.aspx?IdMenu=" + IdMenuEncriptado + "&id=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(idmov.ToString().Encriptar())));
			    else if(cantidad > 1 && idmov != 0 && this.UsuarioActual.SuscriptorSeleccionado.IdMetodoAtencion.ToString() == ConfigurationManager.AppSettings[@"UnoAUno"].ToString())  //si es uno a uno
				    Response.Redirect("MisActividadesDetalle.aspx?IdMenu=" + IdMenuEncriptado + "&id=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(idmov.ToString().Encriptar())));
			    else if((cantidad > 1 && this.UsuarioActual.SuscriptorSeleccionado.IdMetodoAtencion.ToString() == ConfigurationManager.AppSettings[@"PorPote"].ToString()) || (cantidad > 1 && idmov == 0 && this.UsuarioActual.SuscriptorSeleccionado.IdMetodoAtencion.ToString() == ConfigurationManager.AppSettings[@"UnoAUno"].ToString()) || (cantidad == 1 && idmov == 0))  //Si es por pote
			    {
				    Response.Redirect("PoteCasos.aspx?IdMenu=" + IdMenuEncriptado);
			    }
			    else
			    {
				    RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				    if(windowManagerTemp != null)
					    windowManagerTemp.RadAlert(@"No existen actividades pendientes.", 380, 50, @"Actividades pendientes.", string.Empty);
			    }
            }else
            {
                RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
                if (windowManagerTemp != null)
                    windowManagerTemp.RadAlert(@"Este Usuario No tiene metodo de atención de Caso.", 380, 50, @"Actividades pendientes.", string.Empty);
            }
		}

		protected void btnNuevoCaso_Click(object sender, EventArgs e)
		{
			Response.Redirect(@"SolicitudServicio.aspx?IdMenu=" + IdMenuEncriptado, false);
		}
        
        public void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            presentador.MostrarVista();
        }
        public void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                DateTime FechaCreacion = ((HConnexum.Tramitador.Negocio.MovimientoDTO)(e.Item.DataItem)).FechaCreacion;
                int SLAToleranciaPaso = ((HConnexum.Tramitador.Negocio.MovimientoDTO)(e.Item.DataItem)).SLAToleranciaPaso;
                int slaMovimiento = int.Parse(System.Math.Truncate((DateTime.Now - FechaCreacion).TotalSeconds).ToString());
               

                if (slaMovimiento > SLAToleranciaPaso)
                {
                    item["SLAToleranciaPaso"].ForeColor = System.Drawing.Color.Red;
                   
                }
                else
                    item["SLAToleranciaPaso"].ForeColor = System.Drawing.Color.Green;

                string sTexto = string.Format("{0:N0}", System.Math.DivRem(slaMovimiento, 3600, out slaMovimiento)) + ":";
                sTexto += string.Format("{0:N0}", System.Math.DivRem(slaMovimiento, 60, out slaMovimiento)) + ":";
                sTexto += string.Format("{0:N0}", slaMovimiento);
                item["SLAToleranciaPaso"].Text = sTexto;               
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                //get the Hyperlink using the Column uniqueName
                HyperLink hyperLink = (HyperLink)dataItem["Movimiento"].Controls[0];

                hyperLink.NavigateUrl = "MisActividadesDetalle.aspx?IdMenu=" + IdMenuEncriptado + "&id=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(((HConnexum.Tramitador.Negocio.MovimientoDTO)(e.Item.DataItem)).Id.ToString().Encriptar()));
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
		public IList<MovimientoDTO> DatosMovimientos
		{
			set
			{
				listaMovimientos = value;
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
        public IEnumerable<MovimientoDTO> DatosGrid
        {
            set { this.RadGrid1.DataSource = value; }
        }
		public int SegundoReload
		{
			get
			{
				return int.Parse(WebConfigurationManager.AppSettings[@"SegundosRefrescarMisActividades"]) * 1000;
			}
		}

		public bool BIndSimulado
		{
			get
			{
				return indSimulado;
			}
			set
			{
				indSimulado = value;
			}
		}

		public string ContadorActDisMasSimulados
		{
			get
			{
				return contadorActDisMasSimulados;
			}
			set
			{
				contadorActDisMasSimulados = value;
			}
		}

		public string VsContadorActDisMasSimulados
		{
			get
			{
				if(ViewState["Valores"] != null)
					return ViewState["Valores"].ToString();
				else
					return "";
			}
			set
			{
				ViewState["Valores"] = value;
			}
		}

		public string getSessionSkin
		{
			get { return Session["SkinGlobal"].ToString(); }
		}

		#endregion "Propiedades de Presentación"

		#region ..:: [ MÉTODOS ] ::..

		public bool ValidarRol(string sRol)
		{
			bool bvalidarRol = false;
			int idAplicacion = int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
			foreach(HConnexum.Seguridad.RolesUsuario rol in UsuarioActual.AplicacionActual(idAplicacion).Roles)
				if(rol.NombreRol == sRol)
				{
					bvalidarRol = true;
					break;
				}
			return bvalidarRol;
		}
		#endregion
	}


	
}
