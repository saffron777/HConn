<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContainerGadgets.ascx.cs" Inherits="HC.GadgetAplicacion.ContainerGadgets" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<style type="text/css">
    .RadDock_HConnexum .rdContent {
        background-color: transparent !important;
        overflow: hidden !important;
    }
    .StRadtoolbar
{
    background: none;
    border: 0 none !important;
    text-decoration: none;
    border-bottom-width: 0px !important; 
    background-color: Transparent !important;
    overflow: hidden !important;
}
</style>
<script src="DesktopModules/HTML/HC.GadgetAplicacion/Scripts/jquery-ui.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function OnClientSelectedIndexChanged(sender, args) {
        __doPostBack('dropSuscriptor', '');
    }
    function OnClientSelectedIndexChanged(sender, args) {
        __doPostBack('dropSucursal', '');
    }
</script>
<div style="padding: 1px 0px 15px 0px; background: url(DesktopModules/HTML/HC.GadgetAplicacion/Imagenes/bgAplicaciones.png) no-repeat; background-position: bottom;">
    <asp:UpdatePanel runat="server" ID="UpdatePanelHconnexum" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="MenuToolBarDiv" style="background: url(DesktopModules/HTML/HC.GadgetAplicacion/Imagenes/LineaDegradada.jpg) repeat-y; height: 30px; border: 1px solid transparent !important; vertical-align: middle;">
                <table style="width: 100%; margin: 5px;">
                    <tr>
                     <td style="width: 10px;">&nbsp;</td>
                     <td style="width: 45px;" class="texto"><span>Suscriptor:</span>&nbsp;</td>
                     <td style="width: 250px;">
                        <asp:Label runat="server" ID="LblSuscriptor" Text=""></asp:Label>
                        <telerik:RadComboBox ID="dropSuscriptor" ZIndex="250000" runat="server" AutoPostBack="true"
                            DataValueField="IdUsuarioSuscriptor" DataTextField="Nombre"
                            OnSelectedIndexChanged="DropSuscriptorSelectedIndexChanged"
                            OnClientSelectedIndexChanged="clientSelectedIndexChanged" 
                            Width="230px" ViewStateMode="Enabled">
                        </telerik:RadComboBox>
                     </td>
                     <td style="width: 40px;" class="texto"><span>Sucursal:</span>&nbsp;</td>
                     <td style="width: 250px;">
                        <asp:Label runat="server" ID="LblSucursal" Text=""></asp:Label>
                        <telerik:RadComboBox ID="dropSucursal" ZIndex="250000" runat="server" AutoPostBack="true"
                            DataValueField="Id" DataTextField="Nombre"   
                            OnSelectedIndexChanged="dropSucursal_SelectedIndexChanged"   
                           OnClientSelectedIndexChanged="clientSelectedIndexChanged"                  
                            Width="230px" ViewStateMode="Enabled">
                        </telerik:RadComboBox>
                     </td>
                     <td style="width: 300px; text-align: right;">
                         <img alt="" src="DesktopModules/HTML/HC.GadgetAplicacion/Imagenes/logueado.png" />
                         <asp:Label ID="LblUsuario" runat="server" CssClass="usuario"></asp:Label>&nbsp;&nbsp;                                             
                     </td>
                     <td style="width: 20px; height: 15px;" >
                         <asp:Button ID="Button1" runat="server" Text="Cerrar Sesión" OnClick="BtnCerrarSesion" />
                     </td>
                </tr>
            </table>
          </div>
        <telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
            <script type="text/javascript">

                function DropDownOpened(sender, args) {
                    var dropDown = args.get_item();
                    var templength = 0;
                    for (var i = 0; i < dropDown.get_buttons().get_count() ; i++) {
                        var dropDownItemWidth = $.textMetrics(dropDown.get_buttons().getButton(i)._element.innerHTML).width;
                        if (dropDownItemWidth > templength)
                            templength = dropDownItemWidth;
                    }
                if (templength > 0) {
                    args.get_item()._animationContainer.style.width = (templength + 50).toString() + "px"
                    for (var j = 0; j < args.get_item()._animationContainer.childNodes.length; j++)
                        args.get_item()._animationContainer.childNodes[j].style.width = (templength + 50).toString() + "px";
                }
            }

            function WindowActivate(sender, eventArgs) {
                var Windows = GetRadWindowManager().GetWindows();
                $.each(Windows, function () {
                    if (this._popupElement.id != sender._popupElement.id) {
                        $('#' + this._popupElement.id).css('z-index', '3000');
                    }
                });
                $('#' + sender._popupElement.id).css('z-index', '100000');
            }

            function ActivateWindowPopup(sender, args) {
                $.each(GetRadWindowManager().GetWindows(), function () {
                    if (this._name == args.get_item()._itemData) {
                        GetRadWindowManager().GetWindowByName(this._name).SetActive(true);
                    }
                })
            }

            var queryArr = [];

            function OnClientItemClickedHandler(sender, eventArgs) {
                if ((eventArgs._item.get_value()) != "" && (eventArgs._item.get_value() != null)) {
                    var Bandera = true;
                    jQuery.each(queryArr, function () {
                        if (this == eventArgs._item.get_text()) {
                            Bandera = false;
                        }
                    });
                    if (Bandera) {
                        var own = window.radopen(eventArgs._item.get_value(), null);
                        AddNewItem(eventArgs._item.get_text(), own)
                        own.set_minimizeZoneID('MinimizeZone');
                        queryArr.push(eventArgs._item.get_text());
                        if (queryArr.length > 1) {
                            own.moveTo(own._restoreRect.x + (5 * queryArr.length), own._restoreRect.y + (10 * queryArr.length));
                        }
                        own.add_close(closeRadWindow);
                    }
                }
                return false;
            }

            function OpenApplication(nombreapp, urlapp, multipleWindow) {
                var Bandera = true;
                if (!multipleWindow) {
                    var Windows = GetRadWindowManager().GetWindows();
                    $.each(Windows, function () {
                        if (this._title == nombreapp) {
                            Bandera = false;
                            return;
                        }
                    });
                }
                if (Bandera) {
                    var own = window.radopen(urlapp, nombreapp);
                    own.SetTitle(nombreapp);
                    var oWnd = $find("<%=singleton.ClientID %>");
                    oWnd.setUrl(urlapp);
                    oWnd.SetTitle(nombreapp);
                    oWnd.show();
                }
            }

            function OpenApplication_IE7(nombreapp, urlapp, multipleWindow) {
                var bOpenWindow = true;
                if (!multipleWindow) {
                    var Windows = document.getElementById("<%=singleton.ClientID %>").control;
                    for (i = 0; i < Windows._windows.length; i++) {
                        if (Windows._windows[i]._title == nombreapp) {
                            if (Windows._windows[i].IsClosed() == true)
                                bOpenWindow = true;
                            else {
                                bOpenWindow = false;
                                return;
                            }
                        }
                    }
                }
                if (bOpenWindow) {
                    var own = window.radopen(urlapp, null);
                    var imgIcono = new Image();
                    imgIcono.src = "Imagenes/" + nombreapp + ".png";

                    imgIcono.onload = function () {
                        own.set_iconUrl("Imagenes/" + nombreapp + ".png");
                    };

                    imgIcono.onerror = function () {
                        own.set_iconUrl("Imagenes/Ico_NoDisponible.png");
                    };

                    own.SetTitle(nombreapp);
                    own.maximize();
                }
            }
        </script>
    </telerik:RadScriptBlock>
             <telerik:RadDockLayout runat="server" ID="GadgetZoneLayout" OnLoadDockLayout="GadgetZoneLayout_LoadDockLayout">
                <table>
                    <tr>                        
                        <td style="vertical-align: top; width: 100%; height: 425px;">
                            <telerik:RadDockZone ID="AppDockZone" runat="server" BorderColor="Transparent" BorderStyle="Solid" Orientation="Horizontal" Width="100%" />
                        </td>                        
                    </tr>
                </table>
            </telerik:RadDockLayout>
            <telerik:RadWindowManager ID="singleton" runat="server" EnableShadow="True" EnableEmbeddedSkins="True" ReloadOnShow="true" Skin="Windows7"></telerik:RadWindowManager>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dropSuscriptor" EventName="SelectedIndexChanged" /> 
             <asp:AsyncPostBackTrigger ControlID="dropSucursal" EventName="SelectedIndexChanged" /> 
        </Triggers>        
    </asp:UpdatePanel>
</div>
