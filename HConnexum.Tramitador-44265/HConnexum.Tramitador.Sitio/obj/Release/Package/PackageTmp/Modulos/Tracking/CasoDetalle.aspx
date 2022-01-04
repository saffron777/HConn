<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Detalle de Caso" CodeBehind="CasoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.CasoDetalle" %>
<%@ Register Src="~/Modulos/Bloques/MuestraChat.ascx" TagName="MuestraChat" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server"></asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <script type="text/javascript">
        var idMenu = '<%= IdMenuEncriptado %>';
        var ventanaDetalle = "Modulos/tracking/ObservacionesMovimientoDetalle.aspx?IdMenu=" + idMenu + "&";
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
<%--    <style type="text/css">
    .TelerikModalOverlay     
    {     
      width:100% !important;
      height:100% !important;
    }
    </style>--%>

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
        <!--<br />-->
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset >
                <legend>
                    <b>
                        <asp:Label runat="server" Text="Caso" Font-Bold="True" meta:resourcekey="LblLegendCasoResource"/>
                    </b>
                </legend>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblCaso" runat="server" 
                                       Text="Nro. de caso :" Font-Bold="True"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCasoNumero" runat="server" Enabled="False" Width="75px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus:" 
                                       Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstatus" runat="server" Enabled="False" Width="150px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSolicitud" runat="server" 
                                       Text="Solicitud:" Font-Bold="True"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIdSolicitud" runat="server" Enabled="False" Width="75px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblFechaSolicitud" runat="server" 
                                       Text="Fecha solicitud:" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaSolicitud" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSuscriptor" runat="server" 
                                       Text="Suscriptor:" Font-Bold="True"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTipoDoc" runat="server" Enabled="False" Width="40px" />
                            <asp:TextBox ID="txtDocSolicitante" runat="server" Enabled="False" Width="70px" />
                            <asp:TextBox ID="txtSuscriptor" runat="server" Enabled="False" Width="170px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha creación:" 
                                       Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaCreacion2" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCreadorPor" runat="server" 
                                       Text="Creado por:" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreadorPor" runat="server" Enabled="False" Width="300px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Versión:" 
                                       Font-Bold="True"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersion" runat="server" Enabled="False" Width="75px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdServicio" runat="server" 
                                       Text="Servicio:" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtServicio" runat="server" Enabled="False" Width="300px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblFechaRechazo" runat="server"  
                                       Text="Fecha rechazo:" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaRechazo" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFechaAnulacion" runat="server" 
                                       Text="Fecha anulación:" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaAnulacion" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblFechaModificacion" runat="server" 
                                       Text="Fecha modificación:" Font-Bold="True"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaModificacion" runat="server" Enabled="false" Width="150px" Text="No disponible"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblModificadoPor2" runat="server" 
                                       Text="Modificado por:" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtModificado" runat="server" Enabled="False" Width="150px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblPrioridadAtencion" runat="server"  
                                       Text="Prioridad atención:" Font-Bold="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrioridadAtencion" runat="server" Enabled="False" Width="150px" />
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="text-align:right; padding-top: 5px; padding-left:25px; padding-bottom: 5px;">
                            <asp:Button ID="cmdChat" runat="server" CausesValidation="False" 
                                OnClientClick="MostrarChat(); return false;" 
                                UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <div style="height:10px;"></div>
        <table  width="100%">
            <tr>
                <td>
                    <!--<br />-->
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CssClass="cssRadMultiPage"
                        MultiPageID="RadMultiPage1" SelectedIndex="0" Width="100%">
                        <Tabs>
                            <telerik:RadTab Text="Datos generales" Selected="True"></telerik:RadTab>
                            <telerik:RadTab Text="Movimientos"></telerik:RadTab>
                            <telerik:RadTab Text="Mensajes"></telerik:RadTab>
                            <telerik:RadTab Text="Tiempos"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" 
                        CssClass="cssRadMultiPage">
                        <telerik:RadPageView ID="RadPageView4" runat="server" Width="100%" Height="800px" />
                        <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" Height="380px" />
                        <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%" Height="380px" />
                        <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%" Height="218px" />
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadWindow runat="server" ID="rwChat" CssClass="RadWindow" Modal="true" 
                       Behaviors="Maximize, Close, Move" Width="660px" Height="368px" Title="Chat" 
                       DestroyOnClose="True">
        <ContentTemplate>
            <div style="text-align:left;">
                <hcc:MuestraChat ID="controlChat" runat="server" />
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>