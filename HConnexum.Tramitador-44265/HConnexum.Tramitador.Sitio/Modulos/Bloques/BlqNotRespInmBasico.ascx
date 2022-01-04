<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlqNotRespInmBasico.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.BlqNotRespInmBasico" %>
<style type="text/css">
.Sinscrol{ overflow-y:hidden; }

</style>
<table border="0" width="100%">
	<tr>
		<td style="padding-right:10px;">
			<asp:TextBox runat="server" ID="txtMensaje" TextMode="MultiLine" Rows="6" Width="100%" ReadOnly="true" style="overflow:hidden"/>
			<asp:HiddenField runat="server" ID="IdReclamo" />
			<asp:HiddenField runat="server" ID="IndMvtoAutomatico" />
			<asp:HiddenField runat="server" ID="EstatusMovimientoWeb" />
		</td>
	</tr>
	<tr>
		<td style="vertical-align: bottom;">
			<asp:Button runat="server" ID="btnImprimir" Text="Imprimir Comprobante" Visible="false" OnClick="btnImprimir_OnClick" />
		</td>
	</tr>
</table>
