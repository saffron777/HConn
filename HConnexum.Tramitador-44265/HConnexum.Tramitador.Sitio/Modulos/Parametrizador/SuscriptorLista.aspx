<%@ Page Title="Listado de Suscriptores" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="SuscriptorLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.SuscriptorLista" meta:resourcekey="PageResource1" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock2">
        <script type="text/javascript">
            window.onload = window.onresize = function posicionPrincipal(){
                $('#RestrictionZone').height($(window).height() - 5);
            };

            function seleccionarSuscriptor() {

                _HiddenId = window.parent.document.getElementById("cphBody_TxtHiddenId");
                _HiddenTipo = window.parent.document.getElementById("cphBody_TxtHiddenTipo");
                _element = window.parent.document.getElementById("cphBody_TxtIdSuscriptor");
                _element2 = window.parent.document.getElementById("cphBody_TxtTipo");
                btn = window.parent.document.getElementById("cphBody_Button1");
                var grid = $find("<%=RadGridMaster.ClientID %>");
                var selectedIndexes = grid._selectedIndexes;

                $(_element).val(grid._clientKeyValues[selectedIndexes[0]].Nombre);
                $(_element2).val(grid._clientKeyValues[selectedIndexes[0]].Tipo);
                $(_HiddenId).val(grid._clientKeyValues[selectedIndexes[0]].IdEncriptado);
                $(_HiddenTipo).val(grid._clientKeyValues[selectedIndexes[0]].IdTipo);

                $(btn).removeAttr("disabled");
                btn.click();

                GetRadWindow().Close();
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow;
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
                return oWindow;
            }
        </script>
    </telerik:RadScriptBlock>
        <div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
            <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
                <script type="text/javascript">
                    var nombreVentana = '<%=RadWindow1.ClientID %>';
                    var nombreGrid = '<%=RadGridMaster.ClientID%>';
                    var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
                    var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
                    var idMaster = "<%= idMaster %>";
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
                             cellspacing="0" culture="es-ES" gridlines="None" 
                allowcustompaging="True" allowpaging="True" allowsorting="True"  onpageindexchanged="RadGridMaster_PageIndexChanged"
                             onsortcommand="RadGridMaster_SortCommand" 
                onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                             onitemcommand="RadGridMaster_ItemCommand" 
                >
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnRowDblClick="seleccionarSuscriptor" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"  DataKeyNames="IdEncriptado, Nombre, Tipo, IdTipo" ClientDataKeyNames="IdEncriptado, Nombre, Tipo, IdTipo" width="100%">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>


                        <telerik:GridBoundColumn DataField="Nombre" 
                            FilterControlAltText="Filtrar columna Nombre"  HeaderText="Nombre" 
                            UniqueName="Nombre" meta:resourcekey="GridBoundColumnResource1"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NumDoc" 
                            FilterControlAltText="Filtrar columna NumDoc"  HeaderText="Cod. Documento" 
                            UniqueName="NumDoc" meta:resourcekey="GridBoundColumnResource2"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Tipo" 
                            FilterControlAltText="Filtrar columna Tipo" HeaderText="Tipo" UniqueName="Tipo" 
                            meta:resourcekey="GridBoundColumnResource3"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IdTipo" 
                            FilterControlAltText="Filtrar columna IdTipo" HeaderText="IdTipo"  
                            UniqueName="IdTipo" Visible="false" 
                            meta:resourcekey="GridBoundColumnResource4" ></telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IndVigente"  
                            FilterControlAltText="Filtrar columna IndVigente"  HeaderText="Publicar" 
                            UniqueName="IndVigente" meta:resourcekey="GridCheckBoxColumnResource1"></telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="FechaValidez" 
                            FilterControlAltText="Filter column column" HeaderText="Fecha Validez" 
                            UniqueName="column" DataFormatString= "{0:d}" 
                            meta:resourcekey="GridBoundColumnResource5"></telerik:GridBoundColumn>


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
                                            <telerik:RadToolBarButton runat="server" CommandName="Refrescar" 
                                                ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" 
                                                meta:resourcekey="RadToolBarButtonResource1" Owner=""> </telerik:RadToolBarButton>
                                            <telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" 
                                                ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" 
                                                meta:resourcekey="RadToolBarButtonResource2" Owner=""></telerik:RadToolBarButton>

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
            <telerik:RadWindow id="RadWindow1" runat="server" height="350px" 
                               destroyonclose="True" title="Filtro" width="600px" 
                keepinscreenbounds="True" >
                <ContentTemplate>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lgLegendBusquedaAvanzada" runat="server" 
                                   Font-Bold="True" Text="Busqueda Avanzada" 
                                meta:resourcekey="lgLegendBusquedaAvanzadaResource1" />
                        </legend>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster"
                                                       OnItemCommand="RadFilterMaster_ItemCommand" 
                                        ShowApplyButton="False" 
                                                       
                                        CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " 
                                        Culture="es-ES" >
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

		