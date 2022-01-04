using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Vista
{
	public class ArbolPaginaSiteMap : ArbolPagina
	{
		public ArbolPaginaSiteMap() : base()
		{
		}
		
		public ArbolPaginaSiteMap(int id, int idParent, string nombre, string url, bool last) : base()
		{
			this.Id = id;
			this.IdParent = idParent;
			int length = int.Parse(ConfigurationManager.AppSettings[@"truncateLength"]);
			
			if (this.CountNiveles() > int.Parse(ConfigurationManager.AppSettings[@"truncateNivel"]) && nombre.Length > length && !last)
				this.Nombre = nombre.Substring(0, length) + @"...";
			else
				this.Nombre = nombre;
			
			this.NombreCompleto = nombre;
			this.Url = url;
			this.Ultima = last;
		}
		
		public int Id { get; set; }
		public int IdParent { get; set; }
		public string NombreCompleto { get; set; }
		public bool Ultima { get; set; }
		
		private IList<ArbolPagina> ArbolPaginas
		{
			get
			{
				if (HttpContext.Current.Session[@"ArbolPaginas"] != null)
					return (IList<ArbolPagina>)HttpContext.Current.Session[@"ArbolPaginas"];
				
				return new List<ArbolPagina>();
			}
			set
			{
				HttpContext.Current.Session[@"ArbolPaginas"] = value;
			}
		}
		
		private int IdMenu
		{
			get
			{
				string idMenu = this.ExtraerDeViewStateOQueryString(@"IdMenu");
				
				if (!string.IsNullOrEmpty(idMenu))
					return int.Parse(idMenu);
				
				return 0;
			}
		}
		
		public int CountNiveles()
		{
			int cantidad = 0;
			ArbolPagina arbolPagina = this.ArbolPaginas.SingleOrDefault(ap => ap.IdMaster == this.IdMenu);
			this.ContarNiveles(arbolPagina, ref cantidad);
			return cantidad;
		}
		
		public IList<ArbolPaginaSiteMap> ObtenerSiteMap()
		{
			ArbolPagina arbolPagina = this.ArbolPaginas.SingleOrDefault(ap => ap.IdMaster == this.IdMenu);
			IList<ArbolPaginaSiteMap> sitemap = new List<ArbolPaginaSiteMap>();
			this.ConstruirSiteMap(0, arbolPagina, ref sitemap);
			return sitemap;
		}
		
		private int ContarNiveles(ArbolPagina arbolPagina, ref int cantidad)
		{
			cantidad++;
			
			if (arbolPagina.Hija != null)
				this.ContarNiveles(arbolPagina.Hija, ref cantidad);
			
			return cantidad;
		}
		
		private void ConstruirSiteMap(int x, ArbolPagina pagina, ref IList<ArbolPaginaSiteMap> sitemap)
		{
			sitemap.Add(new ArbolPaginaSiteMap(x + 1, x, pagina.Nombre, pagina.Url, pagina.Hija == null));
			
			if (pagina.Hija != null)
				this.ConstruirSiteMap(x++, pagina.Hija, ref sitemap);
		}
		
		/// <summary>Extra una propiedad del ViewState o de un QueryString.</summary>
		/// <param name="propiedad">String. Nombre de la propiedad que se desea extraer.</param>
		/// <returns>String. Valor de la propiedad extraida.</returns>
		private string ExtraerDeViewStateOQueryString(string propiedad)
		{
			string valor = string.Empty;
			
			if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[propiedad]))
				valor = System.Text.Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(HttpContext.Current.Request.QueryString[propiedad])).Desencriptar();
			
			return valor;
		}
	}
}