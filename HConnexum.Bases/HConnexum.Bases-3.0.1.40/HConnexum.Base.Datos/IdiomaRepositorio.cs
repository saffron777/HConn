using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Datos
{
	public sealed class IdiomaRepositorio : RepositorioBase<TB_Idiomas, IdiomaDto>
	{
		#region "Constructores"
		
		///<summary>Constructor de la clase IdiomaRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public IdiomaRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		///<summary>Constructor de la clase IdiomaRepositorio.</summary>
		///<param name="udt">Instanciando una nueva Unidad de trabajo para utilizar HC_CONFIGURADOREntities.</param>
		public IdiomaRepositorio(AuditoriaDto auditoriaDto)
		{
			this.udt = new UnidadDeTrabajo(auditoriaDto, new HC_CONFIGURADOREntities());
		}
		
		#endregion "Constructores"
		
		#region DTO
		
		/// <summary>
		/// Permite obtener un listado de Idioma
		/// </summary>
		/// <returns>Listado de Idioma</returns>
		public IList<IdiomaDto> ObtenerIdiomas()
		{
			var tabIdioma = this.udt.Sesion.CreateObjectSet<TB_Idiomas>();
			var coleccion = (from tabI in tabIdioma
							 where tabI.FechaValidez <= DateTime.Now &&
								   tabI.IndEliminado == false &&
								   tabI.IndVigente == true
							 orderby tabI.Nombre
							 select new IdiomaDto
							 {
								 Id = tabI.Id,
								 Nombre = tabI.Nombre,
								 NombreCorto = tabI.NombreCorto,
								 CreadoPor = tabI.CreadoPor,
								 FecCreacion = tabI.FecCreacion,
								 ModificadoPor = tabI.ModificadoPor,
								 FechaModificacion = tabI.FechaModificacion,
								 FechaValidez = tabI.FechaValidez,
								 IndVigente = tabI.IndVigente,
								 IndEliminado = tabI.IndEliminado
							 }).ToList();
			return coleccion;
		}
		
		#endregion DTO
	}
}