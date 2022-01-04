using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using HConnexum.Infraestructura;

namespace HConnexum.Base.ControlAuditoria
{
	public partial class Auditoria : UserControl
	{
		private bool collapsed = true;
		private bool enabled = false;
		private EnumSpeed speed = EnumSpeed.Normal;
		private bool animation = true;
		
		public enum EnumSpeed
		{
			Lento,
			Normal,
			Rapido
		}
		
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
	}
}