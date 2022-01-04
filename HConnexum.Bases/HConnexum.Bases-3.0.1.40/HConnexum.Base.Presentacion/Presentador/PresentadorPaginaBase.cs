using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Base.Negocio;

namespace HConnexum.Base.Presentacion.Presentador
{
	public class PresentadorPaginaBase
	{
		protected readonly AuditoriaDto auditoriaDto;
		protected readonly GeneralesNegocio negocioBase;
		
		public PresentadorPaginaBase(AuditoriaDto auditoriaDto)
		{
			this.negocioBase = new GeneralesNegocio(auditoriaDto);
			this.auditoriaDto = auditoriaDto;
		}
		
		/// <summary>Obtiene el nombre de una pagina</summary>
		/// <param name="nombreArchivo">esto es el nombre de la pagina que buscara</param>
		/// <returns></returns>
		public string ObtenerPaginasModulos(int idPaginaModulo)
		{
			return this.negocioBase.ObtenerPaginasModulos(idPaginaModulo);
		}
		
		public IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesPaginasModulos(int idPaginasModulo)
		{
			return this.negocioBase.ObtenerTransaccionesPaginasModulos(idPaginasModulo);
		}
		
		public IList<HConnexum.Seguridad.TraModAppPagModAppTraModApp> ObtenerTransaccionesUsuario(int paginaModulo, int[] idRoles)
		{
			return this.negocioBase.ObtenerTransaccionesUsuario(paginaModulo, idRoles).ToList();
		}
		
		public void EliminarRegistroTomado(string tabla, int idRegistro)
		{
			this.negocioBase.EliminarRegistroTomado(tabla, idRegistro);
		}
	}
}