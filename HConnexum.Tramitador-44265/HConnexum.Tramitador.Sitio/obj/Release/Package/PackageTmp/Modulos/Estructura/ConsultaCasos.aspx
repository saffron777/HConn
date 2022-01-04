<%@ Page Title="Consulta de Casos" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="ConsultaCasos.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.ConsultaCasos" %>
<%@ Register Src="../Bloques/SelectorCasos.ascx" TagName="SelectorCasos" TagPrefix="uc1" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server"></asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
		<AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelMaster" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="Button1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ddlFiltro">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="txtFiltro" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadGridMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
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
        <telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
            <script type="text/javascript">
                var nombreGrid = '<%=RadGridMaster.ClientID%>';
                var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
                var idMaster = "<%= idMaster %>";
                var AccionVer = "<%= AccionVer %>";
                var idMenu = '<%= IdMenuEncriptado %>';
                var ventanaDetalleHC2 = "Modulos/Tracking/CasoDetalle.aspx?IdMenu=" + idMenu + "&";
                var ventanaDetalleHC1CE = '<%=urlClave %>';
                var ventanaDetalleHC1CA = '<%=urlCartaAval %>';
                var nombreVentana = '<%= wndHC1.ClientID %>';
                var ServicioCartaAval = '<%= this.ServicioCartaAval %>';
                var ServicioClaveEmergencia = '<%= this.ServicioClaveEmergencia %>';

                function resizeRadGridWithScroll(sender, args) {
                  resizeRadGrid("<%= RadGridMaster.ClientID %>");
                }
            </script>
        </telerik:RadScriptBlock>
        <style type="text/css">      
           div.RadGrid .rgPager .rgAdvPart     
           {     
            display:none;        
           }     
       </style>
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset>
                <legend><asp:Label ID="lgLegendBuscarCasos" runat="server" Font-Bold="True" Text="Buscar Casos" /></legend>
                <table width="100%">
                    <tr>
                        <td>&nbsp;</td>
                        <td style="width: 140px"><asp:Label ID="lblSuscriptorSimu" runat="server" Font-Bold="True" Visible="false" Text="Suscriptor Simulado:" /></td>
                        <td style="width: 301px"><telerik:RadComboBox ID="ddlSuscriptorASimular" runat="server" Culture="es-ES" DataTextField="Nombre" DataValueField="Id" EmptyMessage="Seleccione" OnSelectedIndexChanged="DdlSuscriptorSimSelectedIndexChanged" Visible="false" Width="300px" AutoPostBack="True" /></td>
                        <td></td>
                        <td style="width: 90px"></td>
                        <td style="width: 120px"></td>
                        <td></td>
                        <td style="width: 60px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="width: 140px"><asp:Label ID="lblSuscriptor" runat="server" Font-Bold="True" Text="Suscriptor:" /></td>
                        <td style="width: 301px"><telerik:RadComboBox ID="ddlSuscriptor" runat="server" AutoPostBack="true" Culture="es-ES" DataTextField="Nombre"  DataValueField="Id" EmptyMessage="Seleccione..." OnSelectedIndexChanged="DdlSuscriptorSelectedIndexChanged" Width="300px" /></td>
                        <td>&nbsp;</td>
                        <td style="width: 90px"><asp:Label ID="lblEstatus" runat="server" Font-Bold="True" Text="Estatus:" /></td>
                        <td style="width: 120px"><telerik:RadComboBox ID="ddlEstatus" runat="server" DataTextField="NombreValor" DataValueField="Id" EmptyMessage="Seleccione" Width="110px"></telerik:RadComboBox></td>
                        <td>&nbsp;</td>
                        <td style="width: 60px"><asp:Label ID="lblAsegurado" runat="server" Font-Bold="True" Text="Asegurado:" /></td>
                        <td><asp:TextBox ID="txtAsegurado" runat="server" Width="150px" onkeypress="return ValidarSoloTexto(event);" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="width: 140px"><asp:Label ID="lblIdServicio" runat="server" Text="Servicio:" Font-Bold="True" /></td>
                        <td align="left" style="width: 301px"><telerik:RadComboBox ID="ddlServicio" runat="server" DataTextField="NombreServicioSuscriptor" DataValueField="IdServicioSuscriptor" EmptyMessage="Seleccione" Width="300px"></telerik:RadComboBox></td>
                        <td>&nbsp;</td>
                        <td style="width: 90px"><asp:Label ID="lblFechaDesde" runat="server" Font-Bold="True" Text="Fecha desde:" /></td>
                        <td style="width: 120px">
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
                        <td>&nbsp;</td>
                        <td style="width: 60px"><asp:Label ID="lblIntermediario" runat="server" Font-Bold="True" Text="Intermediario:" /></td>
                        <td><asp:TextBox ID="txtIntermediario" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="width: 140px"><asp:Label ID="lblCaso" runat="server" Font-Bold="True" Text="Filtro:" /></td>
                        <td align="left" style="width: 301px">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="ddlFiltro" runat="server" AutoPostBack="True" EmptyMessage="Seleccione..." OnSelectedIndexChanged="DdlFiltroSelectedIndexChanged" Width="150px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="No. de Caso" Value="Id" />
                                                <telerik:RadComboBoxItem Text="No. de Codigo Externo" Value="Idcasoexterno" />
                                                <telerik:RadComboBoxItem Text="No. de soporte del Incidente" Value="SupportIncident" />
                                                <telerik:RadComboBoxItem Text="No. de Documento" Value="NumDocSolicitante" />
                                                <telerik:RadComboBoxItem Text="Ticket" Value="Ticket" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="padding-left: 4px"><asp:TextBox ID="txtFiltro" runat="server" Width="140px" onkeypress="return SoloNumeros(event);" /></td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 90px"><asp:Label ID="lblFechaHasta" runat="server" Font-Bold="True" Text="Fecha hasta:" /></td><td style="width: 120px">
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
                        <td>&nbsp;</td>
                        <td colspan="2" align="right">
                            <asp:Button ID="cmdBuscar" runat="server" OnClick="CmdBuscarClick" 
                                Text="Buscar" Width="100px" />
                            &nbsp;
                            <asp:Button ID="btnLimpiar" runat="server" onclick="cmdLimpiar_Click" 
                                Text="Limpiar" Width="100px" />
                        </td>
                        <td>
								<asp:Button ID="Button1" runat="server" Text="Voy" OnClick="Button1_Click" Style="display: none" CausesValidation="False" ValidationGroup="Boton" meta:resourcekey="Button1Resource1" />
							</td>
                    </tr>
                </table>
            </fieldset>
            <br />
		    <telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" 
                Width="100%"   CellSpacing="0" GridLines="None" AllowCustomPaging="True" 
                AllowPaging="True" AllowMultiRowSelection="True" AllowSorting="True" 
                OnPageIndexChanged="RadGridMaster_PageIndexChanged" 
                OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
			    OnNeedDataSource="RadGridMaster_NeedDataSource" 
                OnItemCommand="RadGridMaster_ItemCommand" Culture="es-ES"  PageSize="10" 
                 >
			    <ClientSettings EnableRowHoverStyle="true">
				    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnRowDblClick="PanelBarItemClickedNew" />
                    <ClientEvents OnGridCreated="resizeRadGridWithScroll" />
				    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
			    </ClientSettings>
			    <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="Id, IdEncriptado, Origen, IdCasoexterno2, NombreServicioSuscriptor, OrigenesDB, Idcasoexterno" ClientDataKeyNames="Id, IdEncriptado, Origen, IdCasoexterno2, NombreServicioSuscriptor, OrigenesDB, Idcasoexterno" Width="100%">
				    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
				    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
					    <HeaderStyle Width="20px"></HeaderStyle>
				    </RowIndicatorColumn>
				    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
					    <HeaderStyle Width="20px"></HeaderStyle>
				    </ExpandCollapseColumn>
				    <Columns>
					    <telerik:GridTemplateColumn HeaderText="Chat" UniqueName="Chat" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" meta:resourcekey="GridTemplateColumnResource1">
						    <ItemTemplate>
							    <asp:Image runat="server" ID="ImgChat" ImageUrl='<%# this.ResolveUrl(Eval("ImgChat", "~/Imagenes/{0}.png")) %>' />
						    </ItemTemplate>
						    <HeaderStyle Width="4%"></HeaderStyle>
						    <ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
					    </telerik:GridTemplateColumn>
					    <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna Caso"  HeaderText="Nro. Caso" UniqueName="Id" Visible="true">
						    <HeaderStyle Width="8%" />
						    <ItemStyle Width="8%"></ItemStyle>
					    </telerik:GridBoundColumn>
					    <telerik:GridBoundColumn DataField="Ticket" FilterControlAltText="Filtrar columna Ticket"  DataFormatString="{0:N0}" HeaderText="Nro. Ticket" UniqueName="Ticket" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
						    <HeaderStyle Width="8%" />
						    <ItemStyle Width="8%"></ItemStyle>
					    </telerik:GridBoundColumn>
					    <telerik:GridBoundColumn DataField="NombreSolicitante" FilterControlAltText="Filtrar columna NombreSolicitante" HeaderText="Asegurado" UniqueName="NombreSolicitante">
						    <HeaderStyle Width="28%" />
						    <ItemStyle Width="28%" />
					    </telerik:GridBoundColumn>
					    <telerik:GridBoundColumn DataField="NumDocSolicitante" FilterControlAltText="Filtrar columna NumDocSolicitante" HeaderText="Documento de Identidad" UniqueName="NumDocSolicitante" >
						    <HeaderStyle Width="17%" />
						    <ItemStyle Width="17%" />
					    </telerik:GridBoundColumn>
					   
					    <telerik:GridBoundColumn DataField="NombreEstatusCaso" FilterControlAltText="Filtrar columna NombreEstatusCaso" HeaderText="Estatus" UniqueName="NombreEstatusCaso">
						    <HeaderStyle Width="10%" />
						    <ItemStyle Width="10%" />
					    </telerik:GridBoundColumn>
					    <telerik:GridBoundColumn DataField="NombreServicioSuscriptor" FilterControlAltText="Filtrar columna NombreServicioSuscriptor" HeaderText="Servicio" UniqueName="NombreServicioSuscriptor">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Width="25%" />
                        </telerik:GridBoundColumn>
					    <telerik:GridBoundColumn DataField="Origen" FilterControlAltText="Filtrar columna Origen" HeaderText="Origen" UniqueName="Origen" Visible="false"></telerik:GridBoundColumn>
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
			    <PagerStyle AlwaysVisible="false" />
			    <FilterMenu EnableImageSprites="False"></FilterMenu>
			    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
		    </telerik:RadGrid>
        </asp:Panel>
        <telerik:RadWindow ID="wndHC1" runat="server" Height="500px" DestroyOnClose="True" Title="Casos HC1" Modal="true" Width="600px" KeepInScreenBounds="True" />
    </div>
    <telerik:RadCodeBlock runat="server" ID="RadScriptBlock3">
        <script type="text/javascript">

            function cmdlimpiarServicio() {
                var combo2 = $telerik.findControl(theForm, "ddlServicio");
                combo2.set_text("");
                combo2.set_value("");
                combo2.set_emptyMessage("Seleccione...");
            }
                
            function PanelBarItemClickedNew(sender, args){
                var grid = $find(nombreGrid);
                var selectedIndexes = grid._selectedIndexes;
                if (selectedIndexes.length == 0)
                    radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
                else{
                    if (selectedIndexes.length == 1){
                        if (grid._clientKeyValues[selectedIndexes[0]].Origen == "HC2") {           
                            var wnd;
                            wnd = window.radopen("../../" + ventanaDetalleHC2 + "accion=" + AccionVer + "&id=" + grid._clientKeyValues[selectedIndexes[0]].IdEncriptado,null);
                            wnd.set_modal(true);
                            wnd.maximize()
                        }
                        else {

                            btn = document.getElementById("cphBody_Button1");
                            $(btn).removeAttr("disabled");
                            btn.click();

                            if (grid._clientKeyValues[selectedIndexes[0]].NombreServicioSuscriptor == ServicioCartaAval) {
                                var wnd1 = radopen(ventanaDetalleHC1CA + grid._clientKeyValues[selectedIndexes[0]].Idcasoexterno + "&Intermediario=" + grid._clientKeyValues[selectedIndexes[0]].OrigenesDB, nombreVentana);
                                wnd1.set_modal(true);
                                wnd1.center();
                            }
                           else {
                                if (grid._clientKeyValues[selectedIndexes[0]].NombreServicioSuscriptor == ServicioClaveEmergencia) {
                                    var wnd2 = radopen(ventanaDetalleHC1CE + grid._clientKeyValues[selectedIndexes[0]].IdCasoexterno2 + "&Intermediario=" + grid._clientKeyValues[selectedIndexes[0]].OrigenesDB, nombreVentana);
                                    wnd2.set_modal(true);
                                    wnd2.center();
                                } else radalert("No se puede atender este caso, Servicio aun no Configurado", 380, 50, "Atención")
                            }
                        }
                    }
                    else
                        radalert("Seleccione solo un registro para ver el detalle", 380, 50, "Ver detalle de Registro")
                }
            }                
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="">
        <table style="width:100%;height:100%; background-color: White;">
            <tr style="height:100%">
                <td align="center" valign="middle" style="width:100%">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/imgLoader.gif"></asp:Image>
            </td></tr>
        </table>
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
