using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class RegistrodeOpinionMedico : UserControlBase, IRegistrodeOpinionMedico
	{
		RegistrodeOpinionMedicoPresentadorDetalle presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new RegistrodeOpinionMedicoPresentadorDetalle(this);
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
				fechaOpMed.Value = DateTime.Now.ToString("yyyyMMdd");
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

		#region "Propiedades de Presentación"
		public string ValorDesicion = ConfigurationManager.AppSettings[@"ListaValorDesicionSimpleNo"].ToString();

		public string Pobservacionmed
		{
			get
			{
				return observacionmed.Text;
			}
			set
			{
				observacionmed.Text = value;
			}
		}

        public string MensajeError
        {
            get 
            {
                return lblMsjError.Text;
            }
            set
            {
                lblMsjError.Text = value;
            }
        }

		public string IdDecisionSimple
		{
			get
			{
				return OpMed.SelectedValue;
			}
			set
			{
				OpMed.SelectedValue = value;
			}
		}

		public IEnumerable<ListasValorDTO> ComboDecisionSimple
		{
			set
			{
				OpMed.DataSource = value;
				OpMed.DataBind();
			}
		}

		/// <summary>Fecha de la opinión médica.</summary>
		public DateTime Fecha
		{
			get
			{
				return DateTime.Now;
			}
			set
			{
				fechaOpMed.Value = value.ToString("yyyyMMdd");
			}
		}
		#endregion
	}
}