<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListadoMovimientos.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.ListadoMovimientos" %>
<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%" CellSpacing="0" GridLines="None" AllowCustomPaging="false" AllowPaging="false" AllowSorting="True" AllowFilteringByColumn="false" OnSortCommand="RadGridMaster_SortCommand" OnNeedDataSource="RadGridMaster_NeedDataSource">
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
			<telerik:GridBoundColumn DataField="Movimiento" FilterControlAltText="Filtrar columna" HeaderText="Movimiento" UniqueName="Movimiento" AutoPostBackOnFilter="True" HeaderStyle-Width="20%" ItemStyle-Width="20%" />
			<telerik:GridBoundColumn DataField="FechaSolicitud" FilterControlAltText="Filtrar columna" HeaderText="Fecha Solicitud" UniqueName="FechaSolicitud" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="False" HeaderStyle-Width="20%" ItemStyle-Width="20%"/>
			<telerik:GridBoundColumn DataField="HoraSolicitud" FilterControlAltText="Filtrar columna" HeaderText="Hora Solicitud" UniqueName="HoraSolicitud" DataFormatString="{0:t}" AllowFiltering="False" HeaderStyle-Width="20%" ItemStyle-Width="20%"/>
			<telerik:GridBoundColumn DataField="NombreOperador" FilterControlAltText="Filtrar columna" HeaderText="Creado Por" UniqueName="NombreOperador" AllowFiltering="False" HeaderStyle-Width="20%" ItemStyle-Width="20%"/>
			<telerik:GridBoundColumn DataField="Resultado" FilterControlAltText="Filtrar columna" HeaderText="Resultado" UniqueName="Resultado" AllowFiltering="False" HeaderStyle-Width="20%" ItemStyle-Width="20%"/>
		</Columns>
		<EditFormSettings>
			<EditColumn FilterControlAltText="Filter EditCommandColumn column">
			</EditColumn>
		</EditFormSettings>
		<PagerStyle AlwaysVisible="false" />
		<CommandItemTemplate>
		</CommandItemTemplate>
	</MasterTableView>
	<PagerStyle AlwaysVisible="false" />
	<FilterMenu EnableImageSprites="False" OnClientShown="MenuShowing">
	</FilterMenu>
	<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
	</HeaderContextMenu>
</telerik:RadGrid>
