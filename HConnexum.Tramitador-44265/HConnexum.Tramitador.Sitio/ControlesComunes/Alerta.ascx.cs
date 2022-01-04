using System;
using System.Web.UI.WebControls;
using HConnexum.Tramitador.Datos;
using Telerik.Web.UI;
using System.Linq;
using System.Data;
using HConnexum.Seguridad;

namespace HConnexum.Tramitador.Sitio.ControlesComunes
{
	public partial class Alerta : System.Web.UI.UserControl 
	{
		#region Propiedades Publicas
        public int User_suscriptor { get; set; }
		/// <summary>
		/// Width of the Alert Window
		/// </summary>
		public Unit Width { get; set; }
        readonly UnidadDeTrabajo udt = new UnidadDeTrabajo();

		/// <summary>
		/// Title of the Alert Window
		/// </summary>
		public String Title { get; set; }
		#endregion

		#region Metodos Protected
		/// <summary>
		/// Carga de Propiedades del Control
		/// </summary>
		/// <param name="sender">Referencia al Objeto que disapara el evento</param>
		/// <param name="e">Argumentos del Evento</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			NotificacionAlerta.Width = Width;
			NotificacionAlerta.Title = Title;
		}

		/// <summary>
		/// Evento que se dispara por un intervalo de tiempo para buscar en BD y mostrar un mensaje al usuario
		/// </summary>
		/// <param name="sender">Referencia al Objeto que disapara el evento</param>
		/// <param name="e">Argumentos del Evento</param>
		protected void OnCallbackUpdate(object sender, RadNotificationEventArgs e)
		{
			//int newMsgs;
            //HConnexum.Tramitador.Datos.BuzonAlertaRepositorio buzon = new HConnexum.Tramitador.Datos.BuzonAlertaRepositorio(udt);

		  //newMsgs = buzon.LlenarControlALertas(User_suscriptor).Count();
		  //if (newMsgs > 0)
		  //{
		  //    NotificacionAlerta.EnableShadow = true;
		  //    this.lblMessage.Text = string.Format("<a href=" + "'" + "Modulos/Alertas/BuzonAlertaLista.aspx?" + "'" + " onclick='AbrirVentanas(this); return false;'>Tienes {0} mensajes nuevos!</a>", newMsgs);
		  //    NotificacionAlerta.Value = newMsgs.ToString();
		  //}
		  //else NotificacionAlerta.EnableShadow = false;
		}
		#endregion
	}
}