using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: DocumentoRepositorio.</summary>	
	public sealed class DocumentoRepositorio : RepositorioBase<Documento>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase DocumentoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public DocumentoRepositorio(IUnidadDeTrabajo udt)
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
		///<returns>Retorna IEnumerableDocumentoDTO.</returns>
		public IEnumerable<DocumentoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabDocumento = udt.Sesion.CreateObjectSet<Documento>();
			var coleccion = from tab in tabDocumento
							orderby "it." + orden
							select new DocumentoDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre,
								IdRutaArchivo = tab.IdRutaArchivo,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<DocumentoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<DocumentoDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<DocumentoDTO>.Conteo;
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableDocumentoDTO.</returns>
		public IEnumerable<DocumentoDTO> ObtenerDTO()
		{
			var tabDocumento = udt.Sesion.CreateObjectSet<Documento>();
			var coleccion = from tab in tabDocumento
							orderby tab.Nombre
							select new DocumentoDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre,
								IdRutaArchivo = tab.IdRutaArchivo,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<DocumentoDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableDocumentoDTO.</returns>
		public IEnumerable<Documento> ObtenerDocumentosPorIds(IList<int> idDocumentos)
		{
			var tabDocumento = udt.Sesion.CreateObjectSet<Documento>();
			var coleccion = (from d in tabDocumento
							where idDocumentos.Contains(d.Id) &&
								d.IndEliminado == false &&
								d.IndVigente == true &&
								d.FechaValidez <= DateTime.Now
							orderby d.Nombre
							select d).Distinct();
			return coleccion;
		}

		public int BuscarDocumento(Documento documento)
		{
			var tabDocumento = udt.Sesion.CreateObjectSet<Documento>();
			var coleccion = (from tab in tabDocumento
							 where tab.Nombre == documento.Nombre
							 select tab).SingleOrDefault();
			if(coleccion != null)
				return coleccion.Id;
			return 0;
		}
		#endregion DTO
	}
}