<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosExpedientesCE.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.DatosExpedientesCE" %>
<style type="text/css">
TD.labelCell { white-space:normal !important;}
</style>
<table border="0" width="100%">
	<tr>
		<td class="labelCell4colP" align="left">
			<asp:Label runat="server" Text="Síntomas" />
		</td>
		<td class="labelCell4colP">
			<asp:Label runat="server" Text="Clave" />
		</td>
		<td class="labelCell4colP">
			<asp:Label runat="server" Text="Fecha Ocurrencia" />
		</td>
		<td class="labelCell4colP">
			<asp:Label runat="server" Text="Último Movimiento" />
		</td>
	</tr>
	<tr>
		<td class="fieldCell4colP" style="margin-left: 40px">
			<asp:TextBox ID="NomSintomas" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="txtClave" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="FecOcurrencia" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="TipoMov" runat="server" Width="90%" />
		</td>
	</tr>
	<tr>
		<td class="labelCell4colP">
			<asp:Label runat="server" Text="Categoría" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="Label4" runat="server" Text="Días de Hospitalización" />
		</td>
		<td class="labelCell4colP">
			<asp:Label ID="Label1" runat="server" Text="Responsable" />
		</td>
		<td class="labelCell4colP">
		</td>
	</tr>
	<tr>
		<td class="fieldCell4colP">
			<asp:TextBox ID="NomTipoServicio" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="NumDiasHosp" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
			<asp:TextBox ID="txtResponsable" runat="server" Width="90%" />
		</td>
		<td class="fieldCell4colP">
		</td>
	</tr>
	<tr>
		<td class="labelCell" colspan="4" style="padding-right:10px">
			<br/>
            <div style="width:100%">
			<telerik:RadGrid ID="RadGridMaster" runat="server" ShowFooter="True" AutoGenerateColumns="False" Width="100%" CellSpacing="0" GridLines="None" AllowCustomPaging="false" AllowPaging="false" AllowFilteringByColumn="false" AllowSorting="True" OnSortCommand="RadGridMaster_SortCommand" OnNeedDataSource="RadGridMaster_NeedDataSource" OnCustomAggregate="RadGridMaster_CustomAggregate">
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
						<telerik:GridBoundColumn DataField="Certificado" FilterControlAltText="Filtrar columna" HeaderText="Certificado" UniqueName="Certificado" HeaderStyle-Width="6%" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" />
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
			<br/>
		</td>
	</tr>
	<tr>
		<td class="labelCell" colspan="4" >
			<table border="0" width="100%">
				<tr>
					<td style="margin:0; padding-left: 5px; padding-right: 10px; font-weight: bold; width: 33%;">
						<asp:Label runat="server" Text="Observaciones" />
					</td>
					<td style="margin:0; padding-left: 5px; padding-right: 10px; font-weight: bold; width: 33%;">
						<asp:Label runat="server" Text="Observaciones Procesadas" />
					</td>
					<td style="margin:0; padding-left: 5px; padding-right: 10px; font-weight: bold; width: 33%;">
						<asp:Label runat="server" Text="Documentos Fax Solicitados" />
					</td>
				</tr>
				<tr>
					<td style="margin:0; padding-left: 5px; padding-right: 10px; width: 33%;">
						<asp:TextBox ID="observacioneswebtxt" runat="server" TextMode="MultiLine" Rows="3" Width="96%" Style="overflow:auto" />
					</td>
					<td style="margin:0; padding-left: 5px; padding-right: 10px; width: 33%;">
						<asp:TextBox ID="observadeftxt" runat="server" TextMode="MultiLine" Rows="3" Width="96%" Style="overflow:auto"  />
					</td>
					<td style="margin:0; padding-left: 5px; padding-right: 10px; width: 33%;">
						<asp:TextBox ID="Documentos_Fax_Adicionalestxt" runat="server" TextMode="MultiLine" Rows="3" Width="96%" Style="overflow:auto"  />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
