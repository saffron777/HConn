using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Configuration;
using System.Web.UI;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

///<summary>Namespace que engloba la vista maestro detalle de la capa web HC_Configurador.</summary>
namespace HConnexum.Tramitador.Sitio
{
	///<summary>Clase FlujosServicioMaestroDetalle.</summary>
	public partial class SolicitudBloqueMaestroDetalle : PaginaMaestroDetalleBase, ISolicitudBloqueMaestroDetalle
	{
		#region "Variables Locales"
		///<summary>Variable presentador FlujosServicioPresentadorMaestroDetalle.</summary>
		SolicitudBloquePresentadorMaestroDetalle presentador;
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
				this.Accion = AccionDetalle.Ver;
				base.Page_Init(sender, e);
				this.presentador = new SolicitudBloquePresentadorMaestroDetalle(this);
				this.Orden = "Id";
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
				if(!Page.IsPostBack)
				{
					this.CultureDatePicker();
					if(this.Accion == AccionDetalle.Ver || this.Accion == AccionDetalle.Modificar)
						this.RadGridMaster.Rebind();
					else if(this.Accion == AccionDetalle.Agregar)
						RadGridMaster.Visible = false;
					if(this.Accion == AccionDetalle.Modificar)
						this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
				}
				this.RadFilterMaster.Culture = Thread.CurrentThread.CurrentCulture;
				this.RadFilterMaster.RecreateControl();
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
			if(this.Accion == AccionDetalle.Ver)
				this.BloquearControles(true);
			RadToolBarButton filtro = this.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
			if(filtro != null)
				if(this.RadFilterMaster.RootGroup.Expressions.Count > 0)
					filtro.CssClass = @"rtbTextNeg";
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
			if(e != null)
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
					case @"RemoveExpression":
						break;
					case @"AddGroup":
						this.LblMessege.Text = @"La Opcion de Agregar Grupo no esta Disponible y no ejecuta ninguna acción";
						break;
					case @"AddExpression":
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
				this.NumeroPagina = 1;
				this.ParametrosFiltro = this.ExtraerParametrosFiltro(this.RadFilterMaster);
				this.RadGridMaster.Rebind();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
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
			catch(Exception ex)
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
					case @"Eliminar":
						IList<string> eliminadas = new List<string>();
						foreach(GridDataItem item in this.RadGridMaster.Items)
						{
							if(item.Selected)
								if(!((CheckBox)e.Item.OwnerTableView.Items[item.ItemIndex]["IndEliminado"].Controls[0]).Checked)
									eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdEncriptado"].ToString());
						}
						this.presentador.Eliminar(eliminadas);
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

		/// <summary>
		/// Botón que ejecuta el procedimiento para activar un registro marcado como eliminado
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnActivarEliminado_Click(object sender, EventArgs e)
		{
			IList<string> regEliminado = new List<string>();
			foreach(GridDataItem item in this.RadGridMaster.Items)
				if(item.Selected)
					regEliminado.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IdEncriptado"].ToString());
			this.presentador.ActivarEliminado(regEliminado);
			this.RadGridMaster.Rebind();
			string radalertscript = "<script language='javascript'>function f(){changeTextRadAlert();radalert('REGISTRO ACTIVADO...<br/><br/>Seleccione el registro que desea editar para ver el detalle', 380, 50,";
			radalertscript += "'Ver detalle de Registro'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
			Page.ClientScript.RegisterStartupScript(this.GetType(), "", radalertscript);
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

		public string IndPublico
		{
			get
			{
				return chkIndPublico.Checked.ToString();
			}
			set
			{
				chkIndPublico.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}

		public string IdSuscriptor
		{
			get
			{
				return txtIdSuscriptor.Text;
			}
			set
			{
				txtIdSuscriptor.Text = string.Format("{0:N0}", value);
			}
		}

		public string IdServicioSuscriptor
		{
			get
			{
				return txtIdServicioSuscriptor.Text;
			}
			set
			{
				txtIdServicioSuscriptor.Text = string.Format("{0:N0}", value);
			}
		}

		public string Version
		{
			get
			{
				return txtVersion.Text;
			}
			set
			{
				txtVersion.Text = string.Format("{0:N0}", value);
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el indicador de eliminado.</summary>
		public string IndEliminado
		{
			get
			{
				return this.Auditoria.IndEliminado.ToString();
			}
			set
			{
				this.Auditoria.IndEliminado = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el usuario creador del regitros.</summary>
		public string CreadoPor
		{
			get
			{
				return this.Auditoria.CreadoPor;
			}
			set
			{
				this.Auditoria.CreadoPor = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de creación.</summary>
		public string FechaCreacion
		{
			get
			{
				return this.Auditoria.FechaCreacion;
			}
			set
			{
				this.Auditoria.FechaCreacion = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene el usuario que modificó el regitros.</summary>
		public string ModificadoPor
		{
			get
			{
				return this.Auditoria.ModificadoPor;
			}
			set
			{
				this.Auditoria.ModificadoPor = value;
			}
		}

		///<summary>Propiedad de auditoria que asigna u obtiene la fecha de modificación.</summary>
		public string FechaModicacion
		{
			get
			{
				return this.Auditoria.FechaModificacion;
			}
			set
			{
				this.Auditoria.FechaModificacion = value;
			}
		}

		///<summary>Propiedad de publicación que asigna u obtiene la fecha de validez.</summary>
		public string FechaValidez
		{
			get
			{
				return this.Publicacion.FechaValidez;
			}
			set
			{
				this.Publicacion.FechaValidez = value;
			}
		}

		///<summary>Propiedad de publicación que asigna u obtiene el indicador de vigencia.</summary>
		public string IndVigente
		{
			get
			{
				return this.Publicacion.IndVigente;
			}
			set
			{
				this.Publicacion.IndVigente = value;
			}
		}

		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable<SolicitudBloqueDTO> Datos
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
		#endregion "Propiedades de Presentación"
	}
}