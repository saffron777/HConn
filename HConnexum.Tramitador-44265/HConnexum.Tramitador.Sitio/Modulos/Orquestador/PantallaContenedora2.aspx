<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaContenedora2.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Orquestador.PantallaContenedora2" %>
<%@ Register Src="~/ControlesComunes/Telefono.ascx" TagName="Telefono" TagPrefix="hcc" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
	<script language="javascript" type="text/javascript">
		function Redirect(arg) {
			window.location = '<%= UrlRedirect %>';
		}
	</script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<asp:UpdatePanel runat="server" ID="pContenedor"></asp:UpdatePanel>
	<asp:Panel ID="PanelMaster" runat="server">
		<fieldset>
			<legend>
				<asp:Label runat="server" Text="Datos de la Solicitud" />
			</legend>
			<table>
				<tr>
					<td><asp:Label runat="server" Text="Suscriptor" /></td>
					<td><asp:TextBox runat="server" ID="txtSuscriptor" Enabled="false" /></td>
					<td><asp:Label runat="server" Text="Servicio" /></td>
					<td><asp:TextBox runat="server" ID="txtServicio" Enabled="false" /></td>
				</tr>
				<tr>
					<td><asp:Label runat="server" Text="Fecha de la Solicitud" /></td>
					<td colspan="3"><asp:TextBox runat="server" ID="txtFechaSolicitud" Enabled="false" /></td>
				</tr>
				<tr>
					<td><asp:Label runat="server" Text="Solicitante" /></td>
					<td colspan="3">
						<telerik:RadComboBox ID="ddlTipDoc" runat="server" DataTextField="NombreValorCorto" DataValueField="Id" EmptyMessage="Seleccione:" Width="95px" ExpandDelay="500" OnSelectedIndexChanged="ddlTipDoc_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false" />
						<asp:TextBox ID="txtNumDoc" runat="server" Width="231px" OnTextChanged="txtNumDoc_TextChanged" AutoPostBack="true" CausesValidation="false" />
					</td>
				</tr>
				<tr>
					<td><asp:Label runat="server" Text="Nombres" /></td>
					<td colspan="3"><asp:TextBox ID="txtNombres" runat="server" Width="330px" Enabled="false" /></td>
				</tr>
				<tr>
					<td><asp:Label runat="server" Text="Apellidos" /></td>
					<td colspan="3"><asp:TextBox ID="txtApellidos" runat="server" Width="330px" Enabled="false" /></td>
				</tr>
				<tr>
					<td><asp:Label runat="server" Text="Email de Contacto" /></td>
					<td><asp:TextBox ID="txtCorreo" runat="server" Width="200px" Enabled="false" /></td>
					<td><asp:Label runat="server" Text="Télefono de Contacto" /></td>
					<td><hcc:Telefono runat="server" ID="txtTelefono" IsRequired="true" Enabled="false" /></td>
				</tr>
				<asp:Panel runat="server" ID="pCasoRelacionado">
					<tr>
						<td><asp:Label runat="server" Text="Caso Relacionado" /></td>
						<td colspan="3"><asp:TextBox ID="txtCasoRelacionado" runat="server" Width="200px" /></td>
					</tr>
				</asp:Panel>
			</table>
		</fieldset>
	</asp:Panel>
	<br />

	<div style="text-align: right;"><asp:Button ID="cmdGuardar" runat="server" Text="Solicitar" OnClick="cmdGuardar_Click" /></div>

	<telerik:RadInputManager ID="RadInputManager1" runat="server">
		<telerik:TextBoxSetting BehaviorID="BehaviorTNombres" EmptyMessage="Escriba el Nombre" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
			<TargetControls>
				<telerik:TargetInput ControlID="txtNombres" />
			</TargetControls>
		</telerik:TextBoxSetting>
		<telerik:TextBoxSetting BehaviorID="BehaviorTApellidos" EmptyMessage="Escriba el Apellido" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
			<TargetControls>
				<telerik:TargetInput ControlID="txtApellidos" />
			</TargetControls>
		</telerik:TextBoxSetting>
		<telerik:RegExpTextBoxSetting BehaviorID="BehaviorCorreoelectronico" EmptyMessage="Escriba Email Electrónico" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio o inválido">
			<TargetControls>
				<telerik:TargetInput ControlID="txtCorreo" />
			</TargetControls>
		</telerik:RegExpTextBoxSetting>
	</telerik:RadInputManager>
</asp:Content>
