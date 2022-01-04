using System;
using System.Linq;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Datos
{
	public sealed class ServicioRepositorio : RepositorioBase<Servicio>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase MovimientoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public ServicioRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		#endregion "Constructores"
	}
}
