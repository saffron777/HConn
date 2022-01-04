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
	public class PresentadorReporteRelaciones : PresentadorDetalleBase<Caso>
	{
		///<summary>Variable vista de la interfaz IReporteCasos.</summary>
		readonly IReporteRelaciones vista;
		public PresentadorReporteRelaciones(IReporteRelaciones vista)
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

		public IEnumerable<RelacionesDTO> ConvertirtoEntity(DataSet dataSetCollection)
		{
			var iEnumerableRelaciones = from TabCollectionRelaciones in dataSetCollection.Tables["DatosGestor"].AsEnumerable()
										select new RelacionesDTO
										{
											Logo = (from tabDatosLogo in dataSetCollection.Tables[@"DatosLogo"].AsEnumerable()
													select tabDatosLogo.Field<string>(@"logo")).SingleOrDefault(),
											NRemesa = TabCollectionRelaciones.Table.Columns.Contains("numero_remesa") ? TabCollectionRelaciones.Field<string>("numero_remesa") : string.Empty,
											RelacionReclamo = TabCollectionRelaciones.Field<int?>("relacion_reclamos"),
											FechaCierre = TabCollectionRelaciones.Field<DateTime?>("Fecha_Cierre"),
											NCasos = TabCollectionRelaciones.Field<int?>("Nro_Reclamos_Asociados"),
											MontoCubierto = TabCollectionRelaciones.Field<double?>("Total_Retencion") + TabCollectionRelaciones.Field<double>("Monto_Imp_Municipal") + TabCollectionRelaciones.Field<double>("Total_Reclamos"),
											Retenido = TabCollectionRelaciones.Field<double?>("Total_Retencion"),
											ImpMunicipal = TabCollectionRelaciones.Field<double?>("Monto_Imp_Municipal"),
											MontoPagar = TabCollectionRelaciones.Field<double?>("Total_Reclamos"),
										};
			return iEnumerableRelaciones;
		}
		public IEnumerable<RelacionesDTO> GenerarReporte(int idCodExtUsrActual, int idCodExterno, string sP, string conexionString, DateTime? fechainicial, DateTime? fechafinal, string estatus)
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
//#if DEBUG
//                logo = logo.Replace("~/", "");
//#endif
                logo = logo.Replace("~/", "");

				dataSetCollection.Tables[@"DatosLogo"].Rows.Add(logo);

				if(estatus == "Enviada" || estatus == "Pendiente")
				{
					SqlParameter[] colleccionParameter = new SqlParameter[2];
					colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
					colleccionParameter[1] = new SqlParameter("@seguro", idCodExterno);
					dataSetCollection.Tables.Add(servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter).Tables[0].Copy());
				}
				else
				{
					SqlParameter[] colleccionParameter = new SqlParameter[4];
					colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
					colleccionParameter[1] = new SqlParameter("@seguro", idCodExterno);
					colleccionParameter[2] = new SqlParameter("@fecha1", fechainicial);
					colleccionParameter[3] = new SqlParameter("@fecha2", fechafinal);
					dataSetCollection.Tables.Add(servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter).Tables[0].Copy());
				}
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return ConvertirtoEntity(dataSetCollection);
		}
	}
}
