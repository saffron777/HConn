<%@ Page Title="Listado de Acciones del Paso" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="True" CodeBehind="AccionesdelPasoLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.AccionesdelPasoLista" meta:resourcekey="PageResource1" %>
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
                    var ventanaDetalle = "Modulos/Parametrizador/AccionesdelPasoDetalle.aspx?IdMenu=" + idMenu + "&";
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
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblServicio" runat="server" Text="Servicio:" 
                                   meta:resourcekey="lblServicioResource1"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtServicio" runat="server" ReadOnly="True" 
                                     meta:resourcekey="txtServicioResource1" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEstatus" runat="server" Text="Estatus:" 
                                   meta:resourcekey="lblEstatusResource1"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEstatus" runat="server" ReadOnly="True" 
                                     meta:resourcekey="txtEstatusResource1" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblVersion" runat="server" Text="Versi?n:" 
                                   meta:resourcekey="lblVersionResource1"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVersion" runat="server" ReadOnly="True" 
                                     meta:resourcekey="txtVersionResource1" ></asp:TextBox>
                    </td>
                </tr>
            </table>

            <telerik:RadGrid id="RadGridMaster" runat="server" autogeneratecolumns="False" width="100%"
                             cellspacing="0" culture="es-ES" gridlines="None" 
                             allowcustompaging="True" allowpaging="True"
                             allowmultirowselection="True" allowsorting="True" 
                              onpageindexchanged="RadGridMaster_PageIndexChanged"
                             onsortcommand="RadGridMaster_SortCommand" 
                             onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                             onitemcommand="RadGridMaster_ItemCommand">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnRowDblClick="validarRegistro" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
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

                        <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna Id" 
                                                 DataFormatString="{0:N0}" HeaderText="Id" 
                                                 UniqueName="Id" Visible="False" meta:resourcekey="GridBoundColumnResource1"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PasoOrigen" 
                                                 FilterControlAltText="Filter column2 column" HeaderText="Paso Origen" 
                                                 UniqueName="column2" 
                                                 meta:resourcekey="GridBoundColumnResource2">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Respuesta" 
                                                 FilterControlAltText="Filtrar columna IdRespuesta" DataFormatString="{0:N0}" 
                                                 HeaderText="Respuesta" UniqueName="IdRespuesta" 
                                                 meta:resourcekey="GridBoundColumnResource3"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TipoProximoPaso" 
                                                 FilterControlAltText="Filter column1 column" HeaderText="Tipo Proximo Paso" 
                                                 UniqueName="column1" 
                                                 meta:resourcekey="GridBoundColumnResource4">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PasoDestino" 
                                                 FilterControlAltText="Filtrar columna IdPasoDestino" DataFormatString="{0:N0}" 
                                                 HeaderText="Paso Destino" UniqueName="IdPasoDestino" 
                                                 meta:resourcekey="GridBoundColumnResource5"></telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IndVigente" 
                                                    FilterControlAltText="Filtrar columna IndVigente"  HeaderText="Publicar" 
                                                    UniqueName="IndVigente" 
                                                    meta:resourcekey="GridCheckBoxColumnResource1"></telerik:GridCheckBoxColumn>

                        <telerik:GridBoundColumn DataField="FechaValidez" FilterControlAltText="Filter column column" 
                                                 HeaderText="Fecha Validez" UniqueName="column" 
                                                 DataFormatString= "{0:d}" meta:resourcekey="GridBoundColumnResource6"></telerik:GridBoundColumn>

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
                    <WebServiceSettings>
                        <ODataSettings InitialContainerName=""></ODataSettings>
                    </WebServiceSettings>
                </FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                    <WebServiceSettings>
                        <ODataSettings InitialContainerName=""></ODataSettings>
                    </WebServiceSettings>
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
                            <asp:Label ID="lblBusquedaAvanzada" runat="server" Text="B?squeda Avanzada" 
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


		
