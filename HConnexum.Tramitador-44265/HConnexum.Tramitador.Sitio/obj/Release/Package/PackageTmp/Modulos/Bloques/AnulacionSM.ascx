<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnulacionSM.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.AnulacionSM" %>

<table class="ancho" border="0">
	<tr>
		<td class="labelCell"><asp:Label runat="server" Text="¿Aprueba la anulación de los casos?" /></td>
		<td class="fieldCell"><telerik:RadComboBox runat="server" ID="ApruebaAnula" DataTextField="NombreValor" DataValueField="NombreValor" EmptyMessage="Seleccione" Culture="(Default)" TabIndex="1" Width="100%"/></td>
		<td class="labelCell">&nbsp;</td>
		<td class="fieldCell">&nbsp;<td>
	</tr>
</table>
