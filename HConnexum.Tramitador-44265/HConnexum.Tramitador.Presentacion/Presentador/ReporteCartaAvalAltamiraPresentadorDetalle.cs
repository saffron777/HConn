using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Datos;
using System.ServiceModel;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ReporteCartaAvalAltamiraPresentadorDetalle : PresentadorDetalleBase<Caso>
	{

		readonly IReporteCartaAvalAltamira vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public ReporteCartaAvalAltamiraPresentadorDetalle(IReporteCartaAvalAltamira vista)
		{
			this.vista = vista;
		}


		public string ObtenerConexionString(int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using(DataSet ds = servicio.ObtenerConexionStringListaValor(idSuscriptor, ConfigurationManager.AppSettings[@"ListaOrigenDB"]))
				{
					if((ds != null) && (ds.Tables[0].Rows.Count > 0))
						return ds.Tables[0].Rows[0]["ConexionString"].ToString();
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return string.Empty;
		}

		//public IEnumerable<DatosSolicitudDTO> GenerarConsultaReporte(int idCarta, string conexionString)
		public IEnumerable<ReporteCartaAvalAltamiraDTO> GenerarConsultaReporte(int idCarta, int idSuscriptor)
		{
			DataSet dataSetColeccion = new DataSet();
			ConexionADO servicio = new ConexionADO();
			string conexionString = ObtenerConexionString(idSuscriptor);
			if(!string.IsNullOrWhiteSpace(conexionString))
				using(SqlConnection connection = new SqlConnection(conexionString))
				{
					try
					{
						SqlParameter[] colleccionParameterPaciente = new SqlParameter[1];
						colleccionParameterPaciente[0] = new SqlParameter("@idcarta", idCarta);
						dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(WebConfigurationManager.AppSettings[@"SpTraeCartaAval"], conexionString, colleccionParameterPaciente).Tables[0].Copy());
						dataSetColeccion.Tables["DatosGestor"].TableName = "DatosPaciente";
						SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
						DataTable suscriptor = suscriptorRepositorio.ObtenerSuscriptorporIdCompleto_(idSuscriptor);
						dataSetColeccion.Tables.Add(suscriptor);
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
						if(connection != null)
							connection.Close();
					}
				}
			return ConversionDatasetToIEnumerable(dataSetColeccion);
		}

		public IEnumerable<ReporteCartaAvalAltamiraDTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
		{
			var iEnumerableColeccion = (from TabDatosgestor in dataSetColeccion.Tables["DatosPaciente"].AsEnumerable()
										select new ReporteCartaAvalAltamiraDTO
									   {
										   Poliza = TabDatosgestor.Field<int>("poliza"),
										   Certificado = TabDatosgestor.Field<int>("certificado"),
										   DiasCartaAvalVence = TabDatosgestor.Field<int>("Vencimiento_cartas"),

										   Clinica = TabDatosgestor.Field<string>("clinica"),
										   Contratante = TabDatosgestor.Field<string>("a00Contratante"),
										   Titular = TabDatosgestor.Field<string>("a02Titular"),
										   CiTitular = TabDatosgestor.Field<string>("a02Ci_Tit"),
										   Asegurado = TabDatosgestor.Field<string>("asegurado"),
										   CiAsegurado = TabDatosgestor.Field<string>("ciaseg"),

										   Parentesco = TabDatosgestor.Field<string>("parentesco"),
										   Expediente = TabDatosgestor.Field<int>("expediente"),
										   Reclamo = TabDatosgestor.Field<int>("reclamo"),

										   Presupuesto = TabDatosgestor.Field<double?>("presupuesto"),
										   MontoCubierto = TabDatosgestor.Field<double?>("monto_cubierto"),
										   Deducible = TabDatosgestor.Field<double?>("Deducible"),
										   Diagnostico = TabDatosgestor.Field<string>("diagnosti"),
										   Tratamiento = TabDatosgestor.Field<string>("Tratamiento"),
										   FechaSolicitud = TabDatosgestor.Field<DateTime>("fecha_solicitud"),
										   FechaVencimiento = TabDatosgestor.Field<DateTime?>("Fecha_Vencimiento_CA"),
										   logo = (from tabDatosLogo in dataSetColeccion.Tables["SuscriptorDTO"].AsEnumerable()
												   select new ReporteSuscriptorAltamira
														 {
															 LogoSuscriptor = (string.IsNullOrWhiteSpace(tabDatosLogo.Field<string>("Logo")) ? WebConfigurationManager.AppSettings["UrlImgs"] + @"No_Disponible.jpg" : tabDatosLogo.Field<string>("Logo")),
															 suscriptor = tabDatosLogo.Field<string>("Nombre"),
															 Numdoc = tabDatosLogo.Field<string>("NumDoc"),
															 Telefono = "0212-4562536",
															 Fax = tabDatosLogo.Field<string>("Fax")
														 }).FirstOrDefault(),
									   });
			return iEnumerableColeccion;
		}
	}
}
