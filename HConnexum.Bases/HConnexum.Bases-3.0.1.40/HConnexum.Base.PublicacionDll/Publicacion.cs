using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

namespace HConnexum.Base.ControlPublicacion
{
	public partial class Publicacion : UserControl
	{
		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			this.lblFechaValidez = (Label)this.FindControl(@"lblFechaValidez");
			this.rdpFechaValidez = (RadDatePicker)this.FindControl(@"rdpFechaValidez");
			this.rfvtxtFechaValidez = (RequiredFieldValidator)this.FindControl(@"rfvtxtFechaValidez");
			this.lblIndVigente = (Label)this.FindControl(@"lblIndVigente");
			this.chkIndVigente = (CheckBox)this.FindControl(@"chkIndVigente");
			this.ltrCollapsedPublicacion = (Literal)this.FindControl(@"ltrCollapsedPublicacion");
		}
		
		#region CodeBehind
		
		public string FechaValidez
		{
			get
			{
				return this.rdpFechaValidez.SelectedDate.ToString();
			}
			set
			{
				this.rdpFechaValidez.DbSelectedDate = ExtensionesString.ConvertirFecha(value);
			}
		}
		
		public string IndVigente
		{
			get
			{
				return this.chkIndVigente.Checked.ToString();
			}
			set
			{
				this.chkIndVigente.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		
		public enum EnumSpeed
		{
			Lento,
			Normal,
			Rapido
		}
		
		private bool collapsed = true;
		private bool enabled = true;
		private EnumSpeed speed = EnumSpeed.Normal;
		private bool animation = true;
		
		[DefaultValue(false)]
		public bool Collapsed
		{
			get
			{
				return this.collapsed;
			}
			set
			{
				this.collapsed = value;
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
		
		[DefaultValue(EnumSpeed.Normal)]
		public EnumSpeed Speed
		{
			get
			{
				return this.speed;
			}
			set
			{
				this.speed = value;
			}
		}
		
		[DefaultValue(true)]
		public bool Animation
		{
			get
			{
				return this.animation;
			}
			set
			{
				this.animation = value;
			}
		}
		
		protected void Page_Init(object sender, EventArgs e)
		{
			this.ltrCollapsedPublicacion.Text = this.JSHabilitarControles();
			this.HabilitarControles();
			chkIndVigente.Attributes.Add(@"onclick", "ActivarValidator(\"" + rfvtxtFechaValidez.ClientID + "\", this.checked);");
		}
		
		protected void Page_PreRender(object sender, EventArgs e)
		{
			this.rfvtxtFechaValidez.Enabled = this.chkIndVigente.Checked;
			if (this.rdpFechaValidez.SelectedDate != null && this.rdpFechaValidez.SelectedDate.Value < DateTime.Now)
				this.rdpFechaValidez.MinDate = this.rdpFechaValidez.SelectedDate.Value;
			else
				this.rdpFechaValidez.MinDate = DateTime.Now;
		}
		
		private string JSHabilitarControles()
		{
			string sProperty = string.Empty;
			if (this.Collapsed)
				sProperty = @"collapsed:true";
			if (!this.Animation)
			{
				if (sProperty == string.Empty)
					sProperty += @"animation:false";
				else
					sProperty += @",animation:false";
			}
			if (this.Speed != EnumSpeed.Normal)
			{
				if (sProperty == string.Empty)
					sProperty += this.Speed == EnumSpeed.Rapido ? "speed:\"fast\"" : "speed:\"slow\"";
				else
					sProperty += this.Speed == EnumSpeed.Rapido ? ",speed:\"fast\"" : ",speed:\"slow\"";
			}
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<script type=\"text/javascript\">");
			sb.AppendLine("$('#fieldsetPublicacion').coolfieldset({" + sProperty + "});");
			sb.AppendLine(@"</script>");
			return sb.ToString();
		}
		
		private void HabilitarControles()
		{
			this.rdpFechaValidez.Enabled = this.Enabled;
			this.chkIndVigente.Enabled = this.Enabled;
		}
		
		#endregion
		
		#region Miembros
		
		/// <summary>
		/// lblFechaValidez control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Label lblFechaValidez;
		
		/// <summary>
		/// rdpFechaValidez control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::Telerik.Web.UI.RadDatePicker rdpFechaValidez;
		
		/// <summary>
		/// rfvtxtFechaValidez control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.RequiredFieldValidator rfvtxtFechaValidez;
		
		/// <summary>
		/// lblIndVigente control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Label lblIndVigente;
		
		/// <summary>
		/// chkIndVigente control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.CheckBox chkIndVigente;
		
		/// <summary>
		/// ltrCollapsedPublicacion control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Literal ltrCollapsedPublicacion;
		#endregion
	}
}
