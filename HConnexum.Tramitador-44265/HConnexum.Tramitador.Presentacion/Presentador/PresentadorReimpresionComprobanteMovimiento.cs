using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz;

namespace HConnexum.Tramitador.Presentacion.Presentador
{
	public class PresentadorReimpresionComprobanteMovimiento : PresentadorListaBase<Paso>
	{
		///<summary>Variable vista de la interfaz IConsultaCasos.</summary>
		readonly IReimpresionComprobanteMovimiento vista;
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();
		IEnumerable<CasoDTO> datos;
		readonly bool supervisor = false;
		readonly IList<int> idsSupervisados = new List<int>();
		readonly string fechadesde = "";
		readonly string fechahasta = "";
		readonly DateTime fechaInicial = DateTime.Now.AddMonths(-6);
		readonly DateTime fechafinal = DateTime.Now.AddDays(1);

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public PresentadorReimpresionComprobanteMovimiento(IReimpresionComprobanteMovimiento vista)
		{
			this.vista = vista;
			this.vista.NombreTabla = this.GetType();
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
				int numpagina = pagina;
				int cantregistros = tamañoPagina;
				Filtro tes = new Filtro();

				if (this.vista.TipoFiltro.Trim() != "" && !string.IsNullOrEmpty(this.vista.Filtro))
				{
					
					#region Filtros
					
					if (this.vista.TipoFiltro == "Id")
					{
						tes.Campo = "Id";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(int);
						tes.Valor = int.Parse(this.vista.Filtro);
						parametrosFiltro.Add(tes);
					}
					else if (this.vista.TipoFiltro == "SupportIncident")
					{
						tes.Campo = "SupportIncident";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(string);
						tes.Valor = this.vista.Filtro;
						parametrosFiltro.Add(tes);
					}
					else if (this.vista.TipoFiltro == "cedula")
					{
						tes.Campo = "NumDocSolicitante";
						tes.Operador = "EqualTo";
						tes.Tipo = typeof(string);
						tes.Valor = this.vista.Filtro;
						parametrosFiltro.Add(tes);
					}
				
					#endregion
					
				}
				if (!string.IsNullOrEmpty(this.vista.FechaDesde))
				{
					tes.Campo = "FechaSolicitud";
					tes.Operador = "GreaterThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(this.vista.FechaDesde);
					parametrosFiltro.Add(tes);
				}
				if (!string.IsNullOrEmpty(this.vista.FechaHasta))
				{
					tes.Campo = "FechaSolicitud";
					tes.Operador = "LessThanOrEqualTo";
					tes.Tipo = typeof(DateTime);
					tes.Valor = DateTime.Parse(this.vista.FechaHasta).AddDays(+1);
					parametrosFiltro.Add(tes);
				}
				if (!string.IsNullOrEmpty(this.vista.Asegurado))
				{
					tes.Campo = "Asegurado";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(string);
					tes.Valor = (this.vista.Asegurado);
					parametrosFiltro.Add(tes);
				}
				if (!string.IsNullOrEmpty(this.vista.Intermediario))
				{
					tes.Campo = "Intermediario";
					tes.Operador = "EqualTo";
					tes.Tipo = typeof(string);
					tes.Valor = (this.vista.Intermediario);
					parametrosFiltro.Add(tes);
				}
				
				CasoRepositorio repositorioCasos = new CasoRepositorio(this.udt);
				this.datos = repositorioCasos.ReimpresionComprobanteMovimiento(orden, pagina, tamañoPagina, parametrosFiltro, this.vista.UsuarioActual.SuscriptorSeleccionado.Id);
				this.vista.NumeroDeRegistros = repositorioCasos.Conteo;
				this.vista.Datos = this.datos;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				if (e.InnerException != null)
				{
					HttpContext.Current.Trace.Warn("Error", string.Format("Error en la Capa origen: {0}", e.InnerException.Message));
				}
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}