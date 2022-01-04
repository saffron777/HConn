<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosExpedientesCA.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.DatosExpedientesCA" %>
<style type="text/css">
TD.labelCell { white-space:normal !important;}
</style>
<table border="0" width="100%">
	<tr>
		<td class="labelCell4colP" align="left">
			<asp:Label ID="lblSintomas" runat="server" Text="Síntomas" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="lblClave" runat="server" Text="Clave" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="lblMedTra" runat="server" Text="Medico Tratante" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="blUltmov" runat="server" Text="Último Movimiento" />
		</td>
	</tr>
	<tr>
		<td class="fieldCell4colP" style="margin-left: 40px">
			<asp:TextBox ID="Sintomastxt" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="txtClave" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="medicotratante" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="TipoMov" runat="server" Width="90%" />
		</td>
	</tr>
	<tr>
		<td class="labelCell4colP">
			<asp:Label ID="lblCategoria" runat="server" Text="Categoría" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="lblresponsable" runat="server" Text="Responsable" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="Label4" runat="server" Text="Días de Hospitalización" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="Label2" runat="server" Text="Fecha de Solicitud" />
		</td>
	</tr>
	<tr>
		<td class="fieldCell4colP">
			<asp:TextBox ID="NomTipoServicio" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="txtResponsable" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="NumDiasHosp" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="fechasolicitud" runat="server" Width="90%" />
		</td>
	</tr>
	<tr>
		<td class="labelCell4colP">
			<asp:Label ID="Label3" runat="server" Text="Fecha de Notificaci&oacute;n" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="Label5" runat="server" Text="Fecha Ocurrencia" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="Label1" runat="server" Text="Fecha de Vencimiento" />
		</td>
		<td class="labelCell4colP">
			&nbsp;
		</td>
	</tr>
	<tr>
		<td class="fieldCell4colP">
			<asp:TextBox ID="fechanotificacion" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="fechaocurrencia" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="fechavencimiento" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			&nbsp;
		</td>
	</tr>
	<tr>
		<td class="labelCell" colspan="4">
			<br />
            <div style="width:100%">
			<telerik:RadGrid ID="RadGridMaster" runat="server" ShowFooter="True" 
                AutoGenerateColumns="False" Width="100%" CellSpacing="0" GridLines="None" 
                AllowSorting="True" OnSortCommand="RadGridMaster_SortCommand" 
                OnNeedDataSource="RadGridMaster_NeedDataSource" 
                OnCustomAggregate="RadGridMaster_CustomAggregate" Culture="es-ES">
				<PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
				<GroupingSettings CaseSensitive="False" />
				<ClientSettings EnableRowHoverStyle="true">
					<Selecting AllowRowSelect="True" />
					<Scrolling AllowScroll="false" UseStaticHeaders="True" />
				</ClientSettings>
				<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" Width="100%">
					<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
					<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
						<HeaderStyle Width="20px"></HeaderStyle>
					</RowIndicatorColumn>
					<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
						<HeaderStyle Width="20px"></HeaderStyle>
					</ExpandCollapseColumn>
					<Columns>
						<telerik:GridBoundColumn Aggregate="Custom" DataField="Contratante" HeaderText="Contratante" UniqueName="Contratante" HeaderStyle-Width="24%" ItemStyle-Width="24%" FooterStyle-HorizontalAlign="Left" FooterStyle-Font-Bold="true" FooterText=" " ItemStyle-CssClass="WrapRadGrid"/>
						<telerik:GridBoundColumn DataField="Certificado" FilterControlAltText="Filtrar columna" HeaderText="Certificado" UniqueName="Certificado" HeaderStyle-Width="6%" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center"/>
						<telerik:GridBoundColumn Aggregate="Custom" DataField="Parentesco" HeaderText="Parentesco" UniqueName="Parentesco" HeaderStyle-Width="15%" ItemStyle-Width="15%" FooterStyle-HorizontalAlign="Left" FooterStyle-Font-Bold="true" FooterText=" " />
						<telerik:GridBoundColumn Aggregate="Custom" DataField="Cobertura" HeaderText="Cobertura" UniqueName="Cobertura" HeaderStyle-Width="20%" ItemStyle-Width="20%" FooterStyle-HorizontalAlign="Left" FooterStyle-Font-Bold="true" FooterText=" " />
						<telerik:GridBoundColumn DataField="Diasgnostico" HeaderText="Diasgnóstico" UniqueName="Diasgnostico" HeaderStyle-Width="23%" ItemStyle-Width="23%" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" FooterText="Total Aprobado:"  ItemStyle-CssClass="WrapRadGrid"/>
						<telerik:GridNumericColumn Aggregate="Custom" DataField="MontoCubierto" HeaderText="Monto Cubierto" UniqueName="MontoCubierto" DataFormatString="{0:N}" HeaderStyle-Width="12%" FilterControlWidth="12%" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true" FooterText=" " />
					</Columns>
					<EditFormSettings>
						<EditColumn FilterControlAltText="Filter EditCommandColumn column">
						</EditColumn>
					</EditFormSettings>
					<PagerStyle AlwaysVisible="true" />
					<CommandItemTemplate>
					</CommandItemTemplate>
				</MasterTableView>
				<PagerStyle AlwaysVisible="true" />
				<FilterMenu EnableImageSprites="False" OnClientShown="MenuShowing">
				</FilterMenu>
				<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
				</HeaderContextMenu>
			</telerik:RadGrid>
            </div>
			<br />
		</td>
	</tr>
	<tr>
		<td class="labelCell" colspan="4">
			<table border="0" width="100%">
				<tr>
					<td style="margin: 0; padding-left: 5px; padding-right: 10px; font-weight: bold; width: 33%;">
						<asp:Label ID="Label6" runat="server" Text="Observaciones" />
					</td>
					<td style="margin: 0; padding-left: 5px; padding-right: 10px; font-weight: bold; width: 33%;">
						<asp:Label ID="Label7" runat="server" Text="Observaciones Procesadas" />
					</td>
					<td style="margin: 0; padding-left: 5px; padding-right: 10px; font-weight: bold; width: 33%;">
						<asp:Label ID="Label8" runat="server" Text="Documentos Fax Solicitados" />
					</td>
				</tr>
				<tr>
					<td style="margin: 0; padding-left: 5px; padding-right: 10px; width: 33%;">
						<asp:TextBox ID="observacioneswebtxt" runat="server" TextMode="MultiLine" Rows="3" Width="96%" Style="overflow:auto"  />
					</td>
					<td style="margin: 0; padding-left: 5px; padding-right: 10px; width: 33%;">
						<asp:TextBox ID="observadeftxt" runat="server" TextMode="MultiLine" Rows="3" Width="96%" Style="overflow:auto"  />
					</td>
					<td style="margin: 0; padding-left: 5px; padding-right: 10px; width: 33%;">
						<asp:TextBox ID="Documentos_Fax_Adicionalestxt" runat="server" TextMode="MultiLine" Rows="3" Width="96%" Style="overflow:auto"  />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
