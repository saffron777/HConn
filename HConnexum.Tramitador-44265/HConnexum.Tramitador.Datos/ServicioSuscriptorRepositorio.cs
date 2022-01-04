using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Data;
using HConnexum.Servicios.Servicios;
using System.Data.SqlClient;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: ServiciosSuscriptorRepositorio.</summary>
	public sealed class ServiciosSuscriptorRepositorio : RepositorioBase<ServiciosSuscriptor>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase ServicioSucursalRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public ServiciosSuscriptorRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		#endregion "Constructores"

        public DataTable ObtenerDTO(int IdSuscriptor)
        {

            /* var tabServicio = udt.Sesion.CreateObjectSet<Servicio>();
            var tabServiciosTiposSucriptor = udt.Sesion.CreateObjectSet<ServiciosTiposSucriptor>();
            var tabDetallesTiposSuscriptor = udt.Sesion.CreateObjectSet<DetallesTiposSuscriptor>();
            var tabSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();
            var coleccion = (from tab in tabServicio
                             join tabS in tabServiciosTiposSucriptor on tab.Id equals tabS.IdServicio
                             join tabDTS in tabDetallesTiposSuscriptor on tabS.IdTipoSuscriptor equals tabDTS.IdTipoSuscriptor
                             join tabSc in tabSuscriptor on tabDTS.IdSuscriptor equals tabSc.Id
                             where tabSc.Id == IdSuscriptor
                            select new CasoDTO
                            {
                                Id = tab.Id,
                                NombreServicioSuscriptor = tab.Nombre
                            }).Distinct();*/
            var tabServiciosSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
            var tabDetallesTiposSuscriptores = udt.Sesion.CreateObjectSet<DetallesTiposSuscriptor>();
            var coleccion = (from tab in tabServiciosSuscriptor
                             join tabD in tabDetallesTiposSuscriptores on tab.IdDetalleTipoSuscriptor equals tabD.Id
                             where tabD.IdSuscriptor == IdSuscriptor &&
                                   tab.IndVigente == true &&
                                   tab.IndEliminado == false &&
                                   tab.FechaValidez <= DateTime.Now
                             select new
                             {
                                 Id = tab.Id,
                                 Nombre = tab.Nombre
                             }).Distinct();

            return (LinqtoDataSetMethods.CopyToDataTable(coleccion));
        }

        public DataTable ObtenerporId(int IdServicioSuscriptor)
        {


            var tabServiciosSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
            var tabDetallesTiposSuscriptores = udt.Sesion.CreateObjectSet<DetallesTiposSuscriptor>();
            var coleccion = (from tab in tabServiciosSuscriptor
                             join tabD in tabDetallesTiposSuscriptores on tab.IdDetalleTipoSuscriptor equals tabD.Id
                             where tab.Id == IdServicioSuscriptor &&
                                   tab.IndVigente == true &&
                                   tab.IndEliminado == false &&
                                   tab.FechaValidez <= DateTime.Now
                             select new
                             {
                                 Id = tab.Id,
                                 Nombre = tab.Nombre
                             });

            return (LinqtoDataSetMethods.CopyToDataTable(coleccion));
        }
        public DataSet obtenerServiciosASolicitarDeUnSuscriptor(int idSuscriptor) 
        {
            DataSet ds= new DataSet();
            List<ListaServiciosASolicitarDeUnSuscriptor> ListaServiciosASolicitar = new List<ListaServiciosASolicitarDeUnSuscriptor>();
            using (BD_HC_Tramitador dataBase = new BD_HC_Tramitador())
            {
                ListaServiciosASolicitar = dataBase.ServicioASolicitarDeUnSuscriptor(idSuscriptor).ToList();

            }
            if (ListaServiciosASolicitar.Count > 0)
            {
                var Servicios = (from ser in ListaServiciosASolicitar
                                 select ser);

                ds.Tables.Add(LinqtoDataSetMethods.CopyToDataTable(Servicios));
            }

            return ds;
        }
	}
}
