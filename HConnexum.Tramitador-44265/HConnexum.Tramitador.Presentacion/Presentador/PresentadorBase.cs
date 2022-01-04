using System;
using System.Web;
using HConnexum.Tramitador.Datos;
using HConnexum.Tramitador.Negocio;
using HConnexum.Infraestructura;
using Microsoft.Practices.Unity;
using System.Data;
using System.Text;

namespace HConnexum.Tramitador.Presentacion
{
	public class PresentadorBase<T>
	{
		public IUnityContainer container;
		public IUnidadDeTrabajo unidadDeTrabajo;

		public string GetType()
		{
			return typeof(T).Name.Pluralizar();
		}

		public PresentadorBase()
		{
			this.unidadDeTrabajo = new UnidadDeTrabajo();
		}

		public string ObtenerNombreUsuario(int? id)
		{
			UsuarioRepositorio repositorio = new UsuarioRepositorio(this.unidadDeTrabajo);
			if (id != null && id > 0)
			{
				UsuarioDTO usuario = repositorio.ObtenerUsuarioPorIdUsuarioSuscriptor(id.Value);
				if(usuario != null)
					return usuario.LoginUsuario;
			}
			return string.Empty;
		}

		public void BloquearRegistro(int idRegistro, int idPaginaModulo, int idSessionUsuario)
		{
			TomadoRepositorio repositorio = new TomadoRepositorio(this.unidadDeTrabajo);
			if (repositorio.ObtenerTomado(typeof(T).Name.Pluralizar(), idRegistro, idSessionUsuario) == null)
			{
				this.unidadDeTrabajo.IniciarTransaccion();
				Tomado tomado = new Tomado();
				tomado.IdPaginaModulo = idPaginaModulo;
				tomado.Tabla = typeof(T).Name.Pluralizar();
				tomado.IdRegistro = idRegistro;
				tomado.IdSesionUsuario = idSessionUsuario;
				tomado.FechaTomado = DateTime.Now;
				this.unidadDeTrabajo.MarcarNuevo(tomado);
				this.unidadDeTrabajo.Commit();
			}
		}

		//TODO: QUitar
		public void EliminarRegistroTomado(int IdRegistro, int IdPaginaModulo, int IdSessionUsuario)
		{
			this.unidadDeTrabajo.IniciarTransaccion();
			TomadoRepositorio repositorio = new TomadoRepositorio(this.unidadDeTrabajo);
			Tomado tomado = repositorio.ObtenerTomado(typeof(T).Name.Pluralizar(), IdRegistro, IdSessionUsuario);
			if (tomado != null)
				repositorio.Eliminar(tomado);
			this.unidadDeTrabajo.Commit();
		}
		//TODO: Quitar
		public void EliminarRegistroTomadoAjax(string Argument)
		{
			string[] args = Argument.Split(';');
			if(args.Length == 3)
			{
				string Tabla = args[0];
				int IdRegistro = int.Parse(args[1]);
				int IdSessionUsuario = int.Parse(args[2]);
				this.unidadDeTrabajo.IniciarTransaccion();
				TomadoRepositorio repositorio = new TomadoRepositorio(this.unidadDeTrabajo);
				Tomado tomado = repositorio.ObtenerTomado(Tabla, IdRegistro, IdSessionUsuario);
				if(tomado != null)
					repositorio.Eliminar(tomado);
				this.unidadDeTrabajo.Commit();
			}
		}

		//TODO: QUITAR
		public string ObtenerAjaxArguments(int IdRegistro, int IdSessionUsuario)
		{
			return typeof(T).Name.Pluralizar() + @";" + IdRegistro.ToString() + @";" + IdSessionUsuario.ToString();
		}

		public void GenerarColumnEncriptada(ref DataTable tabla, string nombreColumnEncriptar, string nombreAddColumn)
		{
			if((tabla != null) && (tabla.Columns.Contains(nombreColumnEncriptar)))
			{
				if(!tabla.Columns.Contains(nombreAddColumn))
					tabla.Columns.Add(nombreAddColumn);
				for(int i = 0; i < tabla.Rows.Count; i++)
					tabla.Rows[i][nombreAddColumn] = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(tabla.Rows[i][nombreColumnEncriptar].ToString().Encriptar()));
			}
		}
	}
}