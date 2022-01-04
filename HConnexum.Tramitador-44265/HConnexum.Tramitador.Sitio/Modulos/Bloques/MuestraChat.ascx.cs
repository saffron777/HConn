using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Presentacion;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using HConnexum.Tramitador.Negocio;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class MuestraChat : UserControlBase, IBuzonChat
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string _mensaje = null;
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string _NAMESPACE = "HConnexum.Tramitador.Sitio.Modulos.Bloques";
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		///<summary>Objeto 'Presentador' asociado al control Web de usuario.</summary>
		BuzonChatPresentador presentador;

		public string Mensaje
		{
			get { return txtMensaje.Text; }
			set { txtMensaje.Text = value; }
		}
		public int CasoId
		{
			get
			{
				int id = -1;
				int.TryParse(hidCasoId.Value, out id);
				return id;
			}
			set
			{
				hidCasoId.Value = value.ToString();
			}
		}
		public int? MovimientoId
		{
			get
			{
				int id = -1;
				int.TryParse(hidMovimientoId.Value, out id);
				return id;
			}
			set
			{
				hidMovimientoId.Value = value.ToString();
			}
		}
		public int? EnvioSuscriptorId
		{
			get
			{
				int id = -1;
				int.TryParse(hidEnvioSuscriptorId.Value, out id);
				return id;
			}
			set
			{
				hidEnvioSuscriptorId.Value = value.ToString();
			}
		}
		public string Remitente
		{
			get { return hidRemitente.Value; }
			set { hidRemitente.Value = value; }
		}
		public int? CreacionUsuario
		{
			get
			{
				int id = -1;
				int.TryParse(hidCreadoPor.Value, out id);
				return id;
			}
			set
			{
				hidCreadoPor.Value = value.ToString();
			}
		}
		///<summary>Lista de mensajes.</summary>
		public IEnumerable<BuzonChatDTO> Mensajes
		{
			set
			{
				rgBitacora.DataSource = value;
				rgBitacora.DataBind();
			}
		}
		#endregion

		#region M A N E J A D O R E S   D E   E V E N T O S
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.Page_Init()", _NAMESPACE);
                
				base.Page_Init(sender, e);
               
				this.presentador = new BuzonChatPresentador(this);
               
                 
                
			}
			catch (Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.Page_Load()", _NAMESPACE);

				base.Page_Load(sender, e);
				if (!IsPostBack)
					presentador.MostrarVista();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
		protected void btnEnviar_Click(object sender, EventArgs e)
		{
			try
			{
                if (!string.IsNullOrWhiteSpace(txtMensaje.Text.Trim()))
                {
                    presentador.GuardarCambios();
                    txtMensaje.Text = "";
                    presentador.MostrarVista();
                   
                }
			}
			catch (Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}
		#endregion

		#region M É T O D O S   P R I V A D O S
		/// <summary>Registra la auditoría en la aplicación.</summary>
		/// <param name="pException">Excepción a registar.</param>
		/// <param name="pMensaje">Mensaje a registrar.</param>
		private void Auditoria(Exception pException, string pMensaje)
		{
			/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
			string TRAZA_CATEGORIA_ERROR = "Error";
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.Auditoria(pException = {1}, pMensaje = {2})", _NAMESPACE, pException
					, pMensaje);

				Debug.WriteLine(pException.ToString());
				Infraestructura.Errores.ManejarError(pException, pMensaje);
				if (pException.InnerException != null)
					Trace.Warn(TRAZA_CATEGORIA_ERROR, pMensaje + pException.InnerException.Message);
				Trace.Warn(TRAZA_CATEGORIA_ERROR, pMensaje, pException);
				this.Errores = presentador.MostrarMensaje(TiposMensaje.Error_Generico);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				Infraestructura.Errores.ManejarError(ex, _mensaje);
				if (ex.InnerException != null)
					Trace.Warn(TRAZA_CATEGORIA_ERROR, _mensaje + ex.InnerException.Message);
				Trace.Warn(TRAZA_CATEGORIA_ERROR, _mensaje, ex);
				this.Errores = presentador.MostrarMensaje(TiposMensaje.Error_Generico);
			}
		}
		#endregion
	}
}