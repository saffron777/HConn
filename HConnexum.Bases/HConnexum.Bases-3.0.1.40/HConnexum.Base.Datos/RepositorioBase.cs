using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Web;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.EntidadesGlobales;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Datos
{
	public class RepositorioBase<T, D> : IRepositorio<T> where T : EntityObject
	{
		protected IUnidadDeTrabajo udt;
		
		public RepositorioBase(IUnidadDeTrabajo udt)
		{
			this.udt = udt;
		}
		
		///<summary>Constructor usado para las aplicaciones externas de base.
		///Estas podran utilizar las tablas generales de HC_CONFIGURADOREntities</summary>
		public RepositorioBase()
		{
		}
		
		#region propiedades
		
		private int conteo = 0;
		
		public int Conteo
		{
			get
			{
				return this.conteo;
			}
			set
			{
				this.conteo = value;
			}
		}
		
		#endregion propiedades
		
		public void LiberarTomado(int idRegistro)
		{
			TomadoRepositorio repositorioTomado = new TomadoRepositorio(new UnidadDeTrabajo(this.udt.AuditoriaDto, new BD_HC_Tomado_Log()));
			repositorioTomado.EliminarRegistroTomado(idRegistro, typeof(T).Name);
		}
		
		public void Agregar(T entidad)
		{
			this.udt.MarcarNuevo(entidad);
		}
		
		public void Eliminar(T entidad)
		{
			this.udt.MarcarEliminado(entidad);
		}
		
		public void EliminarLogico(T entidad)
		{
			PropertyInfo propinfo = typeof(T).GetProperty(@"IndEliminado");
			propinfo.SetValue(entidad, true, null);
			this.udt.MarcarModificado(entidad);
		}
		
		public bool Eliminar(T entidad, int idRegistro)
		{
			using (BD_HC_Tomado_Log dataBase = new BD_HC_Tomado_Log())
			{
				if (dataBase.VerificacionEliminacion(typeof(T).Name, idRegistro).Single().Value)
					this.udt.MarcarEliminado(entidad);
				else
					return false;
			}
			return true;
		}
		
		public bool EliminarLogico(T entidad, int idRegistro)
		{
			using (BD_HC_Tomado_Log dataBase = new BD_HC_Tomado_Log())
			{
				if (dataBase.VerificacionEliminacion(string.Format(@"TB_{0}", typeof(T).Name), idRegistro).Single().Value)
				{
					PropertyInfo propinfo = typeof(T).GetProperty(@"IndEliminado");
					propinfo.SetValue(entidad, true, null);
					this.udt.MarcarModificado(entidad);
				}
				else
					return false;
				
				return true;
			}
		}
		
		public void EliminarLogicoLista(IList<string> ids, bool verificiacionEliminacion)
		{
			SwebErrorClient swebErrorClient = new SwebErrorClient();
			try
			{
				if (!this.udt.EstaEnTransaccion)
					this.udt.IniciarTransaccion();
				
				foreach (string id in ids)
				{
					T registro = this.ObtenerPorIdT(int.Parse(id));
					
					if (verificiacionEliminacion)
					{
						PropertyInfo propInfoId = typeof(T).GetProperty(@"Id");
						
						if (!this.EliminarLogico(registro, int.Parse(propInfoId.GetValue(registro, null).ToString())))
						{
							this.udt.Rollback();
							throw new CustomException(swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"EliminarLogicoLista", @"Sin Definir", @"0026", this.udt.AuditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString), ErrorType.Advertencia.ToString());
						}
					}
					else
						this.EliminarLogico(registro);
				}
				this.udt.Commit();
			}
			finally
			{
				if (swebErrorClient.State != CommunicationState.Closed)
					swebErrorClient.Close();
			}
		}
		
		public void ActivarEliminarLogico(T entidad)
		{
			PropertyInfo propinfo = typeof(T).GetProperty(@"IndEliminado");
			propinfo.SetValue(entidad, false, null);
			this.udt.MarcarModificado(entidad);
		}
		
		public void ActivarEliminado(IList<string> ids)
		{
			if (!this.udt.EstaEnTransaccion)
				this.udt.IniciarTransaccion();
			
			foreach (string id in ids)
			{
				int idDesencriptado = int.Parse(id);
				T registro = this.ObtenerPorIdT(idDesencriptado);
				this.ActivarEliminarLogico(registro);
			}
			this.udt.Commit();
		}
		
		public void Actualizar(T entidad)
		{
			this.udt.MarcarModificado(entidad);
		}
		
		public IList<T> ObtenerTodos()
		{
			return this.udt.Sesion.CreateObjectSet<T>().ToList();
		}
		
		public T ObtenerPorIdT(int id)
		{
			var nombreCualificado = string.Format("{0}.{1}", this.udt.Sesion.DefaultContainerName, typeof(T).Name);
			EntityKey clave = new EntityKey(nombreCualificado, @"Id", id);
			object value = null;
			this.udt.Sesion.TryGetObjectByKey(clave, out value);
			return value as T;
		}
		
		public IList<T> ObtenerPaginado(string orden, int pagina, int registros, bool indEliminado)
		{
			return this.ObtenerFiltrado(orden, pagina, registros, new List<HConnexum.Infraestructura.Filtro>(), indEliminado);
		}
		
		public IList<T> ObtenerFiltrado(string orden, int pagina, int registros, IList<HConnexum.Infraestructura.Filtro> parametrosFiltro, bool indEliminado)
		{
			if (!indEliminado)
				parametrosFiltro.Add(new HConnexum.Infraestructura.Filtro { Campo = @"IndEliminado", Operador = @"EqualTo", Tipo = typeof(bool), Valor = false });
			
			IEnumerable<T> coleccion = this.udt.Sesion.CreateObjectSet<T>().OrderBy(string.Format("{0}{1}", @"it.", orden));
			Expression<Func<T, bool>> expresionFiltro = UtilidadesExpresiones.ProcesarExpresiones<T>(parametrosFiltro);
			coleccion = coleccion.Where(expresionFiltro.Compile());
			this.Conteo = coleccion.Count();
			coleccion = coleccion.Skip((pagina - 1) * registros).Take(registros);
			IList<T> tempColeccion = coleccion.ToList();
			PropertyInfo propInfoEncriptado = typeof(T).GetProperty(@"IdEncriptado");
			PropertyInfo propInfoId = typeof(T).GetProperty(@"Id");
			IList<int> ids = new List<int>();
			
			if (propInfoEncriptado != null)
			{
				for (int i = 0; i < tempColeccion.Count; i++)
				{
					propInfoEncriptado.SetValue(tempColeccion[i], System.Text.Encoding.UTF8.GetBytes(propInfoId.GetValue(tempColeccion[i], null).ToString().Encriptar()), null);
					ids.Add(int.Parse(propInfoId.GetValue(tempColeccion[i], null).ToString()));
				}
			}
			PropertyInfo propInfoTomado = typeof(T).GetProperty(@"Tomado");
			
			if (propInfoTomado != null)
			{
				PropertyInfo propInfoUsuarioTomado = typeof(T).GetProperty(@"UsuarioTomado");
				TomadoRepositorio repositorioTomado = new TomadoRepositorio(this.udt);
				IList<TomadoDto> tomados = repositorioTomado.ObtenerRegistroTomadoDto(typeof(T).Name, ids);
				
				foreach (T item in tempColeccion)
				{
					propInfoTomado.SetValue(item, @"L", null);
					
					foreach (TomadoDto tomado in tomados)
					{
						if (tomado.IdRegistro == int.Parse(propInfoId.GetValue(item, null).ToString()))
						{
							if (tomado.IdSesionUsuario == this.udt.IdSesion)
								propInfoTomado.SetValue(item, @"TP", null);
							else
								propInfoTomado.SetValue(item, @"T", null);
							
							propInfoUsuarioTomado.SetValue(item, tomado.NombreUsuario, null);
						}
					}
				}
			}
			return coleccion.ToList();
		}
		
		public static void LlenarEntidad<T, U>(T dto, ref U entidad, AccionDetalle accion)
		{
			int creadoPor = 0;
			PropertyInfo propinfoU = null;
			
			if (accion == AccionDetalle.Modificar)
			{
				propinfoU = typeof(U).GetProperty(@"CreadoPor");
				creadoPor = (int)propinfoU.GetValue(entidad, null);
			}
			PropertyInfo[] propertiesT = typeof(T).GetProperties();
			
			foreach (PropertyInfo property in propertiesT)
			{
				propinfoU = typeof(U).GetProperty(property.Name);
				
				if (propinfoU != null)
					propinfoU.SetValue(entidad, property.GetValue(dto, null), null);
			}
			if (accion == AccionDetalle.Modificar)
			{
				propinfoU = typeof(U).GetProperty(@"CreadoPor");
				propinfoU.SetValue(entidad, creadoPor, null);
			}
		}
		
		public static void LlenarDto<T, U>(ref T dto, U entidad)
		{
			PropertyInfo[] propertiesT = typeof(T).GetProperties();
			
			foreach (PropertyInfo propinfoT in propertiesT)
			{
				PropertyInfo propinfoU = typeof(U).GetProperty(propinfoT.Name);
				propinfoT.SetValue(dto, propinfoU.GetValue(entidad, null), null);
			}
		}
		
		///<summary>Método encargado de enviar mensaje(s) error a la vista.</summary>	
		/// <returns>Devuelve mensaje(s) con los datos validados.</returns>
		public static string ValidarDatos<T, U>(T datos, U metadata)
		{
			Type clase = typeof(U);
			StringBuilder errores = new StringBuilder();
			PropertyInfo[] propertiesT = typeof(T).GetProperties();
			
			foreach (PropertyInfo propinfoT in propertiesT)
			{
				if (propinfoT.Name != @"Id" && propinfoT.Name != @"CreadoPor" && propinfoT.Name != @"FechaCreacion" && propinfoT.Name != @"ModificadoPor" && propinfoT.Name != @"FechaModificacion" && propinfoT.Name != @"FechaValidez" && propinfoT.Name != @"IndVigente" && propinfoT.Name != @"IndEliminado")
				{
					object[] parametros = new object[2];
					parametros[0] = propinfoT.Name;
					parametros[1] = propinfoT.GetValue(datos, null);
					object result = (clase.GetMethod(@"ValidarPropiedad").Invoke(metadata, parametros));
					
					if (result != null)
						result.ToString(); //Para evitar el warning del VS o JustCode...
				}
			}
			return errores.ToString();
		}
		
		public void BloquearRegistro(int idRegistro, int idPaginaModulo, string idSessionUsuario)
		{
			TomadoRepositorio repositorioTomado = new TomadoRepositorio(new UnidadDeTrabajo(this.udt.AuditoriaDto, new BD_HC_Tomado_Log()));
			repositorioTomado.BloquearRegistro(idRegistro, idPaginaModulo, idSessionUsuario, this.udt.AuditoriaDto.LoginUsuario, typeof(T).Name);
		}
		
		public void GuardarCambios(D datosVista, AccionDetalle accion)
		{
			string errores = ValidarDatos(datosVista, new Metadata<T>());
			
			if (errores.Length == 0)
			{
				this.udt.IniciarTransaccion();
				Type clase = typeof(T);
				T entidad = (T)Activator.CreateInstance(clase, null);
				
				if (accion == AccionDetalle.Agregar)
				{
					LlenarEntidad(datosVista, ref entidad, accion);
					this.udt.MarcarNuevo(entidad);
				}
				else
				{
					PropertyInfo propinfoU = typeof(D).GetProperty(@"Id");
					entidad = this.ObtenerPorIdT(int.Parse(propinfoU.GetValue(datosVista, null).ToString()));
					LlenarEntidad(datosVista, ref entidad, accion);
					this.udt.MarcarModificado(entidad);
				}
				this.udt.Commit();
			}
			else
				throw new CustomException(string.Format("Se encontraron los siguientes errores: {0}", errores));
		}
		
		public D ObtenerPorId(int id)
		{
			T entidad = this.ObtenerPorIdT(id);
			D dto = (D)Activator.CreateInstance(typeof(D), null);
			LlenarDto(ref dto, entidad);
			return dto;
		}
	}
}