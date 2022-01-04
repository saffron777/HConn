using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.ServiceModel;
using System.Configuration;
using System.Xml.Linq;
using HConnexum.Seguridad;



///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase ConsultaCasosPresentador.</summary>
	public class ConsultaCasosAuditoriaPresentador : PresentadorListaBase<Paso>
	{
		///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
		readonly IConsultaCasosAuditoria vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		//int idSuscriptor = 0;
		//int IdServiciosuscriptor = 0;
		int? idcaso = null;
		int? idsolicitud = null;
		//int idcaso = 0;
		//int idsolicitud = 0;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public ConsultaCasosAuditoriaPresentador(IConsultaCasosAuditoria vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int idSuscriptor, int idServiciosuscriptor)
		{
			try
			{
				Filtro tes = new Filtro();
				string filtrosBusqueda = "";
				DateTime fechaDesde;
				DateTime fechaHasta;
				if (vista.Filtro.Trim() != "" &&  vista.TipoFiltro.Trim() != "")
				{
					#region Filtros
					
					if (vista.TipoFiltro == "Id")
					{
						tes.Campo = "Id";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(int);
						tes.Valor = int.Parse(vista.Filtro);
						parametrosFiltro.Add(tes);
					}
					else if (vista.TipoFiltro == "IdSolicitud")
					{
						tes.Campo = "IdSolicitud";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(int);
						tes.Valor = int.Parse(vista.Filtro);
						parametrosFiltro.Add(tes);
					}
					
					#endregion
				}

				if (vista.FechaDesde.Trim() != "" && vista.FechaHasta != "")
				{
					fechaDesde = DateTime.Parse(vista.FechaDesde);
					fechaHasta = DateTime.Parse(vista.FechaHasta);

					filtrosBusqueda = filtrosBusqueda + " and  FechaCreacion BETWEEN '" + String.Format("{0:dd/MM/yyyy}", fechaDesde) + "' AND '" + String.Format("{0:dd/MM/yyyy}", fechaHasta) + "'";
				}
				if (vista.FechaDesde.Trim() != "")
				{
					fechaDesde = DateTime.Parse(vista.FechaDesde);
					tes.Campo = "FechaCreacion";
					tes.Operador = "GreaterThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(vista.FechaDesde);
					parametrosFiltro.Add(tes);
					filtrosBusqueda = filtrosBusqueda + " and FechaCreacion = '" + String.Format("{0:dd/MM/yyyy}", fechaDesde) + "'";
				}
				if (vista.FechaHasta != "")
				{
					fechaHasta = DateTime.Parse(vista.FechaHasta);
					tes.Campo = "FechaCreacion";
					tes.Operador = "LessThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(vista.FechaHasta).AddMinutes(1439);
					parametrosFiltro.Add(tes);
					if (DateTime.Now.ToShortDateString() != vista.FechaHasta)
						filtrosBusqueda = filtrosBusqueda + " and  FechaCreacion BETWEEN '" + String.Format("{0:dd/MM/yyyy}", DateTime.Now) + "' AND '" + String.Format("{0:dd/MM/yyyy}", fechaHasta) + "'";
				}


				CasoRepositorio repositorioCasos = new CasoRepositorio(unidadDeTrabajo);
				IEnumerable<CasoDTO> datos = repositorioCasos.ObtenerCasosConMovEnAuditoria(orden, pagina, tamañoPagina, parametrosFiltro, idSuscriptor, idServiciosuscriptor);
				this.vista.NumeroDeRegistros = repositorioCasos.Conteo;
				this.vista.Datos = datos;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
		
		public void BuscarTodosSuscriptores()
		{
			SuscriptorRepositorio suscriptorRepositorio = new SuscriptorRepositorio(udt);
			vista.ComboSuscriptores = suscriptorRepositorio.ObtenerSuscriptoresDTO();
		}

	
		public void LlenarSuscriptor(int idSuscriptor)
		{
			SuscriptorRepositorio repositorio = new SuscriptorRepositorio(udt);
			vista.ComboSuscriptores = repositorio.ObtenerSuscriptorPorId(idSuscriptor);
		}

		///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
		public void LlenarComboServicios(int idSuscriptor)
		{
			try
			{
				FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(udt);
				vista.ComboServicios = repositorioFlujosServicio.ObtenerDTOServicioSusporIdSuscriptor(idSuscriptor);
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
		
		public string ReactivarEstatus(int id, int idSolicitud, int idMovimiento, int usuarioActual, int idSuscriptor)
		{
			string idStatus = string.Empty;
			try
			{
				CasoRepositorio repositorio = new CasoRepositorio(udt);
				idStatus = repositorio.ModificarEstatusAuditoria(id, idSolicitud, idMovimiento, usuarioActual, idSuscriptor);
			}
			catch (Exception e)
			{
				
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			return idStatus;
		}		
	}
}