
<%@ Page Language="C#" MasterPageFile="~/Master/Site.Master" Title="Detalle de los Documentos de Servicio" AutoEventWireup="true" CodeBehind="DocumentosServicioDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.DocumentosServicioDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src= "~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src= "~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>
        <asp:Label runat="server" Text="Detalle de DocumentosServicio"></asp:Label>
    </title>
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
        <asp:Panel ID="pnlCabecera" runat="server" meta:resourcekey="pnlCabeceraResource1">
            <fieldset>
                <legend>
                    <asp:Label ID="Label1" runat="server" Text="Datos del Servicio" 
                               Font-Bold="True" meta:resourcekey="Label1Resource1" ></asp:Label>
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblServicio" runat="server" Text="Servicio:" 
                                       meta:resourcekey="lblServicioResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtServicio" runat="server" ReadOnly="True" 
                                         ValidationGroup="Cabecera" meta:resourcekey="txtServicioResource1" />
                        </td>

                        <td>
                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus:" 
                                       meta:resourcekey="lblEstatusResource1" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtEstatus" runat="server" ReadOnly="True" 
                                         ValidationGroup="Cabecera" meta:resourcekey="txtEstatusResource1" />
                        </td>
			   
                        <td>
                            <asp:Label ID="lblVersion" runat="server" Text="Versi?n:" 
                                       meta:resourcekey="lblVersionResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersion" runat="server" ReadOnly="True" 
                                         ValidationGroup="Cabecera" meta:resourcekey="txtVersionResource1" />
                        </td>
                    </tr>

                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
            <fieldset>
                <legend>
                    <asp:Label ID="Label2" runat="server" Text="DocumentosServicio" 
                               Font-Bold="True" meta:resourcekey="Label2Resource1" ></asp:Label>
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdDocumento" runat="server" Text="IdDocumento:" 
                                       meta:resourcekey="lblIdDocumentoResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdDocumento"  DataValueField="Id"  
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio" runat="server" Culture="es-ES" 
                                                 meta:resourcekey="ddlIdDocumentoResource1" />
                            <asp:RequiredFieldValidator ID="rfvIdDocumento" runat="server" ErrorMessage="*" 
                                                        ControlToValidate="ddlIdDocumento"  CssClass="validator" Width="25px" 
                                                        meta:resourcekey="rfvIdDocumentoResource1"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIndDocObligatorio" runat="server" Text="IndDocObligatorio:" 
                                       meta:resourcekey="lblIndDocObligatorioResource1"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIndDocObligatorio" runat="server" 
                                          meta:resourcekey="chkIndDocObligatorioResource1"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIndVisibilidad" runat="server" Text="IndVisibilidad:" 
                                       meta:resourcekey="lblIndVisibilidadResource1"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIndVisibilidad" runat="server" 
                                          meta:resourcekey="chkIndVisibilidadResource1"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hcc:Publicacion id="Publicacion" runat="server"/>
            <hcc:Auditoria id="Auditoria" runat="server"/>
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdFlujoServicio" EmptyMessage="Escriba IdFlujoServicio" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdFlujoServicio"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdDocumento" EmptyMessage="Escriba IdDocumento" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdDocumento"/>
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

    

