using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HConnexum.Base.Dtos.EntidadesGlobales;

namespace HConnexum.Base.Datos
{
	public static class UtilidadesDto<T>
	{
		public static int Conteo { get; set; }
		
		/// <summary>Filtra una colección para los Grid (Paginación y candidad de registros).</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="pagina">int. Pagina que se listara.</param>
		/// <param name="registros">int. Cantidad de registros por paginas.</param>
		/// <param name="parametrosFiltro">IList. Listado de filtros.</param>
		/// <returns>IQueryable. Colección de registros para un Grid.</returns>
		public static IQueryable<T> FiltrarPaginar(IQueryable<T> coleccion, int pagina, int registros, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, IUnidadDeTrabajo udt)
		{
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(expresionFiltro.Compile());
			Conteo = coltemp.Count();
			coltemp = coltemp.Skip((pagina - 1) * registros).Take(registros);
			return IndicadorTomado(coltemp.AsQueryable(), udt);
		}
		
		/// <summary>Encripta el Id y lo agrega en la propiedad EncriptarId en los registros de una colección.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <returns>IList. Listado de registros con el Id encriptado.</returns>
		public static IQueryable<T> IndicadorTomado(IQueryable<T> coleccion, IUnidadDeTrabajo udt)
		{
			IList<T> tempColeccion = coleccion.ToList();
			PropertyInfo propInfoTomado = typeof(T).GetProperty(@"Tomado");
			
			if (propInfoTomado != null)
			{
				PropertyInfo propInfoUsuarioTomado = typeof(T).GetProperty(@"UsuarioTomado");
				PropertyInfo propInfoId = typeof(T).GetProperty(@"Id");
				TomadoRepositorio repositorioTomado = new TomadoRepositorio(udt);
				IList<int> ids = new List<int>();
				
				foreach (T item in tempColeccion)
				{
					ids.Add(int.Parse(propInfoId.GetValue(item, null).ToString()));
				}
				
				IList<TomadoDto> tomados = repositorioTomado.ObtenerRegistroTomadoDto(typeof(T).Name.Substring(0, typeof(T).Name.Length - 3), ids);
				
				foreach (T item in tempColeccion)
				{
					propInfoTomado.SetValue(item, @"L", null);
					
					foreach (TomadoDto tomado in tomados)
					{
						if (tomado.IdRegistro == int.Parse(propInfoId.GetValue(item, null).ToString()))
						{
							if (tomado.IdSesionUsuario == udt.IdSesion)
								propInfoTomado.SetValue(item, @"TP", null);
							else
								propInfoTomado.SetValue(item, @"T", null);
							
							propInfoUsuarioTomado.SetValue(item, tomado.NombreUsuario, null);
						}
					}
				}
			}
			return tempColeccion.AsQueryable();
		}
		
		/// <summary>Filtra una colección a través de una lista de filtros.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="parametrosFiltro">IList. Listado de filtros.</param>
		/// <returns>IQueryable. Colección de registros filtrados.</returns>
		public static IQueryable<T> FiltrarColeccion(IQueryable<T> coleccion, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro)
		{
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(expresionFiltro.Compile());
			return coltemp.AsQueryable();
		}
		
		#region "Filtrado con AplicaRol"
		
		/// <summary>Filtra una colección por el IndEliminado = false</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>IQueryable. Colección de registros filtrados por el IndEliminado.</returns>
		public static IQueryable<T> FiltrarColeccionEliminacion(IQueryable<T> coleccion, bool aplicaRol, bool indEliminado)
		{
			IList<HConnexum.Infraestructura.Filtro> parametrosFiltro = new List<HConnexum.Infraestructura.Filtro>();
			
			if (!aplicaRol || !indEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(expresionFiltro.Compile());
			return coltemp.AsQueryable();
		}
		
		/// <summary>Filtra una colección de registros agregando a parametrosFiltro el IndEliminado.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="parametrosFiltro">IList. Listado de filtros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>IQueryable. Colección de registros filtrados agregando el IndEliminado.</returns>
		public static IQueryable<T> FiltrarColeccionEliminacion(IQueryable<T> coleccion, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, bool aplicaRol, bool indEliminado)
		{
			if (!aplicaRol || !indEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(expresionFiltro.Compile());
			return coltemp.AsQueryable();
		}
		
		/// <summary>Filtra una colección de registros por los datos de auditoria y eliminación.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>IQueryable. Colección de registros filtrados por los datos de auditoria y eliminación.</returns>
		public static IQueryable<T> FiltrarColeccionAuditoria(IQueryable<T> coleccion, bool aplicaRol)
		{
			IList<HConnexum.Infraestructura.Filtro> parametrosFiltro = new List<HConnexum.Infraestructura.Filtro>();
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndVigente", Operador = @"EqualTo", Tipo = typeof(bool), Valor = true });
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"FechaValidez", Operador = @"LessThanOrEqualTo", Tipo = typeof(DateTime), Valor = DateTime.Now });
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(expresionFiltro.Compile());
			return coltemp.AsQueryable();
		}
		
		/// <summary>Filtra una colección de registros agregando a parametrosFiltro los datos de auditoria y eliminación.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="parametrosFiltro">IList. Listado de filtros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>>IQueryable. Colección de registros filtrados agregando los datos de auditoria y eliminación.</returns>
		public static IQueryable<T> FiltrarColeccionAuditoria(IQueryable<T> coleccion, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, bool aplicaRol, bool indEliminado)
		{
			if (!aplicaRol || !indEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndVigente", Operador = @"EqualTo", Tipo = typeof(bool), Valor = true });
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"FechaValidez", Operador = @"LessThanOrEqualTo", Tipo = typeof(DateTime), Valor = DateTime.Now });
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(expresionFiltro.Compile());
			return coltemp.AsQueryable();
		}
		
		#endregion
	}
}