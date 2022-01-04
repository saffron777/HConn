using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
    ///<summary>Clase: PasoRepositorio.</summary>	
    public sealed class PasoRepositorio : RepositorioBase<Paso>
    {
        #region "ConstructoreS"
        ///<summary>Constructor de la clase PasoRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public PasoRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
        }

        #endregion "Constructores"
		
        #region DTO
        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
        ///<param name="pagina">Nro pagina en el RadGrid.</param>
        ///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
        ///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public IEnumerable<PasoDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var coleccion = from tab in tabPaso
                            orderby "it." + orden
                            select new PasoDTO
                            {
                                Id = tab.Id,
                                IdEtapa = tab.IdEtapa,
                                IdTipoPaso = tab.IdTipoPaso,
                                IdSubServicio = tab.IdSubServicio,
                                IdAlerta = tab.IdAlerta,
                                Nombre = tab.Nombre,
                                Observacion = tab.Observacion,
                                IndObligatorio = tab.IndObligatorio,
                                CantidadRepeticion = tab.CantidadRepeticion,
                                IndRequiereRespuesta = tab.IndRequiereRespuesta,
                                IndCerrarEtapa = tab.IndCerrarEtapa,
                                SlaTolerancia = tab.SlaTolerancia,
                                IndSeguimiento = tab.IndSeguimiento,
                                IndAgendable = tab.IndAgendable,
                                IndAnulacion = tab.IndAnulacion,
                                IndCerrarServicio = tab.IndCerrarServicio,
                                Reintentos = tab.Reintentos,
                                IndSegSubServicio = tab.IndSegSubServicio,
                                PorcSlaCritico = tab.PorcSlaCritico,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<PasoDTO>.Conteo;
            return coleccion; 
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public IEnumerable<PasoDTO> ObtenerDTO()
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var coleccion = from tab in tabPaso orderby tab.Nombre
                            select new PasoDTO
                            {
                                Id = tab.Id,
                                Nombre = tab.Nombre,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion; 
        }
        public IEnumerable<PasoDTO> ObtenerDTO(int IdFlujoServicio)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapa =udt.Sesion.CreateObjectSet<Etapa>();
            var coleccion = from tab in tabPaso
                            join tab1 in tabEtapa on tab.IdEtapa equals tab1.Id
                           where tab1.IdFlujoServicio== IdFlujoServicio
                            orderby tab.Nombre
                            select new PasoDTO
                            {
                                Id = tab.Id,
                                Nombre = tab.Nombre,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            if (coleccion.Count() > 0)
            {
                coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            }
            return coleccion;
        }

        /// <summary>
        ///  Metodo que retorna una lista de pasos por un IdIdFlujoServicio
        /// </summary>
        /// <param name="IdFlujoServicio">parámetro de busqueda</param>
        /// <returns>Metodo que retorna una lista de pasos por un IdIdFlujoServicio</returns>
        public IEnumerable<PasoDTO> ObtenerDTOPasosXml(int IdFlujoServicio)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var coleccion = from tab in tabPaso
                            join tab1 in tabEtapa on tab.IdEtapa equals tab1.Id
                            join tab2 in tabTipoPaso on tab.IdTipoPaso equals tab2.Id
                            where tab1.IdFlujoServicio == IdFlujoServicio
                            orderby tab.Nombre
                            select new PasoDTO
                            {
                                Id = tab.Id,
                                Nombre = tab.Nombre,
                                NombreTipoPaso = tab2.Descripcion,
                                Orden = tab.Orden,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            if (coleccion.Count() > 0)
            {
                coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            }
            return coleccion;
        }
        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public IEnumerable<PasoDTO> ObtenerPasoporIdDTO(int id)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var coleccion = from tab in tabPaso
                            where tab.Id == id
                            orderby tab.Nombre
                            select new PasoDTO
                            {
                                Id = tab.Id,
                                Nombre = tab.Nombre,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionEliminacion(coleccion, false);
            return coleccion;
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public IEnumerable<PasoDTO> ObtenerDTOPasosEtapa(int idEtapa, int idFlujoServicio)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var coleccion = from tab in tabPaso
                            join tabE in tabEtapa
                            on tab.IdEtapa equals tabE.Id
                            where tab.IdEtapa == idEtapa
                            select new PasoDTO
                            {
                                Id = tab.Id,
                                IdSubServicio = tab.IdSubServicio,
                                IdEtapa = tab.IdEtapa,
                                Nombre = tab.Nombre,
                                CreadoPor = tab.CreadoPor,
                                FechaCreacion = tab.FechaCreacion,
                                ModificadoPor = tab.ModificadoPor,
                                FechaModificacion = tab.FechaModificacion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public IEnumerable<PasoDTO> ObtenerDTOProximoPaso(int idFlujoServicio, int idTipoProximoPaso)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var coleccion = from tabP in tabPaso
                            join tabTP in tabTipoPaso 
                            on tabP.IdTipoPaso equals tabTP.Id
                            join tabE in tabEtapa
                            on tabP.IdEtapa equals tabE.Id
                            where tabE.IdFlujoServicio==idFlujoServicio &&
                                  tabP.IdTipoPaso == idTipoProximoPaso
                            select new PasoDTO
                            {
                                Id = tabP.Id,
                                Nombre = tabP.Nombre,
                                IndVigente = tabP.IndVigente,
                                FechaValidez = tabP.FechaValidez,
                                IndEliminado = tabP.IndEliminado
                            };
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public IEnumerable<PasoDTO> ObtenerDTOProximoPasoModificarAccionDelPaso(int idPasoDestino)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var coleccion = from tabP in tabPaso
                            join tabTP in tabTipoPaso
                            on tabP.IdTipoPaso equals tabTP.Id
                            join tabE in tabEtapa
                            on tabP.IdEtapa equals tabE.Id
                            where tabP.Id == idPasoDestino
                            select new PasoDTO
                            {
                                Id = tabP.Id,
                                Nombre = tabP.Nombre,
                                IndVigente = tabP.IndVigente,
                                FechaValidez = tabP.FechaValidez,
                                IndEliminado = tabP.IndEliminado
                            };
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableTipoPasoDTO.</returns>
        public IEnumerable<TipoPasoDTO> ObtenerDTOTipoPaso(int id)
        {
            var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var coleccion = (from tab in tabTipoPaso
                             join tabP in tabPaso
                             on tab.Id equals tabP.IdTipoPaso
                             join tabE in tabEtapa
                             on tabP.IdEtapa equals tabE.Id
                             where tabP.Id == id
                             select new TipoPasoDTO
                             {
                                 Id = tab.Id,
                                 Descripcion = tab.Descripcion,
                                 IdTipoPaso = tabP.IdTipoPaso,
                                 IndVigente = tab.IndVigente,
                                 FechaValidez = tab.FechaValidez,
                                 IndEliminado = tab.IndEliminado
                             }).Distinct();
            coleccion = UtilidadesDTO<TipoPasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        /// <summary>
        /// Metodo encargado de cargar los datos de la lista de EtapaMaestroDetalle.aspx
        /// </summary>
        /// <returns>retorna un IEnumerablePasoDTO</returns>
        public IEnumerable<PasoDTO> ObtenerPasosDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var coleccion = from tab in tabPaso
                            join tabTP in tabTipoPaso on tab.IdTipoPaso equals tabTP.Id
                            orderby tab.Nombre
                            select new PasoDTO
                            {
                                Id = tab.Id,
                                IdEtapa = tab.IdEtapa,
                                IdTipoPaso = tab.IdTipoPaso,
                                Nombre = tab.Nombre,
                                Observacion = tab.Observacion,
                                IndObligatorio = tab.IndObligatorio,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado,
                                NombreTipoPaso = tabTP.Descripcion,
                                Orden = tab.Orden
                            };
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<PasoDTO>.Conteo;
            return UtilidadesDTO<PasoDTO>.EncriptarId(coleccion);
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public PasoDTO ObtenerDTOEtapa(int idPaso)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var tabFlujoServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var coleccion = (from tab in tabPaso
                             join tabE in tabEtapa on tab.IdEtapa equals tabE.Id
                             join tabFS in tabFlujoServicio on tabE.IdFlujoServicio equals tabFS.Id
                             where tab.Id == idPaso
                             select new PasoDTO
                             {
                                 Id = tab.Id,
                                 IdEtapa = tab.IdEtapa,
                                 IdTipoPaso = tab.IdTipoPaso,
                                 Nombre = tabE.Nombre,
                                 IdServicioSuscriptor = tabFS.IdServicioSuscriptor,
                                 CreadoPor = tab.CreadoPor,
                                 FechaCreacion = tab.FechaCreacion,
                                 ModificadoPor = tab.ModificadoPor,
                                 FechaModificacion = tab.FechaModificacion,
                                 IndVigente = tab.IndVigente,
                                 FechaValidez = tab.FechaValidez,
                                 IndEliminado = tab.IndEliminado
                             }).SingleOrDefault();
            return coleccion;
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableChadePasoDTO.</returns>
        public ChadePasoDTO ObtenerDTOIdSuscriptor(int idPaso)
        {
            var tabPasos = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapas = udt.Sesion.CreateObjectSet<Etapa>();
            var tabFlujosServicios = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var coleccion = (from tabP in tabPasos
                             join tabE in tabEtapas on tabP.IdEtapa equals tabE.Id
                             join tabFS in tabFlujosServicios on tabE.IdFlujoServicio equals tabFS.Id
                             where tabP.Id == idPaso
                             select new ChadePasoDTO
                             {
                                 IdSuscriptor = tabFS.IdSuscriptor
                             }).SingleOrDefault();
            return coleccion;
        }

        public PasoDTO ObtenerPorIdPaso(int idPaso)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var tabFlujosServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var entidad = (from tab in tabPaso
                           join tabE in tabEtapa on tab.IdEtapa equals tabE.Id
                           join tabF in tabFlujosServicio on tabE.IdFlujoServicio equals tabF.Id
                           where tab.Id == idPaso
                           select new PasoDTO
                           {
                               Nombre = tab.Nombre,
                               IdSuscriptor = tabF.IdSuscriptor,
                               IdFlujoServicio = tabF.Id,
                               IdServicioSuscriptor = tabF.IdServicioSuscriptor,
                               VersionFlujoServicio = tabF.Version,
                               IndVigenteFlujoServicio = tabF.IndVigente,
                               IdEtapa = tab.IdEtapa,
                               NombreEtapa = tabE.Nombre
                           }).SingleOrDefault();
            return entidad;
        }

        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerablePasoDTO.</returns>
        public PasoDTO ObtenerDTOPasoRespuesta(int idPasoRespuesta)
        {
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabPasoRespuesta = udt.Sesion.CreateObjectSet<PasosRepuesta>();
            var coleccion = (from tab in tabPaso
                             join tabPR in tabPasoRespuesta 
                             on tab.Id equals tabPR.IdPaso
                             where tabPR.Id == idPasoRespuesta
                             select new PasoDTO
                             {
                                 Id = tab.Id,
                                 IdEtapa = tab.IdEtapa,
                                 IdTipoPaso = tab.IdTipoPaso,
                                 Nombre = tab.Nombre,
                                 CreadoPor = tab.CreadoPor,
                                 FechaCreacion = tab.FechaCreacion,
                                 ModificadoPor = tab.ModificadoPor,
                                 FechaModificacion = tab.FechaModificacion,
                                 IndVigente = tab.IndVigente,
                                 FechaValidez = tab.FechaValidez,
                                 IndEliminado = tab.IndEliminado
                             }).SingleOrDefault();
            return coleccion;
        }

        public int ObtenerSLA(int IdEtapa)
        {
            var tabPasos = udt.Sesion.CreateObjectSet<Paso>();
            var coleecion = from tab in tabPasos
                            where tab.IdEtapa == IdEtapa & tab.IndEliminado==false & tab.IndVigente==true 
                            select new 
                            {
                                SlaPromedio = tab.SlaTolerancia
                            };
          
            if (coleecion.Count() <= 0)
                return 0;
            else 
                return coleecion.Sum(a => a.SlaPromedio);
        }
        ///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
        ///<returns>Retorna IEnumerableTipoPasoDTO.</returns>
        public IEnumerable<PasoDTO> ObtenerDTOPasoDesborde(int idFlujoServicio)
        {
            var tabTipoPaso = udt.Sesion.CreateObjectSet<TipoPaso>();
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabEtapa = udt.Sesion.CreateObjectSet<Etapa>();
            var tabFlujoServicio = udt.Sesion.CreateObjectSet<FlujosServicio>();
            var coleccion = (//from tab in tabTipoPaso

                             from tabP in tabPaso //on tab.Id equals tabP.IdTipoPaso
                             join tabE in tabEtapa on tabP.IdEtapa equals tabE.Id
                             join tabFS in tabFlujoServicio on tabE.IdFlujoServicio equals tabFS.Id
                             where tabFS.Id== idFlujoServicio //&& tabP.IndEliminado == false && tabP.IndVigente == true
                             select new PasoDTO
                             {
                                 Id = tabP.Id,
                                 Nombre = tabP.Nombre,
                                 IdTipoPaso = tabP.IdTipoPaso,
                                 IndVigente = tabP.IndVigente,
                                 FechaValidez = tabP.FechaValidez,
                                 IndEliminado = tabP.IndEliminado
                             }).Distinct();
            coleccion = UtilidadesDTO<PasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
            return coleccion;
        }

        public PasoDTO PasoHijo(int idMovimiento)
        {
            
            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();

            var coleccion = (
                             from tabM in tabMovimiento 
                             join tabP in tabPaso on tabM.IdPaso equals tabP.Id
                             where tabM.Id == idMovimiento 
                             select new PasoDTO
                             {
                                 Id = tabP.Id,
                                 Nombre = tabP.Nombre,
                                 IdTipoPaso = tabP.IdTipoPaso,
                                 IndVigente = tabP.IndVigente,
                                 FechaValidez = tabP.FechaValidez,
                                 IndEliminado = tabP.IndEliminado,
                                 IndEncadenado = tabP.IndEncadenado,
                                 idMovimientoPadre = tabM.IdMovimientoPadre
                                 
                             }).FirstOrDefault<PasoDTO>();
            
            
            return coleccion;
        }

        public PasoDTO PasoActualMovimiento(int idMovimiento)
        {

            var tabPaso = udt.Sesion.CreateObjectSet<Paso>();
            var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();

            var coleccion = (
                             from tabM in tabMovimiento
                             join tabP in tabPaso on tabM.IdPaso equals tabP.Id
                             where tabM.Id == idMovimiento
                             select new PasoDTO
                             {
                                 Id = tabP.Id,
                                 Nombre = tabP.Nombre,
                                 IdTipoPaso = tabP.IdTipoPaso,
                                 IndVigente = tabP.IndVigente,
                                 FechaValidez = tabP.FechaValidez,
                                 IndEliminado = tabP.IndEliminado,
                                 IndEncadenado = tabP.IndEncadenado,
                                 idMovimientoPadre = tabM.IdMovimientoPadre

                             }).FirstOrDefault<PasoDTO>();


            return coleccion;
        }


        
        #endregion DTO
    }
}
