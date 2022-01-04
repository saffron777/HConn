using System;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary> 
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
	///<summary>Clase MovimientoLista.</summary>
	public partial class MisActividadesDetalle : PaginaDetalleBase, IMisActividadesDetalle
	{
		#region "Variables Locales"
		///<summary>Variable presentador MovimientoPresentadorDetalle.</summary>
		MisActividadesPresentadorDetalle presentador;
        string ImgChat;
		#endregion "Variables Locales"
       
		#region "Eventos de la Página"
		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new MisActividadesPresentadorDetalle(this);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento de carga de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				base.Page_Load(sender, e);
				if(!this.Page.IsPostBack)
				{
					presentador.MostrarVista();
					int id = -1;
					if (int.TryParse(this.NroCaso, out id))
					{
						controlChat.CasoId = id;
						controlChat.MovimientoId = this.Id;
						controlChat.EnvioSuscriptorId = UsuarioActual.SuscriptorSeleccionado.Id;
						controlChat.Remitente = string.Format("{0} {1}", UsuarioActual.DatosBase.Nombre1, UsuarioActual.DatosBase.Apellido1).Trim();
						controlChat.CreacionUsuario = UsuarioActual.IdUsuarioSuscriptorSeleccionado;
					}
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento pre visualización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			base.Page_PreRender(sender, e);
		}
		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		public int Id
		{
			get { return base.Id; }
			set { base.Id = value; }
		}
		public string Servicio
		{
            get { return lblServicio.Text; }
			set { lblServicio.Text = value; }
		}
		public string NroCaso
		{
			get { return lblNroCaso.Text.Trim(); }
			set { lblNroCaso.Text = value; }
		}
		public string EstatusCaso
		{
			set { lblEstatusCaso.Text = value; }
		}
		public string FechaSolicitud
		{
			set { lblFechaSolicitud.Text = value; }
		}
		public string Solicitante
		{
			set { lblSolicitante.Text = value; }
		}
		public string MovilSolicitante
		{
			set { lblMovilSolicitante.Text = value; }
		}
		
		public string Estatus
		{
			set { lblEstatus.Text = value; }
		}
		public string NombrePaso
		{
			set { lblNombrePaso.Text = value; }
		}
		public string FechaCreacion
		{
			set { lblFechaCreacion.Text = value; }
		}
		public string UsuarioCreacion
		{
			set { lblUsuarioCreacion.Text = value; }
		}
		public string Descripcion
		{
			set { lblDescripcion.Text = value; }
		}
		public string FechaModificacion
		{
			set { lblFechaModificacion.Text = value; }
		}
		public string UsuarioModificacion
		{
			set { lblUsuarioModificacion.Text = value; }
		}
		public string FechaProceso
		{
			set { lblFechaProceso.Text = value; }
		}
		public string Observaciones
		{
			set { lblObservaciones.Text = value; }
		}
		public bool IndObligatorio
		{
			set { cmdOmitir.Enabled = value; }
		}
        public string EdadBeneficiario
        {
            set { lblEdadSolicitante.Text = value; }
        }
        public string SexoBeneficiario
        {
            set { lblSexoSolicitante.Text = value; }
        }
        public string Intermediario
        {
            set { lblIntermediario.Text = value; }
        }
        public string Contratante
        {
            set { lblContratante.Text = value; }
        }
        public bool habilitoChat
        {
            set { cmdChat.Visible = value; }
        }
        public string imgChat
        {
            set 
            { ImgChat = value;
          //  cmdChat.ImageUrl = "~/Imagenes/" + ImgChat + ".png";
            }
        }

        public bool atender
        {
            set {
                cmdAtender.Visible = value;
            }
        }

		#endregion "Propiedades de Presentación"

		protected void cmdAtender_Click(object sender, EventArgs e)
		{
			try
			{
                Session["Servicio"] = Servicio;
                if (this.presentador.AtenderActividad())
                    Response.Redirect(@"~/Modulos/Orquestador/PantallaContenedora.aspx?idMenu=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&idmovimiento=" + HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(Id.ToString().Encriptar())),true);
                else
                {
                    string radalertscript = "<script language='javascript'>function f(){changeTextRadAlert();radalert('Actividad no disponible para ser atendida', 380, 50,";
                    radalertscript += "''); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", radalertscript);
                }
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, "Error al Mostrar la data de la Aplicacion");
				Trace.Warn("Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings["MensajeExcepcion"];
			}
		}

		protected void btnAceptar_Click(object sender, EventArgs e)
		{
			int idUsuarioSuscriptor = UsuarioActual.IdUsuarioSuscriptorSeleccionado;
			presentador.OmitirMovimiento(Id, idUsuarioSuscriptor, txtObservacion.Text);
		}
	}
}