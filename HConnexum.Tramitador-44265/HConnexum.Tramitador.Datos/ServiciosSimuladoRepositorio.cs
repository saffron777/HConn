using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

    ///<summary>Clase: ServiciosSimuladoRepositorio.</summary>	
    public sealed class ServiciosSimuladoRepositorio : RepositorioBase<ServicioSimulado>
    {
        #region "ConstructoreS"
        ///<summary>Constructor de la clase ServiciosSimuladoRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public ServiciosSimuladoRepositorio(IUnidadDeTrabajo udt)
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
        ///<returns>Retorna IEnumerableServiciosSimuladoDTO.</returns>
        public IEnumerable<ServiciosSimuladoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            var tabServiciosSimulado = udt.Sesion.CreateObjectSet<ServicioSimulado>(); 
            var tabServiciosSuscriptor = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>(); //Nombre del Servicio 
            var tabSuscriptor = udt.Sesion.CreateObjectSet<Suscriptor>();  //Suscriptor 
            var tabUsuariosSucriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();  //Usuario Incorporador 
            var tabDatosBase = udt.Sesion.CreateObjectSet<DatosBase>();  //Usuario Incorporador 
            var coleccion = from tabSS in tabServiciosSimulado
                            join tabServsus in tabServiciosSuscriptor on tabSS.IdServicioSuscriptor equals tabServsus.Id
                            join tabSus in tabSuscriptor on tabSS.IdSuscriptorASimular equals tabSus.Id
                            join tabUS in tabUsuariosSucriptor on tabSS.CreadoPor equals tabUS.Id
                            join tabDB in tabDatosBase on tabUS.IdUsuario equals tabDB.IdUsuario
                            select new ServiciosSimuladoDTO
                            {
                                Id = tabSS.Id,
                                NombreServicio = tabServsus.Nombre,
                                NombreSuscriptor = tabSus.Nombre,
                                UsuarioIncorporador = tabDB.Nombre1 +  " " +  tabDB.Nombre2 + " " +  tabDB.Apellido1 + " " +  tabDB.Apellido2,
                                FechaInicio = tabSS.FechaInicio,
                                FechaFin = tabSS.FechaFin,
                                CreadoPor = tabSS.CreadoPor,
                                FechaCreacion = tabSS.FechaCreacion,
                                ModificadoPor = tabSS.ModificadoPor,
                                FechaModificacion = tabSS.FechaModificacion,
                                FechaValidez = tabSS.FechaValidez,
                                IndEliminado = tabSS.IndEliminado,
                                IndVigente = tabSS.IndVigente
                            };
            coleccion = UtilidadesDTO<ServiciosSimuladoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<ServiciosSimuladoDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = coleccion.Count();
            return UtilidadesDTO<ServiciosSimuladoDTO>.EncriptarId(coleccion);
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableServiciosSimuladoDTO.</returns>
        public IEnumerable<ServiciosSimuladoDTO> ObtenerDTO()
        {
            var tabServiciosSimulado = udt.Sesion.CreateObjectSet<ServicioSimulado>();
            var coleccion = from tab in tabServiciosSimulado
                            orderby tab.Id
                            select new ServiciosSimuladoDTO
                            {
                                Id = tab.Id,
                                IdServicioSuscriptor = tab.IdServicioSuscriptor,
                                IdSuscriptorASimular = tab.IdSuscriptorASimular,
                                FechaInicio = tab.FechaInicio,
                                FechaFin = tab.FechaFin,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado,
                                IndVigente = tab.IndVigente
                            };
            coleccion = UtilidadesDTO<ServiciosSimuladoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        public IEnumerable<ServiciosSimuladoDTO> ObtenerServicioSuscriptorDTO(int IdSuscriptorConectado)
        {
            var tabFS = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var tabSS = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();
                                    
            var coleccion = (from f in tabFS
                             from ss in tabSS
                             where
                              f.IdSuscriptor == IdSuscriptorConectado &&
                              f.IndSimulable == true &&
                              f.IndEliminado == false &&
                              f.IndVigente == true &&
                              f.FechaValidez < DateTime.Now &&
                              f.Version == ((from fv in tabFS
                                              where
                                                fv.IdServicioSuscriptor == f.IdServicioSuscriptor &&
                                                fv.IndSimulable == true &&
                                                fv.IndEliminado == false &&
                                                fv.IndVigente == true &&
                                                fv.FechaValidez < DateTime.Now
                                              select new
                                              {
                                                  fv.Version
                                              })).Max(p => p.Version) &&
                              ss.Id == f.IdServicioSuscriptor &&
                              ss.IndEliminado == false &&
                              ss.IndVigente == true &&
                              ss.FechaValidez < DateTime.Now
                             select new ServiciosSimuladoDTO
                             {
                                 IdServicioSuscriptor =  f.IdServicioSuscriptor,
                                 NombreServicio = ss.Nombre
                             });
            return coleccion;
        }

        public IEnumerable<FlujosServicioDTO> ObtenerServicioSuscriptor(int IdSuscriptorConectado)
        {
            var tabFS = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var tabSS = udt.Sesion.CreateObjectSet<ServiciosSuscriptor>();

            var coleccion = (from f in tabFS
                             from ss in tabSS
                             where
                              f.IdSuscriptor == IdSuscriptorConectado &&
                              f.IndSimulable == true &&
                              f.IndEliminado == false &&
                              f.IndVigente == true &&
                              f.FechaValidez < DateTime.Now &&
                              f.Version == ((from fv in tabFS
                                             where
                                               fv.IdServicioSuscriptor == f.IdServicioSuscriptor &&
                                               fv.IndSimulable == true &&
                                               fv.IndEliminado == false &&
                                               fv.IndVigente == true &&
                                               fv.FechaValidez < DateTime.Now
                                             select new
                                             {
                                                 fv.Version
                                             }).Take(1)).Max(p => p.Version) &&
                              ss.Id == f.IdServicioSuscriptor &&
                              ss.IndEliminado == false &&
                              ss.IndVigente == true &&
                              ss.FechaValidez < DateTime.Now
                             select new FlujosServicioDTO
                             {
                                 Id = f.IdServicioSuscriptor,
                                 NombreServicioSuscriptor = ss.Nombre
                             });
            return coleccion;
        }
        #endregion DTO
    }
}