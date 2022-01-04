<%@ Page Title="Pantalla Contenedora" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="PantallaContenedora.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Orquestador.PantallaContenedora" %>
<%@ Register Src="~/Modulos/Bloques/MuestraChat.ascx" TagName="MuestraChat" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <link href="../../Temas/ccs/jquery.coolfieldset.css" type="text/css" rel="stylesheet" />
    <link href="../../Temas/UserControls.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <asp:UpdatePanel runat="server" ID="pContenedor">
	</asp:UpdatePanel>
	<div style="text-align: right;">
		<br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" meta:resourcekey="cmdGuardarResource1" OnClick="cmdGuardar_Click" ValidationGroup="Validaciones"  />&nbsp;
		<asp:Button ID="cmdGuardaryContinuar" runat="server" Visible="false" Text="Guardar y Continuar" meta:resourcekey="cmdGuardaryAgregarResource1" OnClick="cmdGuardaryContinuar_Click" ValidationGroup="Validaciones"  />&nbsp;
		<asp:Button ID="cmdcancelar" runat="server" Text="Cancelar" OnClick="cmdcancelar_Click" CausesValidation="false" ValidationGroup="Cancelar" />
		<asp:Button ID="cmdChat" runat="server" CausesValidation="False" Visible="false" OnClientClick="MostrarChat(); return false" Text="Chat" UseSubmitBehavior="False" />
		<br />
		<br />
	</div>
	<telerik:RadWindow runat="server" ID="rwChat" CssClass="RadWindow" Modal="true" Behaviors="Close, Move" Width="660px" Height="300px" Title="Chat" DestroyOnClose="True">
		<ContentTemplate>
			<div style="text-align: left;">
				<hcc:MuestraChat ID="controlChat" runat="server" />
			</div>
		</ContentTemplate>
	</telerik:RadWindow>
	<script type="text/javascript">
		var idMenu = '<%= IdMenuEncriptado %>';
		var ventanaDetalle = "Modulos/tracking/ObservacionesMovimientoDetalle.aspx?IdMenu=" + idMenu + "&";
		function MostrarChat() {
			var oWindow = $find("<%= rwChat.ClientID %>");
			if (oWindow != null) {
				oWindow.show();
			}
		};
	</script>
</asp:Content>
