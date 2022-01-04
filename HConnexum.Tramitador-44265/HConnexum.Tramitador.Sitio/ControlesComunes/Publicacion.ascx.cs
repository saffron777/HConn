using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using HConnexum.Infraestructura;
using System.Web.UI.HtmlControls;

namespace HConnexum.Tramitador.Sitio
{
	public partial class Publicacion : UserControl
	{
		public string FechaValidez
		{
			get { return rdpFechaValidez.SelectedDate.ToString(); }
			set { rdpFechaValidez.DbSelectedDate = ExtensionesString.ConvertirFecha(value); }
		}

		public string IndVigente
		{
			get { return chkIndVigente.Checked.ToString(); }
			set { chkIndVigente.Checked = ExtensionesString.ConvertirBoolean(value); }
		}

		public enum EnumSpeed
		{
			Lento,
			Normal,
			Rapido
		}

		private bool _Collapsed = true;
		private bool _Enabled = true;
		private EnumSpeed _Speed = EnumSpeed.Normal;
		private bool _Animation = true;

		[DefaultValue(false)]
		public bool Collapsed
		{
			get { return _Collapsed; }
			set { _Collapsed = value; }
		}

		[DefaultValue(true)]
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
			ltrCollapsedPublicacion.Text = JSHabilitarControles();
			HabilitarControles();
			chkIndVigente.Attributes.Add("onclick", "ActivarValidator(\"" + rfvtxtFechaValidez.ClientID + "\", this.checked);");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"CFS"))
				this.Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"CFS", this.ResolveClientUrl(@"~/Scripts/jquery.coolfieldset.js"));
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			rfvtxtFechaValidez.Enabled = chkIndVigente.Checked;
			if(rdpFechaValidez.SelectedDate != null && rdpFechaValidez.SelectedDate.Value < DateTime.Now)
					rdpFechaValidez.MinDate = rdpFechaValidez.SelectedDate.Value;
			else
				rdpFechaValidez.MinDate = DateTime.Now;
		}

		private string JSHabilitarControles()
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
			sb.AppendLine("	$('#fieldsetPublicacion').coolfieldset({" + sProperty + "});");
			sb.AppendLine(@"</script>");
			return sb.ToString();
		}

		private void HabilitarControles()
		{
			rdpFechaValidez.Enabled = Enabled;
			chkIndVigente.Enabled = Enabled;
		}
	}
}
