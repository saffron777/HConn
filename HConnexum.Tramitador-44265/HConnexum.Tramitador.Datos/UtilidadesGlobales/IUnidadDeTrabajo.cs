using System;
using System.Data.Objects;

namespace HConnexum.Tramitador.Datos
{
	public interface IUnidadDeTrabajo : IDisposable
	{
		ObjectContext Sesion { get; }
		void MarcarModificado<T>(T entity) where T : class;
		void MarcarNuevo<T>(T entity) where T : class;
		void MarcarEliminado<T>(T entity) where T : class;
		void IniciarTransaccion();
		void Commit();
		void Rollback();
		bool EstaEnTransaccion { get; }
	}
}
