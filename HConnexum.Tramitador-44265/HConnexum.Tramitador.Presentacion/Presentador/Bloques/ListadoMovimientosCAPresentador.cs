using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using System.Web.Configuration;
using System.Configuration;
using System.ServiceModel;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class ListadoMovimientosCAPresentador : BloquesPresentadorBase
	{
		
		#region "Variables"

			private readonly string errorMensajeGenerico = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			private const string NAmespace = @"HConnexum.Tramitador.Presentacion.Presentador.Bloques";
			private string mensaje = string.Empty;
			private string conexionCadena = string.Empty;
			private readonly ConexionADO connGestor = new ConexionADO();
			readonly IListadoMovimientosCA vista;

		#endregion
		
		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public ListadoMovimientosCAPresentador(IListadoMovimientosCA vista)
		{
			try
			{
				this.vista = vista; 
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

		public IQueryable<ListaMovimientosDTO> ConvertToEntity(DataTable coleccion, string orden)
		{
			return (from Tabcoleccion in coleccion.AsEnumerable()
					orderby "it." + orden
					select new ListaMovimientosDTO
					{
						Movimiento = Tabcoleccion.Field<string>(@"Tipo_Movimiento"),
						FechaSolicitud = Tabcoleccion.Field<DateTime?>(@"Fecha_Movimiento"),
						HoraSolicitud = Tabcoleccion.Field<DateTime?>(@"Hora_Registro"),
						Estatus = Tabcoleccion.Field<string>(@"STATUS"),
						Resultado = Tabcoleccion.Field<string>(@"Resultado"),
					}).AsQueryable();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(string orden)
		{
			try
			{
				conexionCadena = ObtenerConexionString(int.Parse(vista.ParametrosEntrada[@"IDSUSINTERMEDIARIO"]));
				int idExpedienteWeb = int.Parse(vista.ParametrosEntrada[@"IDEXPEDIENTEWEB"].ToString());
				if ((idExpedienteWeb != null)&&(!string.IsNullOrWhiteSpace(conexionCadena)))
				{
					DataTable dtMovimientos = ListadeMovimientos(idExpedienteWeb);
					vista.Datos = ConvertToEntity(dtMovimientos, orden);
					vista.NumeroDeRegistros = dtMovimientos.Rows.Count;
				}
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				this.vista.Errores = errorMensajeGenerico;
			}
		}

		public DataTable ListadeMovimientos(int idExpedienteWeb)
		{
			var connGestor = new ConexionADO();
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				mensaje = string.Format("{0}.ListadeMovimientos(idcasoExterno = {1})", NAmespace, idExpedienteWeb);
				SqlParameter[] coleccion = new SqlParameter[1];
				coleccion[0] = new SqlParameter("@expediente", idExpedienteWeb);
				DataSet ds = connGestor.EjecutaStoredProcedure(WebConfigurationManager.AppSettings[@"SpListadoMovimientosCA"], conexionCadena, coleccion);
				if (ds != null && ds.Tables["DatosGestor"].Rows.Count > 0)
					return ds.Tables[0];
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return new DataTable();
		}
		
		public string ObtenerConexionString(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerConexionStringListaValor(idSuscriptor, ConfigurationManager.AppSettings[@"ListaOrigenDB"]))
				{
					if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
						return ds.Tables[0].Rows[0][@"ConexionString"].ToString();
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return string.Empty;
		}
	}
}