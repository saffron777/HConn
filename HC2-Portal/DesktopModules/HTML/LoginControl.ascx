<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="LoginControl.ascx.cs" Inherits="Login.LoginControl" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="TecladoNumericoRandom.ascx" TagPrefix="tnr" TagName="TecladoNumericoRandom" %>


<style type="text/css">
    .ui-dialog-content .ui-widget-content {
        padding-top: 0px;
        width: 100% !important;
        overflow: hidden !important;
        width: 658px;
        position: relative;
        border: 0;
        background: black;
        overflow: auto;
    }

    .dnnFormPopup .ui-resizable-se {
        width: 14px;
        height: 14px;
        float: right;
        padding-top: 0px;
        width: 100% !important;
        overflow: hidden !important;
        width: 658px;
        position: relative;
        border: 0;
        background: black;
        overflow: auto;
    }
    
    body { overflow:hidden; }
    
    </style>

<script type="text/javascript">
    var valorBtn;
    var may = false;
    var tec = false;
    var focusID;
    var range;
    var sem = true;
    var tecant;

    $(document).ready(function () {
        if ("<%=TecladoVirtual%>" == "False") {
            $('.NoTeclado').hide();
            $('.fieldsetGeneral').css("width", "400px");
            $('.fieldsetlegend').css("width", "700px");
            $('#IniciarSesion').css("margin-top", "20px");
            $('#tdAyuda').css("padding-left", "28px");
        }

        if ("<%=TecladoReal%>" == "False") {
            //$('.RadTextBox').attr('readonly', 'readonly');
            $('#RadTextBoxPassword').focus();
            $('#RadTextBoxLogin').focus();
        }
    });

    $(function () {

        $('.RadTextBox').focus(function () {
            focusID = $(this).attr('ID');
        });

        $('.RadTextBoxPassword').focus(function () {
            focusID = $(this).attr('ID');
        });

        $('.RadTextBox').keyup(function () {
            if (jQuery.trim($('#RadTextBoxLogin').val()) == "" || jQuery.trim($('#RadTextBoxPassword').val()) == "") {
                $('#ButtonIniciarSesion').attr("disabled", "disabled");
            }
            else {
                $('#ButtonIniciarSesion').removeAttr("disabled");
            }
        });

        $('#TxtEmail').keyup(function () {
            if (jQuery.trim($(this).val()) == "") {
                $('#btnRecuperar').attr("disabled", "disabled");
            }
            else {
                $('#btnRecuperar').removeAttr("disabled");
            }
        });

        $('.vckl').click(function () {
            if (!(focusID == "RadTextBoxLogin" && !((valorBtn.charCodeAt(0) >= 97 && valorBtn.charCodeAt(0) <= 122) || (valorBtn.charCodeAt(0) >= 65 && valorBtn.charCodeAt(0) <= 90) || valorBtn.charCodeAt(0) == 209 || valorBtn.charCodeAt(0) == 241 || valorBtn.charCodeAt(0) == 199 || valorBtn.charCodeAt(0) == 231))) {
                insertAtCaret(focusID, valorBtn)
                //if (jQuery.trim($('#RadTextBoxLogin').val()) == "") {
                //    $('#ButtonIniciarSesion').attr("disabled", "disabled");
                //}
                //else {
                //    $('#ButtonIniciarSesion').removeAttr("disabled");
                //}
            }
        });

        $('.vckl').mouseover(function () {
            if (sem == true) {
                sem = false
                tecant = $(this);
                valorBtn = $(this).val()
                $(this).val('*')
            }
            else {
                tecant.val(valorBtn);
                tecant = $(this);
                valorBtn = $(this).val()
                $(this).val('*')
            }
        });

        $('.vckl').mouseleave(function () {
            $(this).val(valorBtn)
            sem = true;
        });

        $('.vckl').mouseleave(function () {
            $(this).val(valorBtn)
            sem = true;
        });

    });

    function insertAtCaret(areaId, text) {
        if (areaId != null) {
            var txtarea = document.getElementById(areaId);
            var scrollPos = txtarea.scrollTop;
            var strPos = 0;
            var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ? "ff" : (document.selection ? "ie" : false));
            if (br == "ie") {
                txtarea.focus();
                range = document.selection.createRange();
                range.moveStart('character', -txtarea.value.length);
                strPos = range.text.length;
            }
            else if (br == "ff") {
                strPos = txtarea.selectionStart;
            }

            var front = (txtarea.value).substring(0, strPos);
            var back = (txtarea.value).substring(strPos, txtarea.value.length);
            txtarea.value = front + text + back;
            strPos = strPos + text.length;
            if (br == "ie") {
                txtarea.focus();
                range = document.selection.createRange();
                range.moveStart('character', -txtarea.value.length);
                range.moveStart('character', strPos);
                range.moveEnd('character', 0);
                range.select();
            }
            else if (br == "ff") {
                txtarea.selectionStart = strPos;
                txtarea.selectionEnd = strPos;
                txtarea.focus();
            }
            txtarea.scrollTop = scrollPos;
        }
    };

    function del() {
        var tbInput;
        tbInput = document.getElementById(focusID);
        tbInput.value = tbInput.value.substr(0, tbInput.value.length - 1);
        if (jQuery.trim($('#RadTextBoxLogin').val()) == "") {
            $('#ButtonIniciarSesion').attr("disabled", "disabled");
        }
        else {
            $('#ButtonIniciarSesion').removeAttr("disabled");
        }
    };

    function blank() {
        insertAtCaret(focusID, " ")
    };

    function mayus() {
        if (may == false) {
            $('.vckl').each(function () {
                $(this).val($(this).val().toUpperCase());
            });
            may = true;
        }
        else {
            $('.vckl').each(function () {
                $(this).val($(this).val().toLowerCase());
            });
            may = false;
        }
    };

    function mostrarTeclado(ControlID) {
        if (tec == false) {
            $('#divTeclado').show();
            tec = true;
            coordenadas(ControlID);
        }
        else {
            $('#divTeclado').hide();
            tec = false;
        }
    };

    function mostrarPresion() {
        var element = document.getElementById("icotec");
        element.src = "DesktopModules/HTML/Loginhc2/Imagenes/keyboard-icon_p.gif";
        element.style.cursor = 'pointer';
    };

    function mostrarSinPresion() {
        var element = document.getElementById("icotec");
        element.src = "DesktopModules/HTML/Loginhc2/Imagenes/keyboard-icon.gif";
        element.style.cursor = 'pointer';
    };

    function ayuda() {
        var oWindow = $find("<%= RadWindowAyuda.ClientID %>");
        oWindow.show();
    };

    function recuperar() {
        $("#infoRecuperar_0").attr("checked", "checked");
        var oWindow = $find("<%= RadWindowRecuperar.ClientID %>");
        oWindow.show();
        var email = document.getElementById("<%= TxtEmail.ClientID %>");
        email.value = "";
    };

    getDimensions = function (oElement) {
        var x, y, w, h;
        x = y = w = h = 0;
        if (document.getBoxObjectFor) { // Mozilla
            var oBox = document.getBoxObjectFor(oElement);
            x = oBox.x - 1;
            w = oBox.width;
            y = oBox.y - 1;
            h = oBox.height;
        }
        else if (oElement.getBoundingClientRect) { // IE
            var oRect = oElement.getBoundingClientRect();
            x = oRect.left - 2;
            w = oElement.clientWidth;
            y = oRect.top - 2;
            h = oElement.clientHeight;
        }
        return { x: x, y: y, w: w, h: h };
    }

    function coordenadas(ControlID) {
        var control = document.getElementById(ControlID.id);
        var DivControl = document.getElementById("principal")
        var capa = document.getElementById("divTeclado");
        capa.style.display = "block";
        var xDiv = getDimensions(DivControl).x;
        var yDiv = getDimensions(DivControl).y;
        var posAlaIzq;
        capa.style.left = 1 + "px";
        var CtrlExtras = document.getElementById("<%= extras.ClientID %>");
        if (CtrlExtras.style.display == "none")
            capa.style.top = -180 + "px";
        else
            capa.style.top = -255 + "px";
    }
</script>
<div id="principal" style="position: absolute; top: 0px; left: 0px; height:250px;" >
	<table width="640px">
		<tr>
			<td>
				<table width="100%">
					<tr>
						<td style="text-align: right"><img alt="" src="DesktopModules/HTML/Loginhc2/Imagenes/logo.png" /></td>
					</tr>
					<tr>
						<td>
							<div style="width: 100%;">
								<table width="100%">
									<tr>
										<td style="width: 150px; text-align: right;"></td>
										<td style="width: 1px;">&nbsp;</td>
										<td style="width: 160px;">&nbsp;</td>
										<td style="width: 130px;">&nbsp;</td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td style="width: 150px; text-align: right;">Nombre de Usuario:</td>
										<td style="width: 1px;">&nbsp;</td>
										<td style="width: 160px;">
											<asp:TextBox ID="RadTextBoxLogin" CssClass="RadTextBox" MaxLength="50" ClientIDMode="Static" runat="server" Width="150px" ValidationGroup="LoginGroup"></asp:TextBox></td>
										<td style="width: 130px;">
											<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadTextBoxLogin"
												ForeColor="Red" ValidationGroup="LoginGroup" ErrorMessage="*" />
										</td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td style="width: 150px; text-align: right;">Contraseña:</td>
										<td style="width: 1px;">&nbsp;</td>
										<td style="width: 160px;">
											<asp:TextBox ID="RadTextBoxPassword" CssClass="RadTextBox" MaxLength="50" ClientIDMode="Static" TextMode="Password" runat="server" Width="150px" ValidationGroup="LoginGroup"></asp:TextBox></td>
										<td style="width: 130px;">
											<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadTextBoxPassword"
												ForeColor="Red" ValidationGroup="LoginGroup" ErrorMessage="*" />
											&nbsp;<strong>Teclado Virtual:&nbsp;&nbsp;</strong>
										</td>
										<td>
											<img id="icotec" src="DesktopModules/HTML/Loginhc2/Imagenes/keyboard-icon.gif" alt="Teclado"
												onmouseup="mostrarSinPresion();" onmousedown="mostrarPresion();" onclick="mostrarTeclado(this);"
												onmouseover="this.style.cursor='pointer';" />
										</td>
									</tr>
									<tr>
										<td style="width: 150px; text-align: right;">&nbsp;</td>
										<td style="width: 1px;">&nbsp;</td>
										<td style="width: 160px;">
											<asp:Button runat="server" Text="INICIAR SESIÓN" ForeColor="White"
												AlternateText="Iniciar Sesión" OnClick="btnIniciarSesion_Click" ValidationGroup="LoginGroup"
												BackColor="#152471" BorderColor="#152471" BorderStyle="Solid" ID="btnIniciarSesion" Font-Names="Arial"
												Font-Size="8pt" Width="100px" />
										</td>
										<td style="width: 130px;">&nbsp;</td>
										<td>&nbsp;</td>
									</tr>
								</table>

								<table width="100%">
									<tr>
										<td style="width: 150px; text-align: right;">&nbsp;</td>
										<td style="width: 1px;">&nbsp;</td>
										<td style="width: 200px;"><strong><a href="javascript:recuperar();">Recuperar datos de acceso</a></strong></td>
										<td style="width:120px;"></td>
										<td>&nbsp;</td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td><hr /></td>
		</tr>
		<tr>
			<td>
				<div id="extras" runat="server">
					<table width="100%">
						<tr>
							<td><span style="color: #152471;">Si aún no te has registrado</span></td>
							<td>
								<asp:Button ID="btnRegistro" runat="server" AlternateText="Cerrar" BackColor="#152471" BorderColor="#152471"
									BorderStyle="Solid" ClientIDMode="Static" Font-Names="Arial" Font-Size="8pt"
									ForeColor="White" Text="HAZ CLICK AQUÍ" Width="100px" />
							</td>
						</tr>
						<tr>
							<td colspan="2" style="height:5px;"></td>
						</tr>
						<tr>
							<td><span style="color: #152471;">Si deseas conectarte a la versión anterior</span></td>
							<td>
								<asp:Button ID="btnVersionAnt" runat="server" AlternateText="Cerrar" BackColor="#152471" BorderColor="#152471"
									BorderStyle="Solid" ClientIDMode="Static" Font-Names="Arial" Font-Size="8pt"
									ForeColor="White" Text="HAZ CLICK AQUÍ" Width="100px" />
							</td>
						</tr>
						<tr>
							<td colspan="2" style="height:5px;"></td>
						</tr>
					</table>
				</div>
			</td>
		</tr>
		<tr>
			<td style="text-align: justify; color: #152471;">
				<span>Por su seguridad, le recomendamos ingresar su contraseña utilizando el Teclado Virtual.
					Los dígitos numéricos del teclado cambiarán de posición cada vez que ingrese a H-Connexum.
					Si necesitas ayuda para conectarte, comunícate con nosotros al (0212)- 5050539.
				</span>
			</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
		</tr>
	</table>

	<telerik:RadWindowManager ID="singleton" runat="server" EnableShadow="True" EnableEmbeddedSkins="False"></telerik:RadWindowManager>

	<telerik:RadWindow ID="RadWindowAyuda" runat="server"
		Visible="True" Title="Ayuda" VisibleOnPageLoad="False" VisibleStatusbar="False"
		EnableViewState="False" Animation="None" AutoSize="False"
		Behaviors="Close, Move" Modal="True" BorderStyle="Dotted"
		VisibleTitlebar="True" EnableTheming="False"
		BorderWidth="10" Height="225px" Overlay="False" Width="450px">
		<contenttemplate>
			<div id="Div1" class="button">
				<table border="0" width="300px" cellspacing="0" cellpadding="0">
					<tr>
						<td colspan="2">
							<table width="280px" cellspacing="10px">
								<tr>
									<td>
										<table width="150px">
											<tr>
												<td>Demo:</td>
												<td><input type="button" value="Demo" /></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td colspan="2"><a>Preguntas frecuentes</a></td>
								</tr>
								<tr>
									<td colspan="2">Soporte telefónico: xxx-xxx-xxx / xxx-xxx-xxx</td>
								</tr>
								<tr>
									<td colspan="2">Soporte: soporte@h-connexum.com</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
		</contenttemplate>
	</telerik:RadWindow>

	<telerik:RadWindow ID="RadWindowRecuperar" runat="server"
		Visible="True" Title="Recuperar datos de Acceso" VisibleOnPageLoad="False"
		VisibleStatusbar="False" EnableViewState="False" Animation="None"
		AutoSize="False" Behaviors="Close, Move" Modal="True" BorderStyle="Dotted" VisibleTitlebar="True"
		EnableTheming="False" BorderWidth="10" Height="205px" Overlay="False"
		Width="429px">
		<contenttemplate>
			<div id="Div2" class="button">
				<table width="399px" cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td>
							<table cellspacing="0" style="width: 400px" border="0">
								<tr>
									<td colspan="2">&nbsp;</td>
								</tr>
								<tr>
									<td style="width:5px;"><asp:RadioButton runat="server" ID="infoRecuperar_0" ClientIDMode="Static" GroupName="Recuperar" /></td>
									<td>Recuperar Usuario</td>
								</tr>
								<tr>
									<td style="width:5px;"><asp:RadioButton runat="server" ID="infoRecuperar_1" ClientIDMode="Static" GroupName="Recuperar" /></td>
									<td>Recuperar Contraseña</td>
								</tr>
								<tr>
									<td style="width:5px;"></td>
									<td colspan="2"><strong>Correo electrónico:</strong>&nbsp;&nbsp;<asp:TextBox ID="TxtEmail" ClientIDMode="Static" runat="server" MaxLength="200" Width="98%" /></td>
								</tr>
								<tr>
									<td colspan="2" align="center">&nbsp;</td>
								</tr>
								<tr>
									<td style="width:5px;">&nbsp;</td>
									<td style="text-align:right;">
										<asp:Button ID="btnRecuperar" runat="server" Text="Recuperar" Enabled="true" ClientIDMode="Static" OnClick="btnRecuperar_Click" ValidationGroup="LoginRecuperarGroup" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
		</contenttemplate>
	</telerik:RadWindow>

	<telerik:RadInputManager ID="LoginControlRadInputManager" runat="server">
		<telerik:RegExpTextBoxSetting EmptyMessage="Campo Requerido" ErrorMessage="Email Invalido" IsRequiredFields="True" Validation-IsRequired="true" Validation-ValidationGroup="LoginGroup" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
			<TargetControls>
				<telerik:TargetInput ControlID="TxtEmail" />
			</TargetControls>
			<Validation IsRequired="True" ValidationGroup="LoginRecuperarGroup"></Validation>
		</telerik:RegExpTextBoxSetting>
	</telerik:RadInputManager>
 	
	<div id="divTeclado" style="margin: 0 auto 0 auto; width: 590px; border: 1px solid #152471; position: relative; display: none; background-color: white;">
		<fieldset>
			<table>
				<tr>
					<td align="center">
						<div id="Div4" style="padding-top: 3px; padding-bottom: 3px;">
							<input id="Button1" type="button" value="!" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button2" type="button" value="@" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button3" type="button" value='"' class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button4" type="button" value="#" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button5" type="button" value="$" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button6" type="button" value="%" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button7" type="button" value="&" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button8" type="button" value="/" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button9" type="button" value="(" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button10" type="button" value=")" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button11" type="button" value="=" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button12" type="button" value="?" class="vckl" style="width: 30px; height: 30px;" />
							<br />
							<input id="Button13" type="button" value="q" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button14" type="button" value="w" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button15" type="button" value="e" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button16" type="button" value="r" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button17" type="button" value="t" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button18" type="button" value="y" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button19" type="button" value="u" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button20" type="button" value="i" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button21" type="button" value="o" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button22" type="button" value="p" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button23" type="button" value="`" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button24" type="button" value="+" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button25" type="button" value="Borrar" class="button" onclick="del();" />
							<br />
							<input id="Button26" type="button" value="May" class="button" onclick="mayus();" />
							<input id="Button27" type="button" value="a" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button28" type="button" value="s" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button29" type="button" value="d" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button30" type="button" value="f" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button31" type="button" value="g" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button32" type="button" value="h" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button33" type="button" value="j" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button34" type="button" value="k" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button35" type="button" value="l" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button36" type="button" value="ñ" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button37" type="button" value="´" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button38" type="button" value="ç" class="vckl" style="width: 30px; height: 30px;" />
							<br />
							<input id="Button39" type="button" value="<" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button40" type="button" value="z" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button41" type="button" value="x" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button42" type="button" value="c" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button43" type="button" value="v" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button44" type="button" value="b" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button45" type="button" value="n" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button46" type="button" value="m" class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button47" type="button" value="," class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button48" type="button" value="." class="vckl" style="width: 30px; height: 30px;" />
							<input id="Button49" type="button" value="-" class="vckl" style="width: 30px; height: 30px;" />
							<br />
							<input id="Button50" type="button" value="" class="blank" onclick="blank();" style="width: 300px; height: 30px;" />
						</div>
					</td>
					<td>&nbsp;</td>
					<td align="center"><tnr:TecladoNumericoRandom ID="TecladoNumericoRandom1" runat="server" /></td>
				</tr>
			</table>
		</fieldset>
	</div>
</div>