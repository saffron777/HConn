<%@ Page Title="Detalle del Movimiento" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="True" CodeBehind="MisActividadesDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.MisActividadesDetalle" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>
<%@ Register Src="~/Modulos/Bloques/MuestraChat.ascx" TagName="MuestraChat" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .style1
        {
        width: 145px;
        }
      
        .style5
        {
        width: 139px;
        }
              
        .style7
        {
        width: 142px;
        }
        .style8
        {
        width: 479px;
        }
        .chatWindow {width:1000px;}
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <div>
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset>
                <legend>
                    <b>
                        <asp:Label runat="server" Text="Caso" Font-Bold="True" meta:resourcekey="LblLegendCasoResource"/>
                    </b>
                </legend>
                <table width="100%" cellspacing="6px">
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lblLabelServicio" Font-Bold="True" runat="server" Text="Servicio:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblServicio" runat="server" Text=""/>
                        </td>
                        <td>
                            <asp:Label ID="lblLabelNroCaso" runat="server" Font-Bold="True" Text="Nro. de caso:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblNroCaso" runat="server" Text=""/>
                        </td>
                        <td>
                            <asp:Label ID="lblLabelEstatusCaso" runat="server" Font-Bold="True" Text="Estatus:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblEstatusCaso" runat="server" Text=""/>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lblLabelFechaSolicitud" runat="server" Font-Bold="True" Text="Fecha solicitud:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblFechaSolicitud" runat="server" Text=""/>
                        </td>
                        <td>
                            <asp:Label ID="lblLabelSolicitante" runat="server" Font-Bold="True" Text="Beneficiario:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblSolicitante" runat="server" Text=""/>
                        </td>
                        <td>
                            <asp:Label ID="lblLabelMovilSolicitante" runat="server" Font-Bold="True" Text="Móvil beneficiario:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblMovilSolicitante" runat="server" Text=""/>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblLabelEdadSolicitante" runat="server" Font-Bold="True" Text="Edad del beneficiario:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblEdadSolicitante" runat="server" Text=""/>
                        </td>
                        <td>
                            <asp:Label ID="lblLabelSexoSolicitante" runat="server" Font-Bold="True" Text="Sexo del beneficiario:"/>
                        </td>
                        <td>
                            <asp:Label ID="lblSexoSolicitante" runat="server" Text=""/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLabelFechaSolicitud0" runat="server" Font-Bold="True" Text="Intermediario:" />
                        </td>
                        <td>
                            <asp:Label ID="lblIntermediario" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblLabelFechaSolicitud1" runat="server" Font-Bold="True" Text="Contratante:" />
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblContratante" runat="server" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="cmdChat" runat="server" 
                                        OnClientClick="MostrarChat();
                                        return false" Text="Chat" visible="false"
                                        CausesValidation="False" UseSubmitBehavior="False" Width="100px" />
                            <asp:ImageButton ID="cmdChateee" runat="server"                          
                             OnClientClick="MostrarChat();
                                        return false" visible="false"
                                     />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <fieldset>
                <legend>
                    <b>
                        <asp:Label runat="server" Text="Movimiento" Font-Bold="True" meta:resourcekey="LblLegendMovimientoResource"/>
                    </b>
                </legend>
                <table width="100%" cellspacing="6px">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblLabelNombrePaso" runat="server" Font-Bold="True" 
                                Text="Nombre del Movimiento:"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblNombrePaso" runat="server" Text=""/>
                        </td>
                        <td class="style5">
                            <asp:Label ID="lblLabelEstatus" runat="server" Font-Bold="True" Text="Estatus:" />
                        </td>
                        <td>
                            <asp:Label ID="lblEstatus" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" style="vertical-align: top;">
                            <asp:Label ID="lblLabelDescripcion" runat="server" Font-Bold="True" Text="Descripción:"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblDescripcion" runat="server" Text=""/>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblLabelFechaCreacion" runat="server" Font-Bold="True" Text="Fecha creación:"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblFechaCreacion" runat="server" Text=""/>
                        </td>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblLabelUsuarioModificacion" runat="server" Font-Bold="True" Text="Usuario modificación:"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblUsuarioModificacion" runat="server" Text=""/>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblLabelFechaModificacion" runat="server" Font-Bold="True" Text="Fecha modificación:"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblFechaModificacion" runat="server" Text=""/>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblLabelFechaProceso" runat="server" Font-Bold="True" Text="Fecha en proceso:"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblFechaProceso" runat="server" Text=""/>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblLabelObservaciones" runat="server" Font-Bold="True" Text="Observaciones:"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblObservaciones" runat="server" Text=""/>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lblLabelUsuarioCreacion" runat="server" Font-Bold="True" Text="Usuario creación:" Visible="False"/>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lblUsuarioCreacion" runat="server" Visible="False"/>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="cmdAtender" runat="server" Text="Atender" 
                                        onclick="cmdAtender_Click" Visible="false" Width="100px"/>
                            &nbsp;
                            <asp:Button ID="cmdOmitir" runat="server" Text="Omitir" visible="false"
                                        OnClientClick="MostrarVentanaOmitir();
    
                                        return false" Width="100px"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
    <telerik:RadWindow ID="RadWindowOmitir" runat="server" class="RadWindow" 
                       Title="Omitir Movimiento" VisibleStatusbar="False" EnableViewState="False" 
                       DestroyOnClose="True" KeepInScreenBounds="True" Behaviors="Close, Move" 
                       Modal="True" BorderStyle="Dotted" EnableTheming="False" BorderWidth="10px" Height="200px"
                       Width="372px" Behavior="Close, Move">
        <ContentTemplate>
            <div>
                <table width="350" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblOmitirMovimiento" runat="server" Text="Razones por las que se está realizando dicha acción:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <hcc:MultilineCounter ID="txtObservacion" runat="server" Width="300" MaxLength="500"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                        OnClientClick="CerrarVentanaOmitir();

                                        return false"/>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow runat="server" ID="rwChat" CssClass="RadWindow" Modal="true" 
                       Behaviors=" Close, Move" Width="660px" Height="368px" Title="Chat"
                       DestroyOnClose="true">
                       
        <ContentTemplate>
            <div style="text-align:left;">
                <hcc:MuestraChat ID="controlChat" runat="server" />
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
    <script type="text/javascript">
        function MostrarVentanaOmitir() {
            var oWindow = $find("<%= RadWindowOmitir.ClientID %>");
            oWindow.show();
        };
        function CerrarVentanaOmitir() {
            var oWindow = $find("<%= RadWindowOmitir.ClientID %>");
            oWindow.close();
        };
        function MostrarChat() {
            var oWindow = $find("<%= rwChat.ClientID %>");
            if (oWindow != null) {
                oWindow.show();
            }
        };

        var nombreBoton = '<%=cmdChat.ClientID%>';

        var blink = function () {
            degradado();
        };

        var retroceso = false;
        var color_inicio = new Array(119, 136, 153);
        var color_fin = new Array(238, 238, 238);
        var pasos = 100;
        var iteracion = 7;
        var color_actual = new Array(3);
        var diferencia = new Array(3);

        for (i = 0; i < 3; i++)
            diferencia[i] = (color_fin[i] - color_inicio[i]) / pasos;

        function convierteHexadecimal(num) {
            return (num).toString(16);
        };

        function degradado() {
            if (iteracion == pasos)
                retroceso = true;
            else if (iteracion == 7)
                retroceso = false;
            if (!retroceso) {
                iteracion += 1
                for (i = 0; i < 3; i++)
                    color_actual[i] = (iteracion * diferencia[i]) + color_inicio[i]
            }
            else {
                iteracion -= 1;
                for (i = 0; i < 3; i++)
                    color_actual[i] = color_fin[i] + ((iteracion - pasos) * diferencia[i]);
            }
            colorAplicar = convierteHexadecimal(Math.round(color_actual[0])) + convierteHexadecimal(Math.round(color_actual[1])) + convierteHexadecimal(Math.round(color_actual[2]));
            document.getElementById(nombreBoton).setAttribute('style', 'background-Color: #' + colorAplicar + ';');
        };
    </script>
</asp:Content>
