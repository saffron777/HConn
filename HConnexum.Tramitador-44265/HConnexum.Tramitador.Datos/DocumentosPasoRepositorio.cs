using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: DocumentosPasoRepositorio.</summary>	
    public sealed class  DocumentosPasoRepositorio : RepositorioBase<DocumentosPaso>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase DocumentosPasoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public DocumentosPasoRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableDocumentosPasoDTO.</returns>
public IEnumerable<DocumentosPasoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
{
	var tabDocumentosPaso = udt.Sesion.CreateObjectSet<DocumentosPaso>();
	var coleccion = from tab in tabDocumentosPaso
         orderby "it." + orden  
         select new DocumentosPasoDTO {
		 Id = tab.Id
,IdDocumentoServicio = tab.IdDocumentoServicio
,IdPaso = tab.IdPaso
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};

    coleccion = UtilidadesDTO<DocumentosPasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<DocumentosPasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<DocumentosPasoDTO>.Conteo;
    return coleccion; 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableDocumentosPasoDTO.</returns>
public IEnumerable<DocumentosPasoDTO> ObtenerDTO()
{
    var tabDocumentosPaso = udt.Sesion.CreateObjectSet<DocumentosPaso>();
    var coleccion = from tab in tabDocumentosPaso 
         select new DocumentosPasoDTO {
		 Id = tab.Id
,IdDocumentoServicio = tab.IdDocumentoServicio
,IdPaso = tab.IdPaso
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<DocumentosPasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion; 
}
/// <summary>
/// Lista Documentos obligatorios del paso
/// </summary>
/// <param name="orden"></param>
/// <param name="pagina"></param>
/// <param name="registros"></param>
/// <param name="parametrosFiltro"></param>
/// <returns>lista de documentos con el nombre de documentos traidos desde la tabla documento</returns>
public IEnumerable<DocumentosPasoDTO> ObtenerDTOListaDocumentosPasos(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idPaso)
{
    var tabDocumentosPaso = udt.Sesion.CreateObjectSet<DocumentosPaso>();
    var tabDocumentosServicios = udt.Sesion.CreateObjectSet<DocumentosServicio>();
    var tabDocumentos = udt.Sesion.CreateObjectSet<Documento>();
    var coleccion = from tab in tabDocumentosPaso
                    join tabDS in tabDocumentosServicios on tab.IdDocumentoServicio equals tabDS.Id
                    join tabD in tabDocumentos on tabDS.IdDocumento equals tabD.Id
                    where tab.IdPaso == idPaso
                    select new DocumentosPasoDTO
                    {
                        Id = tab.Id
                        ,
                        DocumentoServicio = tabD.Nombre
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        FechaValidez = tab.FechaValidez
                        ,
                        IndEliminado = tab.IndEliminado
                    };

    coleccion = UtilidadesDTO<DocumentosPasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
    coleccion = UtilidadesDTO<DocumentosPasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<DocumentosPasoDTO>.Conteo;
    return UtilidadesDTO<DocumentosPasoDTO>.EncriptarId(coleccion);
}
		#endregion DTO
	}
}