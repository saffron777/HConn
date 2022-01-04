using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

///<summary>Namespace que engloba el presentador Maestro Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase FlujosServicioPresentadorMaestroDetalle.</summary>
	public class SolicitudBloquePresentadorMaestroDetalle : PresentadorListaBase<FlujosServicio>
	{
		///<summary>Variable vista de la interfaz IFlujosServicioMaestroDetalle.</summary>
		readonly ISolicitudBloqueMaestroDetalle vista;

		///<summary>Variable de la entidad FlujosServicio.</summary>
		FlujosServicioDTO flujosServicio;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public SolicitudBloquePresentadorMaestroDetalle(ISolicitudBloqueMaestroDetalle vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = GetType();
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
				parametrosFiltro.Add(new Infraestructura.Filtro { Campo = "IdFlujoServicio", Operador = "EqualTo", Tipo = typeof(int), Valor = this.vista.Id });
				FlujosServicioRepositorio repositorioMaestro = new FlujosServicioRepositorio(this.udt);
				this.flujosServicio = repositorioMaestro.ObtenerDtoFlujosServicioPorId(this.vista.Id);
				this.PresentadorAVista();
				SolicitudBloqueRepositorio repositorioDetalle = new SolicitudBloqueRepositorio(unidadDeTrabajo);
				IEnumerable<SolicitudBloqueDTO> datos = repositorioDetalle.ObtenerDTO(orden, pagina, tamañoPagina, parametrosFiltro);
				this.vista.NumeroDeRegistros = repositorioDetalle.Conteo;
				this.vista.Datos = datos;
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de eliminar registros del conjunto.</summary>
		///<param name="ids">Objeto tipo lista que contiene los Id's a eliminar.</param>
		public void Eliminar(IList<string> ids)
		{
			try
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				SolicitudBloqueRepositorio repositorio = new SolicitudBloqueRepositorio(this.unidadDeTrabajo);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					SolicitudBloque solicitudBloque = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.EliminarLogico(solicitudBloque, idDesencriptado))
						this.vista.Errores = "Ya Existen registros asociados a la entidad _SolicitudBloque";
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

		///<summary>Método encargado de asignar valores a propiedades de la vista.</summary>	
		private void PresentadorAVista()
		{
			try
			{
				this.vista.Id = this.flujosServicio.Id;
				this.vista.IndPublico = this.flujosServicio.IndPublico.ToString();
				this.vista.IdSuscriptor = this.flujosServicio.NombreSuscriptor;
				this.vista.IdServicioSuscriptor = this.flujosServicio.NombreServicioSuscriptor;
				this.vista.Version = string.Format("{0:N0}", this.flujosServicio.Version);
				this.CargarPublicacion();
				this.CargarAuditoria();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a campos de publicación de la vista.</summary>
		private void CargarPublicacion()
		{
			try
			{
				this.vista.FechaValidez = this.flujosServicio.FechaValidez.ToString();
				this.vista.IndVigente = this.flujosServicio.IndVigente.ToString();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Método encargado de asignar valores a campos de auditoria de la vista.</summary>
		private void CargarAuditoria()
		{
			try
			{
				this.vista.CreadoPor = this.ObtenerNombreUsuario(this.flujosServicio.CreadoPor);
				this.vista.FechaCreacion = this.flujosServicio.FechaCreacion.ToString();
				this.vista.ModificadoPor = this.ObtenerNombreUsuario(this.flujosServicio.ModificadoPor);
				this.vista.FechaModicacion = this.flujosServicio.FechaModicacion.ToString();
				this.vista.IndEliminado = this.flujosServicio.IndEliminado.ToString();
			}
			catch(Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void ActivarEliminado(IList<string> ids)
		{
			try
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				SolicitudBloqueRepositorio repositorio = new SolicitudBloqueRepositorio(this.unidadDeTrabajo);
				foreach(string id in ids)
				{
					int idDesencriptado = int.Parse(System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(id.ToString())).Desencriptar());
					SolicitudBloque solicitudBloque = repositorio.ObtenerPorId(idDesencriptado);
					if(!repositorio.activarEliminarLogico(solicitudBloque, solicitudBloque.Id))
						this.vista.Errores = "Ya Existen registros asociados a este Bloque de Solicitud ";
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
	}
}