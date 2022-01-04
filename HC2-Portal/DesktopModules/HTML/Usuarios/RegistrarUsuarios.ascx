<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrarUsuarios.ascx.cs" Inherits="Usuarios.RegistrarUsuarios" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<script type="text/javascript">
    function onItemChecked(sender, e) {
        var item = e.get_item();
        var items = sender.get_items();
        var checked = item.get_checked();
        var firstItem = sender.getItem(0);
        if (item.get_text() == "Seleccionar todo") {
            items.forEach(function (itm) { itm.set_checked(checked); });
        }
        else {
            if (sender.get_checkedItems().length == items.get_count() - 1) {
                firstItem.set_checked(!firstItem.get_checked());
            }
        }
    }


    function OnClientItemChecked(sender, eventArgs) {
        var lstBoxControl;
        lstBoxControl = $find(sender.get_id());
        var items = lstBoxControl.get_items();
        if (eventArgs.get_item().get_index() == 0) {
            var firstIndex = eventArgs.get_item().get_checked();
            for (var i = 0; i < lstBoxControl.get_items().get_count() ; i++) {
                items.getItem(i).set_checked(firstIndex);
            }
        }
        else {
            items.getItem(0).set_checked(false);
        }
    }
</script>

<br />
<div align="center">
    <table cellpadding="0" cellspacing="0" border="0" width="350px">
        <tr>
            <td>Criterio de Búsqueda</td>
            <td></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtFiltrar" runat="server" Width="100%"></asp:TextBox></td>
            <td style="text-align:right;"><asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" onclick="btnConsultar_Click" Width="80px" /></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2"><telerik:RadListBox ID="rlbUsuarios" runat="server" CheckBoxes="true" Width="100%" Height="300px" DataTextField="LoginUsuarioCMS" DataValueField="Id" OnClientItemChecked="OnClientItemChecked"></telerik:RadListBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:right;"><asp:Button ID="btnCrearUsuarios" runat="server" onclick="btnCrearUsuarios_Click" Text="Crear usuarios" /></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2"><asp:Literal ID="LMensaje" runat="server" ></asp:Literal></td>
        </tr>
    </table>
</div>
<br /><br />





