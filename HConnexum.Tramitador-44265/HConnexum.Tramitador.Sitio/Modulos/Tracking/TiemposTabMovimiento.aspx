<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TiemposTabMovimiento.aspx.cs"
    Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.TiemposTabMovimiento" %>

<%@ Register Src="~/ControlesComunes/HorasMinutosSegundos.ascx" TagName="HorasMinutosSegundos"
    TagPrefix="hcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table>
            <tr>
                <td><asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha creación:"></asp:Label></td>
                <td><asp:TextBox ID="txtFechaCreacion" Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFechaAtencion" runat="server" Text="Fecha atención:"></asp:Label></td>
                <td><asp:TextBox ID="txtFechaAtencion" Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                <td></td>
                <td><hcc:HorasMinutosSegundos runat="server" ID="txtAtencion" Enabled="false" IsRequired="False" Width="200" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblFechaAtencio" runat="server" Text="Fecha ejecución:"></asp:Label></td>
                <td><asp:TextBox ID="txtFechaEjecucion" Enabled="false" runat="server" 
                        Width="150px"></asp:TextBox></td>
                <td></td>
                <td><hcc:HorasMinutosSegundos runat="server" ID="txtEjecucion" Enabled="false" IsRequired="False" Width="200" /></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:right">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:right"><asp:Label ID="LlbtiempoEstimado" runat="server" Text="Tiempo estimado para el movimiento:" />&nbsp;<asp:TextBox 
                        ID="txtTiempoEstimado" runat="server" Enabled="False" Width="100px" />&nbsp; </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
