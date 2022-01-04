using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Collections.Generic;
using HConnexum.Tramitador.Datos;


namespace HConnexum.Tramitador.Presentacion.Presentador
{
    public class PresentadorBusquedaRelaciones : PresentadorDetalleBase<DetalleRemesaDTO>
    {
        #region "Variables Presentador"
        readonly IBusquedaRelaciones vista;
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
        #endregion "Variables Presentador"

        public PresentadorBusquedaRelaciones(IBusquedaRelaciones vista)
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

        public void LlenarComboIntermediario(int idTipoProveedor)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                using (DataSet ds = servicio.ObtenerSuscriptoresXIdTipoProveedor(idTipoProveedor))
                {
                    if ((ds != null) && (ds.Tables.Count > 0))
                        vista.ComboIntermediario = ds.Tables[0];
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
        }

		public void LlenarCombodelIntermediario(int idTipoProveedor, int idSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				using (DataSet ds = servicio.ObtenerSuscriptorXIdTipoProveedorIntermediario(idTipoProveedor, idSuscriptor))
				{
					if ((ds != null) && (ds.Tables.Count > 0))
						vista.ComboIntermediario = ds.Tables[0];
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
                        vista.ComboTipoProveedor = ds.Tables[0];
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
        }

        public bool LlenarComboProveedor(string idsuscriptor)
        {
            ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();
            try
            {
                SuscriptorRepositorio repositorio = new SuscriptorRepositorio(udt);
                DataSet dsind = repositorio.IndicarExisteProveedorReporteRemesa(int.Parse(idsuscriptor));
                if (dsind.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsind.Tables[0].Rows)
                    {
                        idsuscriptor = Convert.ToString(row[0]);
                    }
                    using (DataSet ds = servicio.ObtenerSuscriptoresPorRedes("15".Encriptar()))
                    {
                        if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
                            vista.ComboProveedor = ds.Tables[0];
                    }
                    return true;
                }
                else{
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
                    servicio.Close();
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
                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["CodIdExterno"].ToString()))
                            return Convert.ToInt32(ds.Tables[0].Rows[0]["CodIdExterno"].ToString());
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

        public IQueryable<RelacionesGridDTO> ConvertirtoEntity(DataSet coleccion)
        {
            var iqueriable = (from Tabcoleccion in coleccion.Tables[0].AsEnumerable()
                              select new RelacionesGridDTO
                              {
                                  NRemesaEncriptado = Tabcoleccion.Field<string>("nRemesaEncriptado"),
                                  NRemesa = Tabcoleccion.Field<string>("numero_remesa"),
                                  RelacionReclamo = Tabcoleccion.Field<int?>("relacion_reclamos"),
                                  FechaCreacion = Tabcoleccion.Field<DateTime?>("Rn_create_Date"),
                                  FechaPago = Tabcoleccion.Field<DateTime?>("Fecha_Pago"),
                                  Estatus = Tabcoleccion.Field<string>("Status"),
                                  MontoPagar = Tabcoleccion.Field<double?>("Total_Reclamos"),
                              }).AsQueryable();
            return iqueriable;
        }

        public IEnumerable<RelacionesGridDTO> LlenarGridDetalleRemesa(int idCodExtUsrActual, string conexionString, string sP, string status, DateTime? fechaInicial, DateTime? fechaFinal, int? nRemesa, byte debug, IList<Filtro> parametrosFiltro, int npagina, int nregistros, string nFactura)
        {
            try
            {
                ConexionADO servicio = new ConexionADO();
                SqlParameter[] colleccionParameter = new SqlParameter[8];
                colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
                colleccionParameter[1] = new SqlParameter("@seguro", vista.IdCodExterno);
                if (string.IsNullOrWhiteSpace(status))
                    colleccionParameter[2] = new SqlParameter("@estatus", DBNull.Value);
                else
                    colleccionParameter[2] = new SqlParameter("@estatus", status);
                colleccionParameter[3] = new SqlParameter("@fecha1", fechaInicial);
                colleccionParameter[4] = new SqlParameter("@fecha2", fechaFinal);
                colleccionParameter[5] = new SqlParameter("@remesa", nRemesa);
                colleccionParameter[6] = new SqlParameter("@debug", debug);
                colleccionParameter[7] = new SqlParameter("@factura", nFactura);
                if (!string.IsNullOrEmpty(conexionString))
                {
                    using (DataSet dataGrid = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
                    {
                        if ((dataGrid != null) && (dataGrid.Tables[0].Rows.Count > 0))
                        {
                            DataTable tabla = dataGrid.Tables[0];
                            GenerarColumnEncriptada(ref tabla, @"relacion_reclamos", @"nRemesaEncriptado");
                            if (!string.IsNullOrWhiteSpace(status))
                            {
                                LlenarPanelTotales(idCodExtUsrActual, WebConfigurationManager.AppSettings[@"Estatus" + status], conexionString);
                                vista.ImprimirVisible = true;
                                vista.Totales = true;
                            }
                            vista.NumeroDeRegistros = dataGrid.Tables[0].Rows.Count;
                            IEnumerable<RelacionesGridDTO> collection = ConvertirtoEntity(dataGrid);
                            collection = UtilidadesDTO<RelacionesGridDTO>.FiltrarPaginar(collection.AsQueryable(), npagina, nregistros, parametrosFiltro);
                            if (parametrosFiltro.Count > 0)
                                collection = UtilidadesDTO<RelacionesGridDTO>.FiltrarColeccion(collection.AsQueryable(), parametrosFiltro);
                            return collection;
                        }
                        else{
                            vista.ImprimirVisible = false;
                            vista.Totales = false;
                        }
                    }
                }
                else
                    throw new CustomException("El suscriptor selecionado no posee base de datos");
            }
            catch (CustomException ex)
            {
                Errores.ManejarError(ex, ex.ToString());
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", ex.ToString(), ex);
                this.vista.Errores = ex.Message.ToString();
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            return Enumerable.Empty<RelacionesGridDTO>();
        }

        public void LlenarPanelTotales(int idCodExtUsrActual, string sP, string conexionString)
        {
            int casos = 0;
            int numeroParametros = 2;
            double montocubierto = 0;
            double retenido = 0;
            double impMunicipal = 0;
            double montoTotal = 0;
            try
            {
                if (!string.IsNullOrEmpty(conexionString) && !string.IsNullOrEmpty(sP))
                {
                    if (sP == "pa_reporte_remesas_pagada_HC2")
                    {
                        numeroParametros = 4;
                    }
                    SqlParameter[] colleccionParameter = new SqlParameter[numeroParametros];
                    colleccionParameter[0] = new SqlParameter("@clinica", idCodExtUsrActual);
                    colleccionParameter[1] = new SqlParameter("@seguro", vista.IdCodExterno);
                    if (numeroParametros == 4)
                    {
                        colleccionParameter[2] = new SqlParameter("@fecha1 ", vista.FechaInicial);
                        colleccionParameter[3] = new SqlParameter("@fecha2 ", vista.FechaFinal);
                    }
                    ConexionADO servicio = new ConexionADO();
                    using (DataSet collection = servicio.EjecutaStoredProcedure(sP, conexionString, colleccionParameter))
                    {
                        if ((collection != null) && (collection.Tables.Count > 0))
                        {
                            for (int i = 0; i < collection.Tables[0].Rows.Count; i++)
                            {
                                int nroReclamosAsociados = 0;
                                double totalRetencion = 0;
                                double montoImpMunicipal = 0;
                                double totalReclamos = 0;

                                if (collection.Tables[0].Rows[i]["Nro_Reclamos_Asociados"].ToString() == string.Empty)
                                    nroReclamosAsociados = 0;
                                else
                                    nroReclamosAsociados = Convert.ToInt32(collection.Tables[0].Rows[i]["Nro_Reclamos_Asociados"].ToString());
                                if (collection.Tables[0].Rows[i]["Total_Retencion"].ToString() == string.Empty)
                                    totalRetencion = 0;
                                else
                                    totalRetencion = Convert.ToDouble(collection.Tables[0].Rows[i]["Total_Retencion"].ToString());
                                if (collection.Tables[0].Rows[i]["Monto_Imp_Municipal"].ToString() == string.Empty)
                                    montoImpMunicipal = 0;
                                else
                                    montoImpMunicipal = Convert.ToDouble(collection.Tables[0].Rows[i]["Monto_Imp_Municipal"].ToString());
                                if (collection.Tables[0].Rows[i]["Total_Reclamos"].ToString() == string.Empty)
                                    totalReclamos = 0;
                                else
                                    totalReclamos = Convert.ToDouble(collection.Tables[0].Rows[i]["Total_Reclamos"].ToString());

                                casos += nroReclamosAsociados;
                                montocubierto += totalRetencion + montoImpMunicipal + totalReclamos;
                                retenido += totalRetencion;
                                impMunicipal += montoImpMunicipal;
                                montoTotal += totalReclamos;
                            }
                            vista.Relaciones = collection.Tables[0].Rows.Count.ToString();
                            vista.Casos = casos;
                            vista.MontoCubierto = String.Format("{0:0,0.0}", montocubierto);
                            vista.Retenido = String.Format("{0:0,0.0}", retenido);
                            vista.ImpMunicipal = String.Format("{0:0,0.0}", impMunicipal);
                            vista.MontoTotal = String.Format("{0:0,0.0}", montoTotal);
                        }
                    }
                }
                else
                    throw new CustomException("El Suscriptor Selecionado no Posee String de Conexion");
            }
            catch (CustomException ex)
            {
                Errores.ManejarError(ex, ex.ToString());
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", ex.ToString(), ex);
                this.vista.Errores = this.vista.Errores = ((System.Exception)(ex)).Message;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
        }
    }
}
