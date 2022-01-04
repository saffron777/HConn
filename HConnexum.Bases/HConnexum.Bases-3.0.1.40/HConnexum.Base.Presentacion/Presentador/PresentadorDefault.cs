using System;
using System.Data;
using System.Linq;
using HConnexum.Base.Presentacion.Interfaz;
using HConnexum.Infraestructura;

namespace HConnexum.Base.Presentacion.Presentador
{
	public class PresentadorDefault
	{
		private readonly IDefaultBase vista;
		
		public PresentadorDefault(IDefaultBase vista)
		{
			this.vista = vista;
		}
		
		public void MostrarVista(int idAplicacion)
		{
			using (ServicioAutenticadorClient servicio = new ServicioAutenticadorClient())
			{
				DataSet ds = servicio.ObtenerMenu(idAplicacion.ToString().Encriptar(), this.vista.UsuarioActual.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar());
				
				if (ds.Tables[@"Error"] != null)
					throw new CustomException(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString(), ds.Tables[@"Error"].Rows[0][@"CustomErrorType"].ToString());
				
				this.vista.Menu = ds.Tables[0];
				this.vista.TbUsuario = string.Format("{0} {1}", this.vista.UsuarioActual.DatosBase.Nombre1, this.vista.UsuarioActual.DatosBase.Apellido1);
				this.vista.TbSuscritor = string.Format("{0} - ", this.vista.UsuarioActual.SuscriptorSeleccionado.Nombre);
			}
		}
	}
}