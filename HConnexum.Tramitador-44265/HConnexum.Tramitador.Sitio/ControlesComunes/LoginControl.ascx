<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="LoginControl.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.LoginControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ControlesComunes/TecladoNumericoRandom.ascx" TagName="TecladoNumericoRandom" TagPrefix="tnr" %>
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
			$('.fieldsetGeneral').css("width", "400");
			$('fieldset legend').css("width", "395");
			$('#IniciarSesion').css("margin-top", "20px");
			$('#tdAyuda').css("padding-left", "28px");
		}

		if ("<%=TecladoReal%>" == "False") {
			$('.RadTextBox').attr('readonly', 'readonly');
			$('#RadTextBoxPassword').focus();
			$('#RadTextBoxLogin').focus();
		}

	});

	$(function () {

		$('.RadTextBox').focus(function () {
			focusID = $(this).attr('id');
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
				if (jQuery.trim($('#RadTextBoxLogin').val()) == "") {
					$('#ButtonIniciarSesion').attr("disabled", "disabled");
				}
				else {
					$('#ButtonIniciarSesion').removeAttr("disabled");
				}
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
		var tbInput = document.getElementById(focusID);
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

	function mostrarTeclado() {
		if (tec == false) {
			$('#divTeclado').show();
			tec = true;
		}
		else {
			$('#divTeclado').hide();
			tec = false;
		}
	};

	function mostrarPresion() {
		var element = document.getElementById("icotec");
		element.src = "../Imagenes/keyboard-icon_p.gif";
	};

	function mostrarSinPresion() {
		var element = document.getElementById("icotec");
		element.src = "../Imagenes/keyboard-icon.gif";
	};

	function ayuda() {
		var oWindow = $find("<%= RadWindowAyuda.ClientID %>");
		oWindow.show();
	};

	function recuperar() {
		$("#infoRecuperar_0").attr("checked", "checked");
		var oWindow = $find("<%= RadWindowRecuperar.ClientID %>");
		oWindow.show();
	};




</script>
<style type="text/css">
	.vckl
	{
		font-family: "Arial";
		font-size: 12px;
		width: 28px;
	}
	
	.button
	{
		font-family: "Arial";
		font-size: 12px;
	}
	
	.blank
	{
		font-family: "Arial";
		font-size: 12px;
		width: 160px;
	}
	
	.fieldsetGeneral
	{
		width: 610px;
		padding: 5px;
		border-width: 2px;
		border-color: #4b6c9e;
	}
	
	fieldset legend
	{
		background: #4b6c9e;
		padding: 2px;
		font-weight: normal;
		color: White;
		width: 605px;
	}
	
	.fieldsetAnidadoLog
	{
		margin-left: 20px;
		width: 320px;
		border-radius: 8px;
		-webkit-border-radius: 8px;
		-moz-border-radius: 8px;
		padding: 5px;
		border-width: 2px;
		border-color: #4b6c9e;
	}
	
	.fieldsetAnidadoTec
	{
		margin-left: 20px;
		width: 560px;
		border-radius: 8px;
		-webkit-border-radius: 8px;
		-moz-border-radius: 8px;
		padding: 5px;
		border-width: 2px;
		border-color: #4b6c9e;
	}
	
	.RadTextBox
	{
		width: 200px;
	}
	
	a:hover
	{
		cursor: pointer;
		color: Blue;
		text-decoration: underline;
	}
	
	a
	{
		color: Blue;
		text-decoration: underline;
	}
	
	.ventana1
	{
		width: 300px;
		height: 150px;
		background-color: #F4F4F4;
		border-color: #4b6c9e;
		border-width: medium;
		border-style: solid;
	}
	
	.ventana2
	{
		width: 350px;
		height: 150px;
		background-color: #F4F4F4;
		border-color: #4b6c9e;
		border-width: medium;
		border-style: solid;
	}
	
	.titulo
	{
		background-color: #4b6c9e;
		color: White;
	}
	
	.x
	{
		width: 8px;
		background-color: #4b6c9e;
		color: White;
	}
	
	.xa
	{
		color: White;
		text-decoration: none;
	}
	
	.xa:hover
	{
		cursor: pointer;
		color: Navy;
		text-decoration: none;
	}
	
	.parrafo
	{
		margin-left: 20px;
		margin-right: 20px;
		margin-bottom: 20px;
		margin-top: 5px;
	}
</style>
<fieldset class="fieldsetGeneral">
	<legend>Acceso </legend>
	<fieldset class="fieldsetAnidadoLog">
		<table width="400">
			<tr>
				<td align="right">
					<strong>Usuario:</strong>
				</td>
				<td align="left">
					<asp:TextBox ID="RadTextBoxLogin" CssClass="RadTextBox" MaxLength="50" ClientIDMode="Static" runat="server"></asp:TextBox>*
				</td>
			</tr>
			<tr>
				<td align="right">
					<strong>Clave:</strong>
				</td>
				<td align="left">
					<asp:TextBox ID="RadTextBoxPassword" CssClass="RadTextBox" MaxLength="50" ClientIDMode="Static" TextMode="Password" runat="server"></asp:TextBox>*
				</td>
			</tr>
		</table>
	</fieldset>
	<table width="600">
		<tr>
			<td align="right" class="NoTeclado">
				<strong>Teclado Virtual: </strong>
			</td>
			<td align="left" class="NoTeclado">
				<img id="icotec" src="<%= ResolveClientUrl(@"~/Imagenes/keyboard-icon.gif") %>" alt="Teclado" onmouseup="mostrarSinPresion();" onmousedown="mostrarPresion();" onclick="mostrarTeclado();" />
			</td>
			<td id="tdAyuda">
				<strong><a onclick="ayuda();">Ayuda</a> </strong>
			</td>
			<td>
				<strong><a onclick="recuperar();">Recuperar datos de acceso</a> </strong>
			</td>
		</tr>
	</table>
	<div class="parrafo NoTeclado">
		Por su seguridad, le recomendamos ingresar su contraseña utilizando el Teclado Virtual. Los dígitos numéricos del teclado cambiarán de posición cada vez que ingrese a H-Connexum.
	</div>
	<div id="divTeclado" style="display: none;">
		<fieldset class="fieldsetAnidadoTec">
			<table>
				<tr>
					<td colspan="2" align="center">
						<div id="VirtualKeyLetras">
							<input id="vckl1" type="button" value="!" class="vckl" />
							<input id="vckl2" type="button" value="@" class="vckl" />
							<input id="vckl3" type="button" value='"' class="vckl" />
							<input id="vckl4" type="button" value="#" class="vckl" />
							<input id="vckl5" type="button" value="$" class="vckl" />
							<input id="vckl6" type="button" value="%" class="vckl" />
							<input id="vckl7" type="button" value="&" class="vckl" />
							<input id="vckl8" type="button" value="/" class="vckl" />
							<input id="vckl9" type="button" value="(" class="vckl" />
							<input id="vckl10" type="button" value=")" class="vckl" />
							<input id="vckl11" type="button" value="=" class="vckl" />
							<input id="vckl12" type="button" value="?" class="vckl" />
							<br />
							<input id="vckl13" type="button" value="q" class="vckl" />
							<input id="vckl14" type="button" value="w" class="vckl" />
							<input id="vckl15" type="button" value="e" class="vckl" />
							<input id="vckl16" type="button" value="r" class="vckl" />
							<input id="vckl17" type="button" value="t" class="vckl" />
							<input id="vckl18" type="button" value="y" class="vckl" />
							<input id="vckl19" type="button" value="u" class="vckl" />
							<input id="vckl20" type="button" value="i" class="vckl" />
							<input id="vckl21" type="button" value="o" class="vckl" />
							<input id="vckl22" type="button" value="p" class="vckl" />
							<input id="vckl23" type="button" value="`" class="vckl" />
							<input id="vckl24" type="button" value="+" class="vckl" />
							<input id="vckl25" type="button" value="Borrar" class="button" onclick="del();" />
							<br />
							<input id="vckl26" type="button" value="May" class="button" onclick="mayus();" />
							<input id="vckl27" type="button" value="a" class="vckl" />
							<input id="vckl28" type="button" value="s" class="vckl" />
							<input id="vckl29" type="button" value="d" class="vckl" />
							<input id="vckl30" type="button" value="f" class="vckl" />
							<input id="vckl31" type="button" value="g" class="vckl" />
							<input id="vckl32" type="button" value="h" class="vckl" />
							<input id="vckl33" type="button" value="j" class="vckl" />
							<input id="vckl34" type="button" value="k" class="vckl" />
							<input id="vckl35" type="button" value="l" class="vckl" />
							<input id="vckl36" type="button" value="ñ" class="vckl" />
							<input id="vckl37" type="button" value="´" class="vckl" />
							<input id="vckl38" type="button" value="ç" class="vckl" />
							<br />
							<input id="vckl39" type="button" value="<" class="vckl" />
							<input id="vckl40" type="button" value="z" class="vckl" />
							<input id="vckl41" type="button" value="x" class="vckl" />
							<input id="vckl42" type="button" value="c" class="vckl" />
							<input id="vckl43" type="button" value="v" class="vckl" />
							<input id="vckl44" type="button" value="b" class="vckl" />
							<input id="vckl45" type="button" value="n" class="vckl" />
							<input id="vckl46" type="button" value="m" class="vckl" />
							<input id="vckl47" type="button" value="," class="vckl" />
							<input id="vckl48" type="button" value="." class="vckl" />
							<input id="vckl49" type="button" value="-" class="vckl" />
							<br />
							<input id="vckl50" type="button" value="" class="blank" onclick="blank();" />
						</div>
					</td>
					<td>
						&nbsp;
					</td>
					<td align="center">
						<tnr:TecladoNumericoRandom ID="TecladoNumericoRandom" runat="server" />
					</td>
				</tr>
			</table>
		</fieldset>
	</div>
	<div id="IniciarSesion">
		<center>
			<asp:Button ID="ButtonIniciarSesion" ClientIDMode="Static" runat="server" Text="Iniciar Sesión" Enabled="true" OnClick="btnIniciarSesion_Click" ValidationGroup="LoginGroup"/>
		</center>
	</div>
	<br />
</fieldset>
<telerik:RadWindow CssClass="RadWindow" ID="RadWindowAyuda" runat="server" Visible="True" Title="Ayuda" VisibleOnPageLoad="False" VisibleStatusbar="False" EnableViewState="False" Animation="None" AutoSize="False" Behaviors="Close, Move" Modal="True" BorderStyle="Dotted" VisibleTitlebar="True" EnableTheming="False"
	BorderWidth="10" Height="195" Overlay="False" Width="322">
	<ContentTemplate>
		<div class="ventana1" id="Div1">
			<table border="0" width="300" cellspacing="0" cellpadding="0">
				<tr>
					<td colspan="2">
						<table width="280" cellspacing="10px">
							<tr>
								<td>
									<table width="150">
										<tr>
											<td>
												Demo:
											</td>
											<td>
												<input type="button" value="Demo" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<a>Preguntas frecuentes</a>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									Soporte telefónico: xxx-xxx-xxx / xxx-xxx-xxx
								</td>
							</tr>
							<tr>
								<td colspan="2">
									Soporte: soporte@h-connexum.com
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</ContentTemplate>
</telerik:RadWindow>
<telerik:RadWindow CssClass="RadWindow" ID="RadWindowRecuperar" runat="server" Visible="True" Title="Recuperar datos de Acceso" VisibleOnPageLoad="False" VisibleStatusbar="False" EnableViewState="False" Animation="None" AutoSize="False" Behaviors="Close, Move" Modal="True" BorderStyle="Dotted" VisibleTitlebar="True"
	EnableTheming="False" BorderWidth="10" Height="195" Overlay="False" Width="372">
	<ContentTemplate>
		<div class="ventana2" id="Div2">
			<table width="350" cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td>
						<table width="350" cellspacing="10px">
							<tr>
								<td>
									Recuperar Usuario
								</td>
								<td>
									<asp:RadioButton runat="server" ID="infoRecuperar_0" ClientIDMode="Static" GroupName="Recuperar" />
								</td>
								<td>
									Recuperar Contraseña
								</td>
								<td>
									<asp:RadioButton runat="server" ID="infoRecuperar_1" ClientIDMode="Static" GroupName="Recuperar" />
								</td>
							</tr>
							<tr>
								<td colspan="4">
									E-mail:&nbsp;&nbsp;<asp:TextBox ID="TxtEmail" ClientIDMode="Static" runat="server" MaxLength="200" />
								</td>
							</tr>
							<tr>
								<td colspan="4" align="center">
									<asp:Button ID="btnRecuperar" runat="server" Text="Recuperar" Enabled="true" ClientIDMode="Static" OnClick="btnRecuperar_Click" ValidationGroup="LoginRecuperarGroup" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>
	</ContentTemplate>
</telerik:RadWindow>
<telerik:RadInputManager ID="LoginControlRadInputManager" runat="server" >
	<telerik:TextBoxSetting ErrorMessage="Campo Obligatorio" EmptyMessage="Requerido" Validation-IsRequired="true" Validation-ValidationGroup="LoginGroup">
		<TargetControls>
			<telerik:TargetInput ControlID="RadTextBoxLogin" />
		</TargetControls>
		<Validation IsRequired="True" ValidationGroup="LoginGroup"></Validation>
	</telerik:TextBoxSetting>
	<telerik:TextBoxSetting Validation-IsRequired="true" Validation-ValidationGroup="LoginGroup">
		<TargetControls>
			<telerik:TargetInput ControlID="RadTextBoxPassword" />
		</TargetControls>
		<Validation IsRequired="True" ValidationGroup="LoginGroup"></Validation>
	</telerik:TextBoxSetting>
	<telerik:RegExpTextBoxSetting EmptyMessage="Campo Requerido" ErrorMessage="Email Invalido" IsRequiredFields="True" Validation-IsRequired="true" Validation-ValidationGroup="LoginGroup" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
		<TargetControls>
			<telerik:TargetInput ControlID="TxtEmail" />
		</TargetControls>
		<Validation IsRequired="True" ValidationGroup="LoginRecuperarGroup"></Validation>
	</telerik:RegExpTextBoxSetting>
</telerik:RadInputManager>
