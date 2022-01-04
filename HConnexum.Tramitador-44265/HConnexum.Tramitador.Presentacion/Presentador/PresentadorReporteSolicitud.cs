using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
	public class PresentadorReporteSolicitud : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		private readonly IDetalleSolicitud vista;
		
		public PresentadorReporteSolicitud(IDetalleSolicitud vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = this.GetType();
		}
		
		public string ObtenerUsuarioActual()
		{
			string nombre;
			nombre = string.Format("{0} {1}", this.vista.UsuarioActual.DatosBase.Nombre1.ToString(), this.vista.UsuarioActual.DatosBase.Apellido1.ToString());
			return nombre;
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
		
		public IEnumerable<DatosSolicitudDTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
		{
			var iEnumerableColeccion = from TabDatosSolicitud in dataSetColeccion.Tables[@"DatosSolicitud"].AsEnumerable()
									   join tabDatosPaciente in dataSetColeccion.Tables[@"DatosPaciente"].AsEnumerable() on TabDatosSolicitud.Field<int>(@"Certificado") equals tabDatosPaciente.Field<int?>(@"a00Certificado")
									   select new DatosSolicitudDTO
									   {
										   Logo = (from tabDatosLogo in dataSetColeccion.Tables[@"DatosLogo"].AsEnumerable()
												   select tabDatosLogo.Field<string>(@"logo")).SingleOrDefault(),
										   PacienteAsegurado = tabDatosPaciente.Field<string>("a02asegurado"),
										   CedulaAsegurado = tabDatosPaciente.Field<string>("a02nodocasegurado"),
										   SexoPaciente = tabDatosPaciente.Field<string>("a02sexo"),
										   FechaNacPaciente = tabDatosPaciente.Field<DateTime?>("fecha_nacimiento"),
										   EstadoPaciente = tabDatosPaciente.Field<string>("a02estado"),
										   Parentesco = tabDatosPaciente.Field<string>("parentesco_txt"),
										   ContratantePoliza = tabDatosPaciente.Field<string>("a00Contratante"),
										   Poliza = tabDatosPaciente.Field<string>("a00NroPoliza"),
										   Certificado = tabDatosPaciente.Field<int?>("a00Certificado"),
										   FechaDesde = tabDatosPaciente.Field<DateTime?>("a00FechaDesde"),
										   FechaHasta = tabDatosPaciente.Field<DateTime?>("a00FechaHasta"),
										   ContratanteSolicitud = TabDatosSolicitud.Field<string>("Contratante"),
										   Proveedor = TabDatosSolicitud.Field<string>("Rn_Descriptor"),
										   Diagnostico = TabDatosSolicitud.Field<string>("Diagnostico"),
										   CodClave = TabDatosSolicitud.Field<int?>("expediente").ToString() + " - " + TabDatosSolicitud.Field<string>("Nro_Reclamo"),
										   FechaOcurrencia = TabDatosSolicitud.Field<DateTime?>("Fec_Ocurrencia"),
										   FechaNotificacion = TabDatosSolicitud.Field<DateTime?>("Fec_Notificacion"),
										   FechaLiquidadoEsperaPago = TabDatosSolicitud.Field<DateTime?>("Fec_Liq_epera_pago"),
										   FechaEmisionFactura = TabDatosSolicitud.Field<DateTime?>("Fecha_Emision_Factura"),
										   FechaRecepcionFactura = TabDatosSolicitud.Field<DateTime?>("Fec_Rec_Fact"),
										   Status = TabDatosSolicitud.Field<string>("Status"),
										   SubCategoria = TabDatosSolicitud.Field<string>("SubCategoria"),
										   NumeroControl = TabDatosSolicitud.Field<string>("NCotrol"),
										   NumeroFactura = TabDatosSolicitud.Field<string>("NFactura"),
										   NumeroPoliza = TabDatosSolicitud.Field<string>("NroPoliza"),
										   CertificadoSolicitud = TabDatosSolicitud.Field<int?>("Certificado"),
										   MontoPresupuestoIncial = TabDatosSolicitud.Field<double?>("Monto_Presupuesto_Inicial"),
										   Deducible = TabDatosSolicitud.Field<double?>("deducible"),
										   MontoCubierto = TabDatosSolicitud.Field<double?>("MontoCubierto"),
										   GastosnoCubiertos = TabDatosSolicitud.Field<double?>("GastosNoCubiertos"),
										   MontoSujetoRetencion = TabDatosSolicitud.Field<double?>("MontoSuRetencion"),
										   GastosClinicos = TabDatosSolicitud.Field<double?>("GastosClinicos"),
										   GastosMedicos = TabDatosSolicitud.Field<double?>("GastosMedicos"),
										   PorcentajeRetencion = TabDatosSolicitud.Field<double?>("ISLR"),
										   Retencion = TabDatosSolicitud.Field<double?>("Ret_ISRL"),
										   ParentescoSolicitud = TabDatosSolicitud.Field<string>("Parentesco"),
										   Liquidador = TabDatosSolicitud.Field<string>("Liquidador"),
										   DatosTitular = (from tabDatosTitular in dataSetColeccion.Tables["DatosTitular"].AsEnumerable()
														   select new DatosTitularDTO
														   {
															   Asegurado = tabDatosTitular.Field<string>("a02asegurado"),
															   CedulaTitular = tabDatosTitular.Field<string>("tit_nac") + "" + tabDatosTitular.Field<string>("a02nodocasegurado"),
															   Sexo = tabDatosTitular.Field<string>("a02sexo"),
															   FechaNacimiento = tabDatosTitular.Field<DateTime?>("fecha_nacimiento"),
															   Estado = tabDatosTitular.Field<string>("a02estado"),
														   }).FirstOrDefault(),
									   };
			return iEnumerableColeccion;
		}
		
		public IEnumerable<DatosSolicitudDTO> GenerarConsultaReporte(int idCodExterno, int nRemesa, string conexionString)
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
					string logo = suscriptorreporsitorio.ObtenerLogo(idCodExterno, SuscriptorRepositorio.TipoId.CodIndExterno);
					dataSetColeccion.Tables[@"DatosLogo"].Rows.Add(logo);
					
					SqlParameter[] colleccionParameterPaciente = new SqlParameter[1];
					colleccionParameterPaciente[0] = new SqlParameter("@valor", nRemesa);
					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(WebConfigurationManager.AppSettings[@"SpDatosPacienteDatosPoliza"], conexionString, colleccionParameterPaciente).Tables[0].Copy());
					dataSetColeccion.Tables["DatosGestor"].TableName = "DatosPaciente";
					
					SqlParameter[] colleccionParameterTitular = new SqlParameter[4];
					colleccionParameterTitular[0] = new SqlParameter("@Nropoliza", dataSetColeccion.Tables["DatosPaciente"].Rows[0]["a00NroPoliza"].ToString());
					colleccionParameterTitular[1] = new SqlParameter("@Certificado", dataSetColeccion.Tables["DatosPaciente"].Rows[0]["a00Certificado"].ToString());
					colleccionParameterTitular[2] = new SqlParameter("@Sucursal", dataSetColeccion.Tables["DatosPaciente"].Rows[0][@"a02SucPoliza"].ToString());
					colleccionParameterTitular[3] = new SqlParameter("@Error", SqlDbType.Int, 1, ParameterDirection.Output, false, 1, 1, "", DataRowVersion.Current, 1);
					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(WebConfigurationManager.AppSettings[@"SpDatosTitular"], conexionString, colleccionParameterTitular).Tables[0].Copy());
					dataSetColeccion.Tables["DatosGestor"].TableName = "DatosTitular";
					
					SqlParameter[] colleccionParameter = new SqlParameter[1];
					colleccionParameter[0] = new SqlParameter("@reclamo", nRemesa);
					dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(WebConfigurationManager.AppSettings[@"SpDatosSolicitudMovRemesa"], conexionString, colleccionParameter).Tables[0].Copy());
					dataSetColeccion.Tables["DatosGestor"].TableName = "DatosSolicitud";
					
					this.CambiarEstado(dataSetColeccion);
				}
				catch (Exception ex)
				{
					Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
					if (ex.InnerException != null)
						HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", ex.InnerException.Message));
					
					HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
					this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				}
				finally
				{
					if (connection != null)
						connection.Close();
				}
			}
			return this.ConversionDatasetToIEnumerable(dataSetColeccion);
		}
		
		public DataSet CambiarEstado(DataSet dataSetColeccion)
		{
			for (int i = 0; i < dataSetColeccion.Tables.Count; i++)
			{
				if (dataSetColeccion.Tables[i].Columns.Contains("a02Estado"))
				{
					dataSetColeccion.Tables[i].Columns["a02Estado"].MaxLength = 10;
					for (int j = 0; j < dataSetColeccion.Tables[i].Rows.Count; j++)
					{
						switch(dataSetColeccion.Tables[i].Rows[j]["a02Estado"].ToString())
						{
							case "A":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Activada";
								break;
							case "CA":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Anulada";
								break;
							case "V":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Vencida";
								break;
							case "S":
								dataSetColeccion.Tables[i].Rows[j]["a02Estado"] = "Suspendida";
								break;
						}
					}
				}
			}
			return dataSetColeccion;
		}
	}
}
