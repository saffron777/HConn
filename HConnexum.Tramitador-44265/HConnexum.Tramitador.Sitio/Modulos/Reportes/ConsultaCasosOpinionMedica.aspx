<%@ Page Title="Consulta de Casos" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="ConsultaCasosOpinionMedica.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.ConsultaCasosOpinionMedica" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .RadToolBar { text-align: right; }
        </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   <div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
    <telerik:RadCodeBlock runat="server" ID="RadScriptBlock2">
        <script type="text/javascript">
            var nombreVentana = '<%=RadWindow1.ClientID %>';
            var nombreGrid = '<%=this.RadGridMaster.ClientID%>';
            var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
            var idMaster = "<%= idMaster %>";
            var AccionAgregar = "<%= AccionAgregar %>";
            var AccionModificar = "<%= AccionModificar %>";
            var AccionVer = "<%= AccionVer %>";
            var idMenu = '<%= IdMenuEncriptado %>';
            //var ventanaDetalle = "Modulos/Reportes/ReporteOpMed.aspx?IdMenu=" + idMenu + "&IdMovimiento=";           
            var ventanaDetalleHC2 = "./../Reportes/ReporteOpMed.aspx?IdMenu=" + idMenu + "&";

            function resizeRadGridWithScroll(sender, args) {
                resizeRadGrid("<%= RadGridMaster.ClientID %>");
            }

            function PanelBarItemClickedNew(sender, args) {
                var grid = $find(nombreGrid);
                var selectedIndexes = grid._selectedIndexes;
                if (selectedIndexes.length == 0)
                    radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
                else {
                    if (selectedIndexes.length == 1) {

                        var objIdMov = document.getElementById("<%= txtIdMov.ClientID%>");
                        objIdMov.value = grid._clientKeyValues[selectedIndexes[0]].IdMovimiento;
                        var boton = document.getElementById("<%= btnEnviarIdMov.ClientID%>");
                        boton.click();

                        var wnd;
                        wnd = window.radopen(ventanaDetalleHC2 + "&id=" + "", null);
                        wnd.set_modal(true);
                        wnd.maximize()
                        }
                    else
                        radalert("Seleccione solo un registro para ver el detalle", 380, 50, "Ver detalle de Registro")
                }
            }

            <%--function _PanelBarItemClicked(sender, args) {
                grid = $find(nombreGrid);
                var selectedIndexes = grid._selectedIndexes;
                if (selectedIndexes.length == 0) {
                    radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
                }
                else {
                    if (selectedIndexes.length == 1) {
                        var objIdMov = document.getElementById("<%= txtIdMov.ClientID%>");
                        var boton = document.getElementById("<%= btnEnviarIdMov.ClientID%>");
                        objIdMov.value = grid._clientKeyValues[selectedIndexes[0]].IdMovimiento;
                        boton.click();
                    }
                }
            }--%>

            window.onload = function () {
                changeTextRadAlert();
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server" meta:resourcekey="RadAjaxManager1Resource1" UpdatePanelsRenderMode="Inline">
        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="PanelMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelMaster" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadFilterMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadFilterMaster" />
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
            <telerik:AjaxSetting AjaxControlID="ddlFiltro">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="txtFiltro" />
				</UpdatedControls>
			</telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmdBuscar">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmdLimpiar">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
					</UpdatedControls>
				</telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
      <br />
      <asp:Panel ID="PanelMaster" runat="server"> 
          <asp:Panel ID="Panel1" runat="server" GroupingText="FILTRO DE BUSQUEDA">
			<table runat="server" id="T1" border="0" width="100%" class="ancho">
                <tr>
					<td class="labelCell" style="width: 10%"><asp:Label ID="lblasegurado" runat="server" Text="Asegurado:"/></td>
                    <td class="fieldCell" style="width: 27%"><asp:TextBox ID="txtAsegurado" runat="server" Width="300px"></asp:TextBox></td>
					<td class="labelCell" style="width: 10%"><asp:Label ID="lblFechaInicial" runat="server" Text="Fecha Desde:"></asp:Label></td>
                    <td class="fieldCell">
						<telerik:RadDatePicker ID="txtFechaInicial" runat="server" MinDate="1900-01-01" DateInput-MaxLength="10" DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessage="DD/MM/YYYY" Width="60%">
							<Calendar ID="calendar1" runat="server">
								<SpecialDays>
									<telerik:RadCalendarDay Repeatable="Today">
										<ItemStyle Font-Bold="true" BorderColor="Red" />
									</telerik:RadCalendarDay>
								</SpecialDays>
							</Calendar>
						</telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvtxtFechaInicial" Visible="false" runat="server" ErrorMessage="*" Text="*" ControlToValidate="txtFechaInicial" ValidationGroup="Validaciones" CssClass="validator" Width="20px" />
					</td>
                    <td colspan="2">&nbsp;</td>                    
				</tr>
                <tr>
					<td class="labelCell" style="width: 10%"><asp:Label ID="lblCaso" runat="server" Text="Filtro:"></asp:Label></td>
					<td class="fieldCell" style="width: 27%">
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<telerik:RadComboBox ID="ddlFiltro" runat="server" AutoPostBack="true" MaxLength="20" EmptyMessage="Seleccione..." OnSelectedIndexChanged="DdlFiltro_SelectedIndexChanged" Width="130px">
										<Items>
                                            <telerik:RadComboBoxItem Text="Nro. Caso"  Value="Id"/>
											<telerik:RadComboBoxItem Text="Nro. Support Incident" Value="SupportIncident"/>
                                            <telerik:RadComboBoxItem Text="Nro. de Documento" Value="NumDocSolicitante" />
										</Items>
									</telerik:RadComboBox>
								</td>
								<td><asp:TextBox ID="txtFiltro" runat="server" Width="170px" onkeypress="return SoloNumeros(event);" /></td>
							</tr>
						</table>
					</td>
					<td class="labelCell" style="width: 10%"><asp:Label ID="lblFechahasta" runat="server" Text="Fecha Hasta:"></asp:Label></td>
					<td class="fieldCell">
                        <telerik:RadDatePicker ID="txtFechaFinal" runat="server" MinDate="1900-01-01" DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessage="DD/MM/YYYY" Width="60%">
							<Calendar ID="calendar2" runat="server">
								<SpecialDays>
									<telerik:RadCalendarDay Repeatable="Today">
										<ItemStyle Font-Bold="true" BorderColor="Red" />
									</telerik:RadCalendarDay>
								</SpecialDays>
							</Calendar>
						</telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvtxtFechaFinal" runat="server" Visible="false" ErrorMessage="*"  Text="*" ControlToValidate="txtFechaFinal" ValidationGroup="Validaciones" CssClass="validator" Width="20px" />
					</td>                   
                    <td colspan="2" align="right">
					            <asp:Button ID="CmdBuscar" runat="server" Text="Buscar" Width="100px"  OnClick="CmdBuscar_Click" ValidationGroup="Validaciones"/>
					            <asp:Button ID="CmdLimpiar" runat="server" Text="Limpiar" Width="100px" OnClick="CmdLimpiar_Click" />				               
                    </td>
				</tr>
			</table>
              <br />
              <br />
		</asp:Panel>
        <br />
        <asp:Panel runat="server" Visible="false" ID="PanelGrid" GroupingText="RESULTADO DE BÚSQUEDA">
            <telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="false" Width="99%"
						 CellSpacing="0" Culture="es-ES" GridLines="None" AllowCustomPaging="true" AllowPaging="true"
						 AllowMultiRowSelection="true" AllowSorting="true" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
						 OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
						 OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                <ClientEvents OnRowDblClick="PanelBarItemClickedNew" />
            </ClientSettings>
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado,IdMovimiento" ClientDataKeyNames="IdEncriptado,IdMovimiento">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>

                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna Caso" ItemStyle-Width="20px"  DataFormatString="{0:N0}" HeaderText="Nro. Caso" UniqueName="Id">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="20px"></ItemStyle>
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="SupportIncident" FilterControlAltText="Filtrar columna SupportIncident" ItemStyle-Width="20px" DataFormatString="{0:N0}" HeaderText="Nro. Support Incident" UniqueName="SupportIncident">
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="20px"></ItemStyle>
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Diagnostico" FilterControlAltText="Filtrar columna Diagnostico" HeaderText="Diagnostico" UniqueName="Diagnostico">
                        <HeaderStyle Width="300px" />
                        <ItemStyle Width="300px" />
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="NumDocSolicitante" FilterControlAltText="Filtrar columna NumDocSolicitante" HeaderText="Numero de Documento" UniqueName="NumDocSolicitante" Visible="true" >
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="nomAseg" FilterControlAltText="Filtrar columna nomAseg" HeaderText="Nombre del Asegurado" UniqueName="nomAseg" ItemStyle-Width="20px">
                         <HeaderStyle Width="160px" />
                         <ItemStyle Width="160px" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="Fechasolicitud" FilterControlAltText="Filtrar columna Fechasolicitud" HeaderText="Fecha de la Solicitud" UniqueName="Fechasolicitud">
                        <HeaderStyle Width="100px" />
                        <ItemStyle Width="100px" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="XMLRespuesta" FilterControlAltText="Filtrar columna XMLRespuesta" HeaderText="XMLRespuesta" UniqueName="XMLRespuesta" Visible="false" >
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="IdMovimiento" FilterControlAltText="Filtrar columna IdMovimiento" HeaderText="IdMovimiento" UniqueName="IdMovimiento" Visible="true" >
                    </telerik:GridBoundColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                </EditFormSettings>
                <PagerStyle AlwaysVisible="True" />
                <CommandItemTemplate>
                     <table cellpadding="0" cellspacing="0" border="0" width="100%">
						    <tr>
							    <td align="right">
								    <telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClickedNew" runat="server" OnButtonClick="RadToolBar1_ButtonClick1">
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
            <FilterMenu EnableImageSprites="False"></FilterMenu>
            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
        </telerik:RadGrid>
            <telerik:RadWindow id="RadWindow1" runat="server" height="350px" MaxWidth="770px" destroyonclose="true" title="Filtro" width="600px" keepinscreenbounds="true">
            
			</telerik:RadWindow>
       </asp:Panel>
     </asp:Panel>
    </div>
   
    <div style="display:none">
        <asp:TextBox ID="txtIdMov" runat="server"></asp:TextBox>
        <asp:Button ID="btnEnviarIdMov" runat="server" onclick="btnEnviarIdMov_Click" />
    </div>
    <telerik:RadWindowManager ID="Singleton" runat="server" EnableShadow="true" Localization-OK="Aceptar" VisibleStatusbar="false" />
  </div>
</asp:Content>
