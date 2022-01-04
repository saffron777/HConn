<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Litado de Caso" CodeBehind="PriorizarCasoLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.PriorizarCasoLista" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
        html { overflow-x:hidden; }
        div.MyCustomScrollImage .rgHeaderDiv
        {
        background:0 0 repeat-x url("...............") ;
        }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <div>
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset>
                <legend>
                    <b>
                        <asp:Label ID="lblAnularCaso" runat="server" Text="Priorizar Caso" Font-Bold="True" meta:resourcekey="LblLegendCasoResource"/>
                    </b>
                </legend>
                <table>
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
                        <td>
                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlEstatus" DataValueField="Id"
                                                 DataTextField="NombreValor"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="160px"/>
                        </td>
                    </tr>
                    <tr>
                    <td>
                            <asp:Label ID="lblTipoDoc" runat="server" Text="Tipo Documento:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdTipoDoc" DataValueField="Id"
                                                 DataTextField="NombreValor"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px"/>
                        </td>
                        <td>
                            <asp:Label ID="lblCodDocumento" runat="server" Text="Cod Documento:"/>
                        </td>
                        <td>
                        <asp:TextBox ID="txtCodDoc" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                    <td>
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre Solicitante:"/>
                        </td>
                        <td>
                         <asp:TextBox ID="txtNombre" runat="server" />
                        </td>
                         <td>
                            <asp:Label ID="lblApellido" runat="server" Text="Apellido Solicitante:"/>
                        </td>
                        <td>
                         <asp:TextBox ID="txtApellido" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad:"/>
                        </td>
                        <td>
                           <telerik:RadComboBox ID="ddlPrioridad" DataValueField="Id"
                                                 DataTextField="NombreValor"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="160px"/>
                                                 </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                                    onclick="btnBuscar_Click"/>
                                <asp:Button ID="btnClear" runat="server"
                                    Text="Limpiar" onclick="btnClear_Click" />
                            </td>
                    </tr>
                </table>
                <br />
            </fieldset>
        </asp:Panel>
    </div>
    <br />
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
            var ventanaDetalle = "Modulos/Estructura/AnularSuspenderCasoMaestroDetalle.aspx?IdMenu=" + idMenu + "&";
            function resizeRadGridWithScroll(sender, args) {
                resizeRadGrid("<%= RadGridMaster.ClientID %>");
            }
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
                     onsortcommand="RadGridMaster_SortCommand" CssClass="MyCustomScrollImage"
                     onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                     onitemcommand="RadGridMaster_ItemCommand" Culture="es-ES"  OnItemDataBound="RadGridMaster_ItemDataBound">
        <ClientSettings EnableRowHoverStyle="true" >
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <ClientEvents OnGridCreated="resizeRadGridWithScroll" />
        </ClientSettings>
        <MasterTableView TableLayout="Fixed" CommandItemDisplay="Top"  NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado" width="100%">
            <CommandItemSettings ExportToPdfText="Export to PDF" ></CommandItemSettings>
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="Id"   
                    FilterControlAltText="Filtrar columna Id" DataFormatString="{0:N0}" 
                    HeaderText="Caso" UniqueName="Id" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Cedula" 
                    FilterControlAltText="Filtrar columna Cedula"  
                    HeaderText="Numero De Documento" UniqueName="Cedula"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreCompletoSolicitante" 
                    FilterControlAltText="Filter column NombreCompletoSolicitante" 
                    FilterListOptions="VaryByDataTypeAllowCustom" 
                    HeaderText="Nombre Del Solicitante" UniqueName="NombreCompletoSolicitante">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreServicioSuscriptor" 
                    FilterControlAltText="Filtrar columna NombreServicioSuscriptor" 
                    DataFormatString="{0:N0}" HeaderText="Servicio" 
                    UniqueName="NombreServicioSuscriptor"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreEstatusCaso" 
                    FilterControlAltText="Filtrar columna NombreEstatusCaso" 
                    DataFormatString="{0:N0}" HeaderText="Estatus" UniqueName="NombreEstatusCaso"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Temp" UniqueName="Temp" >  
                  <ItemTemplate> 
                      <telerik:RadComboBox ID="RadComboBox1" runat="server" DataValueField="Id"
                       DataTextField="NombreValor" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" AutoPostBack="true">
                      </telerik:RadComboBox>
                  </ItemTemplate> 
                </telerik:GridTemplateColumn> 


                <telerik:GridBoundColumn DataField="PrioridadAtencion" 
                    FilterControlAltText="Filter PrioridadAtencion column" 
                    HeaderText="PrioridadAtencion" UniqueName="PrioridadAtencion" Visible="False">
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
                            <telerik:RadToolBar ID="RadToolBar1"  OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked" runat="server"  onbuttonclick="RadToolBar1_ButtonClick1" >
                                <Items>
                                    <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar"/>
                                    <telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro"/>
                                    <telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle"/>
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