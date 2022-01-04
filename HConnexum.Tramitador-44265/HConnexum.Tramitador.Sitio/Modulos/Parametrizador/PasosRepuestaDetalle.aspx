<%@ Page Title="Detalle de PasosRepuesta" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PasosRepuestaDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.PasosRepuestaDetalle" meta:resourcekey="PageResource1" %>
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
        <asp:Panel ID="PanelMaster" runat="server" 
                   meta:resourcekey="PanelMasterResource1">
            <fieldset>
                <legend>
                    <asp:Label ID="lgLegendPasosRepuesta" runat="server" 
                               Text="Pasos Repuesta" Font-Bold="True" 
                               meta:resourcekey="lgLegendPasosRepuestaResource1" />
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblIdRespuesta" runat="server" Text="Respuesta:" 
                                       meta:resourcekey="lblIdRespuestaResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValorRespuesta" runat="server" 
                                         meta:resourcekey="txtValorRespuestaResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"
                                       meta:resourcekey="lblDescripcionResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescripcion" runat="server" 
                                         meta:resourcekey="txtDescripcionResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSecuencia" runat="server" Text="Secuencia:" 
                                       meta:resourcekey="lblSecuenciaResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSecuencia" runat="server" 
                                         meta:resourcekey="txtSecuenciaResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIndCierre" runat="server" Text="IndCierre:" 
                                       meta:resourcekey="lblIndCierreResource1"/>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIndCierre" runat="server" 
                                          meta:resourcekey="chkIndCierreResource1"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hcc:Publicacion id="Publicacion" runat="server"/>
            <hcc:Auditoria id="Auditoria" runat="server"/>
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:TextBoxSetting   BehaviorID="BehaviorValor" EmptyMessage="Escriba Respuesta"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtValorRespuesta"/>
                </TargetControls>
            </telerik:TextBoxSetting>
            <telerik:TextBoxSetting   BehaviorID="BehaviorDescripcion" EmptyMessage="Escriba Descripción"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="txtDescripcion"/>
                </TargetControls>
            </telerik:TextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorSecuencia" EmptyMessage="Escriba Secuencia" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtSecuencia"/>
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