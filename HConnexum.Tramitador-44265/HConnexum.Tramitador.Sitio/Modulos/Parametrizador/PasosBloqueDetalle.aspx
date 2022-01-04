<%@ Page Title="Detalle de PasosBloque" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PasosBloqueDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.PasosBloqueDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxManager1Resource1">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadGridDetails">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
		<asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
			<fieldset>
				<legend>
					<asp:Label ID="lgLegendPasosBloque" runat="server" Text="PasosBloque" Font-Bold="True" meta:resourcekey="lgLegendPasosBloqueResource1" />
				</legend>
				<table>
					<tr>
						<td>
							<asp:Label ID="lblIdBloque" runat="server" Text="Bloque:" meta:resourcekey="lblIdBloqueResource1" />
						</td>
						<td>
							<telerik:RadComboBox ID="ddlIdBloque" DataValueField="Id" CausesValidation="false" AutoPostBack="true" DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Culture="es-ES" meta:resourcekey="ddlIdBloqueResource1" OnSelectedIndexChanged="ddlIdBloque_OnSelectedIndexChanged" />
							<asp:RequiredFieldValidator ID="rfvIdBloque" runat="server" ErrorMessage="*" ControlToValidate="ddlIdBloque" CssClass="validator" Width="25px" meta:resourcekey="rfvIdBloqueResource1" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblPosicion" runat="server" Text="Posicion:" meta:resourcekey="lblPosicionResource1" />
						</td>
						<td>
							<asp:TextBox ID="txtPosicion" runat="server" meta:resourcekey="txtPosicionResource1" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblIndActualizable" runat="server" Text="Ind Actualizable:" meta:resourcekey="lblIndActualizableResource1" />
						</td>
						<td>
							<asp:CheckBox ID="chkIndActualizable" runat="server" meta:resourcekey="chkIndActualizableResource1" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label runat="server" Text="Ind Colapsado:"  />
						</td>
						<td>
							<asp:CheckBox ID="chkIndColapsado" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblTituloBloque" runat="server" Text="Titulo Bloque:" meta:resourcekey="lblTituloBloqueResource1" />
						</td>
						<td>
							<asp:TextBox ID="txtTituloBloque" runat="server" meta:resourcekey="txtTituloBloqueResource1" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblIdTipoControl" runat="server" Text="Tipo Control" meta:resourcekey="lblIdTipoControlResource1" Visible="false" />
						</td>
						<td>
							<telerik:RadComboBox ID="ddlIdTipoControl" runat="server" DataTextField="NombreValor" DataValueField="Id" EmptyMessage="Seleccione" Culture="es-ES" meta:resourcekey="ddlIdTipoControlResource1" Visible="false" />
							<asp:RequiredFieldValidator ID="rfvIdTipoControl" runat="server" ErrorMessage="*" ControlToValidate="ddlIdTipoControl" CssClass="validator" Width="25px" meta:resourcekey="rfvIdTipoControlResource1" />
						</td>
					</tr>
				</table>
			</fieldset>
			<hcc:Publicacion ID="Publicacion" runat="server" />
			<hcc:Auditoria ID="Auditoria" runat="server" />
		</asp:Panel>
		<telerik:RadInputManager ID="RadInputManager1" runat="server">
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdPaso" EmptyMessage="Escriba IdPaso" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtIdPaso" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdBloque" EmptyMessage="Escriba IdBloque" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtIdBloque" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorPosicion" EmptyMessage="Escriba Posicion" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtPosicion" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
		</telerik:RadInputManager>
		<br />
		<br />
		<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1" />
		<asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" OnClick="cmdGuardaryAgregar_Click" meta:resourcekey="cmdGuardaryAgregarResource1" />
		<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="$('form').clearForm(); return false" meta:resourcekey="cmdLimpiarResource1" />
	</div>
	<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
		<script type="text/javascript">
			function IrAnterior() {
				var wnd = GetRadWindow();
				wnd.setUrl('<%= RutaPadreEncriptada %>');
			}
		</script>
	</telerik:RadScriptBlock>
</asp:Content>
