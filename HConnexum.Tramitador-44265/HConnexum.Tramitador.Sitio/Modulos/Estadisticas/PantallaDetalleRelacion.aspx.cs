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
	public partial class PantallaDetalleRelacion : PaginaMaestroDetalleBase, IPantallaDetalleRelacion
	{

		#region "Variables Locales"
		PresentadorDetalleRelacion presentador;
		#endregion "Variables Locales"

		#region "Propiedades de Presentación"

		public string NRelacion
		{
			set
			{
				nrelacion.Text = value;
			}

			get
			{
				return nrelacion.Text;
			}
		}

		public string FechaCreacion
		{
			set
			{
				fechacreacion.Text = value;
			}

			get
			{
				return fechacreacion.Text;
			}
		}

		public string Banco
		{
			set
			{
				banco.Text = value;
			}

			get
			{
				return banco.Text;
			}
		}

		public string FechaPago
		{
			set
			{
				fechapago.Text = value;
			}

			get
			{
				return fechapago.Text;
			}
		}

		public string Status
		{
			set
			{
				status.Text = value;
			}

			get
			{
				return status.Text;
			}
		}

		public string Referencia
		{
			set
			{
				referencia.Text = value;
			}

			get
			{
				return referencia.Text;
			}
		}

		public string FormaPago
		{
			set
			{
				formapago.Text = value;
			}

			get
			{
				return formapago.Text;
			}
		}

		public string MontoCubierto
		{
			set
			{
				montocubierto.Text = value;
			}

			get
			{
				return montocubierto.Text;
			}
		}

		public string TotalRetenido
		{
			set
			{
				totalretenido.Text = value;
			}

			get
			{
				return totalretenido.Text;
			}
		}

		public string MontoSujetoRetencion
		{
			set
			{
				montosujetoretencion.Text = value;
			}

			get
			{
				return montosujetoretencion.Text;
			}
		}

		public string MontoImpMunicipal
		{
			set
			{
				montoimpmunicipal.Text = value;
			}

			get
			{
				return montoimpmunicipal.Text;
			}
		}

		public string TotalPagar
		{
			set
			{
				totalpagar.Text = value;
			}

			get
			{
				return totalpagar.Text;
			}
		}

		public string NumeroCasos
		{
			set
			{
				numerocasos.Text = value;
			}
			get
			{
				return numerocasos.Text;
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

		public int Nremesa
		{
			get
			{
				if(ViewState["NroRemesa"] != null)
					return Convert.ToInt32(ViewState["NroRemesa"]);
				return 0;
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
				if(ViewState["IdCodExterno"] != null)
					return Convert.ToInt32(ViewState["IdCodExterno"]);
				return 0;
			}
		}

		public int IdIntermediario
		{
			get
			{
				if(ViewState["IdIntermediario"] != null)
					return Convert.ToInt32(ViewState["IdIntermediario"]);
				return 0;
			}
		}

		public string IdInterEncriptado
		{
			get
			{
				if(ViewState[@"IdIntermediario"] != null)
					return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ViewState[@"IdIntermediario"].ToString().Encriptar()));
				return string.Empty;
			}
		}

		public bool Imprimir
		{
			set
			{
				ButtonImprimir.Enabled = value;
			}
		}

		#endregion "Propiedades de Presentación"

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new PresentadorDetalleRelacion(this);
				ViewState["IdIntermediario"] = ExtraerDeViewStateOQueryString(@"intermediario");
				ViewState["NroRemesa"] = ExtraerDeViewStateOQueryString(@"nremesa");
				ViewState["ConexionString"] = presentador.ObtenerConexionString(IdIntermediario);
                ViewState["IdCodExterno"] = ExtraerDeViewStateOQueryString(@"idcodexterno");
				ViewState["IdCodExternoUsuarioActual"] = ExtraerDeViewStateOQueryString(@"idcodexterusuact");
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
				presentador.CargarDetalleRelacion(IdCodExternoUsuarioActual/*Convert.ToInt32(IdCodExternoUsuarioActual)*/, WebConfigurationManager.AppSettings[@"StoreProcedureDetalleRelacion"], Nremesa, ConexionString);
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
                RadGridMaster.DataSource = presentador.CargarResumenCaso(Convert.ToInt32(CodExternoUsuarioActual), WebConfigurationManager.AppSettings[@"StoreProcedureResumenCaso"], Nremesa, ConexionString, ParametrosFiltro, NumeroPagina, TamanoPagina);
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
					case "PostBack":
						if(RadGridMaster.SelectedItems.Count > 0)
							LevantarDetalleMovimientoRemesa();
						else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "radalert", "radalert('Seleccione el registro a mostrar', 380, 50, 'Ver detalle de Registro')", true);
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

		protected void ButtonImprimir_Click(object sender, EventArgs e)
		{
			StringBuilder url = new StringBuilder();
			url.Append(@"~/Modulos/Estadisticas/ReporteDetalleRemesa.aspx?IdMenu=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&intermediario=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdIntermediario.ToString().Encriptar())) + "&nremesa=");
            url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(Nremesa.ToString().Encriptar())) + "&idcodexterusuact=");
            url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ViewState["IdCodExternoUsuarioActual"].ToString().Encriptar())) + "&idcodexterno=");
            url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(ViewState["IdCodExterno"].ToString().Encriptar())));
			Response.Redirect(url.ToString());
		}

	}
}