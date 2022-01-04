using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Datos
{
	public sealed class ListasValorRepositorio : RepositorioBase<TB_ListasValores, ListasValorDto>
	{
		#region "Constructores"
		
		///<summary>Constructor de la clase ListasValorRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public ListasValorRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		///<summary>Constructor de la clase ListasValorRepositorio.</summary>
		///<param name="udt">Instanciando una nueva Unidad de trabajo para utilizar HC_CONFIGURADOREntities.</param>
		public ListasValorRepositorio(AuditoriaDto auditoriaDto)
		{
			this.udt = new UnidadDeTrabajo(auditoriaDto, new HC_CONFIGURADOREntities());
		}
		
		#endregion "Constructores"
		
		/// <summary>
		/// Este método devuelve los valores de lista, a partir del nombre de la lista maestra
		/// </summary>
		/// <param name="nombreLista">Nombre de la lista</param>
		/// <returns>Lista de valores con los atributos Id, NombreValor, NombreValorCorto</returns>
		public IList<ListasValorDto> ObtenerListasValorByNombreListaDto(string nombreLista)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<TB_ListasValores>();
			var tabListas = this.udt.Sesion.CreateObjectSet<TB_Listas>();
			var coleccion = (from tabLV in tabListasValor
							 join tabL in tabListas on tabLV.IdLista equals tabL.Id
							 where tabL.Nombre == nombreLista &&
								   tabLV.IndEliminado == false &&
								   tabLV.IndVigente == true &&
								   tabLV.FechaValidez <= DateTime.Now &&
								   tabL.IndEliminado == false &&
								   tabL.IndVigente == true &&
								   tabL.FechaValidez <= DateTime.Now
							 orderby tabLV.Posicion
							 select new ListasValorDto
							 {
								 Id = tabLV.Id,
								 IdLista = tabLV.IdLista,
								 NombreValor = tabLV.NombreValor,
								 NombreValorCorto = tabLV.NombreValorCorto,
								 Posicion = tabLV.Posicion,
								 CreadoPor = tabLV.CreadoPor,
								 FechaCreacion = tabLV.FechaCreacion,
								 ModificadoPor = tabLV.ModificadoPor,
								 FechaModificacion = tabLV.FechaModificacion,
								 FechaValidez = tabLV.FechaValidez,
								 IndVigente = tabLV.IndVigente,
								 IndEliminado = tabLV.IndEliminado,
								 Valor = tabLV.Valor
							 }).ToList();
			return coleccion;
		}
		
		/// <summary>
		/// Permite obtener un elemento de una Lista de Valores a partir de su respectivo nombre de valor corto 
		/// y el nombre de la Lista maestra.
		/// </summary>
		/// <param name="nombreLista">Nombre de la Lista</param>
		/// <param name="NombreValorCorto">Nombre corto del valor a obtener</param>
		/// <returns>Un elemento de una Lista de Valores</returns>
		public ListasValorDto ObtenerListasValorByNombreListaNombreValorCortoDto(string nombreLista, string nombreValorCorto)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<TB_ListasValores>();
			var tabListas = this.udt.Sesion.CreateObjectSet<TB_Listas>();
			var coleccion = (from tabLV in tabListasValor
							 join tabL in tabListas on tabLV.IdLista equals tabL.Id
							 where tabL.Nombre == nombreLista &&
								   tabLV.NombreValorCorto == nombreValorCorto &&
								   tabLV.FechaValidez <= DateTime.Now &&
								   tabLV.IndEliminado == false &&
								   tabLV.IndVigente == true &&
								   tabL.FechaValidez <= DateTime.Now &&
								   tabL.IndEliminado == false &&
								   tabL.IndVigente == true
							 orderby tabLV.Posicion
							 select new ListasValorDto
							 {
								 Id = tabLV.Id,
								 IdLista = tabLV.IdLista,
								 NombreValor = tabLV.NombreValor,
								 NombreValorCorto = tabLV.NombreValorCorto,
								 Posicion = tabLV.Posicion,
								 CreadoPor = tabLV.CreadoPor,
								 FechaCreacion = tabLV.FechaCreacion,
								 ModificadoPor = tabLV.ModificadoPor,
								 FechaModificacion = tabLV.FechaModificacion,
								 FechaValidez = tabLV.FechaValidez,
								 IndVigente = tabLV.IndVigente,
								 IndEliminado = tabLV.IndEliminado,
								 Valor = tabLV.Valor
							 }).SingleOrDefault();
			return coleccion;
		}
		
		/// <summary>
		/// Este método devuelve los valores de lista, a partir de un Id de la lista maestra
		/// </summary>
		/// <param name="nombreLista">Nombre de la lista</param>
		/// <returns>Lista de valores con los atributos Id, NombreValor, NombreValorCorto</returns>
		public IList<ListasValorDto> ObtenerListasValorByIdListaDto(int idLista)
		{
			var tabListasValor = this.udt.Sesion.CreateObjectSet<TB_ListasValores>();
			var tabListas = this.udt.Sesion.CreateObjectSet<TB_Listas>();
			var coleccion = (from tabLV in tabListasValor
							 join tabL in tabListas on tabLV.IdLista equals tabL.Id
							 where tabL.Id == idLista &&
								   tabLV.FechaValidez <= DateTime.Now &&
								   tabLV.IndEliminado == false &&
								   tabLV.IndVigente == true &&
								   tabL.FechaValidez <= DateTime.Now &&
								   tabL.IndEliminado == false &&
								   tabL.IndVigente == true
							 orderby tabLV.NombreValorCorto
							 select new ListasValorDto
							 {
								 Id = tabLV.Id,
								 IdLista = tabLV.IdLista,
								 NombreValor = tabLV.NombreValor,
								 NombreValorCorto = tabLV.NombreValorCorto,
								 Posicion = tabLV.Posicion,
								 CreadoPor = tabLV.CreadoPor,
								 FechaCreacion = tabLV.FechaCreacion,
								 ModificadoPor = tabLV.ModificadoPor,
								 FechaModificacion = tabLV.FechaModificacion,
								 FechaValidez = tabLV.FechaValidez,
								 IndVigente = tabLV.IndVigente,
								 IndEliminado = tabLV.IndEliminado,
								 Valor = tabLV.Valor
							 }).ToList();
			return coleccion;
		}
	}
}