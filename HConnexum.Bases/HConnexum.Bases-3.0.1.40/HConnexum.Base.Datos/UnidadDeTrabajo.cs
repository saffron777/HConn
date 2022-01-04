using System;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml.Serialization;
using HConnexum.Base.Datos.Entity;
using HConnexum.Base.Dtos.ObjetosUtilitariosDto;

namespace HConnexum.Base.Datos
{
	/// <summary>
	/// Clase UnidadDeTrabajo
	/// </summary>
	public sealed class UnidadDeTrabajo : IUnidadDeTrabajo, IDisposable
	{
		/// <summary>
		/// sesion
		/// </summary>
		private ObjectContext sesion;
		/// <summary>
		/// sesionInicial
		/// </summary>
		private readonly ObjectContext sesionInicial;
		/// <summary>
		/// The udt log
		/// </summary>
		private readonly UnidadDeTrabajoLog udtLog;
		/// <summary>
		/// log app
		/// </summary>
		private TB_Log logApp;
		
		/// <summary>
		/// Id de la sesión
		/// </summary>
		public string IdSesion { get; set; }
		
		private readonly AuditoriaDto auditoriaDto;
		
		/// <summary>
		/// Información de auditoría
		/// </summary>
		public AuditoriaDto AuditoriaDto
		{
			get
			{
				return this.auditoriaDto;
			}
		}
		
		/// <summary>
		/// Obtiene  la sesion.
		/// </summary>
		/// <value> sesion.</value>
		public ObjectContext Sesion
		{
			get
			{
				return this.sesion;
			}
		}
		
		/// <summary>
		/// Esta en transaccion
		/// </summary>
		private bool estaEnTransaccion = false;
		
		/// <summary>
		/// Valor para indicar que se esta en transaccion.
		/// </summary>
		/// <value><c>true</c> si [esta en transaccion]; no [esta en transaccion];, <c>false</c>.</value>
		public bool EstaEnTransaccion
		{
			get
			{
				return this.estaEnTransaccion;
			}
		}
		
		/// <summary>
		/// Inicializa una nueva instancia de <see cref="UnidadDeTrabajo"/> clase.
		/// </summary>
		public UnidadDeTrabajo(AuditoriaDto auditoriaDto, ObjectContext sesion)
		{
			this.sesion = sesion;
			this.sesionInicial = this.sesion;
			this.udtLog = new UnidadDeTrabajoLog();
			this.auditoriaDto = auditoriaDto;
			this.IdSesion = auditoriaDto.IdSesion;
		}
		
		/// <summary>
		/// Iniciar  transaccion.
		/// </summary>
		public void IniciarTransaccion()
		{
			this.sesion = this.sesionInicial;
			this.udtLog.IniciarTransaccion();
			this.estaEnTransaccion = true;
		}
		
		/// <summary>
		/// Rollbacks de la Transaccion.
		/// </summary>
		public void Rollback()
		{
			this.sesion = this.sesionInicial;
			this.estaEnTransaccion = false;
		}
		
		/// <summary>
		/// Commit de la Transaccion.
		/// </summary>
		/// <exception cref="System.Data.EntityException">
		/// Unidad de Trabajo no puede hacer commit ya que no está en sesión
		/// u
		/// Ocurrió un error durante el Commit.
		/// </exception>
		/// <exception cref="System.Data.EntitySqlException">Ocurrió un error durante el Commit.</exception>
		public void Commit()
		{
			SwebErrorClient swebErrorClient = new SwebErrorClient();
			try
			{
				if (this.estaEnTransaccion == false)
					throw new EntityException(swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"Commit", ConfigurationManager.AppSettings[@"NombreAplicacion"], @"0014", this.auditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString));
				
				this.sesion.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
				this.udtLog.Commit();
				this.estaEnTransaccion = false;
			}
			catch (System.Data.SqlClient.SqlException exception)
			{
				this.sesion = this.sesionInicial;
				this.estaEnTransaccion = false;
				throw new EntitySqlException(swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"Commit", ConfigurationManager.AppSettings[@"NombreAplicacion"], @"0015", this.auditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString), exception);
			}
			catch (Exception exception)
			{
				this.Rollback();
				this.udtLog.Rollback();
				this.udtLog.IniciarTransaccion();
				
				if (exception.InnerException != null)
				{
					if (exception.InnerException is System.Data.SqlClient.SqlException)
					{
						var sqlEx = (System.Data.SqlClient.SqlException)exception.InnerException;
						this.GuardarLogError(string.Format("{0} Stack Trace: {1}", sqlEx.Message, sqlEx.StackTrace), this.logApp.Tabla);
						swebErrorClient.RegistroTraza(1, int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"Sin Definir", string.Format("{0} Stack Trace: {1}", sqlEx.Message, sqlEx.StackTrace), @"Commit", this.auditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString);
						this.udtLog.Commit();
						throw sqlEx;
					}
				}
				this.GuardarLogError(string.Format("{0} Stack Trace: {1}", exception.Message, exception.StackTrace), this.logApp.Tabla);
				swebErrorClient.RegistroTraza(1, int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"Sin Definir", string.Format("{0} Stack Trace: {1}", exception.Message, exception.StackTrace), @"Commit", this.auditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString);
				this.udtLog.Commit();
				throw new EntityException(swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"Commit", ConfigurationManager.AppSettings[@"NombreAplicacion"], @"0015", this.auditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString), exception);
			}
			finally
			{
				if (swebErrorClient.State != CommunicationState.Closed)
					swebErrorClient.Close();
			}
		}
		
		/// <summary>
		/// Marcar nuevo.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">La entidad.</param>
		public void MarcarNuevo<T>(T entity) where T : class
		{
			this.sesion.AddObject(typeof(T).Name, entity);
			this.GuardarLogApp<T>(entity, @"Nuevo", this.IdSesion, this.auditoriaDto.IpUsuario, this.auditoriaDto.HostName);
		}
		
		/// <summary>
		/// Marcar modificado.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">La entidad.</param>
		/// <exception cref="System.Data.EntityException">la entidad no es de tipo EntityObject.</exception>
		public void MarcarModificado<T>(T entity) where T : class
		{
			SwebErrorClient swebErrorClient = new SwebErrorClient();
			try
			{
				var entidadModificada = entity as EntityObject;
				
				if (entidadModificada == null)
					throw new EntityException(swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"MarcarEliminado", ConfigurationManager.AppSettings[@"NombreAplicacion"], @"0016", this.auditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString));
				
				if (!this.sesion.IsAttachedTo(entity))
					this.sesion.AttachTo(typeof(T).Name, entity);
				
				if (entidadModificada.EntityState != EntityState.Modified)
					this.sesion.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
				
				if (bool.Parse(typeof(T).GetProperty(@"IndEliminado").GetValue(entity, null).ToString()))
					this.GuardarLogApp<T>(entity, @"Eliminado Logico", this.IdSesion, this.auditoriaDto.IpUsuario, this.auditoriaDto.HostName);
				else
					this.GuardarLogApp<T>(entity, @"Modificado", this.IdSesion, this.auditoriaDto.IpUsuario, this.auditoriaDto.HostName);
			}
			finally
			{
				if (swebErrorClient.State != CommunicationState.Closed)
					swebErrorClient.Close();
			}
		}
		
		/// <summary>
		/// Marcar eliminado.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">La entidad.</param>
		/// <exception cref="System.Data.EntityException">la entidad no es de tipo EntityObject.</exception>
		public void MarcarEliminado<T>(T entity) where T : class
		{
			SwebErrorClient swebErrorClient = new SwebErrorClient();
			try
			{
				var entidadModificada = entity as EntityObject;
				
				if (entidadModificada == null)
					throw new EntityException(swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"MarcarEliminado", ConfigurationManager.AppSettings[@"NombreAplicacion"], @"0016", this.auditoriaDto.LoginUsuario, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString));
				
				if (!this.sesion.IsAttachedTo(entity))
					this.sesion.AttachTo(typeof(T).Name, entity);
				
				if (entidadModificada.EntityState != EntityState.Deleted)
					this.sesion.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
				
				this.GuardarLogApp<T>(entity, @"Eliminado Fisico", this.IdSesion, this.auditoriaDto.IpUsuario, this.auditoriaDto.HostName);
			}
			finally
			{
				if (swebErrorClient.State != CommunicationState.Closed)
					swebErrorClient.Close();
			}
		}
		
		/// <summary>
		/// Guardar el log error.
		/// </summary>
		/// <param name="mensajeError">Mensaje error.</param>
		/// <param name="tabla"> tabla.</param>
		private void GuardarLogError(string mensajeError, string tabla)
		{
			this.logApp = new TB_Log();
			this.logApp.IdSesion = this.IdSesion;
			this.logApp.FechaLog = DateTime.Now;
			this.logApp.TransaccionExitosa = false;
			this.logApp.Tabla = tabla;
			this.logApp.IpUsuario = this.auditoriaDto.IpUsuario;
			this.logApp.Mensaje = mensajeError;
			this.udtLog.MarcarNuevo(this.logApp);
		}
		
		/// <summary>
		/// Guardar el log app.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">La entidad.</param>
		/// <param name="Accion">la accion.</param>
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		private void GuardarLogApp<T>(T entity, string accion, string idSesion, string ipUsuario, string hostName)
		{
			if (!entity.GetType().Name.ToString().Equals(@"Tomado"))
			{
				this.logApp = new TB_Log();
				MemoryStream stream = null;
				XmlSerializer serializer = null;
				try
				{
					stream = new MemoryStream();
					serializer = new XmlSerializer(typeof(T));
					serializer.Serialize(stream, entity);
					stream.Position = 0;
					System.Xml.XmlReaderSettings setting = new System.Xml.XmlReaderSettings();
					setting.CheckCharacters = false;
					setting.ConformanceLevel = System.Xml.ConformanceLevel.Fragment;
					System.Xml.XmlReader reader = System.Xml.XmlTextReader.Create(stream, setting);
					reader.MoveToContent();
					this.logApp.RegistroXML = reader.ReadInnerXml();
					reader.Close();
				}
				finally
				{
					if (stream != null)
						stream.Dispose();
				}
				this.logApp.IdRegistro = typeof(T).GetProperty(@"Id").GetValue(entity, null).ToString();
				this.logApp.IdSesion = idSesion;
				this.logApp.FechaLog = DateTime.Now;
				this.logApp.SpEjecutado = typeof(T).Name;
				this.logApp.Tabla = typeof(T).Name;
				this.logApp.IpUsuario = ipUsuario;
				this.logApp.TransaccionExitosa = true;
				this.logApp.Accion = accion;
				this.logApp.HostName = hostName;
				this.udtLog.MarcarNuevo(this.logApp);
			}
		}
		
		#region Implementation of IDisposable
		
		/// <summary>
		/// Realiza tareas definidas por la aplicación asociadas a la liberación o al restablecimiento de recursos no administrados.
		/// </summary>
		public void Dispose()
		{
			if (this.sesion != null)
			{
				if (this.sesion.Connection.State != ConnectionState.Closed)
					this.sesion.Connection.Close();
				
				this.udtLog.Dispose();
				this.sesion.Dispose();
			}
			GC.SuppressFinalize(true);
		}
		
		#endregion
	}
}