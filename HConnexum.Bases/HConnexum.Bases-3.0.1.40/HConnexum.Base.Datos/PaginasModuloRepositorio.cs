using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;

namespace HConnexum.Base.Datos
{
	///<summary>Clase: PaginasModuloRepositorio.</summary>
	public sealed class PaginasModuloRepositorio : RepositorioBase<TB_PaginasModulos, TomadoDto>
	{
		#region "Constructores"
		
		///<summary>Constructor de la clase PaginasModuloRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public PaginasModuloRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		#endregion "Constructores"
		
		#region DTO
		
		#endregion DTO
	}
}