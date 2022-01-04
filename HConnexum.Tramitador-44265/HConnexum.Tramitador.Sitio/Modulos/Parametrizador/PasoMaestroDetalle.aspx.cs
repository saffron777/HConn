using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Web;
using System.Text;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio.Modulos.Parametrizador
{
	///<summary>Clase PasoMaestroDetalle.</summary>
	public partial class PasoMaestroDetalle : PaginaMaestroDetalleBase, IPasoMaestroDetalle
	{
		#region "Variables Locales"
		///<summary>Variable presentador PasoPresentadorMaestroDetalle.</summary>
		PasoPresentadorMaestroDetalle presentador;
		public string clientId;
		public string ventanaId;
		public string filtroId;
		public string idEncriptado;
		public string accionEncriptada;
		public string accionVista;
		public string ventanaDetalle;
		public string txtSlaTolerancia_txtHoras;
		public string txtSlaTolerancia_txtMinutos;
		public string txtSlaTolerancia_txtSegundos;
		public string txtSlaTolerancia_rimHorasMinutosSegundos;
		public string RutaPadreEncriptada;
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
				presentador = new PasoPresentadorMaestroDetalle(this);
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
					RutaPadreEncriptada = ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect;
					txtSlaTolerancia_txtHoras = txtSlaTolerancia.FindControl("txtHoras").ClientID;
					txtSlaTolerancia_txtMinutos = txtSlaTolerancia.FindControl("txtMinutos").ClientID;
					txtSlaTolerancia_txtSegundos = txtSlaTolerancia.FindControl("txtSegundos").ClientID;
					txtSlaTolerancia_rimHorasMinutosSegundos = txtSlaTolerancia.FindControl("rimHorasMinutosSegundos").ClientID;
					presentador.LlenarCabecera(Accion);
					presentador.LlenarComboTipoPaso();
					presentador.LlenarComboEstatusInicial();
					presentador.LlenarComboAlerta(Accion);
					presentador.LlenarComboSubServicio(Accion);
					if(Accion == AccionDetalle.Ver || Accion == AccionDetalle.Modificar)
					{
						presentador.MostrarVista(Orden, NumeroPagina, TamanoPagina, ParametrosFiltro, false);
						idEncriptado = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.Id.ToString().Encriptar()));
						accionEncriptada = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(Accion.ToString().Encriptar()));
						this.RadGridMaster.Rebind();
						clientId = RadGridMaster.ClientID;
						ventanaId = RadWindow1.ClientID;
						filtroId = RadFilterMaster.ClientID;
						ventanaDetalle = "Modulos/Parametrizador/PasosRepuestaDetalle.aspx?IdMenu=" + IdMenuEncriptado + "&";
					}
					else if(Accion == AccionDetalle.Agregar)
					{
						accionVista = "Agregar";
						botonera.Visible = false;
						RadGridMaster.Visible = false;
					}
					if(this.Accion == AccionDetalle.Modificar)
					{
						accionVista = "Modificar";
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
			{
				if(this.RadFilterMaster.RootGroup.Expressions.Count > 0)
					filtro.CssClass = @"rtbTextNeg";
			}
			if(Accion == AccionDetalle.Ver)
				BloquearControles(true);
			if(Accion == AccionDetalle.Agregar)
			{
				Publicacion.Visible = false;
				Auditoria.Visible = false;
			}
			MostrarBotones(cmdGuardar, cmdGuardaryAgregar, cmdLimpiar, Accion);
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				presentador.GuardarCambios(Accion);
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
				presentador.GuardarCambios(Accion);
				txtNombre.Text = "";
				txtOrden.Text = "";
				txtObservacion.Text = "";
				ddlIdTipoPaso.ClearSelection();
				txtSlaTolerancia.CantidadTotalEnSegundos = "0";
				txtCantidadRepeticion.Text = "";
				txtReintentos.Text = "";
				txtPorcSlaCritico.Text = "";
				txtURL.Text = "";
				txtMetodo.Text = "";
				ddlIdSubServicio.ClearSelection();
				txtEtiqSincroIn.Text = "";
				txtEtiqSincroOut.Text = "";
				ddlAlerta.ClearSelection();
				chkIndAgendable.Checked = false;
				chkIndCerrarEtapa.Checked = false;
				chkIndCerrarServicio.Checked = false;
				chkIndIniciaEtapa.Checked = false;
				chkIndObligatorio.Checked = false;
				chkIndRequiereRespuesta.Checked = false;
				chkIndSegSubServicio.Checked = false;
				chkIndSeguimiento.Checked = false;
                chkIndAnulacion.Checked = false;
				Publicacion.IndVigente = "false";
				Publicacion.FechaValidez = "";
				presentador.LlenarComboPasoInicial(Accion);
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
				presentador.MostrarVista(Orden, NumeroPagina, TamanoPagina, ParametrosFiltro, true);
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
						IList<string> Eliminadas = new List<string>();
						GridDataItemCollection items;
						items = RadGridMaster.Items;
						foreach(GridDataItem item in items)
							if(item.Selected)
								Eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdEncriptado"].ToString());
						presentador.Eliminar(Eliminadas);
						this.RadGridMaster.Rebind();
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

		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		public int Id
		{
			get
			{
				return base.Id;
			}
			set
			{
				base.Id = value;
			}
		}
		public string NombreServicio
		{
			set
			{
				this.txtNombreServicio.Text = value;
			}
		}
		public string NombreEstatus
		{
			get
			{
				return txtNombreEstatus.Text;
			}
			set
			{
				txtNombreEstatus.Text = value;
			}
		}

		public string EtiqSincroIn
		{
			get
			{
				return txtEtiqSincroIn.Text;
			}
			set
			{
				txtEtiqSincroIn.Text = value;
			}
		}
		public string EtiqSincroOut
		{
			get
			{
				return txtEtiqSincroOut.Text;
			}
			set
			{
				txtEtiqSincroOut.Text = value;
			}
		}

		public string NombreEtapa
		{
			set
			{
				txtNombreEtapa.Text = value;
			}
		}
		public string NombreVersion
		{
			set
			{
				txtNombreVersion.Text = value;
			}
		}
		public string Nombre
		{
			get
			{
				return txtNombre.Text;
			}
			set
			{
				txtNombre.Text = value;
			}
		}
		public string IdTipoPaso
		{
			get
			{
				return ddlIdTipoPaso.SelectedValue;
			}
			set
			{
				ddlIdTipoPaso.SelectedValue = value;
			}
		}
		public IEnumerable<TipoPasoDTO> ComboIdTipoPaso
		{
			set
			{
				ddlIdTipoPaso.DataSource = value;
				ddlIdTipoPaso.DataBind();
			}
		}

		public string IdEstatusInicial
		{
			get
			{
				return ddlIdEstatusInicial.SelectedValue;
			}
			set
			{
				ddlIdEstatusInicial.SelectedValue = value;
			}
		}

		public IEnumerable<ListasValorDTO> ComboIdEstatusInicial
		{
			set
			{
				ddlIdEstatusInicial.DataSource = value;
				ddlIdEstatusInicial.DataBind();
			}
		}

		public string SlaTolerancia
		{
			get
			{
				return txtSlaTolerancia.CantidadTotalEnSegundos;
			}
			set
			{
				txtSlaTolerancia.CantidadTotalEnSegundos = value;
			}
		}
		public string IdSubServicio
		{
			get
			{
				return ddlIdSubServicio.SelectedValue;
			}
			set
			{
				ddlIdSubServicio.SelectedValue = value;
			}
		}
		public List<FlujosServicioDTO> ComboIdSubServicio
		{
			set
			{
				ddlIdSubServicio.DataSource = value;
				ddlIdSubServicio.DataBind();
			}
		}
		public string CantidadRepeticion
		{
			get
			{
				return txtCantidadRepeticion.Text;
			}
			set
			{
				txtCantidadRepeticion.Text = value;
			}
		}
		public string Reintentos
		{
			get
			{
				return txtReintentos.Text;
			}
			set
			{
				txtReintentos.Text = value;
			}
		}
		public string Observacion
		{
			get
			{
				return txtObservacion.Text;
			}
			set
			{
				txtObservacion.Text = value;
			}
		}
		public string URL
		{
			get
			{
				return txtURL.Text;
			}
			set
			{
				txtURL.Text = value;
			}
		}
		public string PgmObtieneRespuestas
		{
			get
			{
				return this.txtPgmObtieneRespuestas.Text;
			}
			set
			{
				txtPgmObtieneRespuestas.Text = value;
			}
		}
		public string Metodo
		{
			get
			{
				return txtMetodo.Text;
			}
			set
			{
				txtMetodo.Text = value;
			}
		}
		public string IdAlerta
		{
			get
			{
				return ddlAlerta.SelectedValue;
			}
			set
			{
				ddlAlerta.SelectedValue = value;
			}
		}
		public DataTable ComboIdAlerta
		{
			set
			{
				ddlAlerta.DataSource = value;
				ddlAlerta.DataBind();
			}
		}
		public string PorcSlaCritico
		{
			get
			{
				return txtPorcSlaCritico.Text;
			}
			set
			{
				txtPorcSlaCritico.Text = value;
			}
		}
		public string OrdenPaso
		{
			get
			{
				return txtOrden.Text;
			}
			set
			{
				txtOrden.Text = value;
			}
		}
		public string IndIniciaEtapa
		{
			get
			{
				return chkIndIniciaEtapa.Checked.ToString();
			}
			set
			{
				chkIndIniciaEtapa.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndSeguimiento
		{
			get
			{
				return chkIndSeguimiento.Checked.ToString();
			}
			set
			{
				chkIndSeguimiento.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndAgendable
		{
			get
			{
				return chkIndAgendable.Checked.ToString();
			}
			set
			{
				chkIndAgendable.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndRequiereRespuesta
		{
			get
			{
				return chkIndRequiereRespuesta.Checked.ToString();
			}
			set
			{
				chkIndRequiereRespuesta.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndSegSubServicio
		{
			get
			{
				return chkIndSegSubServicio.Checked.ToString();
			}
			set
			{
				chkIndSegSubServicio.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndObligatorio
		{
			get
			{
				return chkIndObligatorio.Checked.ToString();
			}
			set
			{
				chkIndObligatorio.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndCerrarEtapa
		{
			get
			{
				return chkIndCerrarEtapa.Checked.ToString();
			}
			set
			{
				chkIndCerrarEtapa.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndCerrarServicio
		{
			get
			{
				return chkIndCerrarServicio.Checked.ToString();
			}
			set
			{
				chkIndCerrarServicio.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		public string IndEncadenado
		{
			get
			{
				return chkIndEncadenado.Checked.ToString();
			}
			set
			{
				chkIndEncadenado.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		///<summary>Propiedad de auditoria que asigna u obtiene el indicador de eliminado.</summary>
		public string IndEliminado
		{
			get
			{
				return Auditoria.IndEliminado.ToString();
			}
			set
			{
				Auditoria.IndEliminado = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el usuario creador del regitros.</summary>
		public string CreadoPor
		{
			get
			{
				return Auditoria.CreadoPor;
			}
			set
			{
				Auditoria.CreadoPor = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de creación.</summary>
		public string FechaCreacion
		{
			get
			{
				return Auditoria.FechaCreacion;
			}
			set
			{
				Auditoria.FechaCreacion = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el usuario que modificó el regitros.</summary>
		public string ModificadoPor
		{
			get
			{
				return Auditoria.ModificadoPor;
			}
			set
			{
				Auditoria.ModificadoPor = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de modificación.</summary>
		public string FechaModificacion
		{
			get
			{
				return Auditoria.FechaModificacion;
			}
			set
			{
				Auditoria.FechaModificacion = value;
			}
		}

		///<summary>Propiedad de publicación que asigna u obtiene la fecha de validez.</summary>
		public string FechaValidez
		{
			get
			{
				return Publicacion.FechaValidez;
			}
			set
			{
				Publicacion.FechaValidez = value;
			}
		}

		///<summary>Propiedad de publicación que asigna u obtiene el indicador de vigencia.</summary>
		public string IndVigente
		{
			get
			{
				return Publicacion.IndVigente;
			}
			set
			{
				Publicacion.IndVigente = value;
			}
		}

		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable<PasosRepuestaDTO> Datos
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

		///<summary>Propiedad que asigna la cadena de errores o información personalizada devuelta desde el presenter.</summary>
		public string ErroresCustomEditar
		{
			set
			{
				RadWindowManager windowManagerTemp = this.Page.Controls.FindAll<RadWindowManager>().FirstOrDefault();
				if(windowManagerTemp != null)
					windowManagerTemp.RadAlert(value, 380, 50, "Mensaje", "IrAnterior");
			}
		}

		public string MetodoAsignacion
		{
			get
			{
				return txtMetodoAsisgnacion.Text;
			}
			set
			{
				txtMetodoAsisgnacion.Text = value;
			}
		}

		public string MetodoAsignacionCha
		{
			get
			{
				return txtAsignacionCHA.Text;
			}
			set
			{
				txtAsignacionCHA.Text = value;
			}
		}

        public string IndAnulacion
        {
            get
            {
                return chkIndAnulacion.Checked.ToString();
            }
            set
            {
                chkIndAnulacion.Checked = ExtensionesString.ConvertirBoolean(value);
            }
        }
		#endregion "Propiedades de Presentación"
	}
}