<%@ Page Title="Transporte Flujo Servicios" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TransporteFlujoServicio.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Utilitarios.TransporteFlujoServicio" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
		td.labelCell
		{
			margin: 0;
			padding: 5px 0 0 5px;
			text-align: left;
			width: 231px;
		}
		td.fieldCell
		{
			margin: 0;
			padding: 5px 0 0 5px;
			text-align: left;
			width: 231px;
		}
	</style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelsRenderMode="Inline">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="ddlFlujoServicio">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="ddlVersion" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ddlVersion">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="pInformacion" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
		<asp:Panel ID="PanelMaster" runat="server">
			<fieldset>
				<legend>
					<asp:Label runat="server" Text="Flujo Servicio" Font-Bold="True"></asp:Label>
				</legend>
				<table>
					<tr>
						<td style="width: 100px">
							<asp:Label Text="Flujo Servicio:" runat="server" />
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="ddlFlujoServicio" EmptyMessage="Seleccione..." CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="ddlFlujoServicio_SelectedIndexChanged" Width="250"/>
							<asp:RequiredFieldValidator ID="rfvFlujoServicio" runat="server" ErrorMessage="*" ControlToValidate="ddlFlujoServicio" CssClass="validator" Width="25px" />
						</td>
					</tr>
					<tr>
						<td style="width: 100px">
							<asp:Label Text="Version:" runat="server" />
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="ddlVersion" EmptyMessage="Seleccione..." CausesValidation="false" AutoPostBack="true" DataTextField="Version" DataValueField="Id" OnSelectedIndexChanged="ddlVersion_SelectedIndexChanged" Width="250"/>
							<asp:RequiredFieldValidator ID="rfvVersion" runat="server" ErrorMessage="*" ControlToValidate="ddlVersion" CssClass="validator" Width="25px" />
						</td>
					</tr>
				</table>
			</fieldset>
			<br />
			<fieldset>
				<legend>
					<asp:Label runat="server" Text="Base de datos destino" Font-Bold="True"></asp:Label>
				</legend>
				<table>
					<tr>
						<td style="width: 100px">
							<asp:Label Text="Servidor:" runat="server" />
						</td>
						<td>
							<asp:TextBox runat="server" ID="txtServidor" Width="250"/>
						</td>
						<td style="width: 100px">
							<asp:Label Text="Instancia:" runat="server" />
						</td>
						<td>
							<asp:TextBox runat="server" ID="txtInstancia" Width="250"/>
						</td>
					</tr>
					<tr>
						<td style="width: 100px">
							<asp:Label Text="Base de Datos:" runat="server" />
						</td>
						<td colspan="3">
							<asp:TextBox runat="server" ID="txtBD" Width="250"/>
						</td>
					</tr>
					<tr>
						<td style="width: 100px">
							<asp:Label Text="Usuario:" runat="server" />
						</td>
						<td>
							<asp:TextBox runat="server" ID="txtUsuario" Width="250"/>
						</td>
						<td>
							<asp:Label Text="Contraseña:" runat="server" />
						</td>
						<td>
							<asp:TextBox runat="server" ID="txtContrasena" TextMode="Password" Width="250"/>
							<asp:CheckBox runat="server" ID="chkContrasena" onclick="hideOrShowPassword();"/>
							<asp:Button runat="server" ID="btnProbarConexion" Text="Probar Conexión" OnClick="btnProbarConexion_OnClick" ValidationGroup="Conexion"/>
						</td>
					</tr>
				</table>
			</fieldset>
			<telerik:RadInputManager ID="rim" runat="server">
				<telerik:TextBoxSetting BehaviorID="BehaviorServidor" EmptyMessage="Escriba el Servidor" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio" Validation-ValidationGroup="Conexion">
					<TargetControls>
						<telerik:TargetInput ControlID="txtServidor" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorInstancia" EmptyMessage="Escriba la Instancia" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio" Validation-ValidationGroup="Conexion">
					<TargetControls>
						<telerik:TargetInput ControlID="txtInstancia" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorBD" EmptyMessage="Escriba la Base de Datos" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio" Validation-ValidationGroup="Conexion">
					<TargetControls>
						<telerik:TargetInput ControlID="txtBD" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorUsuario" EmptyMessage="Escriba el Usuario" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio" Validation-ValidationGroup="Conexion">
					<TargetControls>
						<telerik:TargetInput ControlID="txtUsuario" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorContrasena" EmptyMessage="Escriba la Contraseña" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio" Validation-ValidationGroup="Conexion">
					<TargetControls>
						<telerik:TargetInput ControlID="txtContrasena" />
					</TargetControls>
				</telerik:TextBoxSetting>
			</telerik:RadInputManager>
			<br />
			<asp:Panel ID="pInformacion" runat="server">
				<fieldset>
					<legend>
						<asp:Label runat="server" Text="Información Transporte" Font-Bold="True"></asp:Label>
					</legend>
					<table>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Servicios Sucursales:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblServiciosSucursales" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Alcance Geografico:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblAlcanceGeografico" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Etapas:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblEtapas" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Pasos:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblPasos" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Pasos Bloques:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblPasosBloques" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Bloques:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblBloques" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Pasos Respuestas:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblPasosRespuestas" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Parametros Agenda:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblParametrosAgenda" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad CHA de Pasos:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblCHAPasos" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Mensaje Metodos Destinatarios:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblMensajeMetodosDestinatarios" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Flujo Ejecución:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblFlujoEjecucion" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Tipo de Pasos:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblTipoPasos" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Documentos:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblDocumentos" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Campos Indexación:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblCamposIndexacion" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Documentos Pasos:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblDocumentosPasos" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Atributos Archivos:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblAtributosArchivo" runat="server" />
							</td>
						</tr>
						<tr>
							<td class="labelCell">
								<asp:Label Text="Cantidad Documentos Servicios:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblDocumentosServicios" runat="server" />
							</td>
							<td class="labelCell">
								<asp:Label Text="Cantidad Solicitud Bloques:" runat="server" />
							</td>
							<td class="fieldCell">
								<asp:Label ID="lblSolicitudBloques" runat="server" />
							</td>
						</tr>
					</table>
				</fieldset>
			</asp:Panel>
		</asp:Panel>
		<br />
		<br />
		<telerik:RadButton ID="cmdGenerar" runat="server" Text="Generar" OnClick="cmdGenerar_Click" OnClientClicking="cmdGenerar_Click"/>
	</div>
	<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
		<script type="text/javascript">
			var clickCalledAfterRadconfirm = false;
			var lastClickedItem = null;

			function cmdGenerar_Click(sender, args) {
				var validated = Page_ClientValidate('');
				if (!validated) return;
				if (!clickCalledAfterRadconfirm) {
					lastClickedItem = sender;
					radconfirm("¿Seguro que desea generar el Flujo de Servicio seleccionado en la Base de datos Destino?", confirmCallBackFn, 380, 100, null, "Confirmación");
					args.set_cancel(true);
				}
			}

			function confirmCallBackFn(arg) {
				if (arg) {
					clickCalledAfterRadconfirm = true;
					lastClickedItem.click();
				}
				else
					clickCalledAfterRadconfirm = false;
				lastClickedItem = null;
			}

			function hideOrShowPassword() {
				checkbox = document.getElementById("<%= chkContrasena.ClientID %>");
				passField = document.getElementById("<%= txtContrasena.ClientID %>");
				if(checkbox.checked)
					passField.type = "text";
				else
					passField.type = "password"
			}
		</script>
	</telerik:RadCodeBlock>
</asp:Content>
