using System.Collections.Generic;

namespace HConnexum.Base.Datos
{
	public interface IRepositorio<T>// where T : EntityObject
	{
		void Agregar(T entidad);
		void Actualizar(T entidad);
		void Eliminar(T entidad);
		void EliminarLogico(T entidad);
		IList<T> ObtenerTodos();
		T ObtenerPorIdT(int id);
		IList<T> ObtenerPaginado(string orden, int pagina, int registros, bool indEliminado);
		IList<T> ObtenerFiltrado(string orden, int pagina, int registros, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, bool indEliminado);
	}
}