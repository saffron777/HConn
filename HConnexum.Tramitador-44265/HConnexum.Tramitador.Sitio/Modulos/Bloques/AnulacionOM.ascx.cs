using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class AnulacionOM : UserControlBase, IAnulacion
	{
		///<summary>Objeto 'Presentador' asociado al control Web de usuario.</summary>
		AnulacionPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new AnulacionPresentador(this);
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
					presentador.LlenarCombos();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		#region Propiedades
		/// <summary>Identificador único del control Web de usuario.</summary>
		public string Id
		{
			get
			{
				return this.ApruebaAnula.ID;
			}
			set
			{
				this.ApruebaAnula.ID = value;
			}
		}

		/// <summary>Obtiene el valor del elemento seleccionado actualmente en el combobox.</summary>
		public string IdValorSeleccionado
		{
			get
			{
				return this.ApruebaAnula.SelectedValue;
			}
		}

		///<summary>Obtiene el conjunto de registros a mostrar en un combobox.</summary>
		public IEnumerable<ListasValorDTO> ComboAprobacion
		{
			set
			{
				this.ApruebaAnula.DataSource = value;
				this.ApruebaAnula.DataBind();
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