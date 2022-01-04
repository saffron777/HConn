using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: DocumentosServicioRepositorio.</summary>	
    public sealed class  DocumentosServicioRepositorio : RepositorioBase<DocumentosServicio>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase DocumentosServicioRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public DocumentosServicioRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableDocumentosServicioDTO.</returns>
public IEnumerable<DocumentosServicioDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
{
	var tabDocumentosServicio = udt.Sesion.CreateObjectSet<DocumentosServicio>();
	var coleccion = from tab in tabDocumentosServicio
         orderby "it." + orden  
         select new DocumentosServicioDTO {
		 Id = tab.Id
,IdFlujoServicio = tab.IdFlujoServicio
,IdDocumento = tab.IdDocumento
,IndDocObligatorio = tab.IndDocObligatorio
,IndVisibilidad = tab.IndVisibilidad
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};

    coleccion = UtilidadesDTO<DocumentosServicioDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<DocumentosServicioDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<DocumentosServicioDTO>.Conteo;
    return coleccion; 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableDocumentosServicioDTO.</returns>
public IEnumerable<DocumentosServicioDTO> ObtenerDTO()
{
    var tabDocumentosServicio = udt.Sesion.CreateObjectSet<DocumentosServicio>();
    var coleccion = from tab in tabDocumentosServicio  
         select new DocumentosServicioDTO {
		 Id = tab.Id
,IdFlujoServicio = tab.IdFlujoServicio
,IdDocumento = tab.IdDocumento
,IndDocObligatorio = tab.IndDocObligatorio
,IndVisibilidad = tab.IndVisibilidad
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<DocumentosServicioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion; 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableDocumentosServicioDTO.</returns>
public IEnumerable<DocumentosServicioDTO> ObtenerDTONombres(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int IdFlujoServicio)
{
    var tabDocumentosServicio = udt.Sesion.CreateObjectSet<DocumentosServicio>();
    var tabDocumentos = udt.Sesion.CreateObjectSet<Documento>();
    var coleccion = from tab in tabDocumentosServicio
                    join tabD in tabDocumentos on tab.IdDocumento equals tabD.Id
                    where tab.IdFlujoServicio == IdFlujoServicio
                    select new DocumentosServicioDTO
                    {
                        Id = tab.Id
                        ,
                        IdFlujoServicio = tab.IdFlujoServicio
                        ,
                        Nombre = tabD.Nombre
                        ,
                        IdDocumento = tab.IdDocumento
                        ,
                        IndDocObligatorio = tab.IndDocObligatorio
                        ,
                        IndVisibilidad = tab.IndVisibilidad
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

    coleccion = UtilidadesDTO<DocumentosServicioDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
    coleccion = UtilidadesDTO<DocumentosServicioDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<DocumentosServicioDTO>.Conteo;
    return UtilidadesDTO<DocumentosServicioDTO>.EncriptarId(coleccion);
}

public IEnumerable<DocumentosServicioDTO> ObtenerDTODocumentosServicios(int IdFlujoServicio)
{
    var tabDocumentosServicio = udt.Sesion.CreateObjectSet<DocumentosServicio>();
    var tabDocumento = udt.Sesion.CreateObjectSet<Documento>();
    var coleccion = from tab in tabDocumentosServicio
                    join tabD in tabDocumento on tab.IdDocumento equals tabD.Id
                    where tab.IdFlujoServicio == IdFlujoServicio
                    select new DocumentosServicioDTO
                    {
                        Id = tab.Id
                        ,
                        Nombre = tabD.Nombre
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        FechaValidez = tab.FechaValidez
                        ,
                        IndEliminado = tab.IndEliminado
                    };
    coleccion = UtilidadesDTO<DocumentosServicioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}
		#endregion DTO
	}
}