using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: ListasValorRepositorio.</summary>	
	public sealed class ListasValorRepositorio : RepositorioBase<ListaValor>
	{
		
		#region "Constructores"
		
		///<summary>Constructor de la clase ListasValorRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public ListasValorRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}

		#endregion "Constructores"

		#region DTO
		
		/// <summary>
		/// Este método devuelve los valores de lista, a partir del nombre de la lista maestra
		/// </summary>
		/// <param name="nombreLista">Nombre de la lista</param>
		/// <returns>Lista de valores con los atributos Id, NombreValor, NombreValorCorto</returns>
		public IEnumerable<ListasValorDTO> ObtenerDTOByNombreLista(string nombreLista)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabListas = this.udt.Sesion.CreateObjectSet<Lista>();
			var coleccion = from tab in tabListasValor
							join tabL in tabListas on tab.IdLista equals tabL.Id
							where tabL.Nombre == nombreLista
							orderby tab.Posicion
							select new ListasValorDTO
							{
								Id = tab.Id,
								NombreValor = tab.NombreValor,
								NombreValorCorto = tab.NombreValorCorto,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<ListasValorDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		
		public string ObtenerUrlCasosExternos(string LVnombreCorto)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabListas = this.udt.Sesion.CreateObjectSet<Lista>();
			var coleccion = (from tab in tabListasValor
							 join tabL in tabListas on tab.IdLista equals tabL.Id
							 where
								  tabL.Nombre == "LinkCasosExternos" &&
								  tab.NombreValorCorto == LVnombreCorto
							 orderby tab.Posicion
							 select tab.Valor).FirstOrDefault();
			return coleccion;
		}
		
		/// <summary>
		/// Este método devuelve los valores de lista, a partir de un Id de la lista maestra
		/// </summary>
		/// <param name="nombreLista">Nombre de la lista</param>
		/// <returns>Lista de valores con los atributos Id, NombreValor, NombreValorCorto</returns>
		public IEnumerable<ListasValorDTO> ObtenerDTOByIdLista(int? IdLista)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabListas = this.udt.Sesion.CreateObjectSet<Lista>();
			var coleccion = from tab in tabListasValor
							join tabL in tabListas on tab.IdLista equals tabL.Id
							where tabL.Id == IdLista
							orderby tab.NombreValorCorto
							select new ListasValorDTO
							{
								NombreValor = tab.NombreValor,
								NombreValorCorto = tab.NombreValorCorto,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<ListasValorDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		
		/// <summary>
		/// Permite obtener un listado de ListasValorDTO
		/// </summary>
		/// <returns>Listado de ListasValorDTO</returns>
		public IEnumerable<ListasValorDTO> ObtenerListas()
		{
			var tabListas = this.udt.Sesion.CreateObjectSet<Lista>();
			var coleccion = from tab in tabListas
							select new ListasValorDTO
							{
								Id = tab.Id,
								NombreValor = tab.Nombre,
								IndVigente = tab.IndVigente,
								FechaValidez = tab.FechaValidez,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<ListasValorDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		
		/// <summary>
		/// Permite obtener una Lista de Valores a partir del nombre de la Lista maestra
		/// </summary>
		/// <param name="nombreLista">Nombre de la Lista</param>
		/// <returns>Lista de Valores</returns>
		public IEnumerable<ListasValorDTO> ObtenerListaValoresDTO(string nombreLista)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabListas = this.udt.Sesion.CreateObjectSet<Lista>();
			var coleccion = from lv in tabListasValor
							join l in tabListas on lv.IdLista equals l.Id
							where l.Nombre == nombreLista
							orderby lv.Posicion
							select new ListasValorDTO
							{
								Id = lv.Id,
								IdLista = lv.IdLista,
								NombreValor = lv.NombreValor,
								NombreValorCorto = lv.NombreValorCorto,
								Posicion = lv.Posicion,
								IndVigente = lv.IndVigente,
								FechaValidez = lv.FechaValidez,
								IndEliminado = lv.IndEliminado
							};
			coleccion = UtilidadesDTO<ListasValorDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}
		
		/// <summary>
		/// Permite obtener un elemento de una Lista de Valores a partir de su respectivo nombre de valor corto 
		/// y el nombre de la Lista maestra.
		/// </summary>
		/// <param name="nombreLista">Nombre de la Lista</param>
		/// <param name="NombreValorCorto">Nombre corto del valor a obtener</param>
		/// <returns>Un elemento de una Lista de Valores</returns>
		public ListasValorDTO ObtenerListaValoresDTO(string nombreLista, string NombreValorCorto)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabListas = this.udt.Sesion.CreateObjectSet<Lista>();
			var coleccion = (from lv in tabListasValor
							 join l in tabListas on lv.IdLista equals l.Id
							 where l.Nombre == nombreLista && lv.NombreValorCorto == NombreValorCorto
							 orderby lv.Posicion
							 select new ListasValorDTO
							 {
								 Id = lv.Id,
								 IdLista = lv.IdLista,
								 NombreValor = lv.NombreValor,
								 NombreValorCorto = lv.NombreValorCorto,
								 Posicion = lv.Posicion,
								 Valor = lv.Valor
							 }).SingleOrDefault();
			return coleccion;
		}
		
		public string TipoServicio(int idListaValor)
		{
			int valor = 0;
			var tabListaValor = this.udt.Sesion.CreateObjectSet<ListaValor>();
			var tabServicio = this.udt.Sesion.CreateObjectSet<Servicio>();
			string tipoServicio = string.Empty;

			var coleccion = (from lv in tabListaValor
							 where lv.Id == idListaValor
							 select lv.Valor).FirstOrDefault();
			
			if (!string.IsNullOrEmpty(coleccion))
			{
				valor = int.Parse(coleccion);

				tipoServicio = (from S in tabServicio
									where S.Id == valor
									select S.Nombre).FirstOrDefault();
			}
			return tipoServicio;
		}
        public string ObtengoValorPorListaValor(string ListaValor, string lista)
        {
            var tblista = udt.Sesion.CreateObjectSet<Lista>();
            var tblistavalor = udt.Sesion.CreateObjectSet<ListaValor>();
            var Valor = (from l in tblista
                         join lv in tblistavalor on l.Id equals lv.IdLista
                         where l.Nombre == lista
                         && lv.NombreValor == ListaValor
                         select lv.Valor).FirstOrDefault();
            if (!string.IsNullOrEmpty(Valor))
                return Valor;
            else
                return "";
        }
		#endregion DTO

	}
}
