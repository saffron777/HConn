<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListaSesionesAbiertas.ascx.cs"
    Inherits="ListaSesionesAbiertas.ListaSesionesAbiertas" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    div.noWrapRadListBox .rlbText
    {
        white-space: nowrap;
    }
</style>
<script type="text/javascript">

    function confirmCallBackFn(args) {
        window.parent.location = "../../../MisAplicaciones.aspx";   
    }
    Telerik.Web.UI.RadWindowUtils.Localization =    
{   
    "OK" : "Aceptar",       
};
</script>
<telerik:radscriptmanager id="RadScriptManager1" runat="server" />
<telerik:radwindowmanager id="RadWindowManager1" runat="server" enableshadow="True"
    meta:resourcekey="RadWindowManager1Resource1"></telerik:radwindowmanager>
<div>
    <table border="0" cellpadding="2" cellspacing="2" width="90%" align="center">
        <tr>
            <td>
                <asp:Label ID="lbl" runat="server" Text="Lista de Sesiones Activas con el Suscriptor: "
                    Width="100%" runat="server" Font-Size="12px" Font-Bold="True"></asp:Label>
                <div style="text-align: justify">
                    <asp:Label ID="Label1" Font-Size="12px" runat="server" Text="A continuación se le informa sobre las sesiones que se encuentran activas entre el usuario y el suscriptor seleccionado."></asp:Label>
                </div>
         
            </td>
        </tr>
        <tr>
            <td>
            <br />
                <telerik:radlistbox id="rlbSesiones" datatextfield="nombre" datavaluefield="SessionId"
                    runat="server" showcheckall="true" checkboxes="True" tooltip="Lista de Sesiones Activas con el Usuario/Suscriptor"
                    culture="es-ES" width="350px" height="200px" emptymessage="No existen sesiones abiertas"
                    borderstyle="None" borderwidth="0px" cssclass="noWrapRadListBox">
                        <HeaderTemplate>
                            <asp:Label ID="Label2" runat="server" Text=" Dirección IP - Fecha de Inicio" Font-Size="13px"
                                Font-Bold="True"></asp:Label></HeaderTemplate>
                        <FooterTemplate>
                     <div style="padding: 5px; text-align: justify">
                            <asp:Label ID="lblIniciar" runat="server" Width="100%" Font-Size="11px"  Text="<b>Iniciar Sesión:</b> Permite iniciar una nueva sesión dejando activas las ya iniciadas."></asp:Label>
                            <asp:Label Width="100%" runat="server" ID="lblCerrar" Font-Size="11px" Text="<b>Cerrar Sesión:</b> Permite cerrar las sesiones seleccionadas e iniciar una nueva."></asp:Label>
                          </div>
                        </FooterTemplate>
                    </telerik:radlistbox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <asp:Button ID="btnIniciar" runat="server" Text="Iniciar Sesión" ForeColor="White"
                    ToolTip="Iniciar sesión" AlternateText="Iniciar Sesión" OnClick="BtnIniciarOnclick"
                    ValidationGroup="LoginGroup" BackColor="#152471" BorderColor="#152471" BorderStyle="Solid"
                    Font-Names="Arial" Font-Size="8pt" Width="100px" />
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar Sesión" ToolTip="Cierra la sesiones seleccionas e inicia una nueva"
                    ForeColor="White" AlternateText="Iniciar Sesión" OnClick="BtnCerrarOnclick" ValidationGroup="LoginGroup"
                    BackColor="#152471" BorderColor="#152471" BorderStyle="Solid" Font-Names="Arial"
                    Font-Size="8pt" Width="100px" />
            </td>
        </tr>
    </table>
</div>
