<%@ Page Title="Mis Solicitudes" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="MisSolicitudes.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.MisSolicitudes" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<div>
		<asp:Panel ID="PanelMaster" runat="server">
			<table width="100%">
				<tr>
                    <td>
                        <asp:Label ID="lbUsuario" runat="server" Text="Usuario" />
                        &nbsp;/&nbsp;
                        <asp:Label ID="lbSuscriptor" runat="server" Text="Suscriptor" />
                    </td>
					<td align="right">
						<asp:Button runat="server" ID="btnBusquedaAvanzada" Text="Búsqueda Avanzada" OnClientClick="IrConsultaCasos(); return false;" />
						<asp:Button runat="server" ID="btnNuevoCaso" Text="Nuevo Caso" 
                            OnClientClick="IrSolicitudServicio(); return false;" 
                            onclick="btnNuevoCaso_Click" />
					</td>
				</tr>
			</table>
			<fieldset>
				<legend>
					<asp:Label ID="lblMisSolicitudesPendientes" runat="server" Text="Mis Solicitudes Pendientes" Font-Bold="True" />
				</legend>
				<telerik:RadTreeView runat="server" ID="RadTreeView1" Skin="Outlook" OnTemplateNeeded="RadTreeView1_TemplateNeeded">
				</telerik:RadTreeView>
			</fieldset>
		</asp:Panel>
		<br />
		<br />
	</div>
	<script language="javascript" type="text/javascript">
		function IrConsultaCasos(sender) {
			var wnd = GetRadWindow();
			var idMenu = '<%= IdMenuEncriptado %>';
			wnd.setUrl("Modulos/Estructura/ConsultaCasos.aspx?IdMenu=" + idMenu);
		}

		function IrSolicitudServicio(sender) {
			var wnd = GetRadWindow();
			var idMenu = '<%= IdMenuEncriptado %>';
			wnd.setUrl("Modulos/Estructura/SolicitudServicio.aspx?IdMenu=" + idMenu);
		}
	</script>
</asp:Content>
