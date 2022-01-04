using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using HConnexum.Base.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

namespace HConnexum.Base.Vista
{
	/// <summary>
	/// Actúa como clase base para las páginas tipo MaestroDetalle, heredando las funcionalidades de las clases DetalleBase y ListaBase. Esta clase proporciona los métodos y propiedades necesarios para el funcionamiento de las páginas tipo MaestroDetalle.
	/// </summary>
	/// <typeparam name="T">Tipo de la clase de la página</typeparam>
	public class PaginaMaestroDetalleBase<P> : PaginaDetalleBase<P>, IListaBase
	{
		#region Constructores
		
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		[SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
		public PaginaMaestroDetalleBase()
		{
			this.paginaListaBase = new PaginaListaBase<P>(this);
		}
		
		#endregion
		
		#region Eventos
		
		/// <summary>Evento de inicialización de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected new void Page_Init(object sender, EventArgs e)
		{
			this.paginaListaBase.Interfaz = this.Interfaz;
			base.Page_Init(sender, e);
			this.paginaListaBase.Page_Init();
			this.idMaster = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.Id.ToString().Encriptar()));
		}
		
		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected new void Page_Load(object sender, EventArgs e)
		{
			base.Page_Load_1(sender, e);
			this.paginaListaBase.Page_Load(this.IsPostBack);
			
			if (!this.Page.IsPostBack)
			{
				if (this.Accion == AccionDetalle.Ver || this.Accion == AccionDetalle.Modificar)
					this.RadGrid.Rebind();
				else if (this.Accion == AccionDetalle.Agregar)
					this.RadGrid.Visible = false;
				
				this.EjecutarMetodoPresentador(@"MostrarVistaDetalle", null);
			}
		}
		
		/// <summary>Evento previo al renderizado de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected new void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
			this.paginaListaBase.Page_PreRender();
		}
		
		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			this.paginaListaBase.RadGridMaster_NeedDataSource(sender, e);
		}
		
		///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_ItemCommand(object sender, GridCommandEventArgs e)
		{
			this.paginaListaBase.RadGridMaster_ItemCommand(sender, e);
		}
		
		public void BtnActivarEliminado_Click(object sender, EventArgs e)
		{
			this.paginaListaBase.BtnActivarEliminado_Click(sender, e);
		}
		
		///<summary>Evento del rad filter del grid que se dispara cuando se hace click en aceptar en los filtros. (Este evento será eliminado en futuras versiones, no se recomienda su uso)</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void ApplyButton_Click(object sender, ImageClickEventArgs e)
		{
			this.paginaListaBase.ApplyButton_Click(sender, e);
		}
		
		///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
		{
			this.paginaListaBase.RadGridMaster_PageIndexChanged(sender, e);
		}
		
		///<summary>Evento libreria ajax que se dispara cuando se hace peticiones en la página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			this.paginaListaBase.RadAjaxManager1_AjaxRequest(sender, e);
		}
		
		///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
		{
			this.paginaListaBase.RadGridMaster_SortCommand(sender, e);
		}
		
		///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
		{
			this.paginaListaBase.RadGridMaster_PageSizeChanged(sender, e);
		}
		
		///<summary>Evento del rad filter del grid que se dispara cuando se agrega o modifica una busqueda.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public void RadFilterMaster_ItemCommand(object sender, RadFilterCommandEventArgs e)
		{
			this.paginaListaBase.RadFilterMaster_ItemCommand(sender, e);
		}
		
		public void RadFilterMaster_ApplyExpressions(object sender, RadFilterApplyExpressionsEventArgs e)
		{
			this.paginaListaBase.RadFilterMaster_ApplyExpressions(sender, e);
		}
		
		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{
			this.paginaListaBase.RadToolBar1_ButtonClick1(sender, e);
		}
		
		#endregion
		
		#region Propiedades
		
		/// <summary>string. Variable que contiene el Id del Maestro.</summary>
		protected string idMaster;
		
		public RadGrid RadGrid
		{
			get
			{
				return this.paginaListaBase.RadGrid;
			}
		}
		
		public TiposFiltro TipoFiltro
		{
			set
			{
				this.paginaListaBase.TipoFiltro = value;
			}
		}
		
		/// <summary>string. Variable que contiene la acción agregar encriptada.</summary>
		public string AccionAgregar
		{
			get
			{
				return this.paginaListaBase.AccionAgregar;
			}
		}
		
		/// <summary>string. Variable que contiene la acción modificar encriptada.</summary>
		public string AccionModificar
		{
			get
			{
				return this.paginaListaBase.AccionModificar;
			}
		}
		
		/// <summary>string. Variable que contiene la acción ver encriptada.</summary>
		public string AccionVer
		{
			get
			{
				return this.paginaListaBase.AccionVer;
			}
		}
		
		private readonly PaginaListaBase<P> paginaListaBase;
		
		/// <summary>Propiedad que Obtiene o Asigna el Número de la pagina de la lista.</summary>
		public int NumeroPagina
		{
			get
			{
				return this.paginaListaBase.NumeroPagina;
			}
			set
			{
				this.paginaListaBase.NumeroPagina = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o asigna el tamaño de una pagina de la lista.</summary>
		public int TamanoPagina
		{
			get
			{
				return this.paginaListaBase.TamanoPagina;
			}
			set
			{
				this.paginaListaBase.TamanoPagina = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o asigna el ordenamiento de la lista.</summary>
		public string Orden
		{
			get
			{
				return this.paginaListaBase.Orden;
			}
			set
			{
				this.paginaListaBase.Orden = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o signa los parametros de filtrado de la pagina de la lista.</summary>
		public IList<HConnexum.Infraestructura.Filtro> ParametrosFiltroOriginal
		{
			get
			{
				return this.paginaListaBase.ParametrosFiltroOriginal;
			}
			set
			{
				this.paginaListaBase.ParametrosFiltroOriginal = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o signa los parametros de filtrado de la pagina de la lista.</summary>
		public string ParametrosFiltro
		{
			get
			{
				return this.paginaListaBase.ParametrosFiltro;
			}
			set
			{
				this.paginaListaBase.ParametrosFiltro = value;
			}
		}
		
		public bool IndRolEliminado
		{
			get
			{
				return this.paginaListaBase.IndRolEliminado;
			}
		}
		
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable Datos
		{
			set
			{
				this.paginaListaBase.Datos = value;
			}
		}
		
		///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
		public int NumeroDeRegistros
		{
			get
			{
				return this.paginaListaBase.NumeroDeRegistros;
			}
			set
			{
				this.paginaListaBase.NumeroDeRegistros = value;
			}
		}
		
		public IList<string> IdsEliminar
		{
			get
			{
				return this.paginaListaBase.IdsEliminar;
			}
		}
		
		#endregion
		
		#region Métodos
		
		protected DataTable ObtenerDatatableConIdEncriptado(IEnumerable ien)
		{
			return this.paginaListaBase.ObtenerDatatableConIdEncriptado(ien);
		}
		
		/// <summary>Metodo que extrae del control los filtros de una lista.</summary>
		/// <param name="radFilter">RadFilter. Control que contiene los filtros.</param>
		/// <returns>IList Filtro. Listado de Filtros.</returns>
		protected IList<HConnexum.Infraestructura.Filtro> ExtraerParametrosFiltro(RadFilter radFilter)
		{
			return this.ExtraerParametrosFiltro(radFilter);
		}
		
		#endregion
	}
}