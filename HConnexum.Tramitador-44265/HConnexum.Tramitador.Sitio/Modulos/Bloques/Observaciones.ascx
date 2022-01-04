<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Observaciones.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.Observaciones" %>
<fieldset class="coolfieldset">
	<br />
	<div style="width:100%;">
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td class="labelCell" style="vertical-align: top;">
					<asp:Label ID="label1" runat="server" Text="Observaciones:"></asp:Label>
				</td>
				<td style="padding-left: 7px; padding-right: 21px;">
					<asp:TextBox ID="observacion" runat="server" TextMode="MultiLine" Width="100%" onclick="aspMaxLength(this, 500); return false;"></asp:TextBox>
				</td>
			</tr>
		</table>
	</div>
	<br />
</fieldset>
