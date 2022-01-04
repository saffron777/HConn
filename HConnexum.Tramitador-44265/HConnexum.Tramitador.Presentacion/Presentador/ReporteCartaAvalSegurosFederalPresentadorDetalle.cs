using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ReporteCartaAvalSegurosFederalPresentadorDetalle : PresentadorDetalleBase<Caso>
	{
		readonly IReporteCartaAvalSegurosFederal vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		public ReporteCartaAvalSegurosFederalPresentadorDetalle(IReporteCartaAvalSegurosFederal vista)
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

		public IEnumerable<ReporteCartaAvalSegurosFederalDTO> GenerarConsultaReporte(int idCarta, int idSuscriptor)
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

		public IEnumerable<ReporteCartaAvalSegurosFederalDTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
		{
			var iEnumerableColeccion = (from TabDatosgestor in dataSetColeccion.Tables["DatosPaciente"].AsEnumerable()
										select new ReporteCartaAvalSegurosFederalDTO
									   {
										   //Monto = TabDatosgestor.Field<double>("monto_cubierto"),
										   CiTitular = TabDatosgestor.Field<string>("a02Ci_Tit"),
										   CiAsegurado = TabDatosgestor.Field<string>("ciaseg"),
										   // Cabecera
										   logo = (from tabDatosLogo in dataSetColeccion.Tables["SuscriptorDTO"].AsEnumerable()
												   select new ReporteSuscriptorSegurosFederal
												   {
													   LogoSuscriptor = (string.IsNullOrWhiteSpace(tabDatosLogo.Field<string>("Logo")) ? WebConfigurationManager.AppSettings["UrlImgs"] + @"No_Disponible.jpg" : tabDatosLogo.Field<string>("Logo")),
													   suscriptor = tabDatosLogo.Field<string>("Nombre"),
													   Numdoc = tabDatosLogo.Field<string>("NumDoc"),
													   Telefono = "0212-4562536",
													   Fax = tabDatosLogo.Field<string>("Fax")
												   }).FirstOrDefault(),
										   Expediente = TabDatosgestor.Field<int?>("expediente"),
										   Reclamo = TabDatosgestor.Field<int?>("reclamo"),
										   FechaSolicitud = TabDatosgestor.Field<DateTime?>("fecha_solicitud"),
										   FechaVencimiento = TabDatosgestor.Field<DateTime?>( "Fecha_Vencimiento_CA" ),
										   // Datos del Asegurado
										   Titular = TabDatosgestor.Field<string>("a02Titular"),
										   Asegurado = TabDatosgestor.Field<string>("asegurado"),
										   Parentesco = TabDatosgestor.Field<string>("parentesco"),
										   Sexo = TabDatosgestor.Field<string>("a02Sexo"),
										   FechaNacimiento = TabDatosgestor.Field<DateTime?>("Fecha_Nacimiento"),
										   // Datos de la Poliza
										   NumPoliza = TabDatosgestor.Field<int?>("poliza"),
										   Contratante = TabDatosgestor.Field<string>("a00Contratante"),
										   NumCertificado = TabDatosgestor.Field<int?>("certificado"),
										   Cobertura = TabDatosgestor.Field<string>("a138Descripcion"),
										   VigenciaDesde = TabDatosgestor.Field<DateTime>("a00FechaDesde"),
										   VigenciaHasta = TabDatosgestor.Field<DateTime>("a00FechaHasta"),
										   // Datos del Proveedor
										   Clinica = TabDatosgestor.Field<string>("clinica"),
										   DireccionClinica = TabDatosgestor.Field<string>("Address_1") + ", " + TabDatosgestor.Field<string>("Address_2"),
										   tlfClinica = TabDatosgestor.Field<string>("Phone"),
										   // Detalle de la Inversion
										   Especialidad = TabDatosgestor.Field<string>("especialidad"),
										   Subespecialidad = TabDatosgestor.Field<string>("subespecialidad"),
										   Diagnostico = TabDatosgestor.Field<string>("diagnosti"),
										   Tratamiento = TabDatosgestor.Field<string>("Tratamiento"),
										   // Presupuesto
										   presupuesto = TabDatosgestor.Field<double?>("presupuesto"),
										   // Detalle del Compromiso
										   MontoCubierto = TabDatosgestor.Field<double?>("monto_cubierto"),
										   Deducible = TabDatosgestor.Field<double?>("Deducible") ?? 0,
										   // Observaciones
										   Observaciones = TabDatosgestor.Field<string>("observ"),
									   });
			return iEnumerableColeccion;
		}
	}
}
