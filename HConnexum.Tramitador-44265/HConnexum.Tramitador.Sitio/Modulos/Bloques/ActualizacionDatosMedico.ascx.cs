using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class ActualizacionDatosMedico : UserControlBase, IActualizacionDatosMedico
	{
		ActualizacionDatosMedicoPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new ActualizacionDatosMedicoPresentador(this);
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

		public string ValidarDatos()
		{
			try
			{
				presentador.ValidarDatos();
				if(Errores.Length > 0)
					return Errores;
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
			return string.Empty;
		}

		/// <summary>
		/// Método que habilita y deshabilita los controles dependiendo de los combos
		/// </summary>
		/// <param name="modo"></param>
		protected void ConfigurarControles()
		{
			if(NuevoContacto.SelectedItem != null)
				switch(NuevoContacto.SelectedItem.Text.ToUpper())
				{
					case @"SÍ":
                    case @"SI":
						pTelefono.Visible = true;
						pSolicitudAnula.Visible = false;
						SolicitudAnulacion.ClearSelection();
						break;
					case @"NO":
						pSolicitudAnula.Visible = true;
						pTelefono.Visible = false;
						break;
					default:
						pSolicitudAnula.Visible = pTelefono.Visible = false;
						SolicitudAnulacion.ClearSelection();
						break;
				}
			else
			{
				pSolicitudAnula.Visible = pTelefono.Visible = false;
				SolicitudAnulacion.ClearSelection();
			}
		}

		#region propiedades
		public IEnumerable<ListasValorDTO> ComboNuevoContacto
		{
			set
			{
				NuevoContacto.DataSource = value;
				NuevoContacto.DataBind();
			}
		}

		public string Ptlfmed
		{
			get
			{
				return tlfmed.NumeroTlfCompletoGuion;
			}
			set
			{
				tlfmed.NumeroTlfCompletoGuion = value;
			}
		}

		public IEnumerable<ListasValorDTO> ComboSolicitudAnulacion
		{
			set
			{
				SolicitudAnulacion.DataSource = value;
				SolicitudAnulacion.DataBind();
			}
		}
		#endregion
	}
}