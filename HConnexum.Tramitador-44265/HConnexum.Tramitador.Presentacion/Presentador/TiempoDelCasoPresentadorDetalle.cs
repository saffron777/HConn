using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
    ///<summary>Clase CasoPresentadorDetalle.</summary>
    public class TiempoDelCasoPresentadorDetalle : PresentadorDetalleBase<Caso>
    {
        ///<summary>Variable vista de la interfaz ICasoDetalle.</summary>
        readonly ITiempoDelCasoDetalle vista;

        ///<summary>Variable de la entidad Caso.</summary>
        Caso _Caso;

        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public TiempoDelCasoPresentadorDetalle(ITiempoDelCasoDetalle vista)
        {
            this.vista = vista;
        }

        ///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista()
        {
            try
            {
                CasoRepositorio repositorio = new CasoRepositorio(udt);
                CasoDTO CasoDTO = new CasoDTO();
                CasoDTO = repositorio.ObtenerCasoPorIDCompletoConMovimientos(this.vista.Id);

                if (CasoDTO != null)
                {
                    this.vista.FechaCreacion = CasoDTO.FechaSolicitud.ToString();
                    this.vista.FechaAtencion = CasoDTO.FechaAtencion.ToString();
                    this.vista.FechaCerrado = CasoDTO.FechaEjecucion.ToString();
					TimeSpan segundos = new TimeSpan(0, 0, CasoDTO.sLAEstimado.Value);
					this.vista.SLAestimado = segundos.Days.ToString() + " Dias";

                    if (!string.IsNullOrEmpty(CasoDTO.FechaAtencion.ToString()) & !string.IsNullOrEmpty(CasoDTO.FechaCreacion.ToString()))
                    {
                        vista.Calculo1 = System.Math.Floor(CasoDTO.FechaAtencion.Value.Subtract(CasoDTO.FechaCreacion).TotalSeconds).ToString();
                    }

                    if (!string.IsNullOrEmpty(CasoDTO.FechaEjecucion.ToString()) & !string.IsNullOrEmpty(CasoDTO.FechaCreacion.ToString()))
                    {
                        vista.Calculo2 = System.Math.Floor(CasoDTO.FechaAtencion.Value.Subtract(CasoDTO.FechaCreacion).TotalSeconds).ToString();
                    }
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