using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Seguridad;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;
using System.Data;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class CasoMedicos : UserControlListaBase, ICambioMedico
	{
		#region "M I E M B R O S   P R I V A D O S"
		private string configClaveExcepcionMensaje = "MensajeExcepcion";
		#endregion

		#region "M I E M B R O S   P Ú B L I C O S"
		///<summary>Objeto 'Presentador' asociado al control Web de usuario.</summary>
		CambioMedicoPresentador presentador;
		/// <summary>Identificador único del control Web de usuario.</summary>
		public string Id
		{
			get { return this.RadGridMaster.ID; }
			set { this.RadGridMaster.ID = value; }
		}
		/// <summary>Nombre del médico.</summary>
		public string MedicoNombre
		{
			get { return this.MedicoNombre; }
			set { this.MedicoNombre = value; }
		}
		/// <summary>Nombre del país.</summary>
		public string MedicoPais
		{
			get { return this.MedicoPais; }
			set { this.MedicoPais = value; }
		}
		/// <summary>Nombre de la división territorial 1.</summary>
		public string MedicoDivisionTerritorial1
		{
			get { return this.MedicoDivisionTerritorial1; }
			set { this.MedicoDivisionTerritorial1 = value; }
		}
		/// <summary>Nombre de la división territorial 2.</summary>
		public string MedicoDivisionTerritorial2
		{
			get { return this.MedicoDivisionTerritorial2; }
			set { this.MedicoDivisionTerritorial2 = value; }
		}
		/// <summary>Nombre de la división territorial 3.</summary>
		public string MedicoDivisionTerritorial3
		{
			get { return this.MedicoDivisionTerritorial3; }
			set { this.MedicoDivisionTerritorial3 = value; }
		}
		/// <summary>Nombre de la especialidad del médico.</summary>
		public string MedicoDivisionEspecialidad
		{
			get { return this.MedicoDivisionEspecialidad; }
			set { this.MedicoDivisionEspecialidad = value; }
		}
		/// <summary>Cantidad de casos pendientes del médico.</summary>
		public int MedicoCantidadCasosPendientes
		{
			get { return this.MedicoCantidadCasosPendientes; }
			set { this.MedicoCantidadCasosPendientes = value; }
		}
		/// <summary>Mensaje de error a mostrar por el control Web de usuario.</summary>
		public string Errores
		{
			get { return base.Errores; }
			set { base.Errores = value; }
		}
		public string BuscaPersona
		{
			get { return this.txtBuscaPersona.Text; }
			set { this.txtBuscaPersona.Text = value; }
		}
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable<SuscriptorDTO> Datos
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
		public string IdRed
		{
			get
			{
				return ddlIdredes.SelectedValue;
			}
			set
			{
				ddlIdredes.SelectedValue = value;
			}
		}
		public DataTable ComboIdRed
		{
			set
			{
				ddlIdredes.DataSource = value;
				ddlIdredes.DataBind();
			}
		}
		#endregion

		#region E V E N T O S
		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new CambioMedicoPresentador(this);
				Orden = "Id";
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
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
				if(!this.IsPostBack)
				{
					this.NumeroPagina = 1;
					this.RadGridMaster.Rebind();
					presentador.LlenarCombo();
				}
				this.RadFilterMaster.Culture = Thread.CurrentThread.CurrentCulture;
				this.RadFilterMaster.RecreateControl();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			try
			{
				base.OnPreRender(e);
				RadToolBarButton filtro = null;
				foreach(Control ctrl in this.Controls)
					if(ctrl.ID == @"i1")
					{
						filtro = (RadToolBarButton)ctrl;
						break;
					}
				if(filtro != null)
					if(this.RadFilterMaster.RootGroup.Expressions.Count > 0)
						filtro.CssClass = @"rtbTextNeg";
				if(!this.presentador.bObtenerRolIndEliminado())
				{
					GridColumn columna = RadGridMaster.MasterTableView.GetColumnSafe("IndEliminado");
					if(columna != null)
						columna.Visible = false;
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, ex.ToString());
				Trace.Warn(@"Error", ex.ToString(), ex);
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
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
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
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
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
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
					IList<string> Eliminadas = new List<string>();
					foreach(GridDataItem item in this.RadGridMaster.Items)
					{
						if(item.Selected)
							if(!((CheckBox)e.Item.OwnerTableView.Items[item.ItemIndex]["IndEliminado"].Controls[0]).Checked)
								Eliminadas.Add(item.OwnerTableView.DataKeyValues[item.ItemIndex][@"IdEncriptado"].ToString());
					}
					this.presentador.Eliminar(Eliminadas);
					this.RadGridMaster.Rebind();
					break;
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
		}

		/// <summary>
		/// Botón que ejecuta el procedimiento para activar un registro marcado como eliminado
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

		protected void RadGridMaster_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
		{
		}
		#endregion

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			this.presentador.MostrarVista(this.Orden, this.NumeroPagina, this.TamanoPagina, this.ParametrosFiltro);
			RadGridMaster.Rebind();
		}

		protected void btnLimpiar_Click(object sender, EventArgs e)
		{
			this.txtBuscaPersona.Text = string.Empty;
			this.ddlIdredes.ClearSelection();
			RadGridMaster.Rebind();
		}

		public string ValidarDatos()
		{
			string strReturn = "";
			try
			{
				if(RadGridMaster.SelectedItems.Count <= 0)
					strReturn = "Debe seleccionar un médico para continuar.";
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
			return strReturn;
		}
	}
}