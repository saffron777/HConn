using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Seguridad;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;

namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
	public partial class SolicitudServicio : PaginaDetalleBase, ISolicitudServicio
	{
		#region "Variables Locales"
		///<summary>Variable presentador CasoPresentadorDetalle.</summary>
		SolicitudServicioPresentador presentador;
		#endregion "Variables Locales"

		#region "Eventos de la Página"
		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new SolicitudServicioPresentador(this);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
				if(!Page.IsPostBack)
				{
					if(ValidarRol(ConfigurationManager.AppSettings[@"RolSimulaProveedor"].ToString()))
					{
						PanelSimulaProveedor.Visible = true;
						rfvSuscriptor.Enabled = false;
						presentador.ListarProveedoresServiciosSimulados(UsuarioActual.SuscriptorSeleccionado.Id);
						if(rcbProveeServSimulados.SelectedItem != null && !string.IsNullOrEmpty(rcbProveeServSimulados.SelectedItem.Value))
							if(presentador.CargarDatos(int.Parse(rcbProveeServSimulados.Items[0].Value)) > 0)
								presentador.LlenarComboSuscriptores();
					}
					else
					{
						PanelSimulaProveedor.Visible = false;
						rfvSuscriptor.Enabled = true;
						if(presentador.CargarDatos(UsuarioActual.SuscriptorSeleccionado.Id) <= 0)
						{
							ddlSuscriptor.Enabled = false;
							cmdSolicitar.Enabled = false;
							lblNotiene.Visible = true;
						}
						else
						{
							presentador.LlenarComboSuscriptores();
							lblNotiene.Visible = false;
						}
					}
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		public void cmdSolicitar_Click(object sender, EventArgs e)
		{
			try
			{
				string idSuscriptorProveedor;
				if(ValidarRol(ConfigurationManager.AppSettings[@"RolSimulaProveedor"].ToString()))
					idSuscriptorProveedor = rcbProveeServSimulados.SelectedValue;
				else
					idSuscriptorProveedor = this.UsuarioActual.SuscriptorSeleccionado.Id.ToString();
				Response.Redirect(@"~/Modulos/Orquestador/PantallaContenedora2.aspx?idMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) +
																					"&idflujoservicio=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdServicio.ToString().Encriptar())) +
																					"&idSuscriptorIntermediario=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdSuscriptor.ToString().Encriptar())) +
																					"&idSuscriptorProveedor=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(idSuscriptorProveedor.ToString().Encriptar())), false);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		protected void ddlSuscriptor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
                ddlServicio.ClearSelection();
				ddlServicio.Enabled = true;
				presentador.LlenarComboServicios(int.Parse(IdSuscriptor));
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		protected void btnSimulaProveedor_Click(object sender, EventArgs e)
		{
			Response.Redirect(@"~/Modulos/Parametrizador/ServiciosSimuladoLista.aspx?idMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())), false);

		}

		protected void rcbProveeServSimulados_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
			if(presentador.CargarDatos(int.Parse(rcbProveeServSimulados.SelectedValue)) > 0)
			{
				presentador.LlenarComboSuscriptores();
				lblNotiene.Visible = ddlSuscriptor.Items.Count <= 1;
			}
		}
		#endregion

		#region PROPIEDADES
		public DataSet Datos
		{
			get
			{
				if(this.ViewState[@"Datos"] != null)
					return this.ViewState[@"Datos"] as DataSet;
				return new DataSet();
			}
			set
			{
				this.ViewState[@"Datos"] = value;
			}
		}

		public string IdSuscriptor
		{
			get
			{
				return this.ddlSuscriptor.SelectedValue;
			}
			set
			{
				this.ddlSuscriptor.SelectedValue = value;
			}
		}

		public DataTable ComboSuscriptor
		{
			set
			{
				this.ddlSuscriptor.DataSource = value;
				this.ddlSuscriptor.DataBind();
			}
		}

		public string IdServicio
		{
			get
			{
				return this.ddlServicio.SelectedValue;
			}
			set
			{
				this.ddlServicio.SelectedValue = value;
			}
		}

		public DataTable ComboServicio
		{
			set
			{
				this.ddlServicio.DataSource = value;
				this.ddlServicio.DataBind();
			}
		}

		public DataTable ComboProveeServSimulados
		{
			set
			{
				this.rcbProveeServSimulados.DataSource = value;
				this.rcbProveeServSimulados.DataBind();
			}
		}
		#endregion

		#region MÉTODOS
		public bool ValidarRol(string sRol)
		{
			bool bvalidarRol = false;
			foreach(RolesUsuario rol in UsuarioActual.AplicacionActual(IdAplicacion).Roles)
				if(rol.NombreRol == sRol)
				{
					bvalidarRol = true;
					break;
				}
			return bvalidarRol;
		}
		#endregion
	}
}