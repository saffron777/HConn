<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CambioMedico.ascx.cs"
	Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.CasoMedicos" %>
<style type="text/css">
	.viewWrap
	{
		padding: 15px;
		background: #2291b5 0 0 url(Img/bluegradient.gif) repeat-x;
	}
	.contactWrap
	{
		padding: 10px 15px 15px 15px;
		background: #fff;
		color: #333;
	}
	.contactWrap td
	{
		padding: 0 20px 0 0;
	}
	.contactWrap td td
	{
		padding: 3px 20px 3px 0;
	}
	.contactWrap img
	{
		border: 1px solid #05679d;
	}
</style>
<div>
	<table width="100%">
		<tr>
			<td class="labelCell">
				<asp:Label ID="lblBuscaPersona" runat="server" Text="Nombre del Medico a Buscar:" />
			</td>
			<td class="fieldCell">
				<asp:TextBox ID="txtBuscaPersona" runat="server" Width="252px"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td class="labelCell">
				<asp:Label ID="lblRedes" runat="server" Text="Redes Suscriptor:" />
			</td>
			<td>
				<telerik:RadComboBox ID="ddlIdredes" DataValueField="Id" DataTextField="NombreRed"
					EmptyMessage="Seleccione" runat="server" Culture="es-ES" Width="252px" />
			</td>
			<td class="fieldCell" style="text-align: right;">
				<asp:Button ID="btnBuscar" runat="server" Text="Buscar" CausesValidation="False"
					OnClick="btnBuscar_Click" Width="80px" />
				&nbsp;
				<asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CausesValidation="False"
					OnClick="btnLimpiar_Click" Width="80px" />
			</td>
		</tr>
	</table>
	<br />
	<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%"
		CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True"
		AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged" OnSortCommand="RadGridMaster_SortCommand"
		OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
		OnItemCommand="RadGridMaster_ItemCommand" OnEditCommand="RadGridMaster_EditCommand"
		Culture="es-ES">
		<GroupingSettings CaseSensitive="False" />
		<ClientSettings EnableRowHoverStyle="true">
			<Selecting AllowRowSelect="True" />
			<Scrolling AllowScroll="True" UseStaticHeaders="True" />
			<ClientEvents OnRowSelected="GetSelectedNames" />
		</ClientSettings>
		<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"
			DataKeyNames="Nombre" ClientDataKeyNames="Nombre">
			<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
			<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
				<HeaderStyle Width="20px"></HeaderStyle>
			</RowIndicatorColumn>
			<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
				<HeaderStyle Width="20px"></HeaderStyle>
			</ExpandCollapseColumn>
			<Columns>
				<telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filtrar columna"
					HeaderText="Nombre" UniqueName="Nombre" AutoPostBackOnFilter="True">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="Redes" FilterControlAltText="Filter column Redes"
					HeaderText="Red" UniqueName="Redes">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="Pais" FilterControlAltText="Filtrar columna"
					HeaderText="País" UniqueName="Pais" AllowFiltering="False">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="DivisionTerritorial1" FilterControlAltText="Filtrar columna"
					HeaderText="División territorial 1" UniqueName="DivisionTerritorial1" AllowFiltering="False">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="DivisionTerritorial2" FilterControlAltText="Filtrar columna"
					HeaderText="División territorial 2" UniqueName="DivisionTerritorial2" AllowFiltering="False">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="DivisionTerritorial3" FilterControlAltText="Filtrar columna"
					HeaderText="División territorial 3" UniqueName="DivisionTerritorial3" AllowFiltering="False">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="CasosPendientesCantidad" FilterControlAltText="Filtrar columna"
					HeaderText="Especialidad(es)" UniqueName="Especialidad" AllowFiltering="False">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="CasosPendientesCantidad" FilterControlAltText="Filtrar columna"
					HeaderText="Casos pendientes" UniqueName="CasosPendientesCantidad" AllowFiltering="False">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="IndTipDoc" FilterControlAltText="Filtrar columna"
					HeaderText="Tipo documento" UniqueName="IndTipDoc" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="DocumentoTipo" FilterControlAltText="Filtrar columna"
					HeaderText="DocumentoTipo" UniqueName="DocumentoTipo" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="NumDoc" FilterControlAltText="Filtrar columna"
					HeaderText="Nro. documento" UniqueName="NumDoc" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="RazonSocial" FilterControlAltText="Filtrar columna"
					HeaderText="Razón Social" UniqueName="RazonSocial" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="Direccion" FilterControlAltText="Filtrar columna"
					HeaderText="Dirección" UniqueName="Direccion" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="Estatus" FilterControlAltText="Filtrar columna"
					HeaderText="Estatus" UniqueName="Estatus" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="FechaNacimiento" FilterControlAltText="Filtrar columna"
					HeaderText="Fecha de nacimiento" UniqueName="FechaNacimiento" DataType="System.DateTime"
					DataFormatString="{0:d}" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="FechaInactivacion" FilterControlAltText="Filtrar columna"
					HeaderText="Fecha de inactivación" UniqueName="FechaInactivacion" DataType="System.DateTime"
					DataFormatString="{0:d}" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="IdPais" FilterControlAltText="Filtrar columna"
					HeaderText="IdPais" UniqueName="IdPais" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="DivisionTerritorial1Id" FilterControlAltText="Filtrar columna"
					HeaderText="DivisionTerritorial1Id" UniqueName="DivisionTerritorial1Id" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="DivisionTerritorial2Id" FilterControlAltText="Filtrar columna"
					HeaderText="DivisionTerritorial2Id" UniqueName="DivisionTerritorial2Id" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="DivisionTerritorial3Id" FilterControlAltText="Filtrar columna"
					HeaderText="DivisionTerritorial3Id" UniqueName="DivisionTerritorial3Id" Display="false">
				</telerik:GridBoundColumn>
				<telerik:GridBoundColumn DataField="CodigoExternoId" FilterControlAltText="Filtrar columna"
					HeaderText="CodigoExternoId" UniqueName="CodigoExternoId" Display="false">
				</telerik:GridBoundColumn>
			</Columns>
			<EditFormSettings>
				<EditColumn FilterControlAltText="Filter EditCommandColumn column">
				</EditColumn>
			</EditFormSettings>
			<PagerStyle AlwaysVisible="True" />
			<CommandItemTemplate>
				<div style="width: 100%; height: 40px; line-height: 3em; padding-left: 5px; font-weight: bold;">
					Cambio de Médico
				</div>
			</CommandItemTemplate>
			<NestedViewTemplate>
				<asp:Panel ID="NestedViewPanel" runat="server" CssClass="viewWrap">
					<div class="contactWrap">
						<fieldset style="padding: 10px;">
							<legend style="padding: 5px; font-weight: bold">Suscriptor</legend>
							<table>
								<tbody>
									<tr>
										<td>
											<asp:Label runat="server" Text="Documento:"></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text='<%#Eval("DocumentoTipo")%>'></asp:Label>-<asp:Label
												runat="server" Text='<%#Eval("NumDoc")%>'></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text="Estatus:"></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text='<%#Eval("Estatus")%>'></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											<asp:Label runat="server" Text="Nombre:"></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text='<%#Eval("Nombre")%>'></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text="Fecha de Nacimiento / Constitución:"></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text='<%#Eval("FechaNacimiento","{0:d}")%>'></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											<asp:Label runat="server" Text="Razón Social:"></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text='<%#Eval("RazonSocial")%>'></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text="Fecha de Inactivación:"></asp:Label>
										</td>
										<td>
											<asp:Label runat="server" Text='<%#Eval("FechaInactivacion","{0:d}")%>'></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											<asp:Label runat="server" Text="Dirección:"></asp:Label>
										</td>
										<td colspan="3">
											<asp:Label runat="server" Text='<%#Eval("Direccion")%>'></asp:Label>
										</td>
									</tr>
								</tbody>
							</table>
						</fieldset>
					</div>
				</asp:Panel>
			</NestedViewTemplate>
		</MasterTableView>
		<PagerStyle AlwaysVisible="True" />
		<FilterMenu EnableImageSprites="False" OnClientShown="MenuShowing">
		</FilterMenu>
		<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
		</HeaderContextMenu>
	</telerik:RadGrid>
	<div style="display: none">
		<telerik:RadButton ID="btnActivarEliminado" runat="server" Text="" OnClick="btnActivarEliminado_Click">
		</telerik:RadButton>
	</div>
	<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" DestroyOnClose="true"
		Title="Filtro" Width="600px" KeepInScreenBounds="true">
		<ContentTemplate>
			<fieldset>
				<legend><b>
					<asp:Label runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource" />
				</b></legend>
				<table>
					<tr>
						<td>
							<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster"
								OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="LblMessege" runat="server" Text=""></asp:Label>
						</td>
					</tr>
					<tr>
						<td>
							<asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif"
								OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" />
						</td>
					</tr>
				</table>
			</fieldset>
		</ContentTemplate>
	</telerik:RadWindow>
	<asp:HiddenField ID="tipdocmed" runat="server" />
	<asp:HiddenField ID="numdocmed" runat="server" />
	<asp:HiddenField ID="nomMed" runat="server" />
	<asp:HiddenField ID="idPaisMed" runat="server" />
	<asp:HiddenField ID="idDiv1Med" runat="server" />
	<asp:HiddenField ID="idDiv2Med" runat="server" />
	<asp:HiddenField ID="idDiv3Med" runat="server" />
	<asp:HiddenField ID="espMed" runat="server" />
	<asp:HiddenField ID="pendientes" runat="server" />
	<asp:HiddenField ID="codIdExternoMed" runat="server" />
	<asp:HiddenField ID="ResCambioMed" runat="server" Value="false" />
	<asp:HiddenField ID="numdocmedInicial" runat="server" />
</div>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
	<script type="text/javascript" src="../../Scripts/RadFilterScriptBlock.js"></script>
	<script type="text/javascript">
		/* [ FUNCIÓN QUE MODIFICA LOS MENSAJES DE LOS CONTROLES RadAlert y RadConfirm ejemplo: 'OK' por 'Aceptar'] */
		window.onload = function () {
			changeTextRadAlert();
		}
                        
	</script>
</telerik:RadScriptBlock>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
	<script type="text/javascript">
		var nombreVentana = '<%=RadWindow1.ClientID %>';
		var nombreGrid = '<%=RadGridMaster.ClientID%>';
		var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
		var idMaster = "<%= IdMaster %>";
		var AccionAgregar = "<%= AccionAgregar %>";
		var AccionModificar = "<%= AccionModificar %>";
		var AccionVer = "<%= AccionVer %>";
		var nombreBoton = '<%=btnActivarEliminado.ClientID%>';

		function GetSelectedNames(sender, eventArgs) {
			var grid = sender;
			var MasterTable = grid.get_masterTableView();
			var selectedRows = MasterTable.get_selectedItems();
			if (selectedRows.length > 0) {
				if (document.getElementById("<%= numdocmedInicial.ClientID %>").value == "")
					document.getElementById("<%= numdocmedInicial.ClientID %>").value = document.getElementById("<%= numdocmed.ClientID %>").value;
				for (var i = 0; i < selectedRows.length; i++) {
					var row = selectedRows[i];
					var colName;
					var clientId;
					for (var j = 1; j < 11; j++) {
						switch (j) {
							case 1:
								colName = "IndTipDoc";
								clientId = "<%= tipdocmed.ClientID %>";
								break;
							case 2:
								colName = "NumDoc";
								clientId = "<%= numdocmed.ClientID %>";
								break;
							case 3:
								colName = "Nombre";
								clientId = "<%= nomMed.ClientID %>";
								break;
							case 4:
								colName = "IdPais";
								clientId = "<%= idPaisMed.ClientID %>";
								break;
							case 5:
								colName = "DivisionTerritorial1Id";
								clientId = "<%= idDiv1Med.ClientID %>";
								break;
							case 6:
								colName = "DivisionTerritorial2Id";
								clientId = "<%= idDiv2Med.ClientID %>";
								break;
							case 7:
								colName = "DivisionTerritorial3Id";
								clientId = "<%= idDiv3Med.ClientID %>";
								break;
							case 8:
								colName = "Especialidad";
								clientId = "<%= espMed.ClientID %>";
								break;
							case 9:
								colName = "CasosPendientesCantidad";
								clientId = "<%= pendientes.ClientID %>";
								break;
							case 10:
								colName = "CodigoExternoId";
								clientId = "<%= codIdExternoMed.ClientID %>";
								break;
						}
						cell = MasterTable.getCellByColumnUniqueName(row, colName);
						var control = document.getElementById(clientId);
						control.value = cell.innerHTML;
					}
				}
				var controlRespuesta = document.getElementById("<%= ResCambioMed.ClientID %>");
				if (document.getElementById("<%= numdocmed.ClientID %>").value == (document.getElementById("<%= numdocmedInicial.ClientID %>").value))
					controlRespuesta.value = "false";
				else
					controlRespuesta.value = "true";
			}
		}
		/* MANEJO DE LOS FILTROS DEL GRID ****************************************************************************************/
		function MenuShowing(sender, args) {
			var menu = sender;
			var items = menu.get_items();
			var i = 0;
			while (i < items.get_count()) {
				var item = items.getItem(i);
				if (item != null)
					item.set_visible(false);
				i++;
			}
			menu.repaint();
		}
		/* MANEJO DE LOS FILTROS DEL GRID ****************************************************************************************/
	</script>
</telerik:RadCodeBlock>
