using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;

namespace HConnexum.Base.Datos
{
	///<summary>Clase: TraModAppPagModAppTraModAppRepositorio.</summary>
	public sealed class TraModAppPagModAppTraModAppRepositorio : RepositorioBase<TB_TraModAppPagModAppTraModApp, TomadoDto>
	{
		#region "Constructores"
		
		///<summary>Constructor de la clase TraModAppPagModAppTraModAppRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public TraModAppPagModAppTraModAppRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		#endregion "Constructores"
		
		#region DTO
		
		public TB_TraModAppPagModAppTraModApp ObtenerForeignKey(int idPaginaModulo, int idTransaccionModulo)
		{
			var tabTraModAppPagModAppTraModApp = this.udt.Sesion.CreateObjectSet<TB_TraModAppPagModAppTraModApp>();
			var coleccion = (from tab in tabTraModAppPagModAppTraModApp
							 where (tab.IdPaginaModulo == idPaginaModulo &&
									tab.IdTransaccionModuloAplicacion == idTransaccionModulo) &&
								   tab.IndEliminado == false &&
								   tab.IndVigente == true &&
								   tab.FechaValidez <= DateTime.Now
							 select tab).SingleOrDefault();
			
			return coleccion;
		}
		
		public IEnumerable<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesPaginaModuloDtoPaginaBase(int idPaginaModulo)
		{
			var tabTraModAppPagModAppTraModApp = this.udt.Sesion.CreateObjectSet<TB_TraModAppPagModAppTraModApp>();
			var tabTransaccionesModulosAplicacion = this.udt.Sesion.CreateObjectSet<TB_TransaccionesModulosAplicaciones>();
			var tabTipoTransaccion = this.udt.Sesion.CreateObjectSet<TB_TipoTransacciones>();
			var coleccion = (from TMAPMATA in tabTraModAppPagModAppTraModApp
							 join TMA in tabTransaccionesModulosAplicacion on TMAPMATA.IdTransaccionModuloAplicacion equals TMA.Id
							 join TT in tabTipoTransaccion on TMA.IdTipoTransaccion equals TT.Id
							 orderby TMA.Nombre
							 where TMAPMATA.IdPaginaModulo == idPaginaModulo &&
								   TMAPMATA.IndEliminado == false &&
								   TMAPMATA.IndVigente == true &&
								   TMAPMATA.FechaValidez <= DateTime.Now &&
								   TMA.IndEliminado == false &&
								   TMA.IndVigente == true &&
								   TMA.FechaValidez <= DateTime.Now &&
								   TT.IndEliminado == false &&
								   TT.IndVigente == true &&
								   TT.FechaValidez <= DateTime.Now
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
							 });
			
			coleccion = UtilidadesDto<HConnexum.Seguridad.TraModAppPagModAppTraModApp>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		
		public IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesPorIdRolIdPaginaModulo(int idPaginaModulo, int[] idRoles)
		{
			var tabTransaccionesModulosAplicacionesRolAplicacion = this.udt.Sesion.CreateObjectSet<TB_TransaccionesModulosAplicacionesRolAplicaciones>();
			var tabRol = this.udt.Sesion.CreateObjectSet<TB_Roles>();
			var tabTraModAppPagModAppTraModApp = this.udt.Sesion.CreateObjectSet<TB_TraModAppPagModAppTraModApp>();
			var tabPaginasModulo = this.udt.Sesion.CreateObjectSet<TB_PaginasModulos>();
			var collecion = (from tmara in tabTransaccionesModulosAplicacionesRolAplicacion
							 join r in tabRol on tmara.IdRol equals r.Id
							 join tmarpama in tabTraModAppPagModAppTraModApp on tmara.IdTraModAppPagModAppTraModApp equals tmarpama.Id
							 join pm in tabPaginasModulo on tmarpama.IdPaginaModulo equals pm.Id
							 where pm.Id == idPaginaModulo &&
								   idRoles.Contains(r.Id) &&
								   tmara.IndEliminado == false &&
								   tmara.IndVigente == true &&
								   tmara.FechaValidez <= DateTime.Now &&
								   r.IndEliminado == false &&
								   r.IndVigente == true &&
								   r.FechaValidez <= DateTime.Now &&
								   tmarpama.IndEliminado == false &&
								   tmarpama.IndVigente == true &&
								   tmarpama.FechaValidez <= DateTime.Now &&
								   pm.IndEliminado == false &&
								   pm.IndVigente == true &&
								   pm.FechaValidez <= DateTime.Now
							 select new HConnexum.Seguridad.TraModAppPagModAppTraModApp
							 {
								 Id = tmarpama.Id,
								 IndEliminado = tmara.IndEliminado
							 }).ToList();
			
			return collecion;
		}
		
		#endregion DTO
	}
}