<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Litado de Agrupacion" CodeBehind="AgrupacionLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.AgrupacionLista" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
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
				var ventanaDetalle = "Modulos/Estructura/AgrupacionDetalle.aspx?IdMenu=" + idMenu + "&";
				var nombreBoton = '<%=btnActivarEliminado.ClientID%>';

				window.onload = function () {
				    changeTextRadAlert();
				}
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
		onsortcommand="RadGridMaster_SortCommand" onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
		onitemcommand="RadGridMaster_ItemCommand">
		<ClientSettings EnableRowHoverStyle="true">
			<Selecting AllowRowSelect="True" />
			<ClientEvents OnRowDblClick="ValidarRegistro" />
			<Scrolling AllowScroll="True" UseStaticHeaders="True" />
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
			<telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna Id" DataFormatString="{0:N0}" HeaderText="Id" UniqueName="Id" visible="False"></telerik:GridBoundColumn>
		<telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filtrar columna Nombre"  HeaderText="Nombre" UniqueName="Nombre"></telerik:GridBoundColumn>
		<telerik:GridBoundColumn DataField="IndVigente" FilterControlAltText="Filtrar columna IndVigente"  HeaderText="IndVigente" UniqueName="IndVigente"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FechaValidez" 
                                                FilterControlAltText="Filtrar columna FechaValidez"  HeaderText="Fecha Validez" 
                                                UniqueName="FechaValidez" DataFormatString="{0:d}" 
                                                meta:resourcekey="GridBoundColumnResource3"></telerik:GridBoundColumn>
				<telerik:GridCheckBoxColumn DataField="IndEliminado" FilterControlAltText="Filtrar columna IdEliminado" HeaderText="Eliminado" UniqueName="IndEliminado"></telerik:GridCheckBoxColumn><telerik:GridBoundColumn DataField="FechaValidez" FilterControlAltText="Filter column1 column" HeaderText="Fecha Validez" UniqueName="column1" DataFormatString="{0:d}" meta:resourcekey="GridBoundColumnResource3" />
				<telerik:GridTemplateColumn HeaderText="Tomado" UniqueName="Tomado" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate><asp:Image runat="server" ID="imgTomado" ImageUrl='<%# this.ResolveUrl(Eval("Tomado", "~/Imagenes/{0}.png")) %>' ToolTip='<%# Eval("UsuarioTomado") %>'/></ItemTemplate>
				</telerik:GridTemplateColumn>
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
									<telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle"/>
									<telerik:RadToolBarButton runat="server" CommandName="Add" Text="Agregar" ImagePosition="Right" PostBack="false" ImageUrl="~/Imagenes/AddRecord.gif"/>
									<telerik:RadToolBarButton runat="server" CommandName="Edit" ImagePosition="Right" PostBack="false" ImageUrl="~/Imagenes/Edit.gif" Text="Editar"/>
									<telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right" ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar"/>
								</Items>
							</telerik:RadToolBar>
						</td>
					</tr>
				</table>
			</CommandItemTemplate>
		</MasterTableView>
		<PagerStyle AlwaysVisible="True"  />
		<FilterMenu EnableImageSprites="False"></FilterMenu>
		<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
	</telerik:RadGrid>
            <div style="display:none">
                <telerik:RadButton ID="btnActivarEliminado" runat="server" Text="" onclick="btnActivarEliminado_Click"></telerik:RadButton>
	        </div>
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
</asp:Content>