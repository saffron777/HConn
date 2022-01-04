using System.Linq;
using HConnexum.Tramitador.Negocio;
using System.Data;
using HConnexum.Servicios.Servicios;

///<summary>Namespace que engloba la clase repositorio de la capa de datos HC_Tramitador.</summary>
namespace HConnexum.Tramitador.Datos
{
	///<summary>Clase: UsuarioRepositorio.</summary>
	public sealed class UsuarioRepositorio : RepositorioBase<Usuario>
	{
		#region "Constructores"
		///<summary>Constructor de la clase UsuarioRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public UsuarioRepositorio(IUnidadDeTrabajo udt)
			: base(udt)
		{
		}
		#endregion "Constructores"

		#region DTO
		///<summary>Método encargado de ejecutar sentencias LINQ.</summary>
		///<returns>Retorna UsuarioDTO.</returns>
		public UsuarioDTO ObtenerDTO(int IdUsuario)
		{
			var tabUsuario = udt.Sesion.CreateObjectSet<Usuario>();
			var tabDatosBase = udt.Sesion.CreateObjectSet<DatosBase>();
			var entidad = (from tabU in tabUsuario
						   join tabDB in tabDatosBase
						   on tabU.Id equals tabDB.IdUsuario
						   where tabU.Id == IdUsuario
						   select new UsuarioDTO
							 {
								 NombreApellido = tabDB.Nombre1 + " " + tabDB.Apellido1
							 }).FirstOrDefault();
			return entidad;
		}

		public UsuarioDTO ObtenerUsuarioPorIdUsuarioSuscriptor(int idUsuarioSuscriptor)
		{
			var tabUsuario = this.udt.Sesion.CreateObjectSet<Usuario>();
			var tabDatosBase = this.udt.Sesion.CreateObjectSet<DatosBase>();
			var tabUsuarioSuscriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
			var coleccion = (from db in tabDatosBase
							 join u in tabUsuario on
							 db.IdUsuario equals u.Id
							 join us in tabUsuarioSuscriptor
							 on u.Id equals us.IdUsuario
							 where us.Id == idUsuarioSuscriptor
							 orderby u.LoginUsuario
							 select new UsuarioDTO
							 {
								 Nombre1 = db.Nombre1,
								 Apellido1 = db.Apellido1,
								 LoginUsuario = u.LoginUsuario,
							 }).SingleOrDefault();
			return coleccion;
		}
        public DataTable ObtenerPorIdUsuarioSuscriptor(int idUsuarioSuscriptor)
        {
           
            var tabUsuario = this.udt.Sesion.CreateObjectSet<Usuario>();
            var tabDatosBase = this.udt.Sesion.CreateObjectSet<DatosBase>();
            var tabUsuarioSuscriptor = udt.Sesion.CreateObjectSet<UsuariosSuscriptor>();
            var coleccion = (from db in tabDatosBase 
                             join u in tabUsuario on db.IdUsuario equals u.Id
                             join us in tabUsuarioSuscriptor on u.Id equals us.IdUsuario 
                             where us.Id == idUsuarioSuscriptor
                             orderby u.LoginUsuario
                             select new 
                             {
                                 Id=us.Id,
                                 Nombre = db.Nombre1+" "+ db.Apellido1
                                 
                             });

            return (LinqtoDataSetMethods.CopyToDataTable(coleccion));
        }
        public string ObtenerNombreUsuario(int id)
        {
            UsuarioRepositorio repositorio = new UsuarioRepositorio(udt);
            if (id != null && id > 0)
            {
                UsuarioDTO usuario = repositorio.ObtenerUsuarioPorIdUsuarioSuscriptor(id);
                if (usuario != null)
                    return usuario.Nombre1+" "+usuario.Apellido1;
            }
            return "";
        }
		#endregion DTO
	}
}
