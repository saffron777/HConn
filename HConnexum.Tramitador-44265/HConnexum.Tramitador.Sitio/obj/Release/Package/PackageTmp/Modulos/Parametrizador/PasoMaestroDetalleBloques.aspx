<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Listado de Bloques por Paso" CodeBehind="PasoMaestroDetalleBloques.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.PasoMaestroDetalleBloques" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadAjaxManager id="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1"  runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridDetails">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelMaster" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
    <div>
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset>
                <legend>
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Paso" Font-Bold="True" meta:resourcekey="LblLegendPasoResource"/>
                    </b>
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre:"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Enabled="false"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br/>
        <br/>
        <telerik:RadScriptBlock runat="server" id="RadScriptBlock2">
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
                var ventanaDetalle = "Modulos/Parametrizador/PasosBloqueDetalle.aspx?IdMenu=" + idMenu + "&";
                var nombreBoton = '<%=btnActivarEliminado.ClientID%>';

                window.onload = function () {
                    changeTextRadAlert();
                }
            </script>
        </telerik:RadScriptBlock>
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
            <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="NombreBloque"
                                             FilterControlAltText="Filtrar columna NombreBloque" HeaderText="Bloque"
                                             UniqueName="NombreBloque" 
                                             meta:resourcekey="GridBoundColumnResource4"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Orden"
                                             FilterControlAltText="Filtrar columna Posicion" DataFormatString="{0:N0}"
                                             HeaderText="Pos." UniqueName="Orden" 
                                             meta:resourcekey="GridBoundColumnResource5"></telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="IndCierre" 
                                                FilterControlAltText="Filtrar columna IndCierre"  HeaderText="Cierre"
                                                UniqueName="IndCierre" 
                                                meta:resourcekey="GridCheckBoxColumnResource3"></telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="IndVigente"
                                                FilterControlAltText="Filtrar columna IndVigente"  HeaderText="Publicar"
                                                UniqueName="IndVigente" 
                                                meta:resourcekey="GridCheckBoxColumnResource4"></telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="FechaValidez" 
                                             FilterControlAltText="Filtrar columna FechaValidez" DataFormatString="{0:d}" 
                                             HeaderText="Fecha Validez" UniqueName="FechaValidez" 
                                             meta:resourcekey="GridBoundColumnResource6"></telerik:GridBoundColumn>
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
                                                    meta:resourcekey="RadToolBar1Resource2" >
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right"
                                                                  ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" 
                                                                  meta:resourcekey="RadToolBarButtonResource7" Owner=""> </telerik:RadToolBarButton>
                                        <telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right"
                                                                  ImageUrl="~/Imagenes/Filter.gif" 
                                                                  Text="Mostrar Filtro" meta:resourcekey="RadToolBarButtonResource8" Owner="">
                                        </telerik:RadToolBarButton>
                                        <telerik:RadToolBarButton runat="server" Text="Ver Detalle" 
                                                                  CommandName="ViewDetails" PostBack="False"
                                                                  ImagePosition="Right"  
                                                                  ImageUrl="~/Imagenes/icon_lupa18x18.png" 
                                                                  meta:resourcekey="RadToolBarButtonResource9" Owner="">
                                        </telerik:RadToolBarButton>                     
                                        <telerik:RadToolBarButton runat="server" CommandName="Add" Text="Agregar" 
                                                                  ImagePosition="Right" PostBack="False"
                                                                  ImageUrl="~/Imagenes/AddRecord.gif" 
                                                                  meta:resourcekey="RadToolBarButtonResource10" Owner="">
                                        </telerik:RadToolBarButton>                 
                                        <telerik:RadToolBarButton runat="server" CommandName="Edit" 
                                                                  ImagePosition="Right" PostBack="False"
                                                                  ImageUrl="~/Imagenes/Edit.gif" Text="Editar" 
                                                                  meta:resourcekey="RadToolBarButtonResource11" Owner=""></telerik:RadToolBarButton>         
                                        <telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right"
                                                                  ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" 
                                                                  meta:resourcekey="RadToolBarButtonResource12" Owner=""></telerik:RadToolBarButton>
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
        <telerik:RadWindow id="RadWindow1" runat="server" height="350px" destroyonclose="true" title="Filtro" width="600px" keepinscreenbounds="true">
            <ContentTemplate>
                <fieldset>
                    <legend>
                        <b>
                            <asp:Label ID="Label2" runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource"/>
                        </b>
                    </legend>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False"  CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblMessege" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" />
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
