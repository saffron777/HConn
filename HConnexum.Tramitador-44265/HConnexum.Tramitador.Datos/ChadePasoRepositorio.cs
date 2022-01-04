using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{

	///<summary>Clase: ChadePasoRepositorio.</summary>	
    public sealed class  ChadePasoRepositorio : RepositorioBase<ChadePaso>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase ChadePasoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public ChadePasoRepositorio(IUnidadDeTrabajo udt) : base(udt)
        {
			
        }
		#endregion "Constructores"
		
		#region DTO

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<param name="orden">Parametro de Ordenamiento en el RadGrid.</param>
///<param name="pagina">Nro pagina en el RadGrid.</param>
///<param name="registros">Cantidad de registros a devolver en el RadGrid.</param>
///<param name="parametrosFiltro">Filtros a aplicar en el RadGrid.</param>
///<returns>Retorna IEnumerableChadePasoDTO.</returns>
public IEnumerable<ChadePasoDTO> ObtenerDTOLista(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idPaso)
{
	var tabChadePaso = udt.Sesion.CreateObjectSet<ChadePaso>();
    var tabCargoSuscriptor = udt.Sesion.CreateObjectSet<CargosSuscriptor>();
    var tabHabilidadesSuscriptor = udt.Sesion.CreateObjectSet<HabilidadesSuscriptor>();
    var tabAutonomiasSuscriptor = udt.Sesion.CreateObjectSet<AutonomiasSuscriptor>();
	var coleccion = from tab in tabChadePaso
                    join tabCS in tabCargoSuscriptor
                    on tab.IdCargosuscriptor equals tabCS.Id
                    join tabHS in tabHabilidadesSuscriptor
                    on tab.IdHabilidadSuscriptor equals tabHS.Id
                    into tabtabHS from CHAHS in tabtabHS.DefaultIfEmpty()
                    join tabAS in tabAutonomiasSuscriptor
                    on tab.IdAutonomiaSuscriptor equals tabAS.Id
                    into tabtabAS from CHAAS in tabtabAS.DefaultIfEmpty()
                    where tab.IdPasos == idPaso
         orderby "it." + orden  
         select new ChadePasoDTO {
		 Id = tab.Id
,IdPasos = tab.IdPasos
,NombreCargo = tabCS.Nombre
,IdCargosuscriptor = tab.IdCargosuscriptor
,NombreHabilidad = CHAHS.Nombre
,IdHabilidadSuscriptor = tab.IdHabilidadSuscriptor
,NombreAutonomia = CHAAS.NomAutonomia
,IdAutonomiaSuscriptor = tab.IdAutonomiaSuscriptor
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};

    coleccion = UtilidadesDTO<ChadePasoDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
	coleccion = UtilidadesDTO<ChadePasoDTO>.FiltrarColeccionEliminacion(coleccion, true);
    Conteo = UtilidadesDTO<ChadePasoDTO>.Conteo;
    return UtilidadesDTO<ChadePasoDTO>.EncriptarId(coleccion);
    //return coleccion; 
}

///<summary>Metodo encargado de ejecutar sentencias LINQ.</summary>
///<returns>Retorna IEnumerableChadePasoDTO.</returns>
public IEnumerable<ChadePasoDTO> ObtenerDTO()
{
    var tabChadePaso = udt.Sesion.CreateObjectSet<ChadePaso>();
    var coleccion = from tab in tabChadePaso 
         select new ChadePasoDTO {
		 Id = tab.Id
,IdPasos = tab.IdPasos
,IdCargosuscriptor = tab.IdCargosuscriptor
,IdHabilidadSuscriptor = tab.IdHabilidadSuscriptor
,IdAutonomiaSuscriptor = tab.IdAutonomiaSuscriptor
,CreadoPor = tab.CreadoPor
,FechaCreacion = tab.FechaCreacion
,ModificadoPor = tab.ModificadoPor
,FechaModificacion = tab.FechaModificacion
,IndVigente = tab.IndVigente
,FechaValidez = tab.FechaValidez
,IndEliminado = tab.IndEliminado
			};
	coleccion = UtilidadesDTO<ChadePasoDTO>.FiltrarColeccionAuditoria(coleccion, false);
    return coleccion; 
}

		#endregion DTO
	}
}