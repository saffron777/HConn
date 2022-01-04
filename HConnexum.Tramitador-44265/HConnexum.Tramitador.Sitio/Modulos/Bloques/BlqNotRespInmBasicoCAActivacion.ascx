<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlqNotRespInmBasicoCAActivacion.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.BlqNotRespInmBasicoCAActivacion" %>
<style type="text/css">
.Sinscrol{ overflow-y:hidden; }

</style>
<table border="0" width="100%">
	<tr>
		<td style="padding-right:10px;">
			<asp:TextBox runat="server" ID="txtMensaje" TextMode="MultiLine" Rows="6" Width="100%" ReadOnly="true" style="overflow:hidden;"/>
			<asp:HiddenField runat="server" ID="ActivacionCa"/>
			<asp:HiddenField runat="server" ID="EstatusMovimientoWeb"/>
		</td>
	</tr>
	<tr>
		<td style="vertical-align: bottom;">
			<asp:Button ID="btnImprimir" runat="server" Text="Imprimir Carta Aval" OnClick="btnImprimir_Click" Visible="false" />
		</td>
	</tr>
</table>
