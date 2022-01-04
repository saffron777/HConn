using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PresentadorDetalleMovimientoRemesa : PresentadorBase<DetalleRemesaDTO>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		private readonly IPantallaDetalleMovimientoRemesa vista;
		
		public PresentadorDetalleMovimientoRemesa(IPantallaDetalleMovimientoRemesa vista)
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
		
		public int ObtenerIdCodExterno(int idCompañia)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerSuscriptorPorID(idCompañia))
				{
					if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
						if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0][@"CodIdExterno"].ToString()))
							return int.Parse(ds.Tables[0].Rows[0][@"CodIdExterno"].ToString());
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, @"Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
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
						return ds.Tables[0].Rows[0][@"ConexionString"].ToString();
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, @"Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return string.Empty;
		}
		
		public void CargarDatosAsegurado(string sP, int nremesa, string conexionString)
		{
			try
			{
				ConexionADO servicio = new ConexionADO();
				
				SqlParameter[] colleccionParameterPaciente = new SqlParameter[1];
				colleccionParameterPaciente[0] = new SqlParameter("@valor", nremesa);
				using (DataSet datosDetallePaciente = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameterPaciente))
				{
					if ((datosDetallePaciente != null) && (datosDetallePaciente.Tables.Count > 0))
					{
						this.CambiarEstado(datosDetallePaciente);
						
						SqlParameter[] colleccionParameterTitular = new SqlParameter[4];
						colleccionParameterTitular[0] = new SqlParameter("@Nropoliza", datosDetallePaciente.Tables[0].Rows[0][@"a00NroPoliza"].ToString());
						colleccionParameterTitular[1] = new SqlParameter("@Certificado", datosDetallePaciente.Tables[0].Rows[0][@"a00Certificado"].ToString());
						colleccionParameterTitular[2] = new SqlParameter("@Sucursal", datosDetallePaciente.Tables[0].Rows[0][@"a02SucPoliza"].ToString());
						colleccionParameterTitular[3] = new SqlParameter("@Error", SqlDbType.Int, 1, ParameterDirection.Output, false, 1, 1, "", DataRowVersion.Current, 1);
						using (DataSet datosDetalleTitular = servicio.EjecutaStoredProcedure(WebConfigurationManager.AppSettings[@"SpDatosTitular"], conexionString, colleccionParameterTitular))
						{
							if ((datosDetalleTitular != null) && (datosDetalleTitular.Tables.Count > 0))
							{
								this.CambiarEstado(datosDetalleTitular);
								
								this.vista.Asegurado = datosDetalleTitular.Tables[0].Rows[0][@"a02asegurado"].ToString();
								this.vista.CedulaTitular = string.Format("{0}{1}", datosDetalleTitular.Tables[0].Rows[0][@"tit_nac"].ToString(), datosDetalleTitular.Tables[0].Rows[0][@"a02nodocasegurado"].ToString());
								this.vista.Sexo = datosDetalleTitular.Tables[0].Rows[0][@"a02sexo"].ToString();
								this.vista.FechaNacimiento = Convert.ToDateTime(datosDetalleTitular.Tables[0].Rows[0][@"fecha_nacimiento"].ToString()).ToShortDateString();
								this.vista.Estado = datosDetalleTitular.Tables[0].Rows[0][@"a02estado"].ToString();
							}
						}
						this.vista.PacienteAsegurado = datosDetallePaciente.Tables[0].Rows[0][@"a02asegurado"].ToString();
						this.vista.CedulaAsegurado = datosDetallePaciente.Tables[0].Rows[0][@"a02nodocasegurado"].ToString();
						this.vista.SexoPaciente = datosDetallePaciente.Tables[0].Rows[0][@"a02sexo"].ToString();
						this.vista.FechaNacPaciente = Convert.ToDateTime(datosDetallePaciente.Tables[0].Rows[0][@"fecha_nacimiento"].ToString()).ToShortDateString();
						this.vista.EstadoPaciente = datosDetallePaciente.Tables[0].Rows[0][@"a02estado"].ToString();
						this.vista.Parentesco = datosDetallePaciente.Tables[0].Rows[0][@"parentesco_txt"].ToString();
						this.vista.ContratantePoliza = datosDetallePaciente.Tables[0].Rows[0][@"a00Contratante"].ToString();
						this.vista.Poliza = datosDetallePaciente.Tables[0].Rows[0][@"a00NroPoliza"].ToString();
						this.vista.Certificado = datosDetallePaciente.Tables[0].Rows[0][@"a00Certificado"].ToString();
						this.vista.FechaDesde = Convert.ToDateTime(datosDetallePaciente.Tables[0].Rows[0][@"a00FechaDesde"].ToString()).ToShortDateString();
						this.vista.FechaHasta = Convert.ToDateTime(datosDetallePaciente.Tables[0].Rows[0][@"a00FechaHasta"].ToString()).ToShortDateString();
					}
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, @"Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		public void CargarDetalleSolicitud(string sP, int nremesa, string conexionString)
		{
			try
			{
				ConexionADO servicio = new ConexionADO();
				
				SqlParameter[] colleccionParameter = new SqlParameter[1];
				colleccionParameter[0] = new SqlParameter("@reclamo", nremesa);
				using (DataSet dataGrid = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
				{
					if ((dataGrid != null) && (dataGrid.Tables.Count > 0))
					{
						this.vista.ContratanteSolicitud = dataGrid.Tables[0].Rows[0][@"Contratante"].ToString();
						this.vista.Proveedor = dataGrid.Tables[0].Rows[0][@"Rn_Descriptor"].ToString();
						this.vista.Diagnostico = dataGrid.Tables[0].Rows[0][@"Diagnostico"].ToString();
						this.vista.CodClave = string.Format("{0}-{1}", dataGrid.Tables[0].Rows[0][@"expediente"].ToString(), dataGrid.Tables[0].Rows[0][@"Nro_Reclamo"].ToString());
						this.vista.FechaOcurrencia = Convert.ToDateTime(dataGrid.Tables[0].Rows[0][@"Fec_Ocurrencia"].ToString()).ToShortDateString();
						this.vista.FechaNotificacion = Convert.ToDateTime(dataGrid.Tables[0].Rows[0][@"Fec_Notificacion"].ToString()).ToShortDateString();
						this.vista.FechaLiquidadoEsperaPago = Convert.ToDateTime(dataGrid.Tables[0].Rows[0][@"Fec_Liq_epera_pago"].ToString()).ToShortDateString();
						this.vista.FechaEmisionFactura = Convert.ToDateTime(dataGrid.Tables[0].Rows[0][@"Fecha_Emision_Factura"].ToString()).ToShortDateString();
						this.vista.FechaRecepcionFactura = Convert.ToDateTime(dataGrid.Tables[0].Rows[0][@"Fec_Rec_Fact"].ToString()).ToShortDateString();
						this.vista.Status = dataGrid.Tables[0].Rows[0][@"Status"].ToString();
						this.vista.SubCategoria = dataGrid.Tables[0].Rows[0][@"SubCategoria"].ToString();
						this.vista.NumeroControl = dataGrid.Tables[0].Rows[0][@"NCotrol"].ToString();
						this.vista.NumeroFactura = dataGrid.Tables[0].Rows[0][@"NFactura"].ToString();
						this.vista.NumeroPoliza = dataGrid.Tables[0].Rows[0][@"NroPoliza"].ToString();
						this.vista.CertificadoSolicitud = dataGrid.Tables[0].Rows[0][@"Certificado"].ToString();
						this.vista.MontoPresupuestoIncial = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"Monto_Presupuesto_Inicial"]);
						this.vista.Deducible = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"deducible"]);
						this.vista.MontoCubierto = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"MontoCubierto"]);
						this.vista.GastosnoCubiertos = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"GastosNoCubiertos"]);
						this.vista.MontoSujetoRetencion = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"MontoSuRetencion"]);
						this.vista.GastosClinicos = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"GastosClinicos"]);
						this.vista.GastosMedicos = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"GastosMedicos"]);
						this.vista.PorcentajeRetencion = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"ISLR"]);
						this.vista.Retencion = string.Format("{0:N}", dataGrid.Tables[0].Rows[0][@"Ret_ISRL"]);
						this.vista.ParentescoSolicitud = dataGrid.Tables[0].Rows[0][@"Parentesco"].ToString();
						this.vista.Liquidador = dataGrid.Tables[0].Rows[0][@"Liquidador"].ToString();
					}
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, @"Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		
		public DataSet CambiarEstado(DataSet dataSetColeccion)
		{
			for (int i = 0; i < dataSetColeccion.Tables.Count; i++)
			{
				if (dataSetColeccion.Tables[i].Columns.Contains(@"a02Estado"))
				{
					dataSetColeccion.Tables[i].Columns[@"a02Estado"].MaxLength = 10;
					for (int j = 0; j < dataSetColeccion.Tables[i].Rows.Count; j++)
					{
						switch(dataSetColeccion.Tables[i].Rows[j][@"a02Estado"].ToString())
						{
							case @"A":
								dataSetColeccion.Tables[i].Rows[j][@"a02Estado"] = @"Activada";
								break;
							case @"CA":
								dataSetColeccion.Tables[i].Rows[j][@"a02Estado"] = @"Anulada";
								break;
							case @"V":
								dataSetColeccion.Tables[i].Rows[j][@"a02Estado"] = @"Vencida";
								break;
							case @"S":
								dataSetColeccion.Tables[i].Rows[j][@"a02Estado"] = @"Suspendida";
								break;
						}
					}
				}
			}
			return dataSetColeccion;
		}
	}
}
