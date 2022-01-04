using System;
using System.Linq;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Datos;
using HConnexum.Infraestructura;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Configuration;
using HConnexum.Tramitador.Negocio;
using System.ServiceModel;
using System.Globalization;

///<summary>Namespace que engloba el presentador del Repote de Opinión Médica de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase ReporteOpMedPresentador.</summary>
	public class ReporteOpMedPresentador //: PresentadorBase<HConnexum.Tramitador.Negocio.ReporteOpMedDTO>
	{
		///<summary>Variable vista de la interfaz IReporteOpMed.</summary>
		readonly IReporteOpMed vista;

		///<summary>Variable de la entidad ReporteOpMedDTO.</summary>
		ReporteOpMedDTO _ReporteOpMedDTO;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public ReporteOpMedPresentador(IReporteOpMed vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public ReporteOpMedDTO MostrarVista()
		{
			ReporteOpMedDTO resultado = null;
			try
			{
				resultado = ObtenerReporteOpMed(this.vista);
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return resultado;
		}

		#region "ObtenerReporteOpMed()"

		public ReporteOpMedDTO ObtenerReporteOpMed(IReporteOpMed vista)
		{
			DataSet ds = ObtenerParametroPorPaso(vista.IdMovimiento);
			ReporteOpMedDTO resultado = new ReporteOpMedDTO();
			if ((ds != null) && (ds.Tables[0].Rows.Count > 0))
			{
				string codIndExterno = (from p in ds.Tables[0].AsEnumerable()
										where p.Field<string>(atributoNombre).ToUpper() == @"CODIDEXTERNOINT"
										select p.Field<string>(atributoValor)).SingleOrDefault();

				codIndExterno = codIndExterno != null ? codIndExterno : String.Empty;
				SuscriptorRepositorio sr = new SuscriptorRepositorio(udt);

				HttpContext.Current.Trace.Warn(String.Format("Inicia Carga del logo , codigo externo {0}", codIndExterno));
				resultado.Logos = ConfigurationManager.AppSettings[@"UrlImgReporteOpMed"].ToString().Remove(0,2);
				if (!codIndExterno.Equals(String.Empty))
					resultado.Logos = sr.ObtenerLogo(int.Parse(codIndExterno), SuscriptorRepositorio.TipoId.CodIndExterno).Remove(0,2);
				resultado.FechaActual = DateTime.Now.ToShortDateString(); // Esperando por el campo en base de datos.
				resultado.IdExterno = (from p in ds.Tables[0].AsEnumerable()
									   where p.Field<string>(atributoNombre).ToUpper() == @"IDCASOEXTERNO"
									   select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Titular = (from p in ds.Tables[0].AsEnumerable()
									 where p.Field<string>(atributoNombre).ToUpper() == @"NOMTITULAR"
									 select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Beneficiario = (from p in ds.Tables[0].AsEnumerable()
										  where p.Field<string>(atributoNombre).ToUpper() == @"NOMASEG"
										  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.CedulaBeneficiario = (from p in ds.Tables[0].AsEnumerable()
									where p.Field<string>(atributoNombre).ToUpper() == @"NUMDOCASEG"
										  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.CedulaTitular = (from p in ds.Tables[0].AsEnumerable()
										   where p.Field<string>(atributoNombre).ToUpper() == @"NUMDOCTITULAR"
												select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Diagnostico = (from p in ds.Tables[0].AsEnumerable()
										 where p.Field<string>(atributoNombre).ToUpper() == @"DIAGNOSTICO"
										 select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Tratamiento = (from p in ds.Tables[0].AsEnumerable()
										 where p.Field<string>(atributoNombre).ToUpper() == @"TRATAMIENTO"
										 select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.AsesorMedico = (from p in ds.Tables[0].AsEnumerable()
										  where p.Field<string>(atributoNombre).ToUpper() == @"NOMMED"
										  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Sexo = (from p in ds.Tables[0].AsEnumerable()
								  where p.Field<string>(atributoNombre).ToUpper() == @"SEXOASEG"
								  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Edad = (from p in ds.Tables[0].AsEnumerable()
								  where p.Field<string>(atributoNombre).ToUpper() == @"EDADASEG"
								  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Parentesco = (from p in ds.Tables[0].AsEnumerable()
										where p.Field<string>(atributoNombre).ToUpper() == @"PARENTESCOASEG"
										select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Servicio = (from p in ds.Tables[0].AsEnumerable()
									  where p.Field<string>(atributoNombre).ToUpper() == @"SERVICIO"
									  select p.Field<string>(atributoValor)).SingleOrDefault();
				if (string.IsNullOrWhiteSpace(resultado.Servicio))
				{
					MovimientoRepositorio movimientoRepositorio = new MovimientoRepositorio(udt);
					Movimiento movimiento = movimientoRepositorio.ObtenerPorId(vista.IdMovimiento);
					FlujosServicioRepositorio flujosServicioRepositorio = new FlujosServicioRepositorio(udt);
					FlujosServicioDTO flujo = flujosServicioRepositorio.ObtenerDtoFlujosServicioPorId(movimiento.Caso1.FlujosServicio.Id);
					resultado.Servicio = flujo.NombreServicioSuscriptor;
				}
				resultado.MontoCubierto = (from p in ds.Tables[0].AsEnumerable()
										   where p.Field<string>(atributoNombre).ToUpper() == @"MONTO"
										   select p.Field<string>(atributoValor)).SingleOrDefault();
				HttpContext.Current.Trace.Warn(String.Format("Monto del Cubierto", resultado.MontoCubierto));

				resultado.Antecedentes = (!string.IsNullOrWhiteSpace(vista.Antecedentes)) ? vista.Antecedentes : (from p in ds.Tables[0].AsEnumerable()
																												  where p.Field<string>(atributoNombre).ToUpper() == @"ANTECEDENTES"
																												  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Cicatrices = (!string.IsNullOrWhiteSpace(vista.Cicatrices)) ? vista.Cicatrices : (from p in ds.Tables[0].AsEnumerable()
																											where p.Field<string>(atributoNombre).ToUpper() == @"CICATRICES"
																											select p.Field<string>(atributoValor)).SingleOrDefault();
				
				resultado.Peso = (!string.IsNullOrWhiteSpace(vista.Peso)) ? vista.Peso : (from p in ds.Tables[0].AsEnumerable()
																						  where p.Field<string>(atributoNombre).ToUpper() == @"PESO"
																						  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Talla = (!string.IsNullOrWhiteSpace(vista.Talla)) ? vista.Talla : (from p in ds.Tables[0].AsEnumerable()
																							 where p.Field<string>(atributoNombre).ToUpper() == @"TALLA"
																							 select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.TensionArterial = (!string.IsNullOrWhiteSpace(vista.TensionArterial)) ? vista.TensionArterial : (from p in ds.Tables[0].AsEnumerable()
																														   where p.Field<string>(atributoNombre).ToUpper() == @"TENSION"
																														   select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.OpMed = (!string.IsNullOrWhiteSpace(vista.OpMed)) ? vista.OpMed : (from p in ds.Tables[0].AsEnumerable()
																							 where p.Field<string>(atributoNombre).ToUpper() == @"OPMED"
																							 select p.Field<string>(atributoValor)).SingleOrDefault().ToUpper();
				string fechaOpMed = (from p in ds.Tables[0].AsEnumerable()
									 where p.Field<string>(atributoNombre).ToUpper() == @"FECHAOPMED"
									 select p.Field<string>(atributoValor)).SingleOrDefault();
				DateTime objDate = DateTime.Now;
				if (!string.IsNullOrWhiteSpace(fechaOpMed))
				{
					DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
					dtfi.ShortDatePattern = "yyyyMMdd";
					objDate = DateTime.ParseExact(fechaOpMed, "d", dtfi);
				}
				resultado.FechaOpMed = string.IsNullOrWhiteSpace(fechaOpMed) ? DateTime.Now.ToShortDateString() : objDate.ToShortDateString();
				resultado.Observaciones = (!string.IsNullOrWhiteSpace(vista.Observacionmed)) ? vista.Observacionmed : (from p in ds.Tables[0].AsEnumerable()
																													   where p.Field<string>(atributoNombre).ToUpper() == @"OBSERVACIONMED"
																													   select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Telefono = (from p in ds.Tables[0].AsEnumerable()
									  where p.Field<string>(atributoNombre).ToUpper() == @"TLFASEG"
									  select p.Field<string>(atributoValor)).SingleOrDefault();
				resultado.Firma = string.Empty;
				resultado.SupportIncident = (from p in ds.Tables[0].AsEnumerable()
											 where p.Field<string>(atributoNombre).ToUpper() == @"SUPPORTINCIDENT"
											 select p.Field<string>(atributoValor)).SingleOrDefault();
			}
			else
			{
				resultado.IdMovimiento = vista != null ? vista.IdMovimiento : 0;
				resultado.Logos = ConfigurationManager.AppSettings[@"UrlImgReporteOpMed"].ToString().Remove(0,2);
				resultado.FechaActual = string.Empty;
				resultado.IdExterno = string.Empty;
				resultado.Titular = string.Empty;
				resultado.Beneficiario = string.Empty;
				resultado.CedulaBeneficiario = string.Empty;
				resultado.CedulaTitular = string.Empty;
				resultado.Diagnostico = string.Empty;
				resultado.Tratamiento = string.Empty;
				resultado.AsesorMedico = string.Empty;
				resultado.Sexo = string.Empty;
				resultado.Edad = string.Empty;
				resultado.Parentesco = string.Empty;
				resultado.Servicio = string.Empty;
				resultado.MontoCubierto = string.Empty;
				resultado.Antecedentes = vista != null ? vista.Antecedentes : string.Empty;
				resultado.Cicatrices = vista != null ? vista.Cicatrices : string.Empty;
				resultado.Peso = vista != null ? vista.Peso : string.Empty;
				resultado.Talla = vista != null ? vista.Talla : string.Empty;
				resultado.TensionArterial = vista != null ? vista.TensionArterial : string.Empty;
				resultado.OpMed = vista != null ? vista.OpMed : string.Empty;
				resultado.Observaciones = vista != null ? vista.Observacionmed : string.Empty;
				resultado.Telefono = string.Empty;
				resultado.Firma = string.Empty;
				resultado.SupportIncident = string.Empty;
			}
			return resultado;
		}

		private DataSet ObtenerParametroPorPaso(int idMovimiento)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
				var movimiento = (from pr in tabMovimiento
								  where pr.Id == idMovimiento &&
										pr.IndEliminado == IndEliminado &&
										pr.IndVigente == IndVigente &&
										pr.FechaValidez <= FechaValidez
								  select pr).SingleOrDefault();
				
				if (movimiento != null)
				{
					HttpContext.Current.Trace.Warn(String.Format("Se encontro el caso : {0} para el movimiento: {1}", movimiento.IdCaso, idMovimiento));
					if (movimiento.IdCaso != null)
					{
						DataSet ds = servicio.ObtenerParametrosPorCaso(movimiento.IdCaso);
						if (ds.Tables[@"Error"] != null)
							throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
						if (ds.Tables[0].Rows.Count > 0)
							return ds;
					}
				}
				else
				{
					string sQuery = String.Format("from pr in tabMovimiento where pr.Id == {0} &&  pr.IndEliminado == {1} && pr.IndVigente == {2} &&  pr.FechaValidez <= {3} select pr).SingleOrDefault();", idMovimiento, IndEliminado, IndVigente, FechaValidez);
					HttpContext.Current.Trace.Warn(String.Format("No se encontró el movimiento: {0} Query ejecutado= {1}", movimiento.IdCaso, sQuery));
					throw new Exception(String.Format("No se encontró el movimiento: {0} Query ejecutado= {1}", movimiento.IdCaso, sQuery));
				}
			}
			catch (Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if (ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			finally
			{
				if (servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			HttpContext.Current.Trace.Warn("Se retorno NUll");
			return null;
		}

		#endregion "ObtenerReporteOpMed()"

		#region "Propiedades"

		public string atributoNombre
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoNombreGenerales"].ToString();
			}
		}

		public string atributoValor
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoValor"].ToString();
			}
		}

		public bool IndEliminado
		{
			get
			{
				return false;
			}
		}

		public bool IndVigente
		{
			get
			{
				return true;
			}
		}

		public DateTime FechaValidez
		{
			get
			{
				return DateTime.Now;
			}
		}
		
		#endregion "Propiedades"
	}
}