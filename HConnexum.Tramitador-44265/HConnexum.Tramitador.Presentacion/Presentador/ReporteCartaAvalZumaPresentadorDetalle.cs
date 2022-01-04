using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Datos;
using System.Data;
using HConnexum.Infraestructura;
using System.Web;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
    public class ReporteCartaAvalZumaPresentadorDetalle : PresentadorDetalleBase<Caso>
    {

        readonly IReporteCartaAvalZuma vista;
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

        public ReporteCartaAvalZumaPresentadorDetalle(IReporteCartaAvalZuma vista)
        {
            this.vista = vista;
            this.vista.NombreTabla = GetType();
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

        //public IEnumerable<DatosSolicitudDTO> GenerarConsultaReporte(int idCarta, string conexionString)
        public IEnumerable<ReporteCartaAvalZumaDTO> GenerarConsultaReporte(int idCarta, int idSuscriptor)
        {
            DataSet dataSetColeccion = new DataSet();
            ConexionADO servicio = new ConexionADO();
            string conexionString = ObtenerConexionString(idSuscriptor);
            if (!string.IsNullOrWhiteSpace(conexionString))
                using (SqlConnection connection = new SqlConnection(conexionString))
                {
                    try
                    {
                        SqlParameter[] colleccionParameterPaciente = new SqlParameter[1];
                        colleccionParameterPaciente[0] = new SqlParameter("@idcarta", idCarta);
                        dataSetColeccion.Tables.Add(servicio.EjecutaStoredProcedure(WebConfigurationManager.AppSettings[@"SpTraeCartaAval"], conexionString, colleccionParameterPaciente).Tables[0].Copy());
                        dataSetColeccion.Tables["DatosGestor"].TableName = "DatosPaciente";
                        SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
                        DataTable test = suscriptorRepositorio.ObtenerSuscriptorporIdCompleto_(idSuscriptor);
                        //dataSetColeccion.Tables["SuscriptoresDTO"].TableName = "DatosSuscriptoresDTO";
                        dataSetColeccion.Tables.Add(test);
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        if (connection != null)
                            connection.Close();
                    }
                }
            return ConversionDatasetToIEnumerable(dataSetColeccion);
        }

        public IEnumerable<ReporteCartaAvalZumaDTO> ConversionDatasetToIEnumerable(DataSet dataSetColeccion)
        {
            var iEnumerableColeccion = (from TabDatosgestor in dataSetColeccion.Tables["DatosPaciente"].AsEnumerable()
                                        select new ReporteCartaAvalZumaDTO
                                        {
                                            Clinica = TabDatosgestor.Field<string>("clinica"),
                                            Contratante = TabDatosgestor.Field<string>("a00Contratante"),
                                            Titular = TabDatosgestor.Field<string>("a02Titular"),
                                            CiTitular = TabDatosgestor.Field<string>("a02Ci_Tit"),
                                            Asegurado = TabDatosgestor.Field<string>("asegurado"),
                                            CiAsegurado = TabDatosgestor.Field<string>("ciaseg"),

                                            Parentesco = TabDatosgestor.Field<string>("parentesco"),
                                            Expediente = TabDatosgestor.Field<int>("expediente"),
                                            Reclamo = TabDatosgestor.Field<int>("reclamo"),

                                            presupuesto = TabDatosgestor.Field<double?>("presupuesto"),
                                            MontoCubierto = TabDatosgestor.Field<double?>("monto_cubierto"),
                                            Deducible = TabDatosgestor.Field<double?>("Deducible"),
                                            Diagnostico = TabDatosgestor.Field<string>("diagnosti"),
                                            Tratamiento = TabDatosgestor.Field<string>("Tratamiento"),
                                            FechaSolicitud = TabDatosgestor.Field<DateTime>("fecha_solicitud"),
                                            FechaVencimiento = TabDatosgestor.Field<DateTime?>("Fecha_Vencimiento_CA"),
                                            logo = (from tabDatosLogo in dataSetColeccion.Tables["SuscriptorDTO"].AsEnumerable()
                                                    select new ReporteSuscriptorZuma
                                                          {
															  LogoSuscriptor = (string.IsNullOrWhiteSpace(tabDatosLogo.Field<string>("Logo")) ? WebConfigurationManager.AppSettings["UrlImgs"] + @"No_Disponible.jpg" : tabDatosLogo.Field<string>("Logo")),
                                                              suscriptor = tabDatosLogo.Field<string>("Nombre"),
                                                              Numdoc = tabDatosLogo.Field<string>("NumDoc"),
                                                              Telefono = "0212-4562536",
                                                              Fax = tabDatosLogo.Field<string>("Fax")
                                                          }).FirstOrDefault(),
                                        });
            return iEnumerableColeccion;

        }
    }
}
