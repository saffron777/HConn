using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HConnexum.Base.Presentacion.Interfaz;
using HConnexum.Base.Presentacion.Presentador;
using HConnexum.Infraestructura;
using Telerik.Web.UI;

namespace HConnexum.Base.Vista
{
	public partial class DefaultBase : PaginaBase<PresentadorDefault>, IDefaultBase
	{
		#region Eventos
		
		protected new void Page_Init(object sender, EventArgs e)
		{
			if (!this.UsuarioActual.VerificarRolesPorAplicacion(this.IdAplicacion) == false)
				this.CargarRoles(IdAplicacion);
			
			base.Page_Init(sender, e);
		}
		
		public bool CargarRoles(int pIdAplicacion)
		{
			bool resultado = false;
			using (ServicioAutenticadorClient servicio = new ServicioAutenticadorClient())
			{
				DataSet ds = servicio.ObtenerRolesUsuarioPorAplicacion(this.UsuarioActual.IdUsuarioSuscriptorSeleccionado.ToString().Encriptar(), pIdAplicacion.ToString().Encriptar());
				
				if (ds.Tables[@"Error"] != null)
					throw new CustomException(ds.Tables[@"Error"].Rows[0]["UserMessage"].ToString(), ds.Tables[@"Error"].Rows[0][@"CustomErrorType"].ToString());
				
				if (ds.Tables[0].Rows.Count > 0)
				{
					this.UsuarioActual.ActualizarRolesPorAplicacion(pIdAplicacion, ds);
					resultado = true;
				}
				return resultado;
			}
		}
		
		protected new void Page_Load(object sender, EventArgs e)
		{
			this.presentador = new PresentadorDefault(this);
			this.presentador.MostrarVista(this.IdAplicacion);
			
			if (!this.IsPostBack)
				this.Page.Controls.FindAll<RadNotification>().Where(a => a.ID == @"RadNotification1").First().ShowInterval = ((this.Session.Timeout - 1) * 60000) + 25000;
		}
		
		/// <summary>
		/// Evento que se dispara por un intervalo de tiempo para buscar en BD y mostrar un mensaje al usuario con la cantidad de alertas que tiene el buzon de alertas
		/// </summary>
		/// <param name="sender">Referencia al Objeto que disapara el evento</param>
		/// <param name="e">Argumentos del Evento</param>
		protected void OnCallbackUpdate(object sender, RadNotificationEventArgs e)
		{
		}
		
		protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Argument))
			{
				int idMenu = int.Parse(Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(e.Argument)).Desencriptar());
				ArbolPagina padre = this.ArbolPaginas.ObtenerArbolPaginaNodoPorIdMenu(idMenu);
				this.ElimitarRegistrosTomadosAnidado(padre);
				this.ArbolPaginas.Remove(padre);
			}
		}
		
		#endregion
		
		#region Propiedades
		
		private new PresentadorDefault presentador;
		
		public DataTable Menu
		{
			set
			{
			}
		}
		
		public string TbUsuario
		{
			set
			{
			}
		}
		
		public string TbSuscritor
		{
			set
			{
			}
		}
		
		#endregion
	}
}