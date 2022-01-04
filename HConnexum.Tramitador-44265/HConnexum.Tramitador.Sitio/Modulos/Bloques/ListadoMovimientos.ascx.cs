using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class ListadoMovimientos : UserControlListaBase, IListadoMovimientos
	{
		ListadoMovimientosPresentador presentador;

		#region "M I E M B R O S   P R I V A D O S"
		private string configClaveExcepcionMensaje = "MensajeExcepcion";
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
				this.presentador = new ListadoMovimientosPresentador(this);
				Orden = "Movimiento";
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
				}
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
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, ex.ToString());
				Trace.Warn(@"Error", ex.ToString(), ex);
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
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

		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
				this.presentador.MostrarVista(this.Orden);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
		}
		#endregion

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

		public IEnumerable<ListaMovimientosDTO> Datos
		{
			get
			{
				
				return (IEnumerable<ListaMovimientosDTO>)RadGridMaster.DataSource;
			}
			set
			{
				RadGridMaster.DataSource = value;
			}
		}
	}
}