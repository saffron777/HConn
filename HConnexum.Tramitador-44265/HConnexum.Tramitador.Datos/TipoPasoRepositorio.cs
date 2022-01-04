using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: TipoPasoRepositorio.</summary>	
	public sealed class TipoPasoRepositorio : RepositorioBase<TipoPaso>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase TipoPasoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public TipoPasoRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		#endregion "Constructores"

		#region DTO
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableTipoPasoDTO.</returns>
		public IEnumerable<TipoPasoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var coleccion = from tab in tabTipoPaso
							orderby "it." + orden
							select new TipoPasoDTO
							{
								Id = tab.Id,
								Descripcion = tab.Descripcion,
								Programa = tab.Programa,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<TipoPasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<TipoPasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<TipoPasoDTO>.Conteo;
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableTipoPasoDTO.</returns>
		public IEnumerable<TipoPasoDTO> ObtenerDTO()
		{
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var coleccion = from tab in tabTipoPaso
							select new TipoPasoDTO
							{
								Id = tab.Id,
								Descripcion = tab.Descripcion,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<TipoPasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableTipoPasoDTO.</returns>
		public IEnumerable<TipoPasoDTO> ObtenerDTOTipoPaso(int idFlujoServicio)
		{
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
			var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
			var coleccion = (from tab in tabTipoPaso
							 join tabP in tabPaso
							 on tab.Id equals tabP.IdTipoPaso
							 join tabE in tabEtapa
							 on tabP.IdEtapa equals tabE.Id
							 where tabE.IdFlujoServicio == idFlujoServicio
							 select new TipoPasoDTO
							 {
								 Id = tab.Id,
								 Descripcion = tab.Descripcion,
								 IdTipoPaso = tabP.IdTipoPaso,
								 IndVigente = tab.IndVigente,
								 FechaValidez = tab.FechaValidez,
								 IndEliminado = tab.IndEliminado
							 }).Distinct();
			coleccion = UtilidadesDTO<TipoPasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableTipoPasoDTO.</returns>
		public IEnumerable<TipoPasoDTO> ObtenerDTOTipoPasoModificar(int id)
		{
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
			var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
			var coleccion = (from tab in tabTipoPaso
							 join tabP in tabPaso
							 on tab.Id equals tabP.IdTipoPaso
							 join tabE in tabEtapa
							 on tabP.IdEtapa equals tabE.Id
							 where tabP.Id == id
							 select new TipoPasoDTO
							 {
								 Id = tab.Id,
								 Descripcion = tab.Descripcion,
								 IdTipoPaso = tabP.IdTipoPaso,
								 IndVigente = tab.IndVigente,
								 FechaValidez = tab.FechaValidez,
								 IndEliminado = tab.IndEliminado
							 }).Distinct();
			coleccion = UtilidadesDTO<TipoPasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		public int BuscarTipoPaso(TipoPaso tipoPaso)
		{
			var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
			var coleccion = (from tab in tabTipoPaso
							where tab.Descripcion == tipoPaso.Descripcion
							select tab).SingleOrDefault();
			if(coleccion != null)
				return coleccion.Id;
			return 0;
		}
		#endregion DTO
	}
}