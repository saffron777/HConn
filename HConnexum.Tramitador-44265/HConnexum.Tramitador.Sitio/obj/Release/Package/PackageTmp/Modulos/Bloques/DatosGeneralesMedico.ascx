<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosGeneralesMedico.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.DatosGeneralesMedico" %>
<%@ Register Src="~/ControlesComunes/Telefono.ascx" TagName="Telefono" TagPrefix="hcc" %>

<table class="ancho" border="0">
	<tr>
		<td class="labelCell">
			<asp:Label ID="lblAsegurado" runat="server" Text="Médico:" />
		</td>
		<td class="fieldCell">
			<telerik:RadComboBox ID="tipdocmed" DataValueField="NombreValor" DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="40px"/>
			<asp:TextBox ID="numdocmed" runat="server" Width="208px"></asp:TextBox>
		</td>
		<td colspan="2" class="fieldCell" style="padding: inherit; margin: inherit">
			<asp:TextBox ID="nommed" runat="server" Width="99%"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="labelCell">
			<asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" />
		</td>
		<td class="fieldCell">
			<hcc:Telefono ID="tlfmed" runat="server" ClientIDMode="Inherit" />
		</td>
		<td class="labelCell">
			<asp:Label ID="lblIdExterno" runat="server" Text="Especialidad:" />
		</td>
		<td class="fieldCell" style="padding: 0px; margin: 0px">
			<asp:TextBox ID="espmed" runat="server" Width="98.3%"></asp:TextBox>
		</td>
	</tr>
</table>
