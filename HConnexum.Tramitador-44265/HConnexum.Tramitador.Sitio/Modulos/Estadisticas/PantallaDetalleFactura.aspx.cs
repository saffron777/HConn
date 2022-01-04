using System;
using System.Linq;
using System.Threading;
using System.Web.UI;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Text;
using System.Web;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class PantallaDetalleFactura : PaginaMaestroDetalleBase, IPantallaDetalleFactura
	{

		#region "Variables Locales"
		PantallaDetalleFacturaPresentador presentador;
		#endregion "Variables Locales"

		#region "Propiedades de Presentación"

		public int? Nremesa
		{
			get
			{
				if (ViewState["NroReclamo"] != null)
					return Convert.ToInt32(ViewState["NroReclamo"]);
				return 0;
			}
		}

		public string Nfactura
		{
			set
			{
				if (ViewState["Nfactura"] != null)
				lblNfactura.Text = value;
			}
			get
			{
				if (ViewState["Nfactura"] != null)
					return ViewState["Nfactura"].ToString();
				return string.Empty;
			}
		}

		public string FechaRecepcion
		{
			set
			{
				lblFechaderecepciontxt.Text = value;
			}
			get
			{
				return lblFechaderecepciontxt.Text;
			}
		}

		public string FechaEmision
		{
			set
			{
				lblfechaemisiontxt.Text = value;
			}
			get
			{
				return lblfechaemisiontxt.Text;
			}
		}

		public string Estatus
		{
			set
			{
				lblEstatustxt.Text = value;
			}
			get
			{
				return lblEstatustxt.Text;
			}
		}

		public string Ncontrol
		{
			set
			{
				lblNcontroltxt.Text = value;
			}

			get
			{
				return lblNcontroltxt.Text;
			}
		}		

		public string MontoCubierto
		{
			set
			{
				lblmontocubiertotxt.Text = value;
			}
			get
			{
				return lblmontocubiertotxt.Text;
			}
		}

		public string TotalRetenido
		{
			set
			{
				lbltotalretenidotxt.Text = value;
			}
			get
			{
				return lbltotalretenidotxt.Text;
			}
		}

		public string TotalImp
		{
			set
			{
				lbltotalretenidotxt.Text = value;
			}
			get
			{
				return lbltotalretenidotxt.Text;
			}
		}

		public string MontoSujetoRetencion
		{
			set
			{
				lblmontosujetoretenciontxt.Text = value;
			}
			get
			{
				return lblmontosujetoretenciontxt.Text;
			}
		}

		public string MontoImpMunicipal
		{
			set
			{
				lblmontoimpmunicipaltxt.Text = value;
			}

			get
			{
				return lblmontoimpmunicipaltxt.Text;
			}
		}

		public string ImpIva
		{
			set
			{
				lbltotalIvatxt.Text = value;
			}

			get
			{
				return lbltotalIvatxt.Text;
			}
		}

		public string TotalImpIsrl
		{
			set
			{
				lblTotal_ImpISRLtxt.Text = value;
			}

			get
			{
				return lblTotal_ImpISRLtxt.Text;
			}
		}

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

		public int IdCodExternoUsuarioActual
		{
			get
			{
				if (ViewState["IdCodExternoUsuarioActual"] != null)
					return Convert.ToInt32(ViewState["IdCodExternoUsuarioActual"]);
				return 0;
			}
		}
		
		public string ConexionString
		{
			get
			{
				if(ViewState["ConexionString"] != null)
					return ViewState["ConexionString"].ToString();
				return string.Empty;
			}
		}

		public int IdCodExterno
		{
			get
			{
				if (ViewState["IdCodExterno"] != null)
					return Convert.ToInt32(ViewState["IdCodExterno"]);
				return 0;
			}
		}

		public int IdIntermediario
		{
			get
			{
				if (ViewState["IdIntermediario"] != null)
					return Convert.ToInt32(ViewState["IdIntermediario"]);
				return 0;
			}
		}

		public string IdInterEncriptado
		{
			get
			{
				if (ViewState[@"IdIntermediario"] != null)
					return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ViewState[@"IdIntermediario"].ToString().Encriptar()));
				return string.Empty;
			}
		}



		#endregion "Propiedades de Presentación"

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new PantallaDetalleFacturaPresentador(this);
				ViewState["IdIntermediario"] = this.Session["Tramita_idIntermediario"].ToString();
				ViewState["Nfactura"] = ExtraerDeViewStateOQueryString(@"NFactura");
				ViewState["ConexionString"] = presentador.ObtenerConexionString(IdIntermediario);
				ViewState["IdCodExterno"] = this.Session["Tramita_idCodExterno"].ToString();
				ViewState["IdCodExternoUsuarioActual"] = this.Session["Tramita_idProveedor"].ToString();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Page_Load(sender, e);
			if(!IsPostBack)
			{
				//var IdCodExternoUsuarioActual = ViewState["IdCodExternoUsuarioActual"];
				presentador.CargarDetalleFactura(IdCodExternoUsuarioActual, WebConfigurationManager.AppSettings[@"StoredProceduresDetalleFacturas"], Nfactura, ConexionString);
				RadGridMaster.Rebind();
			}
			this.RadFilterMaster.Culture = Thread.CurrentThread.CurrentCulture;
			this.RadFilterMaster.RecreateControl();
		}

		///<summary>Evento pre visualización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
			RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
			if(filtro != null)
				if(this.RadFilterMaster.RootGroup.Expressions.Count > 0)
					filtro.CssClass = @"rtbTextNeg";
		}

		///<summary>Evento del rad grid que se dispara para obtener el valor del objeto datakey del la fila selecionada.</summary>
		///<param name="dataKey">Referencia al nombre del Objeto DataKey.</param>
		///<returns>Valor de tipo string que contenido en el Objeto DataKey</returns>
		protected string ObtenerDataKeyValue(string dataKey)
		{
			string datakeyvalue = RadGridMaster.SelectedValues[dataKey].ToString();
			return datakeyvalue;
		}

		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
				var CodExternoUsuarioActual = ViewState["IdCodExternoUsuarioActual"];
				RadGridMaster.DataSource = presentador.CargarResumenCaso(Convert.ToInt32(CodExternoUsuarioActual), WebConfigurationManager.AppSettings[@"StoredProcedureListaReclamosFactura"], Nfactura, ConexionString, ParametrosFiltro, NumeroPagina, TamanoPagina);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				this.Page.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
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

		///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
		{
			NumeroPagina = e.NewPageIndex + 1;
			this.RadGridMaster.Rebind();
		}

		///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
		{
			if(e != null)
			{
				Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? " DESC" : " ASC");
				this.RadGridMaster.Rebind();
			}
		}

		///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		
		
		protected void LevantarDetalleMovimientoRemesa()
		{
			StringBuilder url = new StringBuilder();
			url.Append(@"~/Modulos/Estadisticas/PantallaDetalleMovimientoRemesa.aspx?IdMenu=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&intermediario=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdIntermediario.ToString().Encriptar())) + "&nremesa=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ObtenerDataKeyValue("NroReclamo").Encriptar())));
			Response.Redirect(url.ToString(), false);
		}

		protected void LevantarPantallaImpresionFactura()
		{
			StringBuilder url = new StringBuilder();
			url.Append(@"~/Modulos/Estadisticas/ReporteDetalleFactura.aspx?IdMenu=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&intermediario=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdIntermediario.ToString().Encriptar())) + "&nfactura=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(Nfactura.ToString().Encriptar())) + "&idcodexterusuact=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ViewState["IdCodExternoUsuarioActual"].ToString().Encriptar())) + "&idcodexterno=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ViewState["IdCodExterno"].ToString().Encriptar())));
			Response.Redirect(url.ToString());
		}


		
		protected void RadGridMaster_ItemCommand(object sender, GridCommandEventArgs e)
		{
			try
			{
				switch(e.CommandName)
				{
					case "Refrescar":
						this.RadGridMaster.Rebind();
						break;
					case "PostBack":
						if(RadGridMaster.SelectedItems.Count > 0)
							LevantarDetalleMovimientoRemesa();
						else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "radalert", "radalert('Seleccione el registro a mostrar', 380, 50, 'Ver detalle de Registro')", true);
						break;
					case "Imprimir":
						this.LevantarPantallaImpresionFactura();
						break;
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
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
	}
}