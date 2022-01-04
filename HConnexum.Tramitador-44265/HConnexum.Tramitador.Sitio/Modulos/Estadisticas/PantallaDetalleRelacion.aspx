<%@ Page Title="Detalle Relacion" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaDetalleRelacion.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaDetalleRelacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
	table.ancho { font-weight: bold; }
	.PanelMargen { margin-top: 10px; }
	.ocultar { visibility: hidden; }
	.boton-accion { background-color: #d9e5f2; }
	.readOnly { background: #D7D7D7; }
	.number { text-align: right; }
	.arrange { float: left; }
	html { overflow-x: hidden; }
	</style>
	<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
			var IdInterEncritado = "<%= IdInterEncriptado %>"

			window.onload = function () {
				changeTextRadAlert();
			}

			function RowDblClick(sender, args) {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Estadisticas/PantallaDetalleMovimientoRemesa.aspx?IdMenu=" + idMenu + "&intermediario=" + IdInterEncritado + "&nremesa=" + args.getDataKeyValue("NReclamosEncriptado"));
			}
		</script>
	</telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server" meta:resourcekey="RadAjaxManager1Resource1">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadGridMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
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
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<asp:Panel ID="DetalledelaRelacion" runat="server" Width="100%" GroupingText="DETALLE DE LA RELACI&Oacute;N">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblNdeRelacion" runat="server" Text="Nro. de Relaci&oacute;n" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechadeCreacion" runat="server" Text="Fecha de Creaci&oacute;n" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblBanco" runat="server" Text="Banco" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechadePago" runat="server" Text="Fecha de Pago" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="nrelacion" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechacreacion" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="banco" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechapago" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblStatus" runat="server" Text="Estatus" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblReferencia" runat="server" Text="Referencia" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFormadePago" runat="server" Text="Forma de Pago" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="status" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="referencia" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="formapago" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblMontoCubierto" runat="server" Text="Monto Cubierto" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblTotalRetenido" runat="server" Text="Total Retenido" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblMontoSujetoRetencion" runat="server" Text="Monto Sujeto Retenci&oacute;n" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblMontoImpMunicipal" runat="server" Text="Monto Imp Municipal" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montocubierto" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="totalretenido" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montosujetoretencion" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montoimpmunicipal" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblTotalPagar" runat="server" Text="Total a Pagar" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblNumeroCasos" runat="server" Text="Nro. Caso" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="totalpagar" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="numerocasos" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<asp:Panel ID="ResumenCasos" runat="server" Width="100%" GroupingText="RESUMEN DE CASOS">
		<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" CssClass="ancho" CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True"
			AllowMultiRowSelection="True" AllowSorting="True" OnNeedDataSource="RadGridMaster_NeedDataSource" OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
			OnSortCommand="RadGridMaster_SortCommand" OnItemCommand="RadGridMaster_ItemCommand">
				<ClientSettings EnableRowHoverStyle="true">
					<ClientEvents OnRowDblClick="RowDblClick" />
					<Selecting AllowRowSelect="True" />
					<Scrolling AllowScroll="True" UseStaticHeaders="True" />
				</ClientSettings>
				<MasterTableView CommandItemDisplay="Top" Width="100%" NoMasterRecordsText="No se encontraron registros" DataKeyNames="NroReclamo,NReclamosEncriptado" ClientDataKeyNames="NroReclamo,NReclamosEncriptado">
					<CommandItemSettings ExportToPdfText="Export to PDF" />
					<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
						<HeaderStyle Width="20px" />
					</RowIndicatorColumn>
					<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
						<HeaderStyle Width="20px" />
					</ExpandCollapseColumn>
					<Columns>
						<telerik:GridBoundColumn DataField="Reclamo" FilterControlAltText="Filtrar columna NombredelPaciente" DataFormatString="{0:d}" HeaderText="<b>Reclamo</b>" UniqueName="Reclamo"
							ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="NReclamosEncriptado" Visible="false" />
						<telerik:GridBoundColumn DataField="Asegurado" FilterControlAltText="Filtrar columna NombredelPaciente" DataFormatString="{0:d}" HeaderText="<b>Asegurado</b>" UniqueName="NombredelPaciente"
							ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25%" ItemStyle-Width="25%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="DocAsegurado" FilterControlAltText="Filtrar columna Cedula" HeaderText="<b>Documento de Identidad</b>" UniqueName="Cedula"
							ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="17%" ItemStyle-Width="17%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="Factura" FilterControlAltText="Filtrar columna NdeFactura" DataFormatString="{0:d}" HeaderText="<b>Nro. de Factura</b>"
							UniqueName="NdeFactura" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="FechaOcurrencia" FilterControlAltText="Filtrar columna FechadeOcurrencia" HeaderText="<b>Fecha de Ocurrencia</b>" DataFormatString="{0:d}"
							UniqueName="FechadeOcurrencia" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="Monto" FilterControlAltText="Filtrar columna Monto" HeaderText="<b>Monto</b>" UniqueName="Monto"
							ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="13%" ItemStyle-Width="13%" DataFormatString="{0:0,0.00}">
						</telerik:GridBoundColumn>
					</Columns>
					<EditFormSettings>
						<EditColumn FilterControlAltText="Filter EditCommandColumn column" />
					</EditFormSettings>
					<PagerStyle AlwaysVisible="True" />
					<CommandItemTemplate>
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td align="right">
									<telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicked="PanelBarItemClicked" OnButtonClick="RadToolBar1_ButtonClick1" meta:resourcekey="RadToolBar1Resource1">
										<Items>
											<telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" />
											<telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" />
											<telerik:RadToolBarButton runat="server" CommandName="PostBack" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" />
										</Items>
									</telerik:RadToolBar>
								</td>
							</tr>
						</table>
					</CommandItemTemplate>
				</MasterTableView>
			<PagerStyle AlwaysVisible="True" />
			<FilterMenu EnableImageSprites="False" />
			<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default" />
		</telerik:RadGrid>
		<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" DestroyOnClose="True" Title="Filtro" Width="600px" KeepInScreenBounds="True">
			<ContentTemplate>
				<fieldset>
					<legend><asp:Label ID="lblBusquedaAvanzada" runat="server" Text="Búsqueda Avanzada" Font-Bold="True" meta:resourcekey="lblBusquedaAvanzadaResource1" /></legend>
					<table>
						<tr>
							<td>
								<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False"
									CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " Culture="es-ES" />
								</telerik:RadFilter>
							</td>
						</tr>
						<tr>
							<td><asp:Label ID="LblMessege" runat="server" meta:resourcekey="LblMessegeResource1" /></td>
						</tr>
						<tr>
							<td><asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" meta:resourcekey="ApplyButtonResource1" /></td>
						</tr>
					</table>
				</fieldset>
			</ContentTemplate>
		</telerik:RadWindow>
		<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
			<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
		</telerik:RadCodeBlock>
	</asp:Panel>
	<br />
	<table width="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td align="right">
				<asp:Button ID="ButtonImprimir" runat="server" Text="Imprimir" OnClick="ButtonImprimir_Click" Enabled="false" />
			</td>
		</tr>
	</table>
</asp:Content>
