using System;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Detalle de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase UsuariosSucriptorPresentadorDetalle.</summary>
	public class UsuariosSucriptorPresentador : PresentadorDetalleBase<UsuariosSucriptor>
	{
		///<summary>Variable vista de la interfaz IUsuariosSucriptorDetalle.</summary>
		readonly IUsuariosSucriptor vista;
		///<summary>Variable de la entidad UsuariosSucriptor.</summary>
		UsuariosSucriptor _UsuariosSucriptor;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public UsuariosSucriptorPresentador(IUsuariosSucriptor vista)
		{
			this.vista = vista;
            this.vista.NombreTabla = GetType();
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
        public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int idUsuarioActual, int idUsuarioSeleccionado)
		{
			try
			{
				UsuarioSuscriptorRepositorio repositorio = new UsuarioSuscriptorRepositorio(udt);
                vista.Datos = repositorio.SupervisadosPorUsuarioSuscriptor(orden,pagina,tamañoPagina,parametrosFiltro,idUsuarioActual, idUsuarioSeleccionado);
				
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                if (e.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + e.InnerException.Message);
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

	
	}
}
