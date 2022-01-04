<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdmNoticias.ascx.cs" Inherits="Noticias.AdmNoticias" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<style type="text/css">
    .auto-style1 {
        width: 100%;
    }
</style>

<script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        clearControls();
        var _panelAdmin = document.getElementById("<%= PanelAdmin.ClientID %>");
        var _panelPublicar = document.getElementById("<%= PanelPublicacion.ClientID %>");
        if ($telerik.findControl(theForm, "RadtbModo").get_value() == "" || $telerik.findControl(theForm, "<%= RadtbModo.ClientID%>").get_value() == "Nuevo") {
            _panelAdmin.style.display = "block";
            _panelPublicar.style.display = "none";
        }
        else if ($telerik.findControl(theForm, "<%= RadtbModo.ClientID%>").get_value() == "Editar") {
            _panelAdmin.style.display = "none";
            _panelPublicar.style.display = "block";
        }
    });

    function NuevaPublicacion() {
        $telerik.findControl(theForm, "<%= RadtbModo.ClientID%>").set_value("Nuevo");
        clearControls();
        var _panelAdmin = document.getElementById("<%= PanelAdmin.ClientID %>");
        var _panelPublicar = document.getElementById("<%= PanelPublicacion.ClientID %>");
        _panelAdmin.style.display = "none";
        _panelPublicar.style.display = "block";
        var ctrlTxtTitulo = document.getElementById("<%= txtTitulo.ClientID %>");
        ctrlTxtTitulo._emptyMessage = 'Empty';
    }

    function EditarPublicacion(selectedIndexes) {
        var valorFechaPublicacion;
        var valorFechaVencimiento;
        var valorDescripcionHTML;
        var IdNoticiaSeleccionada;
        var valorTitulo;

        var _panelAdmin = document.getElementById("<%= PanelAdmin.ClientID %>");
        var _panelPublicar = document.getElementById("<%= PanelPublicacion.ClientID %>");
        var ctrlTxtTitulo = $("#<%= txtTitulo.ClientID %>");
        var gridTemp = $find("<%= radGridTemporal.ClientID %>");
        var LstDisponibles = $find("<%= rlbTipoSuscriptorDisp.ClientID %>");
        var LstAsociadas = $find("<%= rlbTipoSuscriptorAsoc.ClientID %>");
        var LstRedes = $find("<%= rlbRedes.ClientID %>");
        var ImagenPequenga = $find("<%= ImgThumb.ClientID %>");
        $telerik.findControl(theForm, "<%= RadtbModo.ClientID%>").set_value("Editar");

        LstAsociadas.get_items().clear();
        LstRedes.get_items().clear();
        clearControls();

        IdNoticiaSeleccionada = grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("Id").innerHTML;
        valorTitulo = grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("Titulo").innerHTML;

        if (grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("FechaPublicacion") == null)
            valorFechaPublicacion = "";
        else
            valorFechaPublicacion = grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("FechaPublicacion").innerHTML.substring(0, 10);

        if (grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("FechaVencimiento") == null)
            valorFechaVencimiento = "";
        else
            valorFechaVencimiento = grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("FechaVencimiento").innerHTML.substring(0, 10);

        valorDescripcionHTML = grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("Descripcion").innerHTML;

        var htmlsrc = grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("thumb_image").children[0].src;
        var ctrlImage = document.getElementById("<%= ImgThumb.ClientID %>");
        ctrlImage.src = htmlsrc;

        $telerik.findControl(theForm, "<%= txtId.ClientID%>").set_value(IdNoticiaSeleccionada);
        ctrlTxtTitulo.val(valorTitulo);
        $telerik.findControl(theForm, "<%= rdpFechaPublicacion.ClientID%>")._dateInput.set_value(valorFechaPublicacion);
        $telerik.findControl(theForm, "<%= rdpFechaVencimiento.ClientID%>")._dateInput.set_value(valorFechaVencimiento);
        $telerik.findControl(theForm, "<%= RadEditorNoticias.ClientID%>")._contentArea.innerHTML = valorDescripcionHTML;

        _panelAdmin.style.display = "none";
        _panelPublicar.style.display = "block";

        var _txtLista = document.getElementById("<%= txtLista.ClientID%>");
        _txtLista.value = ""; var _valores;

        for (i = 1; i < gridTemp.MasterTableView.get_element().rows.length; i++) {
            var iIdNoticia = gridTemp.MasterTableView.get_element().rows[i].cells[2].innerHTML;
            if (IdNoticiaSeleccionada == iIdNoticia) {
                var idTipoSuscriptor = gridTemp.MasterTableView.get_element().rows[i].cells[0].innerHTML;
                var sNombreSuscriptor = gridTemp.MasterTableView.get_element().rows[i].cells[1].innerHTML;
                var item = new Telerik.Web.UI.RadListBoxItem();
                if (idTipoSuscriptor != 0) {
                    for (var j = 0; j < LstDisponibles.get_items().get_count() ; j++) {
                        if (idTipoSuscriptor == LstDisponibles.get_items().getItem(j).get_value()) {
                            item.set_text(LstDisponibles.get_items().getItem(j).get_text());
                            item.set_value(LstDisponibles.get_items().getItem(j).get_value());
                            LstAsociadas.get_items().add(item);
                            _valores = LstDisponibles.get_items().getItem(j).get_value() + "|";
                            _txtLista.value = _txtLista.value + _valores;
                            LstRedes.get_items().add(LstDisponibles.get_items().getItem(j));
                            break;
                        }
                    }
                }
                else {
                    item.set_text(sNombreSuscriptor);
                    item.set_value(idTipoSuscriptor);
                    LstRedes.get_items().add(item);
                    _txtLista.value = "0|";
                }
            }
        }
        _txtLista.value = _txtLista.value.substring(0, _txtLista.value.length - 1);
    }

    function editarRegistro(sender, args) {
        grid = $find("<%= RadGridMaster.ClientID %>");
        var selectedIndexes = grid._selectedIndexes;
        EditarPublicacion(selectedIndexes);
    }

    function confirmCallbackFunction(args) {
        if (args) {
            grid = $find("<%= RadGridMaster.ClientID %>");
            var selectedIndexes = grid._selectedIndexes;
            var IdNoticiaSeleccionada = grid.MasterTableView.get_selectedItems(selectedIndexes)[0].get_cell("Id").innerHTML;
            $telerik.findControl(theForm, "<%= txtId.ClientID%>").set_value(IdNoticiaSeleccionada);
            var boton = document.getElementById("<%= EliminarRegistro.ClientID%>");
            boton.click();
        }
    }

    function guardar(args) {
        var boton = document.getElementById("<%= RadGuarda.ClientID%>");
        boton.click();
    }

    function clearControls() {
        document.getElementById("<%= txtId.ClientID %>").value = "";
        var ctrlTxtTitulo = document.getElementById("<%= txtTitulo.ClientID %>");
        ctrlTxtTitulo.value = "";
        $telerik.findControl(theForm, "<%= rdpFechaPublicacion.ClientID%>")._dateInput.set_value("");
        $telerik.findControl(theForm, "<%= rdpFechaVencimiento.ClientID%>")._dateInput.set_value("");
        $telerik.findControl(theForm, "<%= RadEditorNoticias.ClientID%>")._contentArea.innerHTML = "";
    }

    function PanelBarItemClicked(sender, args) {
        grid = $find("<%= RadGridMaster.ClientID %>");
        var selectedIndexes = grid._selectedIndexes;
        clearControls();

        switch (args.get_item().get_commandName()) {
            case "Eliminar":
                if (selectedIndexes.length == 0)
                    radalert("Seleccione el registro a eliminar", 280, 120, "Eliminar registro")
                else
                    radconfirm("¿Seguro de eliminar el registro?", confirmCallbackFunction, 300, 100, null, "Eliminar registro");

                break;

            case "Editar":
                if (selectedIndexes.length == 0)
                    radalert("Seleccione el registro a editar", 280, 120, "Editar registro")
                else
                    EditarPublicacion(selectedIndexes);

                break;
        }
    }

    function AbrirWndRedes(sender, e) {
        var LstRedes = $find("<%= rlbRedes.ClientID %>");
        var _rbRedes = $("#<%= rbRedes.ClientID%>");
        var _rbPublico = $("#<%= rbPublico.ClientID%>");

        if (LstRedes.get_items().get_count() >= 1) {
            if (LstRedes._getAllItems()[0]._text != "Público") {
                _rbRedes[0].checked = true;
                habilitarRedes();
            }
            else {
                _rbPublico[0].checked = true;
                habilitarPublico();
            }
        }
        else {
            _rbPublico[0].checked = true;
            habilitarPublico();
        }

        var oWindow = $find("<%= WndRedes.ClientID %>");
        oWindow.show();
    }

    function ListaRedes(sender, args) {
        args.IsValid = $telerik.findControl(theForm, "<%= rlbRedes.ClientID%>").get_items().get_count() > 0;
    }

    function CerrarVentana(args) {
        var ventana = $find("<%= WndRedes.ClientID %>");
        ventana.Close();
    }

    function Asignar(args) {
        var LstAsociadas = $find("<%= rlbTipoSuscriptorAsoc.ClientID %>");
        var LstRedes = $find("<%= rlbRedes.ClientID %>");
        var _txtLista = document.getElementById("<%= txtLista.ClientID%>");
        _txtLista.value = ""; var _valores;

        LstRedes.get_items().clear();
        var bInsertar;

        for (var j = 0; j < LstAsociadas.get_items().get_count() ; j++) {
            bInsertar = true;
            for (var k = 0; k < LstRedes.get_items().get_count() ; k++) {
                if (LstAsociadas.get_items().getItem(j).get_text() == LstRedes.get_items().getItem(k).get_text()) {
                    bInsertar = false;
                }
            }
            if (bInsertar) {
                var item = new Telerik.Web.UI.RadListBoxItem();
                item.set_text(LstAsociadas.get_items().getItem(j).get_text());
                item.set_value(LstAsociadas.get_items().getItem(j).get_value());
                _valores = LstAsociadas.get_items().getItem(j).get_value() + "|";
                _txtLista.value = _txtLista.value + _valores;
                LstRedes.get_items().add(item);
            }
        }
        _txtLista.value = _txtLista.value.substring(0, _txtLista.value.length - 1);
        CerrarVentana();
    }

    function habilitarRedes() {
        var _rlbTipoSuscriptorDisp = $find("<%= rlbTipoSuscriptorDisp.ClientID%>");
        var _rlbTipoSuscriptorAsoc = $find("<%= rlbTipoSuscriptorAsoc.ClientID%>");
        _rlbTipoSuscriptorDisp.set_enabled(true);
        _rlbTipoSuscriptorAsoc.set_enabled(true);
        if (_rlbTipoSuscriptorAsoc._getAllItems()[0]._text == "Público")
            _rlbTipoSuscriptorAsoc.get_items().clear();
    }

    function habilitarPublico() {
        var _rlbTipoSuscriptorDisp = $find("<%= rlbTipoSuscriptorDisp.ClientID%>");
        var _rlbTipoSuscriptorAsoc = $find("<%= rlbTipoSuscriptorAsoc.ClientID%>");
        _rlbTipoSuscriptorDisp.set_enabled(false)
        _rlbTipoSuscriptorAsoc.set_enabled(false);

        if (_rlbTipoSuscriptorAsoc.get_items().get_count() > 0)
            radconfirm("¿Existen redes asociadas, desea cambiarlo a público?", confirmClearList, 300, 100, null, "Redes");
        else {
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text("Público");
            item.set_value(0);
            _rlbTipoSuscriptorAsoc.get_items().add(item);
        }
    }

    function confirmClearList(arg) {
        var _rbRedes = $("#<%= rbRedes.ClientID%>");
        if (arg) {
            var _rlbTipoSuscriptorAsoc = $find("<%= rlbTipoSuscriptorAsoc.ClientID%>");
            var _rlbTipoSuscriptorDisp = $find("<%= rlbTipoSuscriptorDisp.ClientID%>");
            _rlbTipoSuscriptorDisp.clearSelection();

            //pasar los elementos de la lista Asociados a Disponibles
            while (_rlbTipoSuscriptorAsoc.get_items().get_count() > 0)
                _rlbTipoSuscriptorDisp.get_items().add(_rlbTipoSuscriptorAsoc.get_items()._array[0]);

            //agregar a la lista disponibles el item "Público"
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text("Público");
            item.set_value(0);
            _rlbTipoSuscriptorAsoc.get_items().add(item);
            sortlist();
        }
        else {
            habilitarRedes();
            _rbRedes[0].checked = true;
        }
    }

    function sortlist() {
        var _rlbTipoSuscriptorDisp = $find("<%= rlbTipoSuscriptorDisp.ClientID%>");
        var arrLista = new Array(_rlbTipoSuscriptorDisp.get_items().get_count());

        for (i = 0; i < _rlbTipoSuscriptorDisp.get_items().get_count() ; i++) {
            arrLista[i] = new Array(2);
            arrLista[i][0] = _rlbTipoSuscriptorDisp.get_items()._array[i].get_text();
            arrLista[i][1] = _rlbTipoSuscriptorDisp.get_items()._array[i].get_value();
        }

        arrLista.sort();
        _rlbTipoSuscriptorDisp.get_items().clear()

        for (j = 0; j < arrLista.length; j++) {
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(arrLista[j][0]);
            item.set_value(arrLista[j][1]);
            _rlbTipoSuscriptorDisp.get_items().add(item);
        }
    }

    function SoloNumeros(e) {
        var keynum;
        if (window.event) // IE
            keynum = e.keyCode;
        else if (e.which) // Netscape/Firefox/Opera
            keynum = e.which;
        else if (e.keyCode)
            keynum = e.keyCode;
        var checker = new RegExp("\\d");
        if (keynum == 8 || keynum == 09) {
            return true;
        }
        else {
            return checker.test(String.fromCharCode(keynum));
        }
    }

    function readURL(input) {
        if (input.control._fileInput.files && input.control._fileInput.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#<%= ImgThumb.ClientID%>").attr('src', e.target.result);
            }
            reader.readAsDataURL(input.control._fileInput.files[0]);
            }
        }

        $("#<%= RadUpload1.ClientID%>").change(function () {
        readURL(this);
    });
</script>

<div style="padding: 15px 15px 15px 15px">
    <asp:Panel ID="PanelAdmin" runat="server" Width="100%">
        <table id="TablaAdmin" width="100%">
            <tr>
                <td style="text-align: right">
                    <telerik:radbutton id="btnNueva" runat="server" text="Nueva Publicación" autopostback="false"
                        causesvalidation="False" onclientclicking="NuevaPublicacion" />
                </td>
            </tr>
            <tr>
                <td style="margin-left: 40px">
                    <telerik:radgrid id="RadGridMaster" runat="server" autogeneratecolumns="False" width="100%" height="600px"
                        cellspacing="0" gridlines="None" allowpaging="True" allowsorting="True" culture="es-ES"
                        onneeddatasource="RadGridMaster_NeedDataSource" pagesize="5">
                    <SortingSettings SortToolTip="Click aquí para ordernar" 
                        SortedAscToolTip="Ordenar Asc" SortedDescToolTip="Ordenar Desc" />
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
				        <ClientEvents OnRowDblClick="editarRegistro" />
				        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
				        <Selecting AllowRowSelect="True"></Selecting>
				        <ClientEvents OnRowDblClick="editarRegistro"></ClientEvents>
				        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" Width="100%" NoMasterRecordsText="No se encontraron registros" AllowAutomaticDeletes="True">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="IdNoticia" 
                                FilterControlAltText="Filtrar columna Id" HeaderText="Activo" UniqueName="Id" 
                                Display="False">
                                <HeaderStyle Width="15px" />
                                <ItemStyle Width="15px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Titulo" 
                                FilterControlAltText="Filtrar columna Página" HeaderText="Titulo" 
                                UniqueName="Titulo" >
								<HeaderStyle Font-Bold="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn  DataField="Url" 
                                FilterControlAltText="Filtrar columna Página" HeaderText="Enlace" 
                                UniqueName="Url" >
								<HeaderStyle Font-Bold="True" />
                            </telerik:GridBoundColumn>                        
                             <telerik:GridBoundColumn DataField="FechaCreacion" 
                                FilterControlAltText="Filter column column" HeaderText="Fecha Creación" 
                                UniqueName="FechaCreacion" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="True" />
                                <ItemStyle Width="100px" />
								<ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaPublicacion" 
                                FilterControlAltText="Filter column column" HeaderText="Fecha Publicación" 
                                UniqueName="FechaPublicacion" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="True" />
                                <ItemStyle Width="120px" />
								<ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FechaVencimiento" 
                                FilterControlAltText="Filter column1 column" HeaderText="Fecha Vencimiento" 
                                UniqueName="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="True" />
                                <ItemStyle Width="120px" />
								<ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Imagen" UniqueName="thumb_image">
                                <ItemTemplate>
                                    <asp:Image ID="SushiImage" runat="server" AlternateText="" Height="129px" ImageUrl='<%# String.Format("~/Upload_Imagenes/{0}", Eval("thumb_image")) %>' Style="border: 1px solid #000000;" Width="216px" />
                                </ItemTemplate>
                                <HeaderStyle Width="220px" Font-Bold="True" />
                                <ItemStyle Width="220px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Resumen" 
                                FilterControlAltText="Filter column1 column" HeaderText="Descripción" UniqueName="Resumen">
								<HeaderStyle Font-Bold="True" />
								<ItemStyle HorizontalAlign="Justify"  />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Descripcion" Display="False" 
                                FilterControlAltText="Filter Descripcion column" HeaderText="Descripcion" 
                                UniqueName="Descripcion">
                                <HeaderStyle Width="0px" />
                                <ItemStyle Width="0px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridCheckBoxColumn DataField="Activo" FilterControlAltText="Filtrar columna IndActivo" HeaderText="Activo" UniqueName="IndActivo">
                                <HeaderStyle Width="60px" Font-Bold="True" />
                                <ItemStyle Width="60px" />
                            </telerik:GridCheckBoxColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                        </EditFormSettings>
                        <PagerStyle AlwaysVisible="True" 
                                    FirstPageToolTip="Primera página" 
                                    LastPageToolTip="Última página" NextPageToolTip="Página siguiente" 
                                    PrevPageToolTip="Página anterior" 
                                    PageSizeLabelText="Tamaño de la página:"/>
                        <CommandItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="right">
                                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnClientButtonClicking="PanelBarItemClicked">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" Visible="true" CommandName="Editar"
                                                    ImagePosition="Right" ImageUrl="~/Imagenes/Edit.gif" Text="Editar" CausesValidation="False" />
                                                <telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right"
                                                    PostBack="true" ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" CausesValidation="False" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </td>
                                </tr>
                            </table>
                        </CommandItemTemplate>
                    </MasterTableView>
                        <PagerStyle PageSizeControlType="RadComboBox" />
                    <FilterMenu EnableImageSprites="False"></FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                </telerik:radgrid>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <div style="display: none;">
        <asp:TextBox ID="txtLista" runat="server"></asp:TextBox>
        <telerik:radtextbox id="RadtbModo" runat="server"></telerik:radtextbox>
        <telerik:radtextbox id="txtId" runat="server"></telerik:radtextbox>
        <telerik:radbutton id="RadGuarda" runat="server" text="Guardar Registro" onclick="btnGuardar_Click"></telerik:radbutton>
        &nbsp;
    <telerik:radbutton id="EliminarRegistro" runat="server" onclick="EliminarRegistro_Click" text="Eliminar Registro" autopostback="true" />
        <telerik:radgrid id="radGridTemporal" runat="server" autogeneratecolumns="False" width="300px">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" UniqueName="Id" />
                <telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filter column1 column" UniqueName="Nombre" />
                <telerik:GridBoundColumn DataField="IdNoticia" FilterControlAltText="Filter column2 column" UniqueName="IdNoticia" />
            </Columns>
        </MasterTableView>
    </telerik:radgrid>
    </div>

    <asp:Panel ID="PanelPublicacion" runat="server">
        <table id="TablaPublicacion" class="FuentePublicacion" cellspacing="0" cellpadding="0" width="100%" runat="server">
            <tr>
                <td style="vertical-align: top; width: 130px;" class="SubTitulosPublicacion">Publicación:</td>
                <td style="width: 5px;">&nbsp;</td>
                <td style="vertical-align: top;">
                    <table class="auto-style1">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>&nbsp;&nbsp;&nbsp;Título&nbsp;&nbsp;&nbsp;</legend>
                                    <table style="height:150px;">
                                        <tr>
                                            <td style="vertical-align:top;">
                                                <asp:TextBox ID="txtTitulo" runat="server" CausesValidation="true" CssClass="FuentePublicacion" Height="100px" TextMode="MultiLine" ValidationGroup="vgNoticias" Width="350px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" Display="None" ErrorMessage="- Título de la publicación" Height="25px" ValidationGroup="vgNoticias" Width="25px" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td>
                                <fieldset>
                                    <legend class="SubTitulosPublicacion">&nbsp;&nbsp;&nbsp;Tamaño de la imagen en pixels&nbsp;&nbsp;&nbsp;</legend>
                                    <table style="height:129px;">
                                        <tr>
                                            <td rowspan="2">
                                                <div style="Height:129px; Width:216px;">
                                                    <asp:Image ID="ImgThumb" runat="server" Height="129px" Width="216px" />
                                                </div>
                                            </td>
                                            <td>Ancho:
                                                <asp:TextBox ID="txtAncho" runat="server" CssClass="FuentePublicacion" MaxLength="3" Text="216" Width="47px"></asp:TextBox>
                                                &nbsp;Alto:
                                                <asp:TextBox ID="txtAlto" runat="server" CssClass="FuentePublicacion" MaxLength="3" Text="129" Width="47px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadUpload ID="RadUpload1" Runat="server" AllowedFileExtensions=".jpg,.gif,.png" ControlObjectsVisibility="None" Height="20px" MaxFileInputsCount="1" onchange="readURL(this)" OverwriteExistingFiles="True" Width="220px">
                                                    <localization select="Examinar..." />
                                                </telerik:RadUpload>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </fieldset></td>
                            <td>
                                <fieldset>
                                    <legend class="SubTitulosPublicacion">&nbsp;&nbsp;&nbsp;Redes&nbsp;&nbsp;&nbsp;</legend>
                                    <table style="height:150px;">
                                        <tr>
                                            <td>
                                                <telerik:RadListBox ID="rlbRedes" runat="server" culture="es-ES" datatextfield="Nombre" datavaluefield="Id" enableviewstate="False" height="100px" viewstatemode="Disabled" width="250px">
                                                </telerik:RadListBox>
                                            </td>
                                            <td style="vertical-align: top;">
                                                <telerik:RadButton ID="btnAgregar" runat="server" autopostback="false" onclientclicking="AbrirWndRedes" text="..." />
                                                <br />
                                                <asp:CustomValidator ID="rfvRedes" runat="server" ClientValidationFunction="ListaRedes" Display="None" ErrorMessage="- Red" Height="25px" ValidationGroup="vgNoticias" Width="25px" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 5px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 130px; vertical-align: top" class="SubTitulosPublicacion">Fecha Vigencia:</td>
                <td style="width: 5px;">&nbsp;</td>
                <td style="vertical-align: top;">
                    <table width="100%">
                        <tr>
                            <td class="SubTitulosPublicacion" style="padding-bottom: 5px; width: 120px;">Desde:</td>
                            <td style="padding-bottom: 5px; width: 200px;">
                                <telerik:RadDatePicker ID="rdpFechaPublicacion" runat="server" calendar-showcolumnheaders="True" cssclass="defaultText" title="Entre su palabra de búsqueda aquí" width="100px">
                                    <calendar usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                    </calendar>
                                    <dateinput id="di_rdpFechaPublicacion" runat="server" dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" validationgroup="vgNoticias">
                                    </dateinput>
                                    <datepopupbutton hoverimageurl="" imageurl="" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="rfvFechaPublicacion" runat="server" ControlToValidate="rdpFechaPublicacion" Display="None" ErrorMessage="- Fecha de publicación" Height="25px" ValidationGroup="vgNoticias" Width="25px" />
                            </td>
                            <td class="SubTitulosPublicacion" style="padding-bottom: 5px; width: 120px;">Hasta:</td>
                            <td style="padding-bottom: 5px;">
                                <telerik:RadDatePicker ID="rdpFechaVencimiento" runat="server" calendar-showcolumnheaders="True" width="100px">
                                    <calendar usecolumnheadersasselectors="False" userowheadersasselectors="False">
                                    </calendar>
                                    <dateinput id="di_rdpFechaVencimiento" runat="server" dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" validationgroup="vgNoticias">
                                    </dateinput>
                                    <datepopupbutton hoverimageurl="" imageurl="" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="rfvFechaVencimiento" runat="server" ControlToValidate="rdpFechaVencimiento" Display="None" ErrorMessage="- Fecha de Vencimiento" Height="25px" ValidationGroup="vgNoticias" Width="25px" />
                            </td>
                            <%--<td> <asp:CheckBox runat="server" ID="IndActivo">  </asp:CheckBox> </td>--%>
                            <td class="SubTitulosPublicacion" style="padding-bottom: 5px; width: 230px;" rowspan="2">Desea Publicar esta Noticia en Portal:</td>
                            <td>
                              <table>
                                <tr>
									<td style="width:5px;"><asp:RadioButton runat="server" ID="IndActivo_01" ClientIDMode="Static" GroupName="IndActivo" /></td>
									<td>Si </td>
								</tr>
								<tr>
									<td style="width:5px;"><asp:RadioButton runat="server" ID="IndActivo_02" ClientIDMode="Static" GroupName="IndActivo" /></td>
									<td>No </td>
								</tr>
                              </table>
                            </td>
                        </tr>                        
                    </table>
                </td>
                <td style="width: 5px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="SubTitulosPublicacion" style="width: 130px; vertical-align: top">Descripción:
                    <br />
                </td>
                <td style="width: 5px;">&nbsp;</td>
                <td style="vertical-align: top;"><%--<asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <telerik:RadEditor ID="RadEditorNoticias" runat="server" font-names="Arial" font-size="12pt" height="490px" language="es-ES" skinid="DefaultSetOfTools" toolbarmode="RibbonBar" toolsfile="~/DesktopModules/HTML/Noticias/RibbonItems/FullTools.xml" width="100%">
                                <content>
                                </content>
                                <ImageManager ViewPaths="~/Upload_Imagenes" DeletePaths="~/Upload_Imagenes" UploadPaths="~/Upload_Imagenes" />
                                <DocumentManager ViewPaths="~/Upload_Documentos" DeletePaths="~/Upload_Documentos" UploadPaths="~/Upload_Documentos" />
                                <FlashManager ViewPaths="~/Upload_Flash" DeletePaths="~/Upload_Flash" UploadPaths="~/Upload_Flash" />
                                <MediaManager ViewPaths="~/Upload_Videos" DeletePaths="~/Upload_Videos" UploadPaths="~/Upload_Videos" />
                                <TemplateManager ViewPaths="~/Silverlight" DeletePaths="~/Upload_Silverlight" UploadPaths="~/Upload_Silverlight" />
                                <trackchangessettings canaccepttrackchanges="False" />
                            </telerik:RadEditor>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 5px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 130px">&nbsp;</td>
                <td style="width: 5px">&nbsp;</td>
                <td style="padding-bottom: 5px; padding-top: 5px;">
                    <asp:RequiredFieldValidator ID="rfvEditor" runat="server" ControlToValidate="RadEditorNoticias" Display="None" ErrorMessage="- Contenido de la publicación" Height="25px" ValidationGroup="vgNoticias" Width="120px" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" HeaderText="Debe introducir un valor en los siguientes campos:" ShowMessageBox="True" ShowSummary="False" ValidationGroup="vgNoticias" />
                </td>
                <td style="width: 5px;">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 130px">&nbsp;</td>
                <td style="width: 5px">&nbsp;</td>
                <td style="text-align: center;">
                    <telerik:RadButton ID="btnGuardar" runat="server" onclick="btnGuardar_Click" text="Guardar" validationgroup="vgNoticias" width="160px">
                    </telerik:RadButton>
                    &nbsp;
                    <telerik:RadButton ID="btnCancelar" runat="server" autopostback="True" causesvalidation="False" onclick="btnCancelar_Click" text="Cancelar" usesubmitbehavior="False" width="160px">
                    </telerik:RadButton>
                </td>
                <td style="width: 5px;">&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

</div>

<telerik:radwindowmanager id="RWM_General" runat="server" enableshadow="True" enableembeddedskins="False" />

<telerik:radwindow id="WndRedes" runat="server"
    visible="True" title="Redes" visibleonpageload="False" visiblestatusbar="False"
    enableviewstate="False" animation="None" autosize="False"
    behaviors="Close, Move" modal="True" borderstyle="Dotted"
    visibletitlebar="True" enabletheming="False"
    borderwidth="10" height="380px" overlay="False" width="470px"
    iconurl="~/Imagenes/Ico_Redes.png">
	<ContentTemplate>
		<table class="FuentePublicacion" width="100%">
            <tr>
				<td>
                    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
                    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                        <asp:RadioButton ID="rbPublico" runat="server" Text="Público" AutoPostBack="False" GroupName="GNredes" Checked="True" OnClick="javascript:habilitarPublico()" /><br />
                        <asp:RadioButton ID="rbRedes" runat="server" Text="Redes" AutoPostBack="False" GroupName="GNredes" OnClick="javascript:habilitarRedes()" /> 
                    </telerik:RadAjaxPanel>
                </td>
				<td>&nbsp;</td>
				<td></td>
            </tr>
			<tr>
				<td style="text-align:center; width: 50%" class="SubTitulosPublicacion">Disponibles</td>
				<td>&nbsp;</td>
				<td style="text-align:center; width: 50%" class="SubTitulosPublicacion">Asociadas</td>
			</tr>
			<tr>
				<td colspan="3">
                    <div style="display: none"><telerik:RadListBox ID="rlbTipoSuscriptores" runat="server" DataTextField="Nombre" DataValueField="Id" /></div>
					<telerik:RadListBox  ID="rlbTipoSuscriptorDisp" TransferToID="rlbTipoSuscriptorAsoc" AllowTransfer="True"
                                         Width="220px" Height="200px" runat="server" DataTextField="Nombre" DataValueField="Id" 
                                         SelectionMode="Multiple" TransferMode="Move" Enabled="false" />

					<telerik:RadListBox ID="rlbTipoSuscriptorAsoc" Width="220px" Height="200px" runat="server" 
                                        DataTextField="Nombre" DataValueField="Id" SelectionMode="Multiple" Enabled="false" />
				</td>
			</tr>
            <tr>
				<td colspan="3">&nbsp;</td>
			</tr>
			<tr>
				<td colspan="3" align="center">
                    <telerik:RadButton runat="server" ID="btn_Aceptar" Text="Aceptar" AutoPostBack="false" Width="100px" OnClientClicking="Asignar" CausesValidation="False" UseSubmitBehavior="False"></telerik:RadButton>&nbsp;
                    <telerik:RadButton runat="server" ID="btn_Cerrar" Text="Cerrar" AutoPostBack="false" Width="100px" OnClientClicking="CerrarVentana"></telerik:RadButton>
				</td>
			</tr>
        </table>
	</ContentTemplate>
</telerik:radwindow>
