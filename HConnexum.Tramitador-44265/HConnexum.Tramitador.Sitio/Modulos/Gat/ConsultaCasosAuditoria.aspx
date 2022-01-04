<%@ Page Title="Consulta de Casos En Auditoria" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="True" CodeBehind="ConsultaCasosAuditoria.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Gat.ConsultaCasosAuditoria" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
		<telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
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
				var ventanaDetalle = "Modulos/Tracking/CasoDetalle.aspx?IdMenu=" + idMenu + "&";
			</script>
		</telerik:RadScriptBlock>
		<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
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
				<telerik:AjaxSetting AjaxControlID="cmdBuscar">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="cmdLimpiar">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<asp:Panel ID="PanelMaster" runat="server" GroupingText="CONSULTA CASOS EN AUDITORIA">
			<table runat="server" id="T1" border="0" width="100%" class="ancho">
				<tr>
					<td class="labelCell"><asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:"></asp:Label></td>
					<td class="fieldCell"><telerik:RadComboBox ID="ddlSuscriptor" DataValueField="Id" DataTextField="Nombre" EmptyMessage="Seleccione..." runat="server" Width="92%" AutoPostBack="true" Culture="es-ES" OnSelectedIndexChanged="DdlSuscriptor_SelectedIndexChanged" />
						<asp:RequiredFieldValidator ID="rvfddlSuscriptor" runat="server" ErrorMessage="*" ControlToValidate="ddlSuscriptor" CssClass="validator" ValidationGroup="Validaciones" Width="20px"/>
					</td>
					<td class="labelCell"><asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde:"></asp:Label></td>
					<td class="fieldCell">
						<telerik:RadDatePicker ID="txtFechaDesde" runat="server" MinDate="1900-01-01" DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessage="DD/MM/YYYY" Width="85%">
							<Calendar ID="calendar1" runat="server">
								<SpecialDays>
									<telerik:RadCalendarDay Repeatable="Today">
										<ItemStyle Font-Bold="true" BorderColor="Red" />
									</telerik:RadCalendarDay>
								</SpecialDays>
							</Calendar>
						</telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvtxtFechaDesde" runat="server" ErrorMessage="*" Text="*" ControlToValidate="txtFechaDesde" ValidationGroup="Validaciones" CssClass="validator" Width="20px" />
					</td>
				</tr>
				<tr>
					<td class="labelCell"><asp:Label ID="lblIdServicio" runat="server" Text="Servicio:"></asp:Label></td>
					<td class="fieldCell">
						<telerik:RadComboBox ID="ddlServicio" DataValueField="Id" DataTextField="NombreServicioSuscriptor" EmptyMessage="Seleccione..." runat="server" Width="92%" Culture="es-ES" />
						<asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="*" ControlToValidate="ddlServicio" CssClass="validator" ValidationGroup="Validaciones" Width="20px"/>
					</td>
					<td class="labelCell"><asp:Label ID="lblFechahasta" runat="server" Text="Fecha Hasta:"></asp:Label></td>
					<td class="fieldCell">
						<telerik:RadDatePicker ID="txtFechaHasta" runat="server" MinDate="1900-01-01" DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessage="DD/MM/YYYY" Width="85%">
							<Calendar ID="calendar2" runat="server">
								<SpecialDays>
									<telerik:RadCalendarDay Repeatable="Today">
										<ItemStyle Font-Bold="true" BorderColor="Red" />
									</telerik:RadCalendarDay>
								</SpecialDays>
							</Calendar>
						</telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvtxtFechaHasta" runat="server" ErrorMessage="*" ControlToValidate="txtFechaHasta" ValidationGroup="Validaciones" CssClass="validator" Width="20px" />
					</td>
				</tr>
				<tr>
					<td class="labelCell"><asp:Label ID="lblCaso" runat="server" Text="Filtro:"></asp:Label></td>
					<td class="fieldCell">
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<telerik:RadComboBox ID="ddlFiltro" runat="server" AutoPostBack="true" EmptyMessage="Seleccione..." OnSelectedIndexChanged="DdlFiltro_SelectedIndexChanged" Width="150px">
										<Items>
											<telerik:RadComboBoxItem Text="Nro. Caso" Value="Id" />
											<telerik:RadComboBoxItem Text="Nro. Solicitud" Value="IdSolicitud" />
										</Items>
									</telerik:RadComboBox>
								</td>
								<td><asp:TextBox ID="txtFiltro" runat="server" Width="140px" onkeypress="return SoloNumeros(event);" /></td>
							</tr>
						</table>
					</td>
					<td class="labelCell">&nbsp;</td>
					<td class="fieldCell">&nbsp;</td>
				</tr>
			</table>
			<br />
			<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" OnClick="CmdBuscar_Click" ValidationGroup="Validaciones">
			</asp:Button>
			<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClick="CmdLimpiar_Click">
			</asp:Button>
			<br />
			<br />
			<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="false" Width="99%"
						 CellSpacing="0" Culture="es-ES" GridLines="None" AllowCustomPaging="true" AllowPaging="true"
						 AllowMultiRowSelection="true" AllowSorting="true" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
						 OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
						 OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand">
				<ClientSettings EnableRowHoverStyle="true">
					<Selecting AllowRowSelect="true" />
					<Scrolling AllowScroll="true" UseStaticHeaders="true" />
				</ClientSettings>
				<GroupingSettings CaseSensitive="false" />
				<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"
							 DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado" Width="100%">
					<CommandItemSettings ExportToPdfText="Export to PDF" />
					<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
						<HeaderStyle Width="20px"></HeaderStyle>
					</RowIndicatorColumn>
					<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
						<HeaderStyle Width="20px"></HeaderStyle>
					</ExpandCollapseColumn>
					<Columns>
						<telerik:GridBoundColumn DataField="IdSolicitud" FilterControlAltText="Filtrar columna IdSolicitud"
												 HeaderText="Solicitud" UniqueName="IdSolicitud" />
						<telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna IdCaso"
												 HeaderText="Caso" UniqueName="Id" />
						<telerik:GridBoundColumn DataField="IdMovimiento" FilterControlAltText="Filtrar columna IdMovimiento"
												 HeaderText="Movimiento" UniqueName="IdMovimiento" />
						<telerik:GridBoundColumn DataField="Intermediario" FilterControlAltText="Filtrar columna Movimiento"
												 HeaderText="Descripcion" UniqueName="Intermediario" />
						<telerik:GridBoundColumn DataField="FechaCreacion" FilterControlAltText="Filtrar columna FechaCreacion"
												 HeaderText="Fecha de Creacion" UniqueName="FechaCreacion" />
					</Columns>
					<EditFormSettings>
						<EditColumn FilterControlAltText="Filter EditCommandColumn column" />
					</EditFormSettings>
					<PagerStyle AlwaysVisible="true" />
					<CommandItemTemplate>
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td align="right">
									<telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking"
														OnClientButtonClicked="PanelBarItemClicked" runat="server" OnButtonClick="RadToolBar1_ButtonClick1">
										<Items>
											<telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right"
																	  HoveredImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right"
																	  ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" CommandName="VerDet" ImagePosition="Right"
																	  ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" CommandName="Reactivar" ImagePosition="Right"
																	  HoveredImageUrl="~/Imagenes/Refresh.gif" Text="Reactivar" Owner="">
											</telerik:RadToolBarButton>
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
			<div style="display: none">
				<telerik:RadButton ID="btnFirefoxErrorFiltroEnter" runat="server" Text="" AutoPostBack="false">
				</telerik:RadButton>
			</div>
			<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" DestroyOnClose="True"
							   Title="Filtro" Width="600px" KeepInScreenBounds="true">
				<ContentTemplate>
					<fieldset>
						<legend>
							<asp:Label ID="lblBusquedaAvanzada" runat="server" Text="Búsqueda Avanzada" Font-Bold="true"
									   meta:resourcekey="lblBusquedaAvanzadaResource1"></asp:Label>
						</legend>
						<table>
							<tr>
								<td>
									<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster"
													   OnItemCommand="RadFilterMaster_ItemCommand" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 "
													   Culture="es-ES">
									</telerik:RadFilter>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="LblMessege" runat="server" meta:resourcekey="LblMessegeResource1"></asp:Label>
								</td>
							</tr>
						</table>
					</fieldset>
				</ContentTemplate>
			</telerik:RadWindow>
			<telerik:RadCodeBlock ID="RadScriptBlock1" runat="server">
				<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
			</telerik:RadCodeBlock>
		</asp:Panel>
	</div>
	<telerik:RadWindowManager ID="Singleton" runat="server" EnableShadow="true" Localization-OK="Aceptar" VisibleStatusbar="false" />
</asp:Content>

