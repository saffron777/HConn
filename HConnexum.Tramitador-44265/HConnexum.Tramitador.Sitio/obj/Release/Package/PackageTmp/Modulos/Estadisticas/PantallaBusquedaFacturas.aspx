<%@ Page Title="Búsqueda de Facturas" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaBusquedaFacturas.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaBusquedaFacturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server" meta:resourcekey="RadAjaxManager1Resource1" UpdatePanelsRenderMode="Inline">
		<AjaxSettings>
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
			<telerik:AjaxSetting AjaxControlID="ddlTipoProveedor">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="ddlIntermediarios" />
					<telerik:AjaxUpdatedControl ControlID="ddlTipoProveedor" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ddlIntermediarios">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="ddlIntermediarios" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ButtonBuscar">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
		<ClientEvents OnRequestStart="RequestStart" OnResponseEnd="ResponseEnd" />
	</telerik:RadAjaxManager>
	<asp:Panel ID="PanelMaster" runat="server">
		<asp:Panel ID="Panel1" runat="server" GroupingText="FILTRO PROVEEDOR">
			<table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 5px;">
				<tr>
					<td id="tdlblProveedor" runat="server" visible="false" class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblProveedor"  Text="Proveedor:" runat="server"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblTipoProveedor" Text="Tipo Proveedor:" runat="server"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblCompania" Text="Compañia:" runat="server"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
					<td id="tdlblVacio" runat="server" visible="false" class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				</tr>
				<tr>
					<td class="fieldCell4colP" style="vertical-align: top;" id="tdProveedor" runat="server" visible="false">
						<telerik:RadComboBox ID="ddlProveedor" DataValueField="Id" DataTextField="Nombre" Filter="Contains" EmptyMessage="Seleccione" runat="server" Width="85%" AutoPostBack="true" Culture="es-ES" Visible="true" CausesValidation="false" OnSelectedIndexChanged="DdlProveedorSelectedIndexChanged" />
						<asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ErrorMessage="*&nbsp;&nbsp;&nbsp;&nbsp;" ControlToValidate="ddlProveedor" CssClass="validator" Width="25px" />
					</td>
					<td class="fieldCell4colP" style="vertical-align: top;">
						<telerik:RadComboBox ID="ddlTipoProveedor" DataValueField="Id" DataTextField="Nombre" Filter="Contains" EmptyMessage="Seleccione" runat="server" Width="85%" AutoPostBack="true" Culture="es-ES" Visible="true" CausesValidation="false" Enabled="false" OnSelectedIndexChanged="DdlTipoProveedorSelectedIndexChanged" />
						<asp:RequiredFieldValidator ID="rfvTipoProveedor" runat="server" ErrorMessage="*&nbsp;&nbsp;&nbsp;&nbsp;" ControlToValidate="ddlTipoProveedor" CssClass="validator" Width="25px" />
					</td>
					<td class="fieldCell4colP" style="vertical-align: top;">
						<telerik:RadComboBox ID="ddlIntermediarios" DataValueField="Id" DataTextField="Nombre" Filter="Contains" EmptyMessage="Seleccione" runat="server" Width="85%" AutoPostBack="true" Culture="es-ES" Visible="true" CausesValidation="false" Enabled="false" OnSelectedIndexChanged="DdlIntermediariosSelectedIndexChanged" />
						<asp:RequiredFieldValidator ID="rfvIntermediarios" runat="server" ErrorMessage="*&nbsp;&nbsp;&nbsp;&nbsp;" ControlToValidate="ddlIntermediarios" CssClass="validator" Width="25px" />
					</td>
					<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
					<td id="tdtxtVacio" runat="server" visible="false" class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				</tr>
			</table>
		</asp:Panel>
		<br />
		<asp:Panel ID="PanelEstatus" GroupingText="FILTRO ESTATUS / FECHAS" runat="server">
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td class="fieldCell4colP" style="vertical-align: top;" colspan="4">
						<asp:RadioButtonList runat="server" ID="RadioButtonList1" >
							<asp:ListItem Text="<b>Pagada:</b>&nbsp;(Facturas con Estatus Pagadas)." Value="Pagada" Selected="True" />
							<asp:ListItem Text="<b>Pendiente:</b>&nbsp;(Facturas Pendientes para el Pago)." Value="Pendiente" />
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<td class="labelCell4colP" style="vertical-align: top;" colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaInicial" runat="server" Text="Fecha de Ocurrencia Inicial:"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaFinal" runat="server" Text="Fecha de Ocurrencia Final:"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
					<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				</tr>
				<tr>
					<td class="fieldCell4colP" style="vertical-align: top;">
						<telerik:RadDatePicker ID="txtFechaInicial" runat="server" DateInput-EmptyMessage="DD/MM/YYYY" Width="85%" >
							<Calendar ID="Calendar1" runat="server">
								<SpecialDays>
									<telerik:RadCalendarDay Repeatable="Today">
										<ItemStyle Font-Bold="true" BorderColor="Red" />
									</telerik:RadCalendarDay>
								</SpecialDays>
							</Calendar>
						</telerik:RadDatePicker>
						<asp:RequiredFieldValidator ID="rfvFechaInicial" runat="server" ErrorMessage="*" Text="*" ControlToValidate="txtFechaInicial" CssClass="validator" Width="20px" />
					</td>
					<td class="fieldCell4colP" style="vertical-align: top;">
						<telerik:RadDatePicker ID="txtFechaFinal" runat="server" DateInput-EmptyMessage="DD/MM/YYYY" Width="85%" >
							<Calendar ID="Calendar2" runat="server">
								<SpecialDays>
									<telerik:RadCalendarDay Repeatable="Today">
										<ItemStyle Font-Bold="true" BorderColor="Red" />
									</telerik:RadCalendarDay>
								</SpecialDays>
							</Calendar>
						</telerik:RadDatePicker>
						<asp:RequiredFieldValidator ID="rfvFechaFinal" runat="server" ErrorMessage="*" ControlToValidate="txtFechaFinal" CssClass="validator" Width="20px" />
					</td>
					<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
					<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
				</tr>
				<tr>
					<td class="fieldCell4colP" style="vertical-align: top;" colspan="4">
						<asp:CompareValidator ID="dateCompareValidator" runat="server" Display="Dynamic"
											  ControlToValidate="txtFechaFinal" ControlToCompare="txtFechaInicial" Operator="GreaterThanEqual"
											  ErrorMessage="*&nbsp; &nbsp; &nbsp;El campo <b>Fecha Inicial</b> no puede ser menor al campo <b>Fecha Final</b>"
											  CssClass="validator" />
					</td>
				</tr>
				<tr>
					<td class="fieldCell4col" colspan="4" style="height: 40px; margin-top:45px;">
						<asp:Label ID="lblNota" runat="server" Text="<b>Nota</b>:&nbsp;El rango de fechas solo debe contener períodos menores o iguales a un (1) año, de acuerdo con la <b>Fecha de Ocurrencia</b>" />
					</td>
				</tr>
			</table>
		</asp:Panel>
		<br />
		<asp:Panel ID="Panel3" GroupingText="FILTRO FACTURA" runat="server">
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblnfactura" Text="Número de factura:" runat="server"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblnreclamo" Text="Número de Reclamo:" runat="server"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblcedula" Text="Documento de Identidad del Titular:" runat="server"></asp:Label></td>
					<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				</tr>
				<tr>
					<td class="fieldCell4colP" style="vertical-align: top;"><telerik:RadTextBox ID="BusquedaXNumeroFactura" MaxLength="20" Enabled="true" runat="server" Width="85%" /></td>
					<td class="fieldCell4colP" style="vertical-align: top;"><telerik:RadTextBox ID="BusquedaXNumeroReclamo" MaxLength="10" Enabled="true" runat="server" Width="85%" /></td>
					<td class="fieldCell4colP" style="vertical-align: top;"><telerik:RadTextBox ID="BusquedaXCedula" MaxLength="9" Enabled="true" runat="server" Width="85%" /></td>
					<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
				</tr>
			</table>
		</asp:Panel>
		<br />
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td align="right">
					<asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" OnClientClick="if(ValidarDatePicker())" OnClick="ButtonBuscar_Click" />
					<asp:Button ID="ButtonLimpiar" runat="server" Text="Limpiar" OnClick="ButtonLimpiar_Click" />
				</td>
			</tr>
		</table>
		<br />
		<asp:Panel runat="server" Visible="false" ID="PanelGrid" GroupingText="RESULTADO DE BÚSQUEDA">
			<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" CellSpacing="0"
							 GridLines="None" AllowCustomPaging="True" AllowPaging="True" OnNeedDataSource="RadGridMaster_NeedDataSource"
							 Width="100%" AllowSorting="True" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
							 OnPageIndexChanged="RadGridMaster_PageIndexChanged" OnSortCommand="RadGridMaster_SortCommand"
							 OnItemCommand="RadGridMaster_ItemCommand">
				<ClientSettings EnableRowHoverStyle="true">
					<ClientEvents OnRowDblClick="RowDblClick" />
					<Selecting AllowRowSelect="True" />
					<Scrolling AllowScroll="True" UseStaticHeaders="True" />
			   </ClientSettings>
				<MasterTableView CommandItemDisplay="Top" Width="100%" NoMasterRecordsText="No se encontraron registros" DataKeyNames="Nfactura,NFacturaEncriptado" ClientDataKeyNames="Nfactura,NFacturaEncriptado">
					<CommandItemSettings ExportToPdfText="Export to PDF" />
					<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
						<HeaderStyle Width="20px" />
					</RowIndicatorColumn>
					<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
						<HeaderStyle Width="20px" />
					</ExpandCollapseColumn>
					<Columns>
						<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
												 DataField="NFactura" FilterControlAltText="Filtrar columna NFactura" HeaderText="N° de Factura"
												 UniqueName="NFactura" HeaderStyle-Width="22%" ItemStyle-Width="22%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
												 DataField="Estatus" FilterControlAltText="Filtrar columna Estatus" HeaderText="Estatus"
												 UniqueName="Estatus" HeaderStyle-Width="18%" ItemStyle-Width="18%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
												 DataField="FechaCreacion" FilterControlAltText="Filtrar columna FechaCreacion"
												 DataFormatString="{0:d}" HeaderText="Fecha de Recepción de Factura" UniqueName="FechaCreacion"
												 HeaderStyle-Width="20%" ItemStyle-Width="20%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
												 DataField="FechaPago" FilterControlAltText="Filtrar columna FechaPago" DataFormatString="{0:d}"
												 HeaderText="Fecha de Pago" UniqueName="FechaPago" HeaderStyle-Width="15%"
												 ItemStyle-Width="15%">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center"
												 DataField="MontoPagar" FilterControlAltText="Filtrar columna MontoPagar" DataFormatString="{0:0,0.00}"
												 HeaderText="Monto de Factura" UniqueName="MontoPagar" HeaderStyle-Width="15%"
												 ItemStyle-Width="15%">
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
											<telerik:RadToolBarButton runat="server" CommandName="Imprimir" Enabled="true" ImagePosition="Right" ImageUrl="~/Imagenes/print.png" Text="Imprimir"/>
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
									<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7" Culture="es-ES">
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
	</asp:Panel>
	<br />
	<br />
	<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
		<script type="text/javascript">
			var nombreVentana = '<%=this.RadWindow1.ClientID %>';
			var nombreGrid = '<%=this.RadGridMaster.ClientID%>';
			var nombreRadAjaxManager = '<%= this.RadAjaxManager1.ClientID %>';
			var nombreRadFilter = '<%=this.RadFilterMaster.ClientID %>';
			var idMaster = "<%= this.idMaster %>";
			var AccionAgregar = "<%= this.AccionAgregar %>";
			var AccionModificar = "<%= this.AccionModificar %>";
			var AccionVer = "<%= this.AccionVer %>";
			var idMenu = '<%= this.IdMenuEncriptado %>';
			var validator = document.getElementById("<%=this.rfvFechaInicial.ClientID%>");

			window.onload = function () {
				changeTextRadAlert();
			};

			function HabilitarFechaFinal(sender, args) {
				$find("<%=this.txtFechaFinal.ClientID%>").set_enabled(args._newValue != "");
				if (args._newValue == "") {
					$find("<%=this.txtFechaFinal.ClientID%>").clear();
				}
				ValidatorEnable(validator, false);
			};
			
			function DeshabilitarRadTextBoxRelacion(args) {
				$find("<%=this.BusquedaXNumeroReclamo.ClientID%>").set_value('');
				$find("<%=this.BusquedaXNumeroReclamo.ClientID%>").set_enabled(args);
			};

			function DeshabilitarRadTextBoxFactura() {
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_value('');
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_enabled(args);
			};

			function DeshabilitarRadTextBoxCedula() {
				$find("<%=this.BusquedaXCedula.ClientID%>").set_value('');
				$find("<%=this.BusquedaXCedula.ClientID%>").set_enabled(args);
			};

			function ValidarDatePicker() {
				var obj = document.getElementById("<%=this.RadioButtonList1.ClientID%>");
				var fechainicial = document.getElementById("<%=this.txtFechaInicial.ClientID%>");
				var rblChild = null;
				for (i = 0; i < obj.lastChild.children.length; i++) {
					rblChild = document.getElementById("<%=this.RadioButtonList1.ClientID%>" + "_" + i.toString());
					if ((rblChild.checked) && (rblChild.value == "Pagada") && (rblChild.value == "Pendiente") && (fechainicial.value == "")) {
						ValidatorEnable(validator, true);
						return false;
					}
				}
				return true;
			};

			function RowDblClick(sender, args) {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Estadisticas/PantallaDetallefactura.aspx?IdMenu=" + idMenu + "&NFactura=" + args.getDataKeyValue("NFacturaEncriptado"));
			};

			var currentLoadingPanel = null;
			var currentUpdatedControl = null;
			
			function RequestStart(sender, args) {
				if (currentUpdatedControl == null) {
					currentLoadingPanel = $find('<%= this.Master.FindControl(@"RadAjaxLoadingPanel1").ClientID %>');
					if (args.get_eventTarget() == "<%= this.ddlTipoProveedor.UniqueID %>") {
						currentUpdatedControl = "<%= this.ddlIntermediarios.ClientID %>";
					}
					currentLoadingPanel.show(currentUpdatedControl);
				}
			};
			
			function ResponseEnd(sender, args) {
				var rfvddlTipoProveedor = document.getElementById("<%=this.rfvTipoProveedor.ClientID%>");
				var rfvddlIntermediarios = document.getElementById("<%=this.rfvIntermediarios.ClientID%>");
				var tipoProveedor = document.getElementById("<%=this.ddlTipoProveedor.ClientID%>");
				var intermediarios = document.getElementById("<%=this.ddlIntermediarios.ClientID%>");
				if (currentLoadingPanel != null) {
					currentLoadingPanel.hide(currentUpdatedControl);
				}
				currentUpdatedControl = null;
				currentLoadingPanel = null;
				if ((!rfvddlTipoProveedor.isvalid) && (args.get_eventTarget() == "<%= this.ddlTipoProveedor.UniqueID %>") && (tipoProveedor.value != "")) {
					ValidatorEnable(rfvddlTipoProveedor, false);
				}
				else if ((!rfvddlIntermediarios.isvalid) && (intermediarios.value != "")) {
					ValidatorEnable(rfvddlIntermediarios, false);
				}
			};
		</script>
	</telerik:RadCodeBlock>
	<telerik:RadWindowManager ID="Singleton" runat="server" EnableShadow="true" Localization-OK="Aceptar" VisibleStatusbar="false" />
</asp:Content>

