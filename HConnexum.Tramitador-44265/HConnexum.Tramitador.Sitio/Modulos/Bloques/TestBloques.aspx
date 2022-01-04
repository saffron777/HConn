<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestBloques.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.TestBloques" %>
<%@ Register Src="~/Modulos/Bloques/ActualizacionDatosMedico.ascx" TagName="ActualizacionDatosMedico" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/ActualizacionDeContacto.ascx" TagName="ActualizacionDeContacto" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/AnulacionOM.ascx" TagName="AnulacionOM" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/AnulacionSM.ascx" TagName="AnulacionSM" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/AsistenciaCitaMedica.ascx" TagName="AsistenciaCitaMedica" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/CambioMedico.ascx" TagName="CambioMedico" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/CasosPendientesMedico.ascx" TagName="CasosPendientesMedico" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/ContactoAlMedico.ascx" TagName="ContactoAlMedico" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/ContactoInicial.ascx" TagName="ContactoInicial" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/DatosGenerales.ascx" TagName="DatosGenerales" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/DatosGeneralesMedico.ascx" TagName="DatosGeneralesMedico" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/Imprimir.ascx" TagName="Imprimir" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/MedicoAsignado.ascx" TagName="MedicoAsignado" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/Observaciones.ascx" TagName="Observaciones" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/RegistrodeOpinionMedico.ascx" TagName="RegistrodeOpinionMedico" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/SeguimientoCita.ascx" TagName="SeguimientoCita" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/Egreso.ascx" TagName="Egreso" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/Ingreso.ascx" TagName="Ingreso" TagPrefix="hcc" %>
<%--MÓDULO: CLAVE DE EMERGENCIA--%>
<%@ Register Src="~/Modulos/Bloques/DatosExpedientesCE.ascx" TagName="DatosExpedientesCE" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/SolicitudNuevoMovimiento_Ing.ascx" TagName="SolNueMovIng" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/SolicitudNuevoMovimiento_ExtEgr.ascx" TagName="SolNueMovExtEgr" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/DatosGeneralesCE.ascx" TagName="DatosGeneralesCE" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/SeleccionAsegurado.ascx" TagName="SeleccionAsegurado" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/BlqNotRespInm.ascx" TagName="BlqNotRespInm" TagPrefix="hcc" %>
<%--MÓDULO: CARTA AVAL--%>
<%@ Register Src="~/Modulos/Bloques/DatosGeneralesCA.ascx" TagName="DatosGeneralesCA" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/SolicitudNuevoMovimiento_IngCA.ascx" TagName="SolNueMovIngCA" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/SolicitudNuevoMovimiento_ExtEgrCA.ascx" TagName="SolNueMovExtEgrCA" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/ListadoMovimientosCA.ascx" TagName="ListMovCA" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/DatosExpedientesCA.ascx" TagName="DatExpCA" TagPrefix="hcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
	<link href="../../Temas/Site.css" rel="stylesheet" type="text/css" />
	<link href="../../Temas/ccs/jquery.coolfieldset.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
		window.onload = window.onresize = function posicionPrincipal() {
			$('#RestrictionZone').height($(window).height() - 5);
		};
	</script>
</head>
<body class="master-body">
	<form id="formMain" runat="server">
	<div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
		<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
		<asp:UpdatePanel runat="server" ID="pBloques">
			<ContentTemplate>
				<div id="cphBody_p1" style="padding-top: 10px;">
					<fieldset id="fieldsetp1" class="coolfieldset">
						<legend><b>Bloque</b></legend>
						<div>
							<hcc:SolNueMovIng runat="server" />
						</div>
					</fieldset>
					<script type="text/javascript">
						$('#fieldsetp1').coolfieldset({ collapsed: false });
					</script>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
		<br/>
		<asp:Button runat="server" ID="btnValidaciones" Text="Validaciones" OnClick="btnPost_Click" ValidationGroup="Validaciones"/>
		<asp:Button runat="server" ID="btnPost" Text="PostBack" OnClick="btnPost_Click"/>
		<telerik:RadWindowManager ID="Singleton" Width="800px" Height="600px" DestroyOnClose="True" VisibleStatusbar="False" runat="server" EnableShadow="True" KeepInScreenBounds="True" RestrictionZoneID="RestrictionZone" MinimizeZoneID="MinimizeZone" ReloadOnShow="True" ShowContentDuringLoad="False" />
		<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
		<telerik:RadSkinManager runat="server" ID="rsmGeneralMaster" Skin="Black" />
	</div>
	</form>
</body>
</html>
