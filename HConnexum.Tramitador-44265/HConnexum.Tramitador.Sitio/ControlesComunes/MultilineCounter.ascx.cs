using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HConnexum.Tramitador.Sitio
{
	public partial class MultilineCounter : UserControl
	{
		public string Text
		{
			get
			{
				return this.txtControl.Text;
			}
			set
			{
				this.txtControl.Text = (string.Empty + value).Trim();
				this.MaxLength -= (string.Empty + value).Length;
			}
		}

		private int maxLength = 100;
		private bool enabled = true;
		private TextBoxMode textMode = TextBoxMode.MultiLine;
		private int width = 100;
		private int rows = 3;
		private bool isRequired = false;
		private string errorMessage = @"";
		private string validationGroup = @"";
		private string emptyMessage = @"";

		[DefaultValue(100)]
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				this.maxLength = value;
				this.lblCounter.Text = (string.Empty + value).ToString();
			}
		}

		[DefaultValue(TextBoxMode.SingleLine)]
		public TextBoxMode TextMode
		{
			get
			{
				return this.textMode;
			}
			set
			{
				this.textMode = value;
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
			}
		}

		[DefaultValue(100)]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		[DefaultValue(3)]
		public int Rows
		{
			get
			{
				return this.rows;
			}
			set
			{
				this.rows = value;
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
		public string EmptyMessage
		{
			get
			{
				return this.emptyMessage;
			}
			set
			{
				this.emptyMessage = value;
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
			this.txtControl.TextMode = this.TextMode;
			this.txtControl.MaxLength = this.MaxLength;
			this.txtControl.Enabled = this.Enabled;
			this.txtControl.Width = this.Width;
			this.txtControl.Rows = this.Rows;
            this.txtControl.Attributes.Add(@"onKeyDown", @"textCounter('" + this.txtControl.ID + "', '" + this.lblCounter.ID + "', " + this.maxLength + ");");
            this.txtControl.Attributes.Add(@"onKeyUp", @"textCounter('" + this.txtControl.ID + "', '" + this.lblCounter.ID + "', " + this.maxLength + ");");
            this.rimMultilineCounter.InputSettings[0].Validation.ValidationGroup = this.ValidationGroup;
			this.rimMultilineCounter.InputSettings[0].Validation.IsRequired = this.IsRequired;
			this.rimMultilineCounter.InputSettings[0].ErrorMessage = this.ErrorMessage;
			this.rimMultilineCounter.InputSettings[0].EmptyMessage = this.EmptyMessage;
			this.txtControl.Attributes.Add(@"onkeypress", @"return SinCaracteresEspeciales(event);");
			this.txtControl.Attributes.Add(@"onblur", @"SinEspaciosPrincipioFin(this);");
		}
	}
}