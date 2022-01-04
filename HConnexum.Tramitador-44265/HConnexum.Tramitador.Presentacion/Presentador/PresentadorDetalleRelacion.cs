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
	public class PresentadorDetalleRelacion : PresentadorDetalleBase<DetalleRemesaDTO>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IPantallaDetalleRelacion vista;
		public PresentadorDetalleRelacion(IPantallaDetalleRelacion vista)
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

		public void CargarDetalleRelacion(int idCodExtUsrActual, string sP, int nremesa, string conexionString)
		{
			try
			{
				ConexionADO servicio = new ConexionADO();
				SqlParameter[] colleccionParameter = new SqlParameter[3];
				colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
				colleccionParameter[1] = new SqlParameter("@seguro", vista.IdCodExterno);
				colleccionParameter[2] = new SqlParameter("@codigoremesa", nremesa);
				using(DataSet dataGrid = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
				{
					if((dataGrid != null) && (dataGrid.Tables[0].Rows.Count > 0))
					{
						vista.NRelacion = dataGrid.Tables[0].Rows[0]["reclamo"].ToString();
						vista.FechaCreacion = Convert.ToDateTime(dataGrid.Tables[0].Rows[0]["Rn_Create_Date"].ToString()).ToShortDateString();
						vista.FechaPago = dataGrid.Tables[0].Rows[0]["Fecha_Pago"].ToString();
						vista.FormaPago = dataGrid.Tables[0].Rows[0]["Tipo"].ToString();
						vista.MontoCubierto = string.Format("{0:N}",dataGrid.Tables[0].Rows[0]["Total_Facturado"]);
						vista.MontoImpMunicipal = string.Format("{0:N}",dataGrid.Tables[0].Rows[0]["Monto_Imp_Municipal"]);
						vista.MontoSujetoRetencion = string.Format("{0:N}",dataGrid.Tables[0].Rows[0]["Total_Sujeto_Ret"]);
						vista.TotalPagar = string.Format("{0:N}",dataGrid.Tables[0].Rows[0]["Total_Reclamos"]);
						vista.TotalRetenido = string.Format("{0:N}",dataGrid.Tables[0].Rows[0]["Total_Retencion"]);
						vista.Status = dataGrid.Tables[0].Rows[0]["Status"].ToString();
						vista.Banco = dataGrid.Tables[0].Rows[0]["bt"].ToString();
						vista.Referencia = dataGrid.Tables[0].Rows[0]["Referencia"].ToString();
						vista.NumeroCasos = dataGrid.Tables[0].Rows[0]["Nro_Reclamos_Asociados"].ToString();
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

		public IQueryable<DetalleRemesaGridDTO> ConvertirtoEntity(DataSet coleccion)
		{
			var iqueriable = (from Tabcoleccion in coleccion.Tables[0].AsEnumerable()
								select new DetalleRemesaGridDTO
								{
									NReclamosEncriptado = Tabcoleccion.Field<string>(@"nReclamosEncriptado"),
									Reclamo = Tabcoleccion.Field<int>(@"expediente").ToString() + "-" + Tabcoleccion.Field<string>(@"Nro_Reclamo"),
									NroReclamo = Tabcoleccion.Field<string>(@"Nro_Reclamo"),
									Asegurado = Tabcoleccion.Field<string>(@"a02asegurado"),
									DocAsegurado = Tabcoleccion.Field<string>(@"a02NoDocasegurado"),
									Factura = Tabcoleccion.Field<string>(@"factura"),
									FechaOcurrencia = Tabcoleccion.Field<DateTime?>(@"Fec_Ocurrencia"),
									Monto = Tabcoleccion.Field<double?>(@"monto"),
								}).AsQueryable();
			return iqueriable;
		}

		public IEnumerable<DetalleRemesaGridDTO> CargarResumenCaso(int idCodExtUsrActual, string sP, int nremesa, string conexionString, IList<Filtro> parametrosFiltro, int npagina, int nregistros)
		{
			try
			{
				ConexionADO servicio = new ConexionADO();
				SqlParameter[] colleccionParameter = new SqlParameter[3];
				colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
				colleccionParameter[1] = new SqlParameter("@seguro", vista.IdCodExterno);
				colleccionParameter[2] = new SqlParameter("@reclamo", nremesa);
				using(DataSet dataGrid = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
				{
					if((dataGrid != null) && (dataGrid.Tables[0].Rows.Count > 0))
					{
						DataTable tabla = dataGrid.Tables[0];
						GenerarColumnEncriptada(ref tabla, @"Nro_Reclamo", @"nReclamosEncriptado");
						vista.NumeroDeRegistros = dataGrid.Tables[0].Rows.Count;
						IEnumerable<DetalleRemesaGridDTO>  collection = ConvertirtoEntity(dataGrid);
						collection = UtilidadesDTO<DetalleRemesaGridDTO>.FiltrarPaginar(collection.AsQueryable(), npagina, nregistros, parametrosFiltro);
						if(parametrosFiltro.Count > 0)
							collection = UtilidadesDTO<DetalleRemesaGridDTO>.FiltrarColeccion(collection.AsQueryable(), parametrosFiltro);
						vista.Imprimir = true;
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
			return Enumerable.Empty<DetalleRemesaGridDTO>();
		}
	}
}
