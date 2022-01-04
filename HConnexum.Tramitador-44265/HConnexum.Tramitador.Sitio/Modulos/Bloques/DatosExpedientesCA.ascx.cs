using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class DatosExpedientesCA : UserControlListaBase, IDatosExpedientesCA
	{
		#region "Variables"
		/// <summary>Nombre de la categoría 'Error' de la traza Web.</summary>
		private const string TRaceWarnCategoryErrorNombre = @"Error";
		/// <summary>Mensaje de error genérico.</summary>
		private string errorMensajeGenerico = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
		/// <summary>Nombre de la aplicación bajo la cual registrar un evento en el Registro de Eventos de Windows 
		/// (event log).</summary>
		private const string EVentoRegistroAplicacionNombre = @"HC-Tramitador";
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string NAmespace = @"HConnexum.Tramitador.Presentacion.Presentador.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string mensaje = null;
		/// <summary>Cadena de conexión a la base de datos de Gestor.</summary>
		private string conexionCadena = null;
		private string configCAExcepcionMensaje = @"MensajeExcepcion";

		PresentadorDatosExpedienteCA presentador;
		#endregion "Variables"

		#region "Propiedades de Interfaz"
		public string Sintomas
		{
			get
			{
				return Sintomastxt.Text;
			}
			set
			{
				Sintomastxt.Text = value;
			}
		}

		public string Clave
		{
			get
			{
				return txtClave.Text;
			}
			set
			{
				txtClave.Text = value;
			}
		}

		public string UltimoMovimientoHecho
		{
			get
			{
				return TipoMov.Text;
			}
			set
			{
				TipoMov.Text = value;
			}
		}

		public string Categoria
		{
			get
			{
				return NomTipoServicio.Text;
			}
			set
			{
				NomTipoServicio.Text = value;
			}
		}

		public string FechaSolicitud
		{
			get
			{
				return fechasolicitud.Text;
			}
			set
			{
				fechasolicitud.Text = value;
			}
		}

		public string Responsable
		{
			get
			{
				return txtResponsable.Text;
			}
			set
			{
				txtResponsable.Text = value;
			}
		}


		public string DiasHospitalizacion
		{
			get
			{
				return NumDiasHosp.Text;
			}
			set
			{
				NumDiasHosp.Text = value;
			}
		}

		public IEnumerable<PolizasDTO> Datos
		{
			get
			{
				return (IEnumerable<PolizasDTO>)RadGridMaster.DataSource;
			}
			set
			{
				RadGridMaster.DataSource = value;
			}
		}

		public string Observaciones
		{
			get
			{
				return observacioneswebtxt.Text;
			}
			set
			{
				observacioneswebtxt.Text = value;
			}
		}

		public string ObervacionesProcesadas
		{
			get
			{
				return observadeftxt.Text;
			}
			set
			{
				observadeftxt.Text = value;
			}
		}

		public string DocumentosFaxSolicitados
		{
			get
			{
				return Documentos_Fax_Adicionalestxt.Text;
			}
			set
			{
				Documentos_Fax_Adicionalestxt.Text = value;
			}
		}

		public string FechaNotificacion
		{
			get
			{
				return fechanotificacion.Text;
			}
			set
			{
				fechanotificacion.Text = value;
			}
		}

		public string FechaVencimiento
		{
			get
			{
				return fechavencimiento.Text;
			}
			set
			{
				fechavencimiento.Text = value;
			}
		}

		public string FechaOcurrencia
		{
			get
			{
				return fechaocurrencia.Text;
			}
			set
			{
				fechaocurrencia.Text = value;
			}
		}

		public string MedicoTratante
		{
			get
			{
				return medicotratante.Text;
			}
			set
			{
				medicotratante.Text = value;
			}
		}
		#endregion "Propiedades de Interfaz"

		///<summary>Evento de inicialización de la página.</summary>
		///<param name="sender">Instancia de la página.</param>
		///<param name="e">Instancia con parámetros de argumento.</param>
		///<remarks>Se usa para inicializar el presenter entre otras cosas.</remarks>
		protected void Page_Init(object sender, EventArgs e)
		{
			try
			{
				base.Page_Init(sender, e);
				this.presentador = new PresentadorDatosExpedienteCA(this);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[configCAExcepcionMensaje];
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
				if(!this.IsPostBack)
				{
					presentador.MostrarVista();
					RadGridMaster.Rebind();
				}
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[configCAExcepcionMensaje];
			}
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				if(!string.IsNullOrWhiteSpace(FechaSolicitud))
					FechaSolicitud = DateTime.Parse(FechaSolicitud).ToShortDateString();
				if(!string.IsNullOrWhiteSpace(FechaNotificacion))
					FechaNotificacion = DateTime.Parse(FechaNotificacion).ToShortDateString();
				if(!string.IsNullOrWhiteSpace(FechaOcurrencia))
					FechaOcurrencia = DateTime.Parse(FechaOcurrencia).ToShortDateString();
				if(!string.IsNullOrWhiteSpace(FechaVencimiento))
					FechaVencimiento = DateTime.Parse(FechaVencimiento).ToShortDateString();
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", "Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[@"MensajeExcepcion"];
			}
		}

		public void RadGridMaster_SortCommand(object sender, GridSortCommandEventArgs e)
		{
			if(e != null)
			{
				this.Orden = e.SortExpression + (e.NewSortOrder == GridSortOrder.Descending ? @" DESC" : @" ASC");
				this.RadGridMaster.Rebind();
			}
		}

		public void RadGridMaster_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			try
			{
				this.presentador.ObtenerPolizas(Orden);
			}
			catch(Exception ex)
			{
				HConnexum.Infraestructura.Errores.ManejarError(ex, @"Error al Mostrar la data de la Aplicacion");
				Trace.Warn(@"Error", @"Error al Mostrar la data de la Aplicacion", ex);
				this.Errores = WebConfigurationManager.AppSettings[configCAExcepcionMensaje];
			}
		}

		protected void RadGridMaster_CustomAggregate(object sender, GridCustomAggregateEventArgs e)
		{
			if(e.Column.UniqueName == @"Contratante")
				e.Result = string.Format(@"Monto Facturado: {0}", Datos.Count() == 0 ? @"0,00" : Datos.FirstOrDefault().MontoFacturado.ToString(@"N"));
			else if(e.Column.UniqueName == @"Parentesco")
				e.Result = string.Format(@"Deducible: {0}", Datos.Count() == 0 ? @"0,00" : Datos.FirstOrDefault().Deducible.ToString(@"N"));
			else if(e.Column.UniqueName == @"Cobertura")
			{
				double totalGastosNoCubiertos = Datos.Count() == 0 ? 0 : Datos.FirstOrDefault().MontoFacturado - (Datos.Sum(p => p.MontoCubierto) + Datos.FirstOrDefault().Deducible);
				if(totalGastosNoCubiertos < 0)
					totalGastosNoCubiertos = 0;
				e.Result = string.Format(@"Gastos No Cubiertos: {0}", totalGastosNoCubiertos.ToString(@"N"));
			}
			else if(e.Column.UniqueName == @"MontoCubierto")
				e.Result = Datos.Count() == 0 ? @"0,00" : Datos.Sum(p => p.MontoCubierto).ToString(@"N");
		}
	}
}