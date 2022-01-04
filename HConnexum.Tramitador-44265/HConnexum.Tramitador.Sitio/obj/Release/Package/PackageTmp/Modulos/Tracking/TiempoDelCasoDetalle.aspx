<%@ Page Language="C#" AutoEventWireup="True" Title="Detalle de Caso" CodeBehind="TiempoDelCasoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.TiempoDelCasoDetalle" %>
<%@ Register Src="~/ControlesComunes/HorasMinutosSegundos.ascx" TagName="HorasMinutosSegundos" TagPrefix="hcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <telerik:RadScriptManager ID="RadScriptManager" runat="server"></telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings> 
                    <telerik:AjaxSetting AjaxControlID="RadGridDetails">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="PanelMaster" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <div>
                        <table>
                            <tr>
                                <td colspan="3" align="left"><b>Caso</b></td>
                            </tr>    
                            <tr>
                                <td><asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha creación:"/></td>
                                <td style="width: 200px"><asp:TextBox ID="txtFechaCreacion" runat="server" Enabled="False" Width="150px" Height="19px" /></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblFechaAtencion" runat="server" Text="Fecha de atención:"/></td>
                                <td style="width: 200px"><asp:TextBox ID="txtFechaAtencion" runat="server" Enabled="False" Width="150px" Height="19px" /></td>
                                <td><hcc:HorasMinutosSegundos runat="server" ID="txtAtencion" Enabled="False" /></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="lblFechaCerrado" runat="server" Text="Fecha cerrado:"/></td>
                                <td style="width: 200px"><asp:TextBox ID="txtFechaCerrado" runat="server" Enabled="False" Width="150px" Height="19px" /></td>
                                <td><hcc:HorasMinutosSegundos runat="server" ID="txtCerrado" Enabled="False" /></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="Label1" runat="server" Text="Tiempo esperado para el caso:"/></td>
                                <td style="width: 200px"><asp:TextBox ID="txtSLAestimado" runat="server" Enabled="False" Width="150px" Height="19px" /></td>
                                <td></td>
                            </tr>
                        </table>

            </div>
        </form>
    </body>
</html>