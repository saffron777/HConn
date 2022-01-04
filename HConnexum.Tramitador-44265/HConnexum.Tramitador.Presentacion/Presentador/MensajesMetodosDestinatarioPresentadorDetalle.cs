using System;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Configuration;
using System.Data;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase MensajesMetodosDestinatarioPresentadorDetalle.</summary>
    public class MensajesMetodosDestinatarioPresentadorDetalle : PresentadorDetalleBase<MensajesMetodosDestinatario>
    {
        ///<summary>Variable vista de la interfaz IMensajesMetodosDestinatarioDetalle.</summary>
        IMensajesMetodosDestinatarioDetalle vista;
        ///<summary>Variable de la entidad MensajesMetodosDestinatario.</summary>
        MensajesMetodosDestinatario _MensajesMetodosDestinatario;
        MensajesMetodosDestinatarioDTO us = new MensajesMetodosDestinatarioDTO();
        UnidadDeTrabajo udt = new UnidadDeTrabajo();
        //variables globales
        string idClase;
        string idRutina;
        string idConstante;
        string idSms;

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public MensajesMetodosDestinatarioPresentadorDetalle(IMensajesMetodosDestinatarioDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista()
        {
            try
            {
                MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario2 = new MensajesMetodosDestinatarioRepositorio(this.udt);
                us = _MensajesMetodosDestinatario2.ObtenerSMSDTO(vista.Id, int.Parse(idSms));
                if (us != null)
                {
                    PresentadorAVista();
                    if (us.IndVigenteFlujoServicio == true)
                        vista.ErroresCustomEditar = "El registro seleccionado no puede ser Editado debido a que el Flujo asociado está actualmente Activo";
                }
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
		
        ///<summary>Método encargado de guardar los cambios en BD.</summary>
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        public void GuardarCambios(AccionDetalle accion)
        {
            try
            {
                ///Se actualiza los list 
                LlenarListaValor();
                this.udt.IniciarTransaccion();
                MensajesMetodosDestinatarioRepositorio repositorio = new MensajesMetodosDestinatarioRepositorio(udt);
                MensajesMetodosDestinatario insertUpdateData;
                Dictionary<int, string> tempTransAsociadas = this.vista.CasosMovimientosAsociados;
                Dictionary<int, string> tempTransAsociadasOriginal = new Dictionary<int, string>();
                if (this.vista.ValorBusqueda != null)
                    foreach (MensajesMetodosDestinatarioDTO item in this.vista.ValorBusqueda)
                        tempTransAsociadasOriginal.Add(item.Id, item.ValorBusqueda);
                foreach (KeyValuePair<int, string> valuePair in tempTransAsociadas)
                    if (!tempTransAsociadasOriginal.ContainsKey(valuePair.Key))
                    {
                        insertUpdateData = new MensajesMetodosDestinatario();
                        insertUpdateData.IdPaso = int.Parse(vista.IdPaso);
                        insertUpdateData.IdMensaje = int.Parse(vista.IdMensaje);
                        insertUpdateData.IdMetodo = int.Parse(idSms);
                        insertUpdateData.IdTipoBusquedaDestinatario = int.Parse(idClase);
                        insertUpdateData.ValorBusqueda = valuePair.Value;
                        insertUpdateData.FechaValidez = DateTime.Now;
                        insertUpdateData.IndVigente = true;
                        insertUpdateData.IndEliminado = false;
                        insertUpdateData.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                        insertUpdateData.FechaCreacion = DateTime.Now;
                        insertUpdateData.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                        insertUpdateData.FechaModificacion = DateTime.Now;
                        insertUpdateData.IdTipoPrivacidad = null;
                        this.udt.MarcarNuevo(insertUpdateData);
                    }
                foreach (KeyValuePair<int, string> valuePair in tempTransAsociadasOriginal)
                    if (!tempTransAsociadas.ContainsKey(valuePair.Key))
                        repositorio.Eliminar(repositorio.ObtenerPorId(valuePair.Key));
                /// Elimino las constantes, ya creadas.
                IList<MensajesMetodosDestinatarioDTO> ListaConstantesEliminar = BuscarConstantes(vista.Id);
                foreach (MensajesMetodosDestinatarioDTO Eliminar in ListaConstantesEliminar)
                {
                    var IdConstante = Eliminar.Id;
                    repositorio.Eliminar(repositorio.ObtenerPorId(IdConstante));
                }
                ///Creo las nuevas constantes
                var Texto = vista.Constantes;
                string[] TextoSplit = Texto.Split(';');
                foreach (string textos in TextoSplit)
                    if (textos != "")
                    {
                        insertUpdateData = new MensajesMetodosDestinatario();
                        insertUpdateData.IdPaso = int.Parse(vista.IdPaso);
                        insertUpdateData.IdMensaje = int.Parse(vista.IdMensaje);
                        insertUpdateData.IdMetodo = int.Parse(idSms);
                        insertUpdateData.IdTipoBusquedaDestinatario = int.Parse(idConstante);
                        insertUpdateData.ValorBusqueda = textos.ToString();
                        insertUpdateData.FechaValidez = DateTime.Now;
                        insertUpdateData.IndVigente = true;
                        insertUpdateData.IndEliminado = false;
                        insertUpdateData.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                        insertUpdateData.FechaCreacion = DateTime.Now;
                        insertUpdateData.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                        insertUpdateData.FechaModificacion = DateTime.Now;
                        insertUpdateData.IdTipoPrivacidad = null;
                        this.udt.MarcarNuevo(insertUpdateData);
                    }
                this.udt.Commit();
                this.udt.IniciarTransaccion();
                MensajesMetodosDestinatarioDTO us = new MensajesMetodosDestinatarioDTO();
                MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario2 = new MensajesMetodosDestinatarioRepositorio(this.udt);
                us = _MensajesMetodosDestinatario2.ObtenerRutinaSMSDTO(int.Parse(vista.IdPaso), int.Parse(idSms), int.Parse(idRutina));
                MensajesMetodosDestinatario usDTO = new MensajesMetodosDestinatario();
                if (us != null)
                {
                    usDTO.Id = us.Id;
                    usDTO.IdPaso = us.IdPaso;
                    usDTO.IdMensaje = int.Parse(vista.IdMensaje);
                    usDTO.IdMetodo = int.Parse(idSms);
                    usDTO.IdTipoBusquedaDestinatario = int.Parse(idRutina);
                    usDTO.ValorBusqueda = vista.Rutina;
                    usDTO.FechaValidez = us.FechaValidez;
                    usDTO.IndVigente = us.IndVigente;
                    usDTO.IndEliminado = us.IndEliminado;
                    usDTO.CreadoPor = us.CreadoPor;
                    usDTO.FechaCreacion = us.FechaCreacion;
                    usDTO.ModificadoPor = us.ModificadoPor;
                    usDTO.FechaModificacion = us.FechaModificacion;
                    usDTO.IdTipoPrivacidad = null;
                    this.udt.MarcarModificado(usDTO);
                }
                else if (vista.Rutina != "")
                {
                    usDTO.IdPaso = int.Parse(vista.IdPaso);
                    usDTO.IdMensaje = int.Parse(vista.IdMensaje);
                    usDTO.IdMetodo = int.Parse(idSms);
                    usDTO.IdTipoBusquedaDestinatario = int.Parse(idRutina);
                    usDTO.ValorBusqueda = vista.Rutina;
                    usDTO.FechaValidez = DateTime.Now;
                    usDTO.IndVigente = true;
                    usDTO.IndEliminado = false;
                    usDTO.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    usDTO.FechaCreacion = DateTime.Now;
                    usDTO.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    usDTO.FechaModificacion = DateTime.Now;
                    usDTO.IdTipoPrivacidad = null;
                    this.udt.MarcarNuevo(usDTO);
                }
                this.udt.Commit();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
				
        ///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
        private void PresentadorAVista()
        {
            try
            {
                vista.IdPaso = string.Format("{0:N0}", us.IdPaso);
                vista.IdMensaje = string.Format("{0:N0}", us.IdMensaje);
                vista.FechaModificacion = us.FechaModificacion.ToString();
                CargarPublicacion();
                CargarAuditoria();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
			
        /// <summary>
        /// Metodo encargado de Llenar la lista.
        /// </summary>
        public void LlenarListBox()
        {
            try
            {
                IList<MensajesMetodosDestinatarioDTO> ListaItems;
                if (vista.Id != 0)
                {
                    DataTable ListBoxCasosMovimientos2 = new DataTable();
                    MensajesMetodosDestinatario us = new MensajesMetodosDestinatario();
                    MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario = new MensajesMetodosDestinatarioRepositorio(this.udt);
                    var obtenerMetodoDestinatatio = _MensajesMetodosDestinatario.ObtenerDTO(vista.Id, int.Parse(idClase), int.Parse(idSms));
                    vista.ListBoxCasosMovimientosAsociados = obtenerMetodoDestinatatio.ToList();
                    ListaItems = obtenerMetodoDestinatatio.ToList();
                    ListBoxCasosMovimientos2.Columns.Add("Id");
                    ListBoxCasosMovimientos2.Columns.Add("Nombre");
                    int i = 1;
                    foreach (ObjetoCaso OC in Enum.GetValues(typeof(ObjetoCaso)))
                    {
                        bool consegui = false;
                        foreach (MensajesMetodosDestinatarioDTO MMD in ListaItems)
                        {
                            var Valor = MMD.ValorBusqueda;
                            string[] words = Valor.Split('.');
                            Valor = words[1].ToString();
                            if (Valor == OC.ToString())
                                consegui = true;
                        }
                        if (consegui == false)
                        {
                            DataRow row = ListBoxCasosMovimientos2.NewRow();
                            row["Id"] = i;
                            row["Nombre"] = "Caso." + OC.ToString();
                            ListBoxCasosMovimientos2.Rows.Add(row);
                        }
                        i++;
                    }
                    foreach (ObjetoMovimiento OM in Enum.GetValues(typeof(ObjetoMovimiento)))
                    {
                        bool consegui = false;
                        foreach (MensajesMetodosDestinatarioDTO MMD in ListaItems)
                        {
                            var Valor = MMD.ValorBusqueda;
                            string[] words = Valor.Split('.');
                            Valor = words[1].ToString();
                            if (Valor == MMD.ToString())
                                consegui = true;
                        }
                        if (consegui == false)
                        {
                            DataRow row = ListBoxCasosMovimientos2.NewRow();
                            row["Id"] = i;
                            row["Nombre"] = "Movimiento." + OM.ToString();
                            ListBoxCasosMovimientos2.Rows.Add(row);
                        }
                        i++;
                    }
                    vista.ListBoxCasosMovimientosNoAsociados = ListBoxCasosMovimientos2;
                }
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarCombos()
        {
            try
            {
                PasoRepositorio repositorioPaso = new PasoRepositorio(udt);
                vista.ComboIdPaso = repositorioPaso.ObtenerPasoporIdDTO(vista.Id);
                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                DataTable listadoIdMensaje = null;
                try
                {
                    DataSet ds = servicio.ObtenerMensaje();
                    if (ds.Tables[@"Error"] != null)
                        throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                        listadoIdMensaje = ds.Tables[0];
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
                vista.ComboIdMensaje = listadoIdMensaje;
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        /// <summary>
        /// Metodo Encargado de buscar en la Base de dato, Las constantes pertenecientes a un paso.
        /// </summary>
        /// <param name="id">Id Paso</param>
        /// <returns></returns> //TODO, Cambiar el Id por Id paso
        public IList<MensajesMetodosDestinatarioDTO> BuscarConstantes(int id)
        {
            IList<MensajesMetodosDestinatarioDTO> ListaConstantes;
            MensajesMetodosDestinatario us = new MensajesMetodosDestinatario();
            MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario = new MensajesMetodosDestinatarioRepositorio(this.udt);
            var obtenerMetodoDestinatatio = _MensajesMetodosDestinatario.ObtenerConstantesDTO(id, int.Parse(idConstante), int.Parse(idSms));
            ListaConstantes = obtenerMetodoDestinatatio.ToList();
            return ListaConstantes;
        }

        /// <summary>
        /// Metodo encargado de burcar la Rutina de un paso en caso tal que la posea.
        /// </summary>
        /// <returns></returns>
        public string BuscarRutina()
        {
            MensajesMetodosDestinatario us = new MensajesMetodosDestinatario();
            MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario = new MensajesMetodosDestinatarioRepositorio(this.udt);
            var obtenerMetodoDestinatatio = _MensajesMetodosDestinatario.ObtenerRutinaSMSDTO(vista.Id, int.Parse(idSms), int.Parse(idRutina));
            if (obtenerMetodoDestinatatio != null)
                return vista.Rutina = obtenerMetodoDestinatatio.ValorBusqueda;
            else
                return vista.Rutina = "";
        }

        ///<summary>Rutina para el llenado de el control multiline</summary>
        public void LlenarConstantes()
        {
            try
            {
                IList<MensajesMetodosDestinatarioDTO> ListaItems = BuscarConstantes(vista.Id);

                foreach (MensajesMetodosDestinatarioDTO prueba in ListaItems)
                {
                    var textoAinsertar = prueba.ValorBusqueda + "; ";
                    vista.Constantes = vista.Constantes + textoAinsertar;
                }
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        ///<summary>Rutina para el llenado de Los valores de las listas valores</summary>
        public void LlenarListaValor()
        {
            try
            {
                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                DataTable listadoIdMensaje = null;
                DataTable listadoIdMetodoEnvio = null;
                try
                {
                    DataSet ds = servicio.ObtenerListaValorPorNombre("TipoBusquedaDestinatario");
                    if (ds.Tables[@"Error"] != null)
                        throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                        listadoIdMensaje = ds.Tables[0];
                    int i = 0;
                    foreach (DataRow row in listadoIdMensaje.Rows)
                    {
                        var NombreValorCorto = row["NombreValorCorto"].ToString();
                        if (NombreValorCorto == "Clase")
                            idClase = row["Id"].ToString();
                        if (NombreValorCorto == "Rutina")
                            idRutina = row["Id"].ToString();
                        if (NombreValorCorto == "Constante")
                            idConstante = row["Id"].ToString();
                        i++;
                    }
                    DataSet ds3 = servicio.ObtenerMetodosEnvioporAlerta();
                    if (ds3 != null && ds3.Tables.Count != 0)
                    {
                        if (ds3.Tables[@"Error"] != null)
                            throw new Exception(ds3.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                        if (ds3.Tables[0].Rows.Count > 0)
                            listadoIdMetodoEnvio = ds3.Tables[0];
                        foreach (DataRow rowMetodo in listadoIdMetodoEnvio.Rows)
                        {
                            var Nombre = rowMetodo["Nombre"].ToString();
                            if (Nombre == EnvioSMS)
                                idSms = rowMetodo["Id"].ToString();
                        }
                    }
                    else
                        vista.ErroresCustomEditar = "No se han configurado métodos de envío";
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
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }


        /// <summary>
        /// Nombre que tiene el envio Email en la tabla MetodosEnvioporAlerta
        /// esto es necesario para obtener su ID
        /// </summary>
        public string EnvioSMS
        {
            get {
                return @"SMS";
            }
        }

        ///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
        private void CargarPublicacion()
        {
            try
            {
                vista.IndVigente = us.IndVigente.ToString();
                vista.FechaValidez = us.FechaValidez.ToString();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }

        ///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
        private void CargarAuditoria()
        {
            try
            {
                vista.CreadoPor = this.ObtenerNombreUsuario(us.CreadoPor);
                vista.FechaCreacion = us.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(us.ModificadoPor);
                vista.IndEliminado = us.IndEliminado.ToString();
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
		
        ///<summary>Método encargado de asignar valores de publicación a la entidad.</summary>
        public void AsignarPublicacion()
        {
            try
            {
                if (string.IsNullOrEmpty(vista.FechaValidez))
                    _MensajesMetodosDestinatario.FechaValidez = null;
                else
                    _MensajesMetodosDestinatario.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _MensajesMetodosDestinatario.IndVigente = bool.Parse(vista.IndVigente);
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
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
                    _MensajesMetodosDestinatario.IndEliminado = false;
                    _MensajesMetodosDestinatario.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _MensajesMetodosDestinatario.FechaCreacion = DateTime.Now;
                    _MensajesMetodosDestinatario.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _MensajesMetodosDestinatario.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _MensajesMetodosDestinatario.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _MensajesMetodosDestinatario.FechaModificacion = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
        }
    }
}