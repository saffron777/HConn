function SetHandleDock(dock, args) {
	dock.set_handle(document.getElementById("Handle_" + dock.get_id()));
}

function OpenApplication(nombreapp, urlapp, multipleWindow) {
	var Bandera = true;
	if (!multipleWindow) {
		var Windows = GetRadWindowManager().GetWindows();
		$.each(Windows, function () {
			if (this._title == nombreapp) {
				Bandera = false;
				return;
			}
		});
	}
	if (Bandera) {
		var own = window.radopen(urlapp, null);
		own.SetTitle(nombreapp);
	}
}

function RedirectWindow(url) {
	var wnd = GetRadWindow();
	wnd.setUrl(url);
}

function textCounter(field, cntfield, maxlimit) {
    var tempfield = document.getElementById($telerik.$("[id$='" + field + "']").attr("id"));
    var tempcntfield = document.getElementById($telerik.$("[id$='" + cntfield + "']").attr("id"));
    if (tempfield.value.length > maxlimit)
        tempfield.value = tempfield.value.substring(0, maxlimit);
    else
        tempcntfield.textContent = maxlimit - tempfield.value.length;
}

function SinEspaciosPrincipioFin(obj) {
	obj.value = obj.value.replace(/</g, '').replace(/>/g, '').trim();
}

function SinCaracteresEspecialesNiEspacios(e) {
	var keynum;
	if (window.event) // IE
		keynum = e.keyCode;
	else if (e.which) // Netscape/Firefox/Opera
		keynum = e.which;
	else if (e.keyCode)
		keynum = e.keyCode;
	if (keynum == 60 || keynum == 62 || keynum == 32) {
		return false;
	}
}

function SinCaracteresEspeciales(e) {
	var keynum;
	if (window.event) // IE
		keynum = e.keyCode;
	else if (e.which) // Netscape/Firefox/Opera
		keynum = e.which;
	else if (e.keyCode)
		keynum = e.keyCode;
	if (keynum == 60 || keynum == 62) {
		return false;
	}
}

$.fn.clearForm = function () {
    return this.each(function () {
        var type = this.type, tag = this.tagName.toLowerCase();
        if (tag == 'form')
            return $(':input', this).clearForm();
        if (type == 'text' || type == 'password' || tag == 'textarea') {
            if (this.defaultValue != this.value) {
                control = $find(this.id);
                if (control == null)
                    this.value = '';
                else if (Object.getType(control).getName() != 'Telerik.Web.UI.RadDatePicker')
                    this.value = '';
                else
                    control.clear();
            }
        }
        else if (type == 'checkbox' || type == 'radio')
            this.checked = false;
        else if (tag == 'select')
            this.selectedIndex = -1;
    });
};

function delayer(url) {
	window.top.location = url;
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

function Decimal(e) {
	var keynum;
	if (window.event) // IE
		keynum = e.keyCode;
	else if (e.which) // Netscape/Firefox/Opera
		keynum = e.which;
	else if (e.keyCode)
		keynum = e.keyCode;
	var checker = new RegExp("\\d");
	if (keynum == 8 || keynum == 09 || keynum == 44) {
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

function killBackSpace(e) {
	e = e ? e : window.event;
	var t = e.target ? e.target : e.srcElement ? e.srcElement : null;
	if (t && t.tagName && (t.type && /(password)|(text)|(file)/.test(t.type.toLowerCase())) || t.tagName.toLowerCase() == 'textarea')
		return true;
	var k = e.keyCode ? e.keyCode : e.which ? e.which : null;
	if (k == 8) {
		if (e.preventDefault)
			e.preventDefault();
		return false;
	}
	return true;
};

function ActivarValidator(val, status) {
	var myVal = document.getElementById(val);
	ValidatorEnable(myVal, status);
}

$(document).ready(function () {
	$("body").css("display", "none");
	$("body").fadeIn(500);
});

function GetRadWindow() {
	var oWindow = null;
	if (window.radWindow)
		oWindow = window.radWindow;
	else if (window.frameElement.radWindow)
		oWindow = window.frameElement.radWindow;
	return oWindow;
}

function cerrarVentana() {
	GetRadWindow().Close();
}

function ValidarSoloTexto(e) {
	var keynum;
	if (window.event) // IE
		keynum = e.keyCode;
	else if (e.which) // Netscape/Firefox/Opera
		keynum = e.which;
	else if (e.keyCode)
		keynum = e.keyCode;
	var checker = new RegExp("^[a-zA-Z\\s]+$");
	if (keynum == 8) 
	{
		return true;
	}
	else 
	{
		return checker.test(String.fromCharCode(keynum));
	}
}

function changeTextRadAlert() {
    Telerik.Web.UI.RadWindowUtils.Localization =
    {
        "OK": "Aceptar",
        "Cancel": "Cancelar",
        "Yes": "Si",
        "Close": "Cerrar",
        "Minimize": "Minimizar",
        "Maximize": "Maximizar",
        "Reload": "Recargar",
        "Restore": "Restaurar",
        "PinOn": "Acoplar",
        "PinOff": "Desacoplar"
    };
}

function aspMaxLength(control, limiteCaracteres) {
    ///IMPLEMENTACIÓN: onclick="aspMaxLength(this, 500); return false;"
    var _Control = document.getElementById(control.id);
    _Control.maxLength = limiteCaracteres;
}

//Funcion para reordenar las cabezeras de los radGrids
//Se debe crear funcion js
//function resizeRadGridWithScroll(sender, args) {
//  resizeRadGrid("<%= RadGridMaster.ClientID %>");
//}
//y se debe invocar desde el tag <ClientSettings>
//de la siguiente forma:
//<ClientEvents OnGridCreated="resizeRadGridWithScroll" />
//AC

function resizeRadGrid(crtl) {
        var grid = $find(crtl)
        grid.repaint();
        setTimeout(function () { grid.repaint(); }, 0);
}

function comprobarnavegador() {
    var respuesta = "0";
    var userAgent = navigator.userAgent.toLowerCase();
    jQuery.browser = {
        version: (userAgent.match(/.+(?:rv|it|ra|ie|me)[\/: ]([\d.]+)/) || [])[1],
        chrome: /chrome/.test(userAgent),
        safari: /webkit/.test(userAgent) && !/chrome/.test(userAgent),
        opera: /opera/.test(userAgent),
        msie: /msie/.test(userAgent) && !/opera/.test(userAgent),
        mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent)
    };

    if ($.browser.msie) {
        switch ($.browser.version.substr(0, 1)) {
            case "7":
                respuesta = "IE7";
                break;
            case "8":
                respuesta = "IE8";
                break;
            case "9":
                respuesta = "IE9";
                break;
        }
    }
    else {
        if ($.browser.chrome)
            respuesta = "chrome";
    }

    return respuesta;
}

function espaciosCampoX(sender) {
    var _campo = document.getElementById(sender.id);
    _campo.value = trim(_campo.value);
}

function trim(myString) {
    return myString.replace(/^\s+/g, '').replace(/\s+$/g, '');
}

function fixRadAlert() {

    var userAgent = navigator.userAgent.toLowerCase();
    jQuery.browser = {
        version: (userAgent.match(/.+(?:rv|it|ra|ie|me)[\/: ]([\d.]+)/) || [])[1],
        chrome: /chrome/.test(userAgent),
        safari: /webkit/.test(userAgent) && !/chrome/.test(userAgent),
        opera: /opera/.test(userAgent),
        msie: /msie/.test(userAgent) && !/opera/.test(userAgent),
        mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent)
    };
    var element = document.createElement('link');
    element.type = 'text/css';
    element.rel = 'stylesheet';

    if ($.browser.msie) {
        switch ($.browser.version.substr(0, 1)) {
            case "7":
                $(".radalert :nth-child(2)").find('.rwInnerSpan').text("Aceptar");
                $(".radalert :nth-child(2)").css("padding-left", "25%");
                break;
            default:
                $(".radalert :nth-child(2)").find('.rwInnerSpan').text("Aceptar");
                $(".radalert :nth-child(2)").css("padding-left", "34%");
        }
    }
    else {

        $(".radalert :nth-child(2)").find('.rwInnerSpan').text("Aceptar");
        $(".radalert :nth-child(2)").css("padding-left", "34%");
    }

}