using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Configurador.Datos
{

	///<summary>Clase: AgrupacionRepositorio.</summary>	
    public sealed class AgrupacionRepositorio : RepositorioBase<Agrupacion>
    {
		#region "ConstructoreS"
		///<summary>Constructor de la clase AgrupacionRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public AgrupacionRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableAgrupacionDTO.</returns>
public IEnumerable<AgrupacionDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
{
	var tabAgrupacion = udt.Sesion.CreateObjectSet<Agrupacion>();
	var coleccion = from tab in tabAgrupacion
			orderby "it." + orden  
			select new AgrupacionDTO {
			Id = tab.Id
,Nombre = tab.Nombre
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,FechaValidez = tab.FechaValidez
,IndVigente = tab.IndVigente
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<AgrupacionDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<AgrupacionDTO>.FiltrarColeccionEliminacion(coleccion, true);
	Conteo = UtilidadesDTO<AgrupacionDTO>.Conteo;
	return coleccion; 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableAgrupacionDTO.</returns>
public IEnumerable<AgrupacionDTO> ObtenerDTO()
{
	var tabAgrupacion = udt.Sesion.CreateObjectSet<Agrupacion>();
	var coleccion = from tab in tabAgrupacion  orderby tab.Nombre
			select new AgrupacionDTO {
			Id = tab.Id
,Nombre = tab.Nombre
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,FechaValidez = tab.FechaValidez
,IndVigente = tab.IndVigente
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<AgrupacionDTO>.FiltrarColeccionAuditoria(coleccion, false);
	return coleccion; 
}
		#endregion DTO
	}
}