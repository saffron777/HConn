using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public static class TransaccionesPaginasModulo
	{
		public static IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesPaginasModulos(int IdPaginasModulo)
		{
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			TraModAppPagModAppTraModAppRepositorio repositorio = new TraModAppPagModAppTraModAppRepositorio(udt);
			return repositorio.ObtenerTransaccionesPaginaModuloDTOPaginaBase(IdPaginasModulo).ToList();
		}

		public static IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesUsuario(int PaginaModulo, int[] IdRoles)
		{
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			TraModAppPagModAppTraModAppRepositorio repositorio = new TraModAppPagModAppTraModAppRepositorio(udt);
			return repositorio.ObtenerTransaccionesPorIdRolIdPaginaModulo(PaginaModulo, IdRoles).ToList();
		}
	}
}