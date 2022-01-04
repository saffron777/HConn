using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: SolicitanteRepositorio.</summary>	
	public sealed class SolicitanteRepositorio : RepositorioBase<Solicitante>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase SolicitanteRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public SolicitanteRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		#endregion "Constructores"

		#region DTO
		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
		///<param name="pagina">Nro pagina en el RadGrid.</param>
		///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
		///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
		///<returns>Retorna IEnumerableSolicitanteDTO.</returns>
		public IEnumerable<SolicitanteDTO> ObtenerDTO(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro)
		{
			var tabSolicitante = udt.Sesion.CreateObjectSet<Solicitante>();
			var coleccion = from tab in tabSolicitante
							orderby "it." + orden
							select new SolicitanteDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre,
								Apellido = tab.Apellido,
								TipDoc = tab.TipDoc,
								NumDoc = tab.NumDoc,
								Email = tab.Email,
								Movil = tab.Movil,
								Token = tab.Token,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<SolicitanteDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
			coleccion = UtilidadesDTO<SolicitanteDTO>.FiltrarColeccionEliminacion(coleccion, true);
			Conteo = UtilidadesDTO<SolicitanteDTO>.Conteo;
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableSolicitanteDTO.</returns>
		public IEnumerable<SolicitanteDTO> ObtenerDTO()
		{
			var tabSolicitante = udt.Sesion.CreateObjectSet<Solicitante>();
			var coleccion = from tab in tabSolicitante
							orderby tab.Nombre
							select new SolicitanteDTO
							{
								Id = tab.Id,
								Nombre = tab.Nombre,
								Apellido = tab.Apellido,
								TipDoc = tab.TipDoc,
								NumDoc = tab.NumDoc,
								Email = tab.Email,
								Movil = tab.Movil,
								Token = tab.Token,
								CreadoPor = tab.CreadoPor,
								FechaCreacion = tab.FechaCreacion,
								ModificadoPor = tab.ModificadoPor,
								FechaModificacion = tab.FechaModificacion,
								FechaValidez = tab.FechaValidez,
								IndVigente = tab.IndVigente,
								IndEliminado = tab.IndEliminado
							};
			coleccion = UtilidadesDTO<SolicitanteDTO>.FiltrarColeccionAuditoria(coleccion, false);
			return coleccion;
		}

		///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna IEnumerableSolicitanteDTO.</returns>
		public Solicitante ObtenerPorDocumento(string tipDoc, string numDoc)
		{
			var tabSolicitante = udt.Sesion.CreateObjectSet<Solicitante>();
			var coleccion = (from s in tabSolicitante
							where s.TipDoc == tipDoc &&
								  s.NumDoc == numDoc &&
								  s.IndVigente == true &&
								  s.IndEliminado == false &&
								  s.FechaValidez <= DateTime.Now
							select s).SingleOrDefault();
			return coleccion;
		}
		#endregion DTO
	}
}