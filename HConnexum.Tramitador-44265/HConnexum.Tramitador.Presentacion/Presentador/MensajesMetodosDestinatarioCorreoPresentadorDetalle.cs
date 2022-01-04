using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase MensajesMetodosDestinatarioPresentadorDetalle.</summary>
	public class MensajesMetodosDestinatarioCorreoPresentadorDetalle : PresentadorDetalleBase<MensajesMetodosDestinatario>
	{
		///<summary>Variable vista de la interfaz IMensajesMetodosDestinatarioDetalle.</summary>
		IMensajesMetodosDestinatarioCorreoDetalle vista;
		///<summary>Variable de la entidad MensajesMetodosDestinatario.</summary>
		MensajesMetodosDestinatario _MensajesMetodosDestinatario;
		MensajesMetodosDestinatarioDTO us = new MensajesMetodosDestinatarioDTO();

		//variables globales
		string idClase;
		string idRutina;
		string idConstante;
		string idPara;
		string idCC;
		string idCCO;
		string idEmail;

		UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public MensajesMetodosDestinatarioCorreoPresentadorDetalle(IMensajesMetodosDestinatarioCorreoDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista()
		{
			try
			{
				MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario2 = new MensajesMetodosDestinatarioRepositorio(this.udt);
				us = _MensajesMetodosDestinatario2.ObtenerCorreoDTO(vista.Id, int.Parse(idEmail ?? @"0"));
				if(us != null)
				{
					PresentadorAVista();
					if(us.IndVigenteFlujoServicio == true)
						vista.ErroresCustomEditar = "El registro seleccionado no puede ser Editado debido a que el Flujo asociado está actualmente Activo";
				}
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

		///<summary>Método encargado de guardar los cambios en BD.</summary>
		///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
		public void GuardarCambios(AccionDetalle accion)
		{
			try
			{
				LlenarListaValor();
				this.udt.IniciarTransaccion();
				MensajesMetodosDestinatarioRepositorio repositorio = new MensajesMetodosDestinatarioRepositorio(udt);
				_MensajesMetodosDestinatario = new MensajesMetodosDestinatario();
				var obtenerMetodoDestinatatio = repositorio.ObtenerCLaseCorreoDTO(vista.Id, int.Parse(idClase), int.Parse(idEmail));
				IList<MensajesMetodosDestinatarioDTO> ListaConstantesEliminar = obtenerMetodoDestinatatio.ToList();
				foreach(MensajesMetodosDestinatarioDTO Eliminar in ListaConstantesEliminar)
				{
					var IdConstante = Eliminar.Id;
					repositorio.Eliminar(repositorio.ObtenerPorId(IdConstante));
				}
				///Creo las nuevas constantes
				var Texto = vista.TipoBusquedaDestinatarioPara;
				string[] TextoSplit = Texto.Split(';');
				foreach(string textos in TextoSplit)
				{
					if(textos != "")
					{
						MensajesMetodosDestinatario _MensajesMetodosDestinatarioPara = new MensajesMetodosDestinatario();
						_MensajesMetodosDestinatarioPara.IdPaso = int.Parse(vista.IdPaso);
						_MensajesMetodosDestinatarioPara.IdMensaje = int.Parse(vista.IdMensaje);
						_MensajesMetodosDestinatarioPara.IdMetodo = int.Parse(idEmail);
						_MensajesMetodosDestinatarioPara.IdTipoBusquedaDestinatario = int.Parse(idClase);
						_MensajesMetodosDestinatarioPara.ValorBusqueda = textos.ToString();
						_MensajesMetodosDestinatarioPara.FechaValidez = DateTime.Now;
						_MensajesMetodosDestinatarioPara.IndVigente = true;
						_MensajesMetodosDestinatarioPara.IndEliminado = false;
						_MensajesMetodosDestinatarioPara.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioPara.FechaCreacion = DateTime.Now;
						_MensajesMetodosDestinatarioPara.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioPara.FechaModificacion = DateTime.Now;
						_MensajesMetodosDestinatarioPara.IdTipoPrivacidad = int.Parse(idPara);
						this.udt.MarcarNuevo(_MensajesMetodosDestinatarioPara);
					}
				}
				///Creo las nuevas constantes
				var TextoCC = vista.TipoBusquedaDestinatarioCC;
				string[] TextoSplitCC = TextoCC.Split(';');
				foreach(string textos1 in TextoSplitCC)
				{
					if(textos1 != "")
					{
						MensajesMetodosDestinatario _MensajesMetodosDestinatarioCC = new MensajesMetodosDestinatario();
						_MensajesMetodosDestinatarioCC.IdPaso = int.Parse(vista.IdPaso);
						_MensajesMetodosDestinatarioCC.IdMensaje = int.Parse(vista.IdMensaje);
						_MensajesMetodosDestinatarioCC.IdMetodo = int.Parse(idEmail);
						_MensajesMetodosDestinatarioCC.IdTipoBusquedaDestinatario = int.Parse(idClase);
						_MensajesMetodosDestinatarioCC.ValorBusqueda = textos1.ToString();
						_MensajesMetodosDestinatarioCC.FechaValidez = DateTime.Now;
						_MensajesMetodosDestinatarioCC.IndVigente = true;
						_MensajesMetodosDestinatarioCC.IndEliminado = false;
						_MensajesMetodosDestinatarioCC.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCC.FechaCreacion = DateTime.Now;
						_MensajesMetodosDestinatarioCC.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCC.FechaModificacion = DateTime.Now;
						_MensajesMetodosDestinatarioCC.IdTipoPrivacidad = int.Parse(idCC);
						this.udt.MarcarNuevo(_MensajesMetodosDestinatarioCC);
					}
				}
				///Creo las nuevas constantes
				var TextoCCO = vista.TipoBusquedaDestinatarioCCO;
				string[] TextoSplitCCO = TextoCCO.Split(';');
				foreach(string textos2 in TextoSplitCCO)
				{
					if(textos2 != "")
					{
						MensajesMetodosDestinatario _MensajesMetodosDestinatarioCCO = new MensajesMetodosDestinatario();
						_MensajesMetodosDestinatarioCCO.IdPaso = int.Parse(vista.IdPaso);
						_MensajesMetodosDestinatarioCCO.IdMensaje = int.Parse(vista.IdMensaje);
						_MensajesMetodosDestinatarioCCO.IdMetodo = int.Parse(idEmail);
						_MensajesMetodosDestinatarioCCO.IdTipoBusquedaDestinatario = int.Parse(idClase);
						_MensajesMetodosDestinatarioCCO.ValorBusqueda = textos2.ToString();
						_MensajesMetodosDestinatarioCCO.FechaValidez = DateTime.Now;
						_MensajesMetodosDestinatarioCCO.IndVigente = true;
						_MensajesMetodosDestinatarioCCO.IndEliminado = false;
						_MensajesMetodosDestinatarioCCO.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCCO.FechaCreacion = DateTime.Now;
						_MensajesMetodosDestinatarioCCO.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCCO.FechaModificacion = DateTime.Now;
						_MensajesMetodosDestinatarioCCO.IdTipoPrivacidad = int.Parse(idCCO);
						this.udt.MarcarNuevo(_MensajesMetodosDestinatarioCCO);
					}
				}
				var obtenerMetodoDestinatatio2 = repositorio.ObtenerConstantesCorreoDTO(vista.Id, int.Parse(idConstante), int.Parse(idEmail));
				IList<MensajesMetodosDestinatarioDTO> ListaConstantesEliminar2 = obtenerMetodoDestinatatio2.ToList();
				foreach(MensajesMetodosDestinatarioDTO Eliminar2 in ListaConstantesEliminar2)
				{
					var IdConstante = Eliminar2.Id;
					repositorio.Eliminar(repositorio.ObtenerPorId(IdConstante));
				}
				var TextoPara1 = vista.TipoBusquedaDestinatarioPara1;
				string[] TextoSplitPara1 = TextoPara1.Split(';');
				foreach(string textos3 in TextoSplitPara1)
				{
					if(textos3 != "")
					{
						MensajesMetodosDestinatario _MensajesMetodosDestinatarioPara1 = new MensajesMetodosDestinatario();
						_MensajesMetodosDestinatarioPara1.IdPaso = int.Parse(vista.IdPaso);
						_MensajesMetodosDestinatarioPara1.IdMensaje = int.Parse(vista.IdMensaje);
						_MensajesMetodosDestinatarioPara1.IdMetodo = int.Parse(idEmail);
						_MensajesMetodosDestinatarioPara1.IdTipoBusquedaDestinatario = int.Parse(idConstante);
						_MensajesMetodosDestinatarioPara1.ValorBusqueda = textos3.ToString();
						_MensajesMetodosDestinatarioPara1.FechaValidez = DateTime.Now;
						_MensajesMetodosDestinatarioPara1.IndVigente = true;
						_MensajesMetodosDestinatarioPara1.IndEliminado = false;
						_MensajesMetodosDestinatarioPara1.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioPara1.FechaCreacion = DateTime.Now;
						_MensajesMetodosDestinatarioPara1.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioPara1.FechaModificacion = DateTime.Now;
						_MensajesMetodosDestinatarioPara1.IdTipoPrivacidad = int.Parse(idPara);
						this.udt.MarcarNuevo(_MensajesMetodosDestinatarioPara1);
					}
				}
				///Creo las nuevas constantes
				var TextoCC1 = vista.TipoBusquedaDestinatarioCC1;
				string[] TextoSplitCC1 = TextoCC1.Split(';');
				foreach(string textos4 in TextoSplitCC1)
				{
					if(textos4 != "")
					{
						MensajesMetodosDestinatario _MensajesMetodosDestinatarioCC1 = new MensajesMetodosDestinatario();
						_MensajesMetodosDestinatarioCC1.IdPaso = int.Parse(vista.IdPaso);
						_MensajesMetodosDestinatarioCC1.IdMensaje = int.Parse(vista.IdMensaje);
						_MensajesMetodosDestinatarioCC1.IdMetodo = int.Parse(idEmail);
						_MensajesMetodosDestinatarioCC1.IdTipoBusquedaDestinatario = int.Parse(idConstante);
						_MensajesMetodosDestinatarioCC1.ValorBusqueda = textos4.ToString();
						_MensajesMetodosDestinatarioCC1.FechaValidez = DateTime.Now;
						_MensajesMetodosDestinatarioCC1.IndVigente = true;
						_MensajesMetodosDestinatarioCC1.IndEliminado = false;
						_MensajesMetodosDestinatarioCC1.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCC1.FechaCreacion = DateTime.Now;
						_MensajesMetodosDestinatarioCC1.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCC1.FechaModificacion = DateTime.Now;
						_MensajesMetodosDestinatarioCC1.IdTipoPrivacidad = int.Parse(idCC);
						this.udt.MarcarNuevo(_MensajesMetodosDestinatarioCC1);
					}
				}
				///Creo las nuevas constantes
				var TextoCCO1 = vista.TipoBusquedaDestinatarioCCO1;
				string[] TextoSplitCCO1 = TextoCCO1.Split(';');
				foreach(string textos5 in TextoSplitCCO1)
				{
					if(textos5 != "")
					{
						MensajesMetodosDestinatario _MensajesMetodosDestinatarioCCO1 = new MensajesMetodosDestinatario();
						_MensajesMetodosDestinatarioCCO1.IdPaso = int.Parse(vista.IdPaso);
						_MensajesMetodosDestinatarioCCO1.IdMensaje = int.Parse(vista.IdMensaje);
						_MensajesMetodosDestinatarioCCO1.IdMetodo = int.Parse(idEmail);
						_MensajesMetodosDestinatarioCCO1.IdTipoBusquedaDestinatario = int.Parse(idConstante);
						_MensajesMetodosDestinatarioCCO1.ValorBusqueda = textos5.ToString();
						_MensajesMetodosDestinatarioCCO1.FechaValidez = DateTime.Now;
						_MensajesMetodosDestinatarioCCO1.IndVigente = true;
						_MensajesMetodosDestinatarioCCO1.IndEliminado = false;
						_MensajesMetodosDestinatarioCCO1.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCCO1.FechaCreacion = DateTime.Now;
						_MensajesMetodosDestinatarioCCO1.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						_MensajesMetodosDestinatarioCCO1.FechaModificacion = DateTime.Now;
						_MensajesMetodosDestinatarioCCO1.IdTipoPrivacidad = int.Parse(idCCO);
						this.udt.MarcarNuevo(_MensajesMetodosDestinatarioCCO1);
					}
				}
				udt.Commit();
				this.udt.IniciarTransaccion();
				MensajesMetodosDestinatarioDTO us = new MensajesMetodosDestinatarioDTO();
				MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario2 = new MensajesMetodosDestinatarioRepositorio(this.udt);
				us = _MensajesMetodosDestinatario2.ObtenerRutinaCorreoDTO(int.Parse(vista.IdPaso), int.Parse(idEmail), int.Parse(idRutina));
				MensajesMetodosDestinatario usDTO = new MensajesMetodosDestinatario();
				if(us != null)
				{
					usDTO.Id = us.Id;
					usDTO.IdPaso = us.IdPaso;
					usDTO.IdMensaje = int.Parse(vista.IdMensaje);
					usDTO.IdMetodo = int.Parse(idEmail);
					usDTO.IdTipoBusquedaDestinatario = int.Parse(idRutina);
					usDTO.ValorBusqueda = vista.Rutina;
					usDTO.FechaValidez = us.FechaValidez;
					usDTO.IndVigente = us.IndVigente;
					usDTO.IndEliminado = us.IndEliminado;
					usDTO.CreadoPor = us.CreadoPor;
					usDTO.FechaCreacion = us.FechaCreacion;
					usDTO.ModificadoPor = us.ModificadoPor;
					usDTO.FechaModificacion = us.FechaModificacion;
					usDTO.IdTipoPrivacidad = null;
					this.udt.MarcarModificado(usDTO);
				}
				else
				{
					if(vista.Rutina != "")
					{
						usDTO.IdPaso = int.Parse(vista.IdPaso);
						usDTO.IdMensaje = int.Parse(vista.IdMensaje);
						usDTO.IdMetodo = int.Parse(idEmail);
						usDTO.IdTipoBusquedaDestinatario = int.Parse(idRutina);
						usDTO.ValorBusqueda = vista.Rutina;
						usDTO.FechaValidez = DateTime.Now;
						usDTO.IndVigente = true;
						usDTO.IndEliminado = false;
						usDTO.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						usDTO.FechaCreacion = DateTime.Now;
						usDTO.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
						usDTO.FechaModificacion = DateTime.Now;
						usDTO.IdTipoPrivacidad = null;
						this.udt.MarcarNuevo(usDTO);
					}
				}
				udt.Commit();
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

		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{
				vista.IdPaso = string.Format("{0:N0}", us.IdPaso);
				vista.IdMensaje = string.Format("{0:N0}", us.IdMensaje);
				vista.FechaModificacion = us.FechaModificacion.ToString();
				CargarPublicacion();
				CargarAuditoria();
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

		///<summary>Método encargado de asignar valores a propiedades de la entidad desde la vista.</summary>	
		///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
		private void VistaAPresentador(AccionDetalle accion)
		{
			try
			{
				_MensajesMetodosDestinatario.IdPaso = int.Parse(vista.IdPaso);
				_MensajesMetodosDestinatario.IdMensaje = int.Parse(vista.IdMensaje);
				_MensajesMetodosDestinatario.IdTipoBusquedaDestinatario = int.Parse(vista.TipoBusquedaDestinatario);
				_MensajesMetodosDestinatario.ValorBusqueda = vista.ValorBusqueda;
				_MensajesMetodosDestinatario.IdTipoBusquedaDestinatario = int.Parse(vista.TipoPrivacidad);
				AsignarAuditoria(accion);
				AsignarPublicacion();
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

		///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
		/// <returns>Devuelve mensaje(s) con los datos validados.</returns>
		protected string ValidarDatos()
		{
			StringBuilder errores = new StringBuilder();
			try
			{
				Metadata<MensajesMetodosDestinatario> metadata = new Metadata<MensajesMetodosDestinatario>();
				errores.AppendWithBreak(metadata.ValidarPropiedad("Id", vista.Id));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IdPaso", vista.IdPaso));
				errores.AppendWithBreak(metadata.ValidarPropiedad("IdMensaje", vista.IdMensaje));
				errores.AppendWithBreak(metadata.ValidarPropiedad("TipoBusquedaDestinatario", vista.TipoBusquedaDestinatario));
				errores.AppendWithBreak(metadata.ValidarPropiedad("ValorBusqueda", vista.ValorBusqueda));
				errores.AppendWithBreak(metadata.ValidarPropiedad("TipoPrivacidad", vista.TipoPrivacidad));
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return errores.ToString();
		}

		/// <summary>
		/// Metodo encargado de Llenar la lista.
		/// </summary>
		public void LlenarListBox()
		{
			try
			{
				DataTable ListBoxCasosMovimientos = new DataTable();
				ListBoxCasosMovimientos.Columns.Add("Id");
				ListBoxCasosMovimientos.Columns.Add("Nombre");
				int i = 0;
				foreach(ObjetoCaso OC in Enum.GetValues(typeof(ObjetoCaso)))
				{
					DataRow row = ListBoxCasosMovimientos.NewRow();
					row["Id"] = i;
					row["Nombre"] = "Caso." + OC.ToString();
					ListBoxCasosMovimientos.Rows.Add(row);
					i++;
				}
				foreach(ObjetoMovimiento OM in Enum.GetValues(typeof(ObjetoMovimiento)))
				{
					DataRow row = ListBoxCasosMovimientos.NewRow();
					row["Id"] = i;
					row["Nombre"] = "Movimiento." + OM.ToString();
					ListBoxCasosMovimientos.Rows.Add(row);
					i++;
				}
				vista.ListBoxCasosMovimientos = ListBoxCasosMovimientos;
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

		public void LlenarConstantes()
		{
			try
			{
				IList<MensajesMetodosDestinatarioDTO> ListaClasePara;
				MensajesMetodosDestinatario us = new MensajesMetodosDestinatario();
				MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario = new MensajesMetodosDestinatarioRepositorio(this.udt);
				var obtenerMetodoDestinatatio = _MensajesMetodosDestinatario.ObtenerClasePara2DTO(vista.Id, int.Parse(idConstante), int.Parse(idPara), int.Parse(idEmail ?? @"0"));
				ListaClasePara = obtenerMetodoDestinatatio.ToList();
				if(ListaClasePara != null)
				{
					foreach(MensajesMetodosDestinatarioDTO ClasePara in ListaClasePara)
					{
						var textoAinsertar = ClasePara.ValorBusqueda + "; ";
						vista.TipoBusquedaDestinatarioPara1 = vista.TipoBusquedaDestinatarioPara1 + textoAinsertar;
					}
				}
				else
					vista.TipoBusquedaDestinatarioPara = "";
				IList<MensajesMetodosDestinatarioDTO> ListaClaseCC;
				var obtenerMetodoDestinatatio1 = _MensajesMetodosDestinatario.ObtenerClaseCC2DTO(vista.Id, int.Parse(idConstante), int.Parse(idCC), int.Parse(idEmail ?? @"0"));
				ListaClaseCC = obtenerMetodoDestinatatio1.ToList();
				if(ListaClaseCC != null)
					foreach(MensajesMetodosDestinatarioDTO ClaseCC in ListaClaseCC)
					{
						var textoAinsertar = ClaseCC.ValorBusqueda + "; ";
						vista.TipoBusquedaDestinatarioCC1 = vista.TipoBusquedaDestinatarioCC1 + textoAinsertar;
					}
				else
					vista.TipoBusquedaDestinatarioCC = "";
				IList<MensajesMetodosDestinatarioDTO> ListaClaseCCO;
				var obtenerMetodoDestinatatio2 = _MensajesMetodosDestinatario.ObtenerClaseCCO2DTO(vista.Id, int.Parse(idConstante), int.Parse(idCCO), int.Parse(idEmail ?? @"0"));
				ListaClaseCCO = obtenerMetodoDestinatatio2.ToList();
				if(ListaClaseCCO != null)
					foreach(MensajesMetodosDestinatarioDTO ClaseCCO in ListaClaseCCO)
					{
						var textoAinsertar = ClaseCCO.ValorBusqueda + "; ";
						vista.TipoBusquedaDestinatarioCCO1 = vista.TipoBusquedaDestinatarioCCO1 + textoAinsertar;
					}
				else
					vista.TipoBusquedaDestinatarioCCO = "";
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

		public void LlenarClase()
		{
			try
			{
				IList<MensajesMetodosDestinatarioDTO> ListaClasePara;
				MensajesMetodosDestinatario us = new MensajesMetodosDestinatario();
				MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario = new MensajesMetodosDestinatarioRepositorio(this.udt);
				var obtenerMetodoDestinatatio = _MensajesMetodosDestinatario.ObtenerClaseParaDTO(vista.Id, int.Parse(idClase), int.Parse(idPara), int.Parse(idEmail ?? @"0"));
				ListaClasePara = obtenerMetodoDestinatatio.ToList();
				if(ListaClasePara != null)
				{
					foreach(MensajesMetodosDestinatarioDTO ClasePara in ListaClasePara)
					{
						var textoAinsertar = ClasePara.ValorBusqueda + "; ";
						vista.TipoBusquedaDestinatarioPara = vista.TipoBusquedaDestinatarioPara + textoAinsertar;
					}
				}
				else
					vista.TipoBusquedaDestinatarioPara = "";
				IList<MensajesMetodosDestinatarioDTO> ListaClaseCC;
				var obtenerMetodoDestinatatio1 = _MensajesMetodosDestinatario.ObtenerClaseCCDTO(vista.Id, int.Parse(idClase), int.Parse(idCC), int.Parse(idEmail ?? @"0"));
				ListaClaseCC = obtenerMetodoDestinatatio1.ToList();
				if(ListaClaseCC != null)
					foreach(MensajesMetodosDestinatarioDTO ClaseCC in ListaClaseCC)
					{
						var textoAinsertar = ClaseCC.ValorBusqueda + "; ";
						vista.TipoBusquedaDestinatarioCC = vista.TipoBusquedaDestinatarioCC + textoAinsertar;
					}
				else
					vista.TipoBusquedaDestinatarioCC = "";
				IList<MensajesMetodosDestinatarioDTO> ListaClaseCCO;
				var obtenerMetodoDestinatatio2 = _MensajesMetodosDestinatario.ObtenerClaseCCODTO(vista.Id, int.Parse(idClase), int.Parse(idCCO), int.Parse(idEmail ?? @"0"));
				ListaClaseCCO = obtenerMetodoDestinatatio2.ToList();
				if(ListaClaseCCO != null)
					foreach(MensajesMetodosDestinatarioDTO ClaseCCO in ListaClaseCCO)
					{
						var textoAinsertar = ClaseCCO.ValorBusqueda + "; ";
						vista.TipoBusquedaDestinatarioCCO = vista.TipoBusquedaDestinatarioCCO + textoAinsertar;
					}
				else
					vista.TipoBusquedaDestinatarioCCO = "";
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

		/// <summary>
		/// Metodo encargado de burcar la Rutina de un paso en caso tal que la posea.
		/// </summary>
		/// <returns></returns>
		public string BuscarRutina()
		{
			MensajesMetodosDestinatario us = new MensajesMetodosDestinatario();
			MensajesMetodosDestinatarioRepositorio _MensajesMetodosDestinatario = new MensajesMetodosDestinatarioRepositorio(this.udt);
			if(idEmail == null || idRutina == null)
				LlenarListaValor();
			var obtenerMetodoDestinatatio = _MensajesMetodosDestinatario.ObtenerRutinaCorreoDTO(vista.Id, int.Parse(idEmail ?? @"0"), int.Parse(idRutina));
			if(obtenerMetodoDestinatatio != null)
				return vista.Rutina = obtenerMetodoDestinatatio.ValorBusqueda;
			else
				return vista.Rutina = "";
		}

		///<summary>Rutina para el llenado de Los valores de las listas valores</summary>
		public void LlenarListaValor()
		{
			try
			{
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				DataTable listadoIdLstaValor = null;
				DataTable listadoIdLstaValor2 = null;
				DataTable listadoIdMetodoEnvio = null;
				try
				{
					DataSet ds = servicio.ObtenerListaValorPorNombre("TipoBusquedaDestinatario");
					if(ds.Tables[@"Error"] != null)
						throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if(ds.Tables[0].Rows.Count > 0)
						listadoIdLstaValor = ds.Tables[0];
					int i = 0;
					foreach(DataRow row in listadoIdLstaValor.Rows)
					{
						var NombreValorCorto = row["NombreValor"].ToString();
						if(NombreValorCorto == "Clase")
							idClase = row["Id"].ToString();
						if(NombreValorCorto == "Rutina")
							idRutina = row["Id"].ToString();
						if(NombreValorCorto == "Constante")
							idConstante = row["Id"].ToString();
						i++;
					}
					DataSet ds2 = servicio.ObtenerListaValorPorNombre("TipoPrivacidad");
					if(ds2.Tables[@"Error"] != null)
						throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if(ds2.Tables[0].Rows.Count > 0)
						listadoIdLstaValor2 = ds2.Tables[0];
					int A = 0;
					foreach(DataRow row2 in listadoIdLstaValor2.Rows)
					{
						var NombreValorCorto2 = row2["NombreValorCorto"].ToString();
						if(NombreValorCorto2 == "Para")
							idPara = row2["Id"].ToString();
						if(NombreValorCorto2 == "CC")
							idCC = row2["Id"].ToString();
						if(NombreValorCorto2 == "CCO")
							idCCO = row2["Id"].ToString();
						A++;
					}
					DataSet ds3 = servicio.ObtenerMetodosEnvioporAlerta();
					if(ds3 != null && ds3.Tables.Count != 0)
					{
						if(ds3.Tables[@"Error"] != null)
							throw new Exception(ds3.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
						if(ds3.Tables[0].Rows.Count > 0)
							listadoIdMetodoEnvio = ds3.Tables[0];
						if(listadoIdMetodoEnvio != null)
							foreach(DataRow rowMetodo in listadoIdMetodoEnvio.Rows)
							{
								var nombre = rowMetodo["Nombre"].ToString();
								if(nombre == EnvioEmail)
									idEmail = rowMetodo["Id"].ToString();
							}
					}
					else
						vista.ErroresCustomEditar = "No se han configurado métodos de envío";
				}
				catch(Exception ex)
				{
					Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
					if(ex.InnerException != null)
						HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
					HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
					this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				}
				finally
				{
					if(servicio.State != CommunicationState.Closed)
						servicio.Close();
				}
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

		/// <summary>
		/// Nombre que tiene el envio Email en la tabla MetodosEnvioporAlerta
		/// esto es necesario para obtener su ID
		/// </summary>
		public string EnvioEmail
		{
			get
			{
				return @"EMAIL";
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombos()
		{
			try
			{
				PasoRepositorio repositorioPaso = new PasoRepositorio(udt);
				vista.ComboIdPaso = repositorioPaso.ObtenerPasoporIdDTO(vista.Id);
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				DataTable listadoIdMensaje = null;
				try
				{
					DataSet ds = servicio.ObtenerMensaje();
					if(ds.Tables[@"Error"] != null)
						throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
					if(ds.Tables[0].Rows.Count > 0)
						listadoIdMensaje = ds.Tables[0];
				}
				catch(Exception ex)
				{
					Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
					if(ex.InnerException != null)
						HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
					HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
					this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
				}
				finally
				{
					if(servicio.State != CommunicationState.Closed)
						servicio.Close();
				}
				vista.ComboIdMensaje = listadoIdMensaje;
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

		///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
		private void CargarPublicacion()
		{
			try
			{
				vista.IndVigente = us.IndVigente.ToString();
				vista.FechaValidez = us.FechaValidez.ToString();
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

		///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
		private void CargarAuditoria()
		{
			try
			{
				vista.CreadoPor = this.ObtenerNombreUsuario(us.CreadoPor);
				vista.FechaCreacion = us.FechaCreacion.ToString();
				vista.ModificadoPor = this.ObtenerNombreUsuario(us.ModificadoPor);
				vista.IndEliminado = us.IndEliminado.ToString();
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

		///<summary>Método encargado de asignar valores de publicación a la entidad.</summary>
		public void AsignarPublicacion()
		{
			try
			{
				if(string.IsNullOrEmpty(vista.FechaValidez))
					_MensajesMetodosDestinatario.FechaValidez = null;
				else
					_MensajesMetodosDestinatario.FechaValidez = DateTime.Parse(vista.FechaValidez);
				_MensajesMetodosDestinatario.IndVigente = bool.Parse(vista.IndVigente);
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

		///<summary>Método encargado de asignar valores de auditoria a la entidad.</summary>
		///<param name="accion">Variable Enumerativa que indica la acción a ejecutar sobre la BD.</param>
		private void AsignarAuditoria(AccionDetalle accion)
		{
			try
			{
				if(accion == AccionDetalle.Agregar)
				{
					_MensajesMetodosDestinatario.IndEliminado = false;
					_MensajesMetodosDestinatario.CreadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_MensajesMetodosDestinatario.FechaCreacion = DateTime.Now;
					_MensajesMetodosDestinatario.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_MensajesMetodosDestinatario.FechaModificacion = DateTime.Now;
				}
				else if(accion == AccionDetalle.Modificar)
				{
					_MensajesMetodosDestinatario.ModificadoPor = this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					_MensajesMetodosDestinatario.FechaModificacion = DateTime.Now;
				}
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