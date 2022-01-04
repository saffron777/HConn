using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: ServicioSucursalRepositorio.</summary>	
    public sealed class  ServicioSucursalRepositorio : RepositorioBase<ServicioSucursal>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase ServicioSucursalRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public ServicioSucursalRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableServicioSucursalDTO.</returns>
public IEnumerable<ServicioSucursalDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro,int idFlujoservicio)
{
	var tabServicioSucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var tabFlujo = udt.Sesion.CreateObjectSet<FlujosServicio>();
    var tabServicio = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
    var tabsucursal = udt.Sesion.CreateObjectSet<Sucursal>();
	var coleccion = from tab in tabServicioSucursal
                    join tabF in tabFlujo on tab.IdFlujoServicio equals tabF.Id
                    join tabSu in tabsucursal on tab.IdSucursal equals tabSu.Id
                    join tabSe in tabServicio on tabF.IdServicioSuscriptor equals tabSe.Id
                    where tab.IdFlujoServicio == idFlujoservicio
         orderby "it." + orden  
         select new ServicioSucursalDTO {
		 Id = tab.Id
        ,Servicio = tabSe.Nombre
        ,Sucursal = tabSu.Nombre
        ,IndVigente = tab.IndVigente
        ,FechaValidez = tab.FechaValidez
        ,IndEliminado = tab.IndEliminado
			};

    coleccion = UtilidadesDTO<ServicioSucursalDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<ServicioSucursalDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<ServicioSucursalDTO>.Conteo;
    return UtilidadesDTO<ServicioSucursalDTO>.EncriptarId(coleccion); 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableServicioSucursalDTO.</returns>
public IEnumerable<ServicioSucursalDTO> ObtenerDTO(int id)
{
    var tabServicioSucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var coleccion = from tab in tabServicioSucursal  
                    where tab.Id == id
         select new ServicioSucursalDTO {
		 Id = tab.Id
        ,IdFlujoServicio = tab.IdFlujoServicio
        ,IdSucursal = tab.IdSucursal
        ,CreadoPor = tab.CreadoPor
        ,FechaCreacion = tab.FechaCreacion
        ,ModificadoPor = tab.ModificadoPor
        ,FechaModificacion = tab.FechaModificacion
        ,IndVigente = tab.IndVigente
        ,FechaValidez = tab.FechaValidez
        ,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<ServicioSucursalDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion; 
}
///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableServicioSucursalDTO.</returns>
public IEnumerable<ServicioSucursalDTO> ObtenerDTOporFlujoServicio(int idFlujoServicio)
{
    var tabServicioSucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var coleccion = from tab in tabServicioSucursal
                    where tab.IdFlujoServicio == idFlujoServicio
                    select new ServicioSucursalDTO
                    {
                        Id = tab.Id
                        ,
                        IdFlujoServicio = tab.IdFlujoServicio
                        ,
                        IdSucursal = tab.IdSucursal
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
    coleccion = UtilidadesDTO<ServicioSucursalDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}



///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableServicioSucursalDTO.</returns>
public IEnumerable<ServicioSucursalDTO> ObtenerDTOporServicioSucursal(int idServicioSucursal)
{
    var tabServicioSucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var coleccion = from tab in tabServicioSucursal
                    where tab.Id == idServicioSucursal
                    select new ServicioSucursalDTO
                    {
                        Id = tab.Id
                        ,
                        IdFlujoServicio = tab.IdFlujoServicio
                        ,
                        IdSucursal = tab.IdSucursal
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
    coleccion = UtilidadesDTO<ServicioSucursalDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

public string ObtenerServicioPorIdservicioSucursal(int Idserviciosuscursal)
{
    var tabServiciosucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
    

    var coleccion = (from tab in tabServiciosucursal
                     join tabf in tabFlujosServicio on tab.IdFlujoServicio equals tabf.Id
                     where tab.Id == Idserviciosuscursal
                     select new FlujosServicioDTO
                     {

                         IdServicioSuscriptor = tabf.IdServicioSuscriptor,

                     }).SingleOrDefault();

    return coleccion.IdServicioSuscriptor.ToString();
}
public string ObtenerSuscriptorIdservicioSucursal(int Idserviciosuscursal)
{
    var tabServiciosucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();


    var coleccion = (from tab in tabServiciosucursal
                     join tabf in tabFlujosServicio on tab.IdFlujoServicio equals tabf.Id
                     where tab.Id == Idserviciosuscursal
                     select new FlujosServicioDTO
                     {

                         IdSuscriptor = tabf.IdSuscriptor,

                     }).SingleOrDefault();

    return coleccion.IdSuscriptor.ToString();
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableServicioSucursalDTO.</returns>
public IEnumerable<ServicioSucursalDTO> ObtenerSucursalDTO(int idFlujoServicio)
{
    var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
    var tabServicioSucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
    var tabSucursal = udt.Sesion.CreateObjectSet<Sucursal>();
    var coleccion = (from tabSS in tabServicioSucursal
                     join tab in tabSucursal on tabSS.IdSucursal equals tab.Id
                     where tabSS.IdFlujoServicio == idFlujoServicio
                     select new ServicioSucursalDTO
                     {
                         Id = tabSS.Id,
                         NombreSucursal = tab.Nombre,
                         FechaValidez = tabSS.FechaValidez,
                         IndVigente = tabSS.IndVigente,
                         IndEliminado = tabSS.IndEliminado
                     }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());
    coleccion = UtilidadesDTO<ServicioSucursalDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableFlujosServicioDTO.</returns>
public IEnumerable<ServicioSucursalDTO> ObtenerServiciosSucursalDTOporSuscriptor(int idSuscriptor)
{
    var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
    var tabServicioSucursal = udt.Sesion.CreateObjectSet<ServicioSucursal>();
    var tabServicioSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
    var coleccion = (from tabSS in tabServicioSucursal
                     join tab in tabFlujosServicio on tabSS.IdFlujoServicio equals tab.Id
                     join tabSerS in tabServicioSuscriptor on tab.IdServicioSuscriptor equals tabSerS.Id
                     where tab.IdSuscriptor == idSuscriptor &&
                     tabSS.IndEliminado == false &&
                     tabSS.IndVigente == true &&
                     tabSS.FechaValidez <= DateTime.Now
                     orderby tab.Version descending
                     select new ServicioSucursalDTO
                     {
                         Id = tab.Id,
                         NombreServicio = tabSerS.Nombre,
                         FechaValidez = tabSS.FechaValidez,
                         IndVigente = tabSS.IndVigente,
                         IndEliminado = tabSS.IndEliminado,
                         Version = tab.Version,
                     }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());
    coleccion = UtilidadesDTO<ServicioSucursalDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}
		#endregion DTO
	}
}
