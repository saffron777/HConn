using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: TomadoRepositorio.</summary>
	public sealed class TomadoRepositorio : RepositorioBase<Tomado>
	{
		#region "Constructores"
		///<summary>Constructor de la clase TomadoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public TomadoRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}
		#endregion "Constructores"

		#region DTO
		///<summary>Obtiene los datos de un directorio individual.</summary>
		///<param name="orden">Parámetro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro página en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Detalle del directorio.</returns>
		public IEnumerable<TomadoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, int idUsuario)
		{
			var tabTomado = this.udt.Sesion.CreateObjectSet<Tomado>();
			var tabPaginasModulo = this.udt.Sesion.CreateObjectSet<PaginasModulo>();
			var tabSesionesUsuario = this.udt.Sesion.CreateObjectSet<SesionesUsuario>();
			var tabUsuario = this.udt.Sesion.CreateObjectSet<Usuario>();
			var tabUsuariosSucriptor = this.udt.Sesion.CreateObjectSet<UsuariosSucriptor>();
			var coleccion = from t in tabTomado
							join pm in tabPaginasModulo on
							t.IdPaginaModulo equals pm.Id
							join su in tabSesionesUsuario on
							t.IdSesionUsuario equals su.Id
							join u in tabUsuario on
							su.IdUsuario equals u.Id
							join us in tabUsuariosSucriptor on
							u.Id equals us.IdUsuario
							where us.IdJefeInmediato == idUsuario ||
								  us.IdUsuario == idUsuario
							select new TomadoDTO
							{
								Id = t.Id,
								NombrePagina = pm.NombrePagina,
								Tabla = t.Tabla,
								IdSesionUsuario = t.IdSesionUsuario,
								IdRegistro = t.IdRegistro,
								FechaTomado = t.FechaTomado,
								LoginUsuario = u.LoginUsuario,
								IdSuscriptor = us.IdSuscriptor
							};
			coleccion = UtilidadesDTO<TomadoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			this.Conteo = UtilidadesDTO<TomadoDTO>.Conteo;
			return UtilidadesDTO<TomadoDTO>.EncriptarId(coleccion);
		}

		public IEnumerable<TomadoDTO> ObtenerRegistroTomadoDTO(string tabla, IList<int> ids)
		{
			var tabTomado = this.udt.Sesion.CreateObjectSet<Tomado>();
			var tabSesionesUsuario = this.udt.Sesion.CreateObjectSet<SesionesUsuario>();
			var tabUsuario = this.udt.Sesion.CreateObjectSet<Usuario>();
			var coleccion = from t in tabTomado
							join su in tabSesionesUsuario on
							t.IdSesionUsuario equals su.Id
							join u in tabUsuario on
							su.IdUsuario equals u.Id
							where t.Tabla == tabla &&
								  ids.Contains(t.IdRegistro)
							select new TomadoDTO
							{
								Id = t.Id,
								IdPaginaModulo = t.IdPaginaModulo,
								Tabla = t.Tabla,
								IdRegistro = t.IdRegistro,
								IdSesionUsuario = t.IdSesionUsuario,
								FechaTomado = t.FechaTomado,
								NombreUsuario = u.LoginUsuario
							};
			return coleccion;
		}

		public Tomado ObtenerTomado(string tabla, int idRegistro, int idSesionUsuario)
		{
			var tabTomado = this.udt.Sesion.CreateObjectSet<Tomado>();
			var coleccion = (from t in tabTomado
							 where (t.Tabla == tabla && t.IdRegistro == idRegistro && t.IdSesionUsuario == idSesionUsuario)
							 select t).SingleOrDefault();
			return coleccion;
		}
		#endregion DTO
	}
}