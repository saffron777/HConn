using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Seguridad;
using System.Configuration;

namespace HConnexum.Tramitador.Datos
{
	public abstract class RepositorioBase<T> : IRepositorio<T> where T : EntityObject
	{
		protected readonly IUnidadDeTrabajo udt;

		public RepositorioBase(IUnidadDeTrabajo udt)
		{
			this.udt = udt;
		}

		#region propiedades
		private int conteo = 0;

		public int Conteo
		{
			get
			{
				return this.conteo;
			}
			set
			{
				this.conteo = value;
			}
		}

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

		#endregion propiedades

		public void Agregar(T entidad)
		{
			this.udt.MarcarNuevo(entidad);
		}

		public void Eliminar(T entidad)
		{
			this.udt.MarcarEliminado(entidad);
		}

		public void EliminarLogico(T entidad)
		{
			PropertyInfo propinfo = typeof(T).GetProperty(@"IndEliminado");
			propinfo.SetValue(entidad, true, null);
			this.udt.MarcarModificado(entidad);
		}

		public void activarEliminarLogico(T entidad)
		{
			PropertyInfo propinfo = typeof(T).GetProperty(@"IndEliminado");
			propinfo.SetValue(entidad, false, null);
			this.udt.MarcarModificado(entidad);
		}

		public bool Eliminar(T entidad, int idRegistro)
		{
			BD_HC_Tramitador dataBase = new BD_HC_Tramitador();
			if(dataBase.VerificacionEliminacion(typeof(T).Name.Pluralizar(), idRegistro).Single().Value)
				this.udt.MarcarEliminado(entidad);
			else
				return false;
			return true;
		}

		public bool EliminarLogico(T entidad, int idRegistro)
		{
			BD_HC_Tramitador dataBase = new BD_HC_Tramitador();
			if(dataBase.VerificacionEliminacion(typeof(T).Name.Pluralizar(), idRegistro).Single().Value)
			{
				PropertyInfo propinfo = typeof(T).GetProperty(@"IndEliminado");
				propinfo.SetValue(entidad, true, null);
				this.udt.MarcarModificado(entidad);
			}
			else
				return false;
			return true;
		}

		public bool activarEliminarLogico(T entidad, int idRegistro)
		{
			BD_HC_Tramitador dataBase = new BD_HC_Tramitador();
			if(dataBase.VerificacionEliminacion(typeof(T).Name.Pluralizar(), idRegistro).Single().Value)
			{
				PropertyInfo propinfo = typeof(T).GetProperty(@"IndEliminado");
				propinfo.SetValue(entidad, false, null);
				this.udt.MarcarModificado(entidad);
			}
			else
				return false;
			return true;
		}

		public void Actualizar(T entidad)
		{
			this.udt.MarcarModificado(entidad);
		}

		public IList<T> ObtenerTodos()
		{
			return this.udt.Sesion.CreateObjectSet<T>().ToList();
		}

		public T ObtenerPorId(int id)
		{
			var nombreCualificado = string.Format("{0}.{1}", this.udt.Sesion.DefaultContainerName, typeof(T).Name).Pluralizar();
			EntityKey clave = new EntityKey(nombreCualificado, @"Id", id);
			object value = null;
			this.udt.Sesion.TryGetObjectByKey(clave, out value);
			return value as T;
		}

		public IList<T> ObtenerPaginado(string orden, int pagina, int registros)
		{
			return this.ObtenerFiltrado(orden, pagina, registros, new List<HConnexum.Infraestructura.Filtro>());
		}

		public bool obtenerRolIndEliminado()
		{
			return IndEliminado;
		}

		public IList<T> ObtenerFiltrado(string orden, int pagina, int registros, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro)
		{
			if(!IndEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });

			IEnumerable<T> coleccion = this.udt.Sesion.CreateObjectSet<T>().OrderBy(@"it." + orden);
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			coleccion = coleccion.Where(expresionFiltro.Compile());
			this.Conteo = coleccion.Count();
			coleccion = coleccion.Skip((pagina - 1) * registros).Take(registros);
			IList<T> tempColeccion = coleccion.ToList();
			PropertyInfo propInfoEncriptado = typeof(T).GetProperty(@"IdEncriptado");
			PropertyInfo propInfoId = typeof(T).GetProperty(@"Id");
			IList<int> ids = new List<int>();
			if(propInfoEncriptado != null)
				for(int i = 0; i < tempColeccion.Count; i++)
				{
					propInfoEncriptado.SetValue(tempColeccion[i], HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(propInfoId.GetValue(tempColeccion[i], null).ToString().Encriptar())), null);
					ids.Add(int.Parse(propInfoId.GetValue(tempColeccion[i], null).ToString()));
				}
			PropertyInfo propInfoTomado = typeof(T).GetProperty(@"Tomado");
			if(propInfoTomado != null)
			{
				PropertyInfo propInfoUsuarioTomado = typeof(T).GetProperty(@"UsuarioTomado");
				TomadoRepositorio repositorioTomado = new TomadoRepositorio(this.udt);
				IEnumerable<TomadoDTO> tomados = repositorioTomado.ObtenerRegistroTomadoDTO(typeof(T).Name.Pluralizar(), ids);
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
			return coleccion.ToList();
		}
	}
}