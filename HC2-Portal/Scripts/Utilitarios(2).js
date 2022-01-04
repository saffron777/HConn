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

function getRadControl(_control) {
    return $telerik.findControl(theForm, _control);
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
    location.reload();
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

