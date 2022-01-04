<%@ Page Title="Configuracion XML - Pasos" Language="C#" MasterPageFile="~/Master/Site.Master"
    AutoEventWireup="true" CodeBehind="ConfiguracionPasosVinculaciones.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.ConfiguracionPasosVinculaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
            <!--

            //            var nombreVentana = '<%=RadWindow1.ClientID %>';
            //            var nombreGrid = '<%=RadGridParametros.ClientID%>';
            var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
            var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
            var idMaster = "<%= idMaster %>";
            var AccionAgregar = "<%= AccionAgregar %>";
            var AccionModificar = "<%= AccionModificar %>";
            var AccionVer = "<%= AccionVer %>";
            var idMenu = '<%= IdMenuEncriptado %>';


            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function PasosRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function gridCreated(sender, args) {
                if (sender.get_editIndexes && sender.get_editIndexes().length > 0) {
                    // document.getElementById("OutPut").innerHTML = sender.get_editIndexes().join();
                }
                else {
                    //document.getElementById("OutPut").innerHTML = "";
                }
            }


            function onSelectedIndexChanged(sender, eventArgs) {
                var selectedItem = eventArgs.get_item();
                var divPaso = document.getElementById('divComboPaso');
                if (eventArgs.get_item()._control._value == "3")
                    divPaso.style.display = "block";
                else
                    divPaso.style.display = "none";
            }



            function disableAll() {

                var isChecked = !isChecked;
                var masterTable = $find('<%= RadGridParametros.ClientID %>').get_masterTableView();
                var checkboxes = masterTable.get_element().getElementsByTagName("INPUT");
                var valor = false;
                var index;

                var checkbox = document.getElementsByTagName("input");
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].id.indexOf("CheckBox2") != -1) {
                        valor = checkbox[i].checked;
                        break;
                    }
                }
                for (index = 0; index < checkboxes.length; index++) {
                    if (checkboxes[index].id.indexOf("CheckBox1") != -1) {
                        checkboxes[index].checked = valor;
                    }
                }
            }

            function onToolBarClientButtonClicking(sender, args) {
                var button = args.get_item();
                if (button.get_commandName() == "DeleteSelected") {
                    args.set_cancel(!confirm('Delete all selected customers?'));
                }
            }

            function _PanelBarItemClicked(sender, args) {

                grid = $find(sender.get_parent().ClientID);
                var selectedIndexes = grid._selectedIndexes;
                if (selectedIndexes.length == 0) {
                    radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
                    return false;
                }
                else {
                    var wnd = GetRadWindow();
                    var gridVinculaciones = $telerik.findControl(theForm, 'RadGridVinculaciones').get_masterTableView();
                    gridVinculaciones.rebind();
                    wnd.show();
                    return true;
                }
            }


            function onTabSelected(sender, args) {

                switch (args.get_tab().get_text()) {
                    case "PARAMETROS":
                        nombreGrid = '<%=RadGridParametros.ClientID%>';
                        break;
                    case "VINCULACIONES":
                        nombreGrid = '<%=RadGridVinculaciones.ClientID%>';
                        break;
                    default:

                }
                alert(nombreGrid);
                return false;
            }


            function GetSelectedNames(sender, args) {
                grid = $find(sender.get_id());
                var selectedIndexes = grid._selectedIndexes
                var hfIdPaso = document.getElementById('cphBody_hfIdPaso');
                hfIdPaso.value = grid.get_masterTableView()._getRowByIndexOrItemIndexHierarchical(args.get_itemIndexHierarchical()).children[0].innerText;
                nombreGrid = grid.ClientID;
                valorAtributo = hfIdPaso.value;
                return false;
            }
            var nombreGrid = "";
            var valorAtributo = "";

            window.onload = function () {
                changeTextRadAlert();
                nombreGrid = '<%=RadGridParametros.ClientID%>';
            }
            
        </script>
    </telerik:RadCodeBlock>
    <div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
        <asp:HiddenField ID="hfIdPaso" runat="server" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridVinculaciones">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridVinculaciones" />
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
                        <telerik:AjaxUpdatedControl ControlID="RadGridVinculaciones" />
                        <telerik:AjaxUpdatedControl ControlID="RadFilterMaster" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridVinculaciones" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <br />
        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2"
            SelectedIndex="0">
            <Tabs>
                <telerik:RadTab PageViewID="RadPageView1" Text="PARAMETROS" Selected="True">
                </telerik:RadTab>
                <telerik:RadTab PageViewID="RadPageView2" Text="VINCULACIONES">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage2" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="RadPageView1" runat="server">
                <telerik:RadGrid ID="RadGridParametros" runat="server" AutoGenerateColumns="False"
                    Width="100%" CellSpacing="0" GridLines="None" AllowPaging="True" OnItemDataBound="RadGridParametros_ItemDataBound"
                    AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridParametros_PageIndexChanged"
                    OnSortCommand="RadGridParametros_SortCommand" OnPageSizeChanged="RadGridParametros_PageSizeChanged"
                    OnNeedDataSource="RadGridParametros_NeedDataSource" OnItemCommand="RadGridParametros_ItemCommand"
                    OnInsertCommand="RadGridParametros_InsertCommand" AllowAutomaticInserts="True">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="gridCreated" OnRowDblClick="RowDblClick"></ClientEvents>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <ClientEvents OnRowSelected="GetSelectedNames" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"
                        EditMode="InPlace">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <%--<telerik:GridBoundColumn DataField="NOMBRE" FilterControlAltText="Filtrar columna NOMBRE"
                                HeaderStyle-Width="150px" DataFormatString="{0:N0}" HeaderText="NOMBRE" UniqueName="NOMBRE">
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn UniqueName="NOMBRE" HeaderText="NOMBRE" HeaderStyle-Width="150px">
                                <HeaderStyle Width="210px" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDestino" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <%--<asp:TextBox runat="server" ID="txtInsert" Text='<%# Bind("NOMBRE") %>'></asp:TextBox>--%>
                                    <telerik:RadComboBox ID="rcbParametros" runat="server" Width="200px" Height="150px"
                                        EmptyMessage="Seleccione Parámetro">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Visible" HeaderText="Visible" HeaderStyle-Width="120px">
                                <HeaderStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Label1" Text='<%# Bind("AMBITO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="rcbAmbito" runat="server" AppendDataBoundItems="true"
                                        EmptyMessage="Seleccione Ambito" Width="130px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="AMBOS" Value="AMBOS" />
                                            <telerik:RadComboBoxItem Text="ENTRADA" Value="ENTRADA" />
                                            <telerik:RadComboBoxItem Text="SALIDA" Value="SALIDA" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderText="Editar"
                                ButtonType="ImageButton" />
                        </Columns>
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle AlwaysVisible="True" />
                        <CommandItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="left">
                                        GENERALES
                                    </td>
                                    <td align="right">
                                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="ClientButtonClicking"
                                            OnClientButtonClicked="PanelBarItemClicked" OnButtonClick="RadToolBar1_ButtonClick1">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" CommandName="InitInsert" Text="Agregar"
                                                    ImagePosition="Right" ImageUrl="~/Imagenes/AddRecord.gif" Owner="">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right"
                                                    ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" meta:resourcekey="RadToolBarButtonResource6"
                                                    Owner="">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right"
                                                    ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" meta:resourcekey="RadToolBarButtonResource1"
                                                    Owner="">
                                                </telerik:RadToolBarButton>
                                            </Items>
                                        </telerik:RadToolBar>
                                    </td>
                                </tr>
                            </table>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnRowDblClick="RowDblClick" OnGridCreated="gridCreated" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="True" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server">
                <telerik:RadGrid ID="RadGridVinculaciones" runat="server" AutoGenerateColumns="False"
                    Width="100%" CellSpacing="0" GridLines="None" AllowPaging="True" OnItemDataBound="RadGridVinculaciones_ItemDataBound"
                    AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridVinculaciones_PageIndexChanged"
                    OnSortCommand="RadGridVinculaciones_SortCommand" OnPageSizeChanged="RadGridVinculaciones_PageSizeChanged"
                    OnNeedDataSource="RadGridVinculaciones_NeedDataSource" OnItemCommand="RadGridVinculaciones_ItemCommand"
                    OnInsertCommand="RadGridVinculaciones_InsertCommand" AllowAutomaticInserts="True"
                    Culture="es-ES">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="gridCreated" OnRowDblClick="RowDblClick"></ClientEvents>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <ClientEvents OnRowSelected="GetSelectedNames" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros"
                        EditMode="InPlace">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filtrar columna Nombre"
                                HeaderStyle-Width="50px" DataFormatString="{0:N0}" HeaderText="Nombre" UniqueName="Nombre">
                                <HeaderStyle Width="50px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Origen" HeaderText="Origen" HeaderStyle-Width="150px">
                                <HeaderStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOrigen" Text='<%# Bind("ORIGEN") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="RadComboBox1" runat="server" AppendDataBoundItems="true"
                                        OnClientSelectedIndexChanged="onSelectedIndexChanged" Width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="PARAMETROS" Value="1" />
                                            <telerik:RadComboBoxItem Text="SOLICITUD" Value="2" />
                                            <telerik:RadComboBoxItem Text="PASO" Value="3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <div id="divComboPaso" style="display: none">
                                        <telerik:RadComboBox ID="rcbIdPasos" runat="server" Width="200px" Height="150px"
                                            EmptyMessage="Seleccione IdPaso">
                                        </telerik:RadComboBox>
                                        <%--                                        <telerik:RadComboBox ID="rcbIdPasos" runat="server" Width="200px" Height="150px"
                                            EmptyMessage="Seleccione IdPaso" EnableLoadOnDemand="true" ShowMoreResultsBox="true"
                                            EnableVirtualScrolling="true">
                                            <WebServiceSettings Method="ListaIdPasos" Path="ConfiguracionPasosVinculaciones.aspx" />
                                        </telerik:RadComboBox>
                                        --%>
                                    </div>
                                    <telerik:RadComboBox ID="rcbParametrosOrigen" runat="server" Width="200px" Height="150px"
                                        EmptyMessage="Seleccione Parámetro">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Destino" HeaderText="Destino" HeaderStyle-Width="150px">
                                <HeaderStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDestino" Text='<%# Bind("Destino") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="rcbParametrosDestino" runat="server" Width="200px" Height="150px"
                                        EmptyMessage="Seleccione Parámetro">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderText="Editar"
                                HeaderStyle-Width="100px" ButtonType="ImageButton" />
                        </Columns>
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle AlwaysVisible="True" />
                        <CommandItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="right">
                                        <telerik:RadToolBar ID="RadToolBar3" runat="server" OnClientButtonClicking="ClientButtonClicking"
                                            OnClientButtonClicked="PanelBarItemClicked" OnButtonClick="RadToolBar3_ButtonClick1">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" CommandName="InitInsert" Text="Agregar"
                                                    ImagePosition="Right" ImageUrl="~/Imagenes/AddRecord.gif" Owner="">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right"
                                                    ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" meta:resourcekey="RadToolBarButtonResource6"
                                                    Owner="">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right"
                                                    ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" meta:resourcekey="RadToolBarButtonResource1"
                                                    Owner="">
                                                </telerik:RadToolBarButton>
                                            </Items>
                                        </telerik:RadToolBar>
                                    </td>
                                </tr>
                            </table>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnRowDblClick="RowDblClick" OnGridCreated="gridCreated" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="True" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" DestroyOnClose="True"
            Title="Filtro" Width="600px" KeepInScreenBounds="True">
            <ContentTemplate>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblBusquedaAvanzada" runat="server" Text="Búsqueda Avanzada" Font-Bold="True"
                            meta:resourcekey="lblBusquedaAvanzadaResource1"></asp:Label>
                    </legend>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster"
                                    OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 "
                                    Culture="es-ES">
                                </telerik:RadFilter>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblMessege" runat="server" meta:resourcekey="LblMessegeResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif"
                                    OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" meta:resourcekey="ApplyButtonResource1" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
            <script src="../../Scripts/ListaRadScriptBlock1.js" type="text/javascript"></script>
        </telerik:RadCodeBlock>
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="cmdGuardar_Click"
            meta:resourcekey="cmdGuardarResource1" />
        <asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Continuar" OnClick="cmdGuardaryAgregar_Click"
            meta:resourcekey="cmdGuardaryAgregarResource1" />
        <asp:Button ID="cmdcancelar" runat="server" Text="Cancelar" OnClick="CmdcancelarClick"
            CausesValidation="false" ValidationGroup="Cancelar" />
    </div>
</asp:Content>
