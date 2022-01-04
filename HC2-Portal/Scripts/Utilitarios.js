function textCounter(field, cntfield, maxlimit) {
	var tempfield = document.getElementById(field);
	var tempcntfield = document.getElementById(cntfield);
	if(tempfield.value.length > maxlimit)
		tempfield.value = tempfield.value.substring(0, maxlimit);
	else
		tempcntfield.textContent = maxlimit - tempfield.value.length;
}

function SinEspaciosPrincipioFin(obj) {
	obj.value = obj.value.replace(/</g, '').replace(/>/g, '').trim();
}

function SinCaracteresEspecialesNiEspacios(e) {
	var keynum;
	if(window.event) // IE
		keynum = e.keyCode;
	else if (e.which) // Netscape/Firefox/Opera
		keynum = e.which;
	if(keynum == 60 || keynum == 62 || keynum == 32) {
		return false;
	}
}

function SinCaracteresEspeciales(e) {
	var keynum;
	if(window.event) // IE
		keynum = e.keyCode;
	else if(e.which) // Netscape/Firefox/Opera
		keynum = e.which;
	else if(e.keyCode)
		keynum = e.keyCode;
	if(keynum == 60 || keynum == 62) {
		return false;
	}
}

$.fn.clearForm = function () {
	return this.each(function () {
		var type = this.type, tag = this.tagName.toLowerCase();
		if(tag == 'form')
			return $(':input', this).clearForm();
		if(type == 'text' || type == 'password' || tag == 'textarea')
		{
			if(this.defaultValue != this.value)
				this.value = '';
		}
		else if(type == 'checkbox' || type == 'radio')
			this.checked = false;
		else if(tag == 'select')
			this.selectedIndex = -1;
	});
};

function delayer() {
	window.top.location = "/Login.aspx";
}

function SoloNumeros(e) {
	var keynum;
	if(window.event) // IE
		keynum = e.keyCode;
	else if(e.which) // Netscape/Firefox/Opera
		keynum = e.which;
	else if(e.keyCode)
		keynum = e.keyCode;
	var checker = new RegExp("\\d");
	if(keynum == 8 || keynum == 09) {
		return true;
	}
	else {
		return checker.test(String.fromCharCode(keynum));
	}
}

(function ($) {
	$.textMetrics = function (el) {
		var h = 0, w = 0;
		var div = document.createElement('div');
		document.body.appendChild(div);
		$(div).css({
			position: 'absolute',
			left: -1000,
			top: -1000,
			display: 'none'
		});
		$(div).html($(el).html());
		var styles = ['font-size', 'font-style', 'font-weight', 'font-family', 'line-height', 'text-transform', 'letter-spacing'];
		$(styles).each(function () {
			var s = this.toString();
			$(div).css(s, $(el).css(s));
		});
		h = $(div).outerHeight();
		w = $(div).outerWidth();
		$(div).remove();
		var ret = {
			height: h,
			width: w
		};
		return ret;
	}
})(jQuery);

var initialZone;
var initialIndex;

function OnDragStart(dock, args) {
    initialIndex = dock.get_index();
    initialZone = dock.get_dockZone();
}

function DockPositionChanged(dock, e) {
    var IdZonaActual = dock.get_dockZoneID();
    var currentZone = dock.get_dockZone();
    var dockToBeDocked = currentZone.get_docks()[dock.get_index()];
    var zonasforbidden = dock.get_forbiddenZones();

    for (var item in zonasforbidden) {
        if (IdZonaActual == zonasforbidden[item]) {
            initialZone.dock(dockToBeDocked, initialIndex);
            break;
        }
    }
}

function toogle() {

    var text_ListaDisponibles = getRadControl('txtListaDisponibles');
    var ctrl = getRadControl('txtArrayGadgets')._SetValue('');
    var arCtrlListBox = $('.RadListBox');
    var arCtrlDockZone = $('.RadDockZone');
    var _ListaDisponibles = getRadControl('rlbListaDisponibles');
    var objInfo = Sys.CultureInfo.CurrentCulture.name;
    var params = toogle.arguments;
    var cultureInfo = "";
    var _objlistaZona1 = getRadControl('RadDockZone1').get_docks();
    var _objlistaZona2 = getRadControl('RadDockZone2').get_docks();
    var _objlistaZona3 = getRadControl('RadDockZone3').get_docks();
    var _listaZona1 = getRadControl('ListaZona1');
    var _listaZona2 = getRadControl('ListaZona2');
    var _listaZona3 = getRadControl('ListaZona3');
    _listaZona1.get_items().clear();
    _listaZona2.get_items().clear();
    _listaZona3.get_items().clear();
   
    if (location.pathname.indexOf(objInfo.toLowerCase()) != -1) cultureInfo = "../" + objInfo.toLowerCase() + "/";
    var newImage = "url(" + cultureInfo + "imagenes/backgroundModal.png)";
    
    for (var i = 1; i < params.length; i++) {
        document.getElementById(params[i]).style.display = params[0];
        document.getElementById(params[i]).style.background = "background-color: #808080";
        if (params[i] != "modal") document.getElementById(params[i]).style.background = newImage + " no-repeat";
    }

    if (text_ListaDisponibles.get_value() != "")
        var arrItems = text_ListaDisponibles.get_value().split("|");

    for (var j = 0; j < _objlistaZona1.length; j++) {
        var item = new Telerik.Web.UI.RadListBoxItem();
        item.set_text(LetraCapital(_objlistaZona1[j]._title));
        item.set_value(j);
        _listaZona1.get_items().add(item);
        _listaZona1.get_items()._array[j].set_enabled(false);
        remItemLista(_ListaDisponibles, LetraCapital(_objlistaZona1[j]._title));
    }

    for (var j = 0; j < _objlistaZona2.length; j++) {
        var item = new Telerik.Web.UI.RadListBoxItem();
        item.set_text(LetraCapital(_objlistaZona2[j]._title));
        item.set_value(j);
        _listaZona2.get_items().add(item);
        _listaZona2.get_items()._array[j].set_enabled(false);
        remItemLista(_ListaDisponibles, LetraCapital(_objlistaZona2[j]._title));
    }

    for (var j = 0; j < _objlistaZona3.length; j++) {
        var item = new Telerik.Web.UI.RadListBoxItem();
        item.set_text(LetraCapital(_objlistaZona3[j]._title));
        item.set_value(j);
        _listaZona3.get_items().add(item);
        _listaZona3.get_items()._array[j].set_enabled(false);
        remItemLista(_ListaDisponibles, LetraCapital(_objlistaZona3[j]._title));
    }
}

function remItemLista(objLista, itemText) {
    for (var x = 0; x < objLista.get_items()._array.length; x++) {
        if (objLista.get_items()._array[x].get_text() == LetraCapital(itemText)) {
            var itemToremove = objLista.get_items()._array[x];
            var indice = objLista.get_items()._array[x].get_index();
            objLista.get_items().remove(itemToremove);
        }
    }
}

function transferirItem(arg, e) {

    var strObjListaDestino;
    switch (arguments[0].get_id().split('_')[3]) {
        case "btnTransferirZona1":
            strObjListaDestino = "ListaZona1";
            break;
        case "btnTransferirZona2":
            strObjListaDestino = "ListaZona2";
            break;
        case "btnTransferirZona3":
            strObjListaDestino = "ListaZona3";
            break;
    }

    var arr = $('.RadListBox');
    var objListaDisponibles; var listaZona1; var listaZona2; var listaZona3;

    for (var i = 0; i < arr.length; i++) {
        var _docks = $('.RadDockZone')[i].control.get_docks()
        if (arr[i].control._clientStateFieldID.indexOf('ListaDisponibles') != -1)
            objListaDisponibles = arr[i].control;
        else if (arr[i].control._clientStateFieldID.indexOf(strObjListaDestino) != -1) {
            listBoxDestino = arr[i].control;
            break;
        }
        else if (arr[i].control._clientStateFieldID.indexOf(strObjListaDestino) != -1) {
            listBoxDestino = arr[i].control;
            break;
        }
        else if (arr[i].control._clientStateFieldID.indexOf(strObjListaDestino) != -1) {
            listBoxDestino = arr[i].control;
            break;
        }
    }

    var selectedItemStatus = false;
    var selectedItem;

    for (var i = 0; i < objListaDisponibles.get_items()._array.length; i++)
        if (selectedItemStatus = objListaDisponibles.get_items()._array[i].get_selected()) {
            selectedItem = objListaDisponibles.get_items()._array[i];
            break;
        }

    if (selectedItemStatus == false) {
        alert("selecciona el item a transferir.");
        return false;
    }

    var index = selectedItem.get_index();
    listBoxDestino._element.control.get_items().add(selectedItem);
    if (listBoxDestino._element.control.get_items().get_count() > 1)
        listBoxDestino.reorderItem(selectedItem, 0);

    return false;
	
}

function closeDock(sender, e) {
    var text_ListaDisponibles = getRadControl('txtListaDisponibles');
    text_ListaDisponibles.set_value(text_ListaDisponibles.get_value() + "|" + LetraCapital(sender._title));
}

function updateGadgets() {

    var ctrl;
/*    ctrl = getRadControl('txtArrayGadgets');

    var arr = $('.RadListBox');
    var listaZona; var str; var strCadena; var strConcatenado = ""; var strCtrl; var strZona;
    for (var i = 0; i < arr.length; i++) {
        var _docks = $('.RadDockZone')[i].control.get_docks()
        if (arr[i].control._clientStateFieldID.indexOf('ListaDisponibles') != -1) {
            objListaDisponibles = arr[i].control._element.control.get_items();
            var arrItems = ""; var strCadena = "";
            for (var x = 0; x < objListaDisponibles._array.length; x++) {
                strCadena = objListaDisponibles._array[x]._text + ("|");
                arrItems = arrItems + strCadena;
            }
            arrItems = arrItems.substring(0, arrItems.length - 1);
        }
        else if (arr[i].control._clientStateFieldID.indexOf('ListaZona1') != -1) {
            listaZona = arr[i].control;
            strZona = "RadDockZone1";
        }
        else if (arr[i].control._clientStateFieldID.indexOf('ListaZona2') != -1) {
            listaZona = arr[i].control;
            strZona = "RadDockZone2";
        }
        else if (arr[i].control._clientStateFieldID.indexOf('ListaZona3') != -1) {
            listaZona = arr[i].control;
            strZona = "RadDockZone3";
        }

        if (listaZona != null) {
            for (var x = 0; x < listaZona._element.control.get_items()._array.length; x++) {
                if (listaZona._element.control.get_items()._array[x].get_enabled()) {
                    strCadena = ""
                    strCtrl = listaZona._element.control.get_items()._array[x]._text;
                    str = strCtrl + ":" + strZona + "|";
                    strCadena = strCadena + str;
                    strConcatenado = strConcatenado + strCadena;
                }
            }
            ctrl._SetValue(strConcatenado.substring(0, strConcatenado.length - 1))
        }

    }
    ctrlLstDispon = getRadControl('txtListaDisponibles');
    ctrlLstDispon._SetValue(arrItems);
	*/
}

function setearValor(strControl, valor) {
    var ctrlLstDispon = getRadControl(strControl);
    ctrlLstDispon._SetValue(valor);
}

function getRadControl(_control) {
    return $telerik.findControl(theForm, _control);
}

function cancelarAddGadgets() {
    toogle('none', 'modal', 'AddGagdetLayer');
}

function LetraCapital(string) {
    return string.charAt(0).toUpperCase() + string.slice(1).toLowerCase();
}

function openURL() {
	if(getRadControl('rdBtnSolicitud').get_enabled()){
		var oWnd = GetRadWindowManager();
		oWnd._height = "350px";
		oWnd._width = "800px";
		if(getRadControl('rdTextIdServicio').get_value() != ""){
			oWnd.setUrl(getRadControl('rdTextIdServicio').get_value());
			oWnd.SetTitle("..:: [ HConnexum - Tramitador ] ::..");
			oWnd.show();
		}
	}
}

function LoadServicios(sender, eventArgs) {
    var item = eventArgs.get_item();
    var cmbServicio = getRadControl('ddlServicio');
    if (item.get_index() >= 0)
        cmbServicio.requestItems(item.get_value(), false);
}

function ItemsLoaded(sender, eventArgs) {
    if (sender.get_items().get_count() > 0) {
        sender.set_text(sender.get_items().getItem(0).get_text());
        sender.get_items().getItem(0).highlight();
		sender.showDropDown();
    }
}

function ClientSelectedIndexChangedServicios(sender, eventArgs) {
    var rdBtnSol = getRadControl('rdBtnSolicitud')
    if (sender.get_items().get_count() > 0) {
        rdBtnSol.set_enabled(true);
        getRadControl('rdTextIdServicio').set_value(sender.get_value());
    }
}

function clientSelectedIndexChanged(sender, e) {
    //location.reload();
	//location.href = "misaplicaciones.aspx?id=63";
	//__doPostBack('dnn_ctr432_GadgetsPagina_UpdatePanelDocks', '');
	//__doPostBack('dnn_ctr432_GadgetsPagina_UP', '');
}

function Initialize(dock, args) {
    var closeCmd = dock.get_commands()["Close"];
    var CollapseCmd = dock.get_commands()["ExpandCollapse"];

    if (closeCmd) closeCmd.set_visible(!closeCmd.get_visible());
    if (CollapseCmd) CollapseCmd.set_visible(!CollapseCmd.get_visible());
}

function actualizarUpdatePanel(upanel) {
    var ctrlUpanel = document.getElementById(upanel);
    __doPostBack(ctrlUpanel, '');
}

function closeRadWindow(sender, e){
	GetRadWindow().Close(); 
	return false;
}

function RadWindowClientCommand(sender, e){
	sender.Close();
	/*
	if(e.get_commandName()== 'Close') {
		GetRadWindow().Close(); 
		return false;
	}
	*/
}

function OpenApplication_IE_7(nombreapp, urlapp, multipleWindow) {
	var Bandera = true;
	if (!multipleWindow) {
		var otherWindows = WindowsApp;
		var Windows = $telerik.findControl(theForm, 'singleton').GetWindows();
		for (i = 0; i < Windows.length; i++) {
			if (Windows._windows[i]._title == nombreapp) {
				Bandera = false;
				return;
			}
		}
	}
	if (Bandera) {
		var own = window.radopen(urlapp, null);
		own.SetTitle(nombreapp);
	}
}