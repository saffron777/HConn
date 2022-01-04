using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Negocio;
using System.Collections.Generic;
using System.Data;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ReporteFacturasPresentador : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IReporteFacturas vista;
		public ReporteFacturasPresentador(IReporteFacturas vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		public string ObtenerUsuarioActual()
		{
			string nombre;
			nombre = vista.UsuarioActual.DatosBase.Nombre1.ToString() + " " + vista.UsuarioActual.DatosBase.Apellido1.ToString();
			return nombre;
		}

		public int ObtenerIdCodExterno(int idCompañia)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerSuscriptorPorID(idCompañia))
				{
					if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
						if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["CodIdExterno"].ToString()))
							return int.Parse(ds.Tables[0].Rows[0]["CodIdExterno"].ToString());
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
			return 0;
		}

		public string ObtenerConexionString(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerConexionStringListaValor(idSuscriptor, ConfigurationManager.AppSettings[@"ListaOrigenDB"]))
				{
					if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
						return ds.Tables[0].Rows[0]["ConexionString"].ToString();
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
		//Se hizo la modificación del valor del logo por actuaclización de la libreria del telerik ya que no reconocia (~) y se aplico un Remove
		public IEnumerable<FacturasDTO> ConvertirtoEntity(DataSet dataSetCollection)
		{
			var iEnumerableFacturas = from TabCollectionFacturas in dataSetCollection.Tables["DatosGestor"].AsEnumerable()
										select new FacturasDTO
										{											
											Logo  = (from tabDatosLogo in dataSetCollection.Tables[@"DatosLogo"].AsEnumerable()
													 select tabDatosLogo.Field<string>(@"logo")).SingleOrDefault().Remove(0,2),
											NFactura = TabCollectionFacturas.Table.Columns.Contains("NFactura") ? TabCollectionFacturas.Field<string>("NFactura") : string.Empty,
											Estatus = TabCollectionFacturas.Field<string>("Status"),
											FechaCreacion = TabCollectionFacturas.Field<DateTime?>("Fec_Rec_Fact"),
											FechaPago = TabCollectionFacturas.Field<DateTime?>("Fecha_Pago"),
											MontoPagar = TabCollectionFacturas.Field<double?>("MontoCubierto"),
										};
			return iEnumerableFacturas;
		}
		
		public IEnumerable<FacturasDTO>GenerarReporte(string conexionString, string sP, int idCodExtUsrActual, int idCodExterno, string estatus, DateTime? fechaInicial, DateTime? fechaFinal, string nFactura, int? nReclamo, string nCedula)
		{
			DataSet dataSetCollection = new DataSet();
			ConexionADO servicio = new ConexionADO();
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			SuscriptorRepositorio suscriptorreporsitorio = new SuscriptorRepositorio(udt);
			try
			{
				dataSetCollection.Tables.Add(@"DatosLogo");
				dataSetCollection.Tables[@"DatosLogo"].Columns.Add(@"logo", typeof(string));
				string logo = suscriptorreporsitorio.ObtenerLogo(idCodExterno, SuscriptorRepositorio.TipoId.CodIndExterno);
				dataSetCollection.Tables[@"DatosLogo"].Rows.Add(logo);
				
				SqlParameter[] colleccionParameter = new SqlParameter[8];

				colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
				colleccionParameter[1] = new SqlParameter("@seguro", idCodExterno);
				colleccionParameter[2] = new SqlParameter("@estatus", estatus);
				colleccionParameter[3] = new SqlParameter("@fecha1", fechaInicial);
				colleccionParameter[4] = new SqlParameter("@fecha2", fechaFinal);
				colleccionParameter[5] = new SqlParameter("@factura", nFactura);
				colleccionParameter[6] = new SqlParameter("@reclamo", nReclamo);
				colleccionParameter[7] = new SqlParameter("@cedula", nCedula);
				dataSetCollection.Tables.Add(servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter).Tables[0].Copy());
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return ConvertirtoEntity(dataSetCollection);
		}
	}
}
