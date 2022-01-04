using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using System.Web.Configuration;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class SeguimientoCita : UserControlBase, ISeguimientoCita
	{
		SeguimientoCitaPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new SeguimientoCitaPresentador(this);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
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

		/// <summary>
		/// Método que habilita y deshabilita los controles dependiendo de los combos
		/// </summary>
		/// <param name="modo"></param>
		protected void ConfigurarControles()
		{
			if(AfiliadoContacto.SelectedItem != null)
				switch(AfiliadoContacto.SelectedItem.Text.ToUpper())
				{
					case @"REPETIR LLAMADA":
						pFechaHoraProxLlamada.Visible = true;
						pSolicitudAnulacion.Visible = pFechaHoraCita.Visible = pCita.Visible = pSolicitudCambioMedico.Visible = false;
						CambioDeMedico.ClearSelection();
						Cita.ClearSelection();
						fechacita.Clear();
						horacita.Clear();
						SolicitudAnulacion.Checked = false;
						break;
					case "SÍ":
                    case "SI":
						pSolicitudAnulacion.Visible = pSolicitudCambioMedico.Visible = true;
						pFechaHoraCita.Visible = pCita.Visible = pFechaHoraProxLlamada.Visible = false;
						fechaproxllamada.Clear();
						horaproxllamada.Clear();
						fechacita.Clear();
						horacita.Clear();
						break;
					default:
						pSolicitudAnulacion.Visible = pFechaHoraCita.Visible = pCita.Visible = pSolicitudCambioMedico.Visible = pFechaHoraProxLlamada.Visible = false;
						fechaproxllamada.Clear();
						horaproxllamada.Clear();
						CambioDeMedico.ClearSelection();
						Cita.ClearSelection();
						fechacita.Clear();
						horacita.Clear();
						SolicitudAnulacion.Checked = false;
						break;
				}
			else
			{
				pSolicitudAnulacion.Visible = pFechaHoraCita.Visible = pCita.Visible = pSolicitudCambioMedico.Visible = pFechaHoraProxLlamada.Visible = false;
				fechaproxllamada.Clear();
				horaproxllamada.Clear();
				CambioDeMedico.ClearSelection();
				Cita.ClearSelection();
				fechacita.Clear();
				horacita.Clear();
				SolicitudAnulacion.Checked = false;
			}
			if(CambioDeMedico.SelectedItem != null)
				switch(CambioDeMedico.SelectedItem.Text.ToUpper())
				{
					case @"NO":
						pCita.Visible = true;
						break;
					case @"SÍ":
                    case @"SI":
						pFechaHoraCita.Visible = pCita.Visible = false;
						Cita.ClearSelection();
						fechacita.Clear();
						horacita.Clear();
						break;
				}
			if(Cita.SelectedItem != null)
				switch(Cita.SelectedItem.Text.ToUpper())
				{
					case @"SÍ":
                    case @"SI":
						pFechaHoraCita.Visible = true;
						break;
					case @"NO":
						pFechaHoraCita.Visible = false;
						fechacita.Clear();
						horacita.Clear();
						break;
				}
		}

		protected void AfiliadoContacto_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		protected void Cita_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
		}

		protected void CambioDeMedico_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
		{
		}

		#region Propiedades
		public string PAfiliadoContacto
		{
			get
			{
				return AfiliadoContacto.SelectedValue;
			}
			set
			{
				AfiliadoContacto.SelectedValue = value;
				this.AfiliadoContacto_SelectedIndexChanged(this, EventArgs.Empty);
			}
		}

		public IEnumerable<ListasValorDTO> ComboAfiliadoContacto
		{
			set
			{
				AfiliadoContacto.DataSource = value;
				AfiliadoContacto.DataBind();
			}
		}

		public DateTime Pfechaproxllamada
		{
			get
			{
				return fechaproxllamada.SelectedDate.Value;
			}
			set
			{
				fechaproxllamada.SelectedDate = value;
			}
		}

		public DateTime Phoraproxllamada
		{
			get
			{
				return horaproxllamada.SelectedDate.Value;
			}
			set
			{
				horaproxllamada.SelectedDate = value;
			}
		}

		public string PCambioDeMedico
		{
			get
			{
				return CambioDeMedico.SelectedValue;
			}
			set
			{
				CambioDeMedico.SelectedValue = value;
			}
		}

		public IEnumerable<ListasValorDTO> ComboCambioDeMedico
		{
			set
			{
				CambioDeMedico.DataSource = value;
				CambioDeMedico.DataBind();
			}
		}

		public string PCita
		{
			get
			{
				return Cita.SelectedValue;
			}
			set
			{
				Cita.SelectedValue = value;
			}
		}

		public IEnumerable<ListasValorDTO> ComboCita
		{
			set
			{
				Cita.DataSource = value;
				Cita.DataBind();
			}
		}

		public DateTime Pfechacita
		{
			get
			{
				return fechacita.SelectedDate.Value;
			}
			set
			{
				fechacita.SelectedDate = value;
			}
		}

		public DateTime Phoracita
		{
			get
			{
				return horacita.SelectedDate.Value;
			}
			set
			{
				horacita.SelectedDate = value;
			}
		}

		public bool PSolicitudAnulacion
		{
			get
			{
				return SolicitudAnulacion.Checked;
			}
			set
			{
				SolicitudAnulacion.Checked = value;
			}
		}
		#endregion Propiedades
	}
}