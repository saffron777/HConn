using HConnexum.Base.Negocio;

namespace HConnexum.Base.Servicio
{
	public class ServicioWebBase
	{
		private readonly SeguridadNegocio seguridad = new SeguridadNegocio();
		
		/// <summary>Metodo que permite actualizar la sesion del usuario.</summary>
		/// <param name="pIdUsuarioSuscriptor">string. Id del UsuarioSuscriptor.</param>
		/// <param name="pSessionId">string. SessionId de la sesión actual.</param>
		public bool VerificarPermisoMetodo(int pIdUsuarioSuscriptor, int pIdPagina, int pIdTransaccion)
		{
			return this.seguridad.VerificarPermisoMetodo(pIdUsuarioSuscriptor, pIdPagina, pIdTransaccion);
		}
	}
}