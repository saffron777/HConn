using System.Collections.Generic;
using System.Data.Objects.DataClasses;

namespace HConnexum.Tramitador.Datos
{
	public interface IRepositorio<T> where T : EntityObject
	{
		void Agregar(T entidad);
		void Actualizar(T entidad);
		void Eliminar(T entidad);
		void EliminarLogico(T entidad);
		IList<T> ObtenerTodos();
		T ObtenerPorId(int id);
		IList<T> ObtenerPaginado(string orden, int pagina, int registros);
		IList<T> ObtenerFiltrado(string orden, int pagina, int registros, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro);
	}
}
