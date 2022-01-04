using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Datos;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
    ///<summary>Clase: ListaRepositorio.</summary>	
    public sealed class ListaRepositorio : RepositorioBase<Lista>
    {
        #region "Constructores"
        ///<summary>Constructor de la clase ListaRepositorio.</summary>
        ///<param name="udt">Unidad de trabajo.</param>
        public ListaRepositorio(IUnidadDeTrabajo udt)
        : base(udt)
        {
        }
        #endregion "Constructores"
		
        #region DTO
        /// <summary>
        /// Permite obtener un listado de ListaDTO
        /// </summary>
        /// <returns>Listado de ListaDTO</returns>
        public IEnumerable<ListaDTO> ObtenerDTO()
        {
            var tabLista = udt.Sesion.CreateObjectSet<Lista>();
			var coleccion = from tab in tabLista
							where tab.IndEliminado == false
							orderby tab.Nombre
                            select new ListaDTO
                            {
                                Id = tab.Id,
                                Descripcion = tab.Descripcion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };

            return coleccion; 
        }

        public IEnumerable<ListaDTO> ObtenerDTO(int? Lista)
        {
            var tabLista = udt.Sesion.CreateObjectSet<Lista>();
            var coleccion = from tab in tabLista
                            where tab.Id == Lista && tab.IndEliminado == false
                            orderby tab.Nombre
                            select new ListaDTO
                            {
                                Id = tab.Id,
                                Descripcion = tab.Descripcion,
                                IndVigente = tab.IndVigente,
                                FechaValidez = tab.FechaValidez,
                                IndEliminado = tab.IndEliminado
                            };
            return UtilidadesDTO<ListaDTO>.EncriptarId(coleccion);
        }
        #endregion DTO
    }
}