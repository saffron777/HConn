using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: SolicitudRepositorio.</summary>	
    public sealed class  SolicitudRepositorio : RepositorioBase<Solicitud>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase SolicitudRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public SolicitudRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

        public SolicitudDTO ObtenerOrigen(int idCaso)
        {
            var tabSolicitud = udt.Sesion.CreateObjectSet<Solicitud>();
            var tabCasos = udt.Sesion.CreateObjectSet<Caso>();

            var coleccion = (from tab in tabSolicitud
                             join tab1 in tabCasos on tab.Id equals tab1.IdSolicitud
                             where tab1.Id == idCaso
                             select new SolicitudDTO
                             {
                                 Id = tab.Id,
                                 Origen = tab.Origen,
                                 IdCasoExterno2 = tab.IdCasoExterno2
                             }).FirstOrDefault();
            return coleccion;
        }

        public SolicitudDTO ObtenerSolicitudporMovimiento(int idMovimiento)
        {
            var tabSolicitud = udt.Sesion.CreateObjectSet<Solicitud>();
            var tabCasos = udt.Sesion.CreateObjectSet<Caso>();
            var tabMovimientos = udt.Sesion.CreateObjectSet<Movimiento>();
            
            var coleccion = ( from tabS in tabSolicitud
                              join tabC in tabCasos on tabS.Id equals tabC.IdSolicitud
                              join tab in tabMovimientos on tabC.IdMovimiento equals tab.Id
                              where tab.Id == idMovimiento
                             select new SolicitudDTO
                             {   Id = tabS.Id,
                                 IdCasoExterno = tabS.IdCasoExterno,
                                 IdCasoExterno2 = tabS.IdCasoExterno2,
                                 IdCasoExterno3 = tabS.IdCasoExterno3
                             }).FirstOrDefault();
            return coleccion;
        }
		#endregion DTO
	}
}