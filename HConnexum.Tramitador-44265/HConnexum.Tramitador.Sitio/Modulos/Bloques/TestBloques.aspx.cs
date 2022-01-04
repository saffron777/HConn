using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class TestBloques : System.Web.UI.Page
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			if(!Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"JQ"))
				Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"JQ", this.ResolveClientUrl(@"~/Scripts/jquery-1.7.1.min.js"));
			if(!Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Util"))
				Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"Util", this.ResolveClientUrl(@"~/Scripts/Utilitarios.js"));
			if(!Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Mask"))
				Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"Mask", this.ResolveClientUrl(@"~/Scripts/jquery.maskedinput.js"));
			if(!Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Field"))
				Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"Field", this.ResolveClientUrl(@"~/Scripts/jquery.coolfieldset.js"));
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(Session[@"SkinGlobal"] != null)
				rsmGeneralMaster.Skin = Session[@"SkinGlobal"].ToString();
			else
				rsmGeneralMaster.Skin = ConfigurationManager.AppSettings["Telerik.Skin"];
		}

		protected void btnPost_Click(object sender, EventArgs e)
		{
		}
	}
}