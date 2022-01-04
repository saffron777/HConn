using System;
using System.Linq;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Web;
using HConnexum.Tramitador.Negocio;
using System.Data;
using HConnexum.Infraestructura;

namespace HConnexum.Tramitador.Datos
{
	public sealed class UnidadDeTrabajoLog : IDisposable
	{

		private ObjectContext sesion;
		public ObjectContext Sesion
		{
			get { return sesion; }
		}

		private bool estaEnTransaccion = false;
		public bool EstaEnTransaccion
		{
			get { return estaEnTransaccion; }
		}

		public UnidadDeTrabajoLog()
		{
			sesion = new BD_HC_Tramitador();
		}

		public UnidadDeTrabajoLog(EntityConnection connection)
		{
			sesion = new BD_HC_Tramitador(connection);
		}

		public UnidadDeTrabajoLog(string connectionString)
		{
			sesion = new BD_HC_Tramitador(connectionString);
		}

		public void IniciarTransaccion()
		{
			if(this.sesion != null && this.sesion.Connection != null && !string.IsNullOrEmpty(this.sesion.Connection.ConnectionString))
			{
				EntityConnection connection = new EntityConnection(this.sesion.Connection.ConnectionString);
				this.sesion = new BD_HC_Tramitador(connection);
			}
			else
				this.sesion = new BD_HC_Tramitador();
			this.estaEnTransaccion = true;
		}

		public void Rollback()
		{
			sesion = new BD_HC_Tramitador();
			estaEnTransaccion = false;
		}

		public void Commit()
		{
			try
			{
				if(estaEnTransaccion == false)
					throw new EntityException("Unidad de Trabajo no puede hacer commit ya que no está en sesión");
				sesion.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
				estaEnTransaccion = false;
			}
			catch(Exception exception)
			{
				this.Rollback();
				Errores.EscribirTraza(exception, @"HC tramitador", @"Commit Log");
				HttpContext.Current.Trace.Warn("Error", "Commit Log", exception);
			}
		}

		public void MarcarNuevo(Negocio.Log entity)
		{
			sesion.AddObject(typeof(Negocio.Log).Name.Pluralizar(), entity);
		}

		#region Implementation of IDisposable
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
