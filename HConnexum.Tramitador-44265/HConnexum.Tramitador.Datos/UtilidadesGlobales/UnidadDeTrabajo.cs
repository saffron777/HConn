using System;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Datos
{
	public sealed class UnidadDeTrabajo : IUnidadDeTrabajo, IDisposable
	{
		private ObjectContext sesion;
		private readonly UnidadDeTrabajoLog udtLog;

		private Negocio.Log LogApp;
		public ObjectContext Sesion
		{
			get
			{
				return this.sesion;
			}
		}

		private bool estaEnTransaccion = false;
		public bool EstaEnTransaccion
		{
			get
			{
				return this.estaEnTransaccion;
			}
		}

		public UnidadDeTrabajo()
		{
			this.sesion = new BD_HC_Tramitador();
			this.udtLog = new UnidadDeTrabajoLog();
		}

		public UnidadDeTrabajo(EntityConnection connection)
		{
			this.sesion = new BD_HC_Tramitador(connection);
			this.udtLog = new UnidadDeTrabajoLog(connection);
		}

		public UnidadDeTrabajo(string connectionString)
		{
			this.sesion = new BD_HC_Tramitador(connectionString);
			this.udtLog = new UnidadDeTrabajoLog(connectionString);
		}

		public void IniciarTransaccion()
		{
			if(this.sesion != null && this.sesion.Connection != null && !string.IsNullOrEmpty(this.sesion.Connection.ConnectionString))
			{
				EntityConnection connection = new EntityConnection(this.sesion.Connection.ConnectionString);
				this.sesion = new BD_HC_Tramitador(connection);
				this.udtLog.IniciarTransaccion();
			}
			else
			{
				this.sesion = new BD_HC_Tramitador();
				this.udtLog.IniciarTransaccion();
			}
			this.estaEnTransaccion = true;
		}

		public void Rollback()
		{
			this.sesion = new BD_HC_Tramitador();
			this.estaEnTransaccion = false;
		}

		public void Commit()
		{
			try
			{
				if(this.estaEnTransaccion == false)
					throw new EntityException("Unidad de Trabajo no puede hacer commit ya que no está en sesión");
				this.sesion.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
				this.udtLog.Commit();
				this.estaEnTransaccion = false;
			}
			catch(System.Data.SqlClient.SqlException exception)
			{
				this.sesion = new BD_HC_Tramitador();
				this.estaEnTransaccion = false;
				throw new EntitySqlException("Ocurrió un error durante el Commit.", exception);
			}
			catch(Exception exception)
			{
				this.Rollback();
				this.udtLog.Rollback();
				this.udtLog.IniciarTransaccion();
                if (exception.InnerException != null)
                {
                    if (exception.InnerException is System.Data.SqlClient.SqlException)
                    {
                        var sqlEx = (System.Data.SqlClient.SqlException)exception.InnerException;
                        this.GuardarLogError(string.Format("{0} Stack Trace: {1}", sqlEx.Message, sqlEx.StackTrace), this.LogApp.Tabla);
                        this.udtLog.Commit();
                        throw sqlEx;
                    }
                }
                this.GuardarLogError(string.Format("{0} Stack Trace: {1}", exception.Message, exception.StackTrace), this.LogApp.Tabla);
                this.udtLog.Commit();
				throw new EntityException("Ocurrió un error durante el Commit.", exception);
			}
		}

		public void MarcarNuevo<T>(T entity) where T : class
		{
			this.sesion.AddObject(typeof(T).Name.Pluralizar(), entity);
			this.GuardarLogApp<T>(entity, "Nuevo");
		}

		public void MarcarModificado<T>(T entity) where T : class
		{
			var entidadModificada = entity as EntityObject;
			if(entidadModificada == null)
				throw new EntityException("la entidad no es de tipo EntityObject.");
			if(!this.sesion.IsAttachedTo(entity))
				this.sesion.AttachTo(typeof(T).Name.Pluralizar(), entity);
			if(entidadModificada.EntityState != EntityState.Modified)
				this.sesion.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
			if(bool.Parse(typeof(T).GetProperty("IndEliminado").GetValue(entity, null).ToString()))
				this.GuardarLogApp<T>(entity, "Eliminado Logico");
			else
				this.GuardarLogApp<T>(entity, "Modificado");
		}

		public void MarcarEliminado<T>(T entity) where T : class
		{
			var entidadModificada = entity as EntityObject;
			if(entidadModificada == null)
				throw new EntityException("la entidad no es de tipo EntityObject.");
			if(!this.sesion.IsAttachedTo(entity))
				this.sesion.AttachTo(typeof(T).Name.Pluralizar(), entity);
			if(entidadModificada.EntityState != EntityState.Deleted)
				this.sesion.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
			this.GuardarLogApp<T>(entity, "Eliminado Fisico");
		}

		private void GuardarLogError(string mensajeError, string tabla)
		{
			this.LogApp = new Negocio.Log();
			this.LogApp.IdSesion = UsuarioActual.IdSesion;
			this.LogApp.FechaLog = DateTime.Now;
			this.LogApp.TransaccionExitosa = false;
			this.LogApp.Tabla = tabla;
			this.LogApp.IpUsuario = HttpContext.Current.Request.UserHostAddress;
			this.LogApp.Mensaje = mensajeError;
			this.udtLog.MarcarNuevo(this.LogApp);
		}

		private void GuardarLogApp<T>(T entity, string Accion)
		{

            if (!entity.GetType().Name.ToString().Equals("Tomado"))
            {
                this.LogApp = new Negocio.Log();
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stream, entity);
                    stream.Position = 0;
                    System.Xml.XmlReaderSettings setting = new System.Xml.XmlReaderSettings();
                    setting.CheckCharacters = false;
                    setting.ConformanceLevel = System.Xml.ConformanceLevel.Fragment;
                    System.Xml.XmlReader reader = System.Xml.XmlTextReader.Create(stream, setting);
                    reader.MoveToContent();
                    this.LogApp.RegistroXML = reader.ReadInnerXml();
                    reader.Close();
                }
                this.LogApp.IdRegistro = typeof(T).GetProperty(@"Id").GetValue(entity, null).ToString();
                if (UsuarioActual.IdSesion != null)
                    this.LogApp.IdSesion = UsuarioActual.IdSesion;
                this.LogApp.FechaLog = DateTime.Now;
                this.LogApp.SpEjecutado = typeof(T).Name.Pluralizar();
                this.LogApp.Tabla = typeof(T).Name.Pluralizar();
                this.LogApp.IpUsuario = HttpContext.Current.Request.UserHostAddress;
                this.LogApp.TransaccionExitosa = true;
                this.LogApp.Accion = Accion;
                this.LogApp.HostName = HttpContext.Current.Request.Url.OriginalString;
                this.udtLog.MarcarNuevo(this.LogApp); 
            }
		}

		public static HConnexum.Seguridad.UsuarioActual UsuarioActual
		{
			get
			{
				if(HttpContext.Current.Session[@"UsuarioActual"] != null)
					return (HConnexum.Seguridad.UsuarioActual)HttpContext.Current.Session[@"UsuarioActual"];
				return null;
			}
		}

		#region Implementation of IDisposable
		public void Dispose()
		{
			if(this.sesion != null)
			{
				if(this.sesion.Connection.State != ConnectionState.Closed)
					this.sesion.Connection.Close();
				this.sesion.Dispose();
			}
			GC.SuppressFinalize(true);
		}
		#endregion
	}
}