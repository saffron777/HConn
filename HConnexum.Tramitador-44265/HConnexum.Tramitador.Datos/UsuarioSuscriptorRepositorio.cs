using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Configurador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: UsuarioSuscriptorRepositorio.</summary>	
	public sealed class UsuarioSuscriptorRepositorio : RepositorioBase<UsuariosSucriptor>
	{
		#region "ConstructoreS"
		///<summary>Constructor de la clase MovimientoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
        public UsuarioSuscriptorRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}

		#endregion "Constructores"

		#region DTO
        public IEnumerable<UsuariosSucriptorDTO> SupervisadosPorUsuarioSuscriptor(string orden, int pagina, int registros, IList<Filtro> parametrosFiltro, int idUsuarioSuscriptor, int idUsuarioSeleccionado) 
        {
            var tabUsuariosSucriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
            var tabUsuario = udt.Sesion.CreateObjectSet<Usuario>();
            var tabDatosBase = udt.Sesion.CreateObjectSet<DatosBase>();
            List<int> supervisadosCompleto = new List<int>();
            List<int> supervisadosPorNivel = new List<int>();
            List<int> supervisadosPorNivelAcumulado = new List<int>();
            IEnumerable<int> supervisadosPorUsuarioSuscriptor = Enumerable.Empty<int>();
            supervisadosPorNivel.Add(idUsuarioSuscriptor);
            do
            {
                foreach (int i in supervisadosPorNivel)
                {
                    supervisadosPorUsuarioSuscriptor = (from s in tabUsuariosSucriptor
                                                        join u in tabUsuario on s.IdUsuario equals u.Id
                                                        where s.IdJefeInmediato == i &&
                                                              s.IndVigente == true &&
                                                              s.IndEliminado == false &&
                                                              s.FechaValidez <= DateTime.Now &&
                                                              u.IndVigente == true &&
                                                              u.IndEliminado == false &&
                                                              u.FechaValidez <= DateTime.Now
                                                        select s.Id);
                    supervisadosPorNivelAcumulado.AddRange(supervisadosPorUsuarioSuscriptor);
                }
                supervisadosPorNivel.Clear();
                supervisadosPorNivel.AddRange(supervisadosPorNivelAcumulado);
                supervisadosPorNivelAcumulado.Clear();
                supervisadosCompleto.AddRange(supervisadosPorNivel);
            }
            while (supervisadosPorNivel.Count() != 0);
            var coleccion = (from tabUS in tabUsuariosSucriptor
                                                join tabU in tabUsuario on tabUS.IdUsuario equals tabU.Id
                                                join tabD in tabDatosBase on tabU.Id equals tabD.IdUsuario
                                                where supervisadosCompleto.Contains(tabUS.Id)
                                                && tabD.Nombre1!=""
                                                && tabUS.Id!=idUsuarioSeleccionado
                                                
                                                select new UsuariosSucriptorDTO
                                                {
                                                    Id = tabUS.Id,
                                                    NombreApellido = tabD.Nombre1 + " " + tabD.Apellido1,
                                                    IndEliminado=tabUS.IndEliminado.Value,
                                                    IndVigente=tabUS.IndVigente.Value,
                                                    FechaValidez=tabUS.FechaValidez
                                                });
            var colecion2 = (from tabUS in tabUsuariosSucriptor
                             join tabU in tabUsuario on tabUS.IdUsuario equals tabU.Id
                             join tabD in tabDatosBase on tabU.Id equals tabD.IdUsuario
                             where
                            tabUS.Id == idUsuarioSuscriptor
                             && tabUS.Id != idUsuarioSeleccionado

                             select new UsuariosSucriptorDTO
                             {
                                 Id = tabUS.Id,
                                 NombreApellido = tabD.Nombre1 + " " + tabD.Apellido1,
                                 IndEliminado = tabUS.IndEliminado.Value,
                                 IndVigente = tabUS.IndVigente.Value,
                                 FechaValidez = tabUS.FechaValidez
                             });
            coleccion=coleccion.Union(colecion2);
            coleccion = UtilidadesDTO<UsuariosSucriptorDTO>.FiltrarPaginar(coleccion, pagina, registros, parametrosFiltro);
            coleccion = UtilidadesDTO<UsuariosSucriptorDTO>.FiltrarColeccionEliminacion(coleccion, true);
            Conteo = UtilidadesDTO<UsuariosSucriptorDTO>.Conteo;
            return UtilidadesDTO<UsuariosSucriptorDTO>.EncriptarId(coleccion);
            
        
        }
        public IEnumerable<UsuariosSucriptorDTO> SupervisadosPorUsuarioSuscriptorList( int idUsuarioSuscriptor, int idUsuarioSeleccionado)
        {
            var tabUsuariosSucriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
            var tabUsuario = udt.Sesion.CreateObjectSet<Usuario>();
            var tabDatosBase = udt.Sesion.CreateObjectSet<DatosBase>();
            List<int> supervisadosCompleto = new List<int>();
            List<int> supervisadosPorNivel = new List<int>();
            List<int> supervisadosPorNivelAcumulado = new List<int>();
            IEnumerable<int> supervisadosPorUsuarioSuscriptor = Enumerable.Empty<int>();
            supervisadosPorNivel.Add(idUsuarioSuscriptor);
            do
            {
                foreach (int i in supervisadosPorNivel)
                {
                    supervisadosPorUsuarioSuscriptor = (from s in tabUsuariosSucriptor
                                                        join u in tabUsuario on s.IdUsuario equals u.Id
                                                        where s.IdJefeInmediato == i &&
                                                              s.IndVigente == true &&
                                                              s.IndEliminado == false &&
                                                              s.FechaValidez <= DateTime.Now &&
                                                              u.IndVigente == true &&
                                                              u.IndEliminado == false &&
                                                              u.FechaValidez <= DateTime.Now
                                                        select s.Id);
                    supervisadosPorNivelAcumulado.AddRange(supervisadosPorUsuarioSuscriptor);
                }
                supervisadosPorNivel.Clear();
                supervisadosPorNivel.AddRange(supervisadosPorNivelAcumulado);
                supervisadosPorNivelAcumulado.Clear();
                supervisadosCompleto.AddRange(supervisadosPorNivel);
            }
            while (supervisadosPorNivel.Count() != 0);
            var coleccion = (from tabUS in tabUsuariosSucriptor
                             join tabU in tabUsuario on tabUS.IdUsuario equals tabU.Id
                             join tabD in tabDatosBase on tabU.Id equals tabD.IdUsuario
                             where supervisadosCompleto.Contains(tabUS.Id)
                             && tabD.Nombre1 != ""
                             && tabUS.Id != idUsuarioSeleccionado

                             select new UsuariosSucriptorDTO
                             {
                                 Id = tabUS.Id,
                                 NombreApellido = tabD.Nombre1 + " " + tabD.Apellido1,
                                 IndEliminado = tabUS.IndEliminado.Value,
                                 IndVigente = tabUS.IndVigente.Value,
                                 FechaValidez = tabUS.FechaValidez
                             });
            var colecion2 = (from tabUS in tabUsuariosSucriptor
                             join tabU in tabUsuario on tabUS.IdUsuario equals tabU.Id
                             join tabD in tabDatosBase on tabU.Id equals tabD.IdUsuario
                             where
                            tabUS.Id == idUsuarioSuscriptor
                             && tabUS.Id != idUsuarioSeleccionado

                             select new UsuariosSucriptorDTO
                             {
                                 Id = tabUS.Id,
                                 NombreApellido = tabD.Nombre1 + " " + tabD.Apellido1,
                                 IndEliminado = tabUS.IndEliminado.Value,
                                 IndVigente = tabUS.IndVigente.Value,
                                 FechaValidez = tabUS.FechaValidez
                             });
            coleccion = coleccion.Union(colecion2);
          
            coleccion = UtilidadesDTO<UsuariosSucriptorDTO>.FiltrarColeccionEliminacion(coleccion, true);
            
            return coleccion;


        }

		#endregion DTO

		

	
	}
}
