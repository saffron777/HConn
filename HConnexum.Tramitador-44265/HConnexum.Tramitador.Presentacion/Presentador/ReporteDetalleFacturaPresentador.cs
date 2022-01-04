using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class ReporteDetalleFacturaPresentador : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IReporteDetalleFactura vista;
		public ReporteDetalleFacturaPresentador(IReporteDetalleFactura vista)
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

		public IEnumerable<DetalleFacturaDTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
		{
			var iEnumerableColeccion = from TabDatosHeader in dataSetColeccion.Tables["DatosHeader"].AsEnumerable()
									   join tabDatosBody in dataSetColeccion.Tables["DatosBody"].AsEnumerable() on TabDatosHeader.Field<string>("NFactura") equals tabDatosBody.Field<string>("factura")
									   select new DetalleFacturaDTO
									   {
										   Logo = (from tabDatosLogo in dataSetColeccion.Tables[@"DatosLogo"].AsEnumerable()
												   select tabDatosLogo.Field<string>(@"logo")).SingleOrDefault().Remove(0,2),
										Factura = TabDatosHeader.Field<string>("NFactura"),
										MontoCubierto = TabDatosHeader.Field<double?>("MontoCubierto"),
										MontoImpMunicipal = TabDatosHeader.Field<double?>("Monto_Imp_Municipal"),
										RetIsrl = TabDatosHeader.Field<double?>("Total_ImpISRL"),
										ImpIva = TabDatosHeader.Field<double?>("ImpIva"),
										TotalSujetoRet = TabDatosHeader.Field<double?>("Total_Sujeto_Ret"),
										FechaRecepcionFactura = TabDatosHeader.Field<DateTime?>("Fec_Rec_Fact"),
										FechaEmisionFactura = TabDatosHeader.Field<DateTime?>("Fecha_Emision"),
										NroControl = TabDatosHeader.Field<string>("Nro_Control"),
										Status = TabDatosHeader.Field<string>("Status"),
										TotalRetencion = TabDatosHeader.Field<double?>("Total_Retencion"),
										NroReclamo = tabDatosBody.Field<int>("expediente") + "-" + tabDatosBody.Field<string>("Nro_Reclamo"),
										A02NoDocasegurado = tabDatosBody.Field<string>("a02NoDocasegurado"),
										StatusGrid = tabDatosBody.Field<string>("Status"),
										FechaPago = tabDatosBody.Field<DateTime?>("Fecha_Pago"),
										MontoCubiertoGrid = tabDatosBody.Field<double?>("monto"),
										BeneficiarioProveedor = TabDatosHeader.Field<string>("Beneficiario_Proveedor"),
										RifProveedor = TabDatosHeader.Field<string>("Rif_Proveedor"),
										TotalImp = TabDatosHeader.Field<double?>("TotalImp"),
										NumOrden = tabDatosBody.Field<string>(@"NumOrden"),
										NumReferencia = tabDatosBody.Field<string>(@"NumReferencia"),
										MontoPagoSap = tabDatosBody.Field<Decimal?>(@"MontoPagoSAP")
									   };
			
			return iEnumerableColeccion;
		}

		public IEnumerable<DetalleFacturaDTO> GenerarConsultaReporte(int idSuscriptor, string nFactura, int idCodExtUsrActual, string conexionString)
		{
			DataSet dataSetColeccion = new DataSet();
			ConexionADO servicio = new ConexionADO();
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			SuscriptorRepositorio suscriptorreporsitorio = new SuscriptorRepositorio(udt);
			using (SqlConnection connection = new SqlConnection(conexionString))
			{
				try
				{
					dataSetColeccion.Tables.Add(@"DatosLogo");
					dataSetColeccion.Tables[@"DatosLogo"].Columns.Add(@"logo", typeof(string));
					string logo = suscriptorreporsitorio.ObtenerLogo(idSuscriptor, SuscriptorRepositorio.TipoId.CodIndExterno);
					dataSetColeccion.Tables[@"DatosLogo"].Rows.Add(logo);

					SqlParameter[] colleccionParameter = new SqlParameter[3];
					colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
					colleccionParameter[1] = new SqlParameter("@seguro", idSuscriptor);
					colleccionParameter[2] = new SqlParameter("@factura", nFactura);


					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(ConfigurationManager.AppSettings[@"StoredProceduresDetalleFacturas"], conexionString, colleccionParameter).Tables[0].Copy());
					dataSetColeccion.Tables["DatosGestor"].TableName = "DatosHeader";

					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(ConfigurationManager.AppSettings[@"StoredProcedureListaReclamosFactura"], conexionString, colleccionParameter).Tables[0].Copy());
					dataSetColeccion.Tables["DatosGestor"].TableName = "DatosBody";
				}
				catch (Exception ex)
				{
					Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
					HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
					this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				}
				finally
				{
					if (connection != null)
						connection.Close();
				}
				return ConversionDatasetToIEnumerable(dataSetColeccion);
			}
			
		}
	}
}