using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.ServiceModel;
using System.Data;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using System.Diagnostics;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	///<summary>Presentador para el manejo del control Web de usuario 'CambioMedico'.</summary>
	public class CambioMedicoPresentador : BloquesPresentadorBase
	{
		#region M I E M B R O S   P R I V A D O S
		private string configClaveExcepcionMensaje = "MensajeExcepcion";
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Vista asociada al presentador.</summary>
		readonly ICambioMedico Control;
		#endregion

		///<summary>Constructor del presentador.</summary>
		///<param name="vista">Vista a asociar con el presentador.</param>
		public CambioMedicoPresentador(ICambioMedico control)
		{
			this.Control = control;
		}

		#region E V E N T O S
		///<summary>Método encargado de eliminar registros del conjunto.</summary>
		///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
		public void Eliminar(IList<string> ids)
		{
			try
			{
				udt.IniciarTransaccion();
				var repositorio = new SuscriptorRepositorio(udt);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					Suscriptor elemento = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.EliminarLogico(elemento, elemento.Id))
						this.Control.Errores = "Ya existen registros asociados a este elemento.";
				}
				udt.Commit();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				this.Control.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
		}

		public void activarEliminado(IList<string> ids)
		{
			try
			{
				this.udt.IniciarTransaccion();
				var repositorio = new SuscriptorRepositorio(this.udt);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					Suscriptor elemento = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.activarEliminarLogico(elemento, elemento.Id))
						this.Control.Errores = "Ya existen registros asociados a este elemento.";
				}
				this.udt.Commit();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				this.Control.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
		}

		public bool bObtenerRolIndEliminado()
		{
			var repositorio = new SuscriptorRepositorio(this.udt);
			return repositorio.obtenerRolIndEliminado();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro)
		{
			try
			{
				string Persona = Control.BuscaPersona;
				string IdRed = Control.IdRed;
				string configServicioOpinionMedica =
					string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[@"ListaConfigServicioOpinionMedica"])
					? "Configuración Opinión Médica"
					: ConfigurationManager.AppSettings[@"ListaConfigServicioOpinionMedica"];
				var repositorio = new ListasValorRepositorio(this.unidadDeTrabajo);
				var listaValor = new ListasValorDTO();
				listaValor = repositorio.ObtenerListaValoresDTO(configServicioOpinionMedica, "TipSuscrip");
				string suscriptorTipo = listaValor.Valor;
				if(!string.IsNullOrWhiteSpace(suscriptorTipo))
				{
					IEnumerable<SuscriptorDTO> datos =
						ObtenerSuscriptoresSubordinados(this.Control.UsuarioActual.SuscriptorSeleccionado.Id
						, suscriptorTipo, orden, pagina, tamañoPagina, parametrosFiltro, Persona, IdRed);
				}
				else
				{
					throw new CustomException(
						string.Format("El valor del item 'TipoSuscriptor' de la lista '{0}' es nulo o vacío."
						, configServicioOpinionMedica));
				}
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				this.Control.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
			}
		}

		public void LlenarCombo()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerRedesDTO();
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					Control.ComboIdRed = ds.Tables[0];
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				if(ex.InnerException != null)
					HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		public IEnumerable<SuscriptorDTO> ObtenerSuscriptoresSubordinados(int suscriptorId, string suscriptorSubordinadoTipo, string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, string Persona, string IdRed)
		{
			IQueryable<SuscriptorDTO> consulta;
			var servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerSuscriptorRedSuscriptorPorTipo(suscriptorId, suscriptorSubordinadoTipo, pagina, registros, Persona, IdRed);
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				DataTable dt = ds.Tables[0];
				if(dt != null && dt.Rows.Count > 0)
				{
					var list = new List<SuscriptorDTO>();
					for(int i = 0; i < dt.Rows.Count; i++)
						list.Add(new SuscriptorDTO()
						{
							Id = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["subordinadoId"].ToString()) ? "-1" : dt.Rows[i]["subordinadoId"].ToString()),
							TipoDetalleId = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["subordinadoTipoDetalleId"].ToString()) ? "-1" : dt.Rows[i]["subordinadoTipoDetalleId"].ToString()),
							IndTipDoc = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["IndTipDoc"].ToString()) ? "-1" : dt.Rows[i]["IndTipDoc"].ToString()),
							DocumentoTipo = dt.Rows[i]["documentoTipo"].ToString(),
							NumDoc = dt.Rows[i]["NumDoc"].ToString(),
							Nombre = dt.Rows[i]["subordinado"].ToString(),
							Redes = dt.Rows[i]["Redes"].ToString(),
							RazonSocial = dt.Rows[i]["RazonSocial"].ToString(),
							FechaInactivacion = String.IsNullOrWhiteSpace(dt.Rows[i]["FechaInactivacion"].ToString()) ? (DateTime?)null : (DateTime?)DateTime.Parse(dt.Rows[i]["FechaInactivacion"].ToString()),
							FechaNacimiento = DateTime.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["FechaNacimiento"].ToString()) ? "01/01/1900" : dt.Rows[i]["FechaNacimiento"].ToString()),
							Direccion = dt.Rows[i]["Direccion"].ToString(),
							EstatusId = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["Estatus"].ToString()) ? "-1" : dt.Rows[i]["Estatus"].ToString()),
							Estatus = dt.Rows[i]["estatusNombre"].ToString(),
							CodigoExternoId = dt.Rows[i]["CodIdExterno"].ToString(),
							IdTipo = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["subordinadoTipoId"].ToString()) ? "-1" : dt.Rows[i]["subordinadoTipoId"].ToString()),
							Tipo = dt.Rows[i]["subordinadoTipo"].ToString(),
							IdPais = int.Parse(dt.Rows[i]["IdPais"].ToString()),
							Pais = dt.Rows[i]["pais"].ToString(),
							DivisionTerritorial1Id = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["IdDivisionTerritorial1"].ToString()) ? "-1" : dt.Rows[i]["IdDivisionTerritorial1"].ToString()),
							DivisionTerritorial1 = dt.Rows[i]["divisionTerritorial1"].ToString(),
							DivisionTerritorial2Id = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["IdDivisionTerritorial2"].ToString()) ? "-1" : dt.Rows[i]["IdDivisionTerritorial2"].ToString()),
							DivisionTerritorial2 = dt.Rows[i]["divisionTerritorial2"].ToString(),
							DivisionTerritorial3Id = int.Parse(String.IsNullOrWhiteSpace(dt.Rows[i]["IdDivisionTerritorial3"].ToString()) ? "-1" : dt.Rows[i]["IdDivisionTerritorial3"].ToString()),
							DivisionTerritorial3 = dt.Rows[i]["divisionTerritorial3"].ToString(),
							CasosPendientesCantidad = Suscriptor_ObtenerCantidadMovimientos(int.Parse(
							String.IsNullOrWhiteSpace(dt.Rows[i]["subordinadoId"].ToString()) ? "-1" : dt.Rows[i]["subordinadoId"].ToString()), "Registro de Opinión Médica"
							, "PENDIENTE,EN PROCESO".Split(',').ToList<string>(), "PENDIENTE,EN PROCESO".Split(',').ToList<string>())
						});
					consulta = list.AsQueryable();

					DataTable dt1 = ds.Tables[1]; // contiene la cantidad de registros que se consiguieron
					this.Control.NumeroDeRegistros = int.Parse(dt1.Rows[0]["Cantidad_Reg"].ToString());
					this.Control.Datos = consulta;
					return consulta;
				}
				else
				{
					this.Control.NumeroDeRegistros = 0;
					IList<SuscriptorDTO> sustest = new List<SuscriptorDTO>();
					this.Control.Datos = sustest;
					return sustest;
				}
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				this.Control.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
				Debug.WriteLine(ex.ToString());
			}
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
			return null;
		}

		/// <summary>
		/// Devuelve la cantidad de movimientos de un tipo o tipos determinados, de un suscriptor en específico.
		/// </summary>
		/// <param name="suscriptorId">Identificador del suscriptor a consultar.</param>
		/// <param name="movimiento">Nombre del movimiento a consultar.</param>
		/// <param name="movimientoTipos">Tipo(s) de movimiento(s) a consultar.</param>
		/// <param name="casoTipos">Lista de tipos de caso a consultar.</param>
		/// <returns>Cantidad de movimientos.</returns>
		public int Suscriptor_ObtenerCantidadMovimientos(int suscriptorId, string movimiento, List<string> movimientoTipos, List<string> casoTipos)
		{
			const int NULL_INT = -1;
			try
			{
				var dtoMovimiento = new MovimientoRepositorio(udt);
				List<MovimientoDTO> list = dtoMovimiento.ObtenerPorSuscriptor(suscriptorId, movimiento, movimientoTipos, casoTipos);
				return list.GroupBy(s => s.Idsuscriptor).Select(c => c.Count()).FirstOrDefault();
			}
			catch(Exception ex)
			{
				Errores.ManejarError(ex, ex.ToString());
				this.Control.Errores = WebConfigurationManager.AppSettings[configClaveExcepcionMensaje];
				Debug.WriteLine(ex.ToString());
			}
			return NULL_INT;
		}
		#endregion
	}
}