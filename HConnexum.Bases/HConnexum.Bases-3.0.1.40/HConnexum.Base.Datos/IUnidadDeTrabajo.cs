using System;
using System.Data.Objects;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Datos
{
	/// <summary>
	/// Interface IUnidadDeTrabajo
	/// </summary>
	public interface IUnidadDeTrabajo : IDisposable
	{
		/// <summary>
		/// Información de auditoría
		/// </summary>
		AuditoriaDto AuditoriaDto { get; }
		
		/// <summary>
		/// Gets the sesion.
		/// </summary>
		/// <value> sesion.</value>
		ObjectContext Sesion { get; }
		
		/// <summary>
		/// Valor para indicar que se esta en transaccion.
		/// </summary>
		/// <value><c>true</c>si [esta en transaccion]; de otra manera, <c>false</c>.</value>
		bool EstaEnTransaccion { get; }
		
		string IdSesion { get; }
		
		/// <summary>
		/// Marcar modificado.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"> entity.</param>
		void MarcarModificado<T>(T entity) where T : class;
		
		/// <summary>
		/// Marcar nuevo.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"> entity.</param>
		void MarcarNuevo<T>(T entity) where T : class;
		
		/// <summary>
		/// Marcar eliminado.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity"> entity.</param>
		void MarcarEliminado<T>(T entity) where T : class;
		
		/// <summary>
		/// Iniciar  transaccion.
		/// </summary>
		void IniciarTransaccion();
		
		/// <summary>
		/// Commit de la Transaccion.
		/// </summary>
		void Commit();
		
		/// <summary>
		/// Rollbacks de la Transaccion.
		/// </summary>
		void Rollback();
	}
}