using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase MovimientoPresentadorDetalle.</summary>
	public class MisActividadesPresentadorDetalle : PresentadorDetalleBase<Movimiento>
	{
		///<summary>Variable vista de la interfaz IMovimientoDetalle.</summary>
		readonly IMisActividadesDetalle vista;

		///<summary>Variable de la entidad Movimiento.</summary>
		MovimientoDTO movimiento;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public MisActividadesPresentadorDetalle(IMisActividadesDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
				this.movimiento = repositorio.ObtenerDTO(this.vista.Id);
				if(this.movimiento != null)
				{
					
                    //Manejo de boton atender
                    if (this.movimiento.Estatus != WebConfigurationManager.AppSettings[@"ListaValorEstatusEjecutado"].ToString())
                    {
                        this.vista.atender = true;
                    }
                    else {
                        this.vista.atender = false;
                    }
                    // fin de manejo
                    
                    BuscaSexoEdadAsegurado(movimiento.Id);
					this.PresentadorAVista();
					ObservacionesMovimientoRepositorio repositorioObservaciones = new ObservacionesMovimientoRepositorio(udt);
					IEnumerable<ObservacionesMovimientosDTO> listaObservaciones = repositorioObservaciones.ObtenerDTO(movimiento.Id);
					string observaciones = "";
					foreach(ObservacionesMovimientosDTO observacion in listaObservaciones)
						observaciones += observacion.Observacion + "\r\n\r\n";
					vista.Observaciones = String.IsNullOrWhiteSpace(observaciones.ToString()) ? "No Disponible" : observaciones.ToString();
                    if (movimiento.indChat != null)
                        vista.habilitoChat = (bool)movimiento.indChat; 
				}
				else
					throw new CustomException("No se pudo cargar el movimiento");
			}
			catch(CustomException ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", ex.ToString(), ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

        public int BuscaMensajesPendienteChat(int IdCaso, int IdMovimiento)
        {
            try
            {
                BuzonChatRepositorio Buzon = new BuzonChatRepositorio(udt);
                int mensajes = Buzon.ObtenerMNoLeidoporMovDTO(IdMovimiento, IdCaso, vista.UsuarioActual.SuscriptorSeleccionado.Id);
                return mensajes;

            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
                return 0;
            }
        }


		private string ObtenerValor(DataSet ds, string keyvalor)
		{
			keyvalor = (from p in ds.Tables[0].AsEnumerable()
						where p.Field<string>(AtributoNombre).ToUpper() == keyvalor.ToUpper()
						select p.Field<string>(AtributoValor)).SingleOrDefault();
			return keyvalor;
		}

		private DataSet ObtenerParametroPorPaso(int idMovimiento)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			Movimiento movimiento = (from pr in tabMovimiento
									 where pr.Id == idMovimiento &&
										   pr.IndEliminado == false &&
										   pr.IndVigente == true &&
										   pr.FechaValidez <= DateTime.Now
									 select pr).SingleOrDefault();

			return servicio.ObtenerParametrosPorCaso(movimiento.IdCaso);
		}

		#region Parametros Generales
		public string AtributoNombre
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoNombreGenerales"];
			}
		}

		public string AtributoValor
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoValor"];
			}
		}
		#endregion Parametros Generales

		#region propiedades
		public string Sexo
		{
			get
			{
				return @"SexoAseg";
			}
		}

        public string SexoC
        {
            get
            {
                return @"SEXOBENEF";
            }
        }
		public string Edad
		{
			get
			{
				return @"EdadAseg";
			}
		}
        public string FechaNac
        {
            get
            {
                return @"FECNACBENEF";
            }
        }
		public string Asegurado
		{
			get
			{
				return @"NomAseg";
			}
		}
        public string AseguradoC
        {
            get
            {
                return @"NOMCOMPLETOBENEF";
            }
        }
		public string MovilAsegurado
		{
			get
			{
				return @"TlfAseg";
			}
		}
        public string MovilAseguradoC
        {
            get
            {
                return @"TLFSOLICITANTE";
            }
        }
		/// <summary>Nombre del intermediario.</summary>
		public string Intermediario
		{
			get
			{
				return @"Intermediario";
			}
		}
        public string IntermediarioC
        {
            get
            {
                return @"NOMSUSINTERMED";
            }
        }
		/// <summary>Nombre del contratante.</summary>
		public string Contratante
		{
			get
			{
				return @"Contratante";
			}
		}
        public string ContratanteC
        {
            get
            {
                return @"NOMCONTRATANTE";
            }
        }
		#endregion

		public void BuscaSexoEdadAsegurado(int idMov)
		{
			try
			{
				DataSet ds = ObtenerParametroPorPaso(idMov);
				string valorSexo = string.Empty + ObtenerValor(ds, Sexo);
                if (string.IsNullOrEmpty(valorSexo))
                    valorSexo = string.Empty + ObtenerValor(ds, SexoC);

				string valorEdad = string.Empty + ObtenerValor(ds, Edad);
                if (string.IsNullOrEmpty(valorEdad))
                {
                    DateTime fechaNacimiento = Convert.ToDateTime(ObtenerValor(ds, FechaNac));
                    DateTime nacimiento = new DateTime(fechaNacimiento.Year, fechaNacimiento.Month, fechaNacimiento.Day); //Fecha de nacimiento
                    int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
                    

                    int num = DateTime.Now.Year - fechaNacimiento.Year;

                    valorEdad = Convert.ToString(edad) + " Años";
                }
				string valorAsegurado = string.Empty + ObtenerValor(ds, Asegurado);
                if (string.IsNullOrEmpty(valorAsegurado))
                    valorAsegurado = string.Empty + ObtenerValor(ds, AseguradoC);

				string valorMovilAsegurado = string.Empty + ObtenerValor(ds, MovilAsegurado);
                if (string.IsNullOrEmpty(valorMovilAsegurado))
                    valorMovilAsegurado = string.Empty + ObtenerValor(ds, MovilAseguradoC);

				string intermediario = string.Empty + ObtenerValor(ds, this.Intermediario);
                if (string.IsNullOrEmpty(intermediario))
                    intermediario = string.Empty + ObtenerValor(ds, IntermediarioC);

				string contratante = string.Empty + ObtenerValor(ds, this.Contratante);
                if (string.IsNullOrEmpty(contratante))
                    contratante = string.Empty + ObtenerValor(ds, ContratanteC);

				vista.EdadBeneficiario = String.IsNullOrWhiteSpace(valorEdad.ToString()) ? "No Disponible" : valorEdad.ToString();
				vista.SexoBeneficiario = String.IsNullOrWhiteSpace(valorSexo.ToString()) ? "No Disponible" : valorSexo.ToString();
				vista.Solicitante = String.IsNullOrWhiteSpace(valorAsegurado.ToString()) ? "No Disponible" : valorAsegurado.ToString();
				vista.MovilSolicitante = String.IsNullOrWhiteSpace(valorMovilAsegurado.ToString()) ? "No Disponible" : valorMovilAsegurado.ToString();
				vista.Intermediario = String.IsNullOrWhiteSpace(intermediario) ? "No Disponible" : intermediario;
				vista.Contratante = String.IsNullOrWhiteSpace(contratante) ? "No Disponible" : contratante;
			}
			catch(CustomException ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				HttpContext.Current.Trace.Warn("Error", ex.ToString(), ex);
				vista.EdadBeneficiario = "No Disponible";
				vista.SexoBeneficiario = "No Disponible";
				vista.Solicitante = "No Disponible";
				vista.MovilSolicitante = "No Disponible";
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				vista.EdadBeneficiario = "No Disponible";
				vista.SexoBeneficiario = "No Disponible";
				vista.Solicitante = "No Disponible";
				vista.MovilSolicitante = "No Disponible";
			}
		}

		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{
				vista.Servicio = movimiento.NombreServicio;
				vista.NroCaso = movimiento.IdCaso.ToString();
				vista.EstatusCaso = movimiento.NombreEstatusCaso;
				vista.FechaSolicitud = movimiento.FechaSolicitud.ToString();
				vista.Estatus = movimiento.NombreEstatusMovimiento;
				vista.NombrePaso = movimiento.NombrePaso;
				vista.FechaCreacion = movimiento.FechaCreacion.ToString();
				vista.UsuarioCreacion = String.IsNullOrWhiteSpace(movimiento.NombreCreadoPor) ? "No Disponible" : movimiento.NombreCreadoPor;
				vista.Descripcion = String.IsNullOrWhiteSpace(movimiento.DescripcionPaso) ? "No Disponible" : movimiento.DescripcionPaso;
				vista.FechaModificacion = String.IsNullOrWhiteSpace(movimiento.FechaModificacion.ToString()) ? "No Disponible" : movimiento.FechaModificacion.ToString();
				vista.UsuarioModificacion = String.IsNullOrWhiteSpace(movimiento.NombreModificadoPor) ? "No Disponible" : movimiento.NombreModificadoPor;
				vista.FechaProceso = String.IsNullOrWhiteSpace(movimiento.FechaEnProceso.ToString()) ? "No Disponible" : movimiento.FechaEnProceso.ToString();
				vista.IndObligatorio = movimiento.IndObligatorio;
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
        public bool MovimientoDisponible()
        {
            try
            {

                this.udt.IniciarTransaccion();
                MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
                Movimiento movimiento = repositorio.ObtenerPorId(this.vista.Id);
                if (movimiento.Estatus == 154 || movimiento.UsuarioAsignado == vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado)
                    return true;
                else
                    return false;
                                 
                
            }
            catch (Exception e)
            {

                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
                
            }
            return false;
        }
        
		public bool AtenderActividad()
		{
			try
			{
                if (MovimientoDisponible())
                {
                    this.udt.IniciarTransaccion();
                    ListasValorRepositorio repositorioListaValor = new ListasValorRepositorio(udt);
                    MovimientoRepositorio repositorio = new MovimientoRepositorio(udt);
                    CasoRepositorio repositorioCaso = new CasoRepositorio(udt);

                    Movimiento movimiento = repositorio.ObtenerPorId(this.vista.Id);
                    Caso caso=new Caso();
                    caso = repositorioCaso.ObtenerPorId(movimiento.IdCaso); 
                    if (caso.Estatus == int.Parse(WebConfigurationManager.AppSettings[@"EstatusCasoEnPendiente"]))
                    {
                        int N_Mov = repositorio.Count_Movimientos(movimiento.IdCaso);
                        if (N_Mov == 0)
                        {

                            caso.Estatus = int.Parse(WebConfigurationManager.AppSettings[@"EstatusCasoEnProceso"]);
                            caso.FechaEjecucion = DateTime.Now;
                            caso.ModificadoPor = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                            caso.FechaModificacion = DateTime.Now;
                            this.udt.MarcarModificado(caso);
                        }
                    }
                    movimiento.UsuarioAsignado = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
                    movimiento.IdSuscriptor = vista.UsuarioActual.SuscriptorSeleccionado.Id;
                    movimiento.FechaEnProceso = System.DateTime.Now;
                    movimiento.Estatus = repositorioListaValor.ObtenerListaValoresDTO(WebConfigurationManager.AppSettings[@"ListaEstatusMovimiento"], WebConfigurationManager.AppSettings[@"ListaValorEstatusEnProceso"]).Id;
                    this.udt.MarcarModificado(movimiento);
                    this.udt.Commit();
                    
                return true;
                }
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return false;
		}

		public void OmitirMovimiento(int idMovimiento, int idUsuarioSuscriptor, string observaciones)
		{
			try
			{
				ServicioOrquestadorClient servicio = new ServicioOrquestadorClient();
				ListasValorRepositorio lvr = new ListasValorRepositorio(udt);
				ListasValorDTO lvdto = lvr.ObtenerListaValoresDTO("Estatus del Movimiento", "OMITIDO");
				servicio.InsertarObservacionesMovimiento(idMovimiento, idUsuarioSuscriptor, observaciones);
				servicio.ActualizarEstatusMovimiento(idMovimiento, lvdto.Id, lvdto.NombreValor, vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado);
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}