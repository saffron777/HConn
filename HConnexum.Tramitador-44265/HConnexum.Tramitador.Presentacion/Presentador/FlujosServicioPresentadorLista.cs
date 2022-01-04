using System;
using System.Collections.Generic;
using System.Data;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.Linq;

///<summary>Namespace que engloba el presentador Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase FlujosServicioPresentadorLista.</summary>
    public class FlujosServicioPresentadorLista : PresentadorListaBase<FlujosServicio>
    {
        ///<summary>Variable vista de la interfaz IFlujosServicioLista.</summary>
        readonly IFlujosServicioLista vista;
        DataTable DocServAP = new DataTable("DocumentosServicio");
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
        string accionCI;

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public FlujosServicioPresentadorLista(IFlujosServicioLista vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        ///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
        ///<param name="pagina">Indica el número de página del conjunto de registros.</param>
        ///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
        ///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
        public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro)
        {
            try
            {
                FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(unidadDeTrabajo);
                IEnumerable<FlujosServicioDTO> datos = repositorio.ObtenerDTO(orden, pagina, tamañoPagina, parametrosFiltro);
                vista.NumeroDeRegistros = repositorio.Conteo;
                vista.Datos = datos;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        ///<summary>Método encargado de eliminar registros del conjunto.</summary>
        ///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
        public void Eliminar(IList<string> ids)
        {
            try
            {
                unidadDeTrabajo.IniciarTransaccion();
                FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    FlujosServicio _FlujosServicio = repositorio.ObtenerPorId(idDesencriptado);
                    if (!repositorio.EliminarLogico(_FlujosServicio, _FlujosServicio.Id))
                        this.vista.Errores = "Ya Existen registros asociados a esta  " + _FlujosServicio + "";
                }
                unidadDeTrabajo.Commit();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public bool bObtenerRolIndEliminado()
        {
            FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(this.unidadDeTrabajo);
            return repositorio.obtenerRolIndEliminado();
        }

        public void activarEliminado(IList<string> ids)
        {
            try
            {
                this.unidadDeTrabajo.IniciarTransaccion();
                FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(this.unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    FlujosServicio _Aplicacion = repositorio.ObtenerPorId(idDesencriptado);
                    if (!repositorio.activarEliminarLogico(_Aplicacion, _Aplicacion.Id))
                        this.vista.Errores = "Ya Existen registros asociados a esta Aplicacion";
                }
                this.unidadDeTrabajo.Commit();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        #region "Copiar e Importar"
        /// <summary>
        /// Método encargado de copiar o importar toda la informacion de un servicio seleccionado en el conjunto.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accion"></param>
        public void CopiarImportar(string id, string accion)
        {
            try
            {
                accionCI = accion;
                int IdFlujoServicio = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                FlujosServicio fs = new FlujosServicio();
                //FlujosServicio fsC = new FlujosServicio();
                unidadDeTrabajo.IniciarTransaccion();
                 FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(unidadDeTrabajo);
                FlujosServicio fsC = repositorio.ObtenerPorId(IdFlujoServicio);
                fs.IndPublico = fsC.IndPublico;
                fs.IdSuscriptor = fsC.IdSuscriptor;
                if (accion == "Importar")
                    fs.IdOrigen = IdFlujoServicio;
                else
                    fs.IdOrigen = fsC.IdOrigen;
                fs.IdServicioSuscriptor = fsC.IdServicioSuscriptor;
                fs.SlaTolerancia = fsC.SlaTolerancia;
                fs.SlaPromedio = fsC.SlaPromedio;
                fs.Prioridad = fsC.Prioridad;
                fs.IndCms = fsC.IndCms;
                fs.XLMEstructura = fsC.XLMEstructura;
                fs.Version = fsC.Version + 1; 
                fs.IndVigente = false;
                fs.IndEliminado = false;
                fs.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                fs.FechaCreacion = DateTime.Now;
                fs.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                fs.FechaModicacion = DateTime.Now;
                unidadDeTrabajo.MarcarNuevo(fs);
                unidadDeTrabajo.Commit();
                if (accion == "Copiar")
                {
                    CrearServicioSucursal(fsC.ServicioSucursal , fs.Id); //esto lo deben definir
                    CrearDocumentosServicios(fsC.DocumentosServicio, fs.Id);
                }
                CrearEtapas(fsC.Etapa, fs.Id);
                vista.Mensaje = "Proceso satisfactorio";
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
        /// <summary>
        /// Método encargado de crear ServicioSucursal.
        /// </summary>
        /// <param name="AlcancesGeograficoSucursal"></param>
        /// <param name="idFlujoServicio"></param>
        public void CrearServicioSucursal(System.Data.Objects.DataClasses.EntityCollection<ServicioSucursal> ServicioSucursal, int idFlujoServicio)
        {
            unidadDeTrabajo.IniciarTransaccion();
            foreach (ServicioSucursal SS in ServicioSucursal)
            {
                ServicioSucursal ServSucursal = new ServicioSucursal();
                ServSucursal.IdFlujoServicio = idFlujoServicio;
                ServSucursal.IdSucursal = SS.IdSucursal;
               
                ServSucursal.IndVigente = false;
                ServSucursal.IndEliminado = false;
                ServSucursal.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                ServSucursal.FechaCreacion = DateTime.Now;
                ServSucursal.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                ServSucursal.FechaModificacion = DateTime.Now;

                unidadDeTrabajo.MarcarNuevo(ServSucursal);
                unidadDeTrabajo.Commit();

                CrearAlcanceGeografico(SS.AlcanceGeografico, ServSucursal.Id);
            }

           
               

        }
        /// <summary>
        /// Método encargado de crear ServicioSucursal.
        /// </summary>
        /// <param name="AlcancesGeograficoSucursal"></param>
        /// <param name="idFlujoServicio"></param>
        public void CrearAlcanceGeografico(System.Data.Objects.DataClasses.EntityCollection<AlcanceGeografico> AlcancesGeografico, int IdServicioSucursal)
        {
            unidadDeTrabajo.IniciarTransaccion();
            foreach (AlcanceGeografico AG in AlcancesGeografico)
            {
                AlcanceGeografico AlcanceGeo = new AlcanceGeografico();
                AlcanceGeo.IdServicioSucursal = IdServicioSucursal;
                AlcanceGeo.IdPais = AG.IdPais;
                AlcanceGeo.IdDiv1 = AG.IdDiv1;
                AlcanceGeo.IdDiv2 = AG.IdDiv2;
                AlcanceGeo.IdDiv3 = AG.IdDiv3;

                AlcanceGeo.IndVigente = false;
                AlcanceGeo.IndEliminado = false;
                AlcanceGeo.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                AlcanceGeo.FechaCreacion = DateTime.Now;
                AlcanceGeo.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                AlcanceGeo.FechaModificacion = DateTime.Now;

                unidadDeTrabajo.MarcarNuevo(AlcanceGeo);
            }

            if (AlcancesGeografico.Count > 0)
                unidadDeTrabajo.Commit();

        }
        /// <summary>
        /// Método encargado de crear Documentos de un servicio
        /// </summary>
        /// <param name="DocumentosServicios"></param>
        /// <param name="idFlujoServicio"></param>
        public void CrearDocumentosServicios(System.Data.Objects.DataClasses.EntityCollection<DocumentosServicio> DocumentosServicios, int idFlujoServicio)
        {
            DataColumn columnAnterior = new DataColumn();
            columnAnterior.ColumnName = "DocSerAnterior";
            DocServAP.Columns.Add(columnAnterior);

            DataColumn columnPosterior = new DataColumn();
            columnPosterior.ColumnName = "DocSerPosterior";
            DocServAP.Columns.Add(columnPosterior);

            DataRow row; 

           
            foreach (DocumentosServicio DocServicio in DocumentosServicios)
            {
                unidadDeTrabajo.IniciarTransaccion();
                DocumentosServicio DocumentosServicio = new DocumentosServicio();
                DocumentosServicio.IdFlujoServicio = idFlujoServicio;
                DocumentosServicio.IdDocumento = DocServicio.IdDocumento;
                DocumentosServicio.IndDocObligatorio = DocServicio.IndDocObligatorio;
                DocumentosServicio.IndVisibilidad = DocServicio.IndVisibilidad;
                DocumentosServicio.IndVigente = false;
                DocumentosServicio.IndEliminado = false;
                DocumentosServicio.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                DocumentosServicio.FechaCreacion = DateTime.Now;
                DocumentosServicio.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                DocumentosServicio.FechaModificacion = DateTime.Now;

                unidadDeTrabajo.MarcarNuevo(DocumentosServicio);
                unidadDeTrabajo.Commit();
                row = DocServAP.NewRow();
                row["DocSerAnterior"] = DocServicio.Id;
                row["DocSerPosterior"] = DocumentosServicio.Id;
                DocServAP.Rows.Add(row);
            }

        }
        /// <summary>
        /// Método encargado de crear etapas
        /// </summary>
        /// <param name="Etapas"></param>
        /// <param name="idFlujoServicio"></param>
        public void CrearEtapas(System.Data.Objects.DataClasses.EntityCollection<Etapa> Etapas, int idFlujoServicio)
        {
            
            foreach (Etapa etapa in Etapas)
            {
                unidadDeTrabajo.IniciarTransaccion();
                Etapa eNueva = new Etapa();
                eNueva.Nombre = etapa.Nombre;
                eNueva.Orden = etapa.Orden;
                eNueva.SlaPromedio = etapa.SlaPromedio;
                eNueva.SlaTolerancia = etapa.SlaTolerancia;
                eNueva.IndObligatorio = etapa.IndObligatorio;
                eNueva.IndRepeticion = etapa.IndRepeticion;
                eNueva.IndSeguimiento = etapa.IndSeguimiento;
                eNueva.IndInicioServ = etapa.IndInicioServ;
                eNueva.IndCierre = etapa.IndCierre;
                eNueva.IndVigente = false;
                eNueva.IndEliminado = false;
                eNueva.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                eNueva.FechaCreacion = DateTime.Now;
                eNueva.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                eNueva.FechaModicacion = DateTime.Now;
                eNueva.IdFlujoServicio = idFlujoServicio;
                unidadDeTrabajo.MarcarNuevo(eNueva);
                unidadDeTrabajo.Commit();
                
                CrearPasos(etapa.Paso, eNueva.Id);
            }
  

        }
        /// <summary>
        /// Método encargado de crear pasos
        /// </summary>
        /// <param name="Pasos"></param>
        /// <param name="IdEtapa"></param>
        public void CrearPasos(System.Data.Objects.DataClasses.EntityCollection<Paso> Pasos, int IdEtapa) 
        {
                   
            DataTable pasoAnteriorPosterior = new DataTable("Pasos");
                     
            DataColumn columnAnterior = new DataColumn();
            columnAnterior.ColumnName = "PasoAnterior";
            pasoAnteriorPosterior.Columns.Add(columnAnterior);
            
            DataColumn columnPosterior = new DataColumn();
            columnPosterior.ColumnName = "PasoPosterior";
            pasoAnteriorPosterior.Columns.Add(columnPosterior);
            
            DataRow row; 


            foreach (Paso paso in Pasos)
            {
                unidadDeTrabajo.IniciarTransaccion();
                Paso PasoN = new Paso();
                PasoN.IdEtapa = IdEtapa;
                PasoN.IdTipoPaso = paso.IdTipoPaso;
                PasoN.IdSubServicio = paso.IdSubServicio;  
                PasoN.IdAlerta = paso.IdAlerta;
                PasoN.Nombre = paso.Nombre;
                PasoN.Observacion = paso.Observacion;
                PasoN.IndObligatorio = paso.IndObligatorio;
                PasoN.CantidadRepeticion = paso.CantidadRepeticion;
                PasoN.IndRequiereRespuesta = paso.IndRequiereRespuesta;
                PasoN.IndCerrarEtapa = paso.IndCerrarEtapa;
                PasoN.SlaTolerancia = paso.SlaTolerancia;
                PasoN.IndSeguimiento = paso.IndSeguimiento;
                PasoN.IndAgendable = paso.IndAgendable;
                PasoN.IndCerrarServicio = paso.IndCerrarServicio;
                PasoN.Reintentos = paso.Reintentos;
                PasoN.IndSegSubServicio = paso.IndSegSubServicio;
                PasoN.PorcSlaCritico = paso.PorcSlaCritico;
                PasoN.IndIniciaEtapa = paso.IndIniciaEtapa;
                PasoN.URL = paso.URL;
                PasoN.Metodo = paso.Metodo;
                PasoN.Orden = paso.Orden;
                //PgmObtieneRespuestas??
                //EtiqSincroIn??
                //EtiqSincroOut??
                PasoN.IndVigente = false;
                PasoN.IndEliminado = false;
                PasoN.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                PasoN.FechaCreacion = DateTime.Now;
                PasoN.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                PasoN.FechaModificacion = DateTime.Now;
                unidadDeTrabajo.MarcarNuevo(PasoN);
                unidadDeTrabajo.Commit();
                row = pasoAnteriorPosterior.NewRow();
                row["PasoAnterior"] = paso.Id;
                row["PasoPosterior"] = PasoN.Id;
                pasoAnteriorPosterior.Rows.Add(row);
                

            }
            if (accionCI == "Copiar")
            {
                foreach (Paso paso in Pasos)
                {
                    CrearFlujoEjecucion(paso.FlujosEjecucion, pasoAnteriorPosterior);
                    foreach (DataRow pap in pasoAnteriorPosterior.Rows)
                    {
                        if (paso.Id == int.Parse(pap[0].ToString()))
                        {
                            CrearPasosRespuestas(paso.PasosRepuesta, int.Parse(pap[1].ToString()));

                            CrearCHAdePaso(paso.ChadePaso, int.Parse(pap[1].ToString()));
                        
                            CrearParametrosAgenda(paso.ParametrosAgenda, int.Parse(pap[1].ToString()));
                          
                            CrearMensajesMetodosDestinatarios(paso.MensajesMetodosDestinatario, int.Parse(pap[1].ToString()));

                            CrearDocumentosPaso(paso.DocumentosPaso, int.Parse(pap[1].ToString()));

                            
                        }
                       
                    }
                }
            }
           
        }
        /// <summary>
        /// Método encargado de crear parametros de agenda para un paso
        /// </summary>
        /// <param name="ParametrosAgenda"></param>
        /// <param name="IdPaso"></param>
        public void CrearParametrosAgenda(System.Data.Objects.DataClasses.EntityCollection<ParametrosAgenda> ParametrosAgenda, int IdPaso) 
        { 
            unidadDeTrabajo.IniciarTransaccion();
           
            foreach (ParametrosAgenda PA in ParametrosAgenda)
            {
                ParametrosAgenda ParametroAgenda = new ParametrosAgenda();
                ParametroAgenda.IdPaso = IdPaso;                            
                ParametroAgenda.Cantidad = PA.Cantidad;              
                ParametroAgenda.IndVigente = false;
                ParametroAgenda.IndEliminado = false;
                ParametroAgenda.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                ParametroAgenda.FechaCreacion = DateTime.Now;
                ParametroAgenda.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                ParametroAgenda.FechaModificacion = DateTime.Now;
                
                unidadDeTrabajo.MarcarNuevo(ParametroAgenda);
               

            }
            if (ParametrosAgenda.Count > 0)
            unidadDeTrabajo.Commit();
           
        }
        /// <summary>
        /// Método encargado de crear respuestas de pasos
        /// </summary>
        /// <param name="PasosRespuestas"></param>
        /// <param name="IdPaso"></param>
        public void CrearPasosRespuestas(System.Data.Objects.DataClasses.EntityCollection<PasosRepuesta> PasosRespuestas, int IdPaso) 
        {
            unidadDeTrabajo.IniciarTransaccion();
            foreach (PasosRepuesta PR in PasosRespuestas)
            {
                PasosRepuesta PasoRespuesta = new PasosRepuesta();
                PasoRespuesta.IdPaso = IdPaso;
                PasoRespuesta.ValorRespuesta = PR.ValorRespuesta;
                PasoRespuesta.IndCierre = PR.IndCierre;
                PasoRespuesta.Orden = PR.Orden;
                PasoRespuesta.IndVigente = false;
                PasoRespuesta.IndEliminado = false;
                PasoRespuesta.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                PasoRespuesta.FechaCreacion = DateTime.Now;
                PasoRespuesta.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                PasoRespuesta.FechaModificacion = DateTime.Now;
				//Campo nuevo ingresado por Hanlly el 25/03/2015
				//PasoRespuesta.DescripcionRespuesta = PR.DescripcionRespuesta;
				//PasoRespuesta.FechaValidez = DateTime.Now;
                unidadDeTrabajo.MarcarNuevo(PasoRespuesta);
            }
            if (PasosRespuestas.Count > 0)
                unidadDeTrabajo.Commit();

        }
        /// <summary>
        /// Método encargado de crear CHA de Paso
        /// </summary>
        /// <param name="ChadePasos"></param>
        /// <param name="IdPaso"></param>
        public void CrearCHAdePaso(System.Data.Objects.DataClasses.EntityCollection<ChadePaso> ChadePasos, int IdPaso)
        { 
             unidadDeTrabajo.IniciarTransaccion();
             foreach (ChadePaso ChaP in ChadePasos)
             {
                 ChadePaso ChaPaso = new ChadePaso();
                 ChaPaso.IdPasos = IdPaso;
                 ChaPaso.IdCargosuscriptor = ChaP.IdCargosuscriptor;
                 ChaPaso.IdHabilidadSuscriptor = ChaP.IdHabilidadSuscriptor;
                 ChaPaso.IdAutonomiaSuscriptor = ChaP.IdAutonomiaSuscriptor;
                 ChaPaso.IndVigente = false;
                 ChaPaso.IndEliminado = false;
                 ChaPaso.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                 ChaPaso.FechaCreacion = DateTime.Now;
                 ChaPaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                 ChaPaso.FechaModificacion = DateTime.Now;

                 unidadDeTrabajo.MarcarNuevo(ChaPaso);
             }

             if (ChadePasos.Count > 0)
                 unidadDeTrabajo.Commit();

        }
        /// <summary>
        /// Método encargado de crear MensajesMetodosDestinatarios
        /// </summary>
        /// <param name="MensajesMetodosDestinatarios"></param>
        /// <param name="IdPaso"></param>
        public void CrearMensajesMetodosDestinatarios(System.Data.Objects.DataClasses.EntityCollection<MensajesMetodosDestinatario> MensajesMetodosDestinatarios, int IdPaso)
        {
             unidadDeTrabajo.IniciarTransaccion();
             foreach (MensajesMetodosDestinatario MMD in MensajesMetodosDestinatarios)
             {
                 MensajesMetodosDestinatario MensajeMetodoDestinatario = new MensajesMetodosDestinatario();
                 MensajeMetodoDestinatario.IdPaso = IdPaso;
                 MensajeMetodoDestinatario.IdMetodo = MMD.IdMetodo;
                 MensajeMetodoDestinatario.IdMensaje = MMD.IdMensaje;
                 MensajeMetodoDestinatario.IdTipoBusquedaDestinatario = MMD.IdTipoBusquedaDestinatario;
                 MensajeMetodoDestinatario.ValorBusqueda = MMD.ValorBusqueda;
                 MensajeMetodoDestinatario.IdTipoPrivacidad = MMD.IdTipoPrivacidad;
                 MensajeMetodoDestinatario.IndVigente = false;
                 MensajeMetodoDestinatario.IndEliminado = false;
                 MensajeMetodoDestinatario.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                 MensajeMetodoDestinatario.FechaCreacion = DateTime.Now;
                 MensajeMetodoDestinatario.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                 MensajeMetodoDestinatario.FechaModificacion = DateTime.Now;

                 unidadDeTrabajo.MarcarNuevo(MensajeMetodoDestinatario);
             }

             if (MensajesMetodosDestinatarios.Count > 0)
                 unidadDeTrabajo.Commit();
        }
        /// <summary>
        /// Método encargado de crear flujos de ejecución
        /// </summary>
        /// <param name="FlujosEjec"></param>
        /// <param name="PasosAP"></param>
        public void CrearFlujoEjecucion(System.Data.Objects.DataClasses.EntityCollection<FlujosEjecucion> FlujosEjec, DataTable PasosAP) 
        {
             //26/03/2012 requerimiento postergado por ABrizuela / RPichardo
                unidadDeTrabajo.IniciarTransaccion();
                foreach (FlujosEjecucion FE in FlujosEjec)
                {
                    FlujosEjecucion FlujoEjecucion = new FlujosEjecucion();
                    foreach (DataRow row in PasosAP.Rows)
                    {
                        //if(FE.IdPasoOrigen == int.Parse(row[0].ToString())) 
                        //    FlujoEjecucion.IdPasoOrigen = int.Parse(row[1].ToString());
                        if(FE.IdPasoDestino == int.Parse(row[0].ToString()))
                            FlujoEjecucion.IdPasoDestino = int.Parse(row[1].ToString());
                    }
                    FlujoEjecucion.IndReinicioRepeticion = FE.IndReinicioRepeticion;
                    //FlujoEjecucion.IdRespuesta = FE.IdRespuesta;
                    FlujoEjecucion.Condicion = FE.Condicion;
                    FlujoEjecucion.IndVigente = false;
                    FlujoEjecucion.IndEliminado = false;
                    FlujoEjecucion.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    FlujoEjecucion.FechaCreacion = DateTime.Now;
                    FlujoEjecucion.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    FlujoEjecucion.FechaModicacion = DateTime.Now;
                    unidadDeTrabajo.MarcarNuevo(FlujoEjecucion);
                }
                if (FlujosEjec.Count > 0)
                    unidadDeTrabajo.Commit();

        }
        /// <summary>
        /// Método encargado de crear documentos de un paso
        /// </summary>
        /// <param name="DocumentPasos"></param>
        /// <param name="IdPaso"></param>
        public void CrearDocumentosPaso(System.Data.Objects.DataClasses.EntityCollection<DocumentosPaso> DocumentPasos, int IdPaso)
        {
            unidadDeTrabajo.IniciarTransaccion();
            foreach (DocumentosPaso DocP in DocumentPasos)
            {
                DocumentosPaso DocumentoPaso = new DocumentosPaso();
                DocumentoPaso.IdPaso = IdPaso;
                foreach (DataRow dsap in DocServAP.Rows)
                {
                    if (DocP.IdDocumentoServicio == int.Parse(dsap[0].ToString()))
                    {
                        DocumentoPaso.IdDocumentoServicio = int.Parse(dsap[1].ToString());
                    }
                }
                DocumentoPaso.IndVigente = false;
                DocumentoPaso.IndEliminado = false;
                DocumentoPaso.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                DocumentoPaso.FechaCreacion = DateTime.Now;
                DocumentoPaso.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                DocumentoPaso.FechaModificacion = DateTime.Now;

                unidadDeTrabajo.MarcarNuevo(DocumentoPaso);
            }

            if (DocumentPasos.Count > 0)
                unidadDeTrabajo.Commit();

        }

        #endregion "Copiar e Importar"
    } 
}
