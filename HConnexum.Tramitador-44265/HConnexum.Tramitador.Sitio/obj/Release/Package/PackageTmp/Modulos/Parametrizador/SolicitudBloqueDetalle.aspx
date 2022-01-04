<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Detalle de SolicitudBloque" CodeBehind="SolicitudBloqueDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.SolicitudBloqueDetalle" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadGridDetails">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
		<asp:Panel ID="PanelMaster" runat="server">
			<fieldset>
				<legend><b>
					<asp:Label runat="server" Text="Solicitud Bloque" Font-Bold="True" meta:resourcekey="LblLegendSolicitudBloqueResource" /></b></legend>
				<table>
					<tr>
						<td>
							<asp:Label ID="lblIdBloque" runat="server" Text="Bloque:" />
						</td>
						<td colspan="3">
							<telerik:RadComboBox ID="ddlIdBloque" DataValueField="Id" DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblOrden" runat="server" Text="Orden:" />
						</td>
						<td>
							<asp:TextBox ID="txtOrden" runat="server" />
						</td>
						<td>
							<asp:Label ID="lblIndCierre" runat="server" Text="Ind. Cierre:" />
						</td>
						<td>
							<asp:CheckBox ID="chkIndCierre" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblIdTipoControl" runat="server" Text="Tipo Control:" />
						</td>
						<td>
							<telerik:RadComboBox ID="ddlIdTipoControl" DataValueField="Id" DataTextField="NombreValor" EmptyMessage="Seleccione" runat="server" />
						</td>
						<td>
							<asp:Label ID="lblTituloBloque" runat="server" Text="Titulo Bloque:" />
						</td>
						<td>
							<asp:TextBox ID="txtTituloBloque" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblIndActualizable" runat="server" Text="Ind. Actualizable:" />
						</td>
						<td>
							<asp:CheckBox ID="chkIndActualizable" runat="server" />
						</td>
						<td>
							<asp:Label ID="lblKeyCampoXML" runat="server" Text="Key Campo XML:" />
						</td>
						<td>
							<asp:TextBox ID="txtKeyCampoXML" runat="server" />
						</td>
					</tr>
				</table>
			</fieldset>
			<hcc:Publicacion ID="Publicacion" runat="server" />
			<hcc:Auditoria ID="Auditoria" runat="server" />
		</asp:Panel>
		<telerik:RadInputManager ID="RadInputManager1" runat="server">
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdFlujoServicio" EmptyMessage="Escriba IdFlujoServicio" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtIdFlujoServicio" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdBloque" EmptyMessage="Escriba IdBloque" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtIdBloque" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorOrden" EmptyMessage="Escriba Orden" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtOrden" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdTipoControl" EmptyMessage="Escriba IdTipoControl" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtIdTipoControl" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
			<telerik:TextBoxSetting BehaviorID="BehaviorTituloBloque" EmptyMessage="Escriba TituloBloque" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtTituloBloque" />
				</TargetControls>
			</telerik:TextBoxSetting>
			<telerik:TextBoxSetting BehaviorID="BehaviorKeyCampoXML" EmptyMessage="Escriba KeyCampoXML" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtKeyCampoXML" />
				</TargetControls>
			</telerik:TextBoxSetting>
		</telerik:RadInputManager>
		<br />
		<br />
		<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="cmdGuardar_Click" />
		<asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" OnClick="cmdGuardaryAgregar_Click" />
		<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="$('form').clearForm();return false" />
	</div>
</asp:Content>
