using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Configuration;

///<summary>Namespace que engloba el presentador Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase CasoPresentadorLista.</summary>
	public class GestionSupervisadosPresentadorLista : PresentadorListaBase<Caso>
	{
		///<summary>Variable vista de la interfaz ICasoLista.</summary>
		readonly IGestionSupervisadosLista vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		Caso caso;
		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public GestionSupervisadosPresentadorLista(IGestionSupervisadosLista vista)
		{
			this.vista = vista;
		}

		public GestionSupervisadosPresentadorLista()
		{
		}
        public bool CargarRoles(int pIdAplicacion)
        {
            ServicioAutenticadorClient servicio = new ServicioAutenticadorClient();
            try
            {
                HttpContext.Current.Trace.Warn(@"IdUsuarioSuscriptorSeleccionado: [" + vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado + @"], IdAplicacion: [" + pIdAplicacion.ToString() + "]");
                DataSet ds = servicio.ObtenerRolesUsuarioPorAplicacion(vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar(), pIdAplicacion.ToString().Encriptar());
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                HttpContext.Current.Trace.Warn(@"Cantidad de Roles: [" + ds.Tables[0].Rows.Count + @"]");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    vista.UsuarioActual.ActualizarRolesPorAplicacion(pIdAplicacion, ds);
                    return true;
                }
                return false;
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
		public void GuardarCambiosModificacion(int IdUsuario, bool status, int idUsuarioActual)
		{
			if(IdUsuario != null && status != null && idUsuarioActual != null)
			{
				ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
				try
				{
					bool Respuesta = servicio.ActualizaUsuario(IdUsuario, idUsuarioActual, status);
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
				Filtro tes = new Filtro();
				if(vista.IdSuscriptor != "")
				{
					tes.Campo = "Idsuscriptor";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(int);
					tes.Valor = int.Parse(vista.IdSuscriptor);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdUsuarioSupervisado != "")
				{
					tes.Campo = "IdUsuario";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(int);
					tes.Valor = int.Parse(vista.IdUsuarioSupervisado);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdCargo != "")
				{
					tes.Campo = "CargosSuscriptor";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(int);
					tes.Valor = int.Parse(vista.IdCargo);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdHabilidad != "")
				{
					tes.Campo = "HabilidadSuscriptor";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(int);
					tes.Valor = int.Parse(vista.IdHabilidad);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdAutonomia != "")
				{
					tes.Campo = "AutonomiaSuscriptor";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(int);
					tes.Valor = int.Parse(vista.IdAutonomia);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdServicio != "")
				{
					tes.Campo = "IdServicio";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(int);
					tes.Valor = int.Parse(vista.IdServicio);
					parametrosFiltro.Add(tes);
				}
				if(vista.IdEstatusSupervisados != "")
				{
					if(vista.IdEstatusSupervisados == "24")
					{
						tes.Campo = "UsuarioVigente";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(bool);
						tes.Valor = true;
						parametrosFiltro.Add(tes);
					}
					else
					{
						tes.Campo = "UsuarioVigente";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(bool);
						tes.Valor = false;
						parametrosFiltro.Add(tes);
					}
				}
				CasoRepositorio repositorio = new CasoRepositorio(this.unidadDeTrabajo);
				IEnumerable<CasoDTO> datos = repositorio.ObtenerGridUsuarioDTO(orden, pagina, tamañoPagina, parametrosFiltro, vista.IdSupervisado);
				this.vista.NumeroDeRegistros = repositorio.Conteo;
				this.vista.Datos = datos;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombos(int IdUsuarioSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerSuscriptores();
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					vista.ComboIdSuscriptor = ds.Tables[0];

				DataSet ds3 = servicio.ObtenerListaValorPorNombre("EstatusSuscriptor");
				if(ds3.Tables[@"Error"] != null)
					throw new Exception(ds3.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds3.Tables[0].Rows.Count > 0)
				{
					vista.ComboIdEstatusSupervisados = ds3.Tables[0];
				}

				DataSet ds2 = servicio.ObtenerSupervisados(IdUsuarioSuscriptor);
				if(ds2.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds2.Tables[0].Rows.Count > 0)
				{
					vista.ComboIdUsuarioSupervisado = ds2.Tables[0];
					DataTable test = ds2.Tables[0];
					int i = test.Rows.Count + 1;
					int[] Array = new int[i];
					Array[0] = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					int y = 1;

					foreach(DataRow row in test.Rows)
					{
						Array[y] = int.Parse(row.ItemArray[0].ToString());
						y++;
					}
					vista.IdSupervisado = Array;
				}
				else
				{
					int i = 1;
					int[] Array = new int[i];
					Array[0] = vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					vista.IdSupervisado = Array;
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
			finally
			{
				if(servicio.State != CommunicationState.Closed)
					servicio.Close();
			}
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarCombosServicios(int IdSuscriptor)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				ServiciosSuscriptorRepositorio repositorio = new ServiciosSuscriptorRepositorio(udt);
				vista.ComboIdServicio = repositorio.ObtenerDTO(IdSuscriptor);

				DataSet ds2 = servicio.ObtenerCargosSuscriptorporId(IdSuscriptor);
				if(ds2.Tables[@"Error"] != null)
					throw new Exception(ds2.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds2.Tables[0].Rows.Count > 0)
					vista.ComboIdCargo = ds2.Tables[0];

				DataSet ds3 = servicio.ObtenerHabilidadesPorId(IdSuscriptor);
				if(ds3.Tables[@"Error"] != null)
					throw new Exception(ds3.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds3.Tables[0].Rows.Count > 0)
					vista.ComboHabilidad = ds3.Tables[0];

				DataSet ds4 = servicio.ObtenerAutonomiasSuscriptorPorId(IdSuscriptor);
				if(ds4.Tables[@"Error"] != null)
					throw new Exception(ds3.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds4.Tables[0].Rows.Count > 0)
					vista.ComboAutonomia = ds4.Tables[0];
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

		public DataTable LlenarCombosGrid()
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			try
			{
				DataSet ds = servicio.ObtenerListaValorPorNombre("EstatusSuscriptor");
				if(ds.Tables[@"Error"] != null)
					throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
				if(ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0];
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
			return null;
		}

		///<summary>Método encargado de eliminar registros del conjunto.</summary>
		///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
		public void Eliminar(IList<string> ids)
		{
			try
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				CasoRepositorio repositorio = new CasoRepositorio(this.unidadDeTrabajo);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					Caso _Caso = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.EliminarLogico(_Caso, _Caso.Id))
						this.vista.Errores = "Ya Existen registros asociados a la entidad Caso";
				}
				this.unidadDeTrabajo.Commit();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
		public void CargarUsuarioLog()
		{
			try
			{
				UsuarioRepositorio repositorio = new UsuarioRepositorio(this.unidadDeTrabajo);
				UsuarioDTO usuario = repositorio.ObtenerUsuarioPorIdUsuarioSuscriptor(vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado);
				if(usuario != null)
					vista.UsuarioLog = usuario.Nombre1 + " " + usuario.Apellido1;

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