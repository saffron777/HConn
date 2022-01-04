<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicarNoticias.ascx.cs" Inherits="Noticias.PublicarNoticias" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript">
    function EditarNoticias() {
        var oWindow = $find("<%= RadWindowPub.ClientID %>");
        oWindow.setUrl("DesktopModules/HTML/Noticias/Default.aspx");
        oWindow.SetModal(true);
        oWindow.show();
        /*
        var screenHeight = document.documentElement.clientHeight;
        var screenWidth = document.documentElement.clientWidth;
        oWindow.SetHeight(parseInt(screenHeight - 50));
        oWindow.SetWidth(parseInt(screenWidth));
        */
        oWindow.Center();
    }
</script>
<div style="padding: 15px 15px 15px 15px">
    <table id="TablaAdmin" width="100%">
        <tr>
            <td style="text-align: right">
                <input type="button" id="btnEditarNoticias" value="Editar Noticias" onclick="javascript: EditarNoticias();" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <img alt="" src="Imagenes/imgNoticias.png" /></td>
        </tr>
    </table>
</div>
<telerik:RadWindowManager ID="ventana" runat="server" EnableShadow="True" EnableEmbeddedSkins="False"></telerik:RadWindowManager>
<telerik:RadWindow id="RadWindowPub"
    runat="server"
    animation="Fade"
    visibletitlebar="True"
    enableshadow="True"
    title="Publicación noticias"
    visiblestatusbar="False"
    iconurl="~/Imagenes/Ico_Noticias.png" AutoSize="True" Modal="True">
    <localization cancel="Cancelar" close="Cerrar" maximize="Maximizar" minimize="Minimizar" ok="Aceptar" pinoff="Flotante" pinon="Anclar" reload="Recargar" restore="Restaurar" yes="Si" />
</telerik:RadWindow>
