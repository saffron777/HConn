<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TecladoNumericoRandom.ascx.cs"
	Inherits="HConnexum.Tramitador.Sitio.TecladoNumericoRandom" %>
<script type="text/javascript">
	var valorBtn;
	var focusID = null;
	var range;
	var sem = true;
	var tecant;

	$(function () {
		$('input[type="text"],input[type="password"]').focus(function () {
			focusID = $(this).attr('id');
		});

		$('.vcknbtn').click(function () {
			insertAtCaret(focusID, valorBtn)
		});

		$('.vcknbtn').mouseover(function () {
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

		$('.vcknbtn').mouseleave(function () {
			$(this).val(valorBtn)
			sem = true;
		});

	});

	$(document).ready(function () {
		var array = new Array();

		while (array.length < 10) {
			var temp = Math.round(Math.random() * 9);
			if (!contain(array, temp)) {
				array.push(temp);
			}
		}

		for (i = 0; i < 10; i++) {
			var btn = document.getElementById("vcknbtn" + i);
			btn.value = array[i];
		}
	});

	function contain(array, num) {
		for (var i = 0; i < array.length; i++) {
			if (array[i] == num) {
				return true;
			}
		}
		return false;
	}

	function insertAtCaret(areaId, text) {
		if (areaId != null) {
			var txtarea = document.getElementById(areaId);
			var scrollPos = txtarea.scrollTop;
			var strPos = 0;
			var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ?
                    "ff" : (document.selection ? "ie" : false));
			if (br == "ie") {
				txtarea.focus();
				range = document.selection.createRange();
				range.moveStart('character', -txtarea.value.length);
				strPos = range.text.length;
			}
			else if (br == "ff") strPos = txtarea.selectionStart;

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
	}
</script>
<style type="text/css">
	.vcknbtn
	{
		font-family: "Arial";
		font-size: 12px;
		width: 28px;
	}
</style>
<div id="VirtualKeyNumerico">
	<input id="vcknbtn1" type="button" class="vcknbtn" />
	<input id="vcknbtn2" type="button" class="vcknbtn" />
	<input id="vcknbtn3" type="button" class="vcknbtn" />
	<br />
	<input id="vcknbtn4" type="button" class="vcknbtn" />
	<input id="vcknbtn5" type="button" class="vcknbtn" />
	<input id="vcknbtn6" type="button" class="vcknbtn" />
	<br />
	<input id="vcknbtn7" type="button" class="vcknbtn" />
	<input id="vcknbtn8" type="button" class="vcknbtn" />
	<input id="vcknbtn9" type="button" class="vcknbtn" />
	<br />
	<input id="vcknbtn0" type="button" class="vcknbtn" />
</div>
