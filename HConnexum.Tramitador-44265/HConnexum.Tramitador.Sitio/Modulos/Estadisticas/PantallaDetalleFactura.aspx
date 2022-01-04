<%@ Page Title="Detalle Factura" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaDetalleFactura.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaDetalleFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
		table.ancho { font-weight: bold; }
		.PanelMargen { margin-top: 10px; }
		.ocultar { visibility: hidden; }
		.boton-accion { background-color: #d9e5f2; }
		.readOnly { background: #D7D7D7; }
		.number { text-align: right; }
		.arrange { float: left; }
		.html { overflow-x: hidden; }
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
	<asp:Panel ID="DetalledelaRelacion" runat="server" Width="100%" GroupingText="DETALLE DE LA FACTURA">
		<table width="90%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblNdefactura" runat="server" Text="Nro. de Factura" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblNumControl" runat="server" Text="Nro. de Control" />
				</td>
				
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblMontoCubierto" runat="server" Text="Monto Factura" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					&nbsp;
				</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblNfactura" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblNcontroltxt" runat="server" />
				</td>	
				
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblmontocubiertotxt" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					&nbsp;
				</td>
			</tr>
			<tr><td colspan="5">&nbsp;</td></tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblMontoSujetoRetencion" runat="server" Text="Monto Sujeto Retenci&oacute;n" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblTotal_ImpISRL" runat="server" Text="Monto ISRL" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblMontoImpMunicipal" runat="server" Text="Monto Imp Municipal" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblIva" runat="server" Text="IVA" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblTotalRetenido" runat="server" Text="Monto a Retener" />
				</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblmontosujetoretenciontxt" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					&nbsp;
					<asp:Label ID="lblTotal_ImpISRLtxt" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblmontoimpmunicipaltxt" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lbltotalIvatxt" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lbltotalretenidotxt" runat="server" />
				</td>
			</tr>
			<tr><td colspan="5">&nbsp;</td></tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblFechadeemision" runat="server" Text="Fecha Emisi&oacute;n de Factura" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					
					<asp:Label ID="lblFechaderecepcion" runat="server" Text="Fecha Recepci&oacute;n de Factura" />
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblEstatus" runat="server" Text="Estatus" />
				</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblfechaemisiontxt" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblFechaderecepciontxt" runat="server" />
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:Label ID="lblEstatustxt" runat="server" />
				</td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<asp:Panel ID="ResumenCasos" runat="server" Width="100%" GroupingText="RESUMEN DE RECLAMOS">
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
					<telerik:GridBoundColumn DataField="Reclamo" FilterControlAltText="Filtrar columna NombredelPaciente" DataFormatString="{0:d}" HeaderText="<b>N° Reclamo</b>" UniqueName="Reclamo"
											 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-Width="11%">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="NReclamosEncriptado" Visible="false" />
					<telerik:GridBoundColumn DataField="DocAsegurado" FilterControlAltText="Filtrar columna Cedula" HeaderText="<b>Documento<br> de Identidad</b>" UniqueName="Cedula"
											 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="13%" ItemStyle-Width="13%">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filtrar columna Status" DataFormatString="{0:d}" HeaderText="<b>Estatus</b>" UniqueName="Status"
											 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-Width="11%">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="Monto" FilterControlAltText="Filtrar columna Monto" HeaderText="<b>Monto Reclamo</b>" UniqueName="Monto"
											 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-Width="11%" DataFormatString="{0:0,0.00}">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="NumOrden" FilterControlAltText="Filtrar columna NumOrden" HeaderText="<b>N° Orden de Pago</b>" UniqueName="NumOrden"
											 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-Width="11%">
					</telerik:GridBoundColumn> 
					<telerik:GridBoundColumn DataField="FechaPago" FilterControlAltText="Filtrar columna Fecha_Pago" HeaderText="<b>Fecha de Pago</b>" DataFormatString="{0:d}"
											 UniqueName="Fecha_Pago" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-Width="11%">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="MontoPagoSap" FilterControlAltText="Filtrar columna Monto SAP" HeaderText="<b>Monto Pagado</b>" UniqueName="MontoPagoSap"
											 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-Width="11%" DataFormatString="{0:0,0.00}">
					</telerik:GridBoundColumn> 
					<telerik:GridBoundColumn DataField="NumReferencia" FilterControlAltText="Filtrar columna Número Referencia" HeaderText="<b>N° Referencia</b>" UniqueName="NumReferencia"
											 ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="11%" ItemStyle-Width="11%">
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
										<telerik:RadToolBarButton runat="server" CommandName="Imprimir" ImagePosition="Right" ImageUrl="~/Imagenes/print.png" Text="Imprimir"/>
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
					<legend>
						<asp:Label ID="lblBusquedaAvanzada" runat="server" Text="Búsqueda Avanzada" Font-Bold="True" meta:resourcekey="lblBusquedaAvanzadaResource1" />
					</legend>
					<table>
						<tr>
							<td>
								<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False"
												   CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " Culture="es-ES" />
								</telerik:RadFilter>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="LblMessege" runat="server" meta:resourcekey="LblMessegeResource1" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" meta:resourcekey="ApplyButtonResource1" />
							</td>
						</tr>
					</table>
				</fieldset>
			</ContentTemplate>
		</telerik:RadWindow>
		<telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
			<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
		</telerik:RadCodeBlock>
	</asp:Panel>	
</asp:Content>