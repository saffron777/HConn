<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ReporteCartaAval.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Reportes.ReporteCartaAval1" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
		.RadToolBar
		{
			text-align: right;
		}
	    .style1
        {
            width: 277px;
        }
        .style2
        {
            width: 81px;
        }
        .style3
        {
            width: 155px;
        }
	    .style4
        {
        }
	</style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
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
            <telerik:AjaxSetting AjaxControlID="cmdLimpiar">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
        <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">
		    <script type="text/javascript">
		        var nombreVentana = '<%=RadWindow1.ClientID %>';
		        var nombreGrid = '<%=RadGridMaster.ClientID%>';
		        var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
		        var idMaster = "<%= idMaster %>";
		        var AccionAgregar = "<%= AccionAgregar %>";
		        var AccionModificar = "<%= AccionModificar %>";
		        var AccionVer = "<%= AccionVer %>";
		        var idMenu = '<%= IdMenuEncriptado %>';

		        var ventanaDetalle = "Modulos/Reportes/ReporteOpMed.aspx?IdMenu=" + idMenu + "&IdMovimiento=";

		        function _PanelBarItemClicked(sender, args) {
		            grid = $find(nombreGrid);
		            var selectedIndexes = grid._selectedIndexes;
		            if (selectedIndexes.length == 0) {
		                radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
		            }
		        }

		        window.onload = function () {
		            changeTextRadAlert();
		        }

		        function resizeRadGridWithScroll(sender, args) {
		            resizeRadGrid("<%= RadGridMaster.ClientID %>");
		        }
		    </script>
	    </telerik:RadCodeBlock>
		<asp:Panel ID="PanelMaster" runat="server">
			<fieldset>
				<legend>
					<asp:Label ID="lgLegendBuscarCasos" runat="server" Font-Bold="True" Text="Buscar Casos" />
				</legend>
				<table width="100%">
					<tr>
						<td style="text-align: left;" class="style2">
							<asp:Label ID="lblasegurado" runat="server" Text="Asegurado:" CssClass="style1" Font-Bold="True" />
						</td>
						<td colspan="2">
							<asp:TextBox ID="txtAsegurado" runat="server" Width="300px" onkeypress="return ValidarSoloTexto(event);"></asp:TextBox>
						</td>
						<td class="style4" colspan="3">
							&nbsp;</td>
					</tr>
					<tr>
						<td class="style2">
							<asp:Label ID="lblIntermediario" runat="server" Font-Bold="True" 
                                Text="Intermediario:" />
						</td>
						<td style="text-align: left;" colspan="2">
							<asp:TextBox ID="txtIntermediario" runat="server" Width="300px"></asp:TextBox>
						</td>
						<td align="right" class="style4">
                            <asp:Label ID="lblFechaDesde" runat="server" Font-Bold="True" 
                                Text="Fecha desde:" />
                        </td>
					    <td align="left">
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
                        <td align="right">
                            &nbsp;</td>
					</tr>
				    <tr>
                        <td class="style2">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Filtro:" />
                        </td>
                        <td style="width: 40px; text-align: left;">
                            <telerik:RadComboBox ID="ddlFiltro" runat="server" AutoPostBack="true" 
                                EmptyMessage="Seleccione..." 
                                OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged" Width="150px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="No. de Caso" Value="Id" />
                                    <telerik:RadComboBoxItem Text="No. de Expediente" Value="SupportIncident" />
                                    <telerik:RadComboBoxItem Text="No. de Documento" Value="cedula" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtFiltro" runat="server" Width="150px" onkeypress="return SoloNumeros(event);"></asp:TextBox>
                        </td>
                        <td align="right" class="style4">
                            <asp:Label ID="lblFechaHasta" runat="server" Font-Bold="True" 
                                Text="Fecha hasta:" />
                        </td>
                        <td align="left">
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
                        <td align="right">
                            <asp:Button ID="cmdBuscar" runat="server" OnClick="cmdBuscar_Click" 
                                Text="Buscar" Width="80px" />
                            &nbsp;<asp:Button ID="cmdLimpiar" runat="server" OnClick="cmdLimpiar_Click" 
                                Text="Limpiar" Width="74px" />
                        </td>
                    </tr>
				</table>
			</fieldset>
            <br />
		    <telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%"
			    CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True"
			    AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
			    OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
			    OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand"
			    Culture="es-ES">
			    <ClientSettings EnableRowHoverStyle="true">
				    <Selecting AllowRowSelect="True" />
				    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    <ClientEvents OnGridCreated="resizeRadGridWithScroll" />
			    </ClientSettings>
			    <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"
				    DataKeyNames="IdEncriptado,IdMovimiento" ClientDataKeyNames="IdEncriptado,IdMovimiento">
				    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
				    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
					    <HeaderStyle Width="20px"></HeaderStyle>
				    </RowIndicatorColumn>
				    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
					    <HeaderStyle Width="20px"></HeaderStyle>
				    </ExpandCollapseColumn>
				    <Columns>
					        <telerik:GridBoundColumn DataField="Id" HeaderText="Nro. Caso" UniqueName="Id" FilterControlAltText="Filtrar columna Caso"
						        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
						        <HeaderStyle Width="8%"></HeaderStyle>
						        <ItemStyle Width="8%"></ItemStyle>
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn DataField="SupportIncident" HeaderText="Nro. Expediente"
						        UniqueName="SupportIncident" FilterControlAltText="Filtrar columna SupportIncident"
						        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
						        <HeaderStyle Width="12%"></HeaderStyle>
						        <ItemStyle Width="12%"></ItemStyle>
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn DataField="Diagnostico" HeaderText="Diagnostico" UniqueName="Diagnostico"
						        FilterControlAltText="Filtrar columna Diagnostico" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
						        <HeaderStyle Width="28%"></HeaderStyle>
						        <ItemStyle Width="28%"></ItemStyle>
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn DataField="NumDocSolicitante" HeaderText="Documento Identidad"
						        UniqueName="NumDocSolicitante" FilterControlAltText="Filtrar columna NumDocSolicitante"
						        Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="14%"></HeaderStyle>
						        <ItemStyle Width="14%"></ItemStyle>
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn DataField="nomAseg" HeaderText="Asegurado" UniqueName="nomAseg"
						        FilterControlAltText="Filtrar columna nomAseg" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
						        <HeaderStyle Width="26%"></HeaderStyle>
						        <ItemStyle Width="26%"></ItemStyle>
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn DataField="Fechasolicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:d}"
						        UniqueName="Fechasolicitud" FilterControlAltText="Filtrar columna Fechasolicitud" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
						        <HeaderStyle Width="12%"></HeaderStyle>
						        <ItemStyle Width="12%"></ItemStyle>
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn DataField="XMLRespuesta" HeaderText="XMLRespuesta" UniqueName="XMLRespuesta"
						        FilterControlAltText="Filtrar columna XMLRespuesta" Visible="false" />
					        <telerik:GridBoundColumn DataField="IdMovimiento" HeaderText="Movimiento" UniqueName="IdMovimiento"
						        FilterControlAltText="Filtrar columna IdMovimiento" Visible="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
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
								    <telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking"
									    OnClientButtonClicked="_PanelBarItemClicked" runat="server" OnButtonClick="RadToolBar1_ButtonClick1">
									    <Items>
										    <telerik:RadToolBarButton runat="server" CommandName="imprimir" Text="Imprimir" />
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
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorCaso"
				EmptyMessage="Número de caso" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo obligatorio">
				<TargetControls>
					<%--<telerik:TargetInput ControlID="txtCaso" />--%>
				</TargetControls>
			</telerik:NumericTextBoxSetting>
		</telerik:RadInputManager>
		<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" MaxWidth="770px"
			DestroyOnClose="true" Title="Filtro" Width="600px" KeepInScreenBounds="true" />
	</div>
	<div style="display: none">
		<asp:TextBox ID="txtIdMov" runat="server"></asp:TextBox>
	</div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="">
        <table style="width:100%;height:100%; background-color: White;">
            <tr style="height:100%">
                <td align="center" valign="middle" style="width:100%">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/imgLoader.gif"></asp:Image>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
