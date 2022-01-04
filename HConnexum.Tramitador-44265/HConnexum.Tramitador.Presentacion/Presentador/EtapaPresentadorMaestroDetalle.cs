using System;
using System.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Configuration;
using System.Data;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase EtapaPresentadorMaestroDetalle.</summary>
    public class EtapaPresentadorMaestroDetalle : PresentadorListaBase<Etapa>
    {
        ///<summary>Variable vista de la interfaz IEtapaMaestroDetalle.</summary>
        readonly IEtapaMaestroDetalle vista;
        ///<summary>Variable de la entidad Etapa.</summary>
        Etapa _Etapa;
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public EtapaPresentadorMaestroDetalle(IEtapaMaestroDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        ///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
        ///<param name="pagina">Indica el número de página del conjunto de registros.</param>
        ///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
        ///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
        public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, AccionDetalle accion)
        {
            try
            {
                parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = "IdEtapa", Operador = "EqualTo", Tipo = typeof(int), Valor = vista.Id });
                EtapaRepositorio repositorioMaestro = new EtapaRepositorio(udt);
                _Etapa = repositorioMaestro.ObtenerPorId(vista.Id);
                PresentadorAVista();
                PasoRepositorio repositorioDetalle = new PasoRepositorio(unidadDeTrabajo);
                IEnumerable<PasoDTO> datos = repositorioDetalle.ObtenerPasosDTO(orden, pagina, tamañoPagina, parametrosFiltro);
                vista.NumeroDeRegistros = repositorioDetalle.Conteo;
                vista.Datos = datos;
                if (accion == AccionDetalle.Modificar && _Etapa.FlujosServicio.IndVigente == true)
                    vista.ErroresCustomEditar = "El registro seleccionado no puede ser Editado debido a que el Flujo asociado está actualmente Activo";
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
                PasoRepositorio repositorio = new PasoRepositorio(unidadDeTrabajo);
                PasosBloqueRepositorio repositorioPasosBloque = new PasosBloqueRepositorio(unidadDeTrabajo);
                MensajesMetodosDestinatarioRepositorio repositorioMensajesMetodosDestinatario = new MensajesMetodosDestinatarioRepositorio(unidadDeTrabajo);
                ChadePasoRepositorio repositorioChadePaso = new ChadePasoRepositorio(unidadDeTrabajo);
                FlujosEjecucionRepositorio repositorioFlujosEjecucion = new FlujosEjecucionRepositorio(unidadDeTrabajo);
                DocumentosPasoRepositorio repositorioDocumentosPaso = new DocumentosPasoRepositorio(unidadDeTrabajo);
                ParametrosAgendaRepositorio repositorioParametrosAgenda = new ParametrosAgendaRepositorio(unidadDeTrabajo);
                PasosRepuestaRepositorio repositorioPasosRepuesta = new PasosRepuestaRepositorio(unidadDeTrabajo);
                int idEtapa = 0;
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    Paso _Paso = repositorio.ObtenerPorId(idDesencriptado);
                    idEtapa = _Paso.IdEtapa;
                    if (repositorio.ObtenerPorIdPaso(idDesencriptado).IndVigenteFlujoServicio == true)
                        vista.ErroresCustom = "El registro seleccionado no puede ser Eliminado debido a que el Servicio asociado está actualmente Activo";
                    else
                    {
                        foreach (PasosBloque pb in _Paso.PasosBloque)
                            repositorioPasosBloque.EliminarLogico(pb);
                        foreach (MensajesMetodosDestinatario mmd in _Paso.MensajesMetodosDestinatario)
                            repositorioMensajesMetodosDestinatario.EliminarLogico(mmd);
                        foreach (ChadePaso chp in _Paso.ChadePaso)
                            repositorioChadePaso.EliminarLogico(chp);
                        foreach (FlujosEjecucion fed in _Paso.FlujosEjecucion)
                            repositorioFlujosEjecucion.EliminarLogico(fed);
                        foreach (DocumentosPaso dp in _Paso.DocumentosPaso)
                            repositorioDocumentosPaso.EliminarLogico(dp);
                        foreach (ParametrosAgenda pa in _Paso.ParametrosAgenda)
                            repositorioParametrosAgenda.EliminarLogico(pa);
                        foreach (PasosRepuesta pr in _Paso.PasosRepuesta)
                            repositorioPasosRepuesta.EliminarLogico(pr);
                        repositorio.EliminarLogico(_Paso);
                    }
                }
                unidadDeTrabajo.Commit();
                ActualizoSlaEtapa(idEtapa);
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
		
        ///<summary>Método encargado de guardar los cambios en BD.</summary>
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        public void GuardarCambios(AccionDetalle accion)
        {
            try
            {
                string errores = ValidarDatos();
                if (errores.Length == 0)
                {
                    udt.IniciarTransaccion();
                    EtapaRepositorio repositorio = new EtapaRepositorio(udt);
                    if (accion == AccionDetalle.Agregar)
                    {
                        _Etapa = new Etapa();
                        VistaAPresentador(accion);
                        udt.MarcarNuevo(_Etapa);
                    }
                    else
                    {
                        _Etapa = repositorio.ObtenerPorId(vista.Id);
                        VistaAPresentador(accion);
                        udt.MarcarModificado(_Etapa);
                    }
                    udt.Commit();
                    ActualizoSlaFlujoServicio(_Etapa.IdFlujoServicio);
                }
                else
                    vista.Errores = errores;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void ActualizoSlaFlujoServicio(int idFlujoServicio)
        {
            udt.IniciarTransaccion();
            EtapaRepositorio repositorioetapa = new EtapaRepositorio(udt);
            FlujosServicioRepositorio repositorioFlujo = new FlujosServicioRepositorio(udt);            
            FlujosServicio _Flujo = new FlujosServicio();
            _Flujo = repositorioFlujo.ObtenerPorId(idFlujoServicio);
            _Flujo.SlaPromedio = repositorioetapa.ObtenerSLA(idFlujoServicio);
            udt.MarcarModificado(_Flujo);
            udt.Commit();
        }

        public void ActualizoSlaEtapa(int idEtapa)
        {
            udt.IniciarTransaccion();
            PasoRepositorio repositoriopaso = new PasoRepositorio(udt);
            EtapaRepositorio repositorioetapa = new EtapaRepositorio(udt);
            Etapa _etapa = new Etapa();
            _etapa = repositorioetapa.ObtenerPorId(idEtapa);
            _etapa.SlaPromedio = repositoriopaso.ObtenerSLA(idEtapa);
            udt.MarcarModificado(_etapa);
            udt.Commit();
        }

        ///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
        private void PresentadorAVista()
        {
            try
            {
                vista.IdFlujoServicio = string.Format("{0:N0}", _Etapa.IdFlujoServicio);
                vista.Nombre = _Etapa.Nombre;
                vista.Orden = string.Format("{0:N0}", _Etapa.Orden);
                vista.SlaPromedio = _Etapa.SlaPromedio.ToString();
                vista.SlaTolerancia = _Etapa.SlaTolerancia.ToString();
                vista.IndObligatorio = _Etapa.IndObligatorio.ToString();
                vista.IndRepeticion = _Etapa.IndRepeticion.ToString();
                vista.IndSeguimiento = _Etapa.IndSeguimiento.ToString();
                vista.IndInicioServ = _Etapa.IndInicioServ.ToString();
                vista.IndCierre = _Etapa.IndCierre.ToString();
                CargarPublicacion();
                CargarAuditoria();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
		
        ///<summary>Método encargado de asignar valores a propiedades de la entidad desde la vista.</summary>	
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        private void VistaAPresentador(AccionDetalle accion)
        {
            try
            {
                _Etapa.Id = vista.Id;
                _Etapa.IdFlujoServicio = int.Parse(vista.IdFlujoServicio);
                _Etapa.Nombre = vista.Nombre;
                _Etapa.Orden = int.Parse(vista.Orden);
                _Etapa.SlaPromedio = int.Parse(vista.SlaPromedio);
                _Etapa.SlaTolerancia = int.Parse(vista.SlaTolerancia);
                _Etapa.IndObligatorio = bool.Parse(vista.IndObligatorio);
                _Etapa.IndRepeticion = bool.Parse(vista.IndRepeticion);
                _Etapa.IndSeguimiento = bool.Parse(vista.IndSeguimiento);
                _Etapa.IndInicioServ = bool.Parse(vista.IndInicioServ);
                _Etapa.IndCierre = bool.Parse(vista.IndCierre);
                AsignarAuditoria(accion);
                AsignarPublicacion();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
		
        ///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
        /// <returns>Devuelve mensaje(s) con los datos validados.</returns>
        protected string ValidarDatos()
        {
            StringBuilder errores = new StringBuilder(); 
            try
            {
                Metadata<Etapa> metadata = new Metadata<Etapa>();
                errores.AppendWithBreak(metadata.ValidarPropiedad("Nombre", vista.Nombre));
                errores.AppendWithBreak(metadata.ValidarPropiedad("Orden", vista.Orden));
                errores.AppendWithBreak(metadata.ValidarPropiedad("SlaPromedio", vista.SlaPromedio));
                errores.AppendWithBreak(metadata.ValidarPropiedad("SlaTolerancia", vista.SlaTolerancia));
                return errores.ToString();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            return errores.ToString();
        }
		
        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void ServicioParametrizador(AccionDetalle accion)
        {
            int idSuscriptor = 0;
            int idFlujoServicio = 0;
            List<FlujosServicioDTO> listadoFlujoServicioA;
            FlujosServicioDTO Us = new FlujosServicioDTO();
            FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(udt);
            if (accion == AccionDetalle.Agregar)
            {
                Us = repositorio.ObtenerIdServicioSuscriptor2(vista.Id);
                idSuscriptor = Us.IdSuscriptor;
                FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(udt);
                IEnumerable<FlujosServicioDTO> listadoFlujoServicio = repositorioFlujosServicio.ObtenerFlujosServicioDTO(vista.Id);
                listadoFlujoServicioA = listadoFlujoServicio.ToList();
            }
            else
            {
                Us = repositorio.ObtenerIdServicioSuscriptor(vista.Id);
                idSuscriptor = Us.IdSuscriptor;
                idFlujoServicio = Us.Id;
                FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(udt);
                IEnumerable<FlujosServicioDTO> listadoFlujoServicio = repositorioFlujosServicio.ObtenerFlujosServicioDTO(idFlujoServicio);
                listadoFlujoServicioA = listadoFlujoServicio.ToList();
            }
            DataTable listadoIdSubServicio = null;
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerServiciosPorIdSuscriptor(idSuscriptor);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoIdSubServicio = ds.Tables[0];
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
            for (int i = 0; i < listadoFlujoServicioA.Count; i++)
            {
                int j;
                bool find = false;
                for (j = 0; j < listadoIdSubServicio.Rows.Count && find == false; j++)
                    if (listadoFlujoServicioA[i].IdServicioSuscriptor == int.Parse(listadoIdSubServicio.Rows[j]["Id"].ToString()))
                        find = true;
                if (find == true)
                    listadoFlujoServicioA[i].NombreServicioSuscriptor = listadoIdSubServicio.Rows[j - 1]["Nombre"].ToString();
                 
                else
                    listadoFlujoServicioA[i].NombreServicioSuscriptor = "";
            }
            vista.ComboIdFlujoServicio = listadoFlujoServicioA;
        }

        ///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
        private void CargarPublicacion()
        {
            try
            {
                vista.IndVigente = _Etapa.IndVigente.ToString();
                vista.FechaValidez = _Etapa.FechaValidez.ToString();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        ///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
        private void CargarAuditoria()
        {
            try
            {
                vista.CreadoPor = this.ObtenerNombreUsuario(_Etapa.CreadoPor);
                vista.FechaCreacion = _Etapa.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_Etapa.ModificadoPor);
                vista.IndEliminado = _Etapa.IndEliminado.ToString();
                vista.FechaModificacion = _Etapa.FechaModicacion.ToString();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
		
        ///<summary>Método encargado de asignar valores de publicación a la entidad.</summary>
        public void AsignarPublicacion()
        {
            try
            {
                if (string.IsNullOrEmpty(vista.FechaValidez))
                    _Etapa.FechaValidez = null;
                else
                    _Etapa.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _Etapa.IndVigente = bool.Parse(vista.IndVigente);
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        ///<summary>Método encargado de asignar valores de auditoria a la entidad.</summary>
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        private void AsignarAuditoria(AccionDetalle accion)
        {
            try
            {
                if (accion == AccionDetalle.Agregar)
                {
                    _Etapa.IndEliminado = false;
                    _Etapa.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _Etapa.FechaCreacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _Etapa.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _Etapa.FechaModicacion = DateTime.Now;
                }
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
            PasoRepositorio repositorio = new PasoRepositorio(this.unidadDeTrabajo);
            return repositorio.obtenerRolIndEliminado();
        }

        public void activarEliminado(IList<string> ids)
        {
            try
            {
                this.unidadDeTrabajo.IniciarTransaccion();
                PasoRepositorio repositorio = new PasoRepositorio(this.unidadDeTrabajo);
                foreach (string id in ids)
                {
                    int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
                    Paso _Aplicacion = repositorio.ObtenerPorId(idDesencriptado);
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
    }
}