<%@ Page Language="C#" AutoEventWireup="True" Title="Litado de Movimiento" CodeBehind="MovimientoCasoLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.MovimientoCasoLista" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
	<link href="../../Temas/Site.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		div.RadGrid .rgPager .rgAdvPart
		{
			display: none;
		}
	</style>
</head>
<body style="margin-top: 10px;">
	<form id="form1" runat="server">
	<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
	<telerik:RadScriptManager ID="RadScriptManager" runat="server" />
	<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
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
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%" Height="336px" CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True"
		AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged" OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
		OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand" Culture="es-ES">
		<ClientSettings EnableRowHoverStyle="true">
			<Selecting AllowRowSelect="True" />
			<ClientEvents OnRowDblClick="_PanelBarItemClicked" />
			<Scrolling AllowScroll="True" UseStaticHeaders="false" />
		</ClientSettings>
		<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado, TipoP, Estatus, UsuarioAsignado"
			ClientDataKeyNames="IdEncriptado, TipoP, Estatus, UsuarioAsignado" Width="100%">
			<CommandItemSettings ExportToPdfText="Export to PDF" />
			<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
				<HeaderStyle Width="20px" />
			</RowIndicatorColumn>
			<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
				<HeaderStyle Width="20px" />
			</ExpandCollapseColumn>
			<Columns>
				<telerik:GridBoundColumn DataField="FechaCreacion" FilterControlAltText="Filtrar columna FechaCreacion" HeaderText="Fecha" ItemStyle-Width="20px" UniqueName="FechaCreacion">
					<HeaderStyle Width="20%" />
					<ItemStyle Width="20%" />
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="NombrePaso" FilterControlAltText="Filtrar columna NombrePaso" DataFormatString="{0:N0}" ItemStyle-Width="20px" HeaderText="Nombre movimiento" UniqueName="NombrePaso">
					<HeaderStyle Width="32%" />
					<ItemStyle Width="32%" />
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="TipoP" ItemStyle-Width="20px" FilterControlAltText="Filtrar columna TipoP" DataFormatString="{0:N0}" HeaderText="Tipo movimiento" UniqueName="TipoP">
					<HeaderStyle Width="15%" />
					<ItemStyle Width="16%" />
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="Estatus" ItemStyle-Width="20px" FilterControlAltText="Filtrar columna Estatus" DataFormatString="{0:N0}" HeaderText="Estatus" UniqueName="Estatus">
					<HeaderStyle Width="16%" />
					<ItemStyle Width="16%" />
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="UsuarioAsignado" ItemStyle-Width="20px" FilterControlAltText="Filtrar columna Usuario Asignado" DataFormatString="{0:N0}" HeaderText="Usuario Asignado" UniqueName="UsuarioAsignado">
					<HeaderStyle Width="17%" />
					<ItemStyle Width="17%" />
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
							<telerik:RadToolBar ID="RadToolBar1" Height="30px" OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked2" runat="server" OnButtonClick="RadToolBar1_ButtonClick1">
								<Items>
									<telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" />
									<telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" />
									<telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" Visible="true" />
									<telerik:RadToolBarButton runat="server" Visible="false" Text="Intervenir" CommandName="Intervenir" PostBack="False" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Owner="" />
								</Items>
							</telerik:RadToolBar>
						</td>
					</tr>
				</table>
			</CommandItemTemplate>
		</MasterTableView>
		<PagerStyle AlwaysVisible="false" />
		<FilterMenu EnableImageSprites="False" />
		<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default" />
	</telerik:RadGrid>
	<telerik:RadWindow ID="RadWindow1" runat="server" Height="200px" MaxWidth="770px" DestroyOnClose="true" Title="Filtro" Width="600px" KeepInScreenBounds="true" Behaviors="Close, Reload">
		<ContentTemplate>
			<fieldset>
				<legend><b>
					<asp:Label runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource" />
				</b></legend>
				<table>
					<tr>
						<td><telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " /></td>
					</tr>
					<tr>
						<td><asp:Label ID="LblMessege" runat="server" Text="" /></td>
					</tr>
					<tr>
						<td><asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" /></td>
					</tr>
				</table>
			</fieldset>
		</ContentTemplate>
	</telerik:RadWindow>
	<telerik:RadWindow ID="RadWindowIntervenir" runat="server" class="RadWindow" Title="Intervenir" DestroyOnClose="True" KeepInScreenBounds="True" Behaviors="Close" BorderStyle="Dotted" BorderWidth="10px" Height="180px" Width="380px">
		<ContentTemplate>
			<div>
				<table width="350" cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td align="center">
							<br />
							<b>
								<asp:Label ID="lblPreguntaAnularSuspender" runat="server" Text="¿Que Opción desea Escoger" />
							</b>
						</td>
					</tr>
					<tr>
						<td align="center">
							<br />
							<asp:Button ID="btnFuncionabilidadCambiaFlujo" runat="server" Text="Cambiar Flujo" OnClientClick="AbrirFunCambiaFlujo(); return false;" />
						</td>
					</tr>
					<tr>
						<td align="center"><asp:Button ID="btnCambiaFlujo" runat="server" Text="Editar" OnClientClick="AbrirCambiaFlujo(); return false;" /></td>
					</tr>
				</table>
			</div>
		</ContentTemplate>
	</telerik:RadWindow>
	<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
		<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
		<script src="../../Scripts/Utilitarios.js" type="text/javascript"></script>
	</telerik:RadCodeBlock>
	</form>
	<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
		<script type="text/javascript">
			var nombreVentana = '<%=RadWindow1.ClientID %>';
			var nombreGrid = '<%=RadGridMaster.ClientID%>';
			var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
			var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
			var idMenu = '<%= IdMenuEncriptado %>';
			var ventanaDetalle = "MovimientoDetalle.aspx?IdMenu=" + idMenu + "&id=";
			var ItemSeleccionado;
			var wnd;
			var selectedIndexes;

			function _PanelBarItemClicked(sender, args) {
				grid = $find(nombreGrid);
				var selectedIndexes = grid._selectedIndexes;
				if (selectedIndexes.length == 0) {
					alert("Seleccione el registro a mostrar", 280, 120, "Ver detalle de Registro")
				}
				else {
					if (selectedIndexes.length == 1) {
						wnd = window.parent.radopen(ventanaDetalle + grid._clientKeyValues[selectedIndexes[0]].IdEncriptado);
						wnd.set_modal(true);
						wnd.setSize(700, 600);
						wnd.center();
					}
				}
			}

			function PanelBarItemClicked2(sender, args) {
				wnd = GetRadWindow();
				var comando = args.get_item().get_commandName();
				switch (comando) {
				    case "Intervenir":
				        intervenir = false;
						grid = $find(nombreGrid);
						selectedIndexes = grid._selectedIndexes;
						if (selectedIndexes.length == 0) {
							alert("Seleccione el registro a Intervenir", 280, 120, "Intervenir")
						}
						else {
							if (selectedIndexes.length == 1 && grid._clientKeyValues[selectedIndexes[0]].TipoP == "Pantalla") {
								if ((selectedIndexes.length == 1 && grid._clientKeyValues[selectedIndexes[0]].Estatus == "AUDITORIA") || (selectedIndexes.length == 1 && grid._clientKeyValues[selectedIndexes[0]].Estatus == "EJECUTADO"))
									intervenir = true;

								ItemSeleccionado = grid._clientKeyValues[selectedIndexes[0]].IdEncriptado;
								var oWindow = $find("<%= RadWindowIntervenir.ClientID %>");
								oWindow.set_modal(true);
								oWindow.show();
							}
							else {
								alert("Seleccione Un registro Tipo Pantalla", 280, 120, "Error")
							}
						}
						break;

					case "Refrescar":
						break;

					case "OpenRadFilter":
						$find(nombreVentana).show();
						break;

					default:
						grid = $find(nombreGrid);
						var defaulSelectedIndexes = grid._selectedIndexes;
						if (defaulSelectedIndexes.length == 0) {
							alert("Seleccione el registro a mostrar", 280, 120, "Ver detalle de Registro")
						}
						break;

					case "ViewDetails":
						grid = $find(nombreGrid);
						var selectedIndexes = grid._selectedIndexes;
						if (selectedIndexes.length == 0) {
							alert("Seleccione el registro a mostrar", 280, 120, "Ver detalle de Registro")
						}
						else {
							if (selectedIndexes.length == 1) {
								wnd = window.parent.radopen(ventanaDetalle + grid._clientKeyValues[selectedIndexes[0]].IdEncriptado);
								wnd.set_modal(true);
								wnd.setSize(700, 600);
								wnd.center();
							}
						}
						break;
				}
			}

			function AbrirFunCambiaFlujo() {
				try {
					var NoIntervenir = false;
					if (intervenir) {
						if (window.parent.document.getElementById("cphBody_txtEstatus").value == "ANULADO" || window.parent.document.getElementById("cphBody_txtEstatus").value == "CERRADO") {
							NoIntervenir = true;
						}
						else {
							var test = window.parent.radopen("../Orquestador/PantallaContenedora.aspx?idmovimiento=" + ItemSeleccionado + "&bInterventor=" + '<%= bInterventor%>' + "&Intervencion=" + '<%=VariableEncriptadaFCF %>', "Funcionalidad y cambio de flujo");
							test.setActive(true);
							test.set_modal(true);
							test.setSize(900, 600);
							test.center();
						}
					}
					else
						NoIntervenir = true;
					if (NoIntervenir)
						alert("Este movimiento no se puede Intervenir", 280, 120, "Interventor")
				}
				catch (ex) {
					alert('Error al intentar abrir la ventana, consulte al administrador del sistema...');
				}
			}

			function AbrirCambiaFlujo() {
				try {
					wnd = window.parent.radopen("../Orquestador/PantallaContenedora.aspx?idmovimiento=" + ItemSeleccionado + "&bInterventor=" + '<%= bInterventor%>' + "&Intervencion=" + '<%=VariableEncriptadaF %>');
					wnd.setActive(true);
					wnd.set_modal(true);
					wnd.setSize(900, 600);
					wnd.center();
				}
				catch (ex) {
					alert('Error al intentar abrir la ventana, consulte al administrador del sistema...');
				}
			}
			var intervenir = false;
		</script>
	</telerik:RadScriptBlock>
</body>
</html>
