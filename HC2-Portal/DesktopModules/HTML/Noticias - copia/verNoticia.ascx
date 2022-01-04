<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="verNoticia.ascx.cs" Inherits="Noticias.verNoticia" %>

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
            <asp:Button ID="btnCerrar" runat="server" AlternateText="Cerrar" BackColor="#152471" BorderColor="#152471" 
                                       BorderStyle="Solid" ClientIDMode="Static" Font-Names="Arial" Font-Size="8pt" 
                                       ForeColor="White" OnClientClick="javascript:window.close();" Text="CERRAR" Width="100px" />
            <br /><br />
            <input id="Button1" style="border: 1px solid #152471; background-color: #152471; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif;" type="button" value="NUESTROS SERVICIOS"  />
        </td>
        <td style="width: 20px">&nbsp;</td>
    </tr>
</table>

