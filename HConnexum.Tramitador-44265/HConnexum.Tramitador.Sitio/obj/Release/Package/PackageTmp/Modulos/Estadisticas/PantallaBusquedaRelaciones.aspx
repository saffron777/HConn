<%@ Page Title="Busqueda de Relaciones" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaBusquedaRelaciones.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaBusquedaRelaciones" %>

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
		.Notificacion { text-align: justify; font-weight: bold; }
	</style>
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
		</AjaxSettings>
		<ClientEvents OnRequestStart="RequestStart" OnResponseEnd="ResponseEnd" />
	</telerik:RadAjaxManager>
	<asp:Panel ID="Panel1" runat="server" GroupingText="BÚSQUEDA DE RELACIONES">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblProveedor" Text="Proveedor:" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><telerik:RadComboBox ID="ddlProveedor" DataValueField="Id" DataTextField="Nombre" Filter="Contains" EmptyMessage="Seleccione" runat="server" Width="100%" AutoPostBack="true" Culture="es-ES" Visible="true" CausesValidation="false" OnSelectedIndexChanged="DdlProveedorSelectedIndexChanged" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:RequiredFieldValidator ID="RFVddlProveedor" runat="server" ErrorMessage="*" ControlToValidate="ddlProveedor" CssClass="validator" Width="20px" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label1" Text="Tipo Proveedor:" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><telerik:RadComboBox ID="ddlTipoProveedor" DataValueField="Id" DataTextField="Nombre" Filter="Contains" EmptyMessage="Seleccione" runat="server" Width="100%" AutoPostBack="true" Culture="es-ES" Visible="true" CausesValidation="false" Enabled="false" OnSelectedIndexChanged="DdlTipoProveedorSelectedIndexChanged" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:RequiredFieldValidator ID="RFVddlTipoProveedor" runat="server" ErrorMessage="*" ControlToValidate="ddlTipoProveedor" CssClass="validator" Width="20px" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label2" Text="Compañia:" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><telerik:RadComboBox ID="ddlIntermediarios" DataValueField="Id" DataTextField="Nombre" Filter="Contains" EmptyMessage="Seleccione" runat="server" AutoPostBack="true" Width="100%" Culture="es-ES" Visible="true" CausesValidation="false" Enabled="false" OnSelectedIndexChanged="DdlIntermediariosSelectedIndexChanged" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:RequiredFieldValidator ID="RFVddlIntermediarios" runat="server" ErrorMessage="*" ControlToValidate="ddlIntermediarios" CssClass="validator" Width="20px" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<asp:Panel GroupingText="B&Uacute;SQUEDA POR ESTATUS" runat="server" ID="PanelEstatus">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<asp:RadioButtonList runat="server" ID="RadioButtonGroup" onclick="OcultarValidator(this);">
						<asp:ListItem Text="<b>Pendientes:</b> Relaci&oacute;n de pagos facturas liquidadas en proceso de env&iacute;o para el pago" Value="Pendiente" OnClick="DeshabilitarRadTextBoxEnviadosyPendientes(false)" />
						<asp:ListItem Text="<b>Enviadas:</b> Relaciones de pago facturadas y liquidadas con remesa de pago asignada" Value="Enviada" OnClick="DeshabilitarRadTextBoxEnviadosyPendientes(false)" />
					</asp:RadioButtonList>
				</td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<asp:Panel ID="Panel2" GroupingText="B&Uacute;SQUEDA POR RANGO DE FECHA" runat="server">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="4">
					<asp:RadioButtonList runat="server" ID="RadioButtonList1" onclick="OcultarValidator(this);">
						<asp:ListItem Text="<b>Pagadas</b> (Relaciones de pago facturadas y liquidadas, con pago confirmado) El rango de fechas solo debe contener per&iacute;odos menores o iguales a un (1) a&ntilde;o, de acuerdo con la fecha de pago." Value="Pagada" OnClick="DeshabilitarRadTextBox(true)" />
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Inicial:"></asp:Label></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaFinal" runat="server" Text="Fecha Final:"></asp:Label></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<telerik:RadDatePicker ID="txtFechaInicial" runat="server" Width="100%" DateInput-CausesValidation="false" Enabled="false" DateInput-ClientEvents-OnValueChanged="HabilitarFechaFinal">
						<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda" />
					</telerik:RadDatePicker>
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<telerik:RadDatePicker ID="txtFechaFinal" runat="server" Width="100%" Enabled="false" DateInput-CausesValidation="false">
						<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda" />
					</telerik:RadDatePicker>
				</td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="2">
					<asp:CompareValidator ID="dateCompareValidator" runat="server" Display="Dynamic" ControlToValidate="txtFechaFinal" ControlToCompare="txtFechaInicial" Operator="GreaterThanEqual" ErrorMessage="*&nbsp; &nbsp; &nbsp;El campo <b>Fecha Inicial</b> no puede ser menor al campo <b>Fecha Final</b>" CssClass="validator" />
				</td>
			</tr>
			<tr>

				<td class="fieldCell4colP" style="vertical-align: top;" >
					<asp:RequiredFieldValidator ID="RFVtxtFechaInicial" runat="server" ErrorMessage="*&nbsp; &nbsp; &nbsp;El Campo <b>Fecha Inicial</b> es de caracter obligatorio" ControlToValidate="txtFechaInicial" CssClass="validator" />
				</td>
                <td class="fieldCell4colP" style="vertical-align: top;" >
					<asp:RequiredFieldValidator ID="RFVtxtFechaFinal" runat="server" ErrorMessage="*&nbsp; &nbsp; &nbsp;El Campo <b>Fecha Final</b> es de caracter obligatorio" ControlToValidate="txtFechaFinal" CssClass="validator" />
				</td>
			</tr>           
		</table>
	</asp:Panel>
	<br />
	<asp:Panel ID="Panel3" GroupingText="B&Uacute;SQUEDA POR N&Uacute;MERO DE RELACI&Oacute;N" runat="server">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label3" Text="Número de relaci&oacute;n:" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><telerik:RadTextBox ID="BusquedaXNumeroRelacion" MaxLength="10" Enabled="true" ClientEvents-OnKeyPress="DeshabilitarEstatus" runat="server" Width="100%"></telerik:RadTextBox></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label10" Text="Número de factura:" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">
					<telerik:RadTextBox ID="BusquedaXNumeroFactura" MaxLength="20" Enabled="true" ClientEvents-OnKeyPress="DeshabilitarEstatusRelacion" runat="server" Width="100%"></telerik:RadTextBox>
				</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<table width="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td align="right"><asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" OnClientClick="if(ValidarDatePicker())" OnClick="ButtonBuscar_Click"></asp:Button></td>
		</tr>
	</table>
	<br />
	<asp:Panel runat="server" Visible="false" ID="PanelGrid">
		<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True" OnNeedDataSource="RadGridMaster_NeedDataSource" Width="100%"
						 AllowSorting="True" OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnPageIndexChanged="RadGridMaster_PageIndexChanged" OnSortCommand="RadGridMaster_SortCommand" OnItemCommand="RadGridMaster_ItemCommand">
			<ClientSettings EnableRowHoverStyle="true">
				<ClientEvents OnRowDblClick="RowDblClick" />
				<Selecting AllowRowSelect="True" />
				<Scrolling AllowScroll="True" UseStaticHeaders="True" />
			</ClientSettings>
			<MasterTableView CommandItemDisplay="Top" Width="100%" NoMasterRecordsText="No se encontraron registros" DataKeyNames="RelacionReclamo,NRemesaEncriptado" ClientDataKeyNames="RelacionReclamo,NRemesaEncriptado">
				<CommandItemSettings ExportToPdfText="Export to PDF" />
				<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" HeaderStyle-Width="20px" />
				<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" HeaderStyle-Width="20px" />
				<Columns>
					<telerik:GridBoundColumn DataField="NRemesaEncriptado" Visible="false" />
					<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataField="RelacionReclamo" FilterControlAltText="Filtrar columna Relacion" HeaderText="<b>Relaci&oacute;n</b>" UniqueName="Relacion" HeaderStyle-Width="15%" ItemStyle-Width="15%" />
					<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataField="NRemesa" FilterControlAltText="Filtrar columna RemesaAsociada" HeaderText="<b>Remesa Asociada</b>" UniqueName="RemesaAsociada" HeaderStyle-Width="15%" ItemStyle-Width="15%" />
					<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataField="FechaCreacion" FilterControlAltText="Filtrar columna FechaCreacion" DataFormatString="{0:d}" HeaderText="<b>Fecha de Creaci&oacute;n</b>" UniqueName="FechaCreacion" HeaderStyle-Width="15%" ItemStyle-Width="15%" />
					<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataField="Estatus" FilterControlAltText="Filtrar columna Estatus" HeaderText="<b>Estatus</b>" UniqueName="Estatus" HeaderStyle-Width="15%" ItemStyle-Width="15%" />
					<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataField="FechaPago" FilterControlAltText="Filtrar columna FechaPago" DataFormatString="{0:d}" HeaderText="<b>Fecha de Pago</b>" UniqueName="FechaPago" HeaderStyle-Width="25%" ItemStyle-Width="25%" />
					<telerik:GridBoundColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" DataField="MontoPagar" FilterControlAltText="Filtrar columna MontoRelacion" DataFormatString="{0:0,0.00}" HeaderText="<b>Monto de la Relaci&oacute;n</b>" UniqueName="RelacionRemesaAsociada" HeaderStyle-Width="15%" ItemStyle-Width="15%" />
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
					<legend>
						<asp:Label ID="lblBusquedaAvanzada" runat="server" Text="Búsqueda Avanzada" Font-Bold="True" meta:resourcekey="lblBusquedaAvanzadaResource1"></asp:Label>
					</legend>
					<table>
						<tr>
							<td><telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7" Culture="es-ES"></telerik:RadFilter></td>
						</tr>
						<tr>
							<td><asp:Label ID="LblMessege" runat="server" meta:resourcekey="LblMessegeResource1"></asp:Label></td>
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
	<asp:Panel runat="server" GroupingText="Totales" ID="PanelTotales" Visible="false">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label4" Text="Relaciones" runat="server"></asp:Label></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label5" Text="Casos" runat="server"></asp:Label></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label6" Text="Monto Cubierto" runat="server"></asp:Label></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label7" Text="Retenido" runat="server"></asp:Label></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label8" Text="Imp. Municipal" runat="server"></asp:Label></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label9" Text="Monto Total" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="relaciones" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="casos" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montocubierto" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="retenido" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="impmunicipal" runat="server"></asp:Label></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montototal" runat="server"></asp:Label></td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<table width="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td align="right"><asp:Button ID="ButtonImprimir" runat="server" Text="Imprimir" OnClick="ButtonImprimir_Click" Visible="false"></asp:Button></td>
		</tr>
	</table>
	<telerik:RadWindowManager ID="Singleton" runat="server" VisibleStatusbar="false" Localization-OK="Aceptar" EnableShadow="True" />
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
			var IdInterEncriptado = "<%= this.IdInterEncriptado %>";
			var IdProveeEncriptado = "<%= this.IdProveeEncriptado %>";
			var IdCodExternoEncriptado = "<%= this.IdCodExternoEncriptado %>";
			var idMenu = '<%= this.IdMenuEncriptado %>';
			var validator = document.getElementById("<%=this.RFVtxtFechaInicial.ClientID%>");
		    var validator2 = document.getElementById("<%=this.RFVtxtFechaFinal.ClientID%>");

			window.onload = function () {
				changeTextRadAlert();
				ValidatorEnable(validator, false);
				ValidatorEnable(validator2, false);
			};

			function OcultarValidator(RadioGroup) {
				if (RadioGroup.rows.length > 1) {
					if (!RadioGroup.rows[1].cells[0].firstChild.checked) {
					    ValidatorEnable(validator, false);
					    ValidatorEnable(validator2, false);
					}
				}
			};

			function DeshabilitarEstatus(sender, args) {
				var obj = document.getElementById("<%=this.RadioButtonGroup.ClientID%>");
				var rblChild = null;
				var rblChild2 = null;
				for (i = 0; i < obj.lastChild.children.length; i++) {
					rblChild = document.getElementById("<%=this.RadioButtonGroup.ClientID%>" + "_" + i.toString());
					rblChild.checked = false;
					rblChild2 = document.getElementById("<%=this.RadioButtonList1.ClientID%>" + "_0");
					rblChild2.checked = false;
				}
				$find("<%=this.txtFechaInicial.ClientID%>").set_enabled(false);
				$find("<%=this.txtFechaInicial.ClientID%>").clear();
				$find("<%=this.txtFechaFinal.ClientID%>").set_enabled(false);
				$find("<%=this.txtFechaFinal.ClientID%>").clear();
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_value('');
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").clear();
			    $find("<%=this.BusquedaXNumeroRelacion.ClientID%>").set_enabled(true);

			    
			    ValidatorEnable(validator, false);
			    ValidatorEnable(validator2, false);
			};

			function DeshabilitarEstatusRelacion(sender, args) {
				var obj = document.getElementById("<%=this.RadioButtonGroup.ClientID%>");
				var rblChild = null;
				var rblChild2 = null;
				for (i = 0; i < obj.lastChild.children.length; i++) {
					rblChild = document.getElementById("<%=this.RadioButtonGroup.ClientID%>" + "_" + i.toString());
					rblChild.checked = false;
					rblChild2 = document.getElementById("<%=this.RadioButtonList1.ClientID%>" + "_0");
					rblChild2.checked = false;
				}
				$find("<%=this.txtFechaInicial.ClientID%>").set_enabled(false);
				$find("<%=this.txtFechaInicial.ClientID%>").clear();
				$find("<%=this.txtFechaFinal.ClientID%>").set_enabled(false);
				$find("<%=this.txtFechaFinal.ClientID%>").clear();
				$find("<%=this.BusquedaXNumeroRelacion.ClientID%>").set_value('');
			    $find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_enabled(true);

			    
			    ValidatorEnable(validator, false);
			    ValidatorEnable(validator2, false);
			};

			function HabilitarFechaFinal(sender, args) {
				$find("<%=this.txtFechaFinal.ClientID%>").set_enabled(args._newValue != "");
				if (args._newValue == "") {
					$find("<%=this.txtFechaFinal.ClientID%>").clear();
				}
			    ValidatorEnable(validator, false);
			    ValidatorEnable(validator2, sender._enabled);			    
			};

			function DeshabilitarRadTextBox(args) {
				$find("<%=this.BusquedaXNumeroRelacion.ClientID%>").set_value('');
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_value('');
				if (args) {
					$find("<%=this.txtFechaInicial.ClientID%>").set_enabled(args);
					var obj = document.getElementById("<%=this.RadioButtonGroup.ClientID%>");
					var rblChild = null;
					for (i = 0; i < obj.lastChild.children.length; i++) {
						rblChild = document.getElementById("<%=this.RadioButtonGroup.ClientID%>" + "_" + i.toString());
						rblChild.checked = false;
					}
				}
				else {
					$find("<%=this.txtFechaInicial.ClientID%>").set_enabled(args);
					$find("<%=this.txtFechaInicial.ClientID%>").clear();
					$find("<%=this.txtFechaFinal.ClientID%>").set_enabled(args);
				    $find("<%=this.txtFechaFinal.ClientID%>").clear();

				    
				}
			};

			function DeshabilitarRadTextBoxEnviadosyPendientes(args) {
				$find("<%=this.BusquedaXNumeroRelacion.ClientID%>").set_value('');
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_value('');
				if (args) {
					$find("<%=this.txtFechaInicial.ClientID%>").set_enabled(args);
				}
				else {
					var obj = document.getElementById("<%=this.RadioButtonList1.ClientID%>");
					var rblChild = null;
					for (i = 0; i < obj.lastChild.children.length; i++) {
						rblChild = document.getElementById("<%=this.RadioButtonList1.ClientID%>" + "_" + i.toString());
						rblChild.checked = false;
						ValidatorEnable(validator, false);
						ValidatorEnable(validator2, false);
					}
					$find("<%=this.txtFechaInicial.ClientID%>").set_enabled(args);
					$find("<%=this.txtFechaInicial.ClientID%>").clear();
					$find("<%=this.txtFechaFinal.ClientID%>").set_enabled(args);
				    $find("<%=this.txtFechaFinal.ClientID%>").clear();

				    
				}
			};

			function DeshabilitarRadTextBoxRelacion(args) {
				$find("<%=this.BusquedaXNumeroRelacion.ClientID%>").set_value('');
				$find("<%=this.BusquedaXNumeroRelacion.ClientID%>").set_enabled(args);
			};

			function DeshabilitarRadTextBoxFactura() {
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_value('');
				$find("<%=this.BusquedaXNumeroFactura.ClientID%>").set_enabled(args);
			};

			function ValidarDatePicker() {
				var obj = document.getElementById("<%=this.RadioButtonList1.ClientID%>");
				var fechainicial = document.getElementById("<%=this.txtFechaInicial.ClientID%>");
				var rblChild = null;
				for (i = 0; i < obj.lastChild.children.length; i++) {
					rblChild = document.getElementById("<%=this.RadioButtonList1.ClientID%>" + "_" + i.toString());
					if ((rblChild.checked) && (rblChild.value == "Pagada") && (fechainicial.value == "")) {
					    ValidatorEnable(validator, true);
					    //ValidatorEnable(validator2, true);
						return false;
					}
				}
				return true;
			};

			function RowDblClick(sender, args) {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Estadisticas/PantallaDetalleRelacion.aspx?IdMenu=" + idMenu + "&intermediario=" + IdInterEncriptado.toString() + "&nremesa=" + args.getDataKeyValue("NRemesaEncriptado") + "&idcodexterusuact=" + IdProveeEncriptado.toString() + "&idcodexterno=" + IdCodExternoEncriptado.toString());
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
				var rfvddlTipoProveedor = document.getElementById("<%=this.RFVddlTipoProveedor.ClientID%>");
				var rfvddlIntermediarios = document.getElementById("<%=this.RFVddlIntermediarios.ClientID%>");
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
	
</asp:Content>

