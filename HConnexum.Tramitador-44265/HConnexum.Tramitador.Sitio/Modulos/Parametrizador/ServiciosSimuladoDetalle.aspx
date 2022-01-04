<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Site.Master"
    Title="Servicios Simulado Detalle" CodeBehind="ServiciosSimuladoDetalle.aspx.cs"
    Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.ServiciosSimuladoDetalle" %>

<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
.RadComboBox_Default{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox{vertical-align:middle;display:-moz-inline-stack;display:inline-block}.RadComboBox{text-align:left}.RadComboBox_Default{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox{vertical-align:middle;display:-moz-inline-stack;display:inline-block}.RadComboBox{text-align:left}.RadComboBox_Default{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox{vertical-align:middle;display:-moz-inline-stack;display:inline-block}.RadComboBox{text-align:left}.RadComboBox *{margin:0;padding:0}.RadComboBox *{margin:0;padding:0}.RadComboBox *{margin:0;padding:0}.RadComboBox_Default .rcbInputCellLeft{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbInputCellLeft{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCellLeft{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbInputCellLeft{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCellLeft{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbInputCellLeft{background-color:transparent;background-repeat:no-repeat}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox .rcbInput{text-align:left}.RadComboBox_Default .rcbInput{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox .rcbInput{text-align:left}.RadComboBox_Default .rcbInput{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox .rcbInput{text-align:left}.RadComboBox_Default .rcbArrowCellRight{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbArrowCellRight{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCellRight{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbArrowCellRight{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCellRight{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbArrowCellRight{background-color:transparent;background-repeat:no-repeat}</style>
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
                <legend><b>
                    <asp:Label runat="server" Text="ServiciosSimulado" Font-Bold="True" meta:resourcekey="LblLegendServiciosSimuladoResource" /></b></legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblNombreServicio" runat="server" Text="Nombre Servicio:" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdServicioSuscriptor" runat="server" 
                                Culture="es-ES" DataTextField="NombreServicio" DataValueField="IdServicioSuscriptor" 
                                EmptyMessage="Seleccione..." meta:resourcekey="ddlIdPasoInicialResource1" 
                                AutoPostBack="True"  CausesValidation="false" Width="250px"
                                onselectedindexchanged="DdlIdServicioSuscriptorSelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio:" />
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtFechaInicio" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="true" BorderColor="Red" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                            <asp:CustomValidator ID="cvdFechaInicio" runat="server" EnableClientScript="true"
                                ValidateEmptyText="true" ErrorMessage="Campo requerido" ControlToValidate="txtFechaInicio"
                                ClientValidationFunction="validatePicker" Display="None"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdSuscriptorASimular" runat="server" Text="Suscriptor:" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdSuscriptorASimular" runat="server" 
                                Culture="es-ES" DataTextField="Nombre" DataValueField="Id" Width="250px"
                                EmptyMessage="Seleccione...">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Fin:" />
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtFechaFin" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="true" BorderColor="Red" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDatePicker>
                            <asp:CustomValidator ID="cvdFechaFin" runat="server" EnableClientScript="true" ValidateEmptyText="true"
                                ErrorMessage="Campo requerido" ControlToValidate="txtFechaFin" ClientValidationFunction="validatePicker"
                                Display="None"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hcc:Publicacion ID="Publicacion" runat="server" />
            <hcc:Auditoria ID="Auditoria" runat="server" />
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdServicioSuscriptor"
                EmptyMessage="Escriba IdServicioSuscriptor" Type="Number" Validation-IsRequired="True"
                ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdServicioSuscriptor" />
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdSuscriptorASimular"
                EmptyMessage="Escriba IdSuscriptorASimular" Type="Number" Validation-IsRequired="True"
                ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdSuscriptorASimular" />
                </TargetControls>
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>
        <br />
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="CmdGuardarClick" />
        <asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro"
            OnClick="CmdGuardaryAgregarClick" />
        <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="$('form').clearForm();return false" />
    </div>
</asp:Content>
