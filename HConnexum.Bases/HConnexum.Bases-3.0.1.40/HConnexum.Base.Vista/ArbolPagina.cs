using System;
using System.Linq;
using System.Web;

namespace HConnexum.Base.Vista
{
	[Serializable]
	public class ArbolPagina
	{
		public ArbolPagina()
		{
		}
		
		public ArbolPagina(int idMaster, string url, string nombre, string nombreTabla)
		{
			this.IdMaster = idMaster;
			this.Url = url;
			this.Nombre = nombre;
			this.NombreTabla = nombreTabla;
		}
		
		public int IdMaster { get; set; }
		public string Url { get; set; }
		public string Nombre { get; set; }
		public string NombreTabla { get; set; }
		public ArbolPagina Hija { get; set; }
		
		public string UrlRedirect
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Url))
					return HttpContext.Current.Request.ApplicationPath.Length == 1 ? this.Url : this.Url.Remove(this.Url.IndexOf(HttpContext.Current.Request.ApplicationPath), HttpContext.Current.Request.ApplicationPath.Length);
				
				return string.Empty;
			}
		}
	}
}