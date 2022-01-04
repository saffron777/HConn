using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Data;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
	///<summary>Clase FlujosServicioMaestroDetalle.</summary>
	public partial class FlujosServicioMaestroDetalle : PaginaMaestroDetalleBase, IFlujosServicioMaestroDetalle
	{
		#region "Variables Locales"
		///<summary>Variable presentador FlujosServicioPresentadorMaestroDetalle.</summary>
		FlujosServicioPresentadorMaestroDetalle presentador;
		int casos = 0;
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
				presentador = new FlujosServicioPresentadorMaestroDetalle(this);
				Orden = "Id";
				CultureNumericInput(RadInputManager1);
				CultureDatePicker();
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
				base.Page_Load(sender, e);
				if(!Page.IsPostBack)
				{
					ViewState["PreviousPage"] = Request.UrlReferrer;
					ddlIdServicioSuscriptor.Enabled = false;
					ddlIdPasoInicial.Visible = false;
					LblPasoInicial.Visible = false;
					AccionC.Value = Accion.ToString();
					presentador.LLenarCombo2();
					if(Accion == AccionDetalle.Ver || Accion == AccionDetalle.Modificar)
					{
						ddlIdPasoInicial.Enabled = true;
						ddlIdPasoInicial.Visible = true;
						LblPasoInicial.Visible = true;
						btnSuscriptor.Visible = false;
						btnMenos.Visible = false;
						btnConfiguracionPaso.Visible = true;
						this.cmdAccionesPaso.Visible = true;
						this.cmdOperacionalizar.Visible = true;
						this.btnConfiguracionPaso.Visible = true;
						btnDocumentosServicio.Visible = true;
						cmdSolicitud.Visible = true;
						accionEncriptada.Value = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(Accion.ToString().Encriptar()));
					}
					else if(Accion == AccionDetalle.Agregar)
					{
						ddlIdPasoInicial.Enabled = false;
						RadGridMaster.Visible = false;
						btnConfiguracionPaso.Visible = false;
						btnDocumentosServicio.Visible = false;
						cmdSolicitud.Visible = false;
					}
					if(this.Accion == AccionDetalle.Modificar)
					{
						ddlIdPasoInicial.Enabled = true;
						presentador.LLenarCombo(Id);
						btnSuscriptor.Visible = false;
						btnMenos.Visible = false;
						btnConfiguracionPaso.Visible = true;
						this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
					}
				}
				RadFilterMaster.Culture = new System.Globalization.CultureInfo("es-VE");
				RadFilterMaster.RecreateControl();
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
			RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
			if(filtro != null)
				if(this.RadFilterMaster.RootGroup.Expressions.Count > 0)
					filtro.CssClass = @"rtbTextNeg";
			if(casos > 0)
				Accion = AccionDetalle.Ver;
			if(Accion == AccionDetalle.Ver)
				BloquearControles(true);
			if(Accion == AccionDetalle.Agregar)
			{
				Publicacion.Visible = false;
				Auditoria.Visible = false;
			}
			MostrarBotones(cmdGuardar, cmdGuardaryAgregar, cmdLimpiar, Accion);
			if(!this.presentador.bObtenerRolIndEliminado())
			{
				GridColumn columna = RadGridMaster.MasterTableView.GetColumnSafe("IndEliminado");
				if(columna != null) columna.Visible = false;
			}
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				if(presentador.GuardarCambios(Accion, true))
					if(ArbolPaginas.ArbolPaginaActualIsNode())
						this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
					else
						this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "setTimeout('cerrarVentana()', 0);", true);
			}
			catch(Exception ex)
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
				if(presentador.GuardarCambios(Accion, true))
					LimpiarControles();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Evento libreria ajax que se dispara cuando se hace peticiones en la página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
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
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
				presentador.MostrarVista(Orden, NumeroPagina, TamanoPagina, ParametrosFiltro, Accion);
			}
			catch(Exception ex)
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
					case "Eliminar":
						if(((CheckBox)e.Item.OwnerTableView.Items[RadGridMaster.SelectedIndexes[0]]["IndEliminado"].Controls[0]).Checked == false)
						{
							IList<string> Eliminadas = new List<string>();
							foreach(GridDataItem item in RadGridMaster.Items)
								if(item.Selected) Eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdEncriptado"].ToString());
							presentador.Eliminar(Eliminadas);
							this.RadGridMaster.Rebind();
						}
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

		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{

		}

		/// <summary>
		/// Evento para habilitar el ddl servicios suscriptor
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Button1_Click(object sender, EventArgs e)
		{
			this.ddlIdServicioSuscriptor.ClearSelection();
			this.ddlIdServicioSuscriptor.Items.Clear();
			presentador.ObtenerServicios(TxtHiddenId.Value, TxtHiddenTipo.Value);
			ddlIdServicioSuscriptor.Enabled = true;
		}

		public void ConfirmOK(object sender, EventArgs e)
		{
			IndVigente = "true";
			if(presentador.GuardarCambios(Accion, false))
				if(ViewState["PreviousPage"] != null)
					this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
		}

		protected void btnActivarEliminado_Click(object sender, EventArgs e)
		{
			string indice = null;
			IList<string> regEliminado = new List<string>();
			foreach(GridDataItem item in this.RadGridMaster.Items)
			{
				if(item.Selected)
				{
					indice = item.ItemIndex.ToString();
					regEliminado.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdEncriptado"].ToString());
				}
			}
			this.presentador.activarEliminado(regEliminado);
			this.RadGridMaster.Rebind();
			string radalertscript = "<script language='javascript'>function f(){changeTextRadAlert();radalert('REGISTRO ACTIVADO...<br/><br/>Seleccione el registro que desea editar para ver el detalle', 380, 50,";
			radalertscript += "'Ver detalle de Registro'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
			Page.ClientScript.RegisterStartupScript(this.GetType(), "", radalertscript);

		}
		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		///<summary>Propiedad que asigna u obtiene el Id.</summary>
		public int Id
		{
			get
			{
				return base.Id;
			}
			set
			{
				base.Id = value;
				IdFlujoServicioOculto.Value = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(value.ToString().Encriptar()));
			}
		}

		public string HiddenId
		{
			get { return TxtHiddenId.Value; }
			set { TxtHiddenId.Value = string.Format("{0:N0}", value); }
		}
		public string HiddenTipo
		{
			get { return TxtHiddenTipo.Value; }
			set { TxtHiddenTipo.Value = string.Format("{0:N0}", value); }
		}
		public string Tipo
		{
			get { return TxtTipo.Text; }
			set { TxtTipo.Text = string.Format("{0:N0}", value); }
		}

		public string IdPasoInicial
		{
			get { return ddlIdPasoInicial.SelectedValue; }
			set { ddlIdPasoInicial.SelectedValue = value; }
		}
		public IEnumerable<PasoDTO> ComboIdPasoInicial
		{
			set
			{
				ddlIdPasoInicial.DataSource = value;
				ddlIdPasoInicial.DataBind();
			}
		}

		public string IndPublico
		{
			get { return chkIndPublico.Checked.ToString(); }
			set { chkIndPublico.Checked = ExtensionesString.ConvertirBoolean(value); }
		}

		public string IdSuscriptor
		{
			get { return TxtIdSuscriptor.Text; }
			set { TxtIdSuscriptor.Text = string.Format("{0:N0}", value); }
		}


		public string IdOrigen
		{
			get { return UsuarioActual.Id.ToString(); }

		}

		public string IdServicioSuscriptor
		{
			get { return ddlIdServicioSuscriptor.SelectedValue; }
			set { ddlIdServicioSuscriptor.SelectedValue = value; }
		}

		public DataTable ComboIdServicioSuscriptor
		{
			set
			{
				ddlIdServicioSuscriptor.DataSource = value;
				ddlIdServicioSuscriptor.DataBind();
			}
		}

		public string SlaTolerancia
		{
			get { return txtSlaTolerancia.CantidadTotalEnSegundos; }
			set { txtSlaTolerancia.CantidadTotalEnSegundos = value; }
		}

		public string SlaPromedio
		{
			get { return txtSlaPromedio.CantidadTotalEnSegundos; }
			set { txtSlaPromedio.CantidadTotalEnSegundos = value; }
		}

		public int IdPrioridad
		{
			get { return Int16.Parse(ddlIdPrioridad.SelectedValue); }
			set { ddlIdPrioridad.SelectedValue = value.ToString(); }
		}
		public IEnumerable<ListasValorDTO> ComboIdPrioridad
		{
			set
			{
				ddlIdPrioridad.DataSource = value;
				ddlIdPrioridad.DataBind();
			}
		}

		public int Version
		{
			get { return int.Parse(txtVersion.Text); }
			set { txtVersion.Text = value.ToString(); }
		}

		public string IndCms
		{
			get { return chkIndCms.Checked.ToString(); }
			set { chkIndCms.Checked = ExtensionesString.ConvertirBoolean(value); }
		}

        public string IndSimulable
        {
            get { return chkIndSimulable.Checked.ToString(); }
            set { chkIndSimulable.Checked = ExtensionesString.ConvertirBoolean(value); }
        }


        public string IndChat
        {
            get { return chkIndChat.Checked.ToString(); }
            set { chkIndChat.Checked = ExtensionesString.ConvertirBoolean(value); }
        }

        public string NombrePrograma
        {
            get { return txtNomProg.Text; }
            set { txtNomProg.Text = value; }
        }

		public string IndBloqueGenericoSolicitud
		{
			get { return chkIndBloqueGenericoSolicitud.Checked.ToString(); }
			set { chkIndBloqueGenericoSolicitud.Checked = ExtensionesString.ConvertirBoolean(value); }
		}

		public string MetodoPreSolicitud
		{
			get { return txtMetodoPreSolicitud.Text; }
			set { txtMetodoPreSolicitud.Text = value; }
		}

		public string MetodoPostSolicitud
		{
			get { return txtMetodoPostSolicitud.Text; }
			set { txtMetodoPostSolicitud.Text = value; }
		}

		public int Casos
		{
			set
			{
				casos = value;
			}
		}

		///<summary>Propiedad que asigna la cadena de errores o información personalizada devuelta desde el presenter.</summary>
		public string ErroresCustom
		{
			set
			{
				RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				if(windowManagerTemp != null)
					windowManagerTemp.RadAlert(value, 380, 50, "Mensaje", "IrAnterior");
			}
		}

		///<summary>Propiedad que asigna la cadena de errores o información personalizada devuelta desde el presenter.</summary>
		public string Confirm
		{
			set
			{
				RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				if(windowManagerTemp != null)
					windowManagerTemp.RadConfirm(value, "Confirm", 380, null, null, "Mensaje");
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
		public string FechaModicacion
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

		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable<Etapa> Datos
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

		public string XMLEstructura
		{
			get
			{
				if(this.Session["EstructuraXML"] != null)
					return this.Session["EstructuraXML"].ToString();
				else
					return "";
			}
			set
			{
				this.Session["EstructuraXML"] = value;
			}
		}

		#endregion "Propiedades de Presentación"
	}
}
