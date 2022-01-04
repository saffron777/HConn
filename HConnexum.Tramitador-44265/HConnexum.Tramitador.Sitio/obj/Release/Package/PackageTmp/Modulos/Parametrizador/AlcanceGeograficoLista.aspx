<%@ Page Title="Lista de Alcance Geográfico" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="True" CodeBehind="AlcanceGeograficoLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.AlcanceGeograficoLista" meta:resourcekey="PageResource1" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
        <div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
            <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
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
                    var ventanaDetalle = "Modulos/Parametrizador/AlcanceGeograficoDetalle.aspx?IdMenu=" + idMenu + "&";
                    var nombreBoton = '<%=btnActivarEliminado.ClientID%>';

                    window.onload = function () {
                        changeTextRadAlert();
                    }
                </script>
            </telerik:RadScriptBlock>
            <telerik:RadAjaxManager id="RadAjaxManager1" 
                DefaultLoadingPanelID="RadAjaxLoadingPanel1" 
                onajaxrequest="RadAjaxManager1_AjaxRequest" runat="server" 
                meta:resourcekey="RadAjaxManager1Resource1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadGridMaster">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadFilterMaster">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadFilterMaster"/>
                            <telerik:AjaxUpdatedControl ControlID="LblMessege" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="ApplyButton">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                            <telerik:AjaxUpdatedControl ControlID="RadFilterMaster"/>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridMaster"/>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadGrid id="RadGridMaster" runat="server" autogeneratecolumns="False" width="100%"
                             cellspacing="0" gridlines="None" allowcustompaging="True" allowpaging="True"
                             allowmultirowselection="True" allowsorting="True" 
                 onpageindexchanged="RadGridMaster_PageIndexChanged"
                             onsortcommand="RadGridMaster_SortCommand" 
                onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                             onitemcommand="RadGridMaster_ItemCommand" Culture="es-ES" >
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnRowDblClick="validarRegistro" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
<Selecting CellSelectionMode="None" AllowRowSelect="True"></Selecting>

<ClientEvents OnRowDblClick="validarRegistro"></ClientEvents>

<Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado" width="100%">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Id" 
                            FilterControlAltText="Filtrar columna Id" DataFormatString="{0:N0}" 
                            HeaderText="Id" UniqueName="Id" Visible="false" 
                            meta:resourcekey="GridBoundColumnResource1"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IdServicioSucursal" 
                            FilterControlAltText="Filtrar columna IdServicioSucursal" 
                            DataFormatString="{0:N0}" HeaderText="IdServicioSucursal" 
                            UniqueName="IdServicioSucursal" Visible=false 
                            meta:resourcekey="GridBoundColumnResource2"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Servicio" 
                            FilterControlAltText="Filtrar columna Servicio" DataFormatString="{0:N0}" 
                            HeaderText="Servicio" UniqueName="FlujoServicio" 
                            meta:resourcekey="GridBoundColumnResource3"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Version" 
                            FilterControlAltText="Filter Version column" HeaderText="Version" 
                            UniqueName="Version">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Sucursal" 
                            FilterControlAltText="Filtrar columna Sucursal" DataFormatString="{0:N0}" 
                            HeaderText="Sucursal" UniqueName="Sucursal" 
                            meta:resourcekey="GridBoundColumnResource4"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Pais" 
                            FilterControlAltText="Filtrar columna Pais" DataFormatString="{0:N0}" 
                            HeaderText="País" UniqueName="Pais" meta:resourcekey="GridBoundColumnResource5"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Div1" 
                            FilterControlAltText="Filtrar columna Div1" DataFormatString="{0:N0}" 
                            HeaderText="División Territorial 1" UniqueName="Div1" 
                            meta:resourcekey="GridBoundColumnResource6"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Div2" 
                            FilterControlAltText="Filtrar columna Div2" DataFormatString="{0:N0}" 
                            HeaderText="División Territorial 2" UniqueName="Div2" 
                            meta:resourcekey="GridBoundColumnResource7"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Div3" 
                            FilterControlAltText="Filtrar columna Div3" DataFormatString="{0:N0}" 
                            HeaderText="División Territorial 3" UniqueName="Div3" 
                            meta:resourcekey="GridBoundColumnResource8"></telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IndVigente" 
                            FilterControlAltText="Filtrar columna IndVigente"  HeaderText="Publicar" 
                            UniqueName="IndVigente" meta:resourcekey="GridCheckBoxColumnResource1"></telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="FechaValidez" 
                            FilterControlAltText="Filter column column" HeaderText="Fecha Validez" 
                            UniqueName="column" DataFormatString= "{0:d}" 
                            meta:resourcekey="GridBoundColumnResource9"></telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IndEliminado" FilterControlAltText="Filtrar columna IdEliminado"
                                                 HeaderText="Eliminado" UniqueName="IndEliminado"></telerik:GridCheckBoxColumn>
                        <telerik:GridTemplateColumn HeaderText="Tomado" UniqueName="Tomado" 
                            HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" 
                            meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="imgTomado" 
                                    ImageUrl='<%# this.ResolveUrl(Eval("Tomado", "~/Imagenes/{0}.png")) %>' 
                                    ToolTip='<%# Eval("UsuarioTomado") %>' meta:resourcekey="imgTomadoResource1"/>
                            </ItemTemplate>
<HeaderStyle Width="70px"></HeaderStyle>
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridTemplateColumn>
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
                                    <telerik:RadToolBar ID="RadToolBar1"  
                                        OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked"
                                                        runat="server"  
                                        onbuttonclick="RadToolBar1_ButtonClick1" 
                                        meta:resourcekey="RadToolBar1Resource1" >
                                        <Items>
                                            <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right"
                                                                      ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" 
                                                meta:resourcekey="RadToolBarButtonResource1" Owner=""> </telerik:RadToolBarButton>
                                            <telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right"
                                                                      ImageUrl="~/Imagenes/Filter.gif" 
                                                Text="Mostrar Filtro" meta:resourcekey="RadToolBarButtonResource2" Owner="">
                                            </telerik:RadToolBarButton>
                                            <telerik:RadToolBarButton runat="server" Text="Ver Detalle" 
                                                CommandName="ViewDetails" PostBack="False"
                                                                      ImagePosition="Right"  
                                                ImageUrl="~/Imagenes/icon_lupa18x18.png" 
                                                meta:resourcekey="RadToolBarButtonResource3" Owner="">
                                            </telerik:RadToolBarButton>                     
                                            <telerik:RadToolBarButton runat="server" CommandName="Add" Text="Agregar" 
                                                ImagePosition="Right" PostBack="False"
                                                                      ImageUrl="~/Imagenes/AddRecord.gif" 
                                                meta:resourcekey="RadToolBarButtonResource4" Owner="">
                                            </telerik:RadToolBarButton>                 
                                            <telerik:RadToolBarButton runat="server" CommandName="Edit" 
                                                ImagePosition="Right" PostBack="False"
                                                                      ImageUrl="~/Imagenes/Edit.gif" Text="Editar" 
                                                meta:resourcekey="RadToolBarButtonResource5" Owner=""></telerik:RadToolBarButton>         
                                            <telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right"
                                                                      ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" 
                                                meta:resourcekey="RadToolBarButtonResource6" Owner=""></telerik:RadToolBarButton>
                                        </Items>
                                    </telerik:RadToolBar>
                                </td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                </MasterTableView>
                <PagerStyle AlwaysVisible="True"  />
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                </HeaderContextMenu>
            </telerik:RadGrid>
            <div style="display:none">
                <telerik:RadButton ID="btnActivarEliminado" runat="server" Text="" onclick="btnActivarEliminado_Click"></telerik:RadButton>
	        </div>
            <telerik:RadWindow id="RadWindow1" runat="server" height="350px"
                               destroyonclose="True" title="Filtro" width="600px" 
                keepinscreenbounds="True">
                <ContentTemplate>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblBusquedaAvanzada" runat="server" Text="Búsqueda Avanzada" 
                                Font-Bold="True" meta:resourcekey="lblBusquedaAvanzadaResource1"></asp:Label>
                        </legend>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster"
                                                       OnItemCommand="RadFilterMaster_ItemCommand" 
                                        ShowApplyButton="False" 
                                        CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " 
                                        Culture="es-ES">
                                    </telerik:RadFilter>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblMessege" runat="server" 
                                        meta:resourcekey="LblMessegeResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ApplyButton" runat="server" 
                                        ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click"
                                                     OnClientClick="hideFilterBuilderDialog()" 
                                        meta:resourcekey="ApplyButtonResource1" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>          
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadCodeBlock id="RadCodeBlock1" runat="server">
                <script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
            </telerik:RadCodeBlock>
        </div>
</asp:Content>
