using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: SolicitudBloqueRepositorio.</summary>	
	public sealed class SolicitudBloqueRepositorio : RepositorioBase<SolicitudBloque>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase SolicitudBloqueRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public SolicitudBloqueRepositorio(IUnidadDeTrabajo udt)
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
		///<returns>Retorna IEnumerableSolicitudBloqueDTO.</returns>
		public IEnumerable<SolicitudBloqueDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabSolicitudBloque = udt.Sesion.CreateObjectSet<SolicitudBloque>();
			var tabBloque = udt.Sesion.CreateObjectSet<Bloque>();
			var tabListaValor = udt.Sesion.CreateObjectSet<ListaValor>();
			var coleccion = from sb in tabSolicitudBloque
							join b in tabBloque on sb.IdBloque equals b.Id into joinBloque
							from b in joinBloque.DefaultIfEmpty()
							join lv in tabListaValor on sb.IdTipoControl equals lv.Id into joinListaValor
							from lv in joinListaValor.DefaultIfEmpty()
							orderby "it." + orden
							select new SolicitudBloqueDTO
							{
								Id = sb.Id,
								IdFlujoServicio = sb.IdFlujoServicio,
								NombreBloque = b.Nombre,
								IdBloque = sb.IdBloque,
								Orden = sb.Orden,
								CreadoPor = sb.CreadoPor,
								FechaCreacion = sb.FechaCreacion,
								ModificadoPor = sb.ModificadoPor,
								FechaModificacion = sb.FechaModificacion,
								IndVigente = sb.IndVigente,
								FechaValidez = sb.FechaValidez,
								IndEliminado = sb.IndEliminado,
								IndCierre = sb.IndCierre,
								NombreTipoControl = lv.NombreValor,
								IdTipoControl = sb.IdTipoControl,
								TituloBloque = sb.TituloBloque,
								IndActualizable = sb.IndActualizable,
								KeyCampoXML = sb.KeyCampoXML
							};
			coleccion = UtilidadesDTO<SolicitudBloqueDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<SolicitudBloqueDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<SolicitudBloqueDTO>.Conteo;
			return UtilidadesDTO<SolicitudBloqueDTO>.EncriptarId(coleccion);
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableSolicitudBloqueDTO.</returns>
		public IEnumerable<SolicitudBloqueDTO> ObtenerDTO()
		{
			var tabSolicitudBloque = udt.Sesion.CreateObjectSet<SolicitudBloque>();
			var coleccion = from tab in tabSolicitudBloque
							orderby tab.Id
							select new SolicitudBloqueDTO
							{
								Id = tab.Id,
								IdFlujoServicio = tab.IdFlujoServicio,
								IdBloque = tab.IdBloque,
								Orden = tab.Orden,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado,
								IndCierre = tab.IndCierre,
								IdTipoControl = tab.IdTipoControl,
								TituloBloque = tab.TituloBloque,
								IndActualizable = tab.IndActualizable,
								KeyCampoXML = tab.KeyCampoXML
							};
			coleccion = UtilidadesDTO<SolicitudBloqueDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableSolicitudBloqueDTO.</returns>
		public IEnumerable<SolicitudBloqueDTO> ObtenerDTO(int idFlujoServicio)
		{
			var tabBloque = udt.Sesion.CreateObjectSet<Bloque>();
			var tabSolicitudBloque = udt.Sesion.CreateObjectSet<SolicitudBloque>();
			var coleccion = from b in tabBloque
							join sb in tabSolicitudBloque on b.Id equals sb.IdBloque
							orderby sb.Orden
							where
							  sb.IdFlujoServicio == idFlujoServicio &&
							  sb.IndVigente == true &&
							  sb.IndEliminado == false &&
							  sb.FechaValidez <= DateTime.Now &&
							  b.IndVigente == true &&
							  b.IndEliminado == false &&
							  b.FechaValidez <= DateTime.Now
							select new SolicitudBloqueDTO
							{
								IdBloque = sb.IdBloque,
								Orden = sb.Orden,
								IdTipoControl = sb.IdTipoControl,
								TituloBloque = sb.TituloBloque,
								IndActualizable = sb.IndActualizable,
								NombrePrograma = b.NombrePrograma,
								IdListaValor = b.IdListaValor,
								FechaValidez = b.FechaValidez,
								KeyCampoXML = sb.KeyCampoXML,
								IdFlujoServicio = sb.IdFlujoServicio,
								IndVigente = sb.IndVigente,
							};
			coleccion = UtilidadesDTO<SolicitudBloqueDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		#endregion DTO
	}
}