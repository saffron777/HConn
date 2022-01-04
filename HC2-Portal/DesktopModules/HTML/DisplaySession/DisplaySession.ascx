<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplaySession.ascx.cs" Inherits="DisplaySession.DisplaySession" %>

<div style="padding: 15px 15px 15px 15px">
    <table>
        <tr>
            <td style="width: 150px;"><asp:Button ID="btnSession" runat="server" Text="Cerrar Sesión" BackColor="#11175E" BorderColor="Black" BorderStyle="Solid" ForeColor="White" OnClick="btnSession_Click" /></td>
            <td style="padding-left: 15px;"><asp:Label ID="LblUsuario" runat="server" CssClass="usuario"></asp:Label></td>
        </tr>
    </table>
</div>