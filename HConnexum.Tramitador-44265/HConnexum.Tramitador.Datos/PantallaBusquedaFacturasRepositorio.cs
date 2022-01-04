using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Servicios.Servicios;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: PantallaBusquedaFacturasRepositorio.</summary>	
	public sealed class PantallaBusquedaFacturasRepositorio : RepositorioBase<Suscriptor>
	{
		#region "ConstructoreS"
		
		///<summary>Constructor de la clase PantallaBusquedaFacturasRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public PantallaBusquedaFacturasRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		#endregion "Constructores"
		
		#region DTO
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableSuscriptorDTO.</returns>
		public IEnumerable<SuscriptorDTO> ObtenerDto(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabSuscriptor = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabDetalleS = this.udt.Sesion.CreateObjectSet<DetallesTiposSuscriptor>();
			var tabTipoS = this.udt.Sesion.CreateObjectSet<TiposSuscriptor>();
			var coleccion = from tab in tabSuscriptor
							join tabD in tabDetalleS on tab.Id equals tabD.IdSuscriptor
							join tabT in tabTipoS on tabD.IdTipoSuscriptor equals tabT.Id
							orderby "it." + orden
							select new SuscriptorDTO
							{
								NumDoc = tab.NumDoc,
								Nombre = tab.Nombre,
								Tipo = tabT.Nombre,
								IdTipo = tabT.Id,
								Id = tab.Id,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			
			coleccion = UtilidadesDTO<SuscriptorDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<SuscriptorDTO>.FiltrarColeccionEliminacion(coleccion, true);
			this.Conteo = UtilidadesDTO<SuscriptorDTO>.Conteo;
			return UtilidadesDTO<SuscriptorDTO>.EncriptarId(coleccion);
		}
		
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableCasoDTO.</returns>
		public IEnumerable<SuscriptorDTO> ObtenerSuscriptoresDTO()
		{
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from tab in tabSuscriptores  //orderby tab.Nombre
							select new SuscriptorDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre
							};
			coleccion = UtilidadesDTO<SuscriptorDTO>.FiltrarColeccionEliminacion(coleccion, false);
			return coleccion;
		}
		
		public IEnumerable<SuscriptorDTO> ObtenerSuscriptoresPorNombresDTO(string nombre)
		{
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from tab in tabSuscriptores  //orderby tab.Nombre
							where tab.Nombre.Contains(nombre)
							select new SuscriptorDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre
							};
			coleccion = UtilidadesDTO<SuscriptorDTO>.FiltrarColeccionEliminacion(coleccion, false);
			return coleccion;
		}
		
		public IEnumerable<SuscriptorDTO> ObtenerSuscriptorPorId(int id)
		{
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = from tab in tabSuscriptores
							where tab.Id == id
							select new SuscriptorDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre,
								CodigoExternoId = tab.CodIdExterno
							};
			coleccion = UtilidadesDTO<SuscriptorDTO>.FiltrarColeccionEliminacion(coleccion, false);
			return coleccion;
		}
		
		public string ObtenerNombreSuscriptorPorId(int id)
		{
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = (from tab in tabSuscriptores
							 where tab.Id == id
							 select new SuscriptorDTO
							 {
								 Nombre = tab.Nombre
							 }).SingleOrDefault();
			
			return coleccion.Nombre;
		}
		
		public SuscriptorDTO ObtenerSuscriptorporId_(int IdSuscriptor)
		{
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = (from tab in tabSuscriptores
							 where tab.Id == IdSuscriptor
							 select new SuscriptorDTO
							 {
								 Nombre = tab.Nombre
							 }).FirstOrDefault();
			return coleccion;
		}
		
		public DataTable ObtenerSuscriptorporIdCompleto_(int IdSuscriptor)
		{
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var coleccion = (from tab in tabSuscriptores
							 where tab.Id == IdSuscriptor
							 select new SuscriptorDTO
							 {
								 Id = tab.Id,
								 Nombre = tab.Nombre,
								 NumDoc = tab.NumDoc,
								 Logo = tab.Logo,
								 Fax = tab.Fax
							 });
			return LinqtoDataSetMethods.CopyToDataTable(coleccion);
		}
		
		/// <summary>
		///  Obtiene el Logo del Suscriptor
		/// </summary>
		/// <param name="id">Id de tipo INT</param>
		/// <param name="tipoId">Tipo de Id (IdSuscriptor - CodIndExterno)</param>
		/// <returns></returns>
		public string ObtenerLogo(int id, TipoId tipoId)
		{
			HttpContext.Current.Trace.Warn(String.Format("Se Inicia OntenerLogo(id= {0} , tipo= {1})", id, tipoId));
			var tabSuscriptores = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			
			string ssLogo = string.Empty;
			string idString = id.ToString();
			
			if (String.IsNullOrWhiteSpace(idString))
			{
				HttpContext.Current.Trace.Warn(String.Format("No se puedo parsear a string el id={0}", id));
				throw new Exception(String.Format("No se puedo parsear a string el id={0}", id));
			}
			
			string imagenDefault = ConfigurationManager.AppSettings[@"UrlImgReporteOpMed"].ToString();
			
			if (String.IsNullOrWhiteSpace(imagenDefault))
			{
				HttpContext.Current.Trace.Warn(String.Format("No se puedo cargar el logo por default url= {0}", ssLogo));
			}
			switch (tipoId)
			{
				case TipoId.IdSuscriptor:
					
					ssLogo = (from tab in tabSuscriptores
							  where tab.Id == id
							  select !string.IsNullOrEmpty(tab.Logo) ? tab.Logo : imagenDefault).DefaultIfEmpty(imagenDefault).SingleOrDefault();
					break;
				case TipoId.CodIndExterno:
					
					ssLogo = (from tab in tabSuscriptores
							  where tab.CodIdExterno == idString
							  select !string.IsNullOrEmpty(tab.Logo) ? tab.Logo : imagenDefault).DefaultIfEmpty(imagenDefault).SingleOrDefault();
					
					break;
				default:
					break;
			}
			HttpContext.Current.Trace.Warn(String.Format("Se retorna el logo  {0})", ssLogo));
			return ssLogo;
		}
		
		/// <summary>
		/// IdSuscriptor: parametro utilizado para cargar el logo
		/// CodIndExterno: parametro utilizado para cargar el logo a un reporte
		/// </summary>
		public enum TipoId
		{
			IdSuscriptor,
			CodIndExterno
		}
		
		/// <summary>
		///  Indica si el suscriptor es dueño de algun servicio
		/// </summary>
		/// <param name="idSuscriptor">idSuscriptor de tipo INT</param>
		/// <returns name="indDueño">indDueño de tipo bool</returns>
		public bool IndicarDuenoFlujoServicio(int idSuscriptor)
		{
			var tabFlujosServicio = this.udt.Sesion.CreateObjectSet<FlujosServicio>();
			bool indDueno = (from fs in tabFlujosServicio
							 where fs.IndVigente == true &&
								   fs.IndEliminado == false &&
								   fs.FechaValidez <= DateTime.Now
							 select fs.IdSuscriptor).Contains(idSuscriptor);
			return indDueno;
		}
		
		public DataSet ObtenerSuscriptorXIdTipoProveedorIntermediario(int idTipoProveedor, int idSuscriptor)
		{
			DataSet ds = new DataSet();
			var tabSuscriptor = this.udt.Sesion.CreateObjectSet<Suscriptor>();
			var tabDetallesTiposSuscriptor = this.udt.Sesion.CreateObjectSet<DetallesTiposSuscriptor>();
			var tabTiposSuscriptor = this.udt.Sesion.CreateObjectSet<TiposSuscriptor>();
			
			var suscriptores = (from S in tabSuscriptor
								join Tdts in tabDetallesTiposSuscriptor on S.Id equals Tdts.IdSuscriptor
								join Tts in tabTiposSuscriptor on Tdts.IdTipoSuscriptor equals Tts.Id
								where
									 Tts.Id == idTipoProveedor &&
									 Tdts.IdSuscriptor == idSuscriptor &&
									 Tts.IndVigente == true &&
									 Tts.IndEliminado == false &&
									 Tts.FechaValidez <= DateTime.Now
								select new
								{
									Id = S.Id,
									Nombre = S.Nombre
									,
								});
			ds.Tables.Add(LinqtoDataSetMethods.CopyToDataTable(suscriptores));
			return ds;
		}
		
		public DataSet IndicarExisteProveedorReporteFactura(int idSuscriptor)
		{
			DataSet ds = new DataSet();
			var tabLista = this.udt.Sesion.CreateObjectSet<Lista>();
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var coleccionlista = (from l in tabLista
								  join lv in tabListaValor on l.Id equals lv.IdLista
								  where l.Id == 97 &&
										l.IndVigente == true && l.IndEliminado == false && l.FechaValidez <= DateTime.Now &&
										lv.IndVigente == true && lv.IndEliminado == false && lv.FechaValidez <= DateTime.Now
								  select new
								  {
									  lv.Valor
								  });
			
			DataSet dslista = new DataSet();
			dslista.Tables.Add(LinqtoDataSetMethods.CopyToDataTable(coleccionlista));
			if (dslista.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in dslista.Tables[0].Rows)
				{
					string valor = Convert.ToString(row[0]);
					int valorlista = int.Parse(valor);
					
					var tabDetallesTiposSuscriptor = this.udt.Sesion.CreateObjectSet<DetallesTiposSuscriptor>();
					var tabUsuariosSuscriptor = this.udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
					var coleccion = (from dts in tabDetallesTiposSuscriptor
									 join us in tabUsuariosSuscriptor on dts.IdSuscriptor equals us.IdSuscriptor
									 where dts.IdTipoSuscriptor == valorlista && us.IdSuscriptor == idSuscriptor &&
										   dts.IndVigente == true && dts.IndEliminado == false && dts.FechaValidez <= DateTime.Now &&
										   us.IndVigente == true && us.IndEliminado == false && us.FechaValidez <= DateTime.Now
									 select new
									 {
										 us.IdSuscriptor
									 });
					
					var coleccion2 = (from dts in tabDetallesTiposSuscriptor
									  where dts.IdTipoSuscriptor == 7 && dts.IdSuscriptor == idSuscriptor &&
											dts.IndVigente == true && dts.IndEliminado == false && dts.FechaValidez <= DateTime.Now
									  select new
									  {
										  dts.IdSuscriptor
									  });
					
					if (coleccion.Count() > 0 && coleccion2.Count() == 0)
					{
						ds.Tables.Add(LinqtoDataSetMethods.CopyToDataTable(coleccion));
					}
					else
					{
						ds.Tables.Add();
					}
				}
			}
			return ds;
		}
		#endregion DTO
	}
}
