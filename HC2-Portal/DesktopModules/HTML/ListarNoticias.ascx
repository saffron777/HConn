<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListarNoticias.ascx.cs" Inherits="Noticias.ListarNoticias" %>

<script type="text/javascript">
    function abrirVentana(url) {
        var windowsReference;
        var sfeatures = "menubar=no,location=no,resizable=yes,scrollbars=yes,status=no,width=800,height=600,directories=no,toolbar=no";
        windowObjectReference = window.open(url, "Noticia", sfeatures);
    }
</script>
<br />
<div>
    <table style="width: 100%; height: 600px;">
        <tr style="vertical-align: top;">
            <td style="font-family: Segoe UI, Arial, Sans-Serif; text-align: justify;">
                <asp:Literal ID="LiteralLista" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</div>
