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
	public class PantallaBusquedaFacturasPresentador : PresentadorDetalleBase<DetalleFacturaDTO>
	{
		#region "Variables Presentador"
		
		readonly IPantallaBusquedaFacturas vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		
		#endregion "Variables Presentador"
		
		public PantallaBusquedaFacturasPresentador(IPantallaBusquedaFacturas vista)
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
		
		public void LlenarComboIntermediario(int idTipoProveedor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerSuscriptoresXIdTipoProveedor(idTipoProveedor))
				{
					if ((ds != null) && (ds.Tables.Count > 0))
					{
						this.vista.ComboIntermediario = ds.Tables[0];
					}
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
				{
					servicio.Close();
				}
			}
		}
		
		//FUNCIÓN CREADA PARA LLENAR EL COMBO DE INETRMEDIARIO
		
		public void LlenarCombodelIntermediario(int idTipoProveedor, int idSuscriptor)
		{
			PantallaBusquedaFacturasRepositorio busquedaFacturasRepositorio = new PantallaBusquedaFacturasRepositorio(this.udt);
			try
			{
				DataSet ds = busquedaFacturasRepositorio.ObtenerSuscriptorXIdTipoProveedorIntermediario(idTipoProveedor, idSuscriptor);
				{
					if ((ds != null) && (ds.Tables.Count > 0))
					{
						this.vista.ComboIntermediario = ds.Tables[0];
					}
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
		
		public void LlenarComboTipoProveedor()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				int[] idAseguradoras = ConfigurationManager.AppSettings["IdAseguradoras"].Split(';').Select(a => int.Parse(a)).ToArray();
				using (DataSet ds = servicio.ObtenerTiposSuscriptoresXId(idAseguradoras))
				{
					if ((ds != null) && (ds.Tables.Count > 0))
					{
						this.vista.ComboTipoProveedor = ds.Tables[0];
					}
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
				{
					servicio.Close();
				}
			}
		}
		
		public bool LlenarComboProveedor(string idsuscriptor)
		{
			ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();
			try
			{
				PantallaBusquedaFacturasRepositorio repositorio = new PantallaBusquedaFacturasRepositorio(this.udt);
				DataSet dsind = repositorio.IndicarExisteProveedorReporteFactura(int.Parse(idsuscriptor));
				if (dsind.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dsind.Tables[0].Rows)
					{
						idsuscriptor = Convert.ToString(row[0]);
					}
					using (DataSet ds = servicio.ObtenerSuscriptoresPorRedes("15".Encriptar()))
					{
						if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
						{
							this.vista.ComboProveedor = ds.Tables[0];
						}
					}
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				return false;
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
				{
					servicio.Close();
				}
			}
		}
		
		public int ObtenerIdCodExterno(int idCompañia)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerSuscriptorPorID(idCompañia))
				{
					if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
					{
						if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["CodIdExterno"].ToString()))
						{
							return Convert.ToInt32(ds.Tables[0].Rows[0]["CodIdExterno"].ToString());
						}
					}
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
				{
					servicio.Close();
				}
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
					{
						return ds.Tables[0].Rows[0]["ConexionString"].ToString();
					}
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
				{
					servicio.Close();
				}
			}
			return string.Empty;
		}
		
		public IQueryable<FacturasGridDTO> ConvertirtoEntity(DataSet coleccion)
		{
			var iqueriable = (from Tabcoleccion in coleccion.Tables[0].AsEnumerable()
							  select new FacturasGridDTO
							  {
								  NFacturaEncriptado = Tabcoleccion.Field<string>("NFacturaEncriptado"),
								  NFactura = Tabcoleccion.Field<string>("NFactura"),
								  FechaCreacion = Tabcoleccion.Field<DateTime?>("Fec_Rec_Fact"),
								  FechaPago = Tabcoleccion.Field<DateTime?>("Fecha_Pago"),
								  Estatus = Tabcoleccion.Field<string>("Status"),
								  MontoPagar = Tabcoleccion.Field<double?>("MontoCubierto"),
							  }).AsQueryable();
			return iqueriable;
		}

		public IEnumerable<FacturasGridDTO> LlenarGridDetalleFactura(int idCodExtUsrActual, string conexionString, string sP, string status, DateTime? fechaInicial, DateTime? fechaFinal, IList<Filtro> parametrosFiltro, int npagina, int nregistros, int? nReclamo, string nCedula, string nFactura)
		{
			try
			{
				ConexionADO servicio = new ConexionADO();
				SqlParameter[] colleccionParameter = new SqlParameter[8];
				colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
				colleccionParameter[1] = new SqlParameter("@seguro", this.vista.IdCodExterno);
				colleccionParameter[2] = new SqlParameter("@estatus", status);
				colleccionParameter[3] = new SqlParameter("@fecha1", fechaInicial);
				colleccionParameter[4] = new SqlParameter("@fecha2", fechaFinal);
				colleccionParameter[5] = new SqlParameter("@reclamo", nReclamo);
				colleccionParameter[6] = new SqlParameter("@cedula", nCedula);
				colleccionParameter[7] = new SqlParameter("@factura", nFactura);
				
				if (!string.IsNullOrEmpty(conexionString))
				{
					using (DataSet dataGrid = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
					{
						if ((dataGrid != null) && (dataGrid.Tables[0].Rows.Count > 0))
						{
							DataTable tabla = dataGrid.Tables[0];
							this.GenerarColumnEncriptada(ref tabla, @"nfactura", @"nFacturaEncriptado");
							
							this.vista.NumeroDeRegistros = dataGrid.Tables[0].Rows.Count;
							IEnumerable<FacturasGridDTO> collection = this.ConvertirtoEntity(dataGrid);
							collection = UtilidadesDTO<FacturasGridDTO>.FiltrarPaginar(collection.AsQueryable(), npagina, nregistros, parametrosFiltro);
							if (parametrosFiltro.Count > 0)
							{
								collection = UtilidadesDTO<FacturasGridDTO>.FiltrarColeccion(collection.AsQueryable(), parametrosFiltro);
							}
							return collection;
						}
					}
				}
				else
				{
					throw new CustomException("El suscriptor selecionado no posee base de datos");
				}
			}
			catch (CustomException ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				if (ex.InnerException != null)
				{
					HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", ex.InnerException.Message));
				}
				HttpContext.Current.Trace.Warn("Error", ex.ToString(), ex);
				this.vista.Errores = ((System.Exception)(ex)).Message;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return Enumerable.Empty<FacturasGridDTO>();
		}
	}
}
