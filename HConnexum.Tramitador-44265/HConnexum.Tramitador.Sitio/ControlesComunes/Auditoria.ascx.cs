using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using HConnexum.Infraestructura;
using System.Web.UI.HtmlControls;

namespace HConnexum.Tramitador.Sitio
{
	public partial class Auditoria : UserControl
	{
		public string IndEliminado
		{
			get { return chkIndEliminado.Checked.ToString(); }
			set { chkIndEliminado.Checked = ExtensionesString.ConvertirBoolean(value); }
		}

		public string CreadoPor
		{
			get { return txtCreadoPor.Text; }
			set { txtCreadoPor.Text = string.Format("{0:N0}", value); }
		}

		public string FechaCreacion
		{
			get { return txtFechaCreacion.Text; }
			set { txtFechaCreacion.Text = value; }
		}

		public string ModificadoPor
		{
			get { return txtModificadoPor.Text; }
			set { txtModificadoPor.Text = string.Format("{0:N0}", value); }
		}

		public string FechaModificacion
		{
			get { return txtFechaModificacion.Text; }
			set { txtFechaModificacion.Text = value; }
		}

		public enum EnumSpeed
		{
			Lento,
			Normal,
			Rapido
		}

		private bool _Collapsed = true;
		private bool _Enabled = false;
		private EnumSpeed _Speed = EnumSpeed.Normal;
		private bool _Animation = true;

		[DefaultValue(true)]
		public bool Collapsed
		{
			get { return _Collapsed; }
			set { _Collapsed = value; }
		}

		[DefaultValue(false)]
		public bool Enabled
		{
			get { return _Enabled; }
			set { _Enabled = value; }
		}

		[DefaultValue(EnumSpeed.Normal)]
		public EnumSpeed Speed
		{
			get { return _Speed; }
			set { _Speed = value; }
		}

		[DefaultValue(true)]
		public bool Animation
		{
			get { return _Animation; }
			set { _Animation = value; }
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			HtmlLink link = new HtmlLink();
			link.Attributes.Add("href", this.Page.ResolveUrl("~/Temas/ccs/jquery.coolfieldset.css"));
			link.Attributes.Add("type", "text/css");
			link.Attributes.Add("rel", "stylesheet");
			this.Page.Header.Controls.Add(link);
			ltrCollapsedAuditoria.Text = JSHabilitarControlesBusqueda();
			HabilitarControles();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"CFS"))
				this.Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"CFS", this.ResolveClientUrl(@"~/Scripts/jquery.coolfieldset.js"));
		}

		private string JSHabilitarControlesBusqueda()
		{
			string sProperty = string.Empty;
			if(Collapsed)
				sProperty = @"collapsed:true";
			if(!Animation)
			{
				if(sProperty == string.Empty)
					sProperty += @"animation:false";
				else
					sProperty += @",animation:false";
			}
			if(Speed != EnumSpeed.Normal)
			{
				if(sProperty == string.Empty)
					sProperty += Speed == EnumSpeed.Rapido ? "speed:\"fast\"" : "speed:\"slow\"";
				else
					sProperty += Speed == EnumSpeed.Rapido ? ",speed:\"fast\"" : ",speed:\"slow\"";
			}
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<script type=\"text/javascript\">");
			sb.AppendLine("	$('#fieldsetAuditoria').coolfieldset({" + sProperty + "});");
			sb.AppendLine(@"</script>");
			return sb.ToString();
		}

		private void HabilitarControles()
		{
			txtCreadoPor.Enabled = Enabled;
			txtFechaCreacion.Enabled = Enabled;
			txtModificadoPor.Enabled = Enabled;
			txtFechaModificacion.Enabled = Enabled;
			chkIndEliminado.Enabled = Enabled;
		}
	}
}