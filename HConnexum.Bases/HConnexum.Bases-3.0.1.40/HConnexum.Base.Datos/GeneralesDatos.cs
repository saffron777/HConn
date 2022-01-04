using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;
using HConnexum.Seguridad;

namespace HConnexum.Base.Datos
{
	public class GeneralesDatos
	{
		private readonly AuditoriaDto auditoriaDto;
		
		public GeneralesDatos(AuditoriaDto auditoriaDto)
		{
			this.auditoriaDto = auditoriaDto;
		}
		
		public void EliminarRegistroTomado(string tabla, int idRegistro)
		{
			TomadoRepositorio repositorioTomado = new TomadoRepositorio(new UnidadDeTrabajo(this.auditoriaDto, new BD_HC_Tomado_Log()));
			repositorioTomado.EliminarRegistroTomado(idRegistro, tabla);
		}
		
		/// <summary>Obtiene el nombre de una pagina</summary>
		/// <param name="nombreArchivo">esto es el nombre de la pagina que buscara</param>
		/// <returns></returns>
		public string ObtenerPaginasModulos(int idPaginaModulo)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					PaginasModuloRepositorio repositorio = new PaginasModuloRepositorio(udt);
					return repositorio.ObtenerPorIdT(idPaginaModulo).NombrePagina;
				}
			}
		}
		
		public IList<TraModAppPagModAppTraModApp> ObtenerTransaccionesPaginasModulos(int idPaginasModulo)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					TraModAppPagModAppTraModAppRepositorio repositorio = new TraModAppPagModAppTraModAppRepositorio(udt);
					return repositorio.ObtenerTransaccionesPaginaModuloDtoPaginaBase(idPaginasModulo).ToList();
				}
			}
		}
		
		public IList<TraModAppPagModAppTraModApp> ObtenerTransaccionesUsuario(int paginaModulo, int[] idRoles)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					TraModAppPagModAppTraModAppRepositorio repositorio = new TraModAppPagModAppTraModAppRepositorio(udt);
					return repositorio.ObtenerTransaccionesPorIdRolIdPaginaModulo(paginaModulo, idRoles).ToList();
				}
			}
		}
		
		public string ObtenerNombreUsuario(int? id)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					UsuarioRepositorio repositorio = new UsuarioRepositorio(udt);
					if (id != null && id > 0)
					{
						UsuarioDto usuario = repositorio.ObtenerUsuarioPorIdUsuarioSuscriptor(id.Value);
						if (usuario != null)
							return usuario.LoginUsuario;
					}
					return string.Empty;
				}
			}
		}
		
		public string ObtenerNombreUsuarioTipoSistema(int? id)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					UsuarioRepositorio repositorio = new UsuarioRepositorio(udt);
					if (id != null && id > 0)
					{
						UsuarioDto usuario = repositorio.ObtenerUsuarioTipoSistemaPorIdUsuarioSuscriptor(id.Value);
						if (usuario != null)
							return usuario.LoginUsuario;
					}
					return string.Empty;
				}
			}
		}
		
		/// <summary>Obtiene el valor de la conexionstring que tiene asignado un suscriptor.</summary>
		/// <param name="idExterno">Id externo del suscriptor a consultar.</param>
		/// <param name=" origen "Nombre del dato particular del suscriptor a solicitar.</param>
		/// <returns>conexionstring.</returns>
		public string ObtenerListaValorPorIdExterno(int idExterno, string origen)
		{
			string idExternoString = idExterno.ToString();
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					var suscriptores = udt.Sesion.CreateObjectSet<TB_Suscriptores>();
					var suscriptor = (from s in suscriptores
									  where s.CodIdExterno == idExternoString
									  select new
									  {
										  Id = s.Id
									  }).FirstOrDefault();
					if (suscriptor != null)
						return this.ObtenerListaValorPorSuscriptorId(suscriptor.Id, origen);
					else
						return null;
				}
			}
		}
		
		/// <summary>Obtiene el valor de la conexionstring que tiene asignado un suscriptor.</summary>
		/// <param name="suscriptorId">Identificador único del suscriptor a consultar.</param>
		/// <param name=" origen "Nombre del dato particular del suscriptor a solicitar.</param>
		/// <returns>conexionstring.</returns>
		public string ObtenerListaValorPorSuscriptorId(int suscriptorId, string origen)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					var valoresDatosParticulares = udt.Sesion.CreateObjectSet<TB_ValoresDatosParticulares>();
					var datosParticulares = udt.Sesion.CreateObjectSet<TB_DatosParticulares>();
					var listasValores = udt.Sesion.CreateObjectSet<TB_ListasValores>();
					var conexionstring = (from dp in datosParticulares
										  join VDP in valoresDatosParticulares on dp.Id equals VDP.IdDatoParticular
										  join LV in listasValores on VDP.Valor equals LV.NombreValorCorto
										  where VDP.IdRegistroTablaPropietaria == suscriptorId &&
												VDP.IdTablaPropietaria == dp.IdTablaPropietaria &&
												dp.Nombre == origen
										  select new
										  {
											  ConexionString = LV.Valor
										  }).FirstOrDefault();
					if (conexionstring != null)
						return conexionstring.ConexionString;
					else
						return null;
				}
			}
		}
		
		public string ObtenerCodIdExternoByIdSucursal(int idSucursal)
		{
			using (ObjectContext objectContext = new HC_CONFIGURADOREntities())
			{
				using (UnidadDeTrabajo udt = new UnidadDeTrabajo(this.auditoriaDto, objectContext))
				{
					var tabSucursales = udt.Sesion.CreateObjectSet<TB_Sucursales>();
					var colecion = (from tabS in tabSucursales
									where tabS.Id == idSucursal &&
										  tabS.IndVigente == true &&
										  tabS.FechaValidez <= DateTime.Now &&
										  tabS.IndEliminado == false
									select new
									{
										tabS.CodIdExterno
									}).FirstOrDefault();
					if (string.IsNullOrEmpty(colecion.CodIdExterno))
						return string.Empty;
					else
						return colecion.CodIdExterno;
				}
			}
		}
	}
}