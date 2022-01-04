using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class ContactoInicial : UserControlBase, IContactoInicial
	{
		ContactoInicialPresentador presentador;

        protected void Page_Init(object sender, EventArgs e)
        {
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new ContactoInicialPresentador(this);
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
                if (!Page.IsPostBack)
                   presentador.LlenarCombo();
                                   
                string sOpinionMedicaCargaDiasCantidadMax = WebConfigurationManager.AppSettings[@"OpinionMedicaCargaDiasCantidadMax"] ?? "29";
                int opinionMedicaCargaDiasCantidadMax = 0;
                if (int.TryParse(sOpinionMedicaCargaDiasCantidadMax, out opinionMedicaCargaDiasCantidadMax))
                    FechaTopeCargaOM.Value = 
                        DateTime.Now.Date.AddDays(opinionMedicaCargaDiasCantidadMax).ToString("dd/MM/yyyy");
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void AfiliadoContacto_SelectedIndexChanged(object sender, EventArgs e)
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
        public string ObservacionporAnulacion 
        {
            get
            {
                return ObservacionAnulacion.Text;
            }
            set
            {
                ObservacionAnulacion.Text = value;
                
            }
        }
		#endregion Propiedades
	}
}