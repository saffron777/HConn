<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlqNotRespInmCAActivacion.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.BlqNotRespInmCAActivacion" %>
<style type="text/css">
.Sinscrol{ overflow-y:hidden; }

</style>
<table border="0" width="100%">
	<tr>
		<td>
			<asp:TextBox runat="server" ID="txtMensaje" TextMode="MultiLine" Rows="6" Width="100%" ReadOnly="true"/>
			<asp:HiddenField runat="server" ID="ActivacionCa"/>
			<asp:HiddenField runat="server" ID="EstatusMovimientoWeb"/>
            <asp:HiddenField runat="server" ID="IdSupportIncident"/>
		</td>
	</tr>
	<tr>
		<td style="vertical-align: bottom;">
			<asp:Button ID="btnImprimir" runat="server" Text="Imprimir Carta Aval" OnClick="btnImprimir_Click" Visible="false" />
		</td>
	</tr>
</table>
