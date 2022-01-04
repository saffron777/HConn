using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: MensajesMetodosDestinatarioRepositorio.</summary>	
    public sealed class  MensajesMetodosDestinatarioRepositorio : RepositorioBase<MensajesMetodosDestinatario>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase MensajesMetodosDestinatarioRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public MensajesMetodosDestinatarioRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
{
	var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
	var coleccion = from tab in tabMensajesMetodosDestinatario
         orderby "it." + orden  
         select new MensajesMetodosDestinatarioDTO {
		 Id = tab.Id
,IdPaso = tab.IdPaso
,IdMetodo = tab.IdMetodo
,IdMensaje = tab.IdMensaje
,TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
,ValorBusqueda = tab.ValorBusqueda
,TipoPrivacidad = tab.IdTipoPrivacidad
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,IndEliminado = tab.IndEliminado
			};

    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.Conteo;
    return coleccion; 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerDTO(int? id, int tipo, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == tipo && tab.IdMetodo == metodo
         select new MensajesMetodosDestinatarioDTO {
             Id = tab.Id,
             IdPaso = tab.IdPaso
,
             IdMetodo = tab.IdMetodo
,
             IdMensaje = tab.IdMensaje
,TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
,
             ValorBusqueda = tab.ValorBusqueda
,
             TipoPrivacidad = tab.IdTipoPrivacidad
,
             CreadoPor = tab.CreadoPor
,
             FechaCreacion = tab.FechaCreacion
,
             ModificadoPor = tab.ModificadoPor
,
             FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,IndEliminado = tab.IndEliminado
,FechaValidez = tab.FechaValidez
			};
	coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion; 
}



///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerCorreoDTO(int? id)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == 1 && tab.IdMetodo == 2
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        IdMetodo = tab.IdMetodo
                        ,
                        IdMensaje = tab.IdMensaje
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
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
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public MensajesMetodosDestinatarioDTO ObtenerRutinaCorreoDTO(int? id, int metodo, int tipo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var entidad = (from tab in tabMensajesMetodosDestinatario
                   where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == tipo && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        IdMetodo = tab.IdMetodo
                        ,
                        IdMensaje = tab.IdMensaje
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
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
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    }).FirstOrDefault();
    return entidad;
}


///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public MensajesMetodosDestinatarioDTO ObtenerCorreoDTO(int? id, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
    var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
    var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
    var entidad = (from tab in tabMensajesMetodosDestinatario
                   join tabP in tabPaso
                   on tab.IdPaso equals tabP.Id
                   join tabE in tabEtapa
                   on tabP.IdEtapa equals tabE.Id
                   join tabF in tabFlujosServicio
                   on tabE.IdFlujoServicio equals tabF.Id
                   where tab.IdPaso == id && tab.IdMetodo == metodo
                   select new MensajesMetodosDestinatarioDTO
                   {
                       Id = tab.Id,
                       IdPaso = tab.IdPaso
                       ,
                       IdMetodo = tab.IdMetodo
                       ,
                       IdMensaje = tab.IdMensaje
                       ,
                       TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                       ,
                       ValorBusqueda = tab.ValorBusqueda
                       ,
                       TipoPrivacidad = tab.IdTipoPrivacidad
                       ,
                       IndVigenteFlujoServicio = tabF.IndVigente
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
                       IndEliminado = tab.IndEliminado
                       ,
                       FechaValidez = tab.FechaValidez
                   }).FirstOrDefault();
    return entidad;
}


///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public MensajesMetodosDestinatarioDTO ObtenerRutinaSMSDTO(int? id, int metodo, int rutina)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var entidad = (from tab in tabMensajesMetodosDestinatario
                   where tab.IdPaso == id && tab.IdMetodo == metodo && tab.IdTipoBusquedaDestinatario == rutina
                   select new MensajesMetodosDestinatarioDTO
                   {
                       Id = tab.Id,
                       IdPaso = tab.IdPaso
                       ,
                       IdMetodo = tab.IdMetodo
                       ,
                       IdMensaje = tab.IdMensaje
                       ,
                       TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                       ,
                       ValorBusqueda = tab.ValorBusqueda
                       ,
                       TipoPrivacidad = tab.IdTipoPrivacidad
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
                       IndEliminado = tab.IndEliminado
                       ,
                       FechaValidez = tab.FechaValidez
                   }).SingleOrDefault();
    return entidad;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public MensajesMetodosDestinatarioDTO ObtenerSMSDTO(int? id, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
    var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
    var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
    var entidad = (from tab in tabMensajesMetodosDestinatario
                   join tabP in tabPaso
                   on tab.IdPaso equals tabP.Id
                   join tabE in tabEtapa
                   on tabP.IdEtapa equals tabE.Id
                   join tabF in tabFlujosServicio
                   on tabE.IdFlujoServicio equals tabF.Id
                   where tab.IdPaso == id && tab.IdMetodo == metodo 
                   select new MensajesMetodosDestinatarioDTO
                   {
                       Id = tab.Id,
                       IdPaso = tab.IdPaso
                       ,
                       IdMetodo = tab.IdMetodo
                       ,
                       IdMensaje = tab.IdMensaje
                       ,
                       TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                       ,
                       ValorBusqueda = tab.ValorBusqueda
                       ,
                       TipoPrivacidad = tab.IdTipoPrivacidad
                       ,
                       IndVigenteFlujoServicio = tabF.IndVigente
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
                       IndEliminado = tab.IndEliminado
                       ,
                       FechaValidez = tab.FechaValidez
                   }).FirstOrDefault();
    return entidad;
}
///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerConstantesDTO(int? id, int idConstantes, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == idConstantes && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        IdMetodo = tab.IdMetodo
                        ,
                        IdMensaje = tab.IdMensaje
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
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
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}


///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerCLaseCorreoDTO(int? id, int tipo, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == tipo && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        IdMetodo = tab.IdMetodo
                        ,
                        IdMensaje = tab.IdMensaje
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
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
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}


///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerConstantesCorreoDTO(int? id, int tipo, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == tipo && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        IdMetodo = tab.IdMetodo
                        ,
                        IdMensaje = tab.IdMensaje
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
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
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}
///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerClaseParaDTO(int? id, int clase, int tipoPrivacidad, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == clase && tab.IdTipoPrivacidad == tipoPrivacidad
                          && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerClaseCCDTO(int? id, int clase, int tipoPrivacidad, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == clase && tab.IdTipoPrivacidad == tipoPrivacidad
                          && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerClaseCCODTO(int id, int clase, int tipoPrivacidad, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == clase && tab.IdTipoPrivacidad == tipoPrivacidad
                    && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerClasePara2DTO(int? id, int constante, int tipoProvacidad, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == constante && tab.IdTipoPrivacidad == tipoProvacidad
                          && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerClaseCC2DTO(int? id, int constante, int tipoProvacidad, int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == constante && tab.IdTipoPrivacidad == tipoProvacidad
                          && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableMensajesMetodosDestinatarioDTO.</returns>
public IEnumerable<MensajesMetodosDestinatarioDTO> ObtenerClaseCCO2DTO(int id, int constante, int tipoProvacidad,int metodo)
{
    var tabMensajesMetodosDestinatario = udt.Sesion.CreateObjectSet<MensajesMetodosDestinatario>();
    var coleccion = from tab in tabMensajesMetodosDestinatario
                    //orderby tab.Id
                    where tab.IdPaso == id && tab.IdTipoBusquedaDestinatario == constante && tab.IdTipoPrivacidad == tipoProvacidad
                    && tab.IdMetodo == metodo
                    select new MensajesMetodosDestinatarioDTO
                    {
                        Id = tab.Id,
                        IdPaso = tab.IdPaso
                        ,
                        TipoBusquedaDestinatario = tab.IdTipoBusquedaDestinatario
                        ,
                        ValorBusqueda = tab.ValorBusqueda
                        ,
                        TipoPrivacidad = tab.IdTipoPrivacidad
                        ,
                        IndVigente = tab.IndVigente
                        ,
                        IndEliminado = tab.IndEliminado
                        ,
                        FechaValidez = tab.FechaValidez
                    };
    coleccion = UtilidadesDTO<MensajesMetodosDestinatarioDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion;
}  
      #endregion DTO
	}
}