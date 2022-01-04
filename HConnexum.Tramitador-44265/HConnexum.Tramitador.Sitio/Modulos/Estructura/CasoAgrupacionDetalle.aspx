<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Detalle de CasoAgrupacion" CodeBehind="CasoAgrupacionDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.CasoAgrupacionDetalle" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
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
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset>
                <legend>
                    <b>
                        <asp:Label runat="server" Text="CasoAgrupacion" Font-Bold="True" meta:resourcekey="LblLegendCasoAgrupacionResource"/>
                    </b>
                </legend>
                <table align="center">
                    <tr>
                        <td><asp:Label ID="lgLegend" runat="server" 
                                   Font-Bold="True" Text="Grupos" /></td>
                        <td>
                            <telerik:RadListBox ID="rlbListaCasosAgrupaciones" runat="server" 
                                                AllowTransfer="True" CssClass="Item" Culture="es-ES" DataTextField="NombreAgrupacion" 
                                                DataValueField="Id" Height="200px" meta:resourcekey="rlbListaRolesResource1" 
                                                SelectionMode="Multiple" TransferToID="rlbListaCasosAgrupacionesAsociados" 
                                                Width="220px">
                                <ButtonSettings TransferButtons="All" />
                            </telerik:RadListBox>
                            <telerik:RadListBox ID="rlbListaCasosAgrupacionesAsociados" runat="server" 
                                                CssClass="Item" Culture="es-ES" DataValueField="Id" DataTextField="NombreAgrupacion" 
                                                Height="200px"  SelectionMode="Multiple" 
                                                Width="200px">
                                <ButtonSettings TransferButtons="All" />
                            </telerik:RadListBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdCaso" EmptyMessage="Escriba IdCaso" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdCaso"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdAgrupacion" EmptyMessage="Escriba IdAgrupacion" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdAgrupacion"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdServicio" EmptyMessage="Escriba IdServicio" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdServicio"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdSolicitud" EmptyMessage="Escriba IdSolicitud" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdSolicitud"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdSuscriptor" EmptyMessage="Escriba IdSuscriptor" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdSuscriptor"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>
        <br />
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" onclick="cmdGuardar_Click"/>
        <asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click"/>
        <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar"  onclientclick="$('form').clearForm();

                    return false"/>
        <asp:Button ID="cmdNuevaAgrupacion" runat="server" Text="Nueva Agrupacion" OnClientClick="AbrirVentanas(this);

                    return false;"/>
    </div>
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock2">
        <script type="text/javascript">
            function AbrirVentanas(sender) {
                var wnd = GetRadWindow();
                var idMenu = '<%= IdMenuEncriptado %>';
                switch ($(sender).attr("Id")) {
                    case '<%=  cmdNuevaAgrupacion.ClientID %>':
                        wnd.setUrl("Modulos/Estructura/AgrupacionLista.aspx?IdMenu=" + idMenu + "&");
                        break;
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>