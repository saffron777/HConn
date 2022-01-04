using System;
using System.Linq;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using System.Web.Configuration;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class MedicoAsignado : UserControlBase, IMedicoAsignado
	{
		MedicoAsignadoPresentador presentador;

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new MedicoAsignadoPresentador(this);
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
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		#region Propiedades
		public string Ptipdocmed
		{
			get
			{
				return tipdocmed.Text;
			}
			set
			{
				tipdocmed.Text = value;
			}
		}

		public string Pnumdocmed
		{
			get
			{
				return numdocmed.Text;
			}
			set
			{
				numdocmed.Text = value;
			}
		}

		public string Pnommed
		{
			get
			{
				return nommed.Text;
			}
			set
			{
				nommed.Text = value;
			}
		}

		public string Ptlfmed
		{
			get
			{
				return tlfmed.Text;
			}
			set
			{
				tlfmed.Text = value;
			}
		}

		public string Pespmed
		{
			get
			{
				return espmed.Text;
			}
			set
			{
				espmed.Text = value;
			}
		}

		public string Pidpaismed
		{
			get
			{
				return idpaismed.Text;
			}
			set
			{
				idpaismed.Text = value;
			}
		}

		public string Piddiv1med
		{
			get
			{
				return iddiv1med.Text;
			}
			set
			{
				iddiv1med.Text = value;
			}
		}

		public string Piddiv2med
		{
			get
			{
				return iddiv2med.Text;
			}
			set
			{
				iddiv2med.Text = value;
			}
		}

		public string Piddiv3med
		{
			get
			{
				return iddiv3med.Text;
			}
			set
			{
				iddiv3med.Text = value;
			}
		}

		public string Pdirmed
		{
			get
			{
				return dirmed.Text;
			}
			set
			{
				dirmed.Text = value;
			}
		}
		#endregion Propiedades
	}
}