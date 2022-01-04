using HConnexum.Base.Datos;

namespace HConnexum.Base.Negocio
{
	public class SeguridadNegocio
	{
		/// <summary>Metodo que permite actualizar la sesion del usuario.</summary>
		/// <param name="pIdUsuarioSuscriptor">string. Id del UsuarioSuscriptor.</param>
		/// <param name="pSessionId">string. SessionId de la sesión actual.</param>
		public bool VerificarPermisoMetodo(int pIdUsuarioSuscriptor, int pIdPagina, int pIdTransaccion)
		{
			SeguridadDatos seguridadDatos = new SeguridadDatos();
			return seguridadDatos.VerificarPermisoMetodo(pIdUsuarioSuscriptor, pIdPagina, pIdTransaccion);
		}
	}
}