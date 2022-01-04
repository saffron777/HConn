using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using System.Web.Configuration;
using System.Configuration;
using System.Web.UI.WebControls;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class ActualizacionDeContacto : UserControlBase, IActualizacionDeContacto
	{
		ActualizacionDeContactoPresentadorDetalle presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new ActualizacionDeContactoPresentadorDetalle(this);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
				if(!Page.IsPostBack)
					presentador.LlenarCombo();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		/// <summary>Evento previo al renderizado de la Pagina.</summary>
		/// <param name="sender">Objeto que ejecuta el evento.</param>
		/// <param name="e">Argumentos.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			ConfigurarControles();
		}

		protected void NuevoContacto_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
		}

		/// <summary>
		/// Método que habilita y deshabilita los controles dependiendo de los combos
		/// </summary>
		/// <param name="modo"></param>
		protected void ConfigurarControles()
		{
			string valorDecision = ConfigurationManager.AppSettings[@"ListaValorDesicionSimpleSi"];
			if(NuevoContacto.SelectedItem != null)
			{
				if(NuevoContacto.SelectedItem.Text == valorDecision)
					pTelefono.Visible = true;
				else
				{
					pTelefono.Visible = false;
					tlfaseg.IsRequired = false;
				}
			}
			else
				pTelefono.Visible = false;
		}

		public string ValidarDatos()
		{
			string strReturn = "";
			try
			{
				string valorDecision = ConfigurationManager.AppSettings[@"ListaValorDesicionSimpleSi"];
				if(NuevoContacto.SelectedItem != null)
				{
					if(NuevoContacto.SelectedItem.Text == valorDecision)
					{
						if(((TextBox)(pTelefono.Controls[3].FindControl("txtCodArea"))).Text == string.Empty)
							strReturn = "<b>* Código de área.<br><b>";

						if(((TextBox)(pTelefono.Controls[3].FindControl("txtNumero"))).Text == string.Empty)
							strReturn += "<b>* Número de teléfono.<b>";

						if(strReturn != string.Empty)
							strReturn = "Debe escribir: <br><br>" + strReturn;
					}
					else
					{
						pTelefono.Visible = false;
						tlfaseg.IsRequired = false;
					}
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
			return strReturn;
		}

		#region "Propiedades de Presentación"
		public string Ptlfaseg
		{
			get
			{
				return tlfaseg.NumeroTlfCompletoGuion;
			}
			set
			{
				tlfaseg.NumeroTlfCompletoGuion = value;
			}
		}
		public string IdNuevoContacto
		{
			get
			{
				return NuevoContacto.SelectedValue;
			}
			set
			{
				NuevoContacto.SelectedValue = value;
			}
		}
		public IEnumerable<ListasValorDTO> ComboNuevoContacto
		{
			set
			{
				NuevoContacto.DataSource = value;
				NuevoContacto.DataBind();
			}
		}
		#endregion
	}
}