using System.Collections.Generic;
using HConnexum.Tramitador.Datos;
using HConnexum.Infraestructura;
using Microsoft.Practices.Unity;

namespace HConnexum.Tramitador.Presentacion
{
	public class PresentadorListaBase<T> : PresentadorBase<T>
	{
		public IUnityContainer container;
		public IUnidadDeTrabajo unidadDeTrabajo;

		public PresentadorListaBase()
		{
			this.unidadDeTrabajo = new UnidadDeTrabajo();

			this.ParametrosFiltro = new List<HConnexum.Infraestructura.Filtro>();
		}

		readonly IList<HConnexum.Infraestructura.Filtro> ParametrosFiltro;
	}
}