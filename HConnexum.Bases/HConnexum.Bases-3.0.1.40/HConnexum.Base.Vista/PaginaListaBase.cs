using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Base.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

namespace HConnexum.Base.Vista
{
	/// <summary>
	/// Actúa como clase base para las páginas tipo Lista, heredando las funcionalidades de la clase PaginaBase. Esta clase proporciona los métodos y propiedades necesarios para el funcionamiento de las páginas tipo Lista. 
	/// </summary>
	public class PaginaListaBase<P> : PaginaBase<P>, IListaBase
	{
		#region Constructores
		
		public PaginaListaBase()
		{
		}
		
		public PaginaListaBase(Page pagina)
		{
			this.pagina = pagina;
		}
		
		#endregion
		
		#region Eventos
		
		/// <summary>Evento de inicialización de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected new void Page_Init(object sender, EventArgs e)
		{
			if (this.pagina == null)
				this.pagina = this;
			
			this.Page_Init();
			base.Page_Init(sender, e);
		}
		
		internal void Page_Init()
		{
			this.Presentador = this.CrearInstanciaPresentador();
			this.TipoFiltro = TiposFiltro.Sql;
			
			if (!this.pagina.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"LRSB"))
				this.pagina.ClientScript.RegisterClientScriptInclude(typeof(string), @"LRSB", this.pagina.ResolveClientUrl(@"~/Scripts/ListaRadScriptBlock1.js"));
			
			this.idMaster = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"0".Encriptar()));
			this.Orden = @"Id";
		}
		
		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected new void Page_Load(object sender, EventArgs e)
		{
			base.Page_Load(sender, e);
			this.Page_Load(this.IsPostBack);
		}
		
		internal void Page_Load(bool isPostBack)
		{
			this.RadGrid = ((ContentPlaceHolder)this.pagina.Master.FindControl(@"cphBody")).Controls.FindAll<RadGrid>().First();
			this.radFilter = (RadFilter)(((ContentPlaceHolder)this.pagina.Master.FindControl(@"cphBody")).Controls.FindAll<RadWindow>().First().Controls.FindAll<SingleTemplateContainer>().First().FindControl(@"RadFilterMaster"));
			this.radFilter.Culture = new System.Globalization.CultureInfo(@"es-VE");
			this.radFilter.RecreateControl();
			
			if (!isPostBack)
			{
				this.NumeroPagina = 1;
				this.RadGrid.Rebind();
			}
		}
		
		/// <summary>Evento previo al renderizado de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected new void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
			this.Page_PreRender();
		}
		
		internal void Page_PreRender()
		{
			RadToolBarButton filtro = this.pagina.Controls.FindAll<RadToolBarButton>().Where(control => control.ID == @"i1").FirstOrDefault();
			
			if (filtro != null)
			{
				if (this.radFilter.RootGroup.Expressions.Count > 0)
					filtro.CssClass = @"rtbTextNeg";
			}
			if (!this.IndRolEliminado)
			{
				GridColumn columna = this.RadGrid.MasterTableView.GetColumnSafe(@"IndEliminado");
				
				if (columna != null)
					columna.Visible = false;
			}
		}
		
		///<summary>Evento del rad grid que se dispara para hacer databind con el origen de datos.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			if (this.IndRolEliminado == false)
			{
				if (this.ParametrosFiltro == string.Empty)
					this.ParametrosFiltro = @" IndEliminado=0";
				else if (!this.ParametrosFiltro.Contains(@"IndEliminado=0"))
					this.ParametrosFiltro += @" AND IndEliminado=0";
			}
			this.EjecutarMetodoPresentador(@"MostrarVista", null);
		}
		
		///<summary>Evento del rad grid que se dispara cuando se hace click en algun botón del toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_ItemCommand(object sender, GridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case @"Refrescar":
					this.RadGrid.Rebind();
					break;
				case @"Eliminar":
					if (e.Item.OwnerTableView.Items.Count != 0)
					{
						if (((CheckBox)e.Item.OwnerTableView.Items[this.RadGrid.SelectedIndexes[0]][@"IndEliminado"].Controls[0]).Checked == false)
						{
							this.EjecutarMetodoPresentador(@"Eliminar", null);
							this.RadGrid.Rebind();
						}
					}
					break;
			}
		}
		
		public void BtnActivarEliminado_Click(object sender, EventArgs e)
		{
			this.EjecutarMetodoPresentador(@"ActivarEliminado", null);
			this.RadGrid.Rebind();
			string radalertscript = "<script language='javascript'>function f(){changeTextRadAlert();radalert('REGISTRO ACTIVADO...<br/><br/>Seleccione el registro que desea editar para ver el detalle', 380, 50,";
			radalertscript += "'Advertencia'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
			this.Page.ClientScript.RegisterStartupScript(this.pagina.GetType(), "", radalertscript);
		}
		
		///<summary>Evento del rad filter del grid que se dispara cuando se hace click en aceptar en los filtros. (Este evento será eliminado en futuras versiones, no se recomienda su uso)</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void ApplyButton_Click(object sender, ImageClickEventArgs e)
		{
			this.NumeroPagina = 1;
			this.ParametrosFiltroOriginal = this.ExtraerParametrosFiltro(this.radFilter);
			this.RadGrid.Rebind();
		}
		
		///<summary>Evento del rad grid que se dispara cuando está paginando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_PageIndexChanged(object sender, GridPageChangedEventArgs e)
		{
			this.NumeroPagina = e.NewPageIndex + 1;
			this.RadGrid.Rebind();
		}
		
		///<summary>Evento libreria ajax que se dispara cuando se hace peticiones en la página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Argument))
			{
				object[] parametros = new object[1];
				parametros[0] = e.Argument;
				this.EjecutarMetodoPresentador(@"EliminarRegistroTomadoAjax", parametros);
			}
			this.RadGrid.Rebind();
		}
		
		///<summary>Evento del rad grid que se dispara cuando está ordenando.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
		{
			if (e != null)
			{
				this.Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? " DESC" : " ASC");
				this.RadGrid.Rebind();
			}
		}
		
		///<summary>Evento del rad grid que se dispara cuando se cambia la cantidad de registros a mostrar por página.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadGridMaster_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
		{
			this.NumeroPagina = 1;
			this.TamanoPagina = e.NewPageSize;
			this.RadGrid.Rebind();
		}
		
		///<summary>Evento del rad filter del grid que se dispara cuando se agrega o modifica una busqueda.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public void RadFilterMaster_ItemCommand(object sender, RadFilterCommandEventArgs e)
		{
			SwebErrorClient swebErrorClient = new SwebErrorClient();
			try
			{
				
				if(e != null)
				{
					switch(e.CommandName)
					{
						case @"RemoveExpression":
							break;
						case @"AddGroup":
							this.pagina.Page.Controls.FindAll<Label>().Where(a => a.ID == @"LblMessege").First().Text = swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"RadFilterMaster_ItemCommand", UsuarioActual.Login, @"0027", Session.SessionID, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString);
							break;
						case @"AddExpression":
							this.pagina.Page.Controls.FindAll<Label>().Where(a => a.ID == @"LblMessege").First().Text = string.Empty;
							break;
					}
				}
			}
			finally
			{
				if(swebErrorClient.State != CommunicationState.Closed)
					swebErrorClient.Close();
			}
		}
		
		public void RadFilterMaster_ApplyExpressions(object sender, RadFilterApplyExpressionsEventArgs e)
		{
			this.NumeroPagina = 1;
			
			if (this.TipoFiltro == TiposFiltro.Sql)
			{
				RadFilterSqlQueryProvider providerSql = new RadFilterSqlQueryProvider();
				providerSql.ProcessGroup(e.ExpressionRoot);
				this.ParametrosFiltro = providerSql.Result;
			}
			else
			{
				RadFilterDynamicLinqQueryProvider providerLinq = new RadFilterDynamicLinqQueryProvider();
				providerLinq.ProcessGroup(e.ExpressionRoot);
				this.ParametrosFiltro = providerLinq.Result;
			}
			this.RadGrid.Rebind();
			this.pagina.Page.Controls.FindAll<RadAjaxManager>().Where(a => a.ID == @"RadAjaxManager1").First().ResponseScripts.Add(@"hideFilterBuilderDialog()");
		}
		
		///<summary>Evento necesario para que se activen los de los botones en la toolbar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		public void RadToolBar1_ButtonClick1(object sender, RadToolBarEventArgs e)
		{
		}
		
		#endregion
		
		#region Métodos
		
		public DataTable ObtenerDatatableConIdEncriptado(IEnumerable ien)
		{
			using (DataTable dt = new DataTable())
			{
				foreach (object obj in ien)
				{
					Type t = obj.GetType();
					PropertyInfo[] pis = t.GetProperties();
					
					if (dt.Columns.Count == 0)
					{
						foreach (PropertyInfo pi in pis)
						{
							Type columnType = pi.PropertyType;
							
							if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
								columnType = pi.PropertyType.GetGenericArguments()[0];
							
							dt.Columns.Add(pi.Name, columnType);
						}
						dt.Columns.Add(@"IdEncriptado", typeof(String));
					}
					DataRow dr = dt.NewRow();
					
					foreach (PropertyInfo pi in pis)
					{
						object value = pi.GetValue(obj, null);
						
						if (value == null)
							dr[pi.Name] = DBNull.Value;
						else
							dr[pi.Name] = value;
						
						if (pi.Name == @"Id")
							dr[@"IdEncriptado"] = HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(value.ToString().Encriptar()));
					}
					dt.Rows.Add(dr);
				}
				return dt;
			}
		}
		
		/// <summary>Metodo que extrae del control los filtros de una lista.</summary>
		/// <param name="radFilter">RadFilter. Control que contiene los filtros.</param>
		/// <returns>IList Filtro. Listado de Filtros.</returns>
		public IList<HConnexum.Infraestructura.Filtro> ExtraerParametrosFiltro(RadFilter radFilter)
		{
			IList<HConnexum.Infraestructura.Filtro> pFiltro = new List<HConnexum.Infraestructura.Filtro>();
			string vFiltro = string.Empty;
			string compare = string.Empty;
			
			if (radFilter.RootGroup.Expressions.Count != 0)
			{
				for (int i = 0; i < radFilter.RootGroup.Expressions.Count; i++)
				{
					HConnexum.Infraestructura.Filtro filtro = new HConnexum.Infraestructura.Filtro();
					compare = radFilter.RootGroup.Expressions.ElementAt(i).FilterFunction.ToString();
					DateTime dateTimefilterParsed = DateTime.MinValue;
					
					switch (compare)
					{
						case @"Between":
							filtro.Operador = @"GreaterThanOrEqualTo";
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(vFiltro, i, compare, radFilter, true);
							pFiltro.Add(filtro);
							filtro.Operador = @"LessThan";
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(vFiltro, i, compare, radFilter, false);
							
							if (DateTime.TryParse(filtro.Valor.ToString(), out dateTimefilterParsed))
								filtro.Valor = dateTimefilterParsed.AddDays(1);
							
							pFiltro.Add(filtro);
							break;
						case @"EqualTo":
							
							if (radFilter.RootGroup.Expressions[i].GetType().FullName.ToString().Contains(@"System.DateTime"))
							{
								filtro.Operador = @"GreaterThanOrEqualTo";
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(vFiltro, i, compare, radFilter, true);
								pFiltro.Add(filtro);
								filtro.Operador = @"LessThan";
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(vFiltro, i, compare, radFilter, false);
								
								if (DateTime.TryParse(filtro.Valor.ToString(), out dateTimefilterParsed))
									filtro.Valor = dateTimefilterParsed.AddDays(1);
								
								pFiltro.Add(filtro);
							}
							else
							{
								filtro.Operador = compare;
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(vFiltro, i, compare, radFilter, true);
								pFiltro.Add(filtro);
							}
							break;
						default:
							filtro.Operador = compare;
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(vFiltro, i, compare, radFilter, true);
							pFiltro.Add(filtro);
							break;
					}
				}
			}
			return pFiltro;
		}
		
		/// <summary>Metodo que asigna el valor a un filtro.</summary>
		/// <param name="filtro">Object. Objeto del filtro que sera asignado.</param>
		/// <param name="index">Int. Posicion del elemento en el RadFilter.</param>
		/// <param name="operador">String. Operacion que se evaluara en el filtro (Between, NotBetween, is null, is not null , etc.).</param>
		/// <param name="radFilter">RadFilter. Control donde se extraera el filtro.</param>
		/// <param name="isBetweenLeftValue">Bool. Indica si es IsBetweenLeftValue.</param>
		/// <returns>Object. Filtro.</returns>
		private object AsignarFiltro(object filtro, int index, string operador, RadFilter radFilter, bool isBetweenLeftValue)
		{
			if (radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.Int32"))
			{
				int intfilterParsed = 0;
				filtro = int.TryParse(this.GetFilter<int>(index, operador, radFilter, isBetweenLeftValue), out intfilterParsed) ? intfilterParsed : 0;
			}
			if (radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.DateTime"))
			{
				DateTime dateTimefilterParsed = DateTime.MinValue;
				filtro = DateTime.TryParse(this.GetFilter<System.DateTime>(index, operador, radFilter, isBetweenLeftValue), out dateTimefilterParsed) ? dateTimefilterParsed : DateTime.MinValue;
			}
			if (radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.Boolean"))
			{
				bool boolfilterParsed = false;
				filtro = bool.TryParse(this.GetFilter<bool>(index, operador, radFilter, isBetweenLeftValue), out boolfilterParsed) ? boolfilterParsed : false;
			}
			if (radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.String"))
				filtro = this.GetFilter<string>(index, operador, radFilter, isBetweenLeftValue);
			
			if (radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.Decimal"))
			{
				Decimal decimalfilterParsed = 0;
				filtro = Decimal.TryParse(this.GetFilter<decimal>(index, operador, radFilter, isBetweenLeftValue), out decimalfilterParsed) ? decimalfilterParsed : 0;
			}
			return filtro;
		}
		
		/// <summary>Metodo que obtiene el filtro de un elemento del RadFilter.</summary>
		/// <typeparam name="T">T. </typeparam>
		/// <param name="Index">Int. Posicion de la elemento en a lista.</param>
		/// <param name="Operator">String. Operacion que se evaluara en el filtro (Between, NotBetween, is null, is not null , etc.).</param>
		/// <param name="radFilter">RadFilter. Control que se extrae el filtro.</param>
		/// <param name="IsBetweenLeftValue">Bool. Indica si es IsBetweenLeftValue.</param>
		/// <returns>String. Filtro extraido del elemento.</returns>
		private string GetFilter<U>(int index, string operador, RadFilter radFilter, bool isBetweenLeftValue)
		{
			string filter = string.Empty;
			switch (typeof(U).Name)
			{
				case @"DateTime":
					switch (operador)
					{
						case @"Between":
							filter = (isBetweenLeftValue) ? string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue) : string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue);
							break;
						case @"NotBetween":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + operador + "'" + string.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue) + "'" + "AND" + "'" + string.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue) + "'" + " ";
							break;
						case @" is null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + operador;
							break;
						case @" is not null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + operador;
							break;
						default:
							filter = string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterSingleValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value);
							break;
					}
					break;
				case @"String":
					switch (operador)
					{
						case @" is null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + operador;
							break;
						case @" is not null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + operador;
							break;
						default:
							filter = ((RadFilterSingleValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value.ToString();
							break;
					}
					break;
				case @"Boolean":
					string valor = ((RadFilterSingleValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value.ToString();
					switch (valor)
					{
						case @"True":
							filter = @"True";
							break;
						case @"False":
							filter = @"False";
							break;
					}
					break;
				default:
					switch (operador)
					{
						case @"Between":
							filter = (isBetweenLeftValue) ? ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue.ToString() : ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue.ToString();
							break;
						case @"NotBetween":
							filter = (isBetweenLeftValue) ? ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue.ToString() : ((RadFilterDualValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue.ToString();
							break;
						case @" is null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + operador;
							break;
						case @" is not null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + operador;
							break;
						default:
							filter = ((RadFilterSingleValueExpression<U>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value.ToString();
							break;
					}
					break;
			}
			return filter;
		}
		
		#endregion
		
		#region Propiedades
		
		private Page pagina;
		
		private RadFilter radFilter;
		
		public RadGrid RadGrid { get; private set; }
		
		public TiposFiltro TipoFiltro { private get; set; }
		
		/// <summary>string. Variable que contiene el Id del Maestro.</summary>
		protected string idMaster;
		
		/// <summary>string. Variable que contiene la acción agregar encriptada.</summary>
		public string AccionAgregar
		{
			get
			{
				return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"agregar".Encriptar()));
			}
		}
		
		/// <summary>string. Variable que contiene la acción modificar encriptada.</summary>
		public string AccionModificar
		{
			get
			{
				return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"modificar".Encriptar()));
			}
		}
		
		/// <summary>string. Variable que contiene la acción ver encriptada.</summary>
		public string AccionVer
		{
			get
			{
				return HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"ver".Encriptar()));
			}
		}
		
		/// <summary>Propiedad que Obtiene o Asigna el Número de la pagina de la lista.</summary>
		public int NumeroPagina
		{
			get
			{
				int numero = 1;
				
				if (this.ViewState[@"Pagina"] != null)
					numero = int.Parse(this.ViewState[@"Pagina"].ToString());
				
				return numero;
			}
			set
			{
				this.ViewState[@"Pagina"] = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o asigna el tamaño de una pagina de la lista.</summary>
		public int TamanoPagina
		{
			get
			{
				int numero = 10;
				
				if (this.ViewState[@"TamanoPagina"] != null)
					numero = int.Parse(this.ViewState[@"TamanoPagina"].ToString());
				
				return numero;
			}
			set
			{
				this.ViewState[@"TamanoPagina"] = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o asigna el ordenamiento de la lista.</summary>
		public string Orden
		{
			get
			{
				return this.ViewState[@"Orden"].ToString();
			}
			set
			{
				this.ViewState[@"Orden"] = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o signa los parametros de filtrado de la pagina de la lista.</summary>
		public IList<HConnexum.Infraestructura.Filtro> ParametrosFiltroOriginal
		{
			get
			{
				if (this.ViewState[@"ParametrosFiltroOriginal"] == null)
					return new List<HConnexum.Infraestructura.Filtro>();
				
				return (IList<HConnexum.Infraestructura.Filtro>)this.ViewState[@"ParametrosFiltroOriginal"];
			}
			set
			{
				this.ViewState[@"ParametrosFiltroOriginal"] = value;
			}
		}
		
		/// <summary>Propiedad que obtiene o signa los parametros de filtrado de la pagina de la lista.</summary>
		public string ParametrosFiltro
		{
			get
			{
				if (this.ViewState[@"ParametrosFiltro"] == null)
					return string.Empty;
				
				return (string)this.ViewState[@"ParametrosFiltro"];
			}
			set
			{
				this.ViewState[@"ParametrosFiltro"] = value;
			}
		}
		
		public bool IndRolEliminado
		{
			get
			{
				if (this.UsuarioActual != null)
				{
					int idAplicacion = int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
					
					foreach (HConnexum.Seguridad.RolesUsuario rol in this.UsuarioActual.AplicacionActual(idAplicacion).Roles)
					{
						if (rol.NombreRol == ConfigurationManager.AppSettings[@"RolIndEliminado"].ToString())
							return true;
					}
				}
				return false;
			}
		}
		
		///<summary>Propiedad que asigna u obtiene el conjunto de registros desde el presentador.</summary>
		public IEnumerable Datos
		{
			set
			{
				this.RadGrid.DataSource = this.ObtenerDatatableConIdEncriptado(value);
			}
		}
		
		///<summary>Propiedad que asigna u obtiene el número de registros.</summary>
		public int NumeroDeRegistros
		{
			get
			{
				return this.RadGrid.VirtualItemCount;
			}
			set
			{
				this.RadGrid.VirtualItemCount = value;
			}
		}
		
		public IList<string> IdsEliminar
		{
			get
			{
				IList<string> regEliminado = new List<string>();
				
				foreach (GridDataItem item in this.RadGrid.Items)
				{
					if (item.Selected)
						regEliminado.Add(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(item.OwnerTableView.DataKeyValues[item.ItemIndex][@"IdEncriptado"].ToString())).Desencriptar());
				}
				return regEliminado;
			}
		}
		
		#endregion
	}
}