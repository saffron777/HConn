<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Detalle de FlujosServicio" CodeBehind="SolicitudBloqueMaestroDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.SolicitudBloqueMaestroDetalle" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadGridDetails">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadGridMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadFilterMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadFilterMaster" />
					<telerik:AjaxUpdatedControl ControlID="LblMessege" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ApplyButton">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
					<telerik:AjaxUpdatedControl ControlID="RadFilterMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
		<asp:Panel ID="PanelMaster" runat="server">
			<fieldset>
				<legend><b>
					<asp:Label runat="server" Text="Flujo Servicio" Font-Bold="True" meta:resourcekey="LblLegendFlujosServicioResource" /></b></legend>
				<table>
					<tr>
						<td>
							<asp:Label ID="lblIdSuscriptor" runat="server" Text="IdSuscriptor:" />
						</td>
						<td>
							<asp:TextBox ID="txtIdSuscriptor" runat="server" />
						</td>
						<td>
							<asp:Label ID="lblIdServicioSuscriptor" runat="server" Text="IdServicioSuscriptor:" />
						</td>
						<td>
							<asp:TextBox ID="txtIdServicioSuscriptor" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblIndPublico" runat="server" Text="IndPublico:" />
						</td>
						<td>
							<asp:CheckBox ID="chkIndPublico" runat="server" />
						</td>
						<td>
							<asp:Label ID="lblVersion" runat="server" Text="Version:" />
						</td>
						<td>
							<asp:TextBox ID="txtVersion" runat="server" />
						</td>
					</tr>
				</table>
			</fieldset>
			<hcc:Publicacion ID="Publicacion" runat="server" />
			<hcc:Auditoria ID="Auditoria" runat="server" />
		</asp:Panel>
		<br />
		<br />
		<telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
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
				var ventanaDetalle = "Modulos/Parametrizador/SolicitudBloqueDetalle.aspx?IdMenu=" + idMenu + "&";
				var nombreBoton = '<%=btnActivarEliminado.ClientID%>';
				window.onload = function () {
					changeTextRadAlert();
				}
			</script>
		</telerik:RadScriptBlock>
		<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%" CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True" AllowMultiRowSelection="True" AllowSorting="True" Skin="Windows7" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
			OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand">
			<ClientSettings EnableRowHoverStyle="true">
				<Selecting AllowRowSelect="True" />
				<ClientEvents OnRowDblClick="validarRegistro" />
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
					<telerik:GridBoundColumn DataField="NombreBloque" FilterControlAltText="Filtrar columna Bloque" DataFormatString="{0:N0}" HeaderText="Bloque" UniqueName="NombreBloque"/>
					<telerik:GridBoundColumn DataField="Orden" FilterControlAltText="Filtrar columna Orden" DataFormatString="{0:N0}" HeaderText="Orden" UniqueName="Orden"/>
					<telerik:GridCheckBoxColumn DataField="IndCierre" FilterControlAltText="Filtrar columna Cierre" HeaderText="Cierre" UniqueName="IndCierre" />
					<telerik:GridBoundColumn DataField="NombreTipoControl" FilterControlAltText="Filtrar columna Tipo Control" DataFormatString="{0:N0}" HeaderText="Tipo Control" UniqueName="NombreTipoControl"/>
					<telerik:GridBoundColumn DataField="TituloBloque" FilterControlAltText="Filtrar columna Titulo Bloque" HeaderText="Titulo Bloque" UniqueName="TituloBloque"/>
					<telerik:GridCheckBoxColumn DataField="IndActualizable" FilterControlAltText="Filtrar columna Actualizable" HeaderText="Actualizable" UniqueName="IndActualizable" />
					<telerik:GridBoundColumn DataField="KeyCampoXML" FilterControlAltText="Filtrar columna Key Campo XML" HeaderText="Key Campo XML" UniqueName="KeyCampoXML"/>
					<telerik:GridCheckBoxColumn DataField="IndVigente" FilterControlAltText="Filtrar columna IndVigente" HeaderText="Publicar" UniqueName="IndVigente" meta:resourcekey="GridCheckBoxColumnResource1" />
					<telerik:GridBoundColumn DataField="FechaValidez" FilterControlAltText="Filtrar columna FechaValidez" DataFormatString="{0:d}" HeaderText="Fecha Validez" UniqueName="FechaValidez" meta:resourcekey="GridBoundColumnResource2" />
					<telerik:GridCheckBoxColumn DataField="IndEliminado" FilterControlAltText="Filtrar columna IdEliminado" HeaderText="Eliminado" UniqueName="IndEliminado">
					</telerik:GridCheckBoxColumn>
					<telerik:GridTemplateColumn HeaderText="Tomado" UniqueName="Tomado" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" meta:resourcekey="GridTemplateColumnResource1">
						<ItemTemplate>
							<%if(Request.Browser.Browser == "IE" && Request.Browser.MajorVersion < 7) { %>
								<asp:Image runat="server" ImageUrl='<%# this.ResolveUrl(Eval("Tomado", "~/Imagenes/{0}.gif")) %>' ToolTip='<%# Eval("UsuarioTomado") %>' />
							<% } else { %>
								<asp:Image runat="server" ImageUrl='<%# this.ResolveUrl(Eval("Tomado", "~/Imagenes/{0}.png")) %>' ToolTip='<%# Eval("UsuarioTomado") %>' />
							<% } %>
						</ItemTemplate>
						<HeaderStyle Width="70px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</telerik:GridTemplateColumn>
				</Columns>
				<EditFormSettings>
					<EditColumn FilterControlAltText="Filter EditCommandColumn column">
					</EditColumn>
				</EditFormSettings>
				<PagerStyle AlwaysVisible="True" />
				<CommandItemTemplate>
					<table cellpadding="0" cellspacing="0" border="0" width="100%">
						<tr>
							<td align="right">
								<telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked" runat="server" Skin="Windows7" OnButtonClick="RadToolBar1_ButtonClick1">
									<Items>
										<telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" />
										<telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" />
										<telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" />
										<telerik:RadToolBarButton runat="server" CommandName="Add" Text="Agregar" ImagePosition="Right" PostBack="false" ImageUrl="~/Imagenes/AddRecord.gif" />
										<telerik:RadToolBarButton runat="server" CommandName="Edit" ImagePosition="Right" PostBack="false" ImageUrl="~/Imagenes/Edit.gif" Text="Editar" />
										<telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right" ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" />
									</Items>
								</telerik:RadToolBar>
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
		<div style="display: none">
			<telerik:RadButton ID="btnActivarEliminado" runat="server" Text="" OnClick="btnActivarEliminado_Click"/>
		</div>
		<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" Skin="Windows7" DestroyOnClose="true" Title="Filtro" Width="600px" KeepInScreenBounds="true">
			<ContentTemplate>
				<fieldset>
					<legend><b>
						<asp:Label runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource" /></b></legend>
					<table>
						<tr>
							<td>
								<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" Skin="Windows7" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7" />
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
		<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
			<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
		</telerik:RadCodeBlock>
	</div>
</asp:Content>
