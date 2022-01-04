using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class AsistenciaCitaMedica : UserControlBase, IAsistenciaCitaMedica
	{
		AsistenciaCitaMedicaPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new AsistenciaCitaMedicaPresentador(this);
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
				switch(this.AfiliadoContacto.SelectedItem.Text.ToUpper())
				{
					case @"REPETIR LLAMADA":
						pHoraProxLlamada.Visible = pFechaProxLlamada.Visible = true;
						pHoraCita.Visible = pFechaCita.Visible = pAccion.Visible = pAsistio.Visible = pCambioMedico.Visible = false;
						CambioDeMedico.ClearSelection();
						Asistencia.ClearSelection();
						Accion.ClearSelection();
						fechacita.Clear();
						horacita.Clear();
						break;
					case @"SÍ":
                    case @"SI":
						pCambioMedico.Visible = true;
						pHoraCita.Visible = pFechaCita.Visible = pAccion.Visible = pAsistio.Visible = pHoraProxLlamada.Visible = pFechaProxLlamada.Visible = false;
						fechacita.Clear();
						horacita.Clear();
						break;
					default:
						pHoraCita.Visible = pFechaCita.Visible = pAccion.Visible = pAsistio.Visible = pCambioMedico.Visible = pHoraProxLlamada.Visible = pFechaProxLlamada.Visible = false;
						fechaproxllamada.Clear();
						horaproxllamada.Clear();
						CambioDeMedico.ClearSelection();
						Asistencia.ClearSelection();
						Accion.ClearSelection();
						fechacita.Clear();
						horacita.Clear();
						break;
				}
			else
			{
				pHoraCita.Visible = pFechaCita.Visible = pAccion.Visible = pAsistio.Visible = pCambioMedico.Visible = pHoraProxLlamada.Visible = pFechaProxLlamada.Visible = false;
				fechaproxllamada.Clear();
				horaproxllamada.Clear();
				CambioDeMedico.ClearSelection();
				Asistencia.ClearSelection();
				Accion.ClearSelection();
				fechacita.Clear();
				horacita.Clear();
			}
			if(CambioDeMedico.SelectedItem != null)
				switch(CambioDeMedico.SelectedItem.Text.ToUpper())
				{
					case "NO":
						pAsistio.Visible = true;
						pHoraCita.Visible = pFechaCita.Visible = pAccion.Visible = false;
						fechacita.Clear();
						horacita.Clear();
						break;
					case "SÍ":
                    case "SI":
						pHoraCita.Visible = pFechaCita.Visible = pAccion.Visible = pAsistio.Visible = false;
						Asistencia.ClearSelection();
						Accion.ClearSelection();
						fechacita.Clear();
						horacita.Clear();
						break;
				}
			if(Asistencia.SelectedItem != null)
				switch(Asistencia.SelectedItem.Text.ToUpper())
				{
					case "NO":
						pAccion.Visible = true;
						pHoraCita.Visible = pFechaCita.Visible = false;
						fechacita.Clear();
						horacita.Clear();
						break;
					case "SÍ":
                    case "SI":
						pHoraCita.Visible = pFechaCita.Visible = pAccion.Visible = false;
						Accion.ClearSelection();
						fechacita.Clear();
						horacita.Clear();
						break;
				}
			if(Accion.SelectedItem != null)
				switch(Accion.SelectedItem.Text.ToUpper())
				{
					case "AFILIADO RE PLANIFICÓ":
						pHoraCita.Visible = pFechaCita.Visible = true;
						break;
					default:
						pHoraCita.Visible = pFechaCita.Visible = false;
						horacita.Clear();
						fechacita.Clear();
						break;
				}
		}

		protected void AfiliadoContacto_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		protected void CambioDeMedico_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		protected void Asistencia_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		protected void Accion_SelectedIndexChanged(object sender, EventArgs eventArgs)
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

		public string PAccion
		{
			get
			{
				return Accion.SelectedValue;
			}
			set
			{
				Accion.SelectedValue = value;
				this.Accion_SelectedIndexChanged(this, EventArgs.Empty);
			}
		}

		public IEnumerable<ListasValorDTO> ComboAccion
		{
			set
			{
				Accion.DataSource = value;
				Accion.DataBind();
			}
		}

		public string PAsistencia
		{
			get
			{
				return Asistencia.SelectedValue;
			}
			set
			{
				Asistencia.SelectedValue = value;
				this.Asistencia_SelectedIndexChanged(this, EventArgs.Empty);
			}
		}

		public IEnumerable<ListasValorDTO> ComboAsistencia
		{
			set
			{
				Asistencia.DataSource = value;
				Asistencia.DataBind();
			}
		}
		#endregion Propiedades
	}
}