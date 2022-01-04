using System;
using System.Collections.Generic;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Datos;
using System.Web.Configuration;
using System.Web;
using HConnexum.Infraestructura;
using System.ServiceModel;
using System.Configuration;
using System.Data;
using System.Linq;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class CasosPendientesMedicoPresentador
	{
		///<summary>Variable vista de la interfaz ICasosPendientesMedicoPresentador.</summary>
		readonly ICasosPendientesMedicoPresentador vista;

		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public CasosPendientesMedicoPresentador(ICasosPendientesMedicoPresentador vista)
		{
			this.vista = vista;
		}

		///<summary>Método encargado de páginar y filtrar el conjunto de datos devuelto a la vista.</summary>
		public void MostrarVista(string orden, int pagina, int tamañoPagina, IList<Infraestructura.Filtro> parametrosFiltro, int idmov)
		{
			try
			{
				if(idmov != 0)
				{
					DataSet ds = ObtenerParametroPorPaso(idmov);
					string arreglocasos = ObtenerValor(ds, ArregloCasos);
					DataTable dtCasos = new DataTable();
					dtCasos.Columns.Add("IdCaso", typeof(string));
					dtCasos.Columns.Add("Asegurado", typeof(string));
					dtCasos.Columns.Add("DocumentoIdentificacion", typeof(string));
					if(!string.IsNullOrWhiteSpace(arreglocasos))
					{
						string[] strCasos = arreglocasos.Split('|');
						foreach(string item in strCasos)
						{
							DataRow row = dtCasos.NewRow();
							row["IdCaso"] = Convert.ToInt32(item.Split(new Char[] { '&' })[0].ToString());
							row["Asegurado"] = item.Split(new Char[] { '&' })[1].ToString();
							row["DocumentoIdentificacion"] = item.Split(new Char[] { '&' })[2].ToString();
							dtCasos.Rows.Add(row);
						}
						this.vista.Datos = dtCasos.AsEnumerable().Skip((pagina - 1) * tamañoPagina).Take(tamañoPagina).CopyToDataTable();
					}
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

		private string ObtenerValor(DataSet ds, string keyvalor)
		{
			keyvalor = (from p in ds.Tables[0].AsEnumerable()
						where p.Field<string>(AtributoNombre).ToUpper() == keyvalor
						select p.Field<string>(AtributoValor)).SingleOrDefault();
			return string.Empty + keyvalor;
		}

		private DataSet ObtenerParametroPorPaso(int idMovimiento)
		{
			ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
			var tabMovimiento = udt.Sesion.CreateObjectSet<Movimiento>();
			Movimiento movimiento = (from pr in tabMovimiento
									 where pr.Id == idMovimiento &&
										   pr.IndEliminado == false &&
										   pr.IndVigente == true &&
										   pr.FechaValidez <= DateTime.Now
									 select pr).SingleOrDefault();

			return servicio.ObtenerParametrosPorCaso(movimiento.IdCaso);
		}

		#region Parametros Generales
		public string AtributoNombre
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoNombreGenerales"];
			}
		}

		public string AtributoValor
		{
			get
			{
				return ConfigurationManager.AppSettings[@"AtributoValor"];
			}
		}
		#endregion Parametros Generales

		#region propiedades
		public string ArregloCasos
		{
			get
			{
				return @"ARREGLOCASOS";
			}
		}
		#endregion
	}
}
