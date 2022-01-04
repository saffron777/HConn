using HConnexum.Tramitador.Datos;
using Microsoft.Practices.Unity;

namespace HConnexum.Tramitador.Presentacion
{
	public class PresentadorDetalleBase<T> : PresentadorBase<T>
	{
		public IUnityContainer container;
		public IUnidadDeTrabajo unidadDeTrabajo;

		public PresentadorDetalleBase()
		{
			this.unidadDeTrabajo = new UnidadDeTrabajo();
		}
	}
}