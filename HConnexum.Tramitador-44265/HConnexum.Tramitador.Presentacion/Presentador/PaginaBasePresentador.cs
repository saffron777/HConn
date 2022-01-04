using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public static class PaginaBasePresentador
	{
		/// <summary>Obtiene el nombre de una pagina</summary>
		/// <param name="nombreArchivo">esto es el nombre de la pagina que buscara</param>
		/// <returns></returns>
		public static PaginasModulo ObtenerPaginasModulos(int IdPaginaModulo)
		{
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			PaginasModuloRepositorio repositorio = new PaginasModuloRepositorio(udt);
			return repositorio.ObtenerPorId(IdPaginaModulo);
		}

		public static IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesPaginasModulos(int idPaginasModulo)
		{
			UnidadDeTrabajo udt = new UnidadDeTrabajo ();
			TraModAppPagModAppTraModAppRepositorio repositorio = new TraModAppPagModAppTraModAppRepositorio(udt);
			return repositorio.ObtenerTransaccionesPaginaModuloDTOPaginaBase(idPaginasModulo).ToList();
		}

		public static IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesUsuario(int paginaModulo, int[] idRoles)
		{
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			TraModAppPagModAppTraModAppRepositorio repositorio = new TraModAppPagModAppTraModAppRepositorio(udt);
			return repositorio.ObtenerTransaccionesPorIdRolIdPaginaModulo(paginaModulo, idRoles).ToList();
		}

		public static void EliminarRegistroTomado(string tabla, int idRegistro, int idSession)
		{
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			udt.IniciarTransaccion();
			TomadoRepositorio repositorio = new TomadoRepositorio(udt);
			Tomado tomado = repositorio.ObtenerTomado(tabla, idRegistro, idSession);
			if(tomado != null)
				repositorio.Eliminar(tomado);
			udt.Commit();
		}
        public static string AppOrigen(string Aplicacion)
        {
            UnidadDeTrabajo udt = new UnidadDeTrabajo();
            udt.IniciarTransaccion();
            ListasValorRepositorio repositorio = new ListasValorRepositorio(udt);
            string RutaOrigen = repositorio.ObtengoValorPorListaValor(Aplicacion, "Aplicaciones");
            if (!string.IsNullOrEmpty(RutaOrigen))
                return RutaOrigen;
            else
                return "";

        }
	}
}
