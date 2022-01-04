using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Vista
{
	public static class IlistArbolPaginaExtensions
	{
		public static ArbolPagina ObtenerArbolPaginaNodoPorIdMenu(this IList<ArbolPagina> listadoArbolPagina, int idMenu)
		{
			return listadoArbolPagina.SingleOrDefault(ap => ap.IdMaster == idMenu);
		}
		
		public static ArbolPagina ObtenerArbolPaginaActual(this IList<ArbolPagina> listadoArbolPagina)
		{
			string sIdMenu = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"IdMenu");
			
			if (!string.IsNullOrEmpty(sIdMenu))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
				ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				return ObtenerArbolPaginaActual(arbolPagina);
			}
			return null;
		}
		
		public static ArbolPagina ObtenerArbolPaginaPadre(this IList<ArbolPagina> listadoArbolPagina)
		{
			string sIdMenu = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"IdMenu");
			
			if (!string.IsNullOrEmpty(sIdMenu))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
				ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				
				if (arbolPagina.Url.Split('?')[0] == HttpContext.Current.Request.Url.PathAndQuery.Split('?')[0])
					return null;
				else if (arbolPagina.Hija != null)
					return ObtenerArbolPaginaPadre(arbolPagina);
				else
					return arbolPagina;
			}
			return null;
		}
		
		public static ArbolPagina ObtenerArbolPaginaPadre(this IList<ArbolPagina> listadoArbolPagina, ArbolPagina arbolPaginaActual)
		{
			string sIdMenu = HttpUtility.ParseQueryString(new Uri(@"http:/" + arbolPaginaActual.UrlRedirect).Query).Get(@"IdMenu");
			
			if (!string.IsNullOrEmpty(sIdMenu))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
				ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				
				if (arbolPagina.Url == arbolPaginaActual.Url)
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
		
		public static ArbolPagina ObtenerArbolPaginaPadre(this IList<ArbolPagina> listadoArbolPagina, int cantidadPadre)
		{
			ArbolPagina arbolPagina = listadoArbolPagina.ObtenerArbolPaginaPadre();
			
			for (int i = 0; i < cantidadPadre; i++)
			{
				arbolPagina = ObtenerArbolPaginaPadre(listadoArbolPagina, arbolPagina);
			}
			return arbolPagina;
		}
		
		public static bool ArbolPaginaActualIsNode(this IList<ArbolPagina> listadoArbolPagina)
		{
			string sIdMenu = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"IdMenu");
			string bInterventor = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query).Get(@"bInterventor");
			
			if (!string.IsNullOrEmpty(sIdMenu))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(sIdMenu)).Desencriptar());
				ArbolPagina pagina = listadoArbolPagina.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				
				if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[@"RadUrid"]))
				{
					if (pagina.Url.Split('?')[0] == HttpContext.Current.Request.Url.PathAndQuery.Split('?')[0])
						return false;
				}
				else
				{
					Uri uri = new Uri(@"http:/" + pagina.Url);
					string urlActual = HttpContext.Current.Request.ApplicationPath.Length == 1 ? HttpContext.Current.Request.Url.AbsolutePath : HttpContext.Current.Request.Url.AbsolutePath.Remove(HttpContext.Current.Request.Url.AbsolutePath.IndexOf(HttpContext.Current.Request.ApplicationPath), HttpContext.Current.Request.ApplicationPath.Length);
					
					if (uri.AbsolutePath == urlActual)
						return false;
				}
			}
			if (!string.IsNullOrEmpty(bInterventor))
				return false;
			
			return true;
		}
		
		private static ArbolPagina ObtenerArbolPaginaActual(ArbolPagina arbolPagina)
		{
			if (arbolPagina.Url.Split('?')[0] == HttpContext.Current.Request.Url.PathAndQuery.Split('?')[0])
				return arbolPagina;
			else if (arbolPagina.Hija == null)
				return null;
			
			return ObtenerArbolPaginaActual(arbolPagina.Hija);
		}
		
		private static ArbolPagina ObtenerArbolPaginaPadre(ArbolPagina arbolPagina)
		{
			if (arbolPagina.Hija == null || arbolPagina.Hija.Url.Split('?')[0] == HttpContext.Current.Request.Url.PathAndQuery.Split('?')[0])
				return arbolPagina;
			
			return ObtenerArbolPaginaPadre(arbolPagina.Hija);
		}
	}
}