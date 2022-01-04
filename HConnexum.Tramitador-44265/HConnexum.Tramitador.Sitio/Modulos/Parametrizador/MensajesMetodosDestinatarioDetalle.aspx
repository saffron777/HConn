<%@ Page Title="Detalle de MensajesMetodosDestinatario" Language="C#" MasterPageFile="~/Master/Site.Master"  AutoEventWireup="True" CodeBehind="MensajesMetodosDestinatarioDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.MensajesMetodosDestinatarioDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Telefono.ascx" TagName="Telefono" TagPrefix="hcc" %>
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
        <asp:UpdatePanel ID="UpDatePanel" runat="server" UpdateMode="Always">
            <ContentTemplate>

                <asp:Panel ID="PanelMaster" runat="server" 
                           meta:resourcekey="PanelMasterResource1">
            
                    <fieldset>
                        <legend>
                            <asp:Label ID="lgLegendMensajesMetodosDestinatario" runat="server" 
                                       Text="Mensajes Metodos Destinatario" Font-Bold="True" 
                                       meta:resourcekey="lgLegendMensajesMetodosDestinatarioResource1" />
                        </legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIdPaso" runat="server" Text="Paso:" 
                                               meta:resourcekey="lblIdPasoResource1"/>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlIdPaso"  DataValueField="Id"  
                                                         DataTextField="Nombre"  runat="server" 
                                                         Enabled="False" Culture="es-ES" meta:resourcekey="ddlIdPasoResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIdMensaje" runat="server" Text="Mensaje:" 
                                               meta:resourcekey="lblIdMensajeResource1"/>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlIdMensaje" runat="server" DataTextField="Mensaje" 
                                                         DataValueField="Id" EmptyMessage="Seleccione" Culture="es-ES" 
                                                         meta:resourcekey="ddlIdMensajeResource1" />
                                    <asp:RequiredFieldValidator ID="rfvIdMensaje" runat="server" 
                                                                ErrorMessage="*" ControlToValidate="ddlIdMensaje"  CssClass="validator" 
                                                                Width="25px" meta:resourcekey="rfvIdMensajeResource1"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblValorBusqueda" runat="server" Text="Valor Busqueda:" 
                                               meta:resourcekey="lblValorBusquedaResource1"/>
                                </td>
                                <td align="center">
                                    <telerik:RadListBox ID="rlbListaCasosMovimientos" TransferToID="rlbListBoxCasosMovimientosAsociados" 
                                                        AllowTransfer="True" Width="220px" Height="200px" runat="server" 
                                                        DataTextField="Nombre" DataValueField="Id" SelectionMode="Multiple" 
                                                        CssClass="Item" Culture="es-ES" 
                                                        meta:resourcekey="rlbListaRolesResource1" >
                                        <ButtonSettings TransferButtons="All" />
                                    </telerik:RadListBox>
                                    <telerik:RadListBox ID="rlbListBoxCasosMovimientosAsociados" Width="200px" Height="200px" 
                                                        runat="server" DataValueField="Id" DataTextField="ValorBusqueda" 
                                                        SelectionMode="Multiple" CssClass="Item" Culture="es-ES" 
                                                        meta:resourcekey="rlbListaRolesAsociadosResource1" >
                                        <ButtonSettings TransferButtons="All" />
                                    </telerik:RadListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblRutina" runat="server" Text="Rutina:" 
                                               meta:resourcekey="lblRutinaResource1"/>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRutina" runat="server" 
                                                 meta:resourcekey="txtRutinaResource1" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl" runat="server" Text="Constantes:" 
                                               meta:resourcekey="lblResource1"/>
                                </td>
                                <td>
                                    <hcc:Telefono id="Telefono" runat="server" MaxLength="5000"/>
                                    <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" 
                                                Text="Agregar" CausesValidation="False" 
                                                meta:resourcekey="btnAgregarResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <hcc:MultilineCounter id="MultilineCounter" runat="server" MaxLength="5000"/>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>

                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <hcc:Publicacion id="Publicacion" runat="server"/>
                    <hcc:Auditoria id="Auditoria" runat="server"/>

                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAgregar" />
            </Triggers>
        </asp:UpdatePanel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdPaso" EmptyMessage="Escriba IdPaso" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdPaso"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdMetodo" EmptyMessage="Escriba IdMetodo" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdMetodo"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdMensaje" EmptyMessage="Escriba IdMensaje" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdMensaje"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorTipoBusquedaDestinatario" EmptyMessage="Escriba TipoBusquedaDestinatario" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtTipoBusquedaDestinatario"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:TextBoxSetting   BehaviorID="BehaviorValorBusqueda" EmptyMessage="Escriba ValorBusqueda"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtValorBusqueda"/>
                </TargetControls>
            </telerik:TextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorTipoPrivacidad" EmptyMessage="Escriba TipoPrivacidad" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtTipoPrivacidad"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>               
        <br />
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                    onclick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1"  />
        <asp:Button ID="cmdGuardaryAgregar" runat="server" 
                    Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click" 
                    Visible="False" meta:resourcekey="cmdGuardaryAgregarResource1"/>
    </div>
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
        <script type="text/javascript">
            $("#" + '<%=  MultilineCounter.FindControl("txtControl").ClientID %>').live("keypress", function (event) {

                if (event.which != 8) {

                    event.preventDefault();
                }
            });

            function IrAnterior() {
                var wnd = GetRadWindow();
                wnd.setUrl('<%= RutaPadreEncriptada %>');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>

