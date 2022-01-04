using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Datos
{
	public static class UtilidadesExpresiones
	{
		public static Expression<Func<T, bool>> CrearExpresion<T>(string campo, Type tipo, string operador, object valor)
		{
			Expression<Func<T, bool>> lambda = null;
			
			if (tipo == typeof(string) && operador.Equals(@"EqualTo"))
			{
				var param = Expression.Parameter(typeof(T), @"x");
				var body = Expression.Call(ObtenerMethod(), Expression.PropertyOrField(param, campo), Expression.Constant(valor, typeof(string)));
				lambda = Expression.Lambda<Func<T, bool>>(body, param);
			}
			else
			{
				ParameterExpression e = Expression.Parameter(typeof(T), @"e");
				PropertyInfo propinfo = typeof(T).GetProperty(campo);
				MemberExpression m = Expression.MakeMemberAccess(e, propinfo);
				ConstantExpression c = Expression.Constant(valor, (m.Type.Name.Contains(@"Nullable")) ? m.Type : tipo);
				BinaryExpression b = AplicarOperador(m, c, operador);
				lambda = Expression.Lambda<Func<T, bool>>(b, e);
			}
			return lambda;
		}
		
		public static Expression<Func<T, bool>> ProcesarExpresiones<T>(IList<HConnexum.Infraestructura.Filtro> parametrosFiltro)
		{
			Expression<Func<T, bool>> predicate = PredicateBuilder.True<T>();
			
			foreach (Filtro filtro in parametrosFiltro)
			{
				Expression<Func<T, bool>> expresion = CrearExpresion<T>(filtro.Campo, filtro.Tipo, filtro.Operador, filtro.Valor);
				
				if (predicate == null)
					predicate = PredicateBuilder.Create<T>(expresion);
				else
					predicate = predicate.And<T>(expresion);
			}
			return predicate;
		}
		
		private static BinaryExpression AplicarOperador(MemberExpression memberExpression, ConstantExpression constantExpression, string operador)
		{
			switch(operador)
			{
				case @"EqualTo":
					return Expression.Equal(memberExpression, constantExpression);
				case @"NotEqualTo":
					return Expression.NotEqual(memberExpression, constantExpression);
				case @"GreaterThan":
					return Expression.GreaterThan(memberExpression, constantExpression);
				case @"GreaterThanOrEqualTo":
					return Expression.GreaterThanOrEqual(memberExpression, constantExpression);
				case @"LessThan":
					return Expression.LessThan(memberExpression, constantExpression);
				case @"LessThanOrEqualTo":
					return Expression.LessThanOrEqual(memberExpression, constantExpression);
				case @"IsEmpty":
					return Expression.Equal(memberExpression, constantExpression);
				case @"NotIsEmpty":
					return Expression.NotEqual(memberExpression, constantExpression);
				case @"IsNull":
					return Expression.Equal(memberExpression, constantExpression);
				case @"NotIsNull":
					return Expression.NotEqual(memberExpression, constantExpression);
				default:
					return null;
			}
		}
		
		private static MethodInfo ObtenerMethod()
		{
			return typeof(UtilidadesExpresiones).GetMethod(@"Like", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
		}
	}
}