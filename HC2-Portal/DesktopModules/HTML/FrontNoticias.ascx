<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FrontNoticias.ascx.cs" Inherits="Noticias.FrontNoticias" %>

<script type="text/javascript">
    function abrirVentana(url) {
        var windowsReference;
        var sfeatures = "menubar=no,location=no,resizable=yes,scrollbars=yes,status=no,width=800,height=600,directories=no,toolbar=no";
        windowObjectReference = window.open(url, "Noticia", sfeatures);
    }
</script>
<div>
    <table width="100%">
        <tr>
            <td class="bgTitulo" style="padding-left: 20px;"><strong>NOTICIAS</strong></td>
        </tr>
    </table>
    <asp:Literal ID="LiteralHTML" runat="server"></asp:Literal>
</div>

