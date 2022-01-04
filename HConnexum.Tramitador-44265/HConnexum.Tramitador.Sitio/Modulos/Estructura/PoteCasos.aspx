<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Actividades Pendientes" CodeBehind="PoteCasos.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.PoteCasos" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
		html {overflow-x: hidden; }
	</style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<style type="text/css">
		div.RadGrid .rgPager .rgAdvPart { display: none; }
	</style>
	<div>
		<asp:Panel ID="PanelMaster" runat="server">
			<fieldset>
				<legend><asp:Label ID="lgLegendBuscarCasos" runat="server" Font-Bold="True" Text="Buscar Casos" /></legend>
				<table width="80%">
					<tr>
						<td style="width: 100px"><asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:" Font-Bold="True" /></td>
						<td style="width: 196px">
							<telerik:RadComboBox ID="ddlSuscriptor" DataValueField="IdSuscriptor" DataTextField="NombreSuscriptor" EmptyMessage="Seleccione" runat="server"
								Width="195px" OnSelectedIndexChanged="ddlSuscriptor_SelectedIndexChanged" AutoPostBack="true" />
						</td>
						<td style="width: 190px">&nbsp;</td>
						<td style="width: 30px">&nbsp;</td>
						<td style="width: 190px">&nbsp;</td>
						<td style="width: 100px">&nbsp;</td>
						<td style="width: 196px">&nbsp;</td>
					</tr>
					<tr>
						<td style="width: 100px"><asp:Label ID="lblServcio" runat="server" Text="Servicio:" Font-Bold="True" /></td>
						<td style="width: 196px;">
							<telerik:RadComboBox ID="ddlServicio" DataValueField="IdServicioSuscriptor" DataTextField="NombreServicioSuscriptor" Width="195px"
								EmptyMessage="Seleccione" runat="server" />
						</td>
						<td style="width: 190px">&nbsp;</td>
						<td style="width: 30px"><asp:Label ID="lblFechaDesde" runat="server" Font-Bold="True" Text="Fecha desde:" Width="100px" /></td>
						<td style="width: 190px">
							<telerik:RadDatePicker ID="txtFechaDesde" runat="server" DateInput-EmptyMessage="DD/MM/YYYY" MinDate="1900-01-01" Width="115px">
								<Calendar ID="Calendar5" runat="server">
									<SpecialDays>
										<telerik:RadCalendarDay Repeatable="Today">
											<ItemStyle BorderColor="Red" Font-Bold="true" />
										</telerik:RadCalendarDay>
									</SpecialDays>
								</Calendar>
							</telerik:RadDatePicker>
						</td>
						<td style="width: 100px"><asp:Label ID="lblasegurado" runat="server" Text="Asegurado:" Font-Bold="True" /></td>
						<td style="width: 196px; padding-right: 10px;"><asp:TextBox ID="txtAsegurado" runat="server" Width="195px" onkeypress="return ValidarSoloTexto(event);" /></td>
					</tr>
					<tr>
						<td style="width: 100px"><asp:Label ID="lblCaso" runat="server" Text="Filtro:" Font-Bold="True" /></td>
						<td style="width: 196px">
							<telerik:RadComboBox ID="ddlFiltro" runat="server" EmptyMessage="Seleccione..." Width="195px" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged"
								AutoPostBack="true">
								<Items>
									<telerik:RadComboBoxItem Text="No. de Caso" Value="Id" />
									<telerik:RadComboBoxItem Text="No. de Codigo Externo" Value="Idcasoexterno" />
									<telerik:RadComboBoxItem Text="No. de soporte del Incidente" Value="SupportIncident" />
									<telerik:RadComboBoxItem Text="No. de Documento" Value="cedula" />
									<telerik:RadComboBoxItem Text="Ticket" Value="Ticket" />
								</Items>
							</telerik:RadComboBox>
						</td>
						<td style="width: 190px"><asp:TextBox ID="txtFiltro" runat="server" Width="150px" onkeypress="return SoloNumeros(event);" /></td>
						<td style="width: 30px"><asp:Label ID="lblFechaHasta" runat="server" Font-Bold="True" Text="Fecha hasta:" Width="100px" /></td>
						<td style="width: 190px">
							<telerik:RadDatePicker ID="txtFechaHasta" runat="server" DateInput-EmptyMessage="DD/MM/YYYY" MinDate="1900-01-01" Width="115px">
								<Calendar ID="Calendar6" runat="server">
									<SpecialDays>
										<telerik:RadCalendarDay Repeatable="Today">
											<ItemStyle BorderColor="Red" Font-Bold="true" />
										</telerik:RadCalendarDay>
									</SpecialDays>
								</Calendar>
							</telerik:RadDatePicker>
						</td>
						<td style="width: 100px"><asp:Label ID="lblIntermediario" runat="server" Text="Intermediario:" Font-Bold="True" /></td>
						<td style="width: 196px; padding-right: 10px;"><asp:TextBox ID="txtIntermediario" runat="server" Width="195px" /></td>
					</tr>
				</table>
				<table style="width: 100%">
					<tr>
						<td align="right">
							<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" OnClick="cmdBuscar_Click" Width="80px" />&nbsp;
							<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClick="Limpiar" Width="74px" />
						</td>
						<td>
							<asp:Button ID="Button1" runat="server" Text="Voy" OnClick="Button1_Click" Style="display: none" CausesValidation="False" ValidationGroup="Boton"
								meta:resourcekey="Button1Resource1" />
						</td>
					</tr>
				</table>
			</fieldset>
			<br />
			<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%" CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True"
							 AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged" OnSortCommand="RadGridMaster_SortCommand"
							 OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand"
							 Culture="es-ES" OnItemDataBound="RadGridMaster_ItemDataBound">
				<ClientSettings EnableRowHoverStyle="true">
					<Selecting AllowRowSelect="True" />
					<ClientEvents OnRowDblClick="PanelBarItemClicked2" />
					<Scrolling AllowScroll="True" UseStaticHeaders="True" />
					<Selecting AllowRowSelect="True" CellSelectionMode="None" />
					<ClientEvents OnRowDblClick="PanelBarItemClicked2" />
					<Scrolling AllowScroll="True" UseStaticHeaders="True" />
					<ClientEvents OnGridCreated="resizeRadGridWithScroll" />
				</ClientSettings>
				<MasterTableView Width="100%" CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" TableLayout="Auto"
					DataKeyNames="IdCaso, IdEncriptado, IdExp_Web, OrigenesDB, origen, NombreServicioSuscriptor, Reclamo" ClientDataKeyNames="IdCaso, IdEncriptado, IdExp_Web, OrigenesDB, origen, NombreServicioSuscriptor, Reclamo">
					<CommandItemSettings ExportToPdfText="Export to PDF" />
					<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" />
					<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" />
					<Columns>
						<telerik:GridTemplateColumn HeaderText="Chat" UniqueName="Chat" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" meta:resourcekey="GridTemplateColumnResource1">
							<ItemTemplate>
								<asp:Image runat="server" ID="ImgChat" ImageUrl='<%# this.ResolveUrl(Eval("ImgChat", "~/Imagenes/{0}.png")) %>' />
							</ItemTemplate>
							<HeaderStyle Width="4%" />
							<ItemStyle Width="4%" />
						</telerik:GridTemplateColumn>
						<telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna Id" HeaderText="Id" UniqueName="Id" Visible="false">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="IdCaso" FilterControlAltText="Filtrar columna IdCaso" HeaderText="Caso" UniqueName="IdCaso" Visible="True">
							<HeaderStyle Width="6%" />
							<ItemStyle Width="6%" />
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="Ticket" FilterControlAltText="Filtrar columna Ticket" HeaderText="Ticket" UniqueName="Ticket" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
							<HeaderStyle Width="6%" />
							<ItemStyle Width="6%" />
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="NombreAsegurado" FilterControlAltText="Filtrar columna NombreAsegurado" HeaderText="Asegurado" UniqueName="NombreAsegurado">
							<HeaderStyle Width="26%" />
							<ItemStyle Width="26%" />
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="TipDocAsegurado" FilterControlAltText="Filtrar columna TipDocAsegurado" HeaderText="Documento de Identidad" UniqueName="TipDocAsegurado">
							<HeaderStyle Width="16%" />
							<ItemStyle Width="16%" />
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="FechaCreacionMov" FilterControlAltText="Filtrar columna FechaCreacionMov" DataFormatString="{0:d}" HeaderText="Fecha Movimiento" UniqueName="FechaCreacionMov" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
							<HeaderStyle Width="14%" />
							<ItemStyle Width="14%" />
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="NombreSuscriptor" FilterControlAltText="Filtrar columna NombreSuscriptor" HeaderText="Suscriptor" UniqueName="NombreSuscriptor" Visible="False">
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="Actividad" FilterControlAltText="Filtrar columna Actividad" HeaderText="Actividad" UniqueName="Actividad">
							<HeaderStyle Width="32%" />
							<ItemStyle Width="32%" />
						</telerik:GridBoundColumn>
						<telerik:GridBoundColumn DataField="Intermediario" FilterControlAltText="Filtrar columna Intermediario" HeaderText="Intermediario" UniqueName="Intermediario" Visible="False">
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
									<telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking" runat="server" OnClientButtonClicked="PanelBarItemClicked2">
										<Items>
											<telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" />
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
		</asp:Panel>
	</div>
	<telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
		<script type="text/javascript">
			var nombreGrid = '<%=this.RadGridMaster.ClientID%>';
			var nombreRadAjaxManager = '<%= this.RadAjaxManager1.ClientID %>';
			var idMaster = "<%= this.idMaster %>";
			var AccionAgregar = "<%= this.AccionAgregar %>";
			var AccionModificar = "<%= this.AccionModificar %>";
			var AccionVer = "<%= this.AccionVer %>";
			var idMenu = '<%= this.IdMenuEncriptado %>';
			var idMaster = "<%= this.idMaster %>";
			var AccionVer = "<%= this.AccionVer %>";
			var idMenu = '<%= this.IdMenuEncriptado %>';

			var ventanaDetalle = "Modulos/Estructura/MisActividadesDetalle.aspx?IdMenu=" + idMenu + "&";

			var ventanaDetalleHC1CE = '<%=this.urlClave %>';
			var ventanaDetalleHC1CA = '<%=this.urlCartaAval %>';  //se trae los datos de lista valor
			var ServicioCartaAval = '<%= this.ServicioCartaAval %>';
			var ServicioClaveEmergencia = '<%= this.ServicioClaveEmergencia %>';
			var nombreVentana = '<%= this.wndHC1.ClientID %>';

			function cmdLimpiar_Click() {

				var comboS = $find("<%= this.ddlSuscriptor.ClientID %>");
				comboS.set_text("");
				comboS._applyEmptyMessage();
				var combov = $find("<%= this.ddlServicio.ClientID %>");
				combov.set_text("");
				combov._applyEmptyMessage();
			}

			function PanelBarItemClicked2(sender, args) {
				var wnd = GetRadWindow();
				var grid = $find(nombreGrid);
				var selectedIndexes = grid._selectedIndexes;
				if (selectedIndexes.length == 0) {
					radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
				}
				else {
					if (selectedIndexes.length == 1) {
						if (grid._clientKeyValues[selectedIndexes[0]].origen == "HC2") {
							wnd.setUrl(ventanaDetalle + "id=" + grid._clientKeyValues[selectedIndexes[0]].IdEncriptado);
						}
						else {
							btn = document.getElementById("cphBody_Button1");
							$(btn).removeAttr("disabled");
							btn.click();

							if (grid._clientKeyValues[selectedIndexes[0]].NombreServicioSuscriptor == ServicioCartaAval) {
								var wnd1 = radopen(ventanaDetalleHC1CA + grid._clientKeyValues[selectedIndexes[0]].Reclamo + "&Intermediario=" + grid._clientKeyValues[selectedIndexes[0]].OrigenesDB, nombreVentana);
								wnd1.set_modal(true);
								wnd1.center();
							}
				            else {
				                if (grid._clientKeyValues[selectedIndexes[0]].NombreServicioSuscriptor == ServicioClaveEmergencia) 
                                    {
								      var wnd2 = radopen(ventanaDetalleHC1CE + grid._clientKeyValues[selectedIndexes[0]].IdExp_Web + "&Intermediario=" + grid._clientKeyValues[selectedIndexes[0]].OrigenesDB, nombreVentana);
								      wnd2.set_modal(true);
								      wnd2.center();
                                    }
							   }
						}
					}
					else {
						radalert("Seleccione solo un registro para ver el detalle", 380, 50, "Ver detalle de Registro")
					}
				}
			}

			function ShowMessage2(sender, args) {
				var wnd = GetRadWindow();
				var id = args.getDataKeyValue("IdEncriptado");
				$find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
				wnd.setUrl(ventanaDetalle + "idmovimiento=" + id);
			}
			function resizeRadGridWithScroll(sender, args) {
				resizeRadGrid("<%= this.RadGridMaster.ClientID %>");
			}
		</script>
	</telerik:RadScriptBlock>
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="PanelMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" LoadingPanelID="RadAjaxLoadingPanel1" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadGridMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ddlFiltro">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="txtFiltro" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ddlSuscriptor">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="ddlServicio" />
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
	<telerik:RadInputManager ID="RadInputManager1" runat="server">
		<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorCaso" EmptyMessage="Número de caso" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo obligatorio">
			<TargetControls>
				<telerik:TargetInput ControlID="txtCaso" />
			</TargetControls>
		</telerik:NumericTextBoxSetting>
		<telerik:TextBoxSetting BehaviorID="BehaviorAsegurado" EmptyMessage="Nombre del Asegurado" Validation-IsRequired="false">
			<TargetControls>
				<telerik:TargetInput ControlID="txtAsegurado" />
			</TargetControls>
		</telerik:TextBoxSetting>
		<telerik:TextBoxSetting BehaviorID="BehaviortxtCedula" EmptyMessage="Número de Documento" Validation-IsRequired="false">
			<TargetControls>
				<telerik:TargetInput ControlID="txtCedula" />
			</TargetControls>
		</telerik:TextBoxSetting>
		<telerik:TextBoxSetting BehaviorID="BehaviortxtIntermediario" EmptyMessage="Intermediario" Validation-IsRequired="false">
			<TargetControls>
				<telerik:TargetInput ControlID="txtIntermediario" />
			</TargetControls>
		</telerik:TextBoxSetting>
	</telerik:RadInputManager>
	<telerik:RadWindow ID="wndHC1" runat="server" Height="500px" DestroyOnClose="True" Title="Casos HC1" Modal="true" Width="600px" KeepInScreenBounds="True" />
	<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="">
		<table style="width: 100%; height: 100%; background-color: White;">
			<tr style="height: 100%">
				<td align="center" valign="middle" style="width: 100%"><asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/imgLoader.gif"></asp:Image></td>
			</tr>
		</table>
	</telerik:RadAjaxLoadingPanel>
</asp:Content>
