using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HConnexum.Tramitador.Sitio
{
	public partial class Telefono : UserControl
	{
		[RefreshPropertiesAttribute(RefreshProperties.Repaint)]
		public Unit Width
		{
			set { this.txtNumero.Width = value; }
			get { return this.txtNumero.Width; }
		}

		public string CodigoPais
		{
			get
			{
				return this.txtCodPais.Text;
			}
			set
			{
				this.txtCodPais.Text = value;
			}
		}

		private string CodigoPaisInt
		{
			get
			{
				return this.txtCodPais.Text;
			}
			set
			{
				this.txtCodPais.Text = value.Trim();
			}
		}

		public string CodigoArea
		{
			get
			{
				return this.txtCodArea.Text;
			}
			set
			{
				this.txtCodArea.Text = value.Trim();
			}
		}

		public string Numero
		{
			get
			{
				return this.txtNumero.Text;
			}
			set
			{
				this.txtNumero.Text = value.Trim();
			}
		}

		public string NumeroTlfCompleto
		{
			get
			{
				return this.CodigoPais + this.CodigoArea + this.Numero;
			}
		}

		public string NumeroTlfCompletoMas
		{
			get
			{
				return @"+" + this.CodigoPais + this.CodigoArea + this.Numero;
			}
		}

		public string NumeroTlfCompletoGuion
		{
			get
			{
				return this.CodigoPais + @"-" + this.CodigoArea + @"-" + this.Numero;
			}
			set
			{
				this.Numero = this.CodigoArea = string.Empty;
				string[] numeroTlf = value.Split('-');
				if(numeroTlf.Length == 3)
				{
					this.CodigoPaisInt = numeroTlf[0];
					this.CodigoArea = numeroTlf[1];
					this.Numero = numeroTlf[2];
				}
			}
		}

		private int maxLengthCodigoPais = 4;
		private int maxLengthCodigoArea = 4;
		private int maxLengthNumero = 10;
		private bool enabled = true;
		private string pais = @"venezuela";
		private string defaultPais = @"venezuela";
		private bool isRequired = false;
		private string errorMessage = @"";
		private string validationGroup = @"";

		[DefaultValue(4)]
		public int MaxLengthCodigoPais
		{
			get
			{
				return this.maxLengthCodigoPais;
			}
			set
			{
				this.maxLengthCodigoPais = value;
			}
		}

		[DefaultValue(4)]
		public int MaxLengthCodigoArea
		{
			get
			{
				return this.maxLengthCodigoArea;
			}
			set
			{
				this.maxLengthCodigoArea = value;
			}
		}

		[DefaultValue(10)]
		public int MaxLengthNumero
		{
			get
			{
				return this.maxLengthNumero;
			}
			set
			{
				this.maxLengthNumero = value;
			}
		}

		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				txtNumero.Enabled = txtCodArea.Enabled = value;
			}
		}

		[DefaultValue(@"venezuela")]
		public string Pais
		{
			get
			{
				return this.pais;
			}
			set
			{
				this.pais = value;
				this.CargarConfiguracionPais();
			}
		}

		[DefaultValue(false)]
		public bool IsRequired
		{
			get
			{
				return this.isRequired;
			}
			set
			{
				this.isRequired = value;
			}
		}

		[DefaultValue(@"")]
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.errorMessage = value;
			}
		}

		[DefaultValue(@"")]
		public string ValidationGroup
		{
			get
			{
				return this.validationGroup;
			}
			set
			{
				this.validationGroup = value;
			}
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			this.CargarConfiguracionPais();
			this.rimTelefono.InputSettings[0].Validation.ValidationGroup = this.ValidationGroup;
			this.rimTelefono.InputSettings[0].Validation.IsRequired = this.IsRequired;
			this.rimTelefono.InputSettings[0].ErrorMessage = this.ErrorMessage;
			this.MaxLengthControles();
			this.HabilitarControles();
			this.txtCodArea.Attributes.Add(@"onkeypress", @"return SoloNumeros(event)");
			this.txtNumero.Attributes.Add(@"onkeypress", @"return SoloNumeros(event)");
		}

		private void CargarConfiguracionPais()
		{
			this.CodigoPaisInt = (GetGlobalResourceObject("Tramitador", this.Pais.ToLower().Replace(' ', '_')) != null ? GetGlobalResourceObject("Tramitador", this.Pais.ToLower().Replace(' ', '_')).ToString() : ConfigurationManager.AppSettings[this.defaultPais].ToString());
		}

		private void MaxLengthControles()
		{
			this.txtCodPais.MaxLength = this.MaxLengthCodigoPais;
			this.txtCodArea.MaxLength = this.MaxLengthCodigoArea;
			this.txtNumero.MaxLength = this.MaxLengthNumero;
		}

		private void HabilitarControles()
		{
			this.txtCodArea.Enabled = this.Enabled;
			this.txtNumero.Enabled = this.Enabled;
		}
	}
}