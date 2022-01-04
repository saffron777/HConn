using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Configurador.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using HConnexum.Seguridad;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using System.Data;
using System.Linq;
///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase CasoAgrupacionPresentadorDetalle.</summary>
    public class CasoAgrupacionPresentadorDetalle : PresentadorDetalleBase<HConnexum.Tramitador.Negocio.CasoAgrupacion>
    {
        ///<summary>Variable vista de la interfaz ICasoAgrupacionDetalle.</summary>
        readonly ICasoAgrupacionDetalle vista;

        ///<summary>Variable de la entidad CasoAgrupacion.</summary>
        CasoAgrupacion _CasoAgrupacion;

        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public CasoAgrupacionPresentadorDetalle(ICasoAgrupacionDetalle vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista()
        {
            try
            {
                //HConnexum.Tramitador.Datos.CasoAgrupacionRepositorio repositorio = new HConnexum.Tramitador.Datos.CasoAgrupacionRepositorio(udt);
                //this._CasoAgrupacion = repositorio.ObtenerPorId(this.vista.Id);
                //this.PresentadorAVista();
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
                this.udt.IniciarTransaccion();

                CasoRepositorio caso = new CasoRepositorio(this.udt);
                CasoDTO casoDTO = new CasoDTO();
                casoDTO = caso.ObtenerCasoConSuscriptorDTO(vista.Id);



                CasoAgrupacionRepositorio repositorio = new CasoAgrupacionRepositorio(this.udt);
                CasoAgrupacion insertUpdateData;
                Dictionary<int, string> tempTransAsociadas = this.vista.CasosAsociados;
                Dictionary<int, string> tempTransAsociadasOriginal = new Dictionary<int, string>();
                foreach (CasoAgrupacionDTO item in this.vista.CasoAgrupacion)
                {
                    tempTransAsociadasOriginal.Add(item.Id, item.NombreAgrupacion);
                }
                foreach (KeyValuePair<int, string> valuePair in tempTransAsociadas)
                {
                    if (!tempTransAsociadasOriginal.ContainsKey(valuePair.Key))
                    {
                        insertUpdateData = new CasoAgrupacion();
                        insertUpdateData.IdCaso = vista.Id;
                        insertUpdateData.IdAgrupacion = valuePair.Key;
                        insertUpdateData.IdServicio = casoDTO.IdServicio;
                        insertUpdateData.IdSolicitud = casoDTO.IdSolicitud;
                        insertUpdateData.IdSuscriptor = casoDTO.IdSuscriptor;
                        insertUpdateData.IndVigente = true;
                        insertUpdateData.IndEliminado = false;
                        insertUpdateData.CreadoPor = 1;
                        insertUpdateData.FechaCreacion = DateTime.Now;
                        insertUpdateData.ModificadoPor = 1;
                        insertUpdateData.FechaModificacion = DateTime.Now;
                        this.udt.MarcarNuevo(insertUpdateData);
                    }
                }
                foreach (KeyValuePair<int, string> valuePair in tempTransAsociadasOriginal)
                {
                    if (!tempTransAsociadas.ContainsKey(valuePair.Key))
                        repositorio.Eliminar(repositorio.ObtenerCasoAgupacionIdGrupoIdCasoDTO(vista.Id, valuePair.Key));
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

        ///<summary>Rutina para el llenado de  los listbox, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarListBox()
        {
            try
            {
                CasoAgrupacionRepositorio casoAgrupacionRepositorio = new CasoAgrupacionRepositorio(this.udt);
                vista.ListBoxCasosAgrupacionesNoAsociados = (casoAgrupacionRepositorio.ObtenerCasosAgrupacionesNoAsociadosDTO(this.vista.Id)).ToList();
                vista.ListBoxCasosAgrupacionesAsociados = (casoAgrupacionRepositorio.ObtenerCasosAgrupacionesAsociadosDTO(this.vista.Id)).ToList();
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
