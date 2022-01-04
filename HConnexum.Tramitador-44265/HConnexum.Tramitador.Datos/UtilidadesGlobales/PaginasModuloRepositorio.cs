using System.Linq;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: PaginasModuloRepositorio.</summary>
	public sealed class PaginasModuloRepositorio : RepositorioBase<PaginasModulo>
	{
		#region "Constructores"
		///<summary>Constructor de la clase PaginasModuloRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public PaginasModuloRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}
		#endregion "Constructores"

		#region DTO
		#endregion DTO
	}
}