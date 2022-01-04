using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: BuzonMensajeRepositorio.</summary>	
    public sealed class  BuzonMensajeRepositorio : RepositorioBase<BuzonMensaje>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase BuzonMensajeRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public BuzonMensajeRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableBuzonMensajeDTO.</returns>
public IEnumerable<BuzonMensajeDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
{
	var tabBuzonMensaje = udt.Sesion.CreateObjectSet<BuzonMensaje>();
	var coleccion = from tab in tabBuzonMensaje
			orderby "it." + orden  
			select new BuzonMensajeDTO {
			Id = tab.Id
,IdMovimiento = tab.IdMovimiento
,IdPasoMensaje = tab.IdPasoMensaje
,Destinatario = tab.Destinatario
,TipoEnvio = tab.TipoEnvio
,Estatus = tab.Estatus
,IdMetodoEnvio = tab.IdMetodoEnvio
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<BuzonMensajeDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<BuzonMensajeDTO>.FiltrarColeccionEliminacion(coleccion, true);
	Conteo = UtilidadesDTO<BuzonMensajeDTO>.Conteo;
	return coleccion; 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableBuzonMensajeDTO.</returns>
public IEnumerable<BuzonMensajeDTO> ObtenerDTO()
{
	var tabBuzonMensaje = udt.Sesion.CreateObjectSet<BuzonMensaje>();
	var coleccion = from tab in tabBuzonMensaje 
			select new BuzonMensajeDTO {
			Id = tab.Id
,IdMovimiento = tab.IdMovimiento
,IdPasoMensaje = tab.IdPasoMensaje
,Destinatario = tab.Destinatario
,TipoEnvio = tab.TipoEnvio
,Estatus = tab.Estatus
,IdMetodoEnvio = tab.IdMetodoEnvio
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<BuzonMensajeDTO>.FiltrarColeccionAuditoria(coleccion, false);
	return coleccion; 
}

		#endregion DTO
	}
}