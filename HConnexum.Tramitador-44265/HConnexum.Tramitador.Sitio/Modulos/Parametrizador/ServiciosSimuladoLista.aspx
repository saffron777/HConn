<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Site.Master"
    Title="Servicios Simulados" CodeBehind="ServiciosSimuladoLista.aspx.cs"
    Inherits="HConnexum.Tramitador.Sitio.ServiciosSimuladoLista" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
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
            var ventanaDetalle = "Modulos/Parametrizador/ServiciosSimuladoDetalle.aspx?IdMenu=" + idMenu + "&";
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
         onajaxrequest="RadAjaxManager1_AjaxRequest" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridMaster">
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
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%"
        CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True"
        AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
        OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
        OnNeedDataSource="RadGridMaster_NeedDataSource" 
        OnItemCommand="RadGridMaster_ItemCommand" Culture="es-ES">
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowDblClick="ShowMessage" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <MasterTableView Width="100%" CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"
            DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado">
            <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="Id" 
                    FilterControlAltText="Filtrar columna Id"  HeaderStyle-Width="50px"
                    DataFormatString="{0:N0}" HeaderText="Id" UniqueName="Id" Visible="False">
                <HeaderStyle Width="50px"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreServicio" FilterControlAltText="Filtrar columna NombreServicio"
                    DataFormatString="{0:N0}" HeaderText="Nombre Servicio" UniqueName="NombreServicio">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreSuscriptor" FilterControlAltText="Filtrar columna NombreSuscriptor"
                    DataFormatString="{0:N0}" HeaderText="Suscriptor" UniqueName="NombreSuscriptor">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UsuarioIncorporador" FilterControlAltText="Filtrar columna UsuarioIncorporador"
                    DataFormatString="{0:N0}" HeaderText="Usuario Incorporador" UniqueName="UsuarioIncorporador">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FechaInicio" FilterControlAltText="Filtrar columna FechaInicio"
                    DataFormatString="{0:d}" HeaderText="Fecha Inicio" UniqueName="FechaInicio">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FechaFin" FilterControlAltText="Filtrar columna FechaFin"
                    DataFormatString="{0:d}" HeaderText="Fecha Fin" UniqueName="FechaFin">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Tomado" UniqueName="Tomado" HeaderStyle-Width="70px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image runat="server" ID="imgTomado" ImageUrl='<%# this.ResolveUrl(Eval("Tomado", "~/Imagenes/{0}.png")) %>'
                            ToolTip='<%# Eval("UsuarioTomado") %>' /></ItemTemplate>
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
                            <telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking"
                                OnClientButtonClicked="PanelBarItemClicked" runat="server" Skin="Windows7" OnButtonClick="RadToolBar1_ButtonClick1">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right"
                                        ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" />
                                    <telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right"
                                        ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" />
                                    <telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false"
                                        ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" />
                                    <telerik:RadToolBarButton runat="server" CommandName="Add" Text="Agregar" ImagePosition="Right"
                                        PostBack="false" ImageUrl="~/Imagenes/AddRecord.gif" />
                                    <telerik:RadToolBarButton runat="server" CommandName="Edit" ImagePosition="Right"
                                        PostBack="false" ImageUrl="~/Imagenes/Edit.gif" Text="Editar" />
                                    <telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right"
                                        ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" />
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
    <telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" Skin="Windows7"
        DestroyOnClose="true" Title="Filtro" Width="600px" KeepInScreenBounds="true">
        <ContentTemplate>
            <fieldset>
                <legend><b>
                    <asp:Label runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource" /></b></legend>
                <table>
                    <tr>
                        <td>
                            <telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster"
                                OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" Skin="Windows7"
                                CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " />
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
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
    </telerik:RadCodeBlock>
</asp:Content>
