using System;
using System.Linq;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data;
using System.Web;
using HConnexum.Infraestructura;
using HConnexum.Base.Datos.Entity;
using System.ServiceModel;
using System.Configuration;

namespace HConnexum.Base.Datos
{
	/// <summary>
	/// Clase UnidadDeTrabajoLog
	/// </summary>
	public sealed class UnidadDeTrabajoLog : IDisposable
	{
		/// <summary>
		/// The sesion
		/// </summary>
		private ObjectContext sesion;
		
		/// <summary>
		/// Obtener sesion.
		/// </summary>
		/// <value>sesion.</value>
		public ObjectContext Sesion
		{
			get { return sesion; }
		}
		
		/// <summary>
		/// esta en transaccion
		/// </summary>
		private bool estaEnTransaccion = false;
		
		/// <summary>
		/// Obtener el valor que indica si [esta en transaccion].
		/// </summary>
		/// <value><c>true</c> si [esta en transaccion]; de otra manera, <c>false</c>.</value>
		public bool EstaEnTransaccion
		{
			get { return estaEnTransaccion; }
		}
		
		/// <summary>
		/// Inicializa una nueva instancia de <see cref="UnidadDeTrabajoLog"/> clase.
		/// </summary>
		public UnidadDeTrabajoLog()
		{
			sesion = new BD_HC_Tomado_Log();
		}
		
		/// <summary>
		/// Iniciar la transaccion.
		/// </summary>
		public void IniciarTransaccion()
		{
			if(this.sesion != null && this.sesion.Connection != null && !string.IsNullOrEmpty(this.sesion.Connection.ConnectionString))
			{
				using(EntityConnection connection = new EntityConnection(this.sesion.Connection.ConnectionString))
				{ this.sesion = new BD_HC_Tomado_Log(connection); }
			}
			else
				this.sesion = new BD_HC_Tomado_Log();
			this.estaEnTransaccion = true;
		}
		
		/// <summary>
		/// Rollbacks .
		/// </summary>
		public void Rollback()
		{
			sesion = new BD_HC_Tomado_Log();
			estaEnTransaccion = false;
		}
		
		/// <summary>
		/// Commits .
		/// </summary>
		/// <exception cref="System.Data.EntityException">Unidad de Trabajo no puede hacer commit ya que no está en sesión</exception>
		public void Commit()
		{
			SwebErrorClient swebErrorClient = new SwebErrorClient();
			try
			{
				if(estaEnTransaccion == false)
					throw new EntityException(swebErrorClient.ManejadorError(int.Parse(ConfigurationManager.AppSettings[@"IdAplicacion"]), @"Commit", ConfigurationManager.AppSettings[@"NombreAplicacion"], @"0014", string.Empty, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.Url.OriginalString));
				
				sesion.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
				estaEnTransaccion = false;
			}
			catch(Exception exception)
			{
				this.Rollback();
				Errores.EscribirTraza(exception, @"HC Gestion Documental", @"Commit Log");
			}
			finally
			{
				if(swebErrorClient.State != CommunicationState.Closed)
					swebErrorClient.Close();
			}
		}
		
		/// <summary>
		/// Marcar the nuevo.
		/// </summary>
		/// <param name="entity"> entity.</param>
		public void MarcarNuevo(TB_Log entity)
		{
			sesion.AddObject(typeof(TB_Log).Name, entity);
		}
		
		#region Implementation of IDisposable
		
		/// <summary>
		/// Realiza tareas definidas por la aplicación asociadas a la liberación o al restablecimiento de recursos no administrados.
		/// </summary>
		public void Dispose()
		{
			if(sesion != null)
			{
				if(sesion.Connection.State != ConnectionState.Closed)
					sesion.Connection.Close();
				
				sesion.Dispose();
			}
			GC.SuppressFinalize(true);
		}
		
		#endregion
	}
}
