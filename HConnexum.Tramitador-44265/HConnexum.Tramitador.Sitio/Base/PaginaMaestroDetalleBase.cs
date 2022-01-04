using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio
{
	public class PaginaMaestroDetalleBase : PaginaBase
	{
		/// <summary>string. Variable que contiene el Id del Maestro.</summary>
		public string idMaster;

		/// <summary>string. Variable que contiene la acción agregar encriptada.</summary>
		public string AccionAgregar = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"agregar".Encriptar()));

		/// <summary>string. Variable que contiene la acción modificar encriptada.</summary>
		public string AccionModificar = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"modificar".Encriptar()));

		/// <summary>string. Variable que contiene la acción ver encriptada.</summary>
		public string AccionVer = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"ver".Encriptar()));

		/// <summary>Evento de inicialización de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_Init(object sender, EventArgs e)
		{
			if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"LRSB"))
				this.ClientScript.RegisterClientScriptInclude(typeof(string), @"LRSB", this.ResolveClientUrl(@"~/Scripts/ListaRadScriptBlock1.js"));
			if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"DRSB"))
				this.ClientScript.RegisterClientScriptInclude(typeof(string), @"DRSB", this.ResolveClientUrl(@"~/Scripts/DetalleRadScriptBlock1.js"));
			base.Page_Init(sender, e);
			this.idMaster = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(this.Id.ToString().Encriptar()));
		}

		/// <summary>Evento previo al renderizado de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
		}

		#region "Propiedades"
		/// <summary>Variable que contiene la acción ejecutada sobre una pagina.</summary>
		AccionDetalle accion = AccionDetalle.NoEstablecida;

		/// <summary>Propiedad que obtiene o asigna la accion.</summary>
		protected AccionDetalle Accion
		{
			get
			{
				if (this.accion == AccionDetalle.NoEstablecida)
				{
					string sAccion = this.ExtraerDeViewStateOQueryString("accion");
					switch (sAccion.ToLower())
					{
						case @"agregar":
							this.accion = AccionDetalle.Agregar;
							break;
						case @"modificar":
							this.accion = AccionDetalle.Modificar;
							break;
						case @"ver":
							this.accion = AccionDetalle.Ver;
							break;
						default:
							this.accion = AccionDetalle.NoEstablecida;
							break;
					}
				}
				return this.accion;
			}
			set
			{
				switch (value)
				{
					case AccionDetalle.Agregar:
						this.ViewState[@"accion"] = @"agregar";
						break;
					case AccionDetalle.Modificar:
						this.ViewState[@"accion"] = @"modificar";
						break;
					case AccionDetalle.Ver:
						this.ViewState[@"accion"] = @"ver";
						break;
					default:
						this.ViewState[@"accion"] = @"ver";
						break;
				}
				this.accion = value;
			}
		}

		/// <summary>Variable que contiene el Identificador de una entidad.</summary>
		int id = 0;

		/// <summary>Propiedad que obtiene o asigna el Id.</summary>
		protected int Id
		{
			get
			{
				if (this.id == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString(@"id");
					if (!string.IsNullOrEmpty(sId))
						this.id = int.Parse(sId);
				}
				return this.id;
			}
			set
			{
				this.id = value;
				this.ViewState[@"id"] = this.id;
			}
		}
		#endregion "Propiedades"

		#region "Metodos comunes"
		/// <summary>Metodo que permite visualar los botones o ocultarlos dependiendo de la accion.</summary>
		/// <param name="guardar">Button. Boton Guardar.</param>
		/// <param name="agregarOtro">Button. Boton AgregarOtro.</param>
		/// <param name="limpiar">Button. Boton Limpiar. </param>
		/// <param name="accion">AccionDetalle. Accion ejecutada sobre la pagina.</param>
		protected void MostrarBotones(Button guardar, Button agregarOtro, Button limpiar, AccionDetalle accion)
		{
			switch (accion)
			{
				case AccionDetalle.Agregar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = agregarOtro.Visible == false ? false : true;
					limpiar.Visible = limpiar.Visible == false ? false : true;
					break;
				case AccionDetalle.Modificar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				case AccionDetalle.Ver:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				default:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
			}
		}
		/// <summary>Metodo que permite visualar los botones o ocultarlos dependiendo de la accion.</summary>
		/// <param name="guardar">Button. Boton Guardar.</param>
		/// <param name="agregarOtro">Button. Boton AgregarOtro.</param>
		/// <param name="limpiar">Button. Boton Limpiar. </param>
		/// <param name="accion">AccionDetalle. Accion ejecutada sobre la pagina.</param>
		protected void MostrarBotones(RadButton guardar, RadButton agregarOtro, RadButton limpiar, AccionDetalle accion)
		{
			switch (accion)
			{
				case AccionDetalle.Agregar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = agregarOtro.Visible == false ? false : true;
					limpiar.Visible = limpiar.Visible == false ? false : true;
					break;
				case AccionDetalle.Modificar:
					guardar.Visible = guardar.Visible == false ? false : true;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				case AccionDetalle.Ver:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
				default:
					guardar.Visible = false;
					agregarOtro.Visible = false;
					limpiar.Visible = false;
					break;
			}
		}
		#endregion "Metodos comunes"

		public ControlPagina DatosPagina { get; set; }

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
		public IList<HConnexum.Infraestructura.Filtro> ParametrosFiltro
		{
			get
			{
				if (this.ViewState[@"ParametrosFiltro"] == null)
					return new List<HConnexum.Infraestructura.Filtro>();
				return (IList<HConnexum.Infraestructura.Filtro>)this.ViewState[@"ParametrosFiltro"];
			}
			set
			{
				this.ViewState[@"ParametrosFiltro"] = value;
			}
		}

		/// <summary>Metodo que extrae del control los filtros de una lista.</summary>
		/// <param name="radFilter">RadFilter. Control que contiene los filtros.</param>
		/// <returns>IList Filtro. Listado de Filtros.</returns>
		public IList<HConnexum.Infraestructura.Filtro> ExtraerParametrosFiltro(RadFilter radFilter)
		{
			IList<HConnexum.Infraestructura.Filtro> pFiltro = new List<HConnexum.Infraestructura.Filtro>();
			string Filtro = string.Empty;
			string Compare = string.Empty;
			if (radFilter.RootGroup.Expressions.Count != 0)
			{
				for (int i = 0; i < radFilter.RootGroup.Expressions.Count; i++)
				{
					HConnexum.Infraestructura.Filtro filtro = new HConnexum.Infraestructura.Filtro();
					Compare = radFilter.RootGroup.Expressions.ElementAt(i).FilterFunction.ToString();
					DateTime DateTimefilterParsed = DateTime.MinValue;
					switch (Compare)
					{
						case @"Between":
							filtro.Operador = @"GreaterThanOrEqualTo";
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(Filtro, i, Compare, radFilter, true);
							pFiltro.Add(filtro);
							filtro.Operador = @"LessThan";
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(Filtro, i, Compare, radFilter, false);
							if (DateTime.TryParse(filtro.Valor.ToString(), out DateTimefilterParsed))
								filtro.Valor = DateTimefilterParsed.AddDays(1);
							pFiltro.Add(filtro);
							break;
						case @"EqualTo":
							if (radFilter.RootGroup.Expressions[i].GetType().FullName.ToString().Contains(@"System.DateTime"))
							{
								filtro.Operador = @"GreaterThanOrEqualTo";
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(Filtro, i, Compare, radFilter, true);
								pFiltro.Add(filtro);
								filtro.Operador = @"LessThan";
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(Filtro, i, Compare, radFilter, false);
								if (DateTime.TryParse(filtro.Valor.ToString(), out DateTimefilterParsed))
									filtro.Valor = DateTimefilterParsed.AddDays(1);
								pFiltro.Add(filtro);
							}
							else
							{
								filtro.Operador = Compare;
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(Filtro, i, Compare, radFilter, true);
								pFiltro.Add(filtro);
							}
							break;
						default:
							filtro.Operador = Compare;
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(Filtro, i, Compare, radFilter, true);
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
				DateTime DateTimefilterParsed = DateTime.MinValue;
				filtro = DateTime.TryParse(this.GetFilter<System.DateTime>(index, operador, radFilter, isBetweenLeftValue), out DateTimefilterParsed) ? DateTimefilterParsed : DateTime.MinValue;
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
				Decimal DecimalfilterParsed = 0;
				filtro = Decimal.TryParse(this.GetFilter<decimal>(index, operador, radFilter, isBetweenLeftValue), out DecimalfilterParsed) ? DecimalfilterParsed : 0;
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
		private string GetFilter<T>(int Index, string Operator, RadFilter radFilter, bool IsBetweenLeftValue)
		{
			string Filter = string.Empty;
			switch (typeof(T).Name)
			{
				case @"DateTime":
					switch (Operator)
					{
						case @"Between":
							Filter = (IsBetweenLeftValue) ? string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).LeftValue) : string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).RightValue);
							break;
						case @"NotBetween":
							Filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(Index))).FieldName + Operator + "'" + string.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).LeftValue) + "'" + " AND " + "'" + string.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).RightValue) + "'" + "  ";
							break;
						case @" is null ":
							Filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(Index))).FieldName + Operator;
							break;
						case @" is not null ":
							Filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(Index))).FieldName + Operator;
							break;
						default:
							Filter = string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).Value);
							break;
					}
					break;
				case @"String":
					switch (Operator)
					{
						case " is null ":
							Filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(Index))).FieldName + Operator;
							break;
						case " is not null ":
							Filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(Index))).FieldName + Operator;
							break;
						default:
							Filter = ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).Value.ToString();
							break;
					}
					break;
				case @"Boolean":
					string Valor = ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).Value.ToString();
					switch (Valor)
					{
						case "True":
							Filter = @"True";
							break;
						case "False":
							Filter = @"False";
							break;
					}
					break;
				default:
					switch (Operator)
					{
						case @"Between":
							Filter = (IsBetweenLeftValue) ? ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).LeftValue.ToString() : ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).RightValue.ToString();
							break;
						case @"NotBetween":
							Filter = (IsBetweenLeftValue) ? ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).LeftValue.ToString() : ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).RightValue.ToString();
							break;
						case @" is null ":
							Filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(Index))).FieldName + Operator;
							break;
						case @" is not null ":
							Filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(Index))).FieldName + Operator;
							break;
						default:
							Filter = ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(Index))).Value.ToString();
							break;
					}
					break;
			}
			return Filter;
		}
	}
}