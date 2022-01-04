using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;

namespace HConnexum.Base.Datos
{
	///<summary>Clase: UsuarioRepositorio.</summary>
	public sealed class PaginaPermitidaUsuarioSuscriptorRepositorio : RepositorioBase<Vw_PaginaPermitidaUsuarioSuscriptor, TomadoDto>
	{
		#region "Constructores"
		
		///<summary>Constructor de la clase PaginaPermitidaUsuarioSuscriptorRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public PaginaPermitidaUsuarioSuscriptorRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		#endregion "Constructores"
		
		#region DTO
		
		public bool VerificarPermisoMetodo(int pIdUsuarioSuscriptor, int pIdPagina, int pIdTransaccion)
		{
			var tabVista = this.udt.Sesion.CreateObjectSet<Vw_PaginaPermitidaUsuarioSuscriptor>();
			return tabVista.Any(tab => tab.UsuarioSuscriptor == pIdUsuarioSuscriptor && tab.IdPagina == pIdPagina && tab.IdTransaccion == pIdTransaccion);
		}
		
		#endregion DTO
	}
}