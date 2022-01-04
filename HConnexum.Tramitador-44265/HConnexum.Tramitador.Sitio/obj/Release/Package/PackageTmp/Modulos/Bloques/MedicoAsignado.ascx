<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedicoAsignado.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.MedicoAsignado" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>

<table width="100%" border="0">
	<tr>
		<td class="labelCell"><asp:Label ID="lblMedico" runat="server" Text="Documento Identidad:" /></td>
		<td class="fieldCell">
            <div style="width: 100%">
			    <div style="width: 7%; float: left; padding-right: 1%;"><asp:TextBox ID="tipdocmed" runat="server" Width="100%"/></div>
			    <div style="width: 90%; float: right;"><asp:TextBox ID="numdocmed" runat="server" Width="98%" /></div>
            </div>
        </td>
		<td class="labelCell"><asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" /></td>
		<td class="fieldCell"><asp:TextBox ID="tlfmed" runat="server" Width="98%"/></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label ID="lblAsegurado" runat="server" Text="Nombre:" /></td>
		<td class="fieldCell"><asp:TextBox ID="nommed" runat="server" Width="98%"></asp:TextBox></td>
		<td class="labelCell"><asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad:" /></td>
		<td class="fieldCell"><asp:TextBox ID="espmed" runat="server" Width="98%"></asp:TextBox></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label ID="lblPais" runat="server" Text="País:"  /></td>
		<td class="fieldCell"><asp:TextBox ID="idpaismed" runat="server" Width="98%"></asp:TextBox></td>
		<td class="labelCell"><asp:Label ID="lblDivision1" runat="server" Text="División Territorial 1:" /></td>
		<td class="fieldCell"><asp:TextBox ID="iddiv1med" runat="server" Width="98%"></asp:TextBox></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label ID="lblDivicion2" runat="server" Text="División Territorial 2:" /></td>
		<td class="fieldCell"><asp:TextBox ID="iddiv2med" runat="server" Width="98%"></asp:TextBox></td>
		<td class="labelCell"><asp:Label ID="lblDivision3" runat="server" Text="División Territorial 3:" /></td>
		<td class="fieldCell"><asp:TextBox ID="iddiv3med" runat="server" Width="98%"></asp:TextBox></td>
	</tr>
     <tr>
        <td class="labelCell" style="vertical-align: top;">
		    <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" Font-Bold="true" Width="124px"/>
	    </td>
        <td colspan="3" style="padding-left: 5px; padding-right: 6px; white-space:nowrap;">
		    <div style="width:100%"><hcc:MultilineCounter ID="dirmed" runat="server" MaxLength="500" /></div>
	    </td>
    </tr>
</table>
<script language="javascript" type="text/javascript">
	$(document).ready(function () {
		document.getElementById("<%= dirmed.ClientID %>" + "_txtControl").style.width = "99%";
	});
</script>