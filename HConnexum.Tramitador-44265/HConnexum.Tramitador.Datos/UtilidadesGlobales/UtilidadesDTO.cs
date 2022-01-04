using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Seguridad;

namespace HConnexum.Tramitador.Datos
{
	public static class UtilidadesDTO<T>
	{
		/// <summary>Filtra una colección para los Grid (Paginación y candidad de registros).</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="pagina">int. Pagina que se listara.</param>
		/// <param name="registros">int. Cantidad de registros por paginas.</param>
		/// <param name="parametrosFiltro">IList. Listado de filtros.</param>
		/// <returns>IQueryable. Colección de registros para un Grid.</returns>
		public static IQueryable<T> FiltrarPaginar(IQueryable<T> coleccion, int pagina, int registros, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro)
		{
			Expression<Func<T, bool>> ExpresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(ExpresionFiltro.Compile());
			Conteo = coltemp.Count();
			coltemp = coltemp.Skip((pagina - 1) * registros).Take(registros);
			return IndicadorTomado(coltemp.AsQueryable());
		}

		/// <summary>Encripta el Id y lo agrega en la propiedad EncriptarId en los registros de una colección.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <returns>IList. Listado de registros con el Id encriptado.</returns>
		public static IQueryable<T> IndicadorTomado(IQueryable<T> coleccion)
		{
			IList<T> tempColeccion = coleccion.ToList();
			PropertyInfo propInfoTomado = typeof(T).GetProperty(@"Tomado");
			if(propInfoTomado != null)
			{
				PropertyInfo propInfoUsuarioTomado = typeof(T).GetProperty(@"UsuarioTomado");
				PropertyInfo propInfoId = typeof(T).GetProperty(@"Id");
				IUnidadDeTrabajo udt = new UnidadDeTrabajo();
				TomadoRepositorio repositorioTomado = new TomadoRepositorio(udt);
				IList<int> Ids = new List<int>();
				foreach(T item in tempColeccion)
					Ids.Add(int.Parse(propInfoId.GetValue(item, null).ToString()));
				IEnumerable<TomadoDTO> tomados = repositorioTomado.ObtenerRegistroTomadoDTO(typeof(T).Name.Substring(0, typeof(T).Name.Length - 3).Pluralizar(), Ids);
				foreach(T item in tempColeccion)
				{
					propInfoTomado.SetValue(item, @"L", null);
					foreach(TomadoDTO tomado in tomados)
					{
						if(tomado.IdRegistro == int.Parse(propInfoId.GetValue(item, null).ToString()))
						{
							if(tomado.IdSesionUsuario == UsuarioActual.IdSesion)
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
			Expression<Func<T, bool>> ExpresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(ExpresionFiltro.Compile());
			return coltemp.AsQueryable();
		}

		#region "Filtrado con AplicaRol"
		/// <summary>Filtra una colección por el IndEliminado = false</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>IQueryable. Colección de registros filtrados por el IndEliminado.</returns>
		public static IQueryable<T> FiltrarColeccionEliminacion(IQueryable<T> coleccion, bool AplicaRol)
		{
			IList<HConnexum.Infraestructura.Filtro> parametrosFiltro = new List<HConnexum.Infraestructura.Filtro>();
			if(!AplicaRol || !IndEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			Expression<Func<T, bool>> ExpresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(ExpresionFiltro.Compile());
			return coltemp.AsQueryable();
		}

		/// <summary>Filtra una colección de registros agregando a parametrosFiltro el IndEliminado.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="parametrosFiltro">IList. Listado de filtros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>IQueryable. Colección de registros filtrados agregando el IndEliminado.</returns>
		public static IQueryable<T> FiltrarColeccionEliminacion(IQueryable<T> coleccion, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, bool AplicaRol)
		{
			if(!AplicaRol || !IndEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			Expression<Func<T, bool>> ExpresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(ExpresionFiltro.Compile());
			return coltemp.AsQueryable();
		}

		/// <summary>Filtra una colección de registros por los datos de auditoria y eliminación.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>IQueryable. Colección de registros filtrados por los datos de auditoria y eliminación.</returns>
		public static IQueryable<T> FiltrarColeccionAuditoria(IQueryable<T> coleccion, bool AplicaRol)
		{
			IList<HConnexum.Infraestructura.Filtro> parametrosFiltro = new List<HConnexum.Infraestructura.Filtro>();
			if(!AplicaRol || !IndEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndVigente", Operador = @"EqualTo", Tipo = typeof(bool), Valor = true });
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"FechaValidez", Operador = @"LessThanOrEqualTo", Tipo = typeof(DateTime), Valor = DateTime.Now });
			Expression<Func<T, bool>> ExpresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(ExpresionFiltro.Compile());
			return coltemp.AsQueryable();
		}

		/// <summary>Filtra una colección de registros agregando a parametrosFiltro los datos de auditoria y eliminación.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="parametrosFiltro">IList. Listado de filtros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>>IQueryable. Colección de registros filtrados agregando los datos de auditoria y eliminación.</returns>
		public static IQueryable<T> FiltrarColeccionAuditoria(IQueryable<T> coleccion, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, bool AplicaRol)
		{
			if(!AplicaRol || !IndEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndVigente", Operador = @"EqualTo", Tipo = typeof(bool), Valor = true });
			parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"FechaValidez", Operador = @"LessThanOrEqualTo", Tipo = typeof(DateTime), Valor = DateTime.Now });
			Expression<Func<T, bool>> ExpresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			IEnumerable<T> coltemp = coleccion.Where(ExpresionFiltro.Compile());
			return coltemp.AsQueryable();
		}
		#endregion

		/// <summary>Encripta el Id y lo agrega en la propiedad EncriptarId en los registros de una colección.</summary>
		/// <param name="coleccion">IQueryable. Colección de registros.</param>
		/// <param name="AplicaRol">Bool. Variable que indica si se aplicaca el filtrado de IndEliminado por el Rol del Usuario.</param>
		/// <returns>IList. Listado de registros con el Id encriptado.</returns>
		public static IList<T> EncriptarId(IQueryable<T> coleccion)
		{
			IList<T> tempColeccion = coleccion.ToList();
			PropertyInfo propInfoEncriptado = typeof(T).GetProperty(@"IdEncriptado");
			PropertyInfo propInfoId = typeof(T).GetProperty(@"Id");
			if(propInfoEncriptado != null)
				for(int i = 0; i < tempColeccion.Count; i++)
					propInfoEncriptado.SetValue(tempColeccion[i], HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(propInfoId.GetValue(tempColeccion[i], null).ToString().Encriptar())), null);
			return tempColeccion;
		}

		public static int Conteo { get; set; }

		public static bool IndEliminado
		{
			get
			{
				if(UsuarioActual != null)
				{
					int IdAplicacion = int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]);
					foreach(HConnexum.Seguridad.RolesUsuario Rol in UsuarioActual.AplicacionActual(IdAplicacion).Roles)
						if(Rol.NombreRol == ConfigurationManager.AppSettings[@"RolIndEliminado"].ToString())
							return true;
				}
				return false;
			}
		}

		public static UsuarioActual UsuarioActual
		{
			get
			{
				if(HttpContext.Current.Session[@"UsuarioActual"] != null)
					return (UsuarioActual)HttpContext.Current.Session[@"UsuarioActual"];
				return null;
			}
		}
	}
}