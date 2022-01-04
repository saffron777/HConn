<%@ Page Title="Detalle de Documentos del Paso" Language="C#" MasterPageFile="~/Master/Site.Master"  AutoEventWireup="true" CodeBehind="DocumentosPasoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.DocumentosPasoDetalle" meta:resourcekey="PageResource1" %>
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
        <asp:Panel ID="pnlCabecera" runat="server" 
                   meta:resourcekey="pnlCabeceraResource1">
            <fieldset>
                <legend>
                    <asp:Label ID="Label1" runat="server" Text="Datos de Paso" Font-Bold="True" 
                               meta:resourcekey="Label1Resource1"  />
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblServicio" runat="server" Text="Servicio:" 
                                       meta:resourcekey="lblServicioResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtServicio" runat="server" ReadOnly="True" 
                                         meta:resourcekey="txtServicioResource1" />
                        </td>

                        <td>
                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus:" 
                                       meta:resourcekey="lblEstatusResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstatus" runat="server" ReadOnly="True" 
                                         meta:resourcekey="txtEstatusResource1" />
                        </td>

                        <td>
                            <asp:Label ID="lblVersion" runat="server" Text="Versión:" 
                                       meta:resourcekey="lblVersionResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersion" runat="server" ReadOnly="True" 
                                         meta:resourcekey="txtVersionResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEtapa" runat="server" Text="Etapa:" 
                                       meta:resourcekey="lblEtapaResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEtapa" runat="server" ReadOnly="True" 
                                         meta:resourcekey="txtEtapaResource1" />
                        </td>
                        <td>
                            <asp:Label ID="lblPaso" runat="server" Text="Paso:" 
                                       meta:resourcekey="lblPasoResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPaso" runat="server" ReadOnly="True" 
                                         meta:resourcekey="txtPasoResource1" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
            <fieldset>
                <legend>
                    ><asp:Label ID="Label2" runat="server" Text="Documentos para el Paso" 
                                Font-Bold="True" meta:resourcekey="Label2Resource1" />
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdDocumentoServicio" runat="server" 
                                       Text="Documentos del Servicio:" 
                                       meta:resourcekey="lblIdDocumentoServicioResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdDocumentoServicio"  DataValueField="Id"  
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio" runat="server" Culture="es-ES" 
                                                 meta:resourcekey="ddlIdDocumentoServicioResource1" />
                            <asp:RequiredFieldValidator ID="rfvIdDocumentoServicio" runat="server" 
                                                        ErrorMessage="*" ControlToValidate="ddlIdDocumentoServicio"  
                                                        CssClass="validator" Width="25px" 
                                                        meta:resourcekey="rfvIdDocumentoServicioResource1"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hcc:Publicacion id="Publicacion" runat="server"/>
            <hcc:Auditoria id="Auditoria" runat="server"/>
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdDocumentoServicio" EmptyMessage="Escriba IdDocumentoServicio" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdDocumentoServicio"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdPasoPrograma" EmptyMessage="Escriba IdPasoPrograma" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdPasoPrograma"/>
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
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
        <script type="text/javascript">
            function IrAnterior() {
                var wnd = GetRadWindow();
                wnd.setUrl('<%= RutaPadreEncriptada %>');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>