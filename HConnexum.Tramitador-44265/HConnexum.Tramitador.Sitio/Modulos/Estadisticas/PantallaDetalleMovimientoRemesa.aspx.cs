using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HConnexum.Infraestructura;
using HConnexum.Tramitador.Presentacion.Interfaz;
using HConnexum.Tramitador.Presentacion.Presentador;
using System.Text;

namespace HConnexum.Tramitador.Sitio.Modulos.Estadisticas
{
	public partial class PantallaDetalleMovimientoRemesa:PaginaDetalleBase, IPantallaDetalleMovimientoRemesa
	{
		#region "Variables Locales"
		int nremesa;
		int idcodexterno;
		int idintermediario;
		string conexionstring;
		PresentadorDetalleMovimientoRemesa presentador;
		#endregion "Variables Locales"
		#region "Propiedades de Presentación"

		public string Asegurado
		{
			set
			{
				asegurado.Text = value;
			}

			get
			{
				return asegurado.Text;
			}
		}

		public string CedulaTitular
		{
			set
			{
				cedulatitular.Text = value;
			}

			get
			{
				return cedulatitular.Text;
			}
		}

		public string Sexo
		{
			set
			{
				sexo.Text = value;
			}

			get
			{
				return sexo.Text;
			}
		}

		public string FechaNacimiento
		{
			set
			{
				fechanacimiento.Text = value;
			}

			get
			{
				return fechanacimiento.Text;
			}
		}

		public string Estado
		{
			set
			{
				estado.Text = value;
			}

			get
			{
				return estado.Text;
			}
		}

		public string PacienteAsegurado
		{
			set
			{
				pacienteasegurado.Text = value;
			}

			get
			{
				return pacienteasegurado.Text;
			}
		}

		public string CedulaAsegurado
		{
			set
			{
				cedulaasegurado.Text = value;
			}

			get
			{
				return cedulaasegurado.Text;
			}
		}

		public string SexoPaciente
		{
			set
			{
				sexopaciente.Text = value;
			}

			get
			{
				return sexopaciente.Text;
			}
		}

		public string FechaNacPaciente
		{
			set
			{
				fechanacpaciente.Text = value;
			}

			get
			{
				return fechanacpaciente.Text;
			}
		}

		public string EstadoPaciente
		{
			set
			{
				estadopaciente.Text = value;
			}

			get
			{
				return estadopaciente.Text;
			}
		}

		public string Parentesco
		{
			set
			{
				parentesco.Text = value;
			}

			get
			{
				return parentesco.Text;
			}
		}

		public string ContratantePoliza
		{
			set
			{
				contratantepoliza.Text = value;
			}

			get
			{
				return contratantepoliza.Text;
			}
		}

		public string Poliza
		{
			set
			{
				poliza.Text = value;
			}

			get
			{
				return poliza.Text;
			}
		}

		public string Certificado
		{
			set
			{
				certificado.Text = value;
			}

			get
			{
				return certificado.Text;
			}
		}

		public string FechaDesde
		{
			set
			{
				fechadesde.Text = value;
			}

			get
			{
				return fechadesde.Text;
			}
		}

		public string FechaHasta
		{
			set
			{
				fechahasta.Text = value;
			}

			get
			{
				return fechahasta.Text;
			}
		}

		public string ContratanteSolicitud
		{
			set
			{
				contratantesolicitud.Text = value;
			}

			get
			{
				return contratantesolicitud.Text;
			}
		}

		public string Proveedor
		{
			set
			{
				proveedor.Text = value;
			}

			get
			{
				return proveedor.Text;
			}
		}

		public string Diagnostico
		{
			set
			{
				diagnostico.Text = value;
			}

			get
			{
				return diagnostico.Text;
			}
		}

		public string CodClave
		{
			set
			{
				codclave.Text = value;
			}

			get
			{
				return codclave.Text;
			}
		}

		public string FechaOcurrencia
		{
			set
			{
				fechaocurrencia.Text = value;
			}

			get
			{
				return fechaocurrencia.Text;
			}
		}

		public string FechaNotificacion
		{
			set
			{
				fechanotificacion.Text = value;
			}

			get
			{
				return fechanotificacion.Text;
			}
		}

		public string FechaLiquidadoEsperaPago
		{
			set
			{
				fechaliquidadoesperapago.Text = value;
			}

			get
			{
				return fechaliquidadoesperapago.Text;
			}
		}

		public string FechaEmisionFactura
		{
			set
			{
				fechaemisionfactura.Text = value;
			}

			get
			{
				return fechaemisionfactura.Text;
			}
		}

		public string FechaRecepcionFactura
		{
			set
			{
				fecharecepcionfactura.Text = value;
			}

			get
			{
				return fecharecepcionfactura.Text;
			}
		}

		public string Status
		{
			set
			{
				status.Text = value;
			}

			get
			{
				return status.Text;
			}
		}

		public string SubCategoria
		{
			set
			{
				subcategoria.Text = value;
			}

			get
			{
				return subcategoria.Text;
			}
		}

		public string NumeroControl
		{
			set
			{
				numerocontrol.Text = value;
			}

			get
			{
				return numerocontrol.Text;
			}
		}

		public string NumeroFactura
		{
			set
			{
				numerofactura.Text = value;
			}

			get
			{
				return numerofactura.Text;
			}
		}

		public string NumeroPoliza
		{
			set
			{
				numeropoliza.Text = value;
			}

			get
			{
				return numeropoliza.Text;
			}
		}

		public string CertificadoSolicitud
		{
			set
			{
				certificadosolicitud.Text = value;
			}

			get
			{
				return certificadosolicitud.Text;
			}
		}

		public string MontoPresupuestoIncial
		{
			set
			{
				montopresupuestoincial.Text = value;
			}

			get
			{
				return montopresupuestoincial.Text;
			}
		}

		public string Deducible
		{
			set
			{
				deducible.Text = value;
			}

			get
			{
				return deducible.Text;
			}
		}

		public string MontoCubierto
		{
			set
			{
				montocubierto.Text = value;
			}

			get
			{
				return montocubierto.Text;
			}
		}

		public string GastosnoCubiertos
		{
			set
			{
				gastosnocubiertos.Text = value;
			}

			get
			{
				return gastosnocubiertos.Text;
			}
		}

		public string MontoSujetoRetencion
		{
			set
			{
				montosujetoretencion.Text = value;
			}

			get
			{
				return montosujetoretencion.Text;
			}
		}

		public string GastosClinicos
		{
			set
			{
				gastosclinicos.Text = value;
			}

			get
			{
				return gastosclinicos.Text;
			}
		}

		public string GastosMedicos
		{
			set
			{
				gastosmedicos.Text = value;
			}

			get
			{
				return gastosmedicos.Text;
			}
		}

		public string PorcentajeRetencion
		{
			set
			{
				porcentajeretencion.Text = value;
			}

			get
			{
				return porcentajeretencion.Text;
			}
		}

		public string Retencion
		{
			set
			{
				retencion.Text = value;
			}

			get
			{
				return retencion.Text;
			}
		}

		public string ParentescoSolicitud
		{
			set
			{
				parentescosolicitud.Text = value;
			}

			get
			{
				return parentescosolicitud.Text;
			}
		}

		public string Liquidador
		{
			set
			{
				liquidador.Text = value;
			}

			get
			{
				return liquidador.Text;
			}
		}

		public int Nremesa
		{
			get
			{
				return nremesa;
			}
		}

		public int IdCodExterno
		{
			get
			{
				if(idcodexterno != null)
					return idcodexterno;
				return 0;
			}
		}

		public string ConexionString
		{
			get
			{
				if(conexionstring != null)
					return conexionstring;
				return string.Empty;
			}
		}

		public int IdIntermediario
		{
			get
			{
				if(idintermediario != null)
					return idintermediario;
				return 0;
			}
		}

		#endregion "Propiedades de Presentación"

		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				presentador = new PresentadorDetalleMovimientoRemesa(this);
				idintermediario = Convert.ToInt32(ExtraerDeViewStateOQueryString(@"intermediario"));
				nremesa = Convert.ToInt32(ExtraerDeViewStateOQueryString(@"nremesa"));
				conexionstring = presentador.ObtenerConexionString(IdIntermediario);
				idcodexterno = presentador.ObtenerIdCodExterno(IdIntermediario);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Page_Load(sender, e);
			if(!IsPostBack)
			{
				presentador.CargarDatosAsegurado(WebConfigurationManager.AppSettings[@"SpDatosPacienteDatosPoliza"], Nremesa, ConexionString);
				presentador.CargarDetalleSolicitud(WebConfigurationManager.AppSettings[@"SpDatosSolicitudMovRemesa"], Nremesa, ConexionString);
			}
		}

		protected void ButtonImprimir_Click(object sender, EventArgs e)
		{
			StringBuilder url = new StringBuilder();
			url.Append(@"~/Modulos/Estadisticas/ReporteDetalleSolicitud.aspx?IdMenu=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdMenu.ToString().Encriptar())) + "&intermediario=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdIntermediario.ToString().Encriptar())) + "&nremesa=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(Nremesa.ToString().Encriptar())) + "&idCodExterno=");
			url.Append(HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(IdCodExterno.ToString().Encriptar())));
			Response.Redirect(url.ToString());
		}
	}
}