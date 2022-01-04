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
	public class PresentadorDetalleRemesa : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IDetalleRemesa vista;
		public PresentadorDetalleRemesa(IDetalleRemesa vista)
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
				using(DataSet ds = servicio.ObtenerSuscriptorPorID(idCompañia))
				{
					if((ds != null) && (ds.Tables[0].Rows.Count > 0))
						if(!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["CodIdExterno"].ToString()))
							return int.Parse(ds.Tables[0].Rows[0]["CodIdExterno"].ToString());
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
			return 0;
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

		public IEnumerable<DetalleRemesaDTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
		{
			var iEnumerableColeccion = from TabDatosHeader in dataSetColeccion.Tables["DatosHeader"].AsEnumerable()
									   join tabDatosBody in dataSetColeccion.Tables["DatosBody"].AsEnumerable() on TabDatosHeader.Field<int>("reclamo") equals tabDatosBody.Field<int>("reclamo")
									   select new DetalleRemesaDTO
									   {
										   Logo = (from tabDatosLogo in dataSetColeccion.Tables[@"DatosLogo"].AsEnumerable()
												   select tabDatosLogo.Field<string>(@"logo")).SingleOrDefault(),
										   Reclamo = TabDatosHeader.Field<int>("reclamo"),
										   RnCreateDate = TabDatosHeader.Field<DateTime?>("Rn_Create_Date"),
										   FechaPago = TabDatosHeader.Field<DateTime?>("Fecha_Pago"),
										   Tipo = TabDatosHeader.Field<string>("Tipo"),
										   Referencia = TabDatosHeader.Field<string>("Referencia"),
										   TotalRetencion = TabDatosHeader.Field<double?>("Total_Retencion"),
										   TotalReclamos = TabDatosHeader.Field<double?>("Total_Reclamos"),
										   Status = TabDatosHeader.Field<string>("Status"),
										   OBPago = TabDatosHeader.Field<string>("OB_Pago"),
										   Pagador = TabDatosHeader.Field<string>("Pagador"),
										   NroControl = TabDatosHeader.Field<string>("Nro_Control"),
										   FechaFactura = TabDatosHeader.Field<DateTime?>("Fecha_Factura"),
										   FechaRecepcionFactura = TabDatosHeader.Field<DateTime?>("Fecha_Recepcion_Factura"),
										   NroReclamosAsociados = TabDatosHeader.Field<int?>("Nro_Reclamos_Asociados"),
										   TotalFacturado = TabDatosHeader.Field<double?>("Total_Facturado"),
										   TotalSujetoRet = TabDatosHeader.Field<double?>("Total_Sujeto_Ret"),
										   MontoImpMunicipal = TabDatosHeader.Field<double?>("Monto_Imp_Municipal"),
										   BeneficiarioPreoveedor = TabDatosHeader.Field<string>("Beneficiario_Preoveedor"),
										   RifProveedor = TabDatosHeader.Field<string>("Rif_Proveedor"),
										   Expediente = tabDatosBody.Field<int>("expediente"),
										   NroReclamo = tabDatosBody.Field<string>("Nro_Reclamo"),
										   A02Asegurado = tabDatosBody.Field<string>("a02asegurado"),
										   A02NoDocasegurado = tabDatosBody.Field<string>("a02NoDocasegurado"),
										   Factura = tabDatosBody.Field<string>("factura"),
										   FecOcurrencia = tabDatosBody.Field<DateTime?>("Fec_Ocurrencia"),
										   GastosNoCubiertos = tabDatosBody.Field<double?>("GastosNoCubiertos"),
										   RetIsrl = tabDatosBody.Field<double?>("Ret_ISRL"),
										   AhorroGastosMedicos = tabDatosBody.Field<double?>("Ahorro_Gastos_Medicos"),
										   TotConDesc = tabDatosBody.Field<double?>("Tot_Con_Desc"),
										   MontoCubierto = tabDatosBody.Field<double?>("MontoCubierto"),
										   CodigoBarra = tabDatosBody.Field<int>("expediente")
									   };
			return iEnumerableColeccion;
		}

		public IEnumerable<DetalleRemesaDTO> GenerarConsultaReporte(int idSuscriptor, int nRemesa, int idCodExtUsrActual, string conexionString)
		{
			DataSet dataSetColeccion = new DataSet();
			ConexionADO servicio = new ConexionADO();
			UnidadDeTrabajo udt = new UnidadDeTrabajo();
			SuscriptorRepositorio suscriptorreporsitorio = new SuscriptorRepositorio(udt);
			using(SqlConnection connection = new SqlConnection(conexionString))
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
					colleccionParameter[2] = new SqlParameter("@codigoremesa", nRemesa);
					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(ConfigurationManager.AppSettings[@"SpDatosHeader"], conexionString, colleccionParameter).Tables[0].Copy());
					dataSetColeccion.Tables["DatosGestor"].TableName = "DatosHeader";
					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(ConfigurationManager.AppSettings[@"SpDatosBody"], conexionString, colleccionParameter).Tables[0].Copy());
					dataSetColeccion.Tables["DatosGestor"].TableName = "DatosBody";
				}
				catch(Exception ex)
				{
					Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
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
	}
}