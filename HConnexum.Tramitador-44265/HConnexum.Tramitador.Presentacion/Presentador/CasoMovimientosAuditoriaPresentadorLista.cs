using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using System.Web;
using System.Web.Configuration;

///<summary>Namespace que engloba el presentador Lista de la capa presentación HC_Configurador.</summary>
namespace HConnexum.Tramitador.Presentacion.Presentador
{
	///<summary>Clase CasoPresentadorLista.</summary>
	public class CasoMovimientosAuditoriaPresentadorLista : PresentadorListaBase<Caso>
	{
		///<summary>Variable vista de la interfaz ICasoLista.</summary>
        readonly ICasoMovimientosAuditoriaLista vista;
        int IdAuditoria;
		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public CasoMovimientosAuditoriaPresentadorLista(ICasoMovimientosAuditoriaLista vista)
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

                IdAuditoria = IdListaValor();
                if (IdAuditoria != null)
                {
                    MovimientoRepositorio repositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
                    IEnumerable<MovimientoDTO> datos = repositorio.ObtenerGridGestionMovimientosAuditoriaDTO(orden, pagina, tamañoPagina, parametrosFiltro, IdAuditoria);
                    this.vista.NumeroDeRegistros = repositorio.Conteo;
                    this.vista.Datos = datos;
                }
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

        public int IdListaValor()
        {

            int Valor;
            MovimientoRepositorio repositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
            ListasValorDTO IdValor = new ListasValorDTO();
            IdValor = repositorio.ObtenerIdListaValor("AUDITORIA", "Estatus del Movimiento");
            Valor = IdValor.Id;
            return Valor;
        }
	}
}