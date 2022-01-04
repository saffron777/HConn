using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Sitio
{
	public static class NavegacionArbolPagina
	{
		public static ArbolPagina ObtenerArbolPaginaNodoPorIdMenu(this List<ArbolPagina> listadoArbolPagina, int idMenu)
		{
			return listadoArbolPagina.SingleOrDefault(ap => ap.IdMaster == idMenu);
		}

		public static ArbolPagina ObtenerArbolPaginaActual(this List<ArbolPagina> listadoArbolPagina)
		{
			string sIdMenu = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"IdMenu");
			if(!string.IsNullOrEmpty(sIdMenu))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
				ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				return ObtenerArbolPaginaActual(arbolPagina);
			}
			return null;
		}

		private static ArbolPagina ObtenerArbolPaginaActual(ArbolPagina arbolPagina)
		{
			if(arbolPagina.Url == HttpContext.Current.Request.Url.PathAndQuery)
				return arbolPagina;
			else if(arbolPagina.Hija == null)
				return null;
			return ObtenerArbolPaginaActual(arbolPagina.Hija);
		}

		public static ArbolPagina ObtenerArbolPaginaPadre(this List<ArbolPagina> listadoArbolPagina)
		{
			string sIdMenu = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"IdMenu");
			if(!string.IsNullOrEmpty(sIdMenu))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
				ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				if(arbolPagina.Url == HttpContext.Current.Request.Url.PathAndQuery)
					return null;
				else if(arbolPagina.Hija != null)
					return ObtenerArbolPaginaPadre(arbolPagina);
				else
					return arbolPagina;
			}
			return null;
		}

        public static ArbolPagina ObtenerArbolPaginaPadre(this List<ArbolPagina> listadoArbolPagina, ArbolPagina arbolPaginaActual)
        {
            string sIdMenu = HttpUtility.ParseQueryString(new Uri(@"http:/" + arbolPaginaActual.UrlRedirect).Query).Get(@"IdMenu");
            if (!string.IsNullOrEmpty(sIdMenu))
            {
                int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
                ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
                if(arbolPagina.Url == arbolPaginaActual.Url)
                    return null;
                if (arbolPagina.Hija != null && arbolPagina.Hija.Url == arbolPaginaActual.Url)
                    return arbolPagina;
                else if (arbolPagina.Hija != null)
                    return ObtenerArbolPaginaPadre(arbolPagina);
                else
                    return arbolPagina;
            }
            return null;
        }

        public static ArbolPagina ObtenerArbolPaginaPadre(this List<ArbolPagina> listadoArbolPagina, int CantidadPadre)
        {
            ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaPadre();
            for (int i = 0; i < CantidadPadre; i++)
                arbolPagina = ObtenerArbolPaginaPadre(listadoArbolPagina, arbolPagina);
            return arbolPagina;
        }

		private static ArbolPagina ObtenerArbolPaginaPadre(ArbolPagina arbolPagina)
		{
			if(arbolPagina.Hija == null || arbolPagina.Hija.Url == HttpContext.Current.Request.Url.PathAndQuery)
				return arbolPagina;
			return ObtenerArbolPaginaPadre(arbolPagina.Hija);
		}

		public static bool ArbolPaginaActualIsNode(this List<ArbolPagina> listadoArbolPagina)
		{
			string sIdMenu = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"IdMenu");
			string bInterventor = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"bInterventor");
			if(!string.IsNullOrEmpty(sIdMenu))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
				ArbolPagina pagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				if(string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["RadUrid"]))
				{
					if(pagina.Url == HttpContext.Current.Request.Url.PathAndQuery)
						return false;
				}
				else
				{
					Uri uri = new Uri(@"http:/" + pagina.Url);
					string urlActual = HttpContext.Current.Request.ApplicationPath.Length == 1 ? HttpContext.Current.Request.Url.AbsolutePath : HttpContext.Current.Request.Url.AbsolutePath.Remove(HttpContext.Current.Request.Url.AbsolutePath.IndexOf(HttpContext.Current.Request.ApplicationPath), HttpContext.Current.Request.ApplicationPath.Length);
					if(uri.AbsolutePath == urlActual)
						return false;
				}
			}
			if(!string.IsNullOrEmpty(bInterventor))
				return false;

			return true;
		}
	}
}