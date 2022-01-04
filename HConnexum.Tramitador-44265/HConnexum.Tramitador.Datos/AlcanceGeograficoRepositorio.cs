using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: AlcanceGeograficoRepositorio.</summary>	
    public sealed class  AlcanceGeograficoRepositorio : RepositorioBase<AlcanceGeografico>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase AlcanceGeograficoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public AlcanceGeograficoRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableAlcanceGeograficoDTO.</returns>
public IEnumerable<AlcanceGeograficoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
{
	var tabAlcanceGeografico = udt.Sesion.CreateObjectSet<AlcanceGeografico>();
	var coleccion = from tab in tabAlcanceGeografico
         orderby "it." + orden  
         select new AlcanceGeograficoDTO {
		 Id = tab.Id
,IdServicioSucursal = tab.IdServicioSucursal
,IdPais = tab.IdPais
,IdDiv1 = tab.IdDiv1
,IdDiv2 = tab.IdDiv2
,IdDiv3 = tab.IdDiv3
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};

    coleccion = UtilidadesDTO<AlcanceGeograficoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<AlcanceGeograficoDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<AlcanceGeograficoDTO>.Conteo;
    return coleccion; 
}
        
///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableAlcanceGeograficoDTO.</returns>
public IEnumerable<AlcanceGeograficoDTO> ObtenerDTO()
{
    var tabAlcanceGeografico = udt.Sesion.CreateObjectSet<AlcanceGeografico>();
    var coleccion = from tab in tabAlcanceGeografico  
         select new AlcanceGeograficoDTO {
		 Id = tab.Id
,IdServicioSucursal = tab.IdServicioSucursal
,IdPais = tab.IdPais
,IdDiv1 = tab.IdDiv1
,IdDiv2 = tab.IdDiv2
,IdDiv3 = tab.IdDiv3
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<AlcanceGeograficoDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion; 
}
public IEnumerable<AlcanceGeograficoDTO> ObtenerDTOconTodo(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
{
    var tabAlcanceGeografico = udt.Sesion.CreateObjectSet<AlcanceGeografico>();
    var tabServicioSucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var tabFlujoServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
    var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
    var tabSucursales = udt.Sesion.CreateObjectSet<Sucursal>();
    var tabPaises = udt.Sesion.CreateObjectSet<Pais>();
    var tabDivisionesTerritoriales1 = udt.Sesion.CreateObjectSet<DivisionesTerritoriales1>();
    var tabDivisionesTerritoriales2 = udt.Sesion.CreateObjectSet<DivisionesTerritoriales2>();
    var tabDivisionesTerritoriales3 = udt.Sesion.CreateObjectSet<DivisionesTerritoriales3>();

    
    
    var coleccion = from tab in tabAlcanceGeografico
                    join tabSS in tabServicioSucursal on tab.IdServicioSucursal equals tabSS.Id
                    join tabFS in tabFlujoServicio on tabSS.IdFlujoServicio equals tabFS.Id
                    join tabS in tabServicioSuscriptor on tabFS.IdServicioSuscriptor equals tabS.Id
                    join tabSu in tabSucursales on tabSS.IdSucursal equals tabSu.Id
                    join tabP in tabPaises on tab.IdPais equals tabP.Id
                    join TabDV1 in tabDivisionesTerritoriales1 on tab.IdDiv1 equals TabDV1.Id into tabDiv1
                    from D1 in tabDiv1.DefaultIfEmpty()
                    join TabDV2 in tabDivisionesTerritoriales2 on tab.IdDiv2 equals TabDV2.Id into tabDiv2
                    from D2 in tabDiv2.DefaultIfEmpty()
                    join TabDV3 in tabDivisionesTerritoriales3 on tab.IdDiv3 equals TabDV3.Id into tabDiv3
                    from D3 in tabDiv3.DefaultIfEmpty()
                    orderby "it." + orden
                    select new AlcanceGeograficoDTO
                    {
                        Id = tab.Id
                        ,
                        IdServicioSucursal = tab.IdServicioSucursal
                        ,
                        Servicio = tabS.Nombre
                        ,
                        Sucursal = tabSu.Nombre
                        ,
                        Pais = tabP.Nombre
                        ,
                        Div1 = D1 != null ? D1.NombreDivTer1 : null
                        ,
                        Div2 = D2 != null ? D2.NombreDivTer2 : null
                        ,
                        Div3 = D3 != null ? D3.NombreDivTer3 : null
                        ,
                        IdFlujoServicio = tabSS.IdFlujoServicio
                        ,
                        IdSucursal = tabSS.IdSucursal
                        ,
                        IdPais = tab.IdPais
                        ,
                        IdDiv1 = tab.IdDiv1
                        ,
                        IdDiv2 = tab.IdDiv2
                        ,
                        IdDiv3 = tab.IdDiv3
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
                        IndEliminado = tab.IndEliminado,
                        Version = tabFS.Version
                        
                    };

    coleccion = UtilidadesDTO<AlcanceGeograficoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
    coleccion = UtilidadesDTO<AlcanceGeograficoDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<AlcanceGeograficoDTO>.Conteo;
    return UtilidadesDTO<AlcanceGeograficoDTO>.EncriptarId(coleccion); 
}
		#endregion DTO
	}
}