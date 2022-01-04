using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using System.Configuration;

namespace HConnexum.Tramitador.Sitio
{
	public partial class Site : MasterPage
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			if(!Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"JQ"))
				Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"JQ", this.ResolveClientUrl(@"~/Scripts/jquery-1.7.1.min.js"));
			if(!Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Util"))
				Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"Util", this.ResolveClientUrl(@"~/Scripts/Utilitarios.js"));
			if(!Page.ClientScript.IsClientScriptBlockRegistered(typeof(string), @"Mask"))
				Page.ClientScript.RegisterClientScriptInclude(typeof(string), @"Mask", this.ResolveClientUrl(@"~/Scripts/jquery.maskedinput.js"));
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(Session[@"SkinGlobal"] != null)
				rsmGeneralMaster.Skin = Session[@"SkinGlobal"].ToString();
			else
				rsmGeneralMaster.Skin = ConfigurationManager.AppSettings["Telerik.Skin"];
		}

		protected void rsmMigas_NodeDataBound(object sender, Telerik.Web.UI.RadSiteMapNodeEventArgs e)
		{
			ArbolPaginaSiteMap arbolPagina = (ArbolPaginaSiteMap)e.Node.DataItem;
			e.Node.ToolTip = arbolPagina.NombreCompleto;
			if(arbolPagina.Ultima)
				e.Node.Enabled = false;
		}
	}
}