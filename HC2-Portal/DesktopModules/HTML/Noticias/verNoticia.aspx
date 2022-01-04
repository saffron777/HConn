<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verNoticia.aspx.cs" Inherits="Noticias.verNoticia1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>HConnexum - Noticia</title>
</head>
<body>
    <form id="form1" runat="server">
<table width="100%">
    <tr>
        <td style="font-family: Segoe UI, Arial, Sans-Serif; font-size: 12px; text-decoration:none;">
            <asp:Literal ID="LiteralLista" runat="server"></asp:Literal>
        </td>
        <td style="width: 20px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="font-family: Segoe UI, Arial, Sans-Serif; font-size: 12px;">
            <asp:Literal ID="LiteralFecha" runat="server"></asp:Literal>
        </td>
        <td style="width: 20px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="font-family: Segoe UI, Arial, Sans-Serif;"><h1><asp:Literal ID="LiteralTitulo" runat="server"></asp:Literal></h1></td>
        <td style="width: 20px;">&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td style="width: 20px">&nbsp;</td>
    </tr>
    <tr>
        <td style="font-family: Segoe UI, Arial, Sans-Serif; text-align:justify;"><asp:Literal ID="LiteralNoticia" runat="server"></asp:Literal></td>
        <td style="width: 20px;">&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td style="width: 20px">&nbsp;</td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <asp:Button ID="btnCerrar" runat="server" 
                BackColor="#152471" BorderColor="#152471" BorderStyle="Solid" Font-Names="Arial" Font-Size="8pt" 
                ForeColor="White" Text="CERRAR" Width="100px" 
                OnClientClick="javascript:window.close();"/>
        </td>
        <td style="width: 20px">&nbsp;</td>
    </tr>
</table>
    </form>
</body>
</html>
