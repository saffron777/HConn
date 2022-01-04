using System;
using System.Linq;
using System.Text;
using HConnexum.Infraestructura;
using System.Web;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Datos;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace HConnexum.Tramitador.Presentacion.Presentador.Bloques
{
	public class MedicoAsignadoPresentador : BloquesPresentadorBase
	{
		readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		readonly IMedicoAsignado vista;

		///<summary>Constructor de la clase.</summary>
		///<param name="vista">Variable tipo Interfaz de la vista.</param>
		public MedicoAsignadoPresentador(IMedicoAsignado vista)
		{
			this.vista = vista;
		}

		public void MostrarVista()
		{
			try
			{
				if(!string.IsNullOrWhiteSpace(vista.Pidpaismed) && !string.IsNullOrWhiteSpace(vista.Piddiv1med) && !string.IsNullOrWhiteSpace(vista.Piddiv2med) && !string.IsNullOrWhiteSpace(vista.Piddiv3med))
                    LlenarComboTerritoriales(int.Parse(vista.Pidpaismed), int.Parse(vista.Piddiv1med), int.Parse(vista.Piddiv2med), int.Parse(vista.Piddiv3med));
			}
			catch(Exception ex)
			{
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

        public void LlenarComboTerritoriales(int idpais, int div1, int div2, int div3)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataTable listadoPaises = null;
                DataTable listadoDiv1 = null;
                DataTable listadoDiv2 = null;
                DataTable listadoDiv3 = null;

                DataSet ds = servicio.ObtenerPaises();
                DataSet ds2 = servicio.ObtenerDivisionesTerritoriales1(div1);
                DataSet ds3 = servicio.ObtenerDivisionesTerritoriales2(div2);
                DataSet ds4 = servicio.ObtenerDivisionesTerritoriales3(div3);

                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoPaises = ds.Tables[0];
                string sIdPais = listadoPaises.AsEnumerable().First(c => c.Field<int>("Id") == idpais)["NombreCorto"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdPais))
                    vista.Pidpaismed = sIdPais;

                if (ds2.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoDiv1 = ds.Tables[0];
                string sIdDiv1 = listadoDiv1.AsEnumerable().First(c => c.Field<int>("Id") == div1)["NombreDivTer1"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdDiv1))
                    vista.Piddiv1med = sIdDiv1;

                if (ds3.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoDiv2 = ds.Tables[0];
                string sIdDiv2 = listadoDiv2.AsEnumerable().First(c => c.Field<int>("Id") == div2)["NombreDivTer1"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdDiv2))
                    vista.Piddiv2med = sIdDiv2;

                if (ds4.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoDiv3 = ds.Tables[0]; 
                string sIdDiv3 = listadoDiv3.AsEnumerable().First(c => c.Field<int>("Id") == div3)["NombreDivTer1"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdDiv3))
                    vista.Piddiv3med = sIdDiv3;

            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
        }

        public void LlenarComboPais(int idpaismed)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataTable listadoPaises = null;
                DataSet ds = servicio.ObtenerPaises();
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoPaises = ds.Tables[0];

                string sIdPais = listadoPaises.AsEnumerable().First(c => c.Field<int>("Id") == idpaismed)["NombreCorto"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdPais))
                    vista.Pidpaismed = sIdPais;
                
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
        }

        public void LlenarComboDiv1(int idPais)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataTable listadoDiv1 = null;
                DataSet ds = servicio.ObtenerDivisionesTerritoriales1(idPais);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoDiv1 = ds.Tables[0];

                string sIdDiv1 = listadoDiv1.AsEnumerable().First(c => c.Field<int>("Id") == idPais)["NombreDivTer1"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdDiv1))
                    vista.Piddiv1med = sIdDiv1;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
        }

        public void LlenarComboDiv2(int idDiv1)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataTable listadoDiv2 = null;
                DataSet ds = servicio.ObtenerDivisionesTerritoriales2(idDiv1);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoDiv2 = ds.Tables[0];

                string sIdDiv2 = listadoDiv2.AsEnumerable().First(c => c.Field<int>("Id") == idDiv1)["NombreDivTer1"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdDiv2))
                    vista.Piddiv2med = sIdDiv2;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
        }

        public void LlenarComboDiv3(int idDiv2)
        {
            ServicioParametrizadorClient servicio = new ServicioParametrizadorClient();
            try
            {
                DataTable listadoDiv3 = null;
                DataSet ds = servicio.ObtenerDivisionesTerritoriales3(idDiv2);
                if (ds.Tables[@"Error"] != null)
                    throw new Exception(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                    listadoDiv3 = ds.Tables[0];

                string sIdDiv3 = listadoDiv3.AsEnumerable().First(c => c.Field<int>("Id") == idDiv2)["NombreDivTer1"].ToString();
                if (!string.IsNullOrWhiteSpace(sIdDiv3))
                    vista.Piddiv3med = sIdDiv3;
            }
            catch (Exception e)
            {
                Errores.ManejarError(e, "Error al Mostrar la data de la Aplicacion");
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", e);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
            }
            finally
            {
                if (servicio.State != CommunicationState.Closed)
                    servicio.Close();
            }
        }


		///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
		/// <returns>Devuelve mensaje(s) con los datos validados.</returns>
		public void ValidarDatos()
		{
			StringBuilder errores = new StringBuilder();
			try
			{
			}
			catch(Exception ex)
			{
                Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
                if (ex.InnerException != null)
                    HttpContext.Current.Trace.Warn("Error", "Error en la Capa origen: " + ex.InnerException.Message);
                HttpContext.Current.Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
                this.vista.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
			vista.Errores = errores.ToString();
		}
	}
}
