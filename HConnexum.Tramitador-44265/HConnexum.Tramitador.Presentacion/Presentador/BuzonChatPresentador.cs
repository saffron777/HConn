using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class BuzonChatPresentador : BloquesPresentadorBase
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string _mensaje = null;
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string _NAMESPACE = "HConnexum.Tramitador.Presentacion.Presentador";
		/// <summary>Cadena de conexión a la base de datos de Gestor.</summary>
		private string _conexionCadena = null;
		private DataSet _ds = new DataSet();
		private DataTable _dt = new DataTable();
		private string _SP = null;
		private List<SqlParameter> _parametros = new List<SqlParameter>();
		/// <summary>Objeto para manejo de conexión a la BD de la aplicación 'Gestor'. </summary>
		private ConexionADO _connGestor = new ConexionADO();
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		///<summary>Unidad de trabajo para la gestión del acceso a los datos.</summary>
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		///<summary>Vista asociada al presentador.</summary>
		readonly IBuzonChat Vista;
		#endregion

		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public BuzonChatPresentador(IBuzonChat vista)
		{
			try
			{
				this.Vista = vista;

				//var dtoSuscriptor = new SuscriptorRepositorio(udt);
				//Suscriptor suscriptor = dtoSuscriptor.ObtenerPorId(this.Vista.SuscriptorIntermediarioId);
				//if (suscriptor != null)
				//{
				//    this.Vista.IntermediarioNombre = suscriptor.Nombre;
				//    this.Vista.IntermediarioFaxNumero = suscriptor.Telefono;
				//    if (suscriptor.CodIdExterno != null)
				//    {
				//        enlaceCodigo = _NULL_INT;
				//        if (int.TryParse(suscriptor.CodIdExterno, out enlaceCodigo))
				//            this.Vista.IntermediarioGestorId = ObtenerGestorId(this.Vista.SuscriptorIntermediarioId, this.Vista.SuscriptorIntermediarioId, enlaceCodigo);
				//    }
				//}

				//_conexionCadena = ObtenerBDConexionCadena(this.Vista.SuscriptorIntermediarioId);
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		#region M É T O D O S   P R I V A D O S
		/// <summary>Registra la auditoría en la aplicación.</summary>
		/// <param name="pException">Excepción a registar.</param>
		/// <param name="pMensaje">Mensaje a registrar.</param>
		private void Auditoria(Exception pException, string pMensaje)
		{
			/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
			string TRAZA_CATEGORIA_ERROR = "Error";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.Auditoria(pException = {1}, pMensaje = {2})", _NAMESPACE, pException, pMensaje);

				Debug.WriteLine(pException.ToString());
				Errores.ManejarError(pException, pMensaje);
				if(pException.InnerException != null)
					HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR
						, pMensaje + "Error en la capa origen: " + pException.InnerException.Message);
				HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR, pMensaje, pException);
				this.Vista.Errores = MostrarMensaje(TiposMensaje.Error_Generico);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				Errores.ManejarError(ex, _mensaje);
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR
						, _mensaje + "Error en la capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn(TRAZA_CATEGORIA_ERROR, _mensaje, ex);
				this.Vista.Errores = MostrarMensaje(TiposMensaje.Error_Generico);
			}
		}

		#endregion

		#region M É T O D O S   P Ú B L I C O S
		///<summary>Maneja la presentación de los datos en la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.MostrarVista()", _NAMESPACE);

				var repositorio = new BuzonChatRepositorio(udt);
				IEnumerable<BuzonChatDTO> mensajes = repositorio.ObtenerDTO(this.Vista.CasoId);

				this.Vista.Mensajes = mensajes;

				if(mensajes.Count() != 0)
				{
					foreach(var item in mensajes)
					{
						int suscriptor = (int)item.IdSuscriptorEnvio;
						bool IndLeido = item.IndLeido;
						int IdRegistro = item.Id;
						if(suscriptor != Vista.UsuarioActual.SuscriptorSeleccionado.Id && IndLeido == false)
						{
							udt.IniciarTransaccion();
							var tabBuzonChat = udt.Sesion.CreateObjectSet<BuzonChat>();
							BuzonChat BuzonChat = (from tbc in tabBuzonChat
												   where tbc.Id == IdRegistro
												   select tbc).SingleOrDefault();
							BuzonChat.IdSuscriptorRecibe = Vista.UsuarioActual.SuscriptorSeleccionado.Id;
							BuzonChat.IndLeido = true;
							BuzonChat.LeidoPor = string.Format("{0} {1}", Vista.UsuarioActual.DatosBase.Nombre1, Vista.UsuarioActual.DatosBase.Apellido1).Trim();
							BuzonChat.ModificadoPor = Vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
							BuzonChat.FechaModificacion = DateTime.Now;
							udt.MarcarModificado(BuzonChat);
							udt.Commit();
						}

					}

				}

			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}
		///<summary>Guarda los cambios en la base de datos.</summary>
		public bool GuardarCambios()
		{
			try
			{
				this.udt.IniciarTransaccion();
				var BuzonChat = new BuzonChat();
				BuzonChat.Mensaje = this.Vista.Mensaje;
				BuzonChat.IdCaso = this.Vista.CasoId;
				BuzonChat.IdMovimiento = this.Vista.MovimientoId;
				BuzonChat.IdSuscriptorEnvio = this.Vista.EnvioSuscriptorId;
				BuzonChat.Remitente = this.Vista.Remitente;
				BuzonChat.CreadoPor = this.Vista.CreacionUsuario;
				BuzonChat.FechaCreacion = DateTime.Now;
				BuzonChat.IndValido = true;
				BuzonChat.IndEliminado = false;
				this.udt.MarcarNuevo(BuzonChat);
				this.udt.Commit();

				string NombrePrograma = ObtenerNombrePrograma(Vista.CasoId);
				string IdCasoExterno2 = ObtenerIdCasoExterior(Vista.CasoId);
				string Origen = ObtenerOrigen(Vista.CasoId);

				HttpContext.Current.Trace.Warn(@"NombrePrograma: [" + NombrePrograma + @"]");
				HttpContext.Current.Trace.Warn(@"IdCasoExterno2: [" + IdCasoExterno2 + @"]");
				HttpContext.Current.Trace.Warn(@"Origen: [" + Origen + @"]");

				if((!string.IsNullOrWhiteSpace(NombrePrograma)) && (!string.IsNullOrWhiteSpace(Origen)) && (!string.IsNullOrWhiteSpace(IdCasoExterno2)))
				{
					//    //TODO: LLAMAR EL METODO DEL REFLECTION
					HttpContext.Current.Trace.Warn(@"Comienzo Ejecucion Chat Gesto");
					EjecutaReflection(NombrePrograma, Origen, int.Parse(IdCasoExterno2), Vista.Mensaje, Vista.UsuarioActual.SuscriptorSeleccionado.Id);
					HttpContext.Current.Trace.Warn(@"Fin Ejecucion Chat Gesto");
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return true;
		}

		public string ObtenerIdCasoExterior(int idCaso)
		{
			try
			{
				SolicitudRepositorio repositorio = new SolicitudRepositorio(udt);
				SolicitudDTO SolDTO = new SolicitudDTO();
				SolDTO = repositorio.ObtenerOrigen(idCaso);
				if(SolDTO != null)
					if(!string.IsNullOrWhiteSpace(SolDTO.IdCasoExterno2))
						return SolDTO.IdCasoExterno2.ToString();
					else
						return null;
				else
					return null;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return null;

		}

		public string ObtenerNombrePrograma(int idCaso)
		{
			try
			{
				FlujosServicioRepositorio repositorio = new FlujosServicioRepositorio(udt);
				FlujosServicioDTO FSDTO = new FlujosServicioDTO();
				FSDTO = repositorio.ObtenerNombrePrograma(idCaso);
				if(FSDTO != null)
					if(!string.IsNullOrWhiteSpace(FSDTO.NomPrograma))
						return FSDTO.NomPrograma.ToString();
					else
						return null;
				else
					return null;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return null;
		}

		public string ObtenerOrigen(int idCaso)
		{
			try
			{
				CasoRepositorio repositorio = new CasoRepositorio(udt);
				string IdSusIntermedarior = repositorio.ObtenerOrigen(idCaso);

				ServicioParametrizadorClient service = new ServicioParametrizadorClient();
				if(!string.IsNullOrWhiteSpace(IdSusIntermedarior))
				{
					string Origen = service.ObtenerOrigenDbPorDatoParticular(IdSusIntermedarior);
					if(!string.IsNullOrWhiteSpace(Origen))
						return Origen;
					else
						return null;
				}
				else
					return null;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return null;

		}

		private void EjecutaReflection(string NombrePrograma, string Origen, int IdCasoExterno2, string Mensaje, int Idsuscriptor)
		{
			try
			{
				Type respuestaType = Type.GetType(@"HConnexum.Tramitador.Presentacion.Rutinas.RutinasChat");
				ConstructorInfo respuestaConstructor = respuestaType.GetConstructor(Type.EmptyTypes);
				object respuestaClassObject = respuestaConstructor.Invoke(new object[] { });
				MethodInfo respuestaMethod = respuestaType.GetMethod(NombrePrograma);
				object respuestaValue = respuestaMethod.Invoke(respuestaClassObject, new object[] { Origen, IdCasoExterno2, Mensaje, Idsuscriptor });
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
				throw;
			}
		}

		/// <summary>Rutina para manejo de mensajes.</summary>
		/// <param name="pTipo">Tipo de mensaje a mostrar.</param>
		/// <returns>Contenido del mensaje.</returns>
		public string MostrarMensaje(TiposMensaje pTipo)
		{
			string result = null;
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.MostrarMensaje(pTipo = {1})", _NAMESPACE, pTipo);

				switch(pTipo)
				{
					case TiposMensaje.Error_Generico:
						result = WebConfigurationManager.AppSettings["MensajeExcepcion"];
						break;
					case TiposMensaje.Validacion_CampoRequerido:
						result = "Campo obligatorio.";
						break;
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return result;
		}
		#endregion
	}
}
