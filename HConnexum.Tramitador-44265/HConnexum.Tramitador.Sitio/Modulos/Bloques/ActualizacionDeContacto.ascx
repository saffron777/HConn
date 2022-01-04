<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ActualizacionDeContacto.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.ActualizacionDeContacto" %>
<%@ Register Src="~/ControlesComunes/Telefono.ascx" TagName="Telefono" TagPrefix="tel" %>

<table class="ancho" border="0">
	<tr>
		<td class="labelCell"><asp:Label ID="lblNuevoContacto" runat="server" Text="¿Nueva vía de contacto?" /></td>
		<td class="fieldCell">
			<telerik:RadComboBox ID="NuevoContacto" runat="server" DataValueField="NombreValor" DataTextField="NombreValor" OnSelectedIndexChanged="NuevoContacto_SelectedIndexChanged"
				AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="true" CausesValidation="false" Culture="es-ES" EmptyMessage="Seleccione" Width="90%" />
			<asp:RequiredFieldValidator ID="rfvNuevoContacto" runat="server" ControlToValidate="NuevoContacto" ErrorMessage="*" CssClass="validator" Width="25px" ValidationGroup="Validaciones" />
		</td>
		<td class="labelCell">&nbsp;</td>
		<td class="fieldCell">&nbsp;</td>
	</tr>
	<asp:Panel runat="server" ID="pTelefono">
		<tr>
			<td class="labelCell"><asp:Label runat="server" Text="Teléfono:" /></td>
			<td class="fieldCell"><tel:Telefono ID="tlfaseg" runat="server" IsRequired="true" ValidationGroup="Validaciones" /></td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
</table>
