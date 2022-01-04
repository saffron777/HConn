<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Litado de Caso" CodeBehind="AgruparCasoLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.AgruparCasoLista" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <div>
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset>
                <legend>
                    <b>
                        <asp:Label ID="lblAgruparCaso" runat="server" Text="Agrupar Caso" Font-Bold="True"/>
                    </b>
                </legend>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LblServicio" runat="server" Text="Servicio:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdServicio" DataValueField="Id"
                                                 DataTextField="NombreServicioSuscriptor"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px" Culture="es-ES"/>
                        </td>
                        <td>
                            <asp:Label ID="lblCaso" runat="server" Text="Caso Numero:"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCasoNumero" runat="server" />
                        </td>
                    </tr>
                    <tr>
                    <td>
                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus:"/>
                        </td>
                        <td>
                            
                            <telerik:RadComboBox ID="ddlIdEstatus" DataValueField="Id"
                                                 DataTextField="NombreValor"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px"/>
                        </td>
                        <td>
                            <asp:Label ID="lblCoddoc" runat="server" Text="Cod Documento:" 
                                CssClass="style1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstatus" runat="server" />
                        </td>
                    </tr>
                    <tr>
                     <td>
                            <asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:"/>
                        </td>
                        <td>
                        <telerik:RadComboBox ID="ddlIdSuscriptor" DataValueField="Id"
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px" OnSelectedIndexChanged ="ddlIdSuscriptor_SelectedIndexChanged" AutoPostBack="true"/>
                        </td>
                        <td>
                        <asp:Label ID="lblCreadorPor" runat="server" Text="Creado Por:" 
                                    CssClass="style1"/>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="ddlCreadoPor"  DataValueField="Id"  DataTextField="Nombre"
                                                EmptyMessage="Seleccione" runat="server" Width="200px" 
                                                Culture="es-ES" Enabled="false"/>
                    </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde:" 
                                    CssClass="style1"/>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtFechaDesde" runat="server" 
                            MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY">
                            <Calendar>
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="true" BorderColor="Red"/>
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblFechaHasta" runat="server" Text="Fecha Hasta:" 
                                    CssClass="style1"/>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtFechaHasta" runat="server" 
                            MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY">
                            <Calendar>
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="true" BorderColor="Red"/>
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                    </tr>
                    <tr>
                    <td align="left">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                                    onclick="btnBuscar_Click"/>
                                <asp:Button ID="btnClear" runat="server"
                                    Text="Limpiar" onclick="btnClear_Click" />
                            </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
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
            var ventanaDetalle = "Modulos/Estructura/CasoAgrupacionDetalle.aspx?IdMenu=" + idMenu + "&";
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManager id="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
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
                     allowmultirowselection="True" allowsorting="True"  onpageindexchanged="RadGridMaster_PageIndexChanged"
                     onsortcommand="RadGridMaster_SortCommand" 
                     onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                     onitemcommand="RadGridMaster_ItemCommand" Culture="es-ES">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnRowDblClick="ShowMessage" />
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
                <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna Id" DataFormatString="{0:N0}" HeaderText="Id" UniqueName="Id"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreSuscriptor" 
                    FilterControlAltText="Filtrar columna NombreSuscriptor" 
                    DataFormatString="{0:N0}" HeaderText="Proveedor" UniqueName="NombreSuscriptor"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreServicioSuscriptor" 
                    FilterControlAltText="Filtrar columna NombreServicioSuscriptor" 
                    DataFormatString="{0:N0}" HeaderText="Servicio" 
                    UniqueName="NombreServicioSuscriptor"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NumDocSolicitante" FilterControlAltText="Filtrar columna NumDocSolicitante"  HeaderText="NumDocSolicitante" UniqueName="NumDocSolicitante"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreEstatusCaso" 
                    FilterControlAltText="Filtrar columna NombreEstatusCaso" 
                    DataFormatString="{0:N0}" HeaderText="Estatus" UniqueName="NombreEstatusCaso"></telerik:GridBoundColumn>

            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
            </EditFormSettings>
            <PagerStyle AlwaysVisible="True" />
            <CommandItemTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right">
                            <telerik:RadToolBar ID="RadToolBar1"  OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked" runat="server"  onbuttonclick="RadToolBar1_ButtonClick1" >
                                <Items>
                                    <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar"/>
                                    <telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro"/>
                                    <telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle"/>
                                    <telerik:RadToolBarButton runat="server" CommandName="Edit" ImagePosition="Right" PostBack="false" ImageUrl="~/Imagenes/Edit.gif" Text="Editar"/>
                                </Items>
                            </telerik:RadToolBar>
                        </td>
                    </tr>
                </table>
            </CommandItemTemplate>
        </MasterTableView>
        <PagerStyle AlwaysVisible="True"  />
        <FilterMenu EnableImageSprites="False"></FilterMenu>
        <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
    </telerik:RadGrid>
    <telerik:RadWindow id="RadWindow1" runat="server" height="350px" 
                       destroyonclose="true" title="Filtro" width="600px" keepinscreenbounds="true">
        <ContentTemplate>
            <fieldset>
                <legend>
                    <b>
                        <asp:Label runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource"/>
                    </b>
                </legend>
                <table>
                    <tr>
                        <td>
                            <telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False"  CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 "/>
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
</asp:Content>
