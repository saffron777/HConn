<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="Login.Admin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<form>
<div style="padding: 20px; margin: 10px; width: 600px">
    <table border="0" cellpadding="2" cellspacing="2" width="98%">
        <tr>
            <td>
                <table border="0" width="60%" cellpadding="3" cellspacing="3" align="center" style="padding: 2px">
                    <tr>
                        <td width="30%">
                            <asp:Label ID="Label1" runat="server" Text="Usuario CMS (hconnexum...)"></asp:Label>
                        </td>
                        <td width="40%" align="left" height="30px">
                            <div style="width: 200px">
                                <asp:TextBox ID="txtUsrCMS" runat="server" Width="100%"></asp:TextBox>
                            </div>
                            <asp:HiddenField ID="hdnUsr" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td height="40px">
                            <asp:Button ID="btnAceptar" runat="server" Text="Encriptar Usuario CMS" OnClick="btnAceptar_Click" />
                        </td>
                    </tr>
                    <tr style="padding: 2px">
                        <td width="30%">
                            <asp:Label ID="Label2" runat="server" Text="Usuario CMS Encriptado"></asp:Label>
                        </td>
                        <td width="40%" align="left">
                            <div style="width: 400px">
                                <asp:TextBox ID="txtEncriptado" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" width="60%" cellpadding="3" cellspacing="3" align="center" style="padding: 2px">
                    <tr>
                        <td width="30%">
                            <asp:Label ID="Label3" runat="server" Text="Usuario Encriptado"></asp:Label>
                        </td>
                        <td width="40%" align="left" height="50px">
                            <div style="width: 400px">
                                <asp:TextBox ID="txtDesencriptar" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td height="40px">
                            <asp:Button ID="btnDesEncriptar" runat="server" Text="DesEncriptar" OnClick="btnDesEncriptar_Click" />
                        </td>
                    </tr>
                    <tr style="padding: 2px">
                        <td width="30%">
                            <asp:Label ID="Label4" runat="server" Text="Usuario CMS DesEncriptado"></asp:Label>
                        </td>
                        <td width="40%" align="left">
                            <div style="width: 200px">
                                <asp:TextBox ID="txtNatural" runat="server" Width="100%"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</form>
