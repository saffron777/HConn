<%@ Page Language="C#" AutoEventWireup="true"  Title="Listado de Observaciones del Movimiento de un Caso" CodeBehind="ObservacionesTabMovimientoLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.ObservacionesMovimientoLista" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
   <style type="text/css">      
       div.RadGrid .rgPager .rgAdvPart     
       {     
        display:none;        
       }     
   </style>
    <body>
        <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
	<telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
		<script type="text/javascript">
				var nombreVentana = '<%=RadWindow1.ClientID %>';
				var nombreGrid = '<%=RadGridMaster.ClientID%>';
				var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
				var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
				var idMaster = "<%= idMaster %>";
				var AccionAgregar = "<%= AccionAgregar %>";
				var AccionModificar = "<%= AccionModificar %>";
				var AccionVer = "<%= AccionVer %>";
				var idMenu = '<%= IdMenuEncriptado %>';
				var ventanaDetalle = "Modulos/ESCRIBA SU MODULO AQUI/ObservacionesMovimientoDetalle.aspx?IdMenu=" + idMenu + "&";
		</script>
	</telerik:RadScriptBlock>
	<telerik:RadAjaxManager id="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadGridMaster">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
			</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadFilterMaster">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="RadFilterMaster"/>
				<telerik:AjaxUpdatedControl ControlID="LblMessege" />
			</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ApplyButton">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				<telerik:AjaxUpdatedControl ControlID="RadFilterMaster"/>
			</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
			<UpdatedControls>
				<telerik:AjaxUpdatedControl ControlID="RadGridMaster"/>
			</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<telerik:RadGrid id="RadGridMaster" runat="server" autogeneratecolumns="False" width="100%"
		cellspacing="0" gridlines="None" allowcustompaging="True" allowpaging="True"
		allowmultirowselection="True" allowsorting="True"  onpageindexchanged="RadGridMaster_PageIndexChanged"
		onsortcommand="RadGridMaster_SortCommand" 
            onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
		onitemcommand="RadGridMaster_ItemCommand" SelectedItemStyle-Wrap="True" 
            Culture="es-ES" Height="190px" PageSize="4">
		<ClientSettings EnableRowHoverStyle="true">
			<Selecting />
			
			<Scrolling AllowScroll="True" UseStaticHeaders="True" />
<Selecting CellSelectionMode="None"></Selecting>

<Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
		</ClientSettings>
		<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado" width="100%">
			<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
			<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
			<HeaderStyle Width="20px"></HeaderStyle>
			</RowIndicatorColumn>
			<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
			<HeaderStyle Width="20px"></HeaderStyle>
			</ExpandCollapseColumn>
			<Columns>
             <telerik:GridBoundColumn DataField="Fecha" 
                    FilterControlAltText="Filter column Fecha" HeaderText="Fecha" 
                    UniqueName="Fecha" >
			     <HeaderStyle Width="22%" />
                 <ItemStyle Width="22%" />
                </telerik:GridBoundColumn>
			<telerik:GridBoundColumn DataField="usuario" 
                    FilterControlAltText="Filtrar columna usuario" DataFormatString="{0:N0}" 
                    HeaderText="Usuario" UniqueName="usuario"  >
		            <HeaderStyle Width="14%" />
                    <ItemStyle Width="14%" />
                </telerik:GridBoundColumn>
		            <telerik:GridBoundColumn DataField="Observacion" 
                    FilterControlAltText="Filtrar columna Observacion"  HeaderText="Observacion" 
                    UniqueName="Observacion" >
		            

				
			            <HeaderStyle Width="52%" />
                        <ItemStyle Width="52%" />
                </telerik:GridBoundColumn>
		            

				
			</Columns>
			<EditFormSettings><EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn></EditFormSettings>
			<PagerStyle AlwaysVisible="True" />
			<CommandItemTemplate>
				<table cellpadding="0" cellspacing="0" border="0" width="100%">
					<tr>
						<td align="right">
							<telerik:RadToolBar ID="RadToolBar1"  OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked" runat="server"  onbuttonclick="RadToolBar1_ButtonClick1" >
								<Items>
									<telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar"/>
									<telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro"/>
									
								</Items>
							</telerik:RadToolBar>
						</td>
					</tr>
				</table>
			</CommandItemTemplate>
		</MasterTableView>
		<PagerStyle AlwaysVisible="True"  />

<SelectedItemStyle Wrap="True"></SelectedItemStyle>

		<FilterMenu EnableImageSprites="False"></FilterMenu>
		<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
        </HeaderContextMenu>
	</telerik:RadGrid>
	<telerik:RadWindow id="RadWindow1" runat="server" height="350px" 
		destroyonclose="true" title="Filtro" width="600px" keepinscreenbounds="true">
		<ContentTemplate>
			<fieldset>
				<legend><b><asp:Label runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource"/></b></legend>
				<table>
					<tr>
						<td>
							<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False"  CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 "/>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="LblMessege" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					<tr>
						<td>
							<asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" />
						</td>
					</tr>
				</table>
			</fieldset>
		</ContentTemplate>
	</telerik:RadWindow>
	<telerik:RadCodeBlock id="RadCodeBlock1" runat="server">
		<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
	</telerik:RadCodeBlock>
        </form>
    </body>
</html>
