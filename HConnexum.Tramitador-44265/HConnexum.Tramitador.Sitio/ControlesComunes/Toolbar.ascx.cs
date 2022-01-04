using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web.Security;
using Telerik.Web.UI;
using System.Configuration;
using System.Linq;

namespace HConnexum.Tramitador.Sitio.ControlesComunes
{
	public partial class Toolbar : System.Web.UI.UserControl
	{
		private string _MenuTextField = @"Text";
		private string _MenuValueField = @"Value";
		private string _MenuFieldID = @"ID";
		private string _MenuFieldParentID = @"ParentID";

		[DefaultValue(@"Text")]
		public string MenuTextField
		{
			get { return _MenuTextField; }
			set { _MenuTextField = value; }
		}

		[DefaultValue(@"Value")]
		public string MenuValueField
		{
			get { return _MenuValueField; }
			set { _MenuValueField = value; }
		}

		[DefaultValue(@"ID")]
		public string MenuFieldID
		{
			get { return _MenuFieldID; }
			set { _MenuFieldID = value; }
		}

		[DefaultValue(@"ParentID")]
		public string MenuFieldParentID
		{
			get { return _MenuFieldParentID; }
			set { _MenuFieldParentID = value; }
		}

		public DataTable MenuDataSource
		{
			get { return ((Menu)this.rtbMenu.Items[0].Controls[1]).MenuDataSource; }
			set { ((Menu)this.rtbMenu.Items[0].Controls[1]).MenuDataSource = value; }
		}

		/// <summary>
		/// Esté método Carga a partir de los parámetros o controles que tendrá contenido el toolbar
		/// </summary>
		/// <param name="sender">Referencia al Objeto que disapara el evento</param>
		/// <param name="e">Argumentos del Evento</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			((Menu)this.rtbMenu.Items[0].Controls[1]).MenuTextField = MenuTextField;
			((Menu)this.rtbMenu.Items[0].Controls[1]).MenuValueField = MenuValueField;
			((Menu)this.rtbMenu.Items[0].Controls[1]).MenuFieldID = MenuFieldID;
			((Menu)this.rtbMenu.Items[0].Controls[1]).MenuFieldParentID = MenuFieldParentID;
			((Menu)this.rtbMenu.Items[0].Controls[1]).MenuDataSource = MenuDataSource;
			((Menu)this.rtbMenu.Items[0].Controls[1]).MenuDataBind();
			if(!IsPostBack)
			{
				var list = (from d in Directory.GetDirectories(Server.MapPath("~/app_themes"))
							select Path.GetFileName(d)).ToList();
				RadComboBox combo = ((RadComboBox)this.rtbMenu.Items[3].Controls[1]);
				combo.DataSource = list;
				combo.DataBind();
				if(combo != null)
					if(combo.Items.Count > 0)
					{
						if(Session[@"UsuarioTema"] == null)
							Session[@"UsuarioTema"] = ConfigurationManager.AppSettings["AplicacionTema"];
						string tema = Session[@"UsuarioTema"].ToString();
						if(tema != null)
							combo.Items.FindItemByText(tema).Selected = true;
						if(Session[@"SkinGlobal"] == null)
							Session[@"SkinGlobal"] = ConfigurationManager.AppSettings["Telerik.Skin"];
						string skin = Session[@"SkinGlobal"].ToString();
						if(skin != null)
							((RadSkinManager)this.rtbMenu.Items[2].Controls[1]).Skin = skin;
					}
			}
		}

		protected void ddlRadCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				RadComboBox combo = ((RadComboBox)sender);
				if(combo != null)
					if(combo.Items.Count > 0)
					{
						Session[@"UsuarioTema"] = combo.SelectedValue;
						Session[@"SkinGlobal"] = ObtenerSkin(combo.SelectedValue);
						((RadSkinManager)this.rtbMenu.Items[2].Controls[1]).Skin = Session[@"SkinGlobal"].ToString();
					}
			}
			catch(Exception ex)
			{
				this.Trace.Warn("Error", ex.ToString(), ex);
			}
		}

		protected string ObtenerSkin(string tema)
		{
			try
			{
				string skin = string.Empty;
				switch(tema)
				{
					case "HConnexum": skin = "Office2010Blue"; break;
					case "HConnexumBlack": skin = "Office2010Black"; break;
					case "HConnexumSilver": skin = "Office2010Silver"; break;
				}
				return skin;
			}
			catch(Exception ex)
			{
				this.Trace.Warn("Error", ex.ToString(), ex);
			}
			return null;
		}
	}
}