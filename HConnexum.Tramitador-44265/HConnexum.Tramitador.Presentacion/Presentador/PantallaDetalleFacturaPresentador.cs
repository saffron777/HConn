using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using System.ServiceModel;
using HConnexum.Tramitador.Datos;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PantallaDetalleFacturaPresentador : PresentadorDetalleBase<DetalleFacturaDTO>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IPantallaDetalleFactura vista;
		public PantallaDetalleFacturaPresentador(IPantallaDetalleFactura vista)
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

		public void CargarDetalleFactura(int idCodExtUsrActual, string sP, string nfactura, string conexionString)
		{
			try
			{
				ConexionADO servicio = new ConexionADO();
				SqlParameter[] colleccionParameter = new SqlParameter[3];
				colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
				colleccionParameter[1] = new SqlParameter("@seguro", vista.IdCodExterno);
				colleccionParameter[2] = new SqlParameter("@factura", nfactura);
				using(DataSet dataGrid = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
				{
					if((dataGrid != null) && (dataGrid.Tables[0].Rows.Count > 0))
					{
						vista.Nfactura = dataGrid.Tables[0].Rows[0]["NFactura"].ToString();
						vista.FechaRecepcion = Convert.ToDateTime(dataGrid.Tables[0].Rows[0]["Fec_Rec_Fact"].ToString()).ToShortDateString();
						vista.Estatus = dataGrid.Tables[0].Rows[0]["Status"].ToString();
						vista.Ncontrol = dataGrid.Tables[0].Rows[0]["Nro_Control"].ToString();
						vista.MontoCubierto = string.Format("{0:N}", dataGrid.Tables[0].Rows[0]["MontoCubierto"]);
						vista.MontoImpMunicipal = string.Format("{0:N}", dataGrid.Tables[0].Rows[0]["Monto_Imp_Municipal"]);
						vista.MontoSujetoRetencion = string.Format("{0:N}", dataGrid.Tables[0].Rows[0]["Total_Sujeto_Ret"]);
						vista.TotalImpIsrl = string.Format("{0:N}", dataGrid.Tables[0].Rows[0]["Total_ImpISRL"]);
						vista.ImpIva = string.Format("{0:N}", dataGrid.Tables[0].Rows[0]["ImpIva"]);
						vista.TotalImp = string.Format("{0:N}", dataGrid.Tables[0].Rows[0]["TotalImp"]);
					}
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public IQueryable<DetalleFacturaGridDTO> ConvertirtoEntity(DataSet coleccion)
		{
			var iqueriable = (from Tabcoleccion in coleccion.Tables[0].AsEnumerable()
								select new DetalleFacturaGridDTO
								{
									NReclamosEncriptado = Tabcoleccion.Field<string>(@"nReclamosEncriptado"),
									NroReclamo = Tabcoleccion.Field<string>(@"Nro_Reclamo"),
									Reclamo = Tabcoleccion.Field<int>(@"expediente").ToString() + "-" + Tabcoleccion.Field<string>(@"Nro_Reclamo").ToString(),
									Status = Tabcoleccion.Field<string>(@"Status"),
									DocAsegurado = Tabcoleccion.Field<string>(@"a02NoDocasegurado"),
									Factura = Tabcoleccion.Field<string>(@"factura"),
									FechaPago = Tabcoleccion.Field<DateTime?>(@"Fecha_Pago"),
									Monto = Tabcoleccion.Field<double?>(@"monto"),
									NumOrden = Tabcoleccion.Field<string>(@"NumOrden"),
									NumReferencia = Tabcoleccion.Field<string>(@"NumReferencia"),
									MontoPagoSap = Tabcoleccion.Field<Decimal?>(@"MontoPagoSAP"),
								}).AsQueryable();
			return iqueriable;
		}

		public IEnumerable<DetalleFacturaGridDTO> CargarResumenCaso(int idCodExtUsrActual, string sP, string nfactura, string conexionString, IList<Filtro> parametrosFiltro, int npagina, int nregistros)
		{
			try
			{
				ConexionADO servicio = new ConexionADO();
				SqlParameter[] colleccionParameter = new SqlParameter[3];
				colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
				colleccionParameter[1] = new SqlParameter("@seguro", vista.IdCodExterno);
				colleccionParameter[2] = new SqlParameter("@factura", nfactura);
				using(DataSet dataGrid = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
				{
					if((dataGrid != null) && (dataGrid.Tables[0].Rows.Count > 0))
					{
						DataTable tabla = dataGrid.Tables[0];
						GenerarColumnEncriptada(ref tabla, @"Nro_Reclamo", @"nReclamosEncriptado");
						vista.NumeroDeRegistros = dataGrid.Tables[0].Rows.Count;
						IEnumerable<DetalleFacturaGridDTO>  collection = ConvertirtoEntity(dataGrid);
						collection = UtilidadesDTO<DetalleFacturaGridDTO>.FiltrarPaginar(collection.AsQueryable(), npagina, nregistros, parametrosFiltro);
						if(parametrosFiltro.Count > 0)
							collection = UtilidadesDTO<DetalleFacturaGridDTO>.FiltrarColeccion(collection.AsQueryable(), parametrosFiltro);
						
						return collection;
					}
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return Enumerable.Empty<DetalleFacturaGridDTO>();
		}
	}
}
