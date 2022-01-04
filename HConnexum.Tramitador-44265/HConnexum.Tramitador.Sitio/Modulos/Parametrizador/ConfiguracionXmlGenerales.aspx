<%@ Page Title="Configuracion XML" Language="C#" MasterPageFile="~/Master/Site.Master"
    AutoEventWireup="true" CodeBehind="ConfiguracionXmlGenerales.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.ConfiguracionXmlGenerales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
         //<![CDATA[


            //var nombreVentana = '<%=RadWindow1.ClientID %>';
            // var nombreGrid = '<%=RadGridMaster.ClientID%>';
            var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
            var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
            var idMaster = "<%= idMaster %>";
            var AccionAgregar = "<%= AccionAgregar %>";
            var AccionModificar = "<%= AccionModificar %>";
            var AccionVer = "<%= AccionVer %>";
            var idMenu = '<%= IdMenuEncriptado %>';
            var idFlujoServicio = '<%= Id %>';



            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function PasosRowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }

            function gridCreated(sender, args) {
                if (sender.get_editIndexes && sender.get_editIndexes().length > 0) {
                }
                else {
                }
            }

            function disableAll() {

                var isChecked = !isChecked;
                var masterTable = $find('<%= RadGridMaster.ClientID %>').get_masterTableView();
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

            function _ClientButtonClicking(sender, args) {
                grid = $find(sender.get_parent().ClientID);
                var selectedIndexes = grid._selectedIndexes;
                if (selectedIndexes.length == 0) {
                    radalert("Seleccione el registro a eliminar", 280, 120, "Eliminar registro")
                }
                else {
                    lastClickedItem = args.get_item();
                    radconfirm("¿Seguro de eliminar el registro?", confirmCallbackFunction, 380, 50, null, "Eliminar registro");
                }
                return false;
            }

            function _PanelBarItemClicked(sender, args) {

                var wnd = GetRadWindow();

                switch (args.get_item().get_commandName()) {
                    case "parametrosvinculaciones":
                        grid = $find(sender.get_parent().ClientID);
                        var defaulSelectedIndexesPV = grid._selectedIndexes;
                        if (defaulSelectedIndexesPV.length == 0) {
                            radalert("Seleccione el registro a mostrar", 380, 50, "Ver  detalles del Paso")
                        }
                        else {
                            $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
                            wnd.setUrl(ventanaDetalle + "IdMenu=" + idMenu + "&accion=" + AccionModificar);

                        }
                        break;
                    case "InitInsert":
                        return false;
                        break;
                    case "Eliminar":
                        break;
                    case "Refrescar":
                        break;
                    default:

                }
            }


            function onTabSelected(sender, args) {

                switch (args.get_tab().get_text()) {
                    case "GENERALES":
                        nombreGrid = '<%=RadGridMaster.ClientID%>';
                        break;
                    case "PASOS":
                        nombreGrid = '<%=RadGridPasos.ClientID%>';
                        break;
                    default:

                }
                //alert(nombreGrid);
                return false;
            }

            function GetSelectedNames(sender, args) {
                grid = $find(sender.get_id());
                var selectedIndexes = grid._selectedIndexes
                var hfIdPaso = document.getElementById('cphBody_hfIdPaso');
                hfIdPaso.value = grid.get_masterTableView()._getRowByIndexOrItemIndexHierarchical(args.get_itemIndexHierarchical()).children[0].innerText;
                ventanaDetalle = "Modulos/Parametrizador/ConfiguracionPasosVinculaciones.aspx?IdFS=" + idFlujoServicio + "&IdPaso=" + hfIdPaso.value + "&";
                nombreGrid = grid.ClientID;
                valorAtributo = hfIdPaso.value;

                return false;
            }
            var nombreGrid = "";
            var ventanaDetalle = "";
            var valorAtributo = "";



            window.onload = function () {
                changeTextRadAlert();
                nombreGrid = '<%=RadGridMaster.ClientID%>';

            }
               //]]>
        </script>
    </telerik:RadCodeBlock>
    <div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
        <asp:HiddenField ID="hfIdPaso" runat="server" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridMaster">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridPasos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridPasos" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
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
                        <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                        <telerik:AjaxUpdatedControl ControlID="RadGridPasos" />
                        <telerik:AjaxUpdatedControl ControlID="RadFilterMaster" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                        <telerik:AjaxUpdatedControl ControlID="RadGridPasos" />
                        <telerik:AjaxUpdatedControl ControlID="RadGridVinculaciones" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <br />
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
            OnClientTabSelected="onTabSelected" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab PageViewID="PageView1" Text="GENERALES" Selected="True">
                </telerik:RadTab>
                <telerik:RadTab PageViewID="PageView2" Text="PASOS">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="PageView1" runat="server">
                <telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                    CellSpacing="0" GridLines="None" AllowPaging="True" OnItemDataBound="RadGridMaster_ItemDataBound"
                    AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged" OnSortCommand="RadGridMaster_SortCommand"
                    OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                    OnItemCommand="RadGridMaster_ItemCommand" OnInsertCommand="RadGridMaster_InsertCommand"
                    AllowAutomaticInserts="True" Culture="es-ES">
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
                                HeaderStyle-Width="150px" DataFormatString="{0:N0}" HeaderText="Nombre" UniqueName="Nombre">
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Visible" HeaderText="Visible"
                                HeaderStyle-Width="50px">
                                <HeaderStyle Width="70px" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Label1" Text='<%# Bind("Visible") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="RadComboBox1" runat="server" AppendDataBoundItems="true"
                                        Width="50px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="SI" Value="SI" />
                                            <telerik:RadComboBoxItem Text="NO" Value="NO" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Etiqueta" FilterControlAltText="Filtrar columna Etiqueta"
                                HeaderStyle-Width="200px" DataFormatString="{0:N0}" HeaderText="Etiqueta" UniqueName="Etiqueta">
                                <HeaderStyle Width="200px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Orden" FilterControlAltText="Filtrar columna Orden"
                                HeaderStyle-Width="90px" HeaderText="Orden" UniqueName="Orden">
                                <HeaderStyle Width="90px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Lista" FilterControlAltText="Filter columna Lista"
                                HeaderStyle-Width="160px" HeaderText="Lista" UniqueName="Lista" DataFormatString="{0:d}">
                                <HeaderStyle Width="160px"></HeaderStyle>
                            </telerik:GridBoundColumn>                               
                            <telerik:GridCheckBoxColumn UniqueName="CrearSolicitud" DataField="CrearSolicitud"
                                HeaderText="CrearSolicitud" HeaderStyle-Width="100px" ColumnEditorID="GridCheckBoxColumnEditor1">
                                <HeaderStyle Width="100px" />
                            </telerik:GridCheckBoxColumn>
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
                    </MasterTableView><ClientSettings>
                        <ClientEvents OnRowDblClick="RowDblClick" OnGridCreated="gridCreated" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="True" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView2" runat="server">
                <telerik:RadGrid ID="RadGridPasos" runat="server" AutoGenerateColumns="False" Width="100%"
                    CellSpacing="0" GridLines="None" AllowPaging="True" OnItemDataBound="RadGridPasos_ItemDataBound"
                    AllowSorting="True" OnPageIndexChanged="RadGridPasos_PageIndexChanged" OnSortCommand="RadGridPasos_SortCommand"
                    OnPageSizeChanged="RadGridPasos_PageSizeChanged" OnNeedDataSource="RadGridPasos_NeedDataSource"
                    OnItemCommand="RadGridPasos_ItemCommand" OnInsertCommand="RadGridPasos_InsertCommand"
                    AllowAutomaticInserts="True" Culture="es-ES">
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
                            <telerik:GridBoundColumn DataField="ID" FilterControlAltText="Filtrar columna ID"
                                HeaderStyle-Width="50px" HeaderText="ID" UniqueName="ID">
                                <HeaderStyle Width="50px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOMBRE" FilterControlAltText="Filtrar columna Nombre"
                                HeaderStyle-Width="150px" DataFormatString="{0:N0}" HeaderText="Nombre" UniqueName="Nombre">
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ORDEN" FilterControlAltText="Filtrar columna ORDEN"
                                HeaderStyle-Width="10px" DataFormatString="{0:N0}" HeaderText="ORDEN" UniqueName="ORDEN">
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOMBRETIPOPASO" FilterControlAltText="Filtrar columna NombreTipoPaso"
                                HeaderStyle-Width="150px" DataFormatString="{0:N0}" HeaderText="Tipo paso" UniqueName="NombreTipoPaso">
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" HeaderText="Editar"
                                HeaderStyle-Width="50px" ButtonType="ImageButton" />
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
                                        PASOS
                                    </td>
                                    <td align="right">
                                        <telerik:RadToolBar ID="RadToolBar2" runat="server" OnClientButtonClicking="ClientButtonClicking"
                                            OnClientButtonClicked="PanelBarItemClicked" OnButtonClick="RadToolBar1_ButtonClick1">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" CommandName="Refrescar" Text="Refrescar"
                                                    ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" meta:resourcekey="RadToolBarButtonResource1"
                                                    Owner="">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server" CommandName="InitInsert" Text="Agregar"
                                                    ImagePosition="Right" ImageUrl="~/Imagenes/AddRecord.gif" Owner="">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right"
                                                    ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" meta:resourcekey="RadToolBarButtonResource6"
                                                    Owner="">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server" PostBack="True" CommandName="parametrosvinculaciones"
                                                    Text="Parámetros y Vinculaciones" ImagePosition="Right" ImageUrl="~/Imagenes/Edit.gif"
                                                    meta:resourcekey="RadToolBarButtonResource5" Owner="">
                                                </telerik:RadToolBarButton>
                                            </Items>
                                        </telerik:RadToolBar>
                                    </td>
                                </tr>
                            </table>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnRowDblClick="PasosRowDblClick" OnGridCreated="gridCreated" />
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
