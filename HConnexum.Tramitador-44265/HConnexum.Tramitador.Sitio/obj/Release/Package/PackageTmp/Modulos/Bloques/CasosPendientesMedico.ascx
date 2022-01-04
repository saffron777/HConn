<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CasosPendientesMedico.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.CasosPendientesMedico" %>
<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="51%" CellSpacing="0" Culture="es-ES" GridLines="None" AllowCustomPaging="True" AllowPaging="True" AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
	OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource">
	<ClientSettings EnableRowHoverStyle="true">
		<Selecting AllowRowSelect="True" />
		<ClientEvents OnRowDblClick="ShowMessage" />
		<Scrolling AllowScroll="True" UseStaticHeaders="True" />
	</ClientSettings>
	<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado">
		<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
		<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
			<HeaderStyle Width="20px"></HeaderStyle>
		</RowIndicatorColumn>
		<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
			<HeaderStyle Width="20px"></HeaderStyle>
		</ExpandCollapseColumn>
		<Columns>
			<telerik:GridBoundColumn DataField="IdCaso" FilterControlAltText="Filtrar columna IdCaso" HeaderText="Id Caso" UniqueName="IdCaso" meta:resourcekey="GridBoundColumnResource1"/>
			<telerik:GridCheckBoxColumn DataField="Asegurado" FilterControlAltText="Filtrar columna Asegurado" HeaderText="Asegurado" UniqueName="Asegurado" meta:resourcekey="GridCheckBoxColumnResource1"/>
			<telerik:GridCheckBoxColumn DataField="DocumentoIdentificacion" FilterControlAltText="Filtrar columna DocumentoIdentificacion" HeaderText="Documento de Identificación" UniqueName="DocumentoIdentificacion" meta:resourcekey="GridCheckBoxColumnResource2"/>
		</Columns>
		<EditFormSettings>
			<EditColumn FilterControlAltText="Filter EditCommandColumn column">
			</EditColumn>
		</EditFormSettings>
		<PagerStyle AlwaysVisible="True" />
		<CommandItemTemplate>
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td align="Left" style="vertical-align: middle; font-weight: bold" height="40px">
						<asp:Label ID="Label1" runat="server" Text="Casos Pendientes" Font-Bold="True"></asp:Label>
					</td>
				</tr>
			</table>
		</CommandItemTemplate>
	</MasterTableView>
	<PagerStyle AlwaysVisible="True" />
	<FilterMenu EnableImageSprites="False">
	</FilterMenu>
	<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
	</HeaderContextMenu>
</telerik:RadGrid>
