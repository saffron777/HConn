<%@ Page Title="Detalle de Etapa" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="True" CodeBehind="EtapaMaestroDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.EtapaMaestroDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/HorasMinutosSegundos.ascx" TagName="HorasMinutosSegundos" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
        <telerik:RadAjaxManager id="RadAjaxManager1" 
                                DefaultLoadingPanelID="RadAjaxLoadingPanel1" 
                                onajaxrequest="RadAjaxManager1_AjaxRequest" runat="server" 
                                meta:resourcekey="RadAjaxManager1Resource1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridDetails">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelMaster" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridMaster">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                        <telerik:AjaxUpdatedControl ControlID="PanelMaster" />
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
            <asp:Panel ID="PanelMaster" runat="server" 
                       meta:resourcekey="PanelMasterResource1">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblEtapa" runat="server" Text="Etapa" Font-Bold="True" 
                                   meta:resourcekey="lblEtapaResource1"></asp:Label>
                    </legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdFlujoServicio" runat="server" Text="Flujo Servicio:" 
                                           meta:resourcekey="lblIdFlujoServicioResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlIdFlujoServicio"  DataValueField="Id"  
                                                     DataTextField="NombreServicioSuscriptor"  ErrorMessage="Campo Obligatorio" 
                                                     runat="server" Enabled="False" Culture="es-ES" Width="280"
                                                     meta:resourcekey="ddlIdFlujoServicioResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                                           meta:resourcekey="lblNombreResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" Width="280"
                                             meta:resourcekey="txtNombreResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblOrden" runat="server" Text="Orden:" 
                                           meta:resourcekey="lblOrdenResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrden" runat="server" 
                                             meta:resourcekey="txtOrdenResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSlaPromedio" runat="server" Text="Sla Promedio:" 
                                           meta:resourcekey="lblSlaPromedioResource1"/>
                            </td>
                            <td>
                                <hcc:HorasMinutosSegundos runat="server" ID="txtSlaPromedio" Enabled="false" IsRequired="False"  Width="200"  />
                            </td>
                        </tr>
                        <tr> 
                            <td>
                                <asp:Label ID="lblSlaTolerancia" runat="server" Text="Sla Tolerancia:" 
                                           meta:resourcekey="lblSlaToleranciaResource1"/>
                            </td>
                            <td>
                                <hcc:HorasMinutosSegundos runat="server" ID="txtSlaTolerancia" IsRequired="False"  Width="200"  />

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIndObligatorio" runat="server" Text="Ind Obligatorio:" 
                                           meta:resourcekey="lblIndObligatorioResource1"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIndObligatorio" runat="server" 
                                              meta:resourcekey="chkIndObligatorioResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIndRepeticion" runat="server" Text="Ind Repeticion:" 
                                           meta:resourcekey="lblIndRepeticionResource1"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIndRepeticion" runat="server" 
                                              meta:resourcekey="chkIndRepeticionResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIndSeguimiento" runat="server" Text="Ind Seguimiento:" 
                                           meta:resourcekey="lblIndSeguimientoResource1"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIndSeguimiento" runat="server" 
                                              meta:resourcekey="chkIndSeguimientoResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIndInicioServ" runat="server" Text="Ind Inicio Serv:" 
                                           meta:resourcekey="lblIndInicioServResource1"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIndInicioServ" runat="server" 
                                              meta:resourcekey="chkIndInicioServResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIndCierre" runat="server" Text="Ind Cierre:" 
                                           meta:resourcekey="lblIndCierreResource1"/>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkIndCierre" runat="server" 
                                              meta:resourcekey="chkIndCierreResource1"/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <hcc:Publicacion id="Publicacion" runat="server"/>
                <hcc:Auditoria id="Auditoria" runat="server"/>
            </asp:Panel>
            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdFlujoServicio" EmptyMessage="Escriba IdFlujoServicio" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdFlujoServicio"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:TextBoxSetting   BehaviorID="BehaviorNombre" EmptyMessage="Escriba Nombre"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtNombre"/>
                    </TargetControls>
                </telerik:TextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorOrden" EmptyMessage="Escriba Orden" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtOrden"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
            </telerik:RadInputManager>
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
                    var ventanaDetalle = "Modulos/Parametrizador/PasoMaestroDetalle.aspx?IdMenu=" + idMenu + "&";
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
                <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado" width="100%">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="NombreTipoPaso" 
                                                 FilterControlAltText="Filtrar columna IdTipoPaso" DataFormatString="{0:N0}" 
                                                 HeaderText="Tipo Paso" UniqueName="IdTipoPaso" 
                                                 meta:resourcekey="GridBoundColumnResource1"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Nombre" 
                                                 FilterControlAltText="Filtrar columna Nombre"  HeaderText="Nombre" 
                                                 UniqueName="Nombre" meta:resourcekey="GridBoundColumnResource2"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Observacion" 
                                                 FilterControlAltText="Filtrar columna Observacion"  HeaderText="Observación" 
                                                 UniqueName="Observacion" meta:resourcekey="GridBoundColumnResource3"></telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IndObligatorio" 
                                                    FilterControlAltText="Filter column1 column" HeaderText="Ind Obligatorio" 
                                                    UniqueName="column1" 
                                                    meta:resourcekey="GridCheckBoxColumnResource1">
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="Orden" 
                                                 FilterControlAltText="Filtrar columna Orden"  HeaderText="Orden" 
                                                 UniqueName="Orden" meta:resourcekey="GridBoundColumnResource5"></telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IndVigente" 
                                                    FilterControlAltText="Filtrar columna IndVigente"  HeaderText="Publicar" 
                                                    UniqueName="IndVigente" meta:resourcekey="GridCheckBoxColumnResource2"></telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="FechaValidez" DataFormatString="{0:d}"
                                                 FilterControlAltText="Filtrar columna IndVigente"  HeaderText="FechaValidez" 
                                                 UniqueName="IndVigente" 
                                                 meta:resourcekey="GridBoundColumnResource4"></telerik:GridBoundColumn>
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
            <br />
            <br />
            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                        onclick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1"  />
            <asp:Button ID="cmdGuardaryAgregar" runat="server" 
                        Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click" 
                        meta:resourcekey="cmdGuardaryAgregarResource1"  />
            <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar"  
                        onclientclick="$('form').clearForm();

                        return false" 
                        meta:resourcekey="cmdLimpiarResource1"  />
        </div>
    </div>
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
        <script type="text/javascript">
            function IrAnterior() {
                var wnd = GetRadWindow();
                wnd.setUrl('<%= RutaPadreEncriptada %>');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
