using System.Collections.Generic;
using System.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: BloqueRepositorio.</summary>	
	public sealed class BloqueRepositorio : RepositorioBase<Bloque>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase BloqueRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public BloqueRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}

		#endregion "Constructores"

		#region DTO
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableBloqueDTO.</returns>
		public IEnumerable<BloqueDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabBloque = udt.Sesion.CreateObjectSet<Bloque>();
			var coleccion = from tab in tabBloque
							orderby "it." + orden
							select new BloqueDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado
							};

			coleccion = UtilidadesDTO<BloqueDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<BloqueDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<BloqueDTO>.Conteo;
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableBloqueDTO.</returns>
		public IEnumerable<BloqueDTO> ObtenerDTO()
		{
			var tabBloque = udt.Sesion.CreateObjectSet<Bloque>();
			var coleccion = from tab in tabBloque
							//orderby "it." + orden
							select new BloqueDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado
							};

			coleccion = UtilidadesDTO<BloqueDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		public int BuscarBloque(Bloque bloque)
		{
			var tabBloque = udt.Sesion.CreateObjectSet<Bloque>();
			var coleccion = (from tab in tabBloque
							 where tab.Nombre == bloque.Nombre
							 select tab).SingleOrDefault();
			if(coleccion != null)
				return coleccion.Id;
			return 0;
		}
		#endregion DTO
	}
}