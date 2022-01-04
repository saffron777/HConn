using System;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using System.Web;
using System.ServiceModel;
using System.Configuration;
using System.Data;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
    public class SolicitudServicioPresentador : PresentadorDetalleBase<Solicitud>
    {
        ///<summary>Variable vista de la interfaz IMisSolicitudes.</summary>
        readonly ISolicitudServicio vista;

        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
        
        ///<summary>Constructor de la clase.</summary>
        ///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public SolicitudServicioPresentador(ISolicitudServicio vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
        }

        public int CargarDatos(int IdSuscriptorSeleccionado)
		{
			try
			{
                ServiciosSuscriptorRepositorio repositorio = new ServiciosSuscriptorRepositorio(udt);
                DataSet Data = repositorio.obtenerServiciosASolicitarDeUnSuscriptor(IdSuscriptorSeleccionado);
                vista.Datos = Data;
                if (Data.Tables.Count > 0)
                    return Data.Tables[0].Rows.Count;
                else
                    return 0;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
                return 0;
			}
		}

        public void LlenarComboSuscriptores()
        {
            try
            {
                DataView view = new DataView(vista.Datos.Tables[0]);
                DataTable distinctValues = view.ToTable(true, "IdIntermediario", "SuscriptorIntermediario");
                vista.ComboSuscriptor = distinctValues;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void LlenarComboServicios(int Idsuscriptor)
        {
            try
            {
                DataTable DT = vista.Datos.Tables[0];
                string filtro = "IdIntermediario='" + Idsuscriptor + "'";
                DataTable DTfinal = DT.Select(filtro).CopyToDataTable();
                vista.ComboServicio = DTfinal;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }

        public void ListarProveedoresServiciosSimulados(int Idsuscriptor)
        {
            try
            {
                ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
                DataSet ds = servicio.ProveedoresServiciosSimulados(Idsuscriptor);
                vista.ComboProveeServSimulados = ds.Tables[0];
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
