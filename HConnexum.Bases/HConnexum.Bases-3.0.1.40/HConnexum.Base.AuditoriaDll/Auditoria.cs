using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;

namespace HConnexum.Base.ControlAuditoria
{
	public partial class Auditoria : UserControl
	{
		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			this.lblCreadoPor = (Label)this.FindControl(@"lblCreadoPor");
			this.txtCreadoPor = (TextBox)this.FindControl(@"txtCreadoPor");
			this.hdnCreadoPor = (HiddenField)this.FindControl(@"hdnCreadoPor");
			this.lblFechaCreacion = (Label)this.FindControl(@"lblFechaCreacion");
			this.txtFechaCreacion = (TextBox)this.FindControl(@"txtFechaCreacion");
			this.lblModificadoPor = (Label)this.FindControl(@"lblModificadoPor");
			this.txtModificadoPor = (TextBox)this.FindControl(@"txtModificadoPor");
			this.lblFechaModificacion = (Label)this.FindControl(@"lblFechaModificacion");
			this.txtFechaModificacion = (TextBox)this.FindControl(@"txtFechaModificacion");
			this.lblIndEliminado = (Label)this.FindControl(@"lblIndEliminado");
			this.chkIndEliminado = (CheckBox)this.FindControl(@"chkIndEliminado");
			this.ltrCollapsedAuditoria = (Literal)this.FindControl(@"ltrCollapsedAuditoria");
		}
		
		#region CodeBehind
		
		public string IndEliminado
		{
			get
			{
				return this.chkIndEliminado.Checked.ToString();
			}
			set
			{
				this.chkIndEliminado.Checked = ExtensionesString.ConvertirBoolean(value);
			}
		}
		
		public string CreadoPor
		{
			get
			{
				return this.txtCreadoPor.Text;
			}
			set
			{
				this.txtCreadoPor.Text = string.Format("{0:N0}", value);
			}
		}
		
		public string FechaCreacion
		{
			get
			{
				return this.txtFechaCreacion.Text;
			}
			set
			{
				this.txtFechaCreacion.Text = value;
			}
		}
		
		public string ModificadoPor
		{
			get
			{
				return this.txtModificadoPor.Text;
			}
			set
			{
				this.txtModificadoPor.Text = string.Format("{0:N0}", value);
			}
		}
		
		public string FechaModificacion
		{
			get
			{
				return this.txtFechaModificacion.Text;
			}
			set
			{
				this.txtFechaModificacion.Text = value;
			}
		}
		
		public enum EnumSpeed
		{
			Lento,
			Normal,
			Rapido
		}
		
		private bool collapsed = true;
		private bool enabled = false;
		private EnumSpeed speed = EnumSpeed.Normal;
		private bool animation = true;
		
		[DefaultValue(true)]
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
		
		[DefaultValue(false)]
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
			this.ltrCollapsedAuditoria.Text = this.JSHabilitarControlesBusqueda();
			this.HabilitarControles();
		}
		
		private string JSHabilitarControlesBusqueda()
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
			sb.AppendLine("$('#fieldsetAuditoria').coolfieldset({" + sProperty + "});");
			sb.AppendLine(@"</script>");
			return sb.ToString();
		}
		
		private void HabilitarControles()
		{
			this.txtCreadoPor.Enabled = this.Enabled;
			this.txtFechaCreacion.Enabled = this.Enabled;
			this.txtModificadoPor.Enabled = this.Enabled;
			this.txtFechaModificacion.Enabled = this.Enabled;
			this.chkIndEliminado.Enabled = this.Enabled;
		}
		
		#endregion
		
		#region Miembros
		
		/// <summary>
		/// lblCreadoPor control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Label lblCreadoPor;
		
		/// <summary>
		/// txtCreadoPor control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.TextBox txtCreadoPor;
		
		/// <summary>
		/// hdnCreadoPor control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.HiddenField hdnCreadoPor;
		
		/// <summary>
		/// lblFechaCreacion control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Label lblFechaCreacion;
		
		/// <summary>
		/// txtFechaCreacion control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.TextBox txtFechaCreacion;
		
		/// <summary>
		/// lblModificadoPor control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Label lblModificadoPor;
		
		/// <summary>
		/// txtModificadoPor control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.TextBox txtModificadoPor;
		
		/// <summary>
		/// lblFechaModificacion control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Label lblFechaModificacion;
		
		/// <summary>
		/// txtFechaModificacion control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.TextBox txtFechaModificacion;
		
		/// <summary>
		/// lblIndEliminado control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Label lblIndEliminado;
		
		/// <summary>
		/// chkIndEliminado control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.CheckBox chkIndEliminado;
		
		/// <summary>
		/// ltrCollapsedAuditoria control.
		/// </summary>
		/// <remarks>
		/// Auto-generated field.
		/// To modify move field declaration from designer file to code-behind file.
		/// </remarks>
		protected global::System.Web.UI.WebControls.Literal ltrCollapsedAuditoria;
		#endregion
	}
}
