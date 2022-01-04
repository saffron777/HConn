using System;
using System.Data.Objects;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Datos
{
	public class SeguridadDatos
	{
		/// <summary>Metodo que permite actualizar la sesion del usuario.</summary>
		/// <param name="pIdUsuarioSuscriptor">string. Id del UsuarioSuscriptor.</param>
		/// <param name="pSessionId">string. SessionId de la sesión actual.</param>
		public bool VerificarPermisoMetodo(int pIdUsuarioSuscriptor, int pIdPagina, int pIdTransaccion)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(new AuditoriaDto(), objectContext))
				{
					PaginaPermitidaUsuarioSuscriptorRepositorio repositorio = new PaginaPermitidaUsuarioSuscriptorRepositorio(udt);
					return repositorio.VerificarPermisoMetodo(pIdUsuarioSuscriptor, pIdPagina, pIdTransaccion);
				}
			}
		}
	}
}