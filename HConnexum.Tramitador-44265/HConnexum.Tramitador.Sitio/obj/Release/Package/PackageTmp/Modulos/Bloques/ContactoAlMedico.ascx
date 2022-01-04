<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactoAlMedico.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.ContactoAlMedico" %>

<table class="ancho" border="0">
	<tr>
		<td class="labelCell"><asp:Label ID="Label1" runat="server" Text="¿El médico fue contactado?"></asp:Label></td>
		<td class="fieldCell">
			<telerik:RadComboBox ID="MedicoContacto" runat="server" DataTextField="NombreValor" DataValueField="NombreValor" EmptyMessage="Seleccione..." Width="100%" />
		</td>
		<td class="labelCell">&nbsp;</td>
		<td class="fieldCell">&nbsp;</td>
	</tr>
</table>
