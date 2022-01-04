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
	public class AnularSuspenderCasoPresentadorLista : PresentadorListaBase<Caso>
	{
		///<summary>Variable vista de la interfaz ICasoLista.</summary>
        readonly IAnularSuspenderCasoLista vista;
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();



		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
        public AnularSuspenderCasoPresentadorLista(IAnularSuspenderCasoLista vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		///<param name="orden">Indica el orden del conjunto de registros: ASC o DESC.</param>
		///<param name="pagina">Indica el número de página del conjunto de registros.</param>
		///<param name="tamañoPagina">Indica la cantidad de registros a devolver del conjunto.</param>
		///<param name="parametrosFiltro">Objeto que envuelve los distintos filtros y valores aplicables al conjunto de registros.</param>
        public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int idSuscriptor)
		{
			try
            {
                int pend = IdListaValorPendiente();
                int proc = IdListaValorEnProceso();
                int sol = IdListaValorSolicitado();
                string FiltrosBusqueda = "";
                Filtro tes = new Filtro();
               

                if (vista.TipoFiltro.Trim() != "")
                {
                    if (!String.IsNullOrEmpty(vista.Filtro))
                    {
                        if (vista.TipoFiltro == "Id")
                        {

                            tes.Campo = "Id";
                            tes.Operador = "EqualTo";
                            tes.Tipo = typeof(int);
                            tes.Valor = int.Parse(vista.Filtro);

                        }
                        else
                        {
                            tes.Campo = vista.TipoFiltro;
                            tes.Operador = "EqualTo";
                            tes.Tipo = typeof(string);
                            tes.Valor = vista.Filtro;
                        }
                        parametrosFiltro.Add(tes);
                    }

                    string campo = "";
                    if (tes.Campo == "Id")
                    {
                        campo = "IdCaso";
                        FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = " + tes.Valor;
                    }
                    else if (tes.Campo == "Idcasoexterno")
                    {
                        campo = "Reclamo";
                        FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                    }
                    else if (tes.Campo == "SupportIncident")
                    {
                        campo = "Suport_Incident";
                        FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                    }
                    else if (tes.Campo == "NumDocSolicitante")
                    {
                        campo = "DocumentoId";
                        FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                    }
                    else if (tes.Campo == "Ticket")
                    {
                        campo = "Ticket";
                        FiltrosBusqueda = FiltrosBusqueda + "  and TB_CasosExternos." + campo + " = '" + tes.Valor + "'";
                    }
                }
                //if (vista.IdTipodoc != "")
                //{
                //    tes.Campo = "TipDocSolicitante"; tes.Operador = "EqualTo"; tes.Tipo = typeof(string); tes.Valor = vista.IdTipodoc;
                //    parametrosFiltro.Add(tes);
                //}
                if (vista.IdServicio != "")
                {
                    tes.Campo = "IdFlujoServicio"; tes.Operador = "EqualTo"; tes.Tipo = typeof(int); tes.Valor = int.Parse(vista.IdServicio);
                    parametrosFiltro.Add(tes);
                }

				CasoRepositorio repositorio = new CasoRepositorio(this.unidadDeTrabajo);
                IEnumerable<CasoDTO> datos = repositorio.ObtenerGridDTO(orden, pagina, tamañoPagina, parametrosFiltro, pend, proc, sol, idSuscriptor);
				this.vista.NumeroDeRegistros = repositorio.Conteo;
				this.vista.Datos = datos;
			}
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}


        public int IdListaValorPendiente()
        {

            int Valor;
            MovimientoRepositorio repositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
            ListasValorDTO IdValor = new ListasValorDTO();
            IdValor = repositorio.ObtenerIdListaValor("PEND", "Estatus del Caso");
            Valor = IdValor.Id;
            return Valor;
        }

        public int IdListaValorSolicitado()
        {

            int Valor;
            MovimientoRepositorio repositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
            ListasValorDTO IdValor = new ListasValorDTO();
            IdValor = repositorio.ObtenerIdListaValor("SOL", "Estatus del Caso");
            Valor = IdValor.Id;
            return Valor;
        }
        public int IdListaValorEnProceso()
        {

            int Valor;
            MovimientoRepositorio repositorio = new MovimientoRepositorio(this.unidadDeTrabajo);
            ListasValorDTO IdValor = new ListasValorDTO();
            IdValor = repositorio.ObtenerIdListaValor("PROC", "Estatus del Caso");
            Valor = IdValor.Id;
            return Valor;
        }
        ///<summary>Rutina para el llenado de DropDownList, con la descripción o nombre de la clave foranea.</summary>
        public void LlenarCombos()
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataSet ds = servicio.ObtenerListaValorPorNombre("TipoDocumento");
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                //if (ds.Tables[0].Rows.Count > 0)
                //    vista.ComboIdTipodoc = ds.Tables[0];

                FlujosServicioRepositorio repositorioFlujosServicio = new FlujosServicioRepositorio(udt);
                IEnumerable<FlujosServicioDTO> listadoFlujoServicio = repositorioFlujosServicio.ObtenerDTO();
                vista.ComboIdServicio = listadoFlujoServicio.ToList();

            }
            catch (Exception ex)
            {
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];

            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
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
			catch (Exception e)
			{
				Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
				HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
				this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}
	}
}