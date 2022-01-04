<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditoriaTabMovimiento.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.AuditoriaTabMovimiento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td style="width: 100px"><asp:Label ID="Label1" runat="server" Text="Creado por:"></asp:Label></td>        
                    <td style="width: 160px"><asp:TextBox ID="txtCreadoPor" Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 130px"><asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha creación:"></asp:Label></td>        
                    <td style="width: 160px"><asp:TextBox ID="txtFechaCreacion" Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px"><asp:Label ID="lblModificadopor" runat="server" Text="Modificado por:"></asp:Label></td>        
                    <td style="width: 160px"><asp:TextBox ID="txtModificadopor" Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                    <td style="width: 130px"><asp:Label ID="lblFechaModificacion" runat="server" Text="Fecha modificación:"></asp:Label></td>        
                    <td style="width: 160px"><asp:TextBox ID="txtFechaModificacion"  Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                </tr>    
                <tr>
                    <td style="width: 100px"></td>        
                    <td style="width: 160px"></td>
                    <td style="width: 130px"><asp:Label ID="lblFechaOmision" runat="server" Text="Fecha omisión:"></asp:Label></td>        
                    <td style="width: 160px"><asp:TextBox ID="txtFEchaOmision" Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px"></td>        
                    <td style="width: 160px"></td>
                    <td style="width: 130px"><asp:Label ID="lblFechaEjecutado" runat="server" Text="Fecha ejecutado:"></asp:Label></td>        
                    <td style="width: 160px"><asp:TextBox ID="txtFechaEjecutado" Enabled="false" runat="server" Width="150px"></asp:TextBox></td>
                </tr>   
            </table>
        </div>
    </form>
</body>
</html>
