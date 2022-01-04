function retornarMarcaEliminado() {
    grid = $find(nombreGrid);
    var IndiceSeleccionado = grid._selectedIndexes;

    var masterTable = $find(nombreGrid).get_masterTableView();
    var row = masterTable.get_dataItems()[IndiceSeleccionado];

    var columnCount = grid.get_masterTableView().get_columns().length;
    var UniqueName = "";
    var existeColumnaEliminado = false;
    var celda;
    for (var i = 0; i < columnCount; i++) {
        UniqueName = grid.get_masterTableView().get_columns()[i].get_uniqueName();
        if (UniqueName == "IndEliminado") {
            celda = i;
            existeColumnaEliminado = true;
            break;
        }
    }
    if (existeColumnaEliminado) {
        var chkCtrl = row._element.cells[celda].children[0].children[0];
        var bResultado = chkCtrl.checked;
    }
    else {
        bResultado = existeColumnaEliminado;
    }

    return bResultado;
}

function retornarMarcaEliminadoSelecteds(indiceSelected) {
    grid = $find(nombreGrid);
    var IndiceSeleccionado = indiceSelected;

    var masterTable = $find(nombreGrid).get_masterTableView();
    var row = masterTable.get_dataItems()[IndiceSeleccionado];

    var columnCount = grid.get_masterTableView().get_columns().length;
    var UniqueName = "";
    var existeColumnaEliminado = false;
    var celda;
    for (var i = 0; i < columnCount; i++) {
        UniqueName = grid.get_masterTableView().get_columns()[i].get_uniqueName();
        if (UniqueName == "IndEliminado") {
            celda = i;
            existeColumnaEliminado = true;
            break;
        }
    }
    if (existeColumnaEliminado) {
        var chkCtrl = row._element.cells[celda].children[0].children[0];
        var bResultado = chkCtrl.checked;
    }
    else {
        bResultado = existeColumnaEliminado;
    }
    return bResultado;
}

function validarRegistro(sender, args) {
    if (retornarMarcaEliminado()) {
        radconfirm("El registro seleccionado se encuentra marcado como eliminado. ¿Desea activarlo?", confirmarRestaurarRegEliminado, 380, 100, null, "Activar Registro");
    }
    else {
        ShowMessage(sender, args);
    }
}

function confirmarRestaurarRegEliminado(sender) {
    var boton = $find(nombreBoton);
    if (sender)
        boton.click();
    else
        return false;
}

function ShowMessage(sender, args) {
    var tomado = "";
    if (args.get_item().get_cell('Tomado')) {
        tomado = args.get_item().get_cell('Tomado').getElementsByTagName("img")[0].src;
        tomado = tomado.split("/")[tomado.split("/").length - 1];
        tomado = tomado.substring(0, tomado.length - 4);
    }
    if ((tomado != "" && (tomado == "TP" || tomado == "L")) || tomado == "") {
        var wnd = GetRadWindow();
        var id = args.getDataKeyValue("IdEncriptado");
        $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
        wnd.setUrl(ventanaDetalle + "accion=" + AccionModificar + "&id=" + id);
    }
}

function PanelBarItemClicked(sender, args) {
    var wnd = GetRadWindow();
    var comando = args.get_item().get_commandName();
    switch (comando) {
        case "OpenRadFilter":
            $find(nombreVentana).show();
            break;

        case "ViewDetails":
            grid = $find(nombreGrid);
            var selectedIndexes = grid._selectedIndexes;
            if (selectedIndexes.length == 0) {
                radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
            }
            else {
                if (selectedIndexes.length == 1) {
                    if (retornarMarcaEliminado()) {
                        radconfirm("El registro seleccionado se encuentra marcado como eliminado. ¿Desea activarlo?", confirmarRestaurarRegEliminado, 380, 100, null, "Activar Registro");
                    }
                    else {
                        $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
                        wnd.setUrl(ventanaDetalle + "accion=" + AccionVer + "&id=" + grid._clientKeyValues[selectedIndexes[0]].IdEncriptado);
                    }
                }
                else {
                    var statusEliminado = false;
                    for (elem in selectedIndexes) {
                        if (retornarMarcaEliminadoSelecteds(selectedIndexes[elem]))
                            statusEliminado = true;
                    }
                    if (statusEliminado)
                        radconfirm("El registro seleccionado se encuentra marcado como eliminado. ¿Desea activarlo?", confirmarRestaurarRegEliminado, 380, 100, null, "Activar Registro");
                    else
                        radalert("Seleccione solo un registro para ver el detalle", 280, 50, "Ver detalle de Registro")
                }
            }
            break;

        case "Add":
            $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
            wnd.setUrl(ventanaDetalle + "accion=" + AccionAgregar + "&id=" + idMaster);
            break;

        case "Edit":
            gridEdit = $find(nombreGrid);
            var selectedIndexesEdit = gridEdit._selectedIndexes;
            if (selectedIndexesEdit.length == 0) {
                radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
            }
            else {
                if (selectedIndexesEdit.length == 1) {
                    if (retornarMarcaEliminado()) {
                        radconfirm("El registro seleccionado se encuentra marcado como eliminado. ¿Desea activarlo?", confirmarRestaurarRegEliminado, 380, 100, null, "Activar Registro");
                    }
                    else {
                        var tomado = "";
                        if (gridEdit.get_masterTableView().getColumnByUniqueName("Tomado")) {
                            tomado = gridEdit.MasterTableView.get_selectedItems(selectedIndexesEdit[0])[0].get_cell("Tomado").getElementsByTagName("img")[0].src;
                            tomado = tomado.split("/")[tomado.split("/").length - 1];
                            tomado = tomado.substring(0, tomado.length - 4);
                        }
                        if ((tomado != "" && (tomado == "TP" || tomado == "L")) || tomado == "") {
                            $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
                            wnd.setUrl(ventanaDetalle + "accion=" + AccionModificar + "&id=" + gridEdit._clientKeyValues[selectedIndexesEdit[0]].IdEncriptado);
                        }
                    }
                }
                else {
                    var statusEliminado = false;
                    for (elem in selectedIndexesEdit) {
                        if (retornarMarcaEliminadoSelecteds(selectedIndexesEdit[elem]))
                            statusEliminado = true;
                    }
                    if (statusEliminado)
                        radconfirm("El registro seleccionado se encuentra marcado como eliminado. ¿Desea activarlo?", confirmarRestaurarRegEliminado, 380, 100, null, "Activar Registro");
                    else
                        radalert("Seleccione solo un registro para ver el detalle", 380, 50, "Ver detalle de Registro")
                }
            }
            break;
        case "InitInsert":
            break;
        case "Refrescar":
            break;              
        case "Eliminar":
        	break;
        case "PostBack":
            break;
        case "Reactivar":
            break;
        case "VerDet":
            break;
        case "AgregarDatoParticularEspecial":
            grid = $find(nombreGrid);
            var defaulSelectedIndexesDP = grid._selectedIndexes;
            if (defaulSelectedIndexesDP.length == 0) {
                radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
            }
            else {
                $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
                wnd.setUrl(args._item._properties._data.value + "IdMenu=" + idMenu + "&accion=" + AccionModificar + "&id=" + grid._clientKeyValues[defaulSelectedIndexesDP[0]].IdUsuarioEncriptado);
            }
            break;
        case "parametrosvinculaciones":
            grid = $find(nombreGrid);
            var defaulSelectedIndexesPV = grid._selectedIndexes;
            if (defaulSelectedIndexesPV.length == 0) {
                radalert("Seleccione el registro a mostrar", 380, 50, "Ver  detalles del Paso")
            }
            else {
                $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
                wnd.setUrl(ventanaDetalle + "IdMenu=" + idMenu + "&accion=" + AccionModificar);
                
            }
            break;
        default:
            grid = $find(nombreGrid);
            var defaulSelectedIndexes = grid._selectedIndexes;
            if (defaulSelectedIndexes.length == 0) {
                radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
            }
            else {
                $find($find(nombreRadAjaxManager).get_defaultLoadingPanelID()).show("formMain")
                wnd.setUrl(args._item._properties._data.value + "IdMenu=" + idMenu + "&accion=" + AccionModificar + "&id=" + grid._clientKeyValues[defaulSelectedIndexes[0]].IdEncriptado);
            }
            break;
    }
}

var lastClickedItem = null;
var clickCalledAfterRadconfirm = false;
function ClientButtonClicking(sender, args) {
    switch (args.get_item().get_commandName()) {
        case "Eliminar":
            if (!clickCalledAfterRadconfirm) {
                grid = $find(nombreGrid);
                var selectedIndexes = grid._selectedIndexes;
                if (selectedIndexes.length == 0)
                    radalert("Seleccione el registro a eliminar", 380, 120, "Eliminar registro")
                else {
                    if (selectedIndexes.length > 1) {
                        args.set_cancel(true);
                        lastClickedItem = args.get_item();
                        radconfirm("¿Seguro de eliminar los registros seleccionados?", confirmCallbackFunction, 380, 50, null, "Eliminar registro");
                    }
                    else {
                        if (!retornarMarcaEliminado()) {
                            args.set_cancel(true);
                            lastClickedItem = args.get_item();
                            radconfirm("¿Seguro de eliminar el registro?", confirmCallbackFunction, 380, 50, null, "Eliminar registro");
                        }
                        else {
                            radalert("El registro se encuentra eliminado...", 380, 50, "Registro eliminado");
                            return false;
                            break;
                        }
                    }
                }
            }
            else {
                clickCalledAfterRadconfirm = false;
            }
            break;
    }
}

function confirmCallbackFunction(args) {
    if (args) {
        clickCalledAfterRadconfirm = true;
        lastClickedItem.click();
    }
    else
        clickCalledAfterRadconfirm = false;
    lastClickedItem = null;
}

function hideFilterBuilderDialog() {
    $find(nombreVentana).close();
}


function changeTextRadAlert() {
    Telerik.Web.UI.RadWindowUtils.Localization =
    {
        "Close": "Cerrar",
        "Minimize": "Minimizar",
        "Maximize": "Maximizar",
        "Reload": "Recargar",
        "Restore": "Restaurar",
        "OK": "Aceptar",
        "Cancel": "Cancelar",
        "Yes": "Si",
        "No": "No"
    };
}