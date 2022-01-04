using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Base.Datos;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Seguridad;

namespace HConnexum.Base.Negocio
{
	public class GeneralesNegocio
	{
		private readonly GeneralesDatos datosBase;
		
		public GeneralesNegocio(AuditoriaDto auditoriaDto)
		{
			this.datosBase = new GeneralesDatos(auditoriaDto);
		}
		
		/// <summary>Obtiene el nombre de una pagina</summary>
		/// <param name="nombreArchivo">esto es el nombre de la pagina que buscara</param>
		/// <returns></returns>
		public string ObtenerPaginasModulos(int idPaginaModulo)
		{
			return this.datosBase.ObtenerPaginasModulos(idPaginaModulo);
		}
		
		public IList<TraModAppPagModAppTraModApp> ObtenerTransaccionesPaginasModulos(int idPaginasModulo)
		{
			return this.datosBase.ObtenerTransaccionesPaginasModulos(idPaginasModulo);
		}
		
		public IList<TraModAppPagModAppTraModApp> ObtenerTransaccionesUsuario(int paginaModulo, int[] idRoles)
		{
			return this.datosBase.ObtenerTransaccionesUsuario(paginaModulo, idRoles);
		}
		
		public void EliminarRegistroTomado(string tabla, int idRegistro)
		{
			this.datosBase.EliminarRegistroTomado(tabla, idRegistro);
		}
		
		public string ObtenerNombreUsuario(int? id)
		{
			return this.datosBase.ObtenerNombreUsuario(id);
		}
		
		public string ObtenerNombreUsuarioTipoSistema(int? id)
		{
			return this.datosBase.ObtenerNombreUsuarioTipoSistema(id);
		}
		
		/// <summary>Obtiene el valor de la conexionstring que tiene asignado un suscriptor.</summary>
		/// <param name="idExterno">Id externo del suscriptor a consultar.</param>
		/// <param name=" origen "Nombre del dato particular del suscriptor a solicitar.</param>
		/// <returns>conexionstring.</returns>
		public string ObtenerListaValorPorIdExterno(int idExterno, string origen)
		{
			return this.datosBase.ObtenerListaValorPorIdExterno(idExterno, origen);
		}
		
		/// <summary>Obtiene el valor de la conexionstring que tiene asignado un suscriptor.</summary>
		/// <param name="suscriptorId">Identificador único del suscriptor a consultar.</param>
		/// <param name=" origen "Nombre del dato particular del suscriptor a solicitar.</param>
		/// <returns>conexionstring.</returns>
		public string ObtenerListaValorPorSuscriptorId(int suscriptorId, string origen)
		{
			return this.datosBase.ObtenerListaValorPorSuscriptorId(suscriptorId, origen);
		}
		
		public string ObtenerCodIdExternoByIdSucursal(int idSucursal)
		{
			return this.datosBase.ObtenerCodIdExternoByIdSucursal(idSucursal);
		}
	}
}