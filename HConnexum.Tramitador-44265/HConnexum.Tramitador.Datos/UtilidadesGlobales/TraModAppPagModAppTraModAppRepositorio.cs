using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using System;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: TraModAppPagModAppTraModAppRepositorio.</summary>
	public sealed class TraModAppPagModAppTraModAppRepositorio : RepositorioBase<TraModAppPagModAppTraModApp>
	{
		#region "Constructores"
		///<summary>Constructor de la clase TraModAppPagModAppTraModAppRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public TraModAppPagModAppTraModAppRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}
		#endregion "Constructores"

		#region DTO
		public TraModAppPagModAppTraModApp ObtenerForeignKey(int IdPaginaModulo, int IdTransaccionModulo)
		{
			var tabTraModAppPagModAppTraModApp = this.udt.Sesion.CreateObjectSet<TraModAppPagModAppTraModApp>();
			var coleccion = (from tab in tabTraModAppPagModAppTraModApp
							 where (tab.IdPaginaModulo == IdPaginaModulo &&
									tab.IdTransaccionModuloAplicacion == IdTransaccionModulo)
							 select tab).SingleOrDefault();
			return coleccion;
		}

		public IEnumerable<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesPaginaModuloDTOPaginaBase(int IdPaginaModulo)
		{
			var tabTraModAppPagModAppTraModApp = this.udt.Sesion.CreateObjectSet<TraModAppPagModAppTraModApp>();
			var tabTransaccionesModulosAplicacion = this.udt.Sesion.CreateObjectSet<TransaccionesModulosAplicacion>();
			var tabTipoTransaccion = this.udt.Sesion.CreateObjectSet<TipoTransaccion>();
			var coleccion = from TMAPMATA in tabTraModAppPagModAppTraModApp
							join TMA in tabTransaccionesModulosAplicacion on
							TMAPMATA.IdTransaccionModuloAplicacion equals TMA.Id
							join TT in tabTipoTransaccion on
							TMA.IdTipoTransaccion equals TT.Id
							orderby TMA.Nombre
							where TMAPMATA.IdPaginaModulo == IdPaginaModulo
							select new HConnexum.Seguridad.TraModAppPagModAppTraModApp
							{
								Id = TMAPMATA.Id,
								IdTipoTransaccion = TT.Id,
								NombreTipoTransaccion = TT.Nombre,
								IdPaginaModulo = TMAPMATA.IdPaginaModulo,
								IdTransaccionModuloAplicacion = TMA.Id,
								NombreTransaccion = TMA.Nombre,
								IndVigente = TMAPMATA.IndVigente,
								FechaValidez = TMAPMATA.FechaValidez,
								IndEliminado = TMAPMATA.IndEliminado
							};
			coleccion = UtilidadesDTO<HConnexum.Seguridad.TraModAppPagModAppTraModApp>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		public IEnumerable<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesPorIdRolIdPaginaModulo(int IdPaginaModulo, int[] IDRoles)
		{
			var tabTransaccionesModulosAplicacionesRolAplicacion = this.udt.Sesion.CreateObjectSet<TransaccionesModulosAplicacionesRolAplicacion>();
			var tabRol = this.udt.Sesion.CreateObjectSet<Rol>();
			var tabTraModAppPagModAppTraModApp = this.udt.Sesion.CreateObjectSet<TraModAppPagModAppTraModApp>();
			var tabPaginasModulo = this.udt.Sesion.CreateObjectSet<PaginasModulo>();
			var collecion = from tmara in tabTransaccionesModulosAplicacionesRolAplicacion
							join r in tabRol
							on tmara.IdRol equals r.Id
							join tmarpama in tabTraModAppPagModAppTraModApp
							on tmara.IdTraModAppPagModAppTraModApp equals tmarpama.Id
							join pm in tabPaginasModulo
							on tmarpama.IdPaginaModulo equals pm.Id
							where pm.Id == IdPaginaModulo && IDRoles.Contains(r.Id)
							select new HConnexum.Seguridad.TraModAppPagModAppTraModApp
							{
								Id = tmarpama.Id,
                                IndEliminado=tmara.IndEliminado
							};
			return collecion;
		}
		#endregion DTO
	}
}
