using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;

namespace HConnexum.Base.ControlPublicacion
{
	public class UserControl : System.Web.UI.UserControl
	{
		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			string content = String.Empty;
			Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(this.GetType(), this.GetType().Name + @".ascx");
			using (StreamReader reader = new StreamReader(stream))
			{
				content = reader.ReadToEnd();
			}
			Control userControl = this.Page.ParseControl(content);
			this.Controls.Add(userControl);
		}
	}
}