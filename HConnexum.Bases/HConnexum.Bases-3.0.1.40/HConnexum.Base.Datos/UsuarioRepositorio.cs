using System;
using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;

namespace HConnexum.Base.Datos
{
	///<summary>Clase: UsuarioRepositorio.</summary>
	public sealed class UsuarioRepositorio : RepositorioBase<TB_Usuarios, UsuarioDto>
	{
		#region "Constructores"
		
		///<summary>Constructor de la clase UsuarioRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public UsuarioRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		#endregion "Constructores"
		
		#region DTO
		
		public UsuarioDto ObtenerUsuarioPorIdUsuarioSuscriptor(int idUsuarioSuscriptor)
		{
			var tabUsuario = this.udt.Sesion.CreateObjectSet<TB_Usuarios>();
			var tabDatosBase = this.udt.Sesion.CreateObjectSet<TB_DatosBase>();
			var tabUsuarioSuscriptor = this.udt.Sesion.CreateObjectSet<TB_UsuariosSucriptores>();
			var coleccion = (from db in tabDatosBase
							 join u in tabUsuario on db.IdUsuario equals u.Id
							 join us in tabUsuarioSuscriptor on u.Id equals us.IdUsuario
							 where us.Id == idUsuarioSuscriptor &&
								   db.IndEliminado == false &&
								   db.IndVigente == true &&
								   db.FechaValidez <= DateTime.Now &&
								   u.IndEliminado == false &&
								   u.IndVigente == true &&
								   u.FechaValidez <= DateTime.Now &&
								   us.IndEliminado == false &&
								   us.IndVigente == true &&
								   us.FechaValidez <= DateTime.Now
							 orderby u.LoginUsuario
							 select new UsuarioDto
							 {
								 Nombre1 = db.Nombre1,
								 Apellido1 = db.Apellido1,
								 LoginUsuario = u.LoginUsuario,
							 }).SingleOrDefault();
			
			return coleccion;
		}
		
		public UsuarioDto ObtenerUsuarioTipoSistemaPorIdUsuarioSuscriptor(int idUsuarioSuscriptor)
		{
			var tabUsuario = this.udt.Sesion.CreateObjectSet<TB_Usuarios>();
			var tabUsuarioSuscriptor = this.udt.Sesion.CreateObjectSet<TB_UsuariosSucriptores>();
			var coleccion = (from u in tabUsuario
							 join us in tabUsuarioSuscriptor on u.Id equals us.IdUsuario
							 where us.Id == idUsuarioSuscriptor &&
								   u.IndEliminado == false &&
								   u.IndVigente == true &&
								   u.FechaValidez <= DateTime.Now &&
								   us.IndEliminado == false &&
								   us.IndVigente == true &&
								   us.FechaValidez <= DateTime.Now
							 orderby u.LoginUsuario
							 select new UsuarioDto
							 {
								 LoginUsuario = u.LoginUsuario,
							 }).SingleOrDefault();
			
			return coleccion;
		}
		
		#endregion DTO
	}
}