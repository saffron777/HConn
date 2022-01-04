using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Sitio.Modulos.Bloques;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio
{
	public class UserControlListaBase : UserControlBase
	{
		/// <summary>string. Variable que contiene el Id del Maestro.</summary>
		public string IdMaster;

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
			if(!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"LRSB"))
				this.Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"LRSB", this.ResolveClientUrl(@"~/Scripts/ListaRadScriptBlock1.js"));
			this.IdMaster = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(@"0".Encriptar()));
		}

		public ControlPagina DatosPagina { get; set; }

		/// <summary>Propiedad que Obtiene o Asigna el Número de la pagina de la lista.</summary>
		public int NumeroPagina
		{
			get
			{
				int numero = 1;
				if(this.ViewState[@"Pagina"] != null)
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
				if(this.ViewState[@"TamanoPagina"] != null)
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
				if(this.ViewState[@"Orden"] == null)
					this.ViewState[@"Orden"] = string.Empty;
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
				if(this.ViewState[@"ParametrosFiltro"] == null)
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
			string sFiltro = string.Empty;
			string compare = string.Empty;
			if(radFilter.RootGroup.Expressions.Count != 0)
			{
				for(int i = 0; i < radFilter.RootGroup.Expressions.Count; i++)
				{
					HConnexum.Infraestructura.Filtro filtro = new HConnexum.Infraestructura.Filtro();
					compare = radFilter.RootGroup.Expressions.ElementAt(i).FilterFunction.ToString();
					DateTime dateTimefilterParsed = DateTime.MinValue;
					switch(compare)
					{
						case @"Between":
							filtro.Operador = @"GreaterThanOrEqualTo";
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(sFiltro, i, compare, radFilter, true);
							pFiltro.Add(filtro);
							filtro.Operador = @"LessThan";
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(sFiltro, i, compare, radFilter, false);
							if(DateTime.TryParse(filtro.Valor.ToString(), out dateTimefilterParsed))
								filtro.Valor = dateTimefilterParsed.AddDays(1);
							pFiltro.Add(filtro);
							break;
						case @"EqualTo":
							if(radFilter.RootGroup.Expressions[i].GetType().FullName.ToString().Contains(@"System.DateTime"))
							{
								filtro.Operador = @"GreaterThanOrEqualTo";
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(sFiltro, i, compare, radFilter, true);
								pFiltro.Add(filtro);
								filtro.Operador = @"LessThan";
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(sFiltro, i, compare, radFilter, false);
								if(DateTime.TryParse(filtro.Valor.ToString(), out dateTimefilterParsed))
									filtro.Valor = dateTimefilterParsed.AddDays(1);
								pFiltro.Add(filtro);
							}
							else
							{
								filtro.Operador = compare;
								filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
								filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
								filtro.Valor = this.AsignarFiltro(sFiltro, i, compare, radFilter, true);
								pFiltro.Add(filtro);
							}
							break;
						default:
							filtro.Operador = compare;
							filtro.Tipo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldType;
							filtro.Campo = ((RadFilterNonGroupExpression)radFilter.RootGroup.Expressions[i]).FieldName;
							filtro.Valor = this.AsignarFiltro(sFiltro, i, compare, radFilter, true);
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
			if(radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.Int32"))
			{
				int intfilterParsed = 0;
				filtro = int.TryParse(this.GetFilter<int>(index, operador, radFilter, isBetweenLeftValue), out intfilterParsed) ? intfilterParsed : 0;
			}
			if(radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.DateTime"))
			{
				DateTime dateTimefilterParsed = DateTime.MinValue;
				filtro = DateTime.TryParse(this.GetFilter<System.DateTime>(index, operador, radFilter, isBetweenLeftValue), out dateTimefilterParsed) ? dateTimefilterParsed : DateTime.MinValue;
			}
			if(radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.Boolean"))
			{
				bool boolfilterParsed = false;
				filtro = bool.TryParse(this.GetFilter<bool>(index, operador, radFilter, isBetweenLeftValue), out boolfilterParsed) ? boolfilterParsed : false;
			}
			if(radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.String"))
				filtro = this.GetFilter<string>(index, operador, radFilter, isBetweenLeftValue);
			if(radFilter.RootGroup.Expressions[index].GetType().FullName.ToString().Contains(@"System.Decimal"))
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
		private string GetFilter<T>(int index, string @operator, RadFilter radFilter, bool isBetweenLeftValue)
		{
			string filter = string.Empty;
			switch(typeof(T).Name)
			{
				case @"DateTime":
					switch(@operator)
					{
						case @"Between":
							filter = (isBetweenLeftValue) ? string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue) : string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue);
							break;
						case @"NotBetween":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + @operator + "'" + string.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue) + "'" + " AND " + "'" + string.Format(CultureInfo.InvariantCulture, "{0:MM/dd/yyyy}", ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue) + "'" + "  ";
							break;
						case @" is null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + @operator;
							break;
						case @" is not null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + @operator;
							break;
						default:
							filter = string.Format(CultureInfo.InvariantCulture, "{0:dd/MM/yyyy}", ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value);
							break;
					}
					break;
				case @"String":
					switch(@operator)
					{
						case " is null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + @operator;
							break;
						case " is not null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + @operator;
							break;
						default:
							filter = ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value.ToString();
							break;
					}
					break;
				case @"Boolean":
					string valor = ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value.ToString();
					switch(valor)
					{
						case "True":
							filter = @"True";
							break;
						case "False":
							filter = @"False";
							break;
					}
					break;
				default:
					switch(@operator)
					{
						case @"Between":
							filter = (isBetweenLeftValue) ? ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue.ToString() : ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue.ToString();
							break;
						case @"NotBetween":
							filter = (isBetweenLeftValue) ? ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).LeftValue.ToString() : ((RadFilterDualValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).RightValue.ToString();
							break;
						case @" is null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + @operator;
							break;
						case @" is not null ":
							filter = ((RadFilterNonGroupExpression)(radFilter.RootGroup.Expressions.ElementAt(index))).FieldName + @operator;
							break;
						default:
							filter = ((RadFilterSingleValueExpression<T>)(radFilter.RootGroup.Expressions.ElementAt(index))).Value.ToString();
							break;
					}
					break;
			}
			return filter;
		}
	}
}