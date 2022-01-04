using System;
using System.Collections.Generic;
using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;

namespace HConnexum.Base.Datos
{
	///<summary>Clase: TomadoRepositorio.</summary>
	public sealed class TomadoRepositorio : RepositorioBase<TB_Tomado, TomadoDto>
	{
		#region "Constructores"
		
		///<summary>Constructor de la clase TomadoRepositorio.</summary>
		///<param name="udt">Unidad de trabajo.</param>
		public TomadoRepositorio(IUnidadDeTrabajo udt) : base(udt)
		{
		}
		
		#endregion "Constructores"
		
		#region DTO
		
		public IList<TomadoDto> ObtenerRegistroTomadoDto(string tabla, IList<int> ids)
		{
			var tabTomado = this.udt.Sesion.CreateObjectSet<TB_Tomado>();
			var coleccion = (from t in tabTomado
							 where t.Tabla == tabla &&
								   ids.Contains(t.IdRegistro)
							 select new TomadoDto
							 {
								 Id = t.Id,
								 IdPaginaModulo = t.IdPaginaModulo,
								 Tabla = t.Tabla,
								 IdRegistro = t.IdRegistro,
								 IdSesionUsuario = t.IdSesionUsuario,
								 FechaTomado = t.FechaTomado,
								 NombreUsuario = t.LoginUsuario
							 }).ToList();
			
			return coleccion;
		}
		
		public TB_Tomado ObtenerTomado(string tabla, int idRegistro)
		{
			var tabTomado = this.udt.Sesion.CreateObjectSet<TB_Tomado>();
			var coleccion = (from t in tabTomado
							 where (t.Tabla == tabla && t.IdRegistro == idRegistro)
							 select t).SingleOrDefault();
			
			return coleccion;
		}
		
		#endregion DTO
		
		public void EliminarRegistroTomado(int idRegistro, string nombreTabla)
		{
			this.udt.IniciarTransaccion();
			TomadoRepositorio repositorio = new TomadoRepositorio(this.udt);
			TB_Tomado tomado = repositorio.ObtenerTomado(nombreTabla, idRegistro);
			
			if (tomado != null)
				repositorio.Eliminar(tomado);
			
			this.udt.Commit();
		}
		
		public void BloquearRegistro(int idRegistro, int idPaginaModulo, string idSessionUsuario, string loginUsuario, string nombreTabla)
		{
			if (this.ObtenerTomado(nombreTabla, idRegistro) == null)
			{
				this.udt.IniciarTransaccion();
				TB_Tomado tomado = new TB_Tomado();
				tomado.IdPaginaModulo = idPaginaModulo;
				tomado.Tabla = nombreTabla;
				tomado.IdRegistro = idRegistro;
				tomado.IdSesionUsuario = idSessionUsuario;
				tomado.FechaTomado = DateTime.Now;
				tomado.LoginUsuario = loginUsuario;
				this.udt.MarcarNuevo(tomado);
				this.udt.Commit();
			}
		}
	}
}