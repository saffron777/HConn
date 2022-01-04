using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using HConnexum.Tramitador.Negocio;
using HConnexum.Tramitador.Presentacion;
using HConnexum.Tramitador.Presentacion.Interfaz.Bloques;
using HConnexum.Tramitador.Presentacion.Presentador.Bloques;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio.Modulos.Bloques
{
	public partial class SeleccionAsegurado : UserControlBase, ISeleccionAsegurado
	{
		#region M I E M B R O S   P R I V A D O S
		/// <summary>Nombre del espacio de nombre actual.</summary>
		private const string _NAMESPACE = "HConnexum.Tramitador.Sitio.Modulos.Bloques";
		/// <summary>Mensaje personalizado a escribir en una traza o evento de error.</summary>
		private string _mensaje = null;
		private const int _MENSAJE_CAMPO_OBLIGATORIO_ID = 2;
		private int _aseguradoCedula = -1;
		private int _suscriptorconectadoid = 0;
		#endregion

		#region M I E M B R O S   P Ú B L I C O S
		///<summary>Objeto 'Presentador' asociado al control Web de usuario.</summary>
		SeleccionAseguradoPresentador presentador;

		/// <summary>Código del servicio a procesar.</summary>
		public string ServicioCodigo
		{
			get { return NomTipoServicio.Value; }
			set { NomTipoServicio.Value = value; }
		}

		///<summary>Identificador del suscriptor-proveedor.</summary>
		public int SuscriptorProveedorId
		{
			get
			{
				int id = -1;
				int.TryParse(IdSusProveedor.Value, out id);
				return id;
			}
			set
			{
				IdSusProveedor.Value = value.ToString();
			}
		}

		///<summary>Identificador del proveedor en la aplicación 'Gestor'.</summary>
		public int ProveedorGestorId
		{
			get
			{
				int id = -1;
				int.TryParse(IdProveedor.Value, out id);
				return id;
			}
			set
			{
				IdProveedor.Value = value.ToString();
			}
		}

		/// <summary>Código de la estación del proveedor.</summary>
		public string ProveedorEstacionCodigo
		{
			get { return estacion.Value; }
			set { estacion.Value = value; }
		}
		#region INTERMEDIARIO

		///<summary>Identificador del intermediario (suscriptor) en la aplicación 'HConnexum'.</summary>
		public int SuscriptorIntermediarioId
		{
			get
			{
				int id = -1;
				if(string.IsNullOrWhiteSpace(IdSusIntermediario.Value))
				{
					if(int.TryParse(this.ExtraerDeViewStateOQueryString(@"idSuscriptorIntermediario"), out id))
						IdSusIntermediario.Value = id.ToString();
				}
				else
					int.TryParse(IdSusIntermediario.Value, out id);
				return id;
			}
			set
			{
				IdSusIntermediario.Value = value.ToString();
			}
		}
		///<summary>Identificador del intermediario en la aplicación 'Gestor'.</summary>
		public int IntermediarioGestorId
		{
			get
			{
				int id = -1;
				int.TryParse(IdIntermediario.Value, out id);
				return id;
			}
			set
			{
				IdIntermediario.Value = value.ToString();
			}
		}
		/// <summary>Nombre del intermediario.</summary>
		public string IntermediarioNombre
		{
			get { return NomSusIntermed.Text; }
			set { NomSusIntermed.Text = value; }
		}
		/// <summary>Número del fax del intermediario.</summary>
		public string IntermediarioFaxNumero
		{
			get { return txtSuscriptorFaxNumero.Text; }
			set { txtSuscriptorFaxNumero.Text = value; }
		}
		/// <summary>Indica si un intermediario permite o no que se pueda ingresar los datos de un afiliado en caso 
		/// de que no exista (asegurado no encontrado).</summary>
		public bool IntermediarioPermiteAseguradoNoEncontrado
		{
			get
			{
				bool condicion = false;
				bool.TryParse(intermediarioPermiteAseguradoNoEncontrado.Value, out condicion);
				return condicion;
			}
			set
			{
				intermediarioPermiteAseguradoNoEncontrado.Value = value.ToString();
			}
		}
		#endregion

		/// <summary>Número de cédula del afiliado.</summary>
		public string AfiliadoCedulaNumero
		{
			get { return txtAseguradoCedula.Text.Trim(); }
			set { txtAseguradoCedula.Text = value; }
		}

		#region TITULAR
		/// <summary>Indica si existe o no el titular en al menos una póliza.</summary>
		public bool TitularExiste
		{
			get
			{
				bool existe = false;
				if(bool.TryParse(hidExisteTitular.Value, out existe))
					return existe;
				return false;
			}
			set
			{
				hidExisteTitular.Value = value.ToString();
			}
		}
		/// <summary>Identificador del titular.</summary>
		public string TitularId
		{
			set { IdTit.Value = value; }
		}
		/// <summary>Nacionalidad del titular.</summary>
		public string TitularNacionalidad
		{
			set { NacionalidadTit.Value = value; }
		}
		/// <summary>Número de cédula del titular.</summary>
		public string TitularCedulaNumero
		{
			set { NumDocTit.Value = value; }
		}
		/// <summary>Número de cédula del titular no existente.</summary>
		public string TitularNoExistenteCedulaNumero
		{
			get { return txtTitularNumDoc.Text; }
			set { txtTitularNumDoc.Text = value; }
		}
		/// <summary>Nombre completo del titular.</summary>
		public string TitularNombreCompleto
		{
			set { NomCompletoTit.Value = value; }
		}
		/// <summary>Sexo del titular.</summary>
		public string TitularSexo
		{
            get { return SexoTit.Value; }
			set { SexoTit.Value = value; }
		}

		/// <summary>Fecha de nacimiento del titular.</summary>
		public DateTime? TitularNacimientoFecha
		{
			get { return FecNacTit.SelectedDate; }
			set { FecNacTit.SelectedDate = value; }
		}
		/// <summary>Valor máximo para una fecha de nacimiento de titular no encontrado.</summary>
		public DateTime TitularNoEncontradoNacimientoFechaValorMaximo
		{
			set { FecNacTit.MaxDate = value; }
		}
		/// <summary>Contratante de la póliza.</summary>
		public string TitularContratanteNombre
		{
			get { return NomContratante.Value; }
			set { NomContratante.Value = value; }
		}
		/// <summary>Contratante de la póliza de un titular no existente.</summary>
		public string TitularNoExistenteContratanteNombre
		{
			get { return txtTitularContratante.Text; }
			set { txtTitularContratante.Text = value; }
		}
		#endregion

		#region BENEFICIARIO
		///<summary>Indica si existe o no el beneficiario en al menos una póliza.</summary>
		public bool BeneficiarioExiste
		{
			get
			{
				bool noExiste = false;
				if(bool.TryParse(IndBenefNoExiste.Value, out noExiste))
					return !noExiste;
				return false;
			}
			set
			{
				IndBenefNoExiste.Value = (!value).ToString();
			}
		}
		/// <summary>Identificador del beneficiario en la aplicación del cliente.</summary>
		public string BeneficiarioId
		{
			get { return IdBenef.Value; }
			set { IdBenef.Value = value; }
		}
		/// <summary>Nacionalidad del beneficiario.</summary>
		public string BeneficiarioNacionalidad
		{
			get { return NacionalidadBenef.Value; }
			set { NacionalidadBenef.Value = value; }
		}
		/// <summary>Número de cédula del beneficiario.</summary>
		public string BeneficiarioCedulaNumero
		{
			set { NumDocBenef.Value = value; }
		}
		/// <summary>Número de cédula del beneficiario no existente.</summary>
		public string BeneficiarioNoExistenteCedulaNumero
		{
			get { return numdocbenefne.Text; }
			set { numdocbenefne.Text = value; }
		}
		/// <summary>Nombre completo del beneficiario</summary>
		public string BeneficiarioNombreCompleto
		{
			get { return NomCompletoBenef.Value; }
			set { NomCompletoBenef.Value = value; }
		}
		/// <summary>Sexo del beneficiario</summary>
		public string BeneficiarioSexo
		{
			get { return SexoBenef.Value; }
			set { SexoBenef.Value = value; }
		}
		/// <summary>Parentesco del beneficiario.</summary>
		public string BeneficiarioParentesco
		{
			get { return ParentescoBenef.Value; }
			set { ParentescoBenef.Value = value; }
		}
		/// <summary>Fecha de nacimiento del beneficiario.</summary>
		public DateTime? BeneficiarioNacimientoFecha
		{
			get { return FecNacBenef.SelectedDate; }
			set { FecNacBenef.SelectedDate = value; }
		}
		/// <summary>Fecha de nacimiento del beneficiario Hijo.</summary>
		public DateTime? BeneficiarioHijoNacimientoFecha
		{
			get { return dtpAseguradoNacimientoFecha.SelectedDate; }
			set { dtpAseguradoNacimientoFecha.SelectedDate = value; }
		}
		/// <summary>Edad del beneficiario Hijo.</summary>
		public string BeneficiarioHijoEdad
		{
			get { return txtAseguradoEdad.Text; }
			set { txtAseguradoEdad.Text = value; }
		}
		/// <summary>Valor máximo para una fecha de nacimiento de beneficiario no encontrado.</summary>
		public DateTime BeneficiarioNoEncontradoNacimientoFechaValorMaximo
		{
			set { FecNacBenef.MaxDate = value; }
		}
		/// <summary>Ramo del beneficiario.</summary>
		public string BeneficiarioRamo
		{
			set { RamoBenef.Value = value; }
		}
		#endregion

		///<summary>Obtiene o establece el origen de datos para la lista de pólizas asociadas al afiliado.</summary>
		public DataTable AfiliadoPolizasDatos
		{
			set
			{
				this.rgAfiliadoPolizas.DataSource = value;
			}
		}
		///<summary>Obtiene o establece la cantidad de registros de la lista de pólizas asociadas al afiliado.</summary>
		public int AfiliadoPolizasRegistrosCantidad
		{
			get
			{
				return this.rgAfiliadoPolizas.VirtualItemCount;
			}
			set
			{
				this.rgAfiliadoPolizas.VirtualItemCount = value;
			}
		}
		///<summary>Establece el origen de datos para la lista de integrantes de un grupo familiar asociados a una póliza.</summary>
		public DataTable PolizaGrupoFamiliarDatos
		{
			set
			{
				this.rgPolizaGrupoFamiliar.DataSource = value;
			}
		}
		///<summary>Obtiene o establece la cantidad de registros de la lista de integrantes de un grupo familiar 
		///asociados a una póliza.</summary>
		public int PolizaGrupoFamiliarRegistrosCantidad
		{
			get
			{
				return this.rgPolizaGrupoFamiliar.VirtualItemCount;
			}
			set
			{
				this.rgPolizaGrupoFamiliar.VirtualItemCount = value;
			}
		}
		///<summary>Fecha de inicio de la póliza.</summary>
		public string PolizaVigenciaDesde
		{
			set { FecVigDesde.Value = value; }
		}
		///<summary>Fecha de vencimiento de la póliza.</summary>
		public string PolizaVigenciaHasta
		{
			set { FecVigHasta.Value = value; }
		}
		///<summary>Fecha de la solicitud.</summary>
		public string SolicitudFecha
		{
			set { FecSolicitud.Value = value; }
		}
		///<summary>Establece el valor máximo para la fecha de ocurrencia de una solicitud.</summary>
		public DateTime SolicitudOcurrenciaFechaValorMinimo
		{
			set
			{
				this.FecOcurrencia.MinDate = value;
			}
		}
		///<summary>Establece el valor máximo para la fecha de ocurrencia de una solicitud.</summary>
		public DateTime SolicitudOcurrenciaFechaValorMaximo
		{
			set { this.FecOcurrencia.MaxDate = value; }
		}
		///<summary>Lista de tipos de documento.</summary>
		public IEnumerable<ListasValorDTO> ListaDocumentoTipos
		{
			set
			{
				tipdoctitular.DataSource = value;
				tipdoctitular.DataBind();
				tipdocbenefne.DataSource = value;
				tipdocbenefne.DataBind();
			}
		}
		///<summary>Lista de tipos de sexo.</summary>
		public IEnumerable<ListasValorDTO> ListaSexoTipos
		{
			set
			{
				SexoTitu.DataSource = value;
                SexoTitu.DataBind();
				sexobenefne.DataSource = value;
				sexobenefne.DataBind();
			}
		}
		///<summary>Lista de tipos de parentesco.</summary>
		public IEnumerable<ListasValorDTO> ListaParentescoTipos
		{
			set
			{
				parentescobenefne.DataSource = value;
				parentescobenefne.DataBind();
			}
		}
		///<summary>Tipos de documento seleccionado de la lista de tipos.</summary>
		public string DocumentoTipoSeleccionado
		{
			get { return tipdoctitular.SelectedValue; }
			set { tipdoctitular.SelectedValue = value; }
		}
		///<summary>Tipos de sexo seleccionado de la lista de tipos.</summary>
		public string SexoTipoSeleccionado
		{
			get
			{
				return sexobenefne.SelectedValue;
			}
			set
			{
				sexobenefne.SelectedValue = value;
			}
		}
		///<summary>Tipos de parentesco seleccionado de la lista de tipos.</summary>
		public string ParentescoTipoSeleccionado
		{
			get
			{
				return parentescobenefne.SelectedValue;
			}
			set
			{
				parentescobenefne.SelectedValue = value;
			}
		}
		///<summary>Indica si la sección 'Pólizas asociadas al afiliado' está visible.</summary>
		public bool PanelAfiliadoPolizaVisible
		{
			get { return pnlAfiliadoPolizas.Visible; }
			set { pnlAfiliadoPolizas.Visible = value; }
		}
		///<summary>Indica si la sección 'Grupo famliar asociado a la póliza' está visible.</summary>
		public bool PanelPolizaGrupoFamiliarVisible
		{
			get { return pnlPolizaGrupoFamiliar.Visible; }
			set { pnlPolizaGrupoFamiliar.Visible = value; }
		}
		///<summary>Indica si la sección 'Datos de la solicitud' está visible.</summary>
		public bool PanelSolicitudDatosVisible
		{
			get { return pnlSolicitudDatos.Visible; }
			set { pnlSolicitudDatos.Visible = value; }
		}

		public int SuscriptorProveedor
		{
			get
			{
				if(this._suscriptorconectadoid == 0)
				{
					string sId = this.ExtraerDeViewStateOQueryString(@"idSuscriptorProveedor");
					if(!string.IsNullOrEmpty(sId))
						this._suscriptorconectadoid = int.Parse(sId);
				}
				return this._suscriptorconectadoid;
			}
			set
			{
				this._suscriptorconectadoid = value;
				this.ViewState[@"idSuscriptorProveedor"] = this._suscriptorconectadoid;
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
				this.presentador = new SeleccionAseguradoPresentador(this);
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
				string key = "onkeypress";
				string value = "return SoloNumeros(event)";
				txtAseguradoCedula.Attributes.Add(key, value);
				numdocbenefne.Attributes.Add(key, value);
				txtTitularNumDoc.Attributes.Add(key, value);
				txtAseguradoEdad.Attributes.Add(key, value);
				TlfSolicitante.Attributes.Add(key, value);
				tlfresponsable.Attributes.Add(key, value);
				#endregion
				#endregion
				FecOcurrencia.MaxDate = DateTime.Now.Date;
				dtpAseguradoNacimientoFecha.MaxDate = DateTime.Now.Date;
			}
			catch(Exception ex)
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
				if(!Page.IsPostBack)
				{
					this.BeneficiarioExiste = true;
					presentador.MostrarVista();
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void btnBuscarAsegurado_Click(object sender, EventArgs e)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.btnBuscarAsegurado_Click()", _NAMESPACE);
				#region GUI
				PolizasAsociadasDatos_Limpiar();
				GrupoFamiliarDatos_Limpiar();
				TitularDatos_Limpiar();
				BeneficiarioDatos_Limpiar();
				SolicitudDatos_Limpiar();
				ResponsableDatos_Limpiar();
				lblAfiliadoNoEncontradoMensaje.Visible = false;
				btnAfiliadoNoEncontradoAgregar.Visible = false;
				pnlAfiliadoPolizas.Visible = false;
				pnlPolizaGrupoFamiliar.Visible = false;
				pnlTitularDatos.Visible = false;
				pnlBeneficiarioDatos.Visible = false;
				pnlSolicitudDatos.Visible = false;
				pnlResponsableDatos.Visible = false;
				#endregion
				this.AfiliadoCedulaNumero = txtAseguradoCedula.Text.Trim();
				if(string.IsNullOrWhiteSpace(this.AfiliadoCedulaNumero))
					return;
				if(presentador.ObtenerAfiliadoPolizas(this.SuscriptorIntermediarioId, this.AfiliadoCedulaNumero, this.IntermediarioGestorId))
				{
					rgAfiliadoPolizas.DataBind();
					rgAfiliadoPolizas.Visible = true;
					pnlAfiliadoPolizas.Visible = true;
				}
				else
				{
					

                    if (this.IntermediarioPermiteAseguradoNoEncontrado)
                    {
                        btnAfiliadoNoEncontradoAgregar.Visible = true;
                        TitularNacionalidad = tipdoctitular.SelectedValue;
                        BeneficiarioNacionalidad = tipdocbenefne.SelectedValue;
                     

                      
                    }  MostrarMensaje(presentador.MostrarMensaje(TiposMensaje.AseguradoSinCobertura));
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void btnAfiliadoNoEncontradoAgregar_Click(object sender, EventArgs e)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.btnAfiliadoNoEncontradoAgregar_Click()", _NAMESPACE);
				#region GUI
                if (!this.TitularExiste)
                {
                    TitularDatos_Limpiar();
                }
				BeneficiarioDatos_Limpiar();
				SolicitudDatos_Limpiar();
				ResponsableDatos_Limpiar();
				pnlTitularDatos.Visible = (!this.TitularExiste);
				lblBeneficiarioMensaje.Text = presentador.MostrarMensaje(TiposMensaje.Informacion_Servicio_Clave_Emergencia_Verificacion_Parentesco_Recaudos);
				pnlBeneficiarioDatos.Visible = true;
				pnlSolicitudDatos.Visible = true;
				pnlResponsableDatos.Visible = true;
				#endregion
				this.BeneficiarioExiste = false;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void btnBuscarGrupoFamiliar_Click(object sender, EventArgs e)
		{
			object itemPoliza = null;
			object itemRamo = null;
			object itemPolizaVigenciaDesde = null;
			object itemPolizaVigenciaHasta = null;
			this.TitularExiste = false;
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.btnBuscarGrupoFamiliar_Click()", _NAMESPACE);
				foreach(GridDataItem item in rgAfiliadoPolizas.Items)
				{
					if(item.Selected)
					{
                        itemPoliza = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idpoliza"];
						itemRamo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["ramo"];
						itemPolizaVigenciaDesde = item.OwnerTableView.DataKeyValues[item.ItemIndex]["FechaPolizaInicio"];
						itemPolizaVigenciaHasta = item.OwnerTableView.DataKeyValues[item.ItemIndex]["FechaPolizaFin"];
                        TitularId = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idAsegurado"].ToString();
                        TitularCedulaNumero = item.OwnerTableView.DataKeyValues[item.ItemIndex]["cedulaBenef"].ToString();
                        TitularNacionalidad = item.OwnerTableView.DataKeyValues[item.ItemIndex]["nacionalidad"].ToString();
                        TitularNombreCompleto = item.OwnerTableView.DataKeyValues[item.ItemIndex]["beneficiario"].ToString();
                        TitularSexo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["sexo"].ToString();
						TitularNacimientoFecha = Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex]["fechaNacimiento"].ToString());

						if(itemPoliza != null && !string.IsNullOrWhiteSpace(itemPoliza.ToString())
							&& itemRamo != null && !string.IsNullOrWhiteSpace(itemRamo.ToString())
							&& itemPolizaVigenciaDesde != null && !string.IsNullOrWhiteSpace(itemPolizaVigenciaDesde.ToString())
							&& itemPolizaVigenciaHasta != null && !string.IsNullOrWhiteSpace(itemPolizaVigenciaHasta.ToString()))
						{
							int polizaId = -1;
							string ramo = itemRamo.ToString();
							if(int.TryParse(itemPoliza.ToString(), out polizaId))
							{
								this.AfiliadoCedulaNumero = txtAseguradoCedula.Text.Trim();
								if(presentador.ObtenerPolizaGrupoFamiliar(this.SuscriptorIntermediarioId, this.IntermediarioGestorId, this.AfiliadoCedulaNumero, polizaId, ramo))
								{
									rgPolizaGrupoFamiliar.DataBind();
									rgPolizaGrupoFamiliar.Visible = true;
									pnlPolizaGrupoFamiliar.Visible = true;
									if(this.IntermediarioPermiteAseguradoNoEncontrado)
									{
										lblAfiliadoNoEncontradoMensaje.Visible = true;
										btnAfiliadoNoEncontradoAgregar.Visible = true;
									}
									this.TitularExiste = Titular_ValidarExistencia();
								}
							}
							if(itemPolizaVigenciaDesde != null)
								this.PolizaVigenciaDesde = DateTime.Parse(itemPolizaVigenciaDesde.ToString()).ToShortDateString();
							if(itemPolizaVigenciaHasta != null)
								this.PolizaVigenciaHasta = DateTime.Parse(itemPolizaVigenciaHasta.ToString()).ToShortDateString();
						}
					}
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void btnBuscarBeneficiario_Click(object sender, EventArgs e)
		{
			/// Información necesaria para realizar la traza de seguimiento.
			_mensaje = string.Format("{0}.btnBuscarBeneficiario_Click()", _NAMESPACE);
			#region GUI
			pnlSolicitudDatos.Visible = false;
			pnlResponsableDatos.Visible = false;
			lblSolicitudNacimientoFecha.Visible = false;
			dtpAseguradoNacimientoFecha.Visible = false;
			lblSolicitudEdad.Visible = false;
			txtAseguradoEdad.Visible = false;
			#endregion
			object itemTitularCedula = null;
			object itemParentesco = null;
			object itemBeneficiarioId = null;
			object itemBeneficiarioNacimientoFecha = null;
			object itemBeneficiarioNacionalidad = null;
			object itemRamo = null;
			object itemBeneficiarioDocumentoNumero = null;
			object itemNombreAsegurado = null;
			object itemSexo = null;
			object itemEstado = null;
			try
			{
				foreach(GridDataItem item in rgPolizaGrupoFamiliar.Items)
				{
					itemTitularCedula = item.OwnerTableView.DataKeyValues[item.ItemIndex]["cedulaTitular"];
					itemBeneficiarioId = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idAsegurado"];
					itemBeneficiarioNacionalidad = item.OwnerTableView.DataKeyValues[item.ItemIndex]["nacionalidad"];
					itemBeneficiarioNacimientoFecha = item.OwnerTableView.DataKeyValues[item.ItemIndex]["fechaNacimiento"];
					itemRamo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["ramo"];
					itemParentesco = item.OwnerTableView.DataKeyValues[item.ItemIndex]["parentesco"];
					itemBeneficiarioDocumentoNumero = item.OwnerTableView.DataKeyValues[item.ItemIndex]["cedulaBenef"];
					itemNombreAsegurado = item.OwnerTableView.DataKeyValues[item.ItemIndex]["beneficiario"];
					itemSexo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["sexo"];
					itemEstado = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idEstado"];
					if(itemParentesco != null && itemParentesco.ToString().ToUpper() == "TITULAR")
					{
						this.TitularId = itemBeneficiarioId.ToString();
						this.TitularCedulaNumero = itemBeneficiarioDocumentoNumero.ToString();
						this.TitularNacionalidad = itemBeneficiarioNacionalidad.ToString();
						this.TitularNombreCompleto = itemNombreAsegurado.ToString();
                        this.TitularSexo = itemSexo.ToString();

						if(itemBeneficiarioNacimientoFecha != null && !string.IsNullOrEmpty(itemBeneficiarioNacimientoFecha.ToString()))
						{
							this.TitularNoEncontradoNacimientoFechaValorMaximo = DateTime.Now.Date.AddYears(100);
							this.TitularNacimientoFecha = DateTime.Parse(itemBeneficiarioNacimientoFecha.ToString());
						}
					}
					if(item.Selected)
					{
						if(itemEstado.ToString().ToUpper() == @"A")
						{
							this.BeneficiarioExiste = true;
							if(itemTitularCedula != null && !string.IsNullOrWhiteSpace(itemTitularCedula.ToString())
								&& itemBeneficiarioId != null && !string.IsNullOrWhiteSpace(itemBeneficiarioId.ToString())
								&& itemBeneficiarioNacionalidad != null && !string.IsNullOrWhiteSpace(itemBeneficiarioNacionalidad.ToString())
								&& itemBeneficiarioNacimientoFecha != null && !string.IsNullOrWhiteSpace(itemBeneficiarioNacimientoFecha.ToString())
								&& itemRamo != null && !string.IsNullOrWhiteSpace(itemRamo.ToString()))
							{
								pnlSolicitudDatos.Visible = true;
								pnlResponsableDatos.Visible = true;
								this.TitularCedulaNumero = itemTitularCedula.ToString();
								this.BeneficiarioId = itemBeneficiarioId.ToString();
								this.BeneficiarioNacionalidad = itemBeneficiarioNacionalidad.ToString();
								this.BeneficiarioNacimientoFecha = (DateTime?)itemBeneficiarioNacimientoFecha;
								this.BeneficiarioRamo = itemRamo.ToString();
                                
								if(this.BeneficiarioParentesco.ToUpper().Contains("HIJO"))
								{
									lblSolicitudNacimientoFecha.Visible = true;
									dtpAseguradoNacimientoFecha.Visible = true;
									lblSolicitudEdad.Visible = true;
									txtAseguradoEdad.Visible = true;
								}
							}
							rfvFechaOcurrencia.Enabled = true;
							rfvAseguradoNacimientoFecha.Enabled = true;
						}
						else
							MostrarMensaje(presentador.MostrarMensaje(TiposMensaje.AseguradoInactivo));
					}
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void optListTitularEsBeneficiario_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.optListTitularEsBeneficiario_SelectedIndexChanged()", _NAMESPACE);
                if (optListTitularEsBeneficiario.SelectedValue == "Sí" || optListTitularEsBeneficiario.SelectedValue == "Si")
				{
					PrimerNombreBenef.Text = PrimerNombreTit.Text.Trim();
					SegundoNombreBenef.Text = SegundoNombreTit.Text.Trim();
					PrimerApellidoBenef.Text = PrimerApellidoTit.Text.Trim();
					SegundoApellidoBenef.Text = SegundoApellidoTit.Text.Trim();
					tipdocbenefne.SelectedValue = tipdoctitular.SelectedValue;
					numdocbenefne.Text = txtTitularNumDoc.Text.Trim();
					sexobenefne.SelectedValue = SexoTitu.SelectedValue;
					FecNacBenef.SelectedDate = FecNacTit.SelectedDate;
				}
				else if(optListTitularEsBeneficiario.SelectedValue == "No")
				{
					PrimerNombreBenef.Text = null;
					SegundoNombreBenef.Text = null;
					PrimerApellidoBenef.Text = null;
					SegundoApellidoBenef.Text = null;
					tipdocbenefne.SelectedValue = null;
					numdocbenefne.Text = null;
					sexobenefne.SelectedValue = null;
					FecNacBenef.SelectedDate = null;
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void sexobenefne_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
				SexoBenef.Value = sexobenefne.SelectedValue;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void parentescobenefne_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
				ParentescoBenef.Value = parentescobenefne.SelectedValue;
				if(this.BeneficiarioParentesco.ToUpper().Contains("HIJO"))
				{
					lblSolicitudNacimientoFecha.Visible = true;
					dtpAseguradoNacimientoFecha.Visible = true;
                    rfvFechaOcurrencia.Enabled = true;
					lblSolicitudEdad.Visible = true;
					txtAseguradoEdad.Visible = true;                    
				}
				else
				{
					lblSolicitudNacimientoFecha.Visible = false;
					dtpAseguradoNacimientoFecha.Visible = false;
                    rfvFechaOcurrencia.Enabled = false;
					dtpAseguradoNacimientoFecha.Clear();
					lblSolicitudEdad.Visible = false;
					txtAseguradoEdad.Visible = false;
					txtAseguradoEdad.Text = string.Empty;
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		protected void IndMenor_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.IndMenor_SelectedIndexChanged()", _NAMESPACE);
				if(RadInputManager1 != null && RadInputManager1.InputSettings.Count > 0)
				{
					InputSetting txtSettings = RadInputManager1.GetSettingByBehaviorID("txtSettings");
					if(txtSettings.TargetControls != null && txtSettings.TargetControls.Count > 0)
					{
						TargetInput target = txtSettings.TargetControls.FindTargetInputById("numdocbenefne");
						if(target != null)
						{
							target.Enabled = false;
							if(IndMenor != null && IndMenor.SelectedItem != null)
                                if (IndMenor.SelectedItem.Value.ToUpper() == "SÍ" || IndMenor.SelectedItem.Value.ToUpper() == "SI")
								{
									numdocbenefne.Enabled = target.Enabled = false;
									numdocbenefne.Text = string.Empty;
								}
								else if(IndMenor.SelectedItem.Value.ToUpper() == "NO")
									numdocbenefne.Enabled = target.Enabled = true;
						}
					}
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}
		#endregion

		#region R U T I N A S
		private bool Titular_ValidarExistencia()
		{
			/// Información necesaria para realizar la traza de seguimiento.
			_mensaje = string.Format("{0}.TitularExistencia_Validar()", _NAMESPACE);
			object itemBeneficiarioParentesco = null;
			object itemBeneficiarioId = null;
			try
			{
				foreach(GridDataItem item in rgPolizaGrupoFamiliar.Items)
				{
					itemBeneficiarioParentesco = item.OwnerTableView.DataKeyValues[item.ItemIndex]["parentesco"];
					itemBeneficiarioId = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idAsegurado"];
					if(itemBeneficiarioParentesco != null && !string.IsNullOrWhiteSpace(itemBeneficiarioParentesco.ToString())
						&& itemBeneficiarioId != null && !string.IsNullOrWhiteSpace(itemBeneficiarioId.ToString()))
						if(itemBeneficiarioParentesco.ToString().ToUpper() == "TITULAR")
						{
							this.TitularId = itemBeneficiarioId.ToString();
							return true;
						}
				}
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
			return false;
		}

		/// <summary>Limpia los controles de la sección 'Listado de pólizas asociadas'.</summary>
		private void PolizasAsociadasDatos_Limpiar()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.PolizasAsociadasDatos_Limpiar()", _NAMESPACE);
				rgAfiliadoPolizas.DataSource = null;
				rgAfiliadoPolizas.DataBind();
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		/// <summary>Limpia los controles de la sección 'Grupo familiar'.</summary>
		private void GrupoFamiliarDatos_Limpiar()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.GrupoFamiliarDatos_Limpiar()", _NAMESPACE);
				rgPolizaGrupoFamiliar.DataSource = null;
				rgPolizaGrupoFamiliar.DataBind();
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		/// <summary>Limpia los controles de la sección 'Datos del titular'.</summary>
		private void TitularDatos_Limpiar()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.TitularDatos_Limpiar()", _NAMESPACE);
				PrimerNombreTit.Text = null;
				SegundoNombreTit.Text = null;
				PrimerApellidoTit.Text = null;
				SegundoApellidoTit.Text = null;
				tipdoctitular.ClearSelection();
				txtTitularNumDoc.Text = null;
				SexoTitu.ClearSelection();
				FecNacTit.Clear();
				txtTitularContratante.Text = null;
				optListTitularEsBeneficiario.ClearSelection();
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		/// <summary>Limpia los controles de la sección 'Datos del beneficiario'.</summary>
		private void BeneficiarioDatos_Limpiar()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.BeneficiarioDatos_Limpiar()", _NAMESPACE);
				PrimerNombreBenef.Text = null;
				SegundoNombreBenef.Text = null;
				PrimerApellidoBenef.Text = null;
				SegundoApellidoBenef.Text = null;
				IndMenor.ClearSelection();
				tipdocbenefne.ClearSelection();
				numdocbenefne.Text = null;
				sexobenefne.ClearSelection();
				FecNacBenef.Clear();
				parentescobenefne.ClearSelection();
				IndCondEspecial.ClearSelection();
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		/// <summary>Limpia los controles de la sección 'Datos de la solicitud'.</summary>
		private void SolicitudDatos_Limpiar()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.SolicitudDatos_Limpiar()", _NAMESPACE);
				TlfSolicitante.Text = null;
				FecOcurrencia.Clear();
				NomSintomas.Text = null;
				FecOcurrencia.Clear();
				dtpAseguradoNacimientoFecha.Clear();
				txtAseguradoEdad.Text = null;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

		/// <summary>Limpia los controles de la sección 'Datos del responsable'.</summary>
		private void ResponsableDatos_Limpiar()
		{
			try
			{
				/// Información necesaria para realizar la traza de seguimiento.
				_mensaje = string.Format("{0}.ResponsableDatos_Limpiar()", _NAMESPACE);
				NomResponsable.Text = null;
				tlfresponsable.Text = null;
				EmailResponsable.Text = null;
			}
			catch(Exception ex)
			{
				Auditoria(ex, _mensaje);
			}
		}

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
				_mensaje = string.Format("{0}.Auditoria(pException = {1}, pMensaje = {2})", _NAMESPACE, pException, pMensaje);
				Debug.WriteLine(pException.ToString());
				Infraestructura.Errores.ManejarError(pException, pMensaje);
				if(pException.InnerException != null)
					Trace.Warn(TRAZA_CATEGORIA_ERROR, pMensaje + pException.InnerException.Message);
				Trace.Warn(TRAZA_CATEGORIA_ERROR, pMensaje, pException);
				this.Errores = presentador.MostrarMensaje(TiposMensaje.Error_Generico);
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				Infraestructura.Errores.ManejarError(ex, _mensaje);
				if(ex.InnerException != null)
					Trace.Warn(TRAZA_CATEGORIA_ERROR, _mensaje + ex.InnerException.Message);
				Trace.Warn(TRAZA_CATEGORIA_ERROR, _mensaje, ex);
				this.Errores = presentador.MostrarMensaje(TiposMensaje.Error_Generico);
			}
		}
		#endregion
	}
}
