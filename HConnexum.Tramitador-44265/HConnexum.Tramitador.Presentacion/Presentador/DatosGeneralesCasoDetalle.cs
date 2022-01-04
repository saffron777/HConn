using System;
using System.Data;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.ServiceModel;
using System.Configuration;
using System.Collections.Generic;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase CasoPresentadorDetalle.</summary>
	public class DatosGeneralesCasoDetalle : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz ICasoDetalle.</summary>
		readonly IDatosGeneralesCasoDetalle vista;

		///<summary>Variable de la entidad Caso.</summary>
		Caso _Caso;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public DatosGeneralesCasoDetalle(IDatosGeneralesCasoDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(int idcaso)
		{
			try
			{
				vista.Datos = ObtenerValores(idcaso);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de guardar los cambios en BD.</summary>
		///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>

		protected DataTable ObtenerValores(int idcaso)
		{
			DataTable listado = null;
            DataTable listadoTmp = new DataTable();
            DataTable listadoFinal = new DataTable();
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();

			try
			{
				DataSet ds = servicio.ObtenerParametrosPorCaso(idcaso);

				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] lista = ds.Tables[0].Select("VALOR <> '' and ETIQUETA <> '' and VISIBLE ='SI' ");
                    listado = lista.CopyToDataTable();
                    listadoTmp = listado.Clone();
                    listadoFinal = listado.Clone();
                    listadoTmp.Columns["ORDEN"].DataType = System.Type.GetType("System.Int32");

                    foreach (DataRow item in listado.Rows)
                        listadoTmp.ImportRow(item);

                    listadoTmp.DefaultView.Sort = "ORDEN";
                 
                    foreach (DataRowView item in listadoTmp.DefaultView)
                        listadoFinal.ImportRow((DataRow)item.Row);
                }
                return listadoFinal;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return null;
		}
	}
}