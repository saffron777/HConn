<%@ Page Language="C#" CodeBehind="Default.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Default" %>

<%@ Register Src="~/ControlesComunes/Alerta.ascx" TagName="Alerta" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/ControlHeader.ascx" TagName="ControlHeader" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/ControlFooter.ascx" TagName="ControlFooter" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Toolbar.ascx" TagName="ToolBar" TagPrefix="hcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>HC-Tramitador</title>
		<link href="Temas/Site.css" type="text/css" rel="stylesheet" />
		<script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
		<style type="text/css">
			/*html { overflow-x:hidden; }*/ /*Esta etiqueta causa fallas en Internet Explorer 8 "Bloquea los campos de los bloques"*/
			.rdPlaceHolder
			{
			border: 0 !important;
			background: transparent !important;
			}
			.topeIE7
			{
			width: 100% !important;
			}
			body
			{
			margin: 0px;
			padding: 0px;
			height: 100%;
			width: 100%;
			background-color: #d1e6f7;
			background-image: url('imagenes/PixelGradado.jpg');
			background-repeat: repeat-x;
			overflow: hidden;
			}
			div#piepagina
			{
			position: fixed;
			background-color: black;
			color: white;
			font-family: Segoe UI, Arial, Sans-Serif;
			font-size: 12px;
			bottom: 0;
			display: block;
			width: 100%;
			z-index: 5000;
			height: 30px;
			}
			#bgSection
			{
			position: fixed;
			background: #d1e6f7 url('Imagenes/HConnexumTramitador.jpg');
			top: 50%;
			left: 50%;
			top: 0;
			margin-left: -510px;
			width: 1024px;
			height: 768px;
			}
			.RadNotification
			{
			z-index: 1000000 !important;
			}
		</style>
		<script language="javascript" type="text/javascript">
			$(document).ready(function () {
				changeTextRadAlert();
			});

			function tamañoRestrictionZone() {
				$("#Singleton").css("height", $("#RestrictionZone").height());
				$("#Singleton").height($("#RestrictionZone").height());
			}
			$(window).resize(function () {
				var version = comprobarnavegador();
				if (version == "IE7")
					tamañoRestrictionZone();
			});

		</script>
	</head>
	<body>
		<form id="form1" runat="server">
			<%--.....................:: [ NO BORRAR ESTA TABLA ] ::.................... --%>
			<div id="bgSection">
			</div>
			<table cellpadding="0" cellspacing="0" align="center">
				<tr>
					<td>
					</td>
				</tr>
			</table>
			<%--..:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::.. --%>
			<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
				<script type="text/javascript">
					var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
				</script>
			</telerik:RadScriptBlock>
			<script type="text/javascript">

				//Funciones para el manejo preventivo de vencimiento de sesion

				var mainLblCounter = null;
				var timeLeftCounter = null;
				var seconds = 60;
				var secondsBeforeShow = 60;
				var mainLabel;
				var modalDiv = null;

				//start the main label counter when the page loads
				function pageLoad() {
					var xmlPanel = $find("<% = RadNotification1.ClientID %>")._xmlPanel;
					xmlPanel.set_enableClientScriptEvaluation(true);
					mainLabel = $get("mainLbl");
					resetTimer("mainLblCounter", updateMainLabel, 1000);
				};

				//stop timers for UI
				function stopTimer(timer) {
					clearInterval(this[timer]);
					this[timer] = null;
				};

				//reset timers for UI
				function resetTimer(timer, func, interval) {
					this.stopTimer(timer);
					this[timer] = setInterval(Function.createDelegate(this, func), interval);
				};

				function OnClientShowing(sender, args) {
					//deal with UI labels
					resetTimer("timeLeftCounter", UpdateTimeLabel, 1000);
					stopTimer("mainLblCounter");
				}

				function updateMainLabel(toReset) {
					secondsBeforeShow = (toReset == true) ? 60 : secondsBeforeShow - 1;
				}

				function OnClientHidden() {
					updateMainLabel(true);
					resetTimer("mainLblCounter", updateMainLabel, 1000);
				}

				//-----------------------end of code related only to the demo UI --------------------------------------//

				//update the text in the label in RadNotification
				//this could also be done automatically by using UpdateInterval. However, this will cause callbacks [which is the second best solution than javascript] on every second that is being count
				function UpdateTimeLabel(toReset) {
					var sessionExpired = (seconds == 0);
					if (sessionExpired) {
						stopTimer("timeLeftCounter");
						var notification = $find("<%= RadNotification1.ClientID %>");

						notification.set_showInterval(0); //change the timer to avoid untimely showing, 0 disables automatic showing
						notification.hide();

						stopTimer("timeLeftCounter");
					}
					else {
						var timeLbl = $get("timeLbl");
						timeLbl.innerHTML = seconds--;
					}
				}

				function ContinueSession() {
					var notification = $find("<%= RadNotification1.ClientID %>");
					notification.update();
					notification.hide();

					var showIntervalStorage = notification.get_showInterval();
					notification.set_showInterval(0);
					notification.set_showInterval(showIntervalStorage);

					stopTimer("timeLeftCounter");
					seconds = 60;
					updateMainLabel(true);
				}

				//Funciones para el manejo preventivo de vencimiento de sesion
			</script>
			<telerik:RadNotification ID="RadNotification1" runat="server" Position="Center" Width="380"
									 Height="100" OnCallbackUpdate="OnCallbackUpdate" OnClientShowing="OnClientShowing"
									 OnClientHidden="OnClientHidden" LoadContentOn="PageLoad" AutoCloseDelay="60000"
									 Title="Información" TitleIcon="" Skin="Office2007" EnableRoundedCorners="true"
									 ShowCloseButton="false" CssClass="confirm">
				<ContentTemplate>
					<div class="infoIcon" align="center" style="float: left; width: 60px;">
						<img src="Imagenes/Alert.png" alt="info icon" />
					</div>
					<div class="notificationContent" align="left" style="float: right; width: 300px;">
						Tiempo restante:&nbsp; <span id="timeLbl">60</span>
						<br />
					</div>
					<div align="center" style="width: 330px;">
						<telerik:RadButton Skin="Office2007" ID="continueSession" runat="server" Text="Renovar Sesión"
										   Style="margin-top: 10px;" AutoPostBack="false" OnClientClicked="ContinueSession">
						</telerik:RadButton>
					</div>
				</ContentTemplate>
			</telerik:RadNotification>
			<telerik:RadScriptManager ID="rsmPrincipal" runat="server" />
			<telerik:RadAjaxManager ID="RadAjaxManager1" OnAjaxRequest="RadAjaxManager1_AjaxRequest"
									runat="server" />
			<telerik:RadWindowManager ID="Singleton" InitialBehaviors="Maximize" DestroyOnClose="true"
									  OnClientPageLoad="ContinueSession" OnClientCommand="ContinueSession" OnClientActivate="WindowActivate"
									  VisibleStatusbar="false" runat="server" RestrictionZoneID="RestrictionZone" EnableShadow="true"
									  KeepInScreenBounds="True" ShowContentDuringLoad="false" CssClass="topeIE7" Style="height: auto;">
				<Localization Maximize="<%$ Resources:RadWindow, Maximize %>" Minimize="<%$ Resources:RadWindow, Minimize %>"
							  Close="<%$ Resources:RadWindow, Close %>" PinOff="<%$ Resources:RadWindow, PinOff %>"
							  PinOn="<%$ Resources:RadWindow, PinOn %>" Reload="<%$ Resources:RadWindow,Reload %>"
							  Restore="<%$ Resources:RadWindow, Restore%>" Cancel="<%$ Resources:RadWindow, Cancel %>"
							  OK="<%$ Resources:RadWindow, OK %>" No="<%$ Resources:RadWindow, No %>" Yes="<%$ Resources:RadWindow, Yes %>">
				</Localization>
			</telerik:RadWindowManager>
			<div id="RestrictionZone" style="position: absolute; bottom: 0; left: 0; width: 100%;">
			</div>
			<div id="MinimizeZone" style="position: absolute; width: 100px; bottom: 0; left: 0;
				 right: 933px;">
			</div>
			<hcc:ToolBar ID="tbMenu" runat="server" />
			<div id="piepagina">
				<table cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td style="width: 500px; padding-left: 15px">
							2013 &copy; Servicios Tecnológicos Nubise S.A. Todos los derechos reservados. RIF:
							J-30848669-8
						</td>
						<td style="text-align: right; padding-right: 5px;">
							<a href="http://www.nubise.com" target="_blank" title="ir a www.nubise.com">
								<img alt="" src="imagenes/logo_nubise_fdo_negro.jpg" style="border: 0;" />
							</a>
						</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>
