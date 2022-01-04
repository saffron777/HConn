<%@ Page Title="Consulta de Casos En Auditoria" MasterPageFile="~/Master/Site.Master" Language="C#"AutoEventWireup="True" CodeBehind="ConsultaCasosAuditoria.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.ConsultaCasosAuditoria" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
   
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        runat="server">
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
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                </UpdatedControls>
            </telerik:AjaxSetting>
         
             <telerik:AjaxSetting AjaxControlID="cmdBuscar">
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
        <asp:Panel ID="PanelMaster" runat="server">

            <table width="100%">
                <tr>
                    <td style="width:70px"><asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:" /></td>
                    <td style="width:31%" >
                     <telerik:RadComboBox ID="ddlSuscriptor" DataValueField="Id" DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="365px" AutoPostBack="true" Culture="es-ES"  OnSelectedIndexChanged="ddlSuscriptor_SelectedIndexChanged" />
                    </td>
                     <td  style="width:70px" ><asp:Label ID="Label1" runat="server" Text="Fecha Desde:" /></td>
                    <td style="width:31%">
                        <telerik:RadDatePicker ID="txtFechaDesde" runat="server" MinDate="1900-01-01" 
                            DateInput-EmptyMessage="DD/MM/YYYY" Width="115px">
                            <Calendar ID="Calendar3" runat="server">
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="true" BorderColor="Red" />
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            
             
                <tr>
                    <td ><asp:Label ID="lblIdServicio" runat="server" Text="Servicio:" /></td>
                    <td >
                        <telerik:RadComboBox ID="ddlServicio" DataValueField="Id" DataTextField="NombreServicioSuscriptor"
                                             EmptyMessage="Seleccione" runat="server" Width="365px" Culture="es-ES" />
                    </td>
                  <td  ><asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Hasta:" /></td>
                    <td >
                        <telerik:RadDatePicker ID="txtFechaHasta" runat="server" MinDate="1900-01-01" 
                            DateInput-EmptyMessage="DD/MM/YYYY" Width="115px">
                            <Calendar ID="Calendar1" runat="server">
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="true" BorderColor="Red" />
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                   
                
                </tr>
                <tr>
                   <td></td>
                   <td></td>
                    <td style="width: 80px"><asp:Label ID="lblCaso" runat="server" Text="Caso:" /></td>
                    <td class="style4"><asp:TextBox ID="txtCaso" runat="server" Width="108px" /></td>
                   
                </tr>
            </table>

            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" onclick="cmdLimpiar_Click" />
                        <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" OnClick="cmdBuscar_Click" />
                    </td>
                </tr>
            </table>

        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorCaso"
                EmptyMessage="N° de Caso" Type="Number" Validation-IsRequired="False">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtCaso" />
                </TargetControls>
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>
        <br />
        <br />
        <telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
            <script type="text/javascript">
                var nombreGrid = '<%=RadGridMaster.ClientID%>';
                var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
                var idMaster = "<%= idMaster %>";
                var AccionVer = "<%= AccionVer %>";
                var idMenu = '<%= IdMenuEncriptado %>';
                var ventanaDetalle = "Modulos/Tracking/CasoDetalle.aspx?IdMenu=" + idMenu + "&";
            </script>
        </telerik:RadScriptBlock>
        <telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%"
            CellSpacing="0" GridLines="None" AllowCustomPaging="True" AllowPaging="True"
            AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
            OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged"
            OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand" >
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
            <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"
                DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado">
                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filtrar columna Caso" ItemStyle-Width="10%"  HeaderText="Caso" UniqueName="Id"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IdMovimiento" FilterControlAltText="Filtrar columna NombreSolIdMovimientoicitante" HeaderText="Movimiento" UniqueName="IdMovimiento" ItemStyle-Width="10%"></telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="Movimiento" FilterControlAltText="Filtrar columna Movimiento" HeaderText="Descripcion" UniqueName="Movimiento" ItemStyle-Width="35%"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FechaCreacion" FilterControlAltText="Filtrar columna FechaCreacion" HeaderText="Fecha de Creacion" UniqueName="FechaCreacion" ItemStyle-Width="15%"  ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FechaEjecucion" FilterControlAltText="Filtrar columna FechaEjecucion" HeaderText="Fecha de Ejecucion" UniqueName="FechaEjecucion" ItemStyle-Width="15%"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FechaEnProceso" FilterControlAltText="Filtrar columna FechaEnProceso" HeaderText="Fecha de puesta En Proceso" UniqueName="FechaEnProceso" ItemStyle-Width="15%"> </telerik:GridBoundColumn>
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
                                    OnClientButtonClicked="PanelBarItemClicked" runat="server" OnButtonClick="RadToolBar1_ButtonClick1">
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false"
                                            ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" />
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
    </div>
    <telerik:RadCodeBlock runat="server" ID="RadScriptBlock3">
       
    </telerik:RadCodeBlock>
</asp:Content>
