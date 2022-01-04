<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MuestraChat.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.MuestraChat" %>
<style type="text/css">
.tabla {width:600px;margin: 10px 0 10px 10px;}
.mensaje {width:600px;}
TD.labelCell { white-space:normal !important;}
TD { white-space:normal !important;}
</style>
<table border="0" cellpadding="0" cellspacing="0" class="tabla">
	<tr>
		<td style="text-align:right;padding-top:5px; padding-left:8px;">
			<asp:TextBox runat="server" ID="txtMensaje" TextMode="MultiLine" CssClass="mensaje" Rows="4" style="overflow:auto;"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td style="text-align:right;padding-top:5px; padding-left:8px; white-space:normal;">
			<asp:Button runat="server" ID="btnEnviar" Text="Enviar" ValidationGroup="ChaT"
				onclick="btnEnviar_Click" /><br />&nbsp;<br />
			<telerik:RadGrid runat="server" ID="rgBitacora" AutoGenerateColumns="False" 
				CellSpacing="0" Culture="es-ES" GridLines="None" ShowHeader="False" CellPadding="0" >
				<ClientSettings>
				<Selecting CellSelectionMode="None"></Selecting>
				</ClientSettings>
				<MasterTableView ShowHeader="False" CellPadding="0" CellSpacing="0">
					<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
					<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
					<HeaderStyle Width="20px"></HeaderStyle>
					</RowIndicatorColumn>
					<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
					<HeaderStyle Width="20px"></HeaderStyle>
					</ExpandCollapseColumn>
					<Columns>
						<telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" 
							UniqueName="TemplateColumn">
							<ItemTemplate>
								<table cellpadding="0" cellspacing="0" style="width:100%; white-space:normal;">
									<tr>
										<td style="text-align: left;border-color: #FFFFFF;">
											Enviado por:&nbsp;<asp:Label ID="lblOperadorRemitente" runat="server" Text='<%#Eval("Remitente")%>'></asp:Label>&nbsp;-
											<asp:Label ID="lblSuscriptorRemitente" runat="server" Text='<%#Eval("NombreSuscriptorEnvio")%>'></asp:Label>&nbsp;el
											<asp:Label ID="lblEnvioFecha" runat="server" Text='<%# Eval("FechaCreacion") %>'></asp:Label>
										</td>
									</tr>
									<tr>
										<td style="text-align: left;border-color: #FFFFFF;">
											Leído por:&nbsp;<asp:Label ID="lblOperadorLector" runat="server" Text='<%# Eval("LeidoPor") %>'></asp:Label>&nbsp;
											<asp:Label ID="lblSuscriptorLector" runat="server" Text='<%# Eval("NombreSuscriptorRecibe") %>'></asp:Label>&nbsp;el
                                            <asp:Label ID="lblRecibeFecha" runat="server" Text='<%# Eval("FechaModificacion") %>'></asp:Label>
										</td>
									</tr>
									<tr>
										<td style="text-align:center; border-color:#FFFFFF; padding-right:12px; white-space:normal;">
											<asp:TextBox ID="txtMensajeAMostrar" runat="server" Width="550px" TextMode="MultiLine" Style="overflow: auto;"
												Text='<%#Eval("mensaje")%>' ReadOnly="True" BorderStyle="None" Font-Bold="True"></asp:TextBox>
										</td>
									</tr>
								</table>
							</ItemTemplate>
						</telerik:GridTemplateColumn>
					</Columns>
					<EditFormSettings>
					<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
					</EditFormSettings>
				</MasterTableView>
				<FilterMenu EnableImageSprites="False"></FilterMenu>
			</telerik:RadGrid>
		</td>
	</tr>
</table>
<span class="arrange"><asp:HiddenField runat="server" ID="hidCasoId"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="hidMovimientoId"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="hidEnvioSuscriptorId"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="hidRemitente"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="hidCreadoPor"/></span>