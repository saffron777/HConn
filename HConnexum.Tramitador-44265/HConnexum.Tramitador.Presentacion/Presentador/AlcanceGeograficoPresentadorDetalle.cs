using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase AlcanceGeograficoPresentadorDetalle.</summary>
    public class AlcanceGeograficoPresentadorDetalle : PresentadorDetalleBase<AlcanceGeografico>
    {
        ///<summary>Variable vista de la interfaz IAlcanceGeograficoDetalle.</summary>
        IAlcanceGeograficoDetalle vista;
		
        ///<summary>Variable de la entidad AlcanceGeografico.</summary>
        AlcanceGeografico _AlcanceGeografico;
        
        UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public AlcanceGeograficoPresentadorDetalle(IAlcanceGeograficoDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }
		
        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(int idSuscriptor)
        {
            try
            {
                
                AlcanceGeograficoRepositorio repositorio = new AlcanceGeograficoRepositorio(udt);
                _AlcanceGeografico = repositorio.ObtenerPorId(vista.Id);

                LlenarComboSucursal(_AlcanceGeografico.ServicioSucursal.IdFlujoServicio, idSuscriptor);
                LlenarComboPais();
                LlenarComboDiv1(_AlcanceGeografico.IdPais);
                if (!string.IsNullOrWhiteSpace(_AlcanceGeografico.IdDiv1.ToString()))
                LlenarComboDiv2(_AlcanceGeografico.IdDiv1.ToString());
                if (!string.IsNullOrWhiteSpace(_AlcanceGeografico.IdDiv2.ToString()))
                LlenarComboDiv3(_AlcanceGeografico.IdDiv2.ToString());
                PresentadorAVista();
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
                string errores = ValidarDatos();
                if (errores.Length == 0)
                {
                    udt.IniciarTransaccion();
                    AlcanceGeograficoRepositorio repositorio = new AlcanceGeograficoRepositorio(udt);
					
                    if (accion == AccionDetalle.Agregar)
                    {
                        _AlcanceGeografico = new AlcanceGeografico();
                        VistaAPresentador(accion);
                        udt.MarcarNuevo(_AlcanceGeografico);
                    }
                    else
                    {
                        _AlcanceGeografico = repositorio.ObtenerPorId(vista.Id);
                        VistaAPresentador(accion);
                        udt.MarcarModificado(_AlcanceGeografico);
                    }
                    udt.Commit();
                }
                else
                    vista.Errores = errores;
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
                vista.IdServicioSucursal = string.Format("{0:N0}", _AlcanceGeografico.IdServicioSucursal);
                vista.IdFlujoServicio = string.Format("{0:N0}", _AlcanceGeografico.ServicioSucursal.IdFlujoServicio);
                vista.IdPais = string.Format("{0:N0}", _AlcanceGeografico.IdPais);
                vista.IdDiv1 = string.Format("{0:N0}", _AlcanceGeografico.IdDiv1);
                vista.IdDiv2 = string.Format("{0:N0}", _AlcanceGeografico.IdDiv2);
                vista.IdDiv3 = string.Format("{0:N0}", _AlcanceGeografico.IdDiv3);

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
		
        ///<summary>Método encargado de asignar valores a propiedades de la entidad desde la vista.</summary>	
        ///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
        private void VistaAPresentador(AccionDetalle accion)
        {
            try
            {
                _AlcanceGeografico.IdServicioSucursal = int.Parse(vista.IdServicioSucursal);
                _AlcanceGeografico.IdPais = int.Parse(vista.IdPais);
                if(vista.IdDiv1 != "")
                    _AlcanceGeografico.IdDiv1 = int.Parse(vista.IdDiv1);
                if(vista.IdDiv2 != "")
                    _AlcanceGeografico.IdDiv2 = int.Parse(vista.IdDiv2);
                if(vista.IdDiv3 != "")    
                    _AlcanceGeografico.IdDiv3 = int.Parse(vista.IdDiv3);
                AsignarAuditoria(accion);
                AsignarPublicacion();
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
		
        ///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
        /// <returns>Devuelve mensaje(s) con los datos validados.</returns>
        protected string ValidarDatos()
        {
            StringBuilder errores = new StringBuilder(); 
            try
            {
                Metadata<AlcanceGeografico> metadata = new Metadata<AlcanceGeografico>();
                errores.AppendWithBreak(metadata.ValidarPropiedad("Id", vista.Id));
                errores.AppendWithBreak(metadata.ValidarPropiedad("IdServicioSucursal", vista.IdServicioSucursal));
                errores.AppendWithBreak(metadata.ValidarPropiedad("IdPais", vista.IdPais));
                errores.AppendWithBreak(metadata.ValidarPropiedad("IdDiv1", vista.IdDiv1));
                errores.AppendWithBreak(metadata.ValidarPropiedad("IdDiv2", vista.IdDiv2));
                errores.AppendWithBreak(metadata.ValidarPropiedad("IdDiv3", vista.IdDiv3));
            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            return errores.ToString();
        }
		
        ///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
        private void CargarPublicacion()
        {
            try
            {
                vista.IndVigente = _AlcanceGeografico.IndVigente.ToString();
                vista.FechaValidez = _AlcanceGeografico.FechaValidez.ToString();
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
                vista.CreadoPor = this.ObtenerNombreUsuario(_AlcanceGeografico.CreadoPor);
                vista.FechaCreacion = _AlcanceGeografico.FechaCreacion.ToString();
                vista.ModificadoPor = this.ObtenerNombreUsuario(_AlcanceGeografico.ModificadoPor);
                vista.FechaModificacion = _AlcanceGeografico.FechaModificacion.ToString();
                vista.IndEliminado = _AlcanceGeografico.IndEliminado.ToString();
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
                    _AlcanceGeografico.FechaValidez = null;
                else
                    _AlcanceGeografico.FechaValidez = DateTime.Parse(vista.FechaValidez);
                _AlcanceGeografico.IndVigente = bool.Parse(vista.IndVigente);
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
                    _AlcanceGeografico.IndEliminado = false;
                    _AlcanceGeografico.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _AlcanceGeografico.FechaCreacion = DateTime.Now;
                    _AlcanceGeografico.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _AlcanceGeografico.FechaModificacion = DateTime.Now;
                }
                else if (accion == AccionDetalle.Modificar)
                {
                    _AlcanceGeografico.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    _AlcanceGeografico.FechaModificacion = DateTime.Now;
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

        public void LlenarComboServicio(int idSuscriptor)
        {
            try
            {
                ServicioSucursalRepositorio repositorioServicio = new ServicioSucursalRepositorio(udt);
                IEnumerable<ServicioSucursalDTO> listadoServicio = null;
                listadoServicio = repositorioServicio.ObtenerServiciosSucursalDTOporSuscriptor(idSuscriptor);
                List<ServicioSucursalDTO> listadoFlujoServicioA = listadoServicio.ToList();
                vista.ComboServicio = listadoFlujoServicioA;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void LlenarComboSucursal(int idFlujoServicio, int idSuscriptor)
        {
            try
            {
                DataTable listadoSucursales = null;
                ServicioSucursalRepositorio repositorioServicioSucursal = new ServicioSucursalRepositorio(udt);
                IEnumerable<ServicioSucursalDTO> listadoServicioSucursal = null;
                listadoServicioSucursal = repositorioServicioSucursal.ObtenerSucursalDTO(idFlujoServicio);
                List<ServicioSucursalDTO> listadoServicioSucursalA = listadoServicioSucursal.ToList();
                vista.ComboSucursal = listadoServicioSucursalA;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void LlenarComboPais()
        {
            try
            {
                DataTable listadoPaises = null;

                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                try
                {
                    DataSet ds = servicio.ObtenerPaises();
                    if (ds.Tables[@"Error"] != null)
                        throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                        listadoPaises = ds.Tables[0];
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
                vista.ComboPais = listadoPaises;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void LlenarComboDiv1(int IdPais)
        {
            try
            {
                DataTable listadoDiv1 = null;
                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                try
                {
                    DataSet ds = servicio.ObtenerDivisionesTerritoriales1(IdPais);
                    if (ds.Tables[@"Error"] != null)
                        throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                        listadoDiv1 = ds.Tables[0];
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
               
                vista.ComboDiv1 = listadoDiv1;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void LlenarComboDiv2(string IdDiv1)
        {
            try
            {
                DataTable listadoDiv2 = null;
                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                try
                {
                    DataSet ds = servicio.ObtenerDivisionesTerritoriales2(int.Parse(IdDiv1));
                    if (ds.Tables[@"Error"] != null)
                        throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                        listadoDiv2 = ds.Tables[0];
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
                
                vista.ComboDiv2 = listadoDiv2;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void LlenarComboDiv3(string IdDiv2)
        {
            try
            {
                DataTable listadoDiv3 = null;
                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                try
                {
                    DataSet ds = servicio.ObtenerDivisionesTerritoriales3(int.Parse(IdDiv2));
                    if (ds.Tables[@"Error"] != null)
                        throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                        listadoDiv3 = ds.Tables[0];
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
                
                vista.ComboDiv3 = listadoDiv3;
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
