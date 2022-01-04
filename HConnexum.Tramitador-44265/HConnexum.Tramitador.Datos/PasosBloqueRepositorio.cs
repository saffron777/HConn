using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: PasosBloqueRepositorio.</summary>	
	public sealed class PasosBloqueRepositorio : RepositorioBase<PasosBloque>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase PasosBloqueRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public PasosBloqueRepositorio(IUnidadDeTrabajo udt)
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
		///<returns>Retorna IEnumerablePasosBloqueDTO.</returns>
		public IEnumerable<PasosBloqueDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabPasosBloque = udt.Sesion.CreateObjectSet<PasosBloque>();
			var tabBloque = udt.Sesion.CreateObjectSet<Bloque>();
			var coleccion = from tab in tabPasosBloque
							join tabB in tabBloque
							on tab.IdBloque equals tabB.Id
							orderby "it." + orden
							select new PasosBloqueDTO
							{
								Id = tab.Id
								,
								IdPaso = tab.IdPaso
								,
								NombreBloque = tabB.Nombre
								,
								Orden = tab.Orden
								,
								IndVigente = tab.IndVigente
								,
								FechaValidez = tab.FechaValidez
								,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<PasosBloqueDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<PasosBloqueDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<PasosBloqueDTO>.Conteo;
			return UtilidadesDTO<PasosBloqueDTO>.EncriptarId(coleccion);
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerablePasosBloqueDTO.</returns>
		public IEnumerable<PasosBloqueDTO> ObtenerDTO()
		{
			var tabPasosBloque = udt.Sesion.CreateObjectSet<PasosBloque>();
			var coleccion = from tab in tabPasosBloque
							orderby tab.IdBloque
							select new PasosBloqueDTO
							{
								Id = tab.Id
								,
								IdPaso = tab.IdPaso
								,
								IdBloque = tab.IdBloque
								,
								Orden = tab.Orden
								,
								CreadoPor = tab.CreadoPor
								,
								FechaCreacion = tab.FechaCreacion
								,
								ModificadoPor = tab.ModificadoPor
								,
								FechaModificacion = tab.FechaModificacion
								,
								IndVigente = tab.IndVigente
								,
								FechaValidez = tab.FechaValidez
								,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<PasosBloqueDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		/// <summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		/// <param name="IdPaso">Parámetro de filtro en PasosBloque</param>
		/// <returns>Retorna IEnumerablePasosBloqueDTO.</returns>
		public IEnumerable<PasosBloqueDTO> ObtenerDTO(int idPaso)
		{
			var tabBloque = udt.Sesion.CreateObjectSet<Bloque>();
			var tabPasosBloque = udt.Sesion.CreateObjectSet<PasosBloque>();
			var coleccion = from pb in tabPasosBloque
							join b in tabBloque on pb.IdBloque equals b.Id
							orderby pb.Orden
							where
							 pb.IdPaso == idPaso &&
							 pb.IndVigente == true &&
							 pb.IndEliminado == false &&
							 pb.FechaValidez <= DateTime.Now &&
							 b.IndVigente == true &&
							 b.IndEliminado == false &&
							 b.FechaValidez <= DateTime.Now
							select new PasosBloqueDTO
							{
								IdBloque = pb.IdBloque,
								Orden = pb.Orden,
								IdTipoControl = pb.IdTipoControl,
								TituloBloque = pb.TituloBloque,
								IndActualizable = pb.IndActualizable,
								NombrePrograma = b.NombrePrograma,
								IdListaValor = b.IdListaValor,
								FechaValidez = b.FechaValidez,
								KeyCampoXML = pb.KeyCampoXML,
								IdPaso = pb.IdPaso,
								IndVigente = pb.IndVigente,
								IndColapsado = pb.IndColapsado
							};
			coleccion = UtilidadesDTO<PasosBloqueDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		#endregion DTO
	}
}
