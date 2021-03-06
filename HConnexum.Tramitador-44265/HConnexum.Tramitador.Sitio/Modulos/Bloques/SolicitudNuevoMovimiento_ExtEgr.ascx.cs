using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web.Configuration;
using System.Web.UI;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;
using HConnexum.Tramitador.Presentacion;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
    public partial class SolicitudNuevoMovimiento_ExtEgr : UserControlBase, ISolicitudNuevoMovimiento
    {
        #region M I E M B R O S   P R I V A D O S
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string _NAMESPACE = "HConnexum.Tramitador.Sitio.Modulos.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string _mensaje = null;
        private const string TRAZA_CATEGORIA_ERROR = @"Error";
		/// <summary>Constante de nulo de entero.</summary>
		private const int _NULL_INT = -1;
        #endregion

        #region M I E M B R O S   P Ú B L I C O S
        ///<summary>Objeto 'Presentador' asociado al control Web de usuario.</summary>
        SolicitudNuevoMovimientoPresentador presentador;

        ///<summary>Tipo del bloque.</summary>
        public string BloqueTipo
        {
            get { return hidBloqueTipo.Value; }
        }
        ///<summary>Identificador del paso asociado al movimiento.</summary>
        public int PasoId
        {
            get
            {
                int id = -1;
                int.TryParse(Ult_Mvto_Reversable.Value.Trim(), out id);
                return id;
            }
            set
            {
                Ult_Mvto_Reversable.Value = value.ToString();
            }
        }
		///<summary>Cadena de conexión del suscriptor intermediario.</summary>
		public string SuscriptorIntermediarioConnectionString
		{
			get { return hidSuscriptorIntermediarioConnectionString.Value; }
			set { hidSuscriptorIntermediarioConnectionString.Value = value; }
		}
        ///<summary>Identificador del intermediario en la aplicación 'Gestor'.</summary>
        public int IntermediarioId
        {
            get
            {
                int id = -1;
                int.TryParse(IdIntermediario.Value.Trim(), out id);
                return id;
            }
			set { IdIntermediario.Value = value.ToString(); }
		}
        ///<summary>Identificador del proveedor en la aplicación 'Gestor'.</summary>
        public int ProveedorId
        {
            get
            {
                int id = -1;
                int.TryParse(IdProveedor.Value.Trim(), out id);
                return id;
            }
			set { IdProveedor.Value = value.ToString(); }
		}
		///<summary>Identificador del diagnóstico.</summary>
		public int DiagnosticoId
		{
			get
			{
				int id = _NULL_INT;
				int.TryParse(IdDiagnostico.Value.Trim(), out id);
				return id;
			}
		}
		///<summary>Identificador del procedimiento.</summary>
		public int ProcedimientoId
		{
			get
			{
				int id = _NULL_INT;
				int.TryParse(IdProcedimiento.Value.Trim(), out id);
				return id;
			}
		}
        ///<summary>Estado del último movimiento del caso.</summary>
        public string MovimientoEstado
        {
            get { return EstatusMovimientoWeb.Value; }
            set { EstatusMovimientoWeb.Value = value; }
        }
        /// <summary>Identificador del tipo de movimiento.</summary>
        public string MovimientoTipoId
        {
            get { return NomTipoMov.SelectedValue; }
        }
        ///<summary>Lista de tipos de diagnóstico.</summary>
        public DataTable ListaDiagnosticoTipos
        {
            set
            {
                NomDiagnostico.DataSource = value;
                NomDiagnostico.DataBind();
            }
        }
        ///<summary>Lista de tipos de procedimiento.</summary>
        public DataTable ListaProcedimientoTipos
        {
            set
            {
                NomProcedimiento.DataSource = value;
                NomProcedimiento.DataBind();
            }
        }
        ///<summary>Lista de tipos de movimiento.</summary>
        public IEnumerable<ListasValorDTO> ListaMovimientoTipos
        {
            set
            {
                NomTipoMov.DataSource = value;
                NomTipoMov.DataBind();
            }
        }
        ///<summary>Lista de modos de movimiento.</summary>
        public IEnumerable<ListasValorDTO> ListaMovimientoModos
        {
            set
            {
                NomModoMov.DataSource = value;
				NomModoMov.DataBind();
            }
        }
        #endregion

        #region M A N E J A D O R E S   D E   E V E N T O S
        protected void Page_Init(object sender, EventArgs e)
        {
            string configValor = null;
            try
            {
                base.Page_Init(sender, e);
                this.presentador = new SolicitudNuevoMovimientoPresentador(this);
				#region VALIDACIONES
				#region CAMPOS REQUERIDOS
				InputSetting txtSettings1 = RadInputManager1.GetSettingByBehaviorID("txtSettings");
				txtSettings1.Validation.IsRequired = true;
				txtSettings1.ErrorMessage = presentador.MostrarMensaje(TiposMensaje.Validacion_CampoRequerido);
				InputSetting txtNumericSettings = RadInputManager1.GetSettingByBehaviorID("txtNumericSettings");
				txtNumericSettings.Validation.IsRequired = true;
				txtNumericSettings.ErrorMessage = presentador.MostrarMensaje(TiposMensaje.Validacion_CampoRequerido);
				#endregion
				#region CAMPOS NUMÉRICOS
				string key = "onkeypress"; string value = "return SoloNumeros(event)";
				//MontoPresup.Attributes.Add(key, value);
				NumDiasHosp.Attributes.Add(key, value);
				#endregion
				#endregion
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
                base.Page_Load(sender, e);
                if (!Page.IsPostBack)
				{
					presentador.MostrarVista();

					if (ParametrosEntrada != null
					&& ParametrosEntrada["IDINTERMEDIARIO"] != null && !string.IsNullOrWhiteSpace(ParametrosEntrada["IDINTERMEDIARIO"])
					&& ParametrosEntrada["IDPROVEEDOR"] != null && !string.IsNullOrWhiteSpace(ParametrosEntrada["IDPROVEEDOR"])
					&& ParametrosEntrada["NOMDIAGNOSTICO"] != null && ParametrosEntrada["NOMPROCEDIMIENTO"] != null)
					{
						int nIntermediarioId = _NULL_INT;
						if (int.TryParse(ParametrosEntrada["IDINTERMEDIARIO"], out nIntermediarioId))
						{
							this.IntermediarioId = nIntermediarioId;
							int nProveedorId = _NULL_INT;
							if (int.TryParse(ParametrosEntrada["IDPROVEEDOR"], out nProveedorId))
							{
								this.ProveedorId = nProveedorId;
								RadComboBoxItem diagnosticoItem = NomDiagnostico.FindItemByText(ParametrosEntrada["NOMDIAGNOSTICO"]);
								if (diagnosticoItem != null)
								{
									NomDiagnostico.SelectedValue = diagnosticoItem.Value;
									this.NomDiagnostico_SelectedIndexChanged(null, null);
									RadComboBoxItem procedimientoItem = NomProcedimiento.FindItemByText(ParametrosEntrada["NOMPROCEDIMIENTO"]);
									if (procedimientoItem != null)
										NomProcedimiento.SelectedValue = procedimientoItem.Value;
								}
							}
						}
					}
				}
            }
            catch (Exception ex)
            {
				Auditoria(ex, _mensaje);
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                    if (NomTipoMov.SelectedValue == presentador.MOVIMIENTO_EGRESO_ID
                        && EstatusMovimientoWeb.Value.ToUpper() == presentador.MOVIMIENTO_ESTADO_RECHAZADO.ToUpper())
                        foreach (RadComboBoxItem item in NomTipoMov.Items)
                            if (item.Value != presentador.MOVIMIENTO_EGRESO_ID
                                && item.Value != presentador.MOVIMIENTO_EGRESO_ANULACION_ID)
                                NomTipoMov.Items.Remove(item);
            }
            catch (Exception ex)
            {
				Auditoria(ex, _mensaje);
            }
        }
        protected void NomDiagnostico_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
				NomProcedimiento.ClearSelection();
				NomProcedimiento.Items.Clear();
				IdDiagnostico.Value = NomDiagnostico.SelectedValue;
				if (NomDiagnostico.Text.ToUpper() == "OTROS" || string.IsNullOrWhiteSpace(NomDiagnostico.SelectedValue))
				{
					NomProcedimiento.Items.Add(new RadComboBoxItem("Otros", ""));
				}
				else
				{
					int nDiagnosticoId = _NULL_INT;
					if (int.TryParse(IdDiagnostico.Value, out nDiagnosticoId))
						this.ListaProcedimientoTipos = presentador.ObtenerProcedimientoTipos(
							nDiagnosticoId, this.IntermediarioId, this.ProveedorId);
				}
            }
            catch (Exception ex)
            {
				Auditoria(ex, _mensaje);
            }
        }
        protected void NomProcedimiento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                IdProcedimiento.Value = NomProcedimiento.SelectedValue;
            }
            catch (Exception ex)
            {
				Auditoria(ex, _mensaje);
            }
        }
        protected void NomTipoMov_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool mostrar = true;
            try
			{
				#region GUI
				mostrar = (NomTipoMov.SelectedValue != presentador.MOVIMIENTO_EGRESO_ANULACION_ID);
                lblDiagnostico.Visible = mostrar;
                NomDiagnostico.Visible = mostrar;
                lblProcedimiento.Visible = mostrar;
                NomProcedimiento.Visible = mostrar;
                lblModo.Visible = mostrar;
                NomModoMov.Visible = mostrar;
                lblMedico.Visible = mostrar;
                NomMedico.Visible = mostrar;
                lblMontoPresupuesto.Visible = mostrar;
                MontoPresup.Visible = mostrar;
                lblDiasHospitalizacion.Visible = mostrar;
                NumDiasHosp.Visible = mostrar;
				lblObservacion.Visible = mostrar;
                ObsDiagnostico.Visible = mostrar;
                pnlDocumentos.Visible = mostrar;
				lblObservacion2.Visible = !mostrar;
				ObsAnulacion.Visible = !mostrar;
				#endregion
				if(NomTipoMov.SelectedValue == presentador.MOVIMIENTO_EXTENSION_ID)
				{
					ULTIMOTIPOMOV.Value = ParametrosEntrada[@"TIPOMOV"];
					this.TipoMov.Value = "EXTENSIÓN";
				}
				else if(NomTipoMov.SelectedValue == presentador.MOVIMIENTO_EGRESO_ID)
				{
					ULTIMOTIPOMOV.Value = ParametrosEntrada[@"TIPOMOV"];
					this.TipoMov.Value = "EGRESO";
				}
				else if(NomTipoMov.SelectedValue == presentador.MOVIMIENTO_EGRESO_ANULACION_ID)
				{
					ULTIMOTIPOMOV.Value = ParametrosEntrada[@"TIPOMOV"];
					this.TipoMov.Value = "ANULACIÓN";
				}
			}
            catch (Exception ex)
            {
				Auditoria(ex, _mensaje);
            }
        }
        #endregion

		#region R U T I N A S
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

		/// <summary>Validación de campos por la página contenedora del bloque.</summary>
		/// <returns>Mensaje de error, si existe.</returns>
		public string ValidarDatos()
		{
			try
			{
				presentador.ValidarDatos();
				if(Errores.Length > 0)
					return Errores;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return string.Empty;
		}
    }
}