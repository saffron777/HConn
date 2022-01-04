<%@ Page Title="Detalle de ChadePaso" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="ChadePasoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.ChadePasoDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
                            DefaultLoadingPanelID="RadAjaxLoadingPanel1" 
                            meta:resourcekey="RadAjaxManager1Resource1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridDetails">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelMaster" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div>
        <asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
            <fieldset>
                <legend>
                    <asp:Label ID="lblCHAPaso" runat="server" Text="CHA del Paso" Font-Bold="True" 
                               meta:resourcekey="lblCHAPasoResource1"></asp:Label>
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdCargosuscriptor" runat="server" Text="Cargo Suscriptor:" 
                                       meta:resourcekey="lblIdCargosuscriptorResource1"/>
                        </td>
                        <td>				
                            <telerik:RadComboBox ID="ddlIdCargosuscriptor"  DataValueField="Id"  
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio" runat="server" Culture="es-ES" 
                                                 Width="300" meta:resourcekey="ddlIdCargosuscriptorResource1" >

                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="rfvIdCargosuscriptor" runat="server" 
                                                        ErrorMessage="*" ControlToValidate="ddlIdCargosuscriptor"  CssClass="validator" 
                                                        Width="25px" meta:resourcekey="rfvIdCargosuscriptorResource1"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdHabilidadSuscriptor" runat="server" 
                                       Text="Habilidad Suscriptor:" 
                                       meta:resourcekey="lblIdHabilidadSuscriptorResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdHabilidadSuscriptor"  DataValueField="Id"  
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio" runat="server" Culture="es-ES" 
                                                 Width="300" meta:resourcekey="ddlIdHabilidadSuscriptorResource1" >

                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdAutonomiaSuscriptor" runat="server" 
                                       Text="Autonomía Suscriptor:" 
                                       meta:resourcekey="lblIdAutonomiaSuscriptorResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdAutonomiaSuscriptor"  
                                                 DataValueField="Id"  DataTextField="Nombre"  
                                                 EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" runat="server" 
                                                 Culture="es-ES" meta:resourcekey="ddlIdAutonomiaSuscriptorResource1" >
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hcc:Publicacion id="Publicacion" runat="server"/>
            <hcc:Auditoria id="Auditoria" runat="server"/>
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdPasos" EmptyMessage="Escriba IdPasos" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdPasos"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdCargosuscriptor" EmptyMessage="Escriba IdCargosuscriptor" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdCargosuscriptor"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdHabilidadSuscriptor" EmptyMessage="Escriba IdHabilidadSuscriptor" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdHabilidadSuscriptor"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdAutonomiaSuscriptor" EmptyMessage="Escriba IdAutonomiaSuscriptor" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdAutonomiaSuscriptor"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>
        <br />
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                    onclick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1"  />
        <asp:Button ID="cmdGuardaryAgregar" runat="server" 
                    Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click" 
                    meta:resourcekey="cmdGuardaryAgregarResource1"  />
        <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar"  
                    onclientclick="$('form').clearForm();

                    return false" 
                    meta:resourcekey="cmdLimpiarResource1"  />
    </div>
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
        <script type="text/javascript">
            function IrAnterior() {
                var wnd = GetRadWindow();
                wnd.setUrl('<%= RutaPadreEncriptada %>');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>