using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using HConnexum.Configurador.Presentacion.Interfaz;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Presentador;
using HConnexum.Tramitador.Sitio;
using HConnexum.Tramitador.Negocio;

///<summary>Namespace que engloba la vista detalle de la capa web HC_Configurador.</summary> 
namespace HConnexum.Tramitador.Sitio.Modulos.Estructura
{
	///<summary>Clase CasoAgrupacionLista.</summary>
	public partial class CasoAgrupacionDetalle : PaginaDetalleBase, ICasoAgrupacionDetalle
	{
		#region "Variables Locales"
		///<summary>Variable presentador CasoAgrupacionPresentadorDetalle.</summary>
		CasoAgrupacionPresentadorDetalle presentador;
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
				this.presentador = new CasoAgrupacionPresentadorDetalle(this);
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
					this.CultureDatePicker();
                    presentador.LlenarListBox();
                    if(this.Accion == AccionDetalle.Modificar)
						this.presentador.BloquearRegistro(Id, IdPaginaModulo, UsuarioActual.IdSesion);
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
			if(this.Accion == AccionDetalle.Ver) this.BloquearControles(true);
			this.MostrarBotones(this.cmdGuardar, this.cmdGuardaryAgregar, this.cmdLimpiar, this.Accion);
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				this.Page.Controls.isValidSpecialCharacterEmptySpace();
				this.presentador.GuardarCambios(this.Accion);
				if(ArbolPaginas.ArbolPaginaActualIsNode())
					this.Response.Redirect(@"~" + ArbolPaginas.ObtenerArbolPaginaPadre().UrlRedirect);
				else
					this.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "CerrarVentana", "setTimeout('cerrarVentana()', 0);", true);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		///<summary>Evento de comando que se dispara cuando se hace click en el boton guardar y agregar.</summary>
		///<param name="sender">Referencia al objeto que provocó el evento.</param>
		///<param name="e">Argumentos del evento.</param>
		protected void cmdGuardaryAgregar_Click(object sender, EventArgs e)
		{
			try
			{
				this.Page.Controls.isValidSpecialCharacterEmptySpace();
				this.presentador.GuardarCambios(this.Accion);
				this.LimpiarControles();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}
		#endregion "Eventos de la Página"

		#region "Propiedades de Presentación"
		public int Id
		{
		    get { return base.Id; }
		    set { base.Id = value; }
		}


        public IList<CasoAgrupacionDTO> CasoAgrupacion
        {
            get
            {
                return (IList<CasoAgrupacionDTO>)this.Session["TAOCasoAgrupacionDTO"];
            }
            set
            {
                if (this.Session["TAOCasoAgrupacionDTO"] == null)
                    this.Session.Add("TAOCasoAgrupacionDTO", value);
                else
                    this.Session["TAOCasoAgrupacionDTO"] = value;
            }
        }

        public Dictionary<int, string> CasosAsociados
        {
            get
            {
                Dictionary<int, string> tempDic = new Dictionary<int, string>();
                for (int i = 0; i < this.rlbListaCasosAgrupacionesAsociados.Items.Count; i++)
                    tempDic.Add(int.Parse(this.rlbListaCasosAgrupacionesAsociados.Items[i].Value), this.rlbListaCasosAgrupacionesAsociados.Items[i].Text);
                return tempDic;
            }
        }

        public IList<CasoAgrupacionDTO> ListBoxCasosAgrupacionesNoAsociados
        {
            set
            {
                this.rlbListaCasosAgrupaciones.DataSource = value;
                this.rlbListaCasosAgrupaciones.DataBind();
            }
        }

        public IList<CasoAgrupacionDTO> ListBoxCasosAgrupacionesAsociados
        {
            set
            {
                this.CasoAgrupacion = value;
                this.rlbListaCasosAgrupacionesAsociados.DataSource = value;
                this.rlbListaCasosAgrupacionesAsociados.DataBind();
            }
        }
		#endregion "Propiedades de Presentación"
	}
}