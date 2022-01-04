<%@ Page Title="Detalle de Alcance Geografico" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="AlcanceGeograficoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.AlcanceGeograficoDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src= "~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src= "~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
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
            <asp:Panel ID="PanelMaster" runat="server" 
                meta:resourcekey="PanelMasterResource1">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblAlcanceGeografico" runat="server" Text="Alcance Geográfico" 
                            Font-Bold="True" meta:resourcekey="lblAlcanceGeograficoResource1"></asp:Label>
                    </legend>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="lblFlujoServicio" runat="server" Text="Servicio:" 
                                    meta:resourcekey="lblFlujoServicioResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlIdFlujoServicio"  DataValueField="Id"  DataTextField="NombreServicio"  
                                                     EmptyMessage="Seleccione" 
                                                     ErrorMessage="Campo Obligatorio" runat="server" 
                                                     
                                    onselectedindexchanged="ddlIdFlujoServicio_SelectedIndexChanged" 
                                    AutoPostBack="True" CausesValidation="False" Culture="es-ES" 
                                    meta:resourcekey="ddlIdFlujoServicioResource1" />
                                <asp:RequiredFieldValidator ID="rfvIdFlujoServicio" runat="server" ErrorMessage="*" 
                                                            ControlToValidate="ddlIdFlujoServicio"  
                                    CssClass="validator" Width="25px" 
                                    meta:resourcekey="rfvIdFlujoServicioResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" 
                                    meta:resourcekey="lblSucursalResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlIdSucursal" runat="server" DataTextField="NombreSucursal"
                                                     DataValueField="Id"    
                                                     EmptyMessage="Seleccione" 
                                                     ErrorMessage="Campo Obligatorio" 
                                                     
                                    onselectedindexchanged="ddlIdSucursal_SelectedIndexChanged" AutoPostBack="True" 
                                    CausesValidation="False" Culture="es-ES" 
                                    meta:resourcekey="ddlIdSucursalResource1"  />
                                <asp:RequiredFieldValidator ID="rfvIdSucursal" runat="server" ErrorMessage="*" 
                                                            ControlToValidate="ddlIdSucursal"  
                                    CssClass="validator" Width="25px" meta:resourcekey="rfvIdSucursalResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdPais" runat="server" Text="País:" 
                                    meta:resourcekey="lblIdPaisResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlPais" runat="server" DataTextField="Nombre" 
                                                     DataValueField="Id" EmptyMessage="Seleccione" 
                                                     ErrorMessage="Campo Obligatorio" 
                                                     onselectedindexchanged="ddlPais_SelectedIndexChanged" 
                                    AutoPostBack="True" CausesValidation="False" Culture="es-ES" 
                                    meta:resourcekey="ddlPaisResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdDiv1" runat="server" Text="IdDiv1:" 
                                    meta:resourcekey="lblIdDiv1Resource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlDiv1" runat="server" DataTextField="NombreDivTer1" 
                                                     DataValueField="Id" EmptyMessage="Seleccione" 
                                                     ErrorMessage="Campo Obligatorio" 
                                                     onselectedindexchanged="ddlDiv1_SelectedIndexChanged" 
                                    CausesValidation="False" AutoPostBack="True" Culture="es-ES" 
                                    meta:resourcekey="ddlDiv1Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdDiv2" runat="server" Text="IdDiv2:" 
                                    meta:resourcekey="lblIdDiv2Resource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlDiv2" runat="server" DataTextField="NombreDivTer1" 
                                                     DataValueField="Id" EmptyMessage="Seleccione" 
                                                     ErrorMessage="Campo Obligatorio" 
                                                     onselectedindexchanged="ddlDiv2_SelectedIndexChanged" 
                                    CausesValidation="False" AutoPostBack="True" Culture="es-ES" 
                                    meta:resourcekey="ddlDiv2Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdDiv3" runat="server" Text="IdDiv3:" 
                                    meta:resourcekey="lblIdDiv3Resource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlDiv3" runat="server" DataTextField="NombreDivTer1" 
                                                     DataValueField="Id" EmptyMessage="Seleccione" 
                                                     ErrorMessage="Campo Obligatorio" Culture="es-ES" 
                                    meta:resourcekey="ddlDiv3Resource1" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <hcc:Publicacion id="Publicacion" runat="server"/>
                <hcc:Auditoria id="Auditoria" runat="server"/>
            </asp:Panel>
            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdServicioSucursal" EmptyMessage="Escriba IdServicioSucursal" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdServicioSucursal"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdPais" EmptyMessage="Escriba IdPais" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdPais"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdDiv1" EmptyMessage="Escriba IdDiv1" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdDiv1"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdDiv2" EmptyMessage="Escriba IdDiv2" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdDiv2"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdDiv3" EmptyMessage="Escriba IdDiv3" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdDiv3"/>
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
                onclientclick="$('form').clearForm();return false" 
                meta:resourcekey="cmdLimpiarResource1"  />
        </div>
</asp:Content>
